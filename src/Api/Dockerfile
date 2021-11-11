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

WORKDIR /tst/Unit.Tests
RUN dotnet test --logger:trx

FROM build AS publish
WORKDIR /
RUN pwd
RUN dotnet publish "src/Api/Api.csproj" -c Release --no-build -o app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 443
COPY --from=publish /app ./
ENTRYPOINT ["dotnet", "Api.dll"]