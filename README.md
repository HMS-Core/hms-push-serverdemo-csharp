# HMS Pushkit Csharp Severdemo
## Table of Contents
* [Introduction](#introduction)
* [Installation](#installation)
* [Configuration](#configuration)
* [Supported Environment](#supported-environment)
* [Sample Code](#sample-code)
* [License](#license)

## Introduction

C# sample code encapsulates APIs of the HUAWEI Push Kit server. It provides many sample programs for your reference or usage. The following table describes folders of C# sample code:

| Folder       | Description |
| ------------ | ----------- |
|AGConnectAdmin|Class library encapsulated for HUAWEI Push Kit.|
|AGConnectAdmin.Examples|Examples of using the Class library.|

The following table defines main classes in the sample code:

| Class       | Description |
| ----------- | ----------- |
|AppOptions|App configurations.|
|AGConnectApp|App.|
|AGConnectMessaging|HUAWEI Push Kit API calling methods.|
|Message|Body of a message.|

## Installation

1. Decompress the sample code.
2. Copy the decompressed **AGConnectAdmin** project to a proper location in your app's Visual Studio Solution, and then reference the project in your app project.
3. Refer to the example codes to find out how to use classes in **AGConnectAdmin** project.

## Configuration

The following table describes parameters of the **AppOptions** class:

| Parameter   | Description |
| ----------- | ----------- |
|ClientId|App ID, which is obtained from app information.|
|ClientSecret|Secret access key of an app, which is obtained from app information.|
|LoginUri|URL for the Huawei OAuth 2.0 service to obtain a token, please refer to [Generating an App-Level Access Token](https://developer.huawei.com/consumer/en/doc/development/parts-Guides/generating_app_level_access_token).|
|ApiBaseUri|URL for accessing HUAWEI Push Kit, please refer to [Sending Messages](https://developer.huawei.com/consumer/en/doc/development/HMS-References/push-sendapi).|

## Supported Environment

Sample code projects need to be opened by Visual Studio 2017 or a later version and supports the following frameworks:

- .NET Framework 4.5+
- .NET Standard 2.0+

## Sample Code

All sample code are in **AGConnectAdmin.Examples** project included below use cases.

1. Send an Android data message.
   > AGConnectAdmin.Examples/Example.SendDataMessage.cs
2. Send an Android notification message.
   > AGConnectAdmin.Examples/Example.SendAndroidMessage.cs
3. Send a message by topic.
   > AGConnectAdmin.Examples/Example.SendTopicMessage.cs
4. Send a message by conditions.
   > AGConnectAdmin.Examples/Example.SendConditionMessage.cs
5. Send a message to a Huawei quick app.
   > AGConnectAdmin.Examples/Example.SendInstanceAppMessage.cs
6. Send a message through the WebPush agent.
   > AGConnectAdmin.Examples/Example.SendWebpushMessage.cs
7. Send a message through the APNs agent.
   > AGConnectAdmin.Examples/Example.SendApnsMessage.cs
8. Send a test message.
   > AGConnectAdmin.Examples/Example.SendTestMessage.cs

## Question or issues
If you want to evaluate more about HMS Core,
[r/HMSCore on Reddit](https://www.reddit.com/r/HMSCore/) is for you to keep up with latest news about HMS Core, and to exchange insights with other developers.

If you have questions about how to use HMS samples, try the following options:
- [Stack Overflow](https://stackoverflow.com/questions/tagged/huawei-mobile-services) is the best place for any programming questions. Be sure to tag your question with 
**huawei-mobile-services**.
- [Huawei Developer Forum](https://forums.developer.huawei.com/forumPortal/en/home?fid=0101187876626530001) HMS Core Module is great for general questions, or seeking recommendations and opinions.

If you run into a bug in our samples, please submit an [issue](https://github.com/HMS-Core/hms-push-serverdemo-csharp/issues) to the Repository. Even better you can submit a [Pull Request](https://github.com/HMS-Core/hms-push-serverdemo-csharp/pulls) with a fix.

## License
Pushkit Csharp sample codes are licensed under the Apache License, version 2.0.
