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

namespace AGConnectAdmin
{
    /// <summary>
    /// Configurable options that can be specified when creating a <see cref="AGConnectApp"/>.
    /// </summary>
    public sealed class AppOptions
    {
        /// <summary>
        /// Initializes a new instance of the  <see cref="AppOptions"/> class.
        /// </summary>
        public AppOptions()
        {
            LoginUri = "https://oauth-login.cloud.huawei.com/oauth2/v3/token";
            ApiBaseUri = "https://push-api.cloud.huawei.com/v1";
        }

        internal AppOptions(AppOptions options)
        {
            this.LoginUri = options.LoginUri;
            this.ApiBaseUri = options.ApiBaseUri;
            this.ClientId = options.ClientId;
            this.ClientSecret = options.ClientSecret;
        }

        internal string GetApiUri()
        {
            return ApiBaseUri + string.Format("/{0}/messages:send", ClientId) ;
        }

        /// <summary>
        /// Gets or sets the login url that use to request access token, it's optional.
        /// </summary>
        public string LoginUri { get; set; }

        /// <summary>
        /// Gets or sets the API base path, it's optional.
        /// This property is optional.
        /// </summary>
        public string ApiBaseUri { get; set; }

        /// <summary>
        /// Gets or sets the APP ID from AGC.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets orset the App Secret from AGC.
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
