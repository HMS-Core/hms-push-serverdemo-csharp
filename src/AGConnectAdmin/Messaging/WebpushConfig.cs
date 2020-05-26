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

using AGConnectAdmin.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// Represents Web push configuration of notification.
    /// </summary>
    public class WebpushConfig : MessagePart<WebpushConfig>
    {
        /// <summary>
        /// Gets or sets the Webpush options included in the message.
        /// </summary>
        [JsonProperty("hms_options")]
        public WebpushHmsOptions HmsOptions { get; set; }

        /// <summary>
        /// Gets or sets the Webpush HTTP headers. Refer
        /// <a href="https://tools.ietf.org/html/rfc8030#section-5">
        /// Webpush specification</a> for supported headers.
        /// </summary>
        [JsonProperty("headers")]
        public IReadOnlyDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets or sets the Webpush notification included in the message.
        /// </summary>
        [JsonProperty("notification")]
        public WebpushNotification Notification { get; set; }

        /// <inheritdoc/>
        internal protected override WebpushConfig CopyAndValidate()
        {
            return new WebpushConfig()
            {
                Headers = this.Headers?.Copy(),
                Notification = this.Notification?.CopyAndValidate(),
                HmsOptions = this.HmsOptions?.CopyAndValidate(),
            };
        }
    }

    /// <summary>
    /// Represents the Webpush-specific notification options.
    /// </summary>
    public sealed class WebpushHmsOptions : MessagePart<WebpushHmsOptions>
    {
        /// <summary>
        /// Gets or sets the link to open when the user clicks on the notification.
        /// </summary>
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <inheritdoc/>
        internal protected override WebpushHmsOptions CopyAndValidate()
        {
            var copy = new WebpushHmsOptions()
            {
                Link = this.Link,
            };

            if (copy.Link != null)
            {
                if (!Validator.IsHttpsUrl(copy.Link))
                {
                    throw new ArgumentException("The link options should be a valid https url.");
                }
            }

            return copy;
        }
    }
}
