# netcore-ef-util

[![devel0 MyGet Build Status](https://www.myget.org/BuildSource/Badge/devel0?identifier=ccad32de-3eb4-472d-967c-86817bc95994)](https://www.myget.org/)

.NET core entity framework util

## install and usage

browse [myget istructions](https://www.myget.org/feed/devel0/package/nuget/netcore-ef-util)

add `nuget.config` where your solution or csproj that refer this library in order to allow other to restore correcly myget dependencies.

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
