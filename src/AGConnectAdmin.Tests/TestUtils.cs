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
using System.Collections.Generic;
using System.IO;

namespace AGConnectAdmin.Tests
{
    internal static class TestUtils
    {
        private const string ServiceAccountFile = "/options.json";

        private static bool Initialized = false;
        public static void GlobalInit()
        {
            if (!Initialized)
            {
                Initialized = true;
                AGConnectApp.Create(TestUtils.ReadOptionsFromDisk());
            }
        }

        public static AppOptions ReadOptionsFromDisk()
        {
            var content = File.ReadAllText(Environment.CurrentDirectory + ServiceAccountFile);
            IDictionary<string, string> keyValuePairs = NewtonsoftJsonSerializer.Instance.Deserialize<IDictionary<string, string>>(content);
            return new AppOptions()
            {
                LoginUri = keyValuePairs["login_uri"],
                ApiVersion = ApiVersion.V2,
                ProjectId = keyValuePairs["dev_proj_id"],
                ClientId = keyValuePairs["dev_app_id"],
                ClientSecret = keyValuePairs["client_secret"]
            };
        }
    }
}
