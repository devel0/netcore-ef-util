# netcore-ef-util

[![NuGet Badge](https://buildstats.info/nuget/netcore-util)](https://www.nuget.org/packages/netcore-ef-util/)

.NET core entity framework util

## install

- [nuget package](https://www.nuget.org/packages/netcore-ef-util/)

## usage

- [ExecSQL](https://github.com/devel0/worked-hours-tracker/blob/cec37c8c07bef5e07c03ca2c9a129094faab1fa0/WorkedHoursTrackerWebapi/Controllers/ApiController.cs#L481)

- [API](doc/api/EFUtil/Util.md)

## how this project was built

```sh
mkdir netcore-ef-util
cd netcore-ef-util

dotnet new sln
dotnet new classlib -n netcore-ef-util

cd netcore-ef-util
dotnet add package Microsoft.EntityFrameworkCore --version 2.2.0-preview1-35029
dotnet add package Microsoft.EntityFrameworkCore.Relational --version 2.2.0-preview1-35029
dotnet add package Microsoft.Extensions.Identity.Stores --version 2.2.0-preview1-35029
cd ..

dotnet new xunit -n test
cd test
dotnet add reference ../netcore-ef-util/netcore-ef-util.csproj
cd ..

dotnet sln netcore-ef-util.sln add netcore-ef-util/netcore-ef-util.csproj
dotnet restore
dotnet build
dotnet test test/test.csproj
```
