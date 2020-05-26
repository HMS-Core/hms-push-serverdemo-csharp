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
 * 2019.12.27-Changed base class
 */

using AGConnectAdmin.Utils;
using Newtonsoft.Json;
using System;

namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// Represents the click action options that can be included in a
    /// <see cref="AndroidNotification"/>.
    /// </summary>
    public class ClickAction : MessagePart<ClickAction>
    {
        /// <summary>
        /// Gets or sets the type of the click action.
        /// </summary>
        [JsonProperty("type")]
        private int Type { get; set; }

        /// <summary>
        /// Gets or sets the intent of the click action.
        /// </summary>
        [JsonProperty("intent")]
        private string Intent { get; set; }

        /// <summary>
        /// Gets or sets the URL of the click action.
        /// </summary>
        [JsonProperty("url")]
        private string URL { get; set; }

        /// <summary>
        /// Gets or sets the rich resource of the click action.
        /// </summary>
        [JsonProperty("rich_resource")]
        private string RichResource { get; set; }

        /// <summary>
        /// Gets or sets the action of the click event.
        /// </summary>
        [JsonProperty("action")]
        private string Action { get; set; }

        internal ClickAction() { }

        /// <summary>
        /// Custom intent
        /// </summary>
        /// <param name="intent"></param>
        /// <returns></returns>
        public static ClickAction CustomIntent(string intent)
        {
            if (string.IsNullOrEmpty(intent))
            {
                return OpenApp();
            }

            return new ClickAction()
            {
                Type = 1,
                Intent = intent,
            };
        }

        /// <summary>
        /// Custom action
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ClickAction CustomAction(string action)
        {
            if (string.IsNullOrEmpty(action))
            {
                return OpenApp();
            }

            return new ClickAction()
            {
                Type = 1,
                Action = action,
            };
        }

        /// <summary>
        /// Open specified url
        /// </summary>
        /// <param name="url">Url to be opened, must be https</param>
        /// <returns></returns>
        public static ClickAction OpenUrl(string url)
        {
            return new ClickAction()
            {
                Type = 2,
                URL = url,
            };
        }


        /// <summary>
        /// Open app
        /// </summary>
        public static ClickAction OpenApp()
        {
            return new ClickAction()
            {
                Type = 3
            };
        }

        /// <summary>
        /// Open rich resource
        /// </summary>
        /// <param name="resourceUrl">Url of zip file of rich resource, must be https</param>
        /// <returns></returns>
        public static ClickAction OpenRichResource(string resourceUrl)
        {
            return new ClickAction()
            {
                Type = 4,
                RichResource = resourceUrl,
            };
        }

        /// <inheritdoc/>
        internal protected override ClickAction CopyAndValidate()
        {
            var copy = new ClickAction()
            {
                Type = this.Type,
                Intent = this.Intent,
                URL = this.URL,
                RichResource = this.RichResource,
                Action = this.Action
            };

            if (copy.URL != null && !Validator.IsHttpsUrl(copy.URL))
            {
                throw new ArgumentException("Click action url must be a https url.");
            }

            if (copy.RichResource != null && !Validator.IsHttpsUrl(copy.RichResource))
            {
                throw new ArgumentException("Rich resource url must be a https url.");
            }

            return copy;
        }
    }
}
