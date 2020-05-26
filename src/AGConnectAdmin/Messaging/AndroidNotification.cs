/* Copyright 2018, Google Inc. All rights reserved.
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
 * 
 * 2019.12.27-Changed base class and some properties
 */

using System;
using System.Collections.Generic;
using System.Linq;
using AGConnectAdmin.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace AGConnectAdmin.Messaging
{
    /// <summary>
    /// Represents the Android-specific notification options that can be included in a
    /// <see cref="Message"/>.
    /// </summary>
    public class AndroidNotification : MessagePart<AndroidNotification>
    {
        /// <summary>
        /// Gets or sets the title of the Android notification. When provided, overrides the title
        /// set via <see cref="Notification.Title"/>.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the title of the Android notification. When provided, overrides the title
        /// set via <see cref="Notification.Body"/>.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the icon of the Android notification.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the notification icon color. Must be of the form <c>#RRGGBB</c>.
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the sound to be played when the device receives the notification.
        /// </summary>
        [JsonProperty("sound")]
        public string Sound { get; set; }

        /// <summary>
        /// Indicate whether the default sound should be played when the device receives the notification.
        /// </summary>
        [JsonProperty("default_sound")]
        public bool DefaultSound { get; set; }

        /// <summary>
        /// Gets or sets the notification tag. This is an identifier used to replace existing
        /// notifications in the notification drawer. If not specified, each request creates a new
        /// notification.
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the action associated with a user click on the notification. If specified,
        /// an activity with a matching Intent Filter is launched when a user clicks on the
        /// notification.
        /// </summary>
        [JsonProperty("click_action")]
        public ClickAction ClickAction { get; set; }

        /// <summary>
        /// Gets or sets the key of the body string in the app's string resources to use to
        /// localize the body text.
        /// </summary>
        [JsonProperty("body_loc_key")]
        public string BodyLocKey { get; set; }

        /// <summary>
        /// Gets or sets the collection of resource key strings that will be used in place of the
        /// format specifiers in <see cref="BodyLocKey"/>.
        /// </summary>
        [JsonProperty("body_loc_args")]
        public IEnumerable<string> BodyLocArgs { get; set; }

        /// <summary>
        /// Gets or sets the key of the title string in the app's string resources to use to
        /// localize the title text.
        /// </summary>
        [JsonProperty("title_loc_key")]
        public string TitleLocKey { get; set; }

        /// <summary>
        /// Gets or sets the collection of resource key strings that will be used in place of the
        /// format specifiers in <see cref="TitleLocKey"/>.
        /// </summary>
        [JsonProperty("title_loc_args")]
        public IEnumerable<string> TitleLocArgs { get; set; }

        /// <summary>
        /// Gets or sets the multi localization keys.
        /// </summary>
        [JsonProperty("multi_lang_key")]
        public JObject MultiLangKey { get; set; }

        /// <summary>
        /// Gets or sets the Android notification channel ID (new in Android O). The app must
        /// create a channel with this channel ID before any notification with this channel ID is
        /// received. If you don't send this channel ID in the request, or if the channel ID
        /// provided has not yet been created by the app, AGC uses the channel ID specified in the
        /// app manifest.
        /// </summary>
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the notify summary.
        /// </summary>
        [JsonProperty("notify_summary")]
        public string NotifySummary { get; set; }

        /// <summary>
        /// Gets or sets the image of the message.
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>
        /// Gets or set notification style.
        /// </summary>
        [JsonProperty("style")]
        public NotificationStyle Style { get; set; }

        /// <summary>
        /// Gets or sets big size title. It will override the <see cref="Title"/>
        /// if the <see cref="Style"/> is <see cref="NotificationStyle.BigText"/>.
        /// </summary>
        [JsonProperty("big_title")]
        public string BigTitle { get; set; }

        /// <summary>
        /// Gets or sets big size body. It will override the <see cref="Body"/>
        /// if the <see cref="Style"/> is <see cref="NotificationStyle.BigText"/>.
        /// </summary>
        [JsonProperty("big_body")]
        public string BigBody { get; set; }

        /// <summary>
        /// Gets or sets the auto clear time (milliseconds) of the message.
        /// </summary>
        [JsonProperty("auto_clear")]
        public int? AutoClear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("notify_id")]
        public int? NotifyId { get; set; }

        /// <summary>
        /// Group of notification
        /// </summary>
        [JsonProperty("group")]
        public string Group { get; set; }

        /// <summary>
        /// Badge notification
        /// </summary>
        [JsonProperty("badge")]
        public BadgeNotification Badge { get; set; }

        /// <summary>
        /// Gets or sets the ticker of the notification.
        /// </summary>
        [JsonProperty("ticker")]
        public string Ticker { get; set; }

        /// <summary>
        /// Indicate whether the notification should be hide automatically.
        /// </summary>
        [JsonProperty("auto_cancel")]
        public bool AutoCancel { get; set; }

        /// <summary>
        /// Gets or sets the event time of the notification which will affect the sorting.
        /// </summary>
        [JsonProperty("when")]
        public DateTime? EventTime { get; set; }

        /// <summary>
        /// Gets or set the priority of the notification.
        /// </summary>
        [JsonProperty("importance")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationImportance? Importance { get; set; }

        /// <summary>
        /// Indicate whether should be use default vibrate timings of device for the notification.
        /// </summary>
        [JsonProperty("use_default_vibrate")]
        public bool? DefaultVibrateTimings { get; set; }

        /// <summary>
        /// Gets or sets the vibrate timings of the notification.
        /// Each item represents seconds of vibrate timings and must be within 60.
        /// </summary>
        [JsonProperty("vibrate_config")]
        public IEnumerable<double> VibrateTimings { get; set; }

        /// <summary>
        /// Gets or sets the visibility of the notification.
        /// </summary>
        [JsonProperty("visibility")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationVisibility? Visibility { get; set; }

        /// <summary>
        /// Indicate whether should be use default light settings of device for the notification.
        /// </summary>
        [JsonProperty("use_default_light")]
        public bool? DefaultLightSettings { get; set; }

        /// <summary>
        /// Gets or sets the light settings.
        /// </summary>
        [JsonProperty("light_settings")]
        public LightSettings LightSettings { get; set; }

        /// <summary>
        /// Indicate whether show the notification when the application is active.
        /// </summary>
        [JsonProperty("foreground_show")]
        public bool? ForegroundShow { get; set; }

        /// <inheritdoc/>
        internal protected override AndroidNotification CopyAndValidate()
        {
            // copy
            var copy = new AndroidNotification()
            {
                Title = this.Title,
                Body = this.Body,
                Icon = this.Icon,
                Color = this.Color,
                Sound = this.Sound,
                DefaultSound = this.DefaultSound,
                Tag = this.Tag,
                ClickAction = this.ClickAction?.CopyAndValidate(),
                BodyLocKey = this.BodyLocKey,
                BodyLocArgs = this.BodyLocArgs?.ToList(),
                TitleLocKey = this.TitleLocKey,
                TitleLocArgs = this.TitleLocArgs?.ToList(),
                MultiLangKey = MultiLangKey == null ? null : new JObject(this.MultiLangKey),
                ChannelId = this.ChannelId,
                NotifySummary = this.NotifySummary,
                Image = this.Image,
                Style = this.Style,
                BigTitle = this.BigTitle,
                BigBody = BigBody,
                AutoClear = AutoClear,
                NotifyId = NotifyId,
                Group = Group,
                Badge = this.Badge?.CopyAndValidate(),
                Ticker = this.Ticker,
                AutoCancel = this.AutoCancel,
                EventTime = this.EventTime,
                Importance = this.Importance,
                DefaultVibrateTimings = this.DefaultVibrateTimings,
                DefaultLightSettings = this.DefaultLightSettings,
                VibrateTimings = this.VibrateTimings?.ToArray(),
                Visibility = this.Visibility,
                LightSettings = this.LightSettings?.CopyAndValidate(),
                ForegroundShow = this.ForegroundShow
            };

            // validate
            if (copy.Color != null && !Validator.IsColor(copy.Color))
            {
                throw new ArgumentException("Color must be in the form #RRGGBB.");
            }

            if (copy.Image != null && !Validator.IsHttpsUrl(copy.Image))
            {
                throw new ArgumentException("Image must be a https url.");
            }

            if (copy.TitleLocArgs?.Any() == true && string.IsNullOrEmpty(copy.TitleLocKey))
            {
                throw new ArgumentException($"{nameof(TitleLocKey)} is required when specifying {nameof(TitleLocArgs)}.");
            }

            if (copy.BodyLocArgs?.Any() == true && string.IsNullOrEmpty(copy.BodyLocKey))
            {
                throw new ArgumentException($"{nameof(BodyLocKey)} is required when specifying {nameof(BodyLocArgs)}.");
            }

            if (copy.VibrateTimings?.Any() == true && !Validator.IsAllInRange(copy.VibrateTimings, 0, 60))
            {
                throw new ArgumentException("Vibrate timing must be within 60 seconds");
            }

            return copy;
        }
    }
}
