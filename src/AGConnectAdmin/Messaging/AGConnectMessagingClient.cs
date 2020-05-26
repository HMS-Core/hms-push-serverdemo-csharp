/* Copyright 2018, Google Inc. All rights reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 2019.12.27-Changed implement
 */

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Collections.Generic;
using AGConnectAdmin.Auth;

namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// A client for making authorized HTTP calls to the HCM backend service. Handles request
    /// serialization, response parsing, and HTTP error handling.
    /// </summary>
    internal sealed class AGConnectMessagingClient : IDisposable
    {
        private const string SUCCESS_CODE = "80000000";
        private readonly HttpClient httpClient;
        private string TokenUrl;
        private readonly string sendUrl;
        private AppOptions options;

        private TokenResponse tokenResponse;
        private Task<string> accessTokenTask;
        private object accessTokenTaskLock = new object();

        public AGConnectMessagingClient(AppOptions options)
        {
            this.options = options;        
            TokenUrl = options.LoginUri;
            sendUrl = options.GetApiUri();
#if PROXY
            httpClient = new HttpClient(new HttpClientHandler()
            {
                UseProxy = true,
                // Use cntlm proxy setting to accesss internet.
                Proxy = new WebProxy("localhost", 3128),
            });
#else
            httpClient = new HttpClient();
#endif
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        /// <summary>
        /// Sends a message to the AGC service for delivery. The message gets validated both by
        /// the Admin SDK, and the remote AGC service. A successful return value indicates
        /// that the message has been successfully sent to AGC, where it has been accepted by the
        /// AGC service.
        /// </summary>
        /// <returns>A task that completes with a message ID string, which represents
        /// successful handoff to AGC.</returns>
        /// <exception cref="ArgumentNullException">If the message argument is null.</exception>
        /// <exception cref="ArgumentException">If the message contains any invalid
        /// fields.</exception>
        /// <exception cref="AGConnectException">If an error occurs while sending the
        /// message.</exception>
        /// <param name="message">The message to be sent. Must not be null.</param>
        /// <param name="dryRun">A boolean indicating whether to perform a dry run (validation
        /// only) of the send. If set to true, the message will be sent to the AGC backend service,
        /// but it will not be delivered to any actual recipients.</param>
        /// <param name="cancellationToken">A cancellation token to monitor the asynchronous
        /// operation.</param>
        public async Task<string> SendAsync(
            Message message,
            bool dryRun = false,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var sendRequest = new SendRequest()
                {
                    Message = (message.ThrowIfNull(nameof(message)) as MessagePart<Message>).CopyAndValidate(),
                    ValidateOnly = dryRun,
                };
                var accessToken = await GetAccessTokenAsync().ConfigureAwait(false);
                var payload = NewtonsoftJsonSerializer.Instance.Serialize(sendRequest);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(sendUrl),
                    Content = content,
                };

                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var error = $"Response status code does not indicate success: {response.StatusCode}";
                    throw new AGConnectException(error);
                }
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var parsed = JsonConvert.DeserializeObject<SingleMessageResponse>(json);
                if (parsed.Code != SUCCESS_CODE)
                {
                    throw new AGConnectException($"Send message failed: {parsed.Code}{Environment.NewLine}{parsed.Message}");
                }
                return parsed.RequestId;
            }
            catch (HttpRequestException e)
            {
                throw new AGConnectException("Error while calling the AGC messaging service.", e);
            }
        }

        /// <summary>
        /// Subscribes a list of registration tokens to a topic.
        /// </summary>
        /// <param name="registrationTokens">A list of registration tokens to subscribe.</param>
        /// <param name="topic">The topic name to subscribe to.</param>
        /// <returns>A task that completes with a <see cref="TopicManagementResponse"/>, giving details about the topic subscription operations.</returns>
        public async Task<TopicManagementResponse> SubscribeToTopicAsync(
            IReadOnlyList<string> registrationTokens, string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Topic can not be empty");
            }
            if (registrationTokens == null)
            {
                throw new ArgumentException("Registration tokens can not be null");
            }
            if (registrationTokens.Count == 0)
            {
                throw new ArgumentException("Registration tokens can not be empty");
            }
            if (registrationTokens.Count > 1000)
            {
                throw new ArgumentException("Registration token list must not contain more than 1000 tokens");
            }

            var accessToken = await GetAccessTokenAsync().ConfigureAwait(false);
            var body = new TopicManagementRequest()
            {
                Topic = topic,
                Tokens = new List<string>(registrationTokens)
            };
            var url = $"{options.ApiBaseUri}/v1/{options.ClientId}/topic:subscribe";
            var payload = NewtonsoftJsonSerializer.Instance.Serialize(body);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = content,
            };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var resp = await httpClient.SendAsync(request).ConfigureAwait(false);
            var json = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            return NewtonsoftJsonSerializer.Instance.Deserialize<TopicManagementResponse>(json);
        }

        /// <summary>
        /// Unsubscribes a list of registration tokens from a topic.
        /// </summary>
        /// <param name="registrationTokens">A list of registration tokens to unsubscribe.</param>
        /// <param name="topic">The topic name to unsubscribe from.</param>
        /// <returns>A task that completes with a <see cref="TopicManagementResponse"/>, giving details about the topic unsubscription operations.</returns>
        public async Task<TopicManagementResponse> UnsubscribeFromTopicAsync(
            IReadOnlyList<string> registrationTokens, string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Topic can not be empty");
            }
            if (registrationTokens == null)
            {
                throw new ArgumentException("Registration tokens can not be null");
            }
            if (registrationTokens.Count == 0)
            {
                throw new ArgumentException("Registration tokens can not be empty");
            }
            if (registrationTokens.Count > 1000)
            {
                throw new ArgumentException("Registration token list must not contain more than 1000 tokens");
            }

            var accessToken = await GetAccessTokenAsync().ConfigureAwait(false);
            var body = new TopicManagementRequest()
            {
                Topic = topic,
                Tokens = new List<string>(registrationTokens)
            };
            var url = $"{options.ApiBaseUri}/v1/{options.ClientId}/topic:unsubscribe";
            var payload = NewtonsoftJsonSerializer.Instance.Serialize(body);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = content,
            };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var resp = await httpClient.SendAsync(request).ConfigureAwait(false);
            var json = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            return NewtonsoftJsonSerializer.Instance.Deserialize<TopicManagementResponse>(json);
        }

        /// <summary>
        /// Retrieve a list of topics that the specified registration token subscribe to.
        /// </summary>
        /// <param name="registrationToken">The topic name to unsubscribe from.</param>
        /// <returns>A task that completes with a <see cref="TopicListResponse"/>, giving details about the topic retrieve operations.</returns>
        public async Task<TopicListResponse> GetTopicListAsync(string registrationToken)
        {
            if (string.IsNullOrWhiteSpace(registrationToken))
            {
                throw new ArgumentException("Registration token can not be empty");
            }
            var accessToken = await GetAccessTokenAsync().ConfigureAwait(false);
            var body = new TopicListRequest()
            {
                Token = registrationToken
            };
            var url = $"{options.ApiBaseUri}/v1/{options.ClientId}/topic:list";
            var payload = NewtonsoftJsonSerializer.Instance.Serialize(body);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = content,
            };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var resp = await httpClient.SendAsync(request).ConfigureAwait(false);
            var json = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            return NewtonsoftJsonSerializer.Instance.Deserialize<TopicListResponse>(json);
        }

        private async Task<string> GetAccessTokenAsync()
        {
            string accessToken = tokenResponse?.GetValidAccessToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                lock (accessTokenTaskLock)
                {
                    if (accessTokenTask == null)
                    {
                        // access token task may be shared by multiple messaging task, thus should not pass cancellationToken to it
                        accessTokenTask = RequestAccessTokenAsync(default);
                    }
                }

                accessToken = await accessTokenTask.ConfigureAwait(false);

                lock (accessTokenTaskLock)
                {
                    accessTokenTask = null;
                }
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                var error = "AccessToken is null or empty";
                throw new AGConnectException(error);
            }

            return accessToken;
        }

        private async Task<string> RequestAccessTokenAsync(CancellationToken cancellationToken)
        {
            string content = string.Format("grant_type=client_credentials&client_secret={0}&client_id={1}", options.ClientSecret, options.ClientId);
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(TokenUrl),
                Content = new StringContent(content),
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var error = $"Response status code does not indicate success: {response.StatusCode}";
                throw new AGConnectException(error);
            }

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var parsed = JsonConvert.DeserializeObject<TokenResponse>(json);
            switch (parsed.Error)
            {
                case 1101:
                    throw new AGConnectException("Get access token failed: invalid request");
                case 1102:
                    throw new AGConnectException("Get access token failed: missing required param");
                case 1104:
                    throw new AGConnectException("Get access token failed: unsupported response type");
                case 1105:
                    throw new AGConnectException("Get access token failed: unsupported grant type");
                case 1107:
                    throw new AGConnectException("Get access token failed: access denied");
                case 1201:
                    throw new AGConnectException("Get access token failed: invalid ticket");
                case 1202:
                    throw new AGConnectException("Get access token failed: invalid sso_st");
                default:
                    break;
            }

            var token = parsed.GetValidAccessToken();
            if (string.IsNullOrEmpty(token))
            {
                var error = "AccessToken return by AGC is null or empty";
                throw new AGConnectException(error);
            }

            tokenResponse = parsed;
            return token;
        }
    }
}
