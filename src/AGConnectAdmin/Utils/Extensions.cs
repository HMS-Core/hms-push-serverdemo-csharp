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

namespace AGConnectAdmin
{
    /// <summary>
    /// A collection of extension methods for internal use in the SDK.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// A utility method for throwing an System.ArgumentNullException if the object is null.
        /// </summary>
        public static T ThrowIfNull<T>(this T obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }

            return obj;
        }

        /// <summary>
        /// Creates a shallow copy of a collection of key-value pairs.
        /// </summary>
        public static IReadOnlyDictionary<TKey, TValue> Copy<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var copy = new Dictionary<TKey, TValue>();
            foreach (var entry in source)
            {
                copy[entry.Key] = entry.Value;
            }

            return copy;
        }
    }
}
