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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("AGConnectAdmin.Tests,PublicKey=" +
"0024000004800000940000000602000000240000525341310004000001000100cd7e49c156e0f4" +
"abc12b5177925ac69822032c9fad95fb82c8a79d2ff277e3c541311856b65c2e1b67d8b3964a65" +
"bfb1c09bbca8475e8234c6ba56004f568db317c65fcd067fcda1972a97d623c20eb4c19cc6d450" +
"f2233fd5bde42943f6d08dec916414f76b3187439603176223bda09146c1836a64b6b5bd36e9cd" +
"435643ba")]
namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// This is the entry point to all server-side AGConnect Cloud Messaging (HCM) operations. You
    /// can get an instance of this class via <c>AGConnectMessaging.DefaultInstance</c>.
    /// </summary>
    public sealed class AGConnectMessaging : IAGConnectService
    {
        private readonly AGConnectMessagingClient messagingClient;

        private AGConnectMessaging(AGConnectApp app)
        {
            this.messagingClient = new AGConnectMessagingClient(app.Options);
        }

        /// <summary>
        /// Gets the messaging instance associated with the default AGConnect app. This property is
        /// <c>null</c> if the default app doesn't yet exist.
        /// </summary>
        public static AGConnectMessaging DefaultInstance
        {
            get
            {
                var app = AGConnectApp.DefaultInstance;
                if (app == null)
                {
                    return null;
                }

                return GetMessaging(app);
            }
        }

        /// <summary>
        /// Returns the messaging instance for the specified app.
        /// </summary>
        /// <returns>The <see cref="AGConnectMessaging"/> instance associated with the specified
        /// app.</returns>
        /// <exception cref="System.ArgumentNullException">If the app argument is null.</exception>
        /// <param name="app">An app instance.</param>
        internal static AGConnectMessaging GetMessaging(AGConnectApp app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.GetOrInit(typeof(AGConnectMessaging).Name, () =>
            {
                return new AGConnectMessaging(app);
            });
        }

        /// <summary>
        /// Sends a message to the HCM service for delivery. The message gets validated both by
        /// the Admin SDK, and the remote HCM service. A successful return value indicates
        /// that the message has been successfully sent to HCM, where it has been accepted by the
        /// AGC service.
        /// </summary>
        /// <returns>A task that completes with a message ID string, which represents
        /// successful handoff to HCM.</returns>
        /// <exception cref="ArgumentNullException">If the message argument is null.</exception>
        /// <exception cref="ArgumentException">If the message contains any invalid
        /// fields.</exception>
        /// <exception cref="AGConnectException">If an error occurs while sending the
        /// message.</exception>
        /// <param name="message">The message to be sent. Must not be null.</param>
        public async Task<string> SendAsync(Message message)
        {
            return await this.SendAsync(message, false).ConfigureAwait(false);
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
        /// <param name="cancellationToken">A cancellation token to monitor the asynchronous
        /// operation.</param>
        public async Task<string> SendAsync(Message message, CancellationToken cancellationToken)
        {
            return await this.SendAsync(message, false, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a message to the AGC service for delivery. The message gets validated both by
        /// the Admin SDK, and the remote AGC service. A successful return value indicates
        /// that the message has been successfully sent to AGC, where it has been accepted by the
        /// AGC service.
        /// <para>If the <paramref name="dryRun"/> option is set to true, the message will not be
        /// actually sent to the recipients. Instead, the AGC service performs all the necessary
        /// validations, and emulates the send operation. This is a good way to check if a
        /// certain message will be accepted by AGC for delivery.</para>
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
        public async Task<string> SendAsync(Message message, bool dryRun)
        {
            return await this.SendAsync(message, dryRun, default(CancellationToken)).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends a message to the AGC service for delivery. The message gets validated both by
        /// the Admin SDK, and the remote AGC service. A successful return value indicates
        /// that the message has been successfully sent to AGC, where it has been accepted by the
        /// AGC service.
        /// <para>If the <paramref name="dryRun"/> option is set to true, the message will not be
        /// actually sent to the recipients. Instead, the AGC service performs all the necessary
        /// validations, and emulates the send operation. This is a good way to check if a
        /// certain message will be accepted by AGC for delivery.</para>
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
            Message message, bool dryRun, CancellationToken cancellationToken)
        {
            return await this.messagingClient.SendAsync(
                message, dryRun, cancellationToken).ConfigureAwait(false);
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
            return await this.messagingClient.SubscribeToTopicAsync(registrationTokens, topic).ConfigureAwait(false);
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
            return await this.messagingClient.UnsubscribeFromTopicAsync(registrationTokens, topic).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a list of topics that the specified registration token subscribe to.
        /// </summary>
        /// <param name="registrationToken">The topic name to unsubscribe from.</param>
        /// <returns>A task that completes with a <see cref="TopicListResponse"/>, giving details about the topic retrieve operations.</returns>
        public async Task<TopicListResponse> GetTopicListAsync(string registrationToken)
        {
            return await this.messagingClient.GetTopicListAsync(registrationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes this <see cref="AGConnectMessaging"/> service instance.
        /// </summary>
        void IAGConnectService.Delete()
        {
            this.messagingClient.Dispose();
        }
    }
}
