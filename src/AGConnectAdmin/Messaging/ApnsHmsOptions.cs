/* Copyright 2020. Huawei Technologies Co., Ltd. All rights reserved.
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
 */

using Newtonsoft.Json;

namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// Represents Apple Push Notification Service HMS options.
    /// </summary>
    public class ApnsHmsOptions : MessagePart<ApnsHmsOptions>
    {
        /// <summary>
        /// Gets or sets target user type.
        /// </summary>
        [JsonProperty("target_user_type")]
        public ApnsTargetUserType TargetUserType { get; set; } = ApnsTargetUserType.Test;

        /// <inheritdoc/>
        internal protected override ApnsHmsOptions CopyAndValidate()
        {
            var copy = new ApnsHmsOptions()
            {
                TargetUserType = this.TargetUserType,
            };

            return copy;
        }
    }

    /// <summary>
    /// Represents the target user type.
    /// </summary>
    public enum ApnsTargetUserType
    {
        /// <summary>
        /// Test user.
        /// </summary>
        Test = 1,

        /// <summary>
        /// Normal user.
        /// </summary>
        Normal = 2,

        /// <summary>
        /// VoIP user.
        /// </summary>
        VoIP = 3
    }
}
