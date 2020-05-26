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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AGConnectAdmin.Tests
{
    public class MessageTest
    {
        static MessageTest()
        {
            TestUtils.GlobalInit();
        }

        [Fact]
        public void FullPropertyMessage()
        {
            var message = new Message()
            {
                Data = "k1=1,k2=2",
                Notification = new Notification
                {
                    Title = "Notification",
                    Body = "Hello world!",
                    Image = "https://example.com/image.png"
                },
                Android = new AndroidConfig
                {
                    CollapseKey = -1,
                    Urgency = UrgencyPriority.NORMAL,
                    Category = "PLAY_VOICE",
                    TTL = TimeSpan.FromSeconds(20),
                    BITag = "Test",
                    FastAppTarget = FastAppTarget.Development,
                    Data = "k1=1,k2=2",
                    Notification = new AndroidNotification
                    {
                        Title = "Notification",
                        Body = "Hello world!",
                        Icon = "icon.png",
                        Color = "#000000",
                        Sound = "sound.mp3",
                        DefaultSound = false,
                        Tag = "News",
                        ClickAction = ClickAction.OpenApp(),
                        //ClickAction = ClickAction.OpenUrl("https://example.com"),
                        //ClickAction = ClickAction.OpenRichResource("https://example.com/res.zip"),
                        //ClickAction = ClickAction.CustomAction("customAction"),
                        //ClickAction = ClickAction.CustomIntent("customIntent"),
                        BodyLocKey = "title_key",
                        BodyLocArgs = new List<string>() { "arg1", "arg2" },
                        TitleLocKey = "body_key",
                        TitleLocArgs = new List<string>() { "arg1", "arg2" },
                        MultiLangKey = new JObject
                        {
                            ["title_key"] = new JObject
                            {
                                ["zh"] = "ÄãºÃ",
                                ["en"] = "Hello",
                            },
                            ["body_key"] = new JObject
                            {
                                ["zh"] = "ÔÙ¼û",
                                ["en"] = "Byte",
                            }
                        },
                        ChannelId = "channel1",
                        NotifySummary = "notify summary",
                        Image = "https://example.com/image.png",
                        Style = NotificationStyle.BigText,
                        BigTitle = "Big tile",
                        BigBody = "Big body",
                        AutoClear = 20,
                        NotifyId = -1,
                        Group = "News",
                        Badge = new BadgeNotification
                        {
                            AddNum = 13,
                            SetNum = 10,
                            Class = "com.huawei.DemoActivity",
                        },
                        Ticker = "Say hello.",
                        AutoCancel = false,
                        EventTime = DateTime.Now,
                        Importance = NotificationImportance.NORMAL,
                        DefaultVibrateTimings = false,
                        VibrateTimings = new double[] { 1, 1, 1, 1 },
                        Visibility = NotificationVisibility.VISIBILITY_UNSPECIFIED,
                        DefaultLightSettings = false,
                        LightSettings = new LightSettings
                        {
                            Color = LightColor.FromArbg(0.5, 1.0, 0.5),
                            LightOnDuration = TimeSpan.FromSeconds(3),
                            LightOffDuration = TimeSpan.FromSeconds(3),
                        },
                        ForegroundShow = true
                    }
                },
                Apns = new ApnsConfig
                {
                    HmsOptions = new ApnsHmsOptions
                    {
                        TargetUserType = ApnsTargetUserType.Test
                    },
                    Headers = new Dictionary<string, string>
                    {
                        ["authorization"] = "bearer xxxxx",
                        ["apns-id"] = "123e4567-e89b-12d3-a456-42665544000",
                        ["apns-expiration"] = "0",
                        ["apns-priority"] = "10",
                        ["apns-topic"] = "Apple",
                        ["apns-collapse-id"] = "collapse-id",
                    },
                    Aps = new Aps
                    {
                        //AlertString = "hello",
                        Alert = new ApsAlert
                        {
                            Title = "Notification",
                            Subtitle = "Say Hello",
                            Body = "Hello world!",
                            LaunchImage = "launch.png",
                            TitleLocKey = "title-loc-key",
                            TitleLocArgs = new string[] { "arg1", "arg2" },
                            SubtitleLocKey = "subtitle-loc-key",
                            SubtitleLocArgs = new string[] { "arg1", "arg2" },
                            LocKey = "loc-key",
                            LocArgs = new string[] { "arg1", "arg2" },
                        },
                        Badge = 0,
                        //Sound = "Sound",
                        CriticalSound = new CriticalSound
                        {
                            Critical = true,
                            Name = "hh",
                            Volume = 0.5
                        },
                        ContentAvailable = true,
                        MutableContent = true,
                        Category = "News",
                        ThreadId = "thread-id",
                        TargetContentId = "target-content-id",
                        CustomData = new Dictionary<string, object>
                        {
                            ["custom-key"] = "custom-value"
                        }
                    },

                },
                Webpush = new WebpushConfig
                {
                    HmsOptions = new WebpushHmsOptions
                    {
                        Link = "https://example.com"
                    },
                    Headers = new Dictionary<string, string>
                    {
                        ["ttl"] = "20s",
                        ["topic"] = "topic",
                        ["urgency"] = "normal"  // very-low | low | normal | high
                    },
                    Notification = new WebpushNotification
                    {
                        Title = "Notification",
                        Body = "Hello world!",
                        Icon = "https://example.com/icon.png",
                        Image = "https://example.com/image.png",
                        Language = "zh",
                        Tag = "News",
                        Badge = "https://example.com/badge.png",
                        Direction = Direction.Auto,
                        Vibrate = new int[] { 100, 200, 300 },
                        Renotify = false,
                        RequireInteraction = false,
                        Silent = false,
                        Timestamp = 1575722408765,
                        Actions = new WebpushAction[]
                        {
                            new WebpushAction
                            {
                                Action = "ok",
                                Title = "OK",
                                Icon = "https://example.com/ok.png"
                            },
                            new WebpushAction
                            {
                                Action = "close",
                                Title = "Close",
                                Icon = "https://example.com/close.png"
                            }
                        },
                        Data = new JObject
                        {
                            ["mykey"] = "myvalue"
                        },
                        CustomData = new Dictionary<string, object>
                        {
                            ["other-property"] = "other-value"
                        }
                    }
                },
                Token = new List<string> { "xxxxxxxxxxxxxxxxx" },
                //Topic = "game",
                //Condition = "game in topics"
            };

            AssertJsonEquals("{}", message.CopyAndValidate());
        }

        [Fact]
        public void EmptyMessage()
        {
            var message = new Message() { Token = new List<string> { "test-token1", "test-token2" } };
            this.AssertJsonEquals(new JObject() { { "token", new JArray() { "test-token1", "test-token2" } } }, message);

            message = new Message() { Topic = "test-topic" };
            this.AssertJsonEquals(new JObject() { { "topic", "test-topic" } }, message);

            message = new Message() { Condition = "test-condition" };
            this.AssertJsonEquals(new JObject() { { "condition", "test-condition" } }, message);
        }

        [Fact]
        public void DataMessage()
        {
            var message = new Message()
            {
                Topic = "test-topic",
                Data = "test-data" 
            };
            this.AssertJsonEquals(
                new JObject()
                {
                    { "topic", "test-topic" },
                    { "data", "test-data" },
                }, message);
        }

        [Fact]
        public void Notification()
        {
            var message = new Message()
            {
                Topic = "test-topic",
                Notification = new Notification()
                {
                    Title = "title",
                    Body = "body",
                },
            };
            var expected = new JObject()
            {
                { "topic", "test-topic" },
                {
                    "notification", new JObject()
                    {
                        { "title", "title" },
                        { "body", "body" },
                    }
                },
            };
            this.AssertJsonEquals(expected, message);
        }

        [Fact]
        public void MessageDeserialization()
        {
            var original = new Message()
            {
                Topic = "test-topic",
                Data = "test-data",
                Notification = new Notification()
                {
                    Title = "title",
                    Body = "body",
                },
                Android = new AndroidConfig()
                {
                    Urgency = UrgencyPriority.HIGH,
                }
            };
            var json = NewtonsoftJsonSerializer.Instance.Serialize(original);
            var copy = NewtonsoftJsonSerializer.Instance.Deserialize<Message>(json);
            Assert.Equal(original.Topic, copy.Topic);
            Assert.Equal(original.Data, copy.Data);
            Assert.Equal(original.Notification.Title, copy.Notification.Title);
            Assert.Equal(original.Notification.Body, copy.Notification.Body);
            Assert.Equal(original.Android.Urgency, copy.Android.Urgency);
        }

        [Fact]
        public void MessageCopy()
        {
            var original = new Message()
            {
                Topic = "test-topic",
                Data = "test-data",
                Notification = new Notification(),
                Android = new AndroidConfig()
            };
            var copy = original?.CopyAndValidate();
            Assert.NotSame(original, copy);
            Assert.NotSame(original.Notification, copy.Notification);
            Assert.NotSame(original.Android, copy.Android);
        }

        [Fact]
        public void MessageWithoutTarget()
        {
            Assert.Throws<ArgumentException>(() =>new Message().CopyAndValidate());
        }

        [Fact]
        public void MultipleTargets()
        {
            var message = new Message()
            {
                Token = new List<string> { "test-token" },
                Topic = "test-topic",
            };
            Assert.Throws<ArgumentException>(() => (message as MessagePart<Message>)?.CopyAndValidate());

            message = new Message()
            {
                Token = new List<string> { "test-token" },
                Condition = "test-condition",
            };
            Assert.Throws<ArgumentException>(() => (message as MessagePart<Message>)?.CopyAndValidate());

            message = new Message()
            {
                Condition = "test-condition",
                Topic = "test-topic",
            };
            Assert.Throws<ArgumentException>(() => (message as MessagePart<Message>)?.CopyAndValidate());

            message = new Message()
            {
                Token = new List<string> { "test-token" },
                Topic = "test-topic",
                Condition = "test-condition",
            };
            Assert.Throws<ArgumentException>(() => (message as MessagePart<Message>)?.CopyAndValidate());
        }

        [Fact]
        public void AndroidConfig()
        {
            var message = new Message()
            {
                Topic = "test-topic",
                Android = new AndroidConfig()
                {
                    CollapseKey = 5,
                    Urgency = UrgencyPriority.HIGH,
                    TTL = TimeSpan.FromMilliseconds(10),
                    Notification = new AndroidNotification()
                    {
                        Title = "title",
                        Body = "body",
                        Icon = "icon",
                        Color = "#112233",
                        Sound = "sound",
                        Tag = "tag",
                        ClickAction = ClickAction.OpenRichResource("https://123"),
                        TitleLocKey = "title-loc-key",
                        TitleLocArgs = new List<string>() { "arg1", "arg2" },
                        BodyLocKey = "body-loc-key",
                        BodyLocArgs = new List<string>() { "arg3", "arg4" },
                        ChannelId = "channel-id",
                    },
                },
            };
            var expected = new JObject()
            {
                { "topic", "test-topic" },
                {
                    "android", new JObject()
                    {
                        { "collapse_key", 5 },
                        { "priority", "HIGH" },
                        { "ttl", "0.010000000s" },
                        {
                            "notification", new JObject()
                            {
                                { "style", 0 },
                                { "title", "title" },
                                { "body", "body" },
                                { "icon", "icon" },
                                { "color", "#112233" },
                                { "sound", "sound" },
                                { "tag", "tag" },
                                {
                                    "click_action", new JObject()
                                    {
                                        { "type", 4 },
                                        { "rich_resource", "https://123" }
                                    }
                                },
                                { "title_loc_key", "title-loc-key" },
                                { "title_loc_args", new JArray() { "arg1", "arg2" } },
                                { "body_loc_key", "body-loc-key" },
                                { "body_loc_args", new JArray() { "arg3", "arg4" } },
                                { "channel_id", "channel-id" },
                            }
                        },
                    }
                },
            };
            this.AssertJsonEquals(expected, message);
        }

        [Fact]
        public void AndroidConfigMinimal()
        {
            var message = new Message()
            {
                Topic = "test-topic",
                Android = new AndroidConfig(),
            };
            var expected = new JObject()
            {
                { "topic", "test-topic" },
                { "android", new JObject() },
            };
            this.AssertJsonEquals(expected, message);
        }

        [Fact]
        public void AndroidConfigFullSecondsTTL()
        {
            var message = new Message()
            {
                Topic = "test-topic",
                Android = new AndroidConfig()
                {
                    TTL = TimeSpan.FromHours(1),
                },
            };
            var expected = new JObject()
            {
                { "topic", "test-topic" },
                {
                    "android", new JObject()
                    {
                        { "ttl", "3600s" },
                    }
                },
            };
            this.AssertJsonEquals(expected, message);
        }

        [Fact]
        public void AndroidConfigDeserialization()
        {
            var original = new AndroidConfig()
            {
                CollapseKey = 2,
                TTL = TimeSpan.FromSeconds(10.5),
                Urgency = UrgencyPriority.HIGH,
                Notification = new AndroidNotification()
                {
                    Title = "title",
                },
            };
            var json = NewtonsoftJsonSerializer.Instance.Serialize(original);
            var copy = NewtonsoftJsonSerializer.Instance.Deserialize<AndroidConfig>(json);
            Assert.Equal(original.CollapseKey, copy.CollapseKey);
            Assert.Equal(original.Urgency, copy.Urgency);
            Assert.Equal(original.TTL, copy.TTL);
            Assert.Equal(original.Notification.Title, copy.Notification.Title);
        }

        [Fact]
        public void AndroidConfigCopy()
        {
            var original = new AndroidConfig()
            {
                Notification = new AndroidNotification()
                {
                    Style = NotificationStyle.BigText,
                    BigTitle = "title",
                    BigBody = "body"
                }
            };
            var copy = (original as MessagePart<AndroidConfig>)?.CopyAndValidate();
            Assert.NotSame(original, copy);
            Assert.NotSame(original.Notification, copy.Notification);
        }

        [Fact]
        public void AndroidNotificationDeserialization()
        {
            var original = new AndroidNotification()
            {
                Title = "title",
                Body = "body",
                Icon = "icon",
                Color = "#112233",
                Sound = "sound",
                Tag = "tag",
                ClickAction = ClickAction.OpenUrl("https://123"),
                TitleLocKey = "title-loc-key",
                TitleLocArgs = new List<string>() { "arg1", "arg2" },
                BodyLocKey = "body-loc-key",
                BodyLocArgs = new List<string>() { "arg3", "arg4" },
                ChannelId = "channel-id",
            };
            var json = NewtonsoftJsonSerializer.Instance.Serialize(original);
            var copy = NewtonsoftJsonSerializer.Instance.Deserialize<AndroidNotification>(json);
            Assert.Equal(original.Title, copy.Title);
            Assert.Equal(original.Body, copy.Body);
            Assert.Equal(original.Icon, copy.Icon);
            Assert.Equal(original.Color, copy.Color);
            Assert.Equal(original.Sound, copy.Sound);
            Assert.Equal(original.Tag, copy.Tag);
            Assert.NotSame(original.ClickAction, copy.ClickAction);
            Assert.Equal(original.TitleLocKey, copy.TitleLocKey);
            Assert.Equal(original.TitleLocArgs, copy.TitleLocArgs);
            Assert.Equal(original.BodyLocKey, copy.BodyLocKey);
            Assert.Equal(original.BodyLocArgs, copy.BodyLocArgs);
            Assert.Equal(original.ChannelId, copy.ChannelId);
        }

        [Fact]
        public void AndroidNotificationCopy()
        {
            var original = new AndroidNotification()
            {
                TitleLocKey = "title-loc-key",
                TitleLocArgs = new List<string>() { "arg1", "arg2" },
                BodyLocKey = "body-loc-key",
                BodyLocArgs = new List<string>() { "arg3", "arg4" },
            };
            var copy = (original as MessagePart<AndroidNotification>)?.CopyAndValidate();
            Assert.NotSame(original, copy);
            Assert.NotSame(original.TitleLocArgs, copy.TitleLocArgs);
            Assert.NotSame(original.BodyLocArgs, copy.BodyLocArgs);
        }

        [Fact]
        public void AndroidConfigInvalidTTL()
        {
            var message = new Message()
            {
                Topic = "test-topic",
                Android = new AndroidConfig()
                {
                    TTL = TimeSpan.FromHours(-1),
                },
            };
            Assert.Throws<ArgumentException>(() => (message as MessagePart<Message>)?.CopyAndValidate());
        }

        [Fact]
        public void AndroidNotificationInvalidColor()
        {
            var message = new Message()
            {
                Topic = "test-topic",
                Android = new AndroidConfig()
                {
                    Notification = new AndroidNotification()
                    {
                        Color = "not-a-color",
                    },
                },
            };
            Assert.Throws<ArgumentException>(() => (message as MessagePart<Message>)?.CopyAndValidate());
        }

        [Fact]
        public void AndroidNotificationInvalidTitleLocArgs()
        {
            var message = new Message()
            {
                Topic = "test-topic",
                Android = new AndroidConfig()
                {
                    Notification = new AndroidNotification()
                    {
                        TitleLocArgs = new List<string>() { "arg" },
                    },
                },
            };
            Assert.Throws<ArgumentException>(() => (message as MessagePart<Message>)?.CopyAndValidate());
        }

        [Fact]
        public void AndroidNotificationInvalidBodyLocArgs()
        {
            var message = new Message()
            {
                Topic = "test-topic",
                Android = new AndroidConfig()
                {
                    Notification = new AndroidNotification()
                    {
                        BodyLocArgs = new List<string>() { "arg" },
                    },
                },
            };
            Assert.Throws<ArgumentException>(() => (message as MessagePart<Message>)?.CopyAndValidate());
        }

        private void AssertJsonEquals(JObject expected, Message actual)
        {
            var json = NewtonsoftJsonSerializer.Instance.Serialize(actual.CopyAndValidate());
            var parsed = JObject.Parse(json);
            Assert.True(
                JToken.DeepEquals(expected, parsed),
                $"Expected: {expected.ToString()}\nActual: {parsed.ToString()}");
        }

        private void AssertJsonEquals(string expected, Message actual)
        {
            var json = NewtonsoftJsonSerializer.Instance.Serialize(actual.CopyAndValidate());
            var parsedObject = JObject.Parse(json);
            var expectedObject = JObject.Parse(expected);
            Assert.True(
                JToken.DeepEquals(expectedObject, parsedObject),
                $"Expected: {expectedObject.ToString()}\nActual: {parsedObject.ToString()}");
        }
    }
}
