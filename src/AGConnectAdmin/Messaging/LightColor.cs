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

using AGConnectAdmin.Utils;
using Newtonsoft.Json;
using System;

namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// Represents an ARGB (alpha, red, green, blue) color.
    /// </summary>
    public sealed class LightColor : MessagePart<LightColor>
    {
        /// <summary>
        /// Gets the alpha component value of this Color structure.
        /// </summary>
        [JsonProperty("alpha")]
        public double Alpha { get; private set; }

        /// <summary>
        /// Gets the red component value of this Color structure.
        /// </summary>
        [JsonProperty("red")]
        public double Red { get; private set; }

        /// <summary>
        /// Gets the green component value of this Color structure.
        /// </summary>
        [JsonProperty("green")]
        public double Green { get; private set; }

        /// <summary>
        /// Gets the blue component value of this Color structure.
        /// </summary>
        [JsonProperty("blue")]
        public double Blue { get; private set; }

        /// <summary>
        /// Creates a Color structure from the four ARGB component (alpha, red, green, and blue) values.
        /// </summary>
        /// <param name="alpha">The alpha value (from 0 to 1)</param>
        /// <param name="red">The red value (from 0 to 1)</param>
        /// <param name="green">The green value (from 0 to 1)</param>
        /// <param name="blue">The blue value (from 0 to 1)</param>
        /// <returns></returns>
        public static LightColor FromArbg(double alpha, double red, double green, double blue)
        {
            return new LightColor()
            {
                Alpha = alpha,
                Red = red,
                Green = green,
                Blue = blue
            };
        }

        /// <summary>
        /// Creates a Color structure from the specified 8-bit color values (red, green, and blue). The alpha value is implicitly 1.0 (fully opaque).
        /// </summary>
        /// <param name="red">The red value (from 0 to 1)</param>
        /// <param name="green">The green value (from 0 to 1)</param>
        /// <param name="blue">The blue value (from 0 to 1)</param>
        /// <returns></returns>
        public static LightColor FromArbg(double red, double green, double blue)
        {
            return FromArbg(1.0f, red, green, blue);
        }

        /// <inheritdoc/>
        internal protected override LightColor CopyAndValidate()
        {
            if (!Validator.IsInRange(this.Red, 0, 1) ||
               !Validator.IsInRange(this.Green, 0, 1) ||
               !Validator.IsInRange(this.Blue, 0, 1))
            {
                throw new ArgumentException("The value of red/blue/green color must be within [0,1].");
            }

            return FromArbg(Alpha, Red, Green, Blue);
        }
    }
}
