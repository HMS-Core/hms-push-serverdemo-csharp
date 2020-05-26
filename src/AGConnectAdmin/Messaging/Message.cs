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
 * 2019.12.27-Changed base class and some properties
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// Represents a message that can be sent via AGConnect Cloud Messaging. Contains payload
    /// information as well as the recipient information. The recipient information must be
    /// specified by setting exactly one of the <see cref="Token"/>, <see cref="Topic"/> or
    /// <see cref="Condition"/> fields.
    /// </summary>
    public class Message : MessagePart<Message>
    {
        /// <summary>
        /// Gets or sets the data to be included in the message.
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the notification information to be included in the message.
        /// </summary>
        [JsonProperty("notification")]
        public Notification Notification { get; set; }

        /// <summary>
        /// Gets or sets the Android-specific information to be included in the message.
        /// </summary>
        [JsonProperty("android")]
        public AndroidConfig Android { get; set; }

        /// <summary>
        /// Gets or sets the APNs-specific information to be included in the message.
        /// </summary>
        [JsonProperty("apns")]
        public ApnsConfig Apns { get; set; }

        /// <summary>
        /// Gets or sets the Webpush-specific information to be included in the message.
        /// </summary>
        [JsonProperty("webpush")]
        public WebpushConfig Webpush { get; set; }

        /// <summary>
        /// Gets or sets the registration tokens of the devices to which the message should be sent.
        /// </summary>
        [JsonProperty("token")]
        public IReadOnlyList<string> Token { get; set; }

        /// <summary>
        /// Gets or sets the name of the AGC messaging topic to which the message should be sent.
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; set; }

        /// <summary>
        /// Gets or sets the condition to which the message should be sent. Must be a valid
        /// condition string such as <c>"'foo' in topics"</c>.
        /// </summary>
        [JsonProperty("condition")]
        public string Condition { get; set; }

        /// <inheritdoc/>
        internal protected override Message CopyAndValidate()
        {
            // copy
            var copy = new Message()
            {
                Data = Data,
                Notification = Notification?.CopyAndValidate(),
                Android = Android?.CopyAndValidate(),
                Apns = Apns?.CopyAndValidate(),
                Webpush = Webpush?.CopyAndValidate(),
                Token = Token?.ToList(),
                Topic = Topic,
                Condition = Condition,
            };

            // validate
            var tokenList = Token != null ? new List<string>(Token) : new List<string>();
            var nonnullTokens = tokenList.FindAll((target) => !string.IsNullOrEmpty(target));

            var fieldList = new List<string>()
            {
                copy.Topic, copy.Condition,
            };
            var nonnullFields = fieldList.FindAll((target) => !string.IsNullOrEmpty(target));

            if (nonnullFields.Count > 1)
            {
                throw new ArgumentException("Exactly one of Token, Topic or Condition is required.");
            }

            if (nonnullFields.Count == 1 && nonnullTokens.Count > 0)
            {
                throw new ArgumentException("Exactly one of Token, Topic or Condition is required.");
            }

            if (nonnullFields.Count == 0 && nonnullTokens.Count == 0)
            {
                throw new ArgumentException("Exactly one of Token, Topic or Condition is required.");
            }

            return copy;
        }
    }
}
