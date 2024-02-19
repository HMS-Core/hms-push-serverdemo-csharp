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

using Xunit.Abstractions;

namespace AGConnectAdmin.Examples
{
    public partial class Example
    {
        private const ApiVersion _apiVersion = ApiVersion.V1;

        #region Tokens
        private const string TOKEN_IOS = "your ios token";
        private const string TOKEN_ANDROID = "your android token";
        private const string TOKEN_WEB = "your web token";
        #endregion

        protected ITestOutputHelper Logger { get; private set; }

        public Example(ITestOutputHelper logger)
        {
            Logger = logger;

            AGConnectApp.Create(new AppOptions()
            {
                ApiVersion = _apiVersion,
                ProjectId = "your project id",
                ClientId = "your client id",
                ClientSecret = "your cliient secret",
            });
        }
    }
}
