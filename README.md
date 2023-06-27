# HMS Core Push Kit Sample Code (C#)
English | [中文](README_ZH.md)
## Contents
 * [Introduction](#Introduction)
 * [Installation](#Installation)
 * [Configuration](#Configuration)
 * [Environment Requirements](#Environment-Requirements)
 * [Sample Code](#Sample-Code)
 * [Technical Support](#technical-support)
 * [License](#License)

## Introduction

The sample code for C# encapsulates the server-side APIs of Push Kit, for your reference or direct use.

The following table describes folders of C# sample code.
| Folder| Description|
| ------------ | ----------- |
|AGConnectAdmin|Class library where Push Kit server APIs are encapsulated.|
|AGConnectAdmin.Examples|Class library usage examples.|

The following table describes main classes used in the sample code.
| Class Name| Description|
| ----------- | ----------- |
|AppOptions|App-related configuration.|
|AGConnectApp|App.|
|AGConnectMessaging|Push Kit API calling methods.|
|Message|Message body.|

## Installation

1. Decompress the sample code.
2. Copy **AGConnectAdmin** to a proper position in your Visual Studio solution and reference the corresponding assembly in your project.
3. Use the classes in **AGConnectAdmin** by referring to the sample code.

## Configuration

The following table describes the parameters related to the **AppOptions** class.

| Parameter| Description|
| ----------- | ----------- |
|ClientId|App ID, which is obtained from the app information.|
|ClientSecret|App secret, which is obtained from the app information.|
|LoginUri|URL for Huawei OAuth 2.0 to obtain a token. For details, please refer to [OAuth 2.0-based Authentication](https://developer.huawei.com/consumer/en/doc/development/HMSCore-Guides/oauth2-0000001212610981).|
|ApiBaseUri|Access address of Push Kit. For details, please refer to [Downlink Message Sending](https://developer.huawei.com/consumer/en/doc/development/HMSCore-Guides/android-server-dev-0000001050040110?ha_source=hms1).|

## Environment Requirements

The demo projects need to be opened using Visual Studio 2017 or a later version. The following framework versions are supported:

- .NET Framework 4.5 or later
- .NET Standard 2.0 or later

## Sample Code

**AGConnectAdmin.Examples** provides all sample code and corresponding functions.

1. Send an Android data message. Code location: [SendDataMessage.cs](src/AGConnectAdmin.Examples/Example.SendDataMessage.cs)

2. Send an Android notification message. Code location: [SendAndroidMessage.cs](src/AGConnectAdmin.Examples/Example.SendAndroidMessage.cs)

3. Send a message by topic.Code location: [SendTopicMessage.cs](src/AGConnectAdmin.Examples/Example.SendTopicMessage.cs)

4. Send a message by conditions. Code location: [SendConditionMessage.cs](src/AGConnectAdmin.Examples/Example.SendConditionMessage.cs)

5. Send a message to a Huawei quick app. Code location: [SendInstanceAppMessage.cs](src/AGConnectAdmin.Examples/Example.SendInstanceAppMessage.cs)

6. Send a message through the WebPush agent. Code location: [SendWebpushMessage.cs](src/AGConnectAdmin.Examples/Example.SendWebpushMessage.cs)

7. Send a message through the APNs agent. Code location: [SendApnsMessage.cs](src/AGConnectAdmin.Examples/Example.SendApnsMessage.cs)

8. Send a test message. Code location: [SendTestMessage.cs](src/AGConnectAdmin.Examples/Example.SendTestMessage.cs)


## Technical Support
You can visit the [Reddit community](https://www.reddit.com/r/HuaweiDevelopers/) to obtain the latest information about HMS Core and communicate with other developers.

If you have any questions about the sample code, try the following:
- Visit [Stack Overflow](https://stackoverflow.com/questions/tagged/huawei-mobile-services?tab=Votes), submit your questions, and tag them with `huawei-mobile-services`. Huawei experts will answer your questions.
- Visit the HMS Core section in the [HUAWEI Developer Forum](https://forums.developer.huawei.com/forumPortal/en/home?fid=0101187876626530001?ha_source=hms1) and communicate with other developers.

If you encounter any issues when using the sample code, submit your [issues](https://github.com/HMS-Core/hms-push-serverdemo-csharp/issues) or submit a [pull request](https://github.com/HMS-Core/hms-push-serverdemo-csharp/pulls).

## License
The sample code is licensed under [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0).
