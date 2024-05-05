FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

ARG RABBITMQ_HOST
ARG RABBITMQ_USER
ARG RABBITMQ_PASS

ARG REDIS_HOST
ARG REDIS_PORT

ENV RABBITMQ_$HOST=RABBITMQ_HOST RABBITMQ_USER=$RABBITMQ_USER RABBITMQ_PASS=$RABBITMQ_PASS REDIS_HOST=$REDIS_HOST REDIS_PORT=$REDIS_PORT

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Coingecko.dll"]
