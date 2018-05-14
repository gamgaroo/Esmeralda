# Esmeralda
ASP.NET Core Web App for hosting Unity3D WebGL apps with support of Continuous Delivery via Unity Cloud Webhooks.
Currently tested only on IIS.

[![GitHub release](https://img.shields.io/github/release/Gamgaroo/Esmeralda.js.svg)](https://GitHub.com/Gamgaroo/Esmeralda/releases/)
[![Build Status](https://travis-ci.org/Gamgaroo/Esmeralda.svg?branch=master)](https://travis-ci.org/Gamgaroo/Esmeralda)

## Getting Started
1) Configure *appsettings.json* with your Unity Cloud API Key in Unity section
2) Create a Unity Cloud webhook with URL http://<APP_URL>/api/builds
3) Start new build on Unity Cloud
4) After build completion Unity Cloud will push a notification to Esmeralda
5) After deployment completion the build will be accessible on http://<APP_URL>/index.html

## Notifications
You can enable Slack notifications. App notify on Deployment Start, Deployment Success and Deployment Failure.

1) Create Slack webhook on Slack admin page
2) Configure *appsettings.json* with your WebhookUrl in Slack section and set Enable to true
