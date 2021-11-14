# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY src/Api/*.csproj Api/
COPY src/Core/*.csproj Core/
COPY src/Data/*.csproj Data/
COPY src/Infrastructure/*.csproj Infrastructure/
WORKDIR /src/Api
RUN dotnet restore Api.csproj

WORKDIR /
COPY . .
WORKDIR /src
RUN dotnet build "Api/Api.csproj" -c Release --no-restore

FROM build AS test
WORKDIR /tst/Unit.Tests
ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM build AS publish
WORKDIR /
RUN pwd
RUN dotnet publish "src/Api/Api.csproj" -c Release --no-build -o app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
RUN useradd -ms /bin/bash  dotnet
USER dotnet
WORKDIR /app
COPY --from=publish /app ./

EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "Api.dll"]