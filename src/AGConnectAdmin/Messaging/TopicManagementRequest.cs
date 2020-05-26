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
using System.Collections.Generic;

namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// Represents request parameter for topic management.
    /// </summary>
    public sealed class TopicManagementRequest
    {
        /// <summary>
        /// Gets or sets the specified topic.
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; set; }

        /// <summary>
        /// Gets or sets the specified tokens.
        /// </summary>
        [JsonProperty("tokenArray")]
        public List<string> Tokens { get; set; }
    }
}
