# 华为推送服务服务端C#示例代码
[English](README.md) | 中文

## 目录
 * [简介](#简介)
 * [安装](#安装)
 * [配置](#配置)
 * [环境要求](#环境要求)
 * [示例代码](#示例代码)
 * [授权许可](#授权许可)

## 简介

C#示例代码对华为推送服务（HUAWEI Push Kit）服务端接口进行封装，供您参考使用。

示例代码目录结构如下：
| 文件夹      | 说明 |
| ------------ | ----------- |
|AGConnectAdmin|封装服务端接口的类库|
|AGConnectAdmin.Examples|类库使用示例|

示例代码中的主要的类定义如下：
| 类名      | 说明 |
| ----------- | ----------- |
|AppOptions|应用相关配置|
|AGConnectApp|应用|
|AGConnectMessaging|推送服务相关接口的调用方法|
|Message|消息体|

## 安装

1. 解压示例代码。
2. 将解压后的AGConnectAdmin复制到你的Visual Studio Solution中适当的位置，在你的应用工程中引用对应的程序集即可。
3. 参考示例代码来使用AGConnectAdmin中的类。

## 配置

以下描述了AppOptions类的相关参数。

| 参数   | 说明 |
| ----------- | ----------- |
|ClientId|应用ID，从应用信息中获取|
|ClientSecret|应用访问密钥，从应用信息中获取|
|LoginUri|华为OAuth 2.0获取token的地址。详情请参见 [基于OAuth 2.0开放鉴权-客户端模式](https://developer.huawei.com/consumer/cn/doc/development/HMSCore-Guides/oauth2-0000001212610981#section128682386159?ha_source=hms1).|
|ApiBaseUri|推送服务的访问地址。详情请参见 [推送服务-下行消息](https://developer.huawei.com/consumer/cn/doc/development/HMSCore-Guides/android-server-dev-0000001050040110?ha_source=hms1).|

## 环境要求

示例代码工程需要使用Visual Studio 2017或以上版本的开发工具打开，类库提供以下种框架版本：

- .NET Framework 4.5以上
- .NET Standard 2.0以上

## 示例代码

AGConnectAdmin.Examples提供所有示例代码及相应功能。

1. 发送Android透传消息。
文件目录：AGConnectAdmin.Examples/Example.SendDataMessage.cs

2. 发送Android通知栏消息。
文件目录：AGConnectAdmin.Examples/Example.SendAndroidMessage.cs

3. 基于主题发送消息。
文件目录：AGConnectAdmin.Examples/Example.SendTopicMessage.cs

4. 基于条件发送消息。
文件目录：AGConnectAdmin.Examples/Example.SendConditionMessage.cs

5. 向华为快应用发送消息。
文件目录：AGConnectAdmin.Examples/Example.SendInstanceAppMessage.cs

6. 基于WebPush代理发送消息。
文件目录：AGConnectAdmin.Examples/Example.SendWebpushMessage.cs

7. 基于APNs代理发送消息。
文件目录：AGConnectAdmin.Examples/Example.SendApnsMessage.cs

8. 发送测试消息。
文件目录：AGConnectAdmin.Examples/Example.SendTestMessage.cs


## 技术支持
如果您对HMS Core还处于评估阶段，可在[Reddit社区](https://www.reddit.com/r/HuaweiDevelopers/)获取关于HMS Core的最新讯息，并与其他开发者交流见解。

如果您对使用HMS示例代码有疑问，请尝试：
- 开发过程遇到问题上[Stack Overflow](https://stackoverflow.com/questions/tagged/huawei-mobile-services?tab=Votes)，在`huawei-mobile-services`标签下提问，有华为研发专家在线一对一解决您的问题。
- 到[华为开发者论坛](https://developer.huawei.com/consumer/cn/forum/blockdisplay?fid=18?ha_source=hms1) HMS Core板块与其他开发者进行交流。

如果您在尝试示例代码中遇到问题，请向仓库提交[issue](https://github.com/HMS-Core/hms-push-serverdemo-csharp/issues)，也欢迎您提交[Pull Request](https://github.com/HMS-Core/hms-push-serverdemo-csharp/pulls)。

## 授权许可
华为推送服务C#示例代码经过[Apache License, version 2.0](http://www.apache.org/licenses/LICENSE-2.0)授权许可.
