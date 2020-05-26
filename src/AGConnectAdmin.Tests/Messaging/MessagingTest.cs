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
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AGConnectAdmin.Tests
{
    public class MessagingTest
    {
        static MessagingTest()
        {
            TestUtils.GlobalInit();
        }

        [Fact]
        public async Task SendMessageCancel()
        {
            var app = AGConnectApp.DefaultInstance;
            var messaging = AGConnectMessaging.DefaultInstance;
            var canceller = new CancellationTokenSource();
            canceller.Cancel();
            await Assert.ThrowsAsync<TaskCanceledException>(
                async () => await messaging.SendAsync(
                    new Message() { Topic = "test-topic" }, canceller.Token));
        }

        [Fact]
        public async Task SendTopicMessage()
        {
            var app = AGConnectApp.Create(new AppOptions()
            {
                ClientId = "11111111",
                ClientSecret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
            });

            var msg = AGConnectMessaging.GetMessaging(app);

            await msg.SendAsync(new Message()
            {
                Notification = new Notification()
                {
                    Title = "Test Message",
                    Body = "Detail Message"
                },
                Topic = "News",
                Token = new string[] { "yyyyyyyyyyyyyyyyyy" }
            });
        }

        [Fact]
        public async Task SendConditionMessage()
        {
            var app = AGConnectApp.Create(new AppOptions()
            {
                ClientId = "11111111",
                ClientSecret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
            });

            var msg = AGConnectMessaging.GetMessaging(app);

            await msg.SendAsync(new Message()
            {
                Notification = new Notification()
                {
                    Title = "Test Message",
                    Body = "Detail Message",
                },
                Android = new AndroidConfig()
                {
                    Notification = new AndroidNotification()
                    {
                        ClickAction = ClickAction.OpenUrl("http://example.com")
                    }
                },
                Token = new string[] { "yyyyyyyyyyyyyyyyyy" }
            });
        }

        [Fact]
        public async Task SubscribeTopic()
        {
            var app = AGConnectApp.Create(new AppOptions()
            {
                ClientId = "11111111",
                ClientSecret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
            });

            var msg = AGConnectMessaging.GetMessaging(app);
            var tokens = new List<string>() { "sdjlfjiwekfnskdjfksdfjskdfjsdf" };
            var topic = "News";
            await msg.SubscribeToTopicAsync(tokens.AsReadOnly(), topic);
        }

        [Fact]
        public async Task UnsubscribeTopic()
        {
            var app = AGConnectApp.Create(new AppOptions()
            {
                ClientId = "11111111",
                ClientSecret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
            });

            var msg = AGConnectMessaging.GetMessaging(app);
            var tokens = new List<string>() { "sdjlfjiwekfnskdjfksdfjskdfjsdf" };
            var topic = "News";
            await msg.UnsubscribeFromTopicAsync(tokens.AsReadOnly(), topic);
        }

        [Fact]
        public async Task GetTopicList()
        {
            var app = AGConnectApp.Create(new AppOptions()
            {
                ClientId = "11111111",
                ClientSecret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
            });

            var msg = AGConnectMessaging.GetMessaging(app);
            var token = "sdjlfjiwekfnskdjfksdfjskdfjsdf";
            TopicListResponse resp = await msg.GetTopicListAsync(token);
        }
    }
}
