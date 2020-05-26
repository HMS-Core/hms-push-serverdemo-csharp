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

namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// Represents the notification parameters that can be included in a <see cref="Message"/>.
    /// </summary>
    public class Notification : MessagePart<Notification>
    {
        /// <summary>
        /// Gets or sets the title of the notification.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the body of the notification.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the image url of the notification.
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <inheritdoc/>
        internal protected override Notification CopyAndValidate()
        {
            if (!string.IsNullOrEmpty(Image) && !Validator.IsHttpsUrl(Image))
            {
                throw new ArgumentException("Image must be a https url.");
            }

            return new Notification()
            {
                Title = Title,
                Body = Body,
                Image = Image
            };
        }
    }
}
