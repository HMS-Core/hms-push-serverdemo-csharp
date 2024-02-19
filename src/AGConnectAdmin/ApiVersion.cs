namespace AGConnectAdmin
{
    /// <summary>
    /// Huawei PushKit API version level
    /// </summary>
    public enum ApiVersion
    {
        /// <summary>
        /// API Base Uri V1 (App-level)
        /// </summary>
        V1,
        /// <summary>
        /// API Base Uri V2 (Project-level)
        /// </summary>
        V2
    }

    /// <summary>
    /// ApiVersion Enum extension
    /// </summary>
    public static class ApiVersionExt
    {
        private const string _apiBaseUriPrefix = "https://push-api.cloud.huawei.com/v";
        private const string _appLevelApiBaseUri = _apiBaseUriPrefix + "1";
        private const string _projLevelApiBaseUri = _apiBaseUriPrefix + "2";

        /// <summary>
        /// Api version base Uri
        /// </summary>
        public static string ApiBaseUri(this ApiVersion apiVersion)
        {
            switch (apiVersion)
            {
                case ApiVersion.V1: return _appLevelApiBaseUri;
                case ApiVersion.V2: return _projLevelApiBaseUri;
            }
            return string.Empty;
        }
    }
}
