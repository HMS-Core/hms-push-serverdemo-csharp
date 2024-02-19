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
            ApiVersion = ApiVersion.V2;
        }

        internal AppOptions(AppOptions options)
        {
            this.LoginUri = options.LoginUri;
            this.ApiVersion = options.ApiVersion;
            this.ProjectId = options.ProjectId;
            this.ClientId = options.ClientId;
            this.ClientSecret = options.ClientSecret;
        }

        internal string GetApiUri()
        {
            var res = ApiBaseUri;
            switch (ApiVersion)
            {
                case ApiVersion.V1:
                    res += '/' + ClientId;
                    break;
                case ApiVersion.V2:
                    res += '/' + ProjectId;
                    break;
            }
            res += "/messages:send";
            return res;
        }

        /// <summary>
        /// Gets or sets the login url that use to request access token, it's optional.
        /// </summary>
        public string LoginUri { get; set; }

        /// <summary>
        /// Gets or sets the PushKit API version. Default value is project-level (V2)
        /// This property is optional.
        /// </summary>
        public ApiVersion ApiVersion { get; set; }

        /// <summary>
        /// Gets the API base uri according version
        /// </summary>
        public string ApiBaseUri { get => ApiVersion.ApiBaseUri(); }

        /// <summary>
        /// Gets or sets the PROJECT ID from AGC
        /// </summary>
        public string ProjectId { get; set; }

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
