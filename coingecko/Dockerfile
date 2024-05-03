FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

ARG RABBITMQ_CONNECTION_STRING
ARG REDIS_CONNECTION_STRING
ENV RABBITMQ_CONNECTION_STRING=$RABBITMQ_CONNECTION_STRING REDIS_CONNECTION_STRING=$REDIS_CONNECTION_STRING

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Coingecko.dll"]
