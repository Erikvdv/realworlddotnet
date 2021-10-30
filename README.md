### migrations
Add migration by going to Data folder and execute:
dotnet ef migrations add MigrationName --startup-project ../Api/Api.csproj

Run db upgrade:
dotnet ef database update --startup-project ../Api/Api.csproj


### certificates
Add the identity_server testing certificate to your local user store. The certificate is provided in the /docs folder
and the password is "password"