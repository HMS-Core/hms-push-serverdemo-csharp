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
using System.Threading;
using System.Threading.Tasks;
using AGConnectAdmin.Messaging;
using Xunit;

namespace AGConnectAdmin.Tests
{
    public class MessagingClientTest
    {
        static MessagingClientTest()
        {
            TestUtils.GlobalInit();
        }


        private Message GetMessage(string content)
        {
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = content,
                    Body = "Body",
                },
                Android = new AndroidConfig()
                {
                    CollapseKey = -1,
                    Urgency = UrgencyPriority.HIGH,
                    TTL = TimeSpan.FromHours(1),
                    BITag = "tag",
                    FastAppTarget = FastAppTarget.Development,
                    Notification = new AndroidNotification()
                    {
                        Title = "title",
                        Body = "body",
                        Icon = "icon",
                        Color = "#AACCDD",
                        Sound = "sound",
                        Tag = "tag",
                        ClickAction = ClickAction.CustomIntent("intent"),
                        BodyLocKey = "key",
                        BodyLocArgs = new List<string> { "arg1", "arg2" },
                        TitleLocKey = "key",
                        TitleLocArgs = new List<string> { "arg1", "arg2" },
                        ChannelId = "channel",
                        NotifySummary = "summary",
                        Style = NotificationStyle.Default,
                        AutoClear = 3000,
                        NotifyId = 123,
                        Group = "group",
                        Badge = new BadgeNotification()
                        {
                            AddNum = 32,
                            Class = "class"
                        }
                    },
                },
                Token = new List<string> { "YOUR_REGISTRATION_TOKEN" }

            };
            return message;
        }

        [Fact]
        public async Task SendSingle()
        {
            var requestId = await AGConnectMessaging.DefaultInstance.SendAsync(GetMessage("content"), dryRun: true);
            Assert.True(!string.IsNullOrEmpty(requestId));
        }

        [Fact]
        public void SendMany()
        {
            List<Task<string>> tasks = new List<Task<string>>();
            for (int i = 0; i < 100; i++)
            {
                var task = AGConnectMessaging.DefaultInstance.SendAsync(GetMessage("content " + i), dryRun: true);
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var task in tasks)
            {
                Assert.True(!string.IsNullOrEmpty(task.Result));
            }
        }

        [Fact]
        public void SendManyConcurrent()
        {
            List<Task<string>> tasks = new List<Task<string>>();

            for (int i = 0; i < 100; i++)
            {
                var task = Task.Run(() =>
                {
                    return AGConnectMessaging.DefaultInstance.SendAsync(GetMessage("content " + i), dryRun: true);
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var task in tasks)
            {
                Assert.True(!string.IsNullOrEmpty(task.Result));
            }
        }

        [Fact]
        public void SendManyConcurrentAndDelayed()
        {
            List<Task<string>> tasks = new List<Task<string>>();

            for (int i = 0; i < 100; i++)
            {
                var task = Task.Run(() =>
                {
                    Thread.Sleep(i);
                    return AGConnectMessaging.DefaultInstance.SendAsync(GetMessage("content " + i), dryRun: true);
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var task in tasks)
            {
                Assert.True(!string.IsNullOrEmpty(task.Result));
            }
        }

        [Fact]
        public void SendManyConcurrentAndDelayed2()
        {
            List<Task<string>> tasks = new List<Task<string>>();


            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(i);
                var task = Task.Run(() =>
                {
                    return AGConnectMessaging.DefaultInstance.SendAsync(GetMessage("content " + i), dryRun: true);
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var task in tasks)
            {
                Assert.True(!string.IsNullOrEmpty(task.Result));
            }
        }
    }
}
