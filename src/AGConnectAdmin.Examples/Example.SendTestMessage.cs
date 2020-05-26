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

using AGConnectAdmin.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AGConnectAdmin.Examples
{
    partial class Example
    {
        [Fact]
        public async Task SendTestMessage()
        {
            await AGConnectMessaging.DefaultInstance.SendAsync(new Message()
            {
                Android = new AndroidConfig()
                {
                    Notification = new AndroidNotification()
                    {
                        Title = "Notification from .NET",
                        Body = "Hello world!",
                        ClickAction = ClickAction.OpenApp()
                    }
                },
                Token = new List<string>() { TOKEN_ANDROID }
            }, true);
        }
    }
}
