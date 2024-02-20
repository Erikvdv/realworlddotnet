# ![RealWorld Example App](logo.png)

> ### [Dotnet 8] codebase containing real world examples (CRUD, auth, advanced patterns, etc) that adheres to the [RealWorld](https://github.com/gothinkster/realworld) spec and API.

For a version using **dotnet minimal api's** check [here](https://github.com/Erikvdv/realworldapiminimal)

### [Demo](https://demo.realworld.io/)&nbsp;&nbsp;&nbsp;&nbsp;[RealWorld](https://github.com/gothinkster/realworld)

This codebase was created to demonstrate a fully fledged fullstack application built with **[Dotnet 8]** including CRUD operations, authentication, routing, pagination, and more.

We've gone to great lengths to adhere to the **[Dotnet 8]** community styleguides & best practices.

For more information on how to this works with other frontends/backends, head over to the [RealWorld](https://github.com/gothinkster/realworld) repo.

# How it works

Traditional Clean Architecture setup using Dotnet 6.
Consisting of the following layers:

- api
- core
- data
- infrastructure

Build using the following features:

- the new WebApplication.CreateBuilder(args)
- file scoped namespaces
- Entity Framework Core with SQLite db
- serilog for logging
- logging integrated with application insights, including realtime monitoring
- Hellang.Middleware.ProblemDetails for consistent error output
- sonarlint for code scanning

# Getting started

This solution uses an in memory SQLite db. In case you want to use a file based db
then you can add migrations as indicated below

## migrations

Add migration by going to Data folder and execute:
dotnet ef migrations add MigrationName --startup-project ../Api/Api.csproj

Run db upgrade:
dotnet ef database update --startup-project ../Api/Api.csproj
