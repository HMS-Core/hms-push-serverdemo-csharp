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
    /// Represents the envelope message accepted by the AGC backend service, including the message
    /// payload and other options like <c>validate_only</c>.
    /// </summary>
    internal class SendRequest
    {
        /// <summary>
        /// Gets or sets the message body.
        /// </summary>
        [JsonProperty("message")]
        public Message Message { get; set; }

        /// <summary>
        /// Gets or sets if it's a validation message only that it will not be sent actually.
        /// </summary>
        [JsonProperty("validate_only")]
        public bool ValidateOnly { get; set; }
    }
}
