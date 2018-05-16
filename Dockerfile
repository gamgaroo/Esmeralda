FROM microsoft/aspnetcore-build:2.0 as build-env
WORKDIR /app

COPY . ./
RUN dotnet restore src/Gamgaroo.Esmeralda.App/*.csproj
RUN dotnet publish src/Gamgaroo.Esmeralda.App/*.csproj -c Release -o out

FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/src/Gamgaroo.Esmeralda.App/out ./
ENTRYPOINT ["dotnet", "Gamgaroo.Esmeralda.App.dll"]
EXPOSE 80
ENV Admin:Username admin
ENV Admin:Password admin
ENV Slack:Enable false
ENV Slack:WebhookUrl WEBHOOK_URL
ENV Unity:ApiKey API_KEY
ENV Unity:CloudUrl https://build-api.cloud.unity3d.com