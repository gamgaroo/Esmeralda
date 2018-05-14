# Esmeralda
ASP.NET Core Web App for hosting Unity3D WebGL builds with support of Continuous Delivery via Unity Cloud Webhooks.

[![GitHub release](https://img.shields.io/github/release/Gamgaroo/Esmeralda/all.svg)](https://GitHub.com/Gamgaroo/Esmeralda/releases/)
[![Build Status](https://travis-ci.org/Gamgaroo/Esmeralda.svg?branch=master)](https://travis-ci.org/Gamgaroo/Esmeralda)

## Getting Started
### Host on IIS
1) Configure *Settings/unity.json* with your Unity Cloud API Key in Unity section
2) Create a Unity Cloud webhook with URL http://{esmeralda-url}/api/builds
3) Start new build on Unity Cloud
4) After build completion Unity Cloud will push a notification to Esmeralda
5) After deployment completion the build will be accessible on http://{esmeralda-url}/index.html

### Run in Docker container
1) Run docker container
```
docker run gamgaroo/esmeralda -e Unity:ApiKey="API_KEY" -p 80:80
```
2) Create a Unity Cloud webhook with URL http://{esmeralda-url}/api/builds
3) Start new build on Unity Cloud
4) After build completion Unity Cloud will push a notification to Esmeralda
5) After deployment completion the build will be accessible on http://{esmeralda-url}/index.html

### Environment variables

**Required**

Unity:ApiKey

**Optional**

Slack:Enable (Default: _false_)

Slack:WebhookUrl (Defalt: _WEBHOOK_URL_)

Unity:CloudUrl (Default: _[https://build-api.cloud.unity3d.com](https://build-api.cloud.unity3d.com)_)

**Example**
```
docker run -e Unity:ApiKey="API_KEY" -e Slack:Enable=true -e Slack:WebhookUrl="WEBHOOK_URL" -p 80:80 gamgaroo/esmeralda
```

## Settings API
You can configure Esmeralda using settings API

You can _GET_ or _POST_ on _http://{esmeralda-url}/api/settings_

```json
{
    "unity": {
        "apiKey": "API_KEY",
        "cloudUrl": "https://build-api.cloud.unity3d.com"
    },
    "slack": {
        "enable": false,
        "webhookUrl": "WEBHOOK_URL"
    }
}
```

## Notifications
You can enable Slack notifications. App notify on Deployment Start, Deployment Success and Deployment Failure.

1) Create Slack webhook on Slack admin page
2) Configure *Settings/slack.json* with your WebhookUrl in Slack section and set Enable to true
