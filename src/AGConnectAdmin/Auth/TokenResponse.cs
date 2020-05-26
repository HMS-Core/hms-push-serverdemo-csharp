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

using System;
using Newtonsoft.Json;

namespace AGConnectAdmin.Auth
{
    internal sealed class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public long? Expires { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("error")]
        public long? Error { get; set; }

        [JsonProperty("error_description")]
        public string Description { get; set; }

        [JsonIgnore]
        private DateTime CreateTime;

        TokenResponse()
        {
            CreateTime = DateTime.UtcNow;
        }

        private static TimeSpan TimeInAdvance = TimeSpan.FromMinutes(5);

        internal string GetValidAccessToken()
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                return null;
            }

            TimeSpan leftDuration = CreateTime.AddSeconds(Expires ?? 0) - DateTime.UtcNow;
            if (leftDuration < TimeInAdvance)
            {
                return null;
            }

            return AccessToken;
        }
    }
}
