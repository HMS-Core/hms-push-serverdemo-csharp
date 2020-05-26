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
using AGConnectAdmin.Messaging.Convertors;
using Newtonsoft.Json;
using Xunit;

namespace AGConnectAdmin.Tests
{
    public class ConvertorTest
    {
        [Fact]
        public void SerializeTimeSpan()
        {
            string json = JsonConvert.SerializeObject(new TestObject1()
            {
                TTL = TimeSpan.FromSeconds(5)
            });

            Assert.Equal("{\"TTL\":\"5s\"}", json);
        }

        [Fact]
        public void SerializeNullableTimeSpan()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            string json1 = JsonConvert.SerializeObject(new TestObject2()
            {
            });
            string json2 = JsonConvert.SerializeObject(new TestObject2()
            {
                TTL = TimeSpan.FromSeconds(5),
                TTLS = new TimeSpan[] { TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5) }
            });

            Assert.Equal("{\"TTL\":\"5s\"}", json2);
        }

        [Fact]
        public void NoConvertor()
        {
            string json = JsonConvert.SerializeObject(new TestObject3()
            {
                TTL = TimeSpan.FromSeconds(5)
            });

            Assert.NotEqual("{\"TTL\":\"5s\"}", json);
        }

        [Fact]
        public void DeserializeTimeSpan()
        {
            TestObject1 to = JsonConvert.DeserializeObject<TestObject1>("{\"TTL\":\"5s\"}");
            Assert.Equal(5, to.TTL.TotalSeconds);
        }

        [Fact]
        public void DeserializeNullableTimeSpan()
        {
            var to1 = JsonConvert.DeserializeObject<TestObject2>("{}");
            var to2 = JsonConvert.DeserializeObject<TestObject2>("{\"TTL\":\"5s\"}");

            Assert.Null(to1.TTL);
            Assert.NotNull(to2.TTL);
            Assert.Equal(5, to2.TTL.Value.TotalSeconds);
        }

        class TestObject1
        {
            [JsonConverter(typeof(TimeSpanJsonConverter))]
            public TimeSpan TTL { get; set; }
        }

        class TestObject2
        {
            [JsonConverter(typeof(TimeSpanJsonConverter))]
            public TimeSpan? TTL { get; set; }

            [JsonConverter(typeof(TimeSpanJsonConverter))]
            public TimeSpan[] TTLS { get; set; }
        }

        class TestObject3
        {
            public TimeSpan? TTL { get; set; }
        }
    }
}
