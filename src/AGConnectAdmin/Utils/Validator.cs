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
using System.Text.RegularExpressions;
using System.Linq;

namespace AGConnectAdmin.Utils
{
    internal static class Validator
    {
        private static Regex COLOR_PATTERN = new Regex("^#[0-9a-f]{6}$", RegexOptions.IgnoreCase);

        internal static bool IsUrl(string url)
        {
            Uri uri;
            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
            }
            return false;
        }

        internal static bool IsHttpsUrl(string url)
        {
            Uri uri;
            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                return uri.Scheme == Uri.UriSchemeHttps;
            }
            return false;
        }

        internal static bool IsColor(string color)
        {
            return COLOR_PATTERN.Match(color).Success;
        }

        internal static bool IsInRange<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (min.CompareTo(max) == 1)
            {
                throw new ArgumentException("The specified range is not correct, maximum must greater than or equal to minimum.");
            }
            return min.CompareTo(value) <= 0 && max.CompareTo(value) >= 0;
        }

        internal static bool IsAllInRange<T>(IEnumerable<T> values, T min, T max) where T : IComparable<T>
        {
            return values.All(i => IsInRange(i, min, max));
        }
    }
}
