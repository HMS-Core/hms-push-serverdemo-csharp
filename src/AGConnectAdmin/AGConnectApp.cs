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
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AGConnectAdmin.Tests,PublicKey=" +
"002400000480000094000000060200000024000052534131000400000100010081328559eaab41" +
"055b84af73469863499d81625dcbba8d8decb298b69e0f783a0958cf471fd4f76327b85a7d4b02" +
"3003684e85e61cf15f13150008c81f0b75a252673028e530ea95d0c581378da8c6846526ab9597" +
"4c6d0bc66d2462b51af69968a0e25114bde8811e0d6ee1dc22d4a59eee6a8bba4712cba839652f" +
"badddb9c")]
namespace AGConnectAdmin
{
    internal delegate TResult ServiceFactory<out TResult>()
        where TResult : IAGConnectService;

    /// <summary>
    /// This is the entry point to the AGConnect Admin SDK. It holds configuration and state common
    /// to all APIs exposed from the SDK.
    /// <para>Use one of the provided <c>Create()</c> methods to obtain a new instance.</para>
    /// </summary>
    public sealed class AGConnectApp
    {
        private const string DefaultAppName = "[DEFAULT]";

        private static readonly Dictionary<string, AGConnectApp> Apps = new Dictionary<string, AGConnectApp>();

        // Guards the mutable state local to an app instance.
        private readonly object appLock = new object();
        private readonly AppOptions options;

        // A collection of stateful services initialized using this app instance (e.g.
        // AGConnectAuth). Services are tracked here so they can be cleaned up when the app is
        // deleted.
        private readonly Dictionary<string, IAGConnectService> services = new Dictionary<string, IAGConnectService>();
        private bool deleted = false;

        /// <summary>
        /// Gets the default app instance. This property is <c>null</c> if the default app instance
        /// doesn't yet exist.
        /// </summary>
        public static AGConnectApp DefaultInstance
        {
            get
            {
                return GetInstance(DefaultAppName);
            }
        }

        private AGConnectApp(AppOptions options, string name)
        {
            this.options = new AppOptions(options);
            this.Name = name;
        }

        /// <summary>
        /// Gets a copy of the <see cref="AppOptions"/> this app was created with.
        /// </summary>
        public AppOptions Options
        {
            get
            {
                return new AppOptions(this.options);
            }
        }

        /// <summary>
        /// Gets the name of this app.
        /// </summary>
        private string Name { get; }

        /// <summary>
        /// Returns the app instance identified by the given name.
        /// </summary>
        /// <returns>The <see cref="AGConnectApp"/> instance with the specified name or null if it
        /// doesn't exist.</returns>
        /// <exception cref="System.ArgumentException">If the name argument is null or empty.</exception>
        /// <param name="name">Name of the app to retrieve.</param>
        private static AGConnectApp GetInstance(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("App name to lookup must not be null or empty");
            }

            lock (Apps)
            {
                AGConnectApp app;
                if (Apps.TryGetValue(name, out app))
                {
                    return app;
                }
            }

            return null;
        }

        /// <summary>
        /// Creates the default app instance with the specified options.
        /// </summary>
        /// <returns>The newly created <see cref="AGConnectApp"/> instance.</returns>
        /// <exception cref="System.ArgumentException">If the default app instance already
        /// exists.</exception>
        /// <param name="options">Options to create the app with. Must at least contain the
        /// <c>Credential</c>.</param>
        public static AGConnectApp Create(AppOptions options)
        {
            return Create(options, DefaultAppName);
        }

        /// <summary>
        /// Creates an app with the specified name and options.
        /// </summary>
        /// <returns>The newly created <see cref="AGConnectApp"/> instance.</returns>
        /// <exception cref="System.ArgumentException">If the default app instance already
        /// exists.</exception>
        /// <param name="options">Options to create the app with. Must at least contain the
        /// <c>Credential</c>.</param>
        /// <param name="name">Name of the app.</param>
        private static AGConnectApp Create(AppOptions options, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("App name must not be null or empty");
            }

            if (options == null)
            {
                throw new ArgumentException("options must not be null");
            }

            lock (Apps)
            {
                if (Apps.ContainsKey(name))
                {
                    throw new ArgumentException($"AGConnectApp named {name} already exists.");
                }

                var app = new AGConnectApp(options, name);
                Apps.Add(name, app);
                return app;
            }
        }

        /// <summary>
        /// Deletes this app instance and cleans up any state associated with it. Once an app has
        /// been deleted, accessing any services related to it will result in an exception.
        /// If the app is already deleted, this method is a no-op.
        /// </summary>
        public void Delete()
        {
            // Clean up local state
            lock (this.appLock)
            {
                this.deleted = true;
                foreach (var entry in this.services)
                {
                    try
                    {
                        entry.Value.Delete();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                this.services.Clear();
            }

            // Clean up global state
            lock (Apps)
            {
                Apps.Remove(this.Name);
            }
        }

        /// <summary>
        /// Deleted all the apps created so far. Used for unit testing.
        /// </summary>
        internal static void DeleteAll()
        {
            lock (Apps)
            {
                var copy = new Dictionary<string, AGConnectApp>(Apps);
                foreach (var entry in copy)
                {
                    entry.Value.Delete();
                }

                if (Apps.Count > 0)
                {
                    throw new InvalidOperationException("Failed to delete all apps");
                }
            }
        }

        /// <summary>
        /// Returns the current version of the .NET assembly.
        /// </summary>
        /// <returns>A version string in major.minor.patch format.</returns>
        internal static string GetSdkVersion()
        {
            const int majorMinorPatch = 3;
            return typeof(AGConnectApp).GetTypeInfo().Assembly.GetName().Version.ToString(majorMinorPatch);
        }

        internal T GetOrInit<T>(string id, ServiceFactory<T> initializer)
            where T : class, IAGConnectService
        {
            lock (this.appLock)
            {
                if (this.deleted)
                {
                    throw new InvalidOperationException("Cannot use an app after it has been deleted");
                }

                IAGConnectService service;
                if (!this.services.TryGetValue(id, out service))
                {
                    service = initializer();
                    this.services.Add(id, service);
                }

                return (T)service;
            }
        }
    }
}
