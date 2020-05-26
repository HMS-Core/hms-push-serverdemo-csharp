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
using AGConnectAdmin.Messaging.Convertors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// Represents the Android-specific options that can be included in a <see cref="Message"/>.
    /// </summary>
    public class AndroidConfig : MessagePart<AndroidConfig>
    {
        /// <summary>
        /// Gets or sets a collapse key for the message. Collapse key serves as an identifier for a
        /// group of messages that can be collapsed, so that only the last message gets sent when
        /// delivery can be resumed. A maximum of 4 different collapse keys may be active at any
        /// given time.
        /// </summary>
        [JsonProperty("collapse_key")]
        public int? CollapseKey { get; set; }

        /// <summary>
        /// Gets or sets the priority of the message.
        /// </summary>
        [JsonProperty("urgency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UrgencyPriority? Urgency { get; set; }

        /// <summary>
        /// Gets or sets the Category, eg. "PLAY_VOICE"
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the time-to-live duration of the message.
        /// </summary>
        [JsonProperty("ttl")]
        [JsonConverter(typeof(TimeSpanJsonConverter))]
        public TimeSpan? TTL { get; set; }

        /// <summary>
        /// Gets or sets the BI-tag of the message.
        /// </summary>
        [JsonProperty("bi_tag")]
        public string BITag { get; set; }

        /// <summary>
        /// Gets or sets the fast app target of the message.
        /// </summary>
        [JsonProperty("fast_app_target")]
        public FastAppTarget? FastAppTarget { get; set; }

        /// <summary>
        /// Gets or sets the custom data of the message.
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the Android notification to be included in the message.
        /// </summary>
        [JsonProperty("notification")]
        public AndroidNotification Notification { get; set; }

        /// <inheritdoc/>
        internal protected override AndroidConfig CopyAndValidate()
        {
            // copy
            var copy = new AndroidConfig()
            {
                CollapseKey = this.CollapseKey,
                Urgency = this.Urgency,
                TTL = this.TTL,
                Category = this.Category,
                BITag = this.BITag,
                FastAppTarget = this.FastAppTarget,
                Data = this.Data,
                Notification = this.Notification?.CopyAndValidate()
            };

            // validate
            var totalSeconds = copy.TTL?.TotalSeconds ?? 0;
            if (totalSeconds < 0)
            {
                throw new ArgumentException("TTL must not be negative.");
            }

            return copy;
        }
    }

    /// <summary>
    /// Priority that can be set on an <see cref="AndroidConfig"/>.
    /// </summary>
    public enum UrgencyPriority
    {
        /// <summary>
        /// High priority message.
        /// </summary>
        HIGH,

        /// <summary>
        /// Normal priority message.
        /// </summary>
        NORMAL
    }

    /// <summary>
    /// Fast app target of <see cref="AndroidConfig"/>.
    /// </summary>
    public enum FastAppTarget
    {
        /// <summary>
        /// Development target
        /// </summary>
        Development = 1,

        /// <summary>
        /// Production target
        /// </summary>
        Production = 2,
    }
}
