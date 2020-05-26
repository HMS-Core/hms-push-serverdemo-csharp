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
    /// Represents response result for topic management.
    /// </summary>
    public sealed class TopicManagementResponse : SingleMessageResponse
    {
        /// <summary>
        /// Gets or sets the count of failure.
        /// </summary>
        [JsonProperty("failureCount")]
        public int FailureCount { get; set; }

        /// <summary>
        /// Gets or sets the count of success.
        /// </summary>
        [JsonProperty("successCount")]
        public int SuccessCount { get; set; }
    }
}
