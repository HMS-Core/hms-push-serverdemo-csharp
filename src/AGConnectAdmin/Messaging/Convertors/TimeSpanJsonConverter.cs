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

using Newtonsoft.Json;
using System;

namespace AGConnectAdmin.Messaging.Convertors
{
    /// <summary>
    /// Convert TimeSpan to a string.
    /// </summary>
    public class TimeSpanJsonConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return typeof(TimeSpan).Equals(objectType) || typeof(TimeSpan?).Equals(objectType);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException("Only string value can be convert to TimeSpan.");
            }
            try
            {
                var value = reader.Value.ToString();
                var segments = value.TrimEnd('s').Split('.');
                var seconds = long.Parse(segments[0]);
                var timeSpan = TimeSpan.FromSeconds(seconds);
                if (segments.Length == 2)
                {
                    var subsecondNanos = long.Parse(segments[1].TrimStart('0'));
                    timeSpan = timeSpan.Add(TimeSpan.FromMilliseconds(subsecondNanos / 1e6));
                }
                return timeSpan;
            }
            catch (Exception e)
            {
                throw new JsonSerializationException($"Convert value \"{reader.Value}\" to TimeSpan failed.", e);
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                if (serializer.NullValueHandling == NullValueHandling.Include)
                {
                    writer.WriteNull();
                }
                return;
            }

            var timeSpan = value as TimeSpan?;
            var totalSeconds = timeSpan.Value.TotalSeconds;
            var seconds = (long)Math.Floor(totalSeconds);
            var subsecondNanos = (long)((totalSeconds - seconds) * 1e9);
            if (subsecondNanos > 0)
            {
                writer.WriteValue(string.Format("{0}.{1:D9}s", seconds, subsecondNanos));
            }

            writer.WriteValue(string.Format("{0}s", seconds));
        }
    }
}
