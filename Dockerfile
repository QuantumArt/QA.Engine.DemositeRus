FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
LABEL stage=intermediate

WORKDIR /src
COPY *.sln nuget.config ./
ADD projectfiles.tar .
RUN dotnet restore

COPY . .

WORKDIR /src/Demosite
RUN dotnet publish "Demosite.csproj" -c Release -o /app/out -f net6.0

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ARG SERVICE_NAME
ENV SERVICE_NAME=${SERVICE_NAME:-QA.Engine.Demosite}

ARG SERVICE_VERSION
ENV SERVICE_VERSION=${SERVICE_VERSION:-0.0.0.0}

WORKDIR /app
COPY ./Fonts/Marlboro.ttf ./
RUN mkdir -p /usr/share/fonts/truetype/
RUN install -m644 Marlboro.ttf /usr/share/fonts/truetype/
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Demosite.dll"]
