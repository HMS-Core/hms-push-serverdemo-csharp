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

using Newtonsoft.Json;

namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// Represents an action available to users when the notification is presented.
    /// </summary>
    public class WebpushAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebpushAction"/> class.
        /// </summary>
        public WebpushAction() { }

        internal WebpushAction(WebpushAction action)
        {
            this.Action = action.Action;
            this.Title = action.Title;
            this.Icon = action.Icon;
        }

        /// <summary>
        /// Gets or sets the name of the Action.
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the title text.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the icon URL.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
