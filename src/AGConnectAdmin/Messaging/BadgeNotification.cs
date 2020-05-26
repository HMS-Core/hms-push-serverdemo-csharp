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
    /// Represents the badge notification options that can be included in a
    /// <see cref="AndroidNotification"/>.
    /// </summary>
    public class BadgeNotification : MessagePart<BadgeNotification>
    {
        /// <summary>
        /// Gets or sets the increase number of the click badge notification.
        /// </summary>
        /// <remarks>The number must between 0 to 100</remarks>
        [JsonProperty("add_num")]
        public int? AddNum { get; set; }

        /// <summary>
        /// Gets or sets the number of the click badge notification.
        /// </summary>
        /// <remarks>The number must between 0 to 100</remarks>
        [JsonProperty("set_num")]
        public int? SetNum { get; set; }

        /// <summary>
        /// Gets or sets the class of the click badge notification.
        /// </summary>
        [JsonProperty("class")]
        public string Class { get; set; }

        /// <inheritdoc/>
        internal protected override BadgeNotification CopyAndValidate()
        {
            var copy = new BadgeNotification()
            {
                AddNum = this.AddNum,
                SetNum = this.SetNum,
                Class = this.Class,
            };

            if(AddNum != null && !Validator.IsInRange(AddNum.Value, 0, 100))
            {
                throw new ArgumentException("AddNum must be within [0, 100].");
            }

            if (SetNum != null && !Validator.IsInRange(SetNum.Value, 0, 100))
            {
                throw new ArgumentException("SetNum must be within [0, 100].");
            }

            return copy;
        }
    }
}
