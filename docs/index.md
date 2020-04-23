![Nuget](https://img.shields.io/nuget/v/ZimLabs.Database.MySql) ![GitHub release (latest by date)](https://img.shields.io/github/v/release/InvaderZim85/ZimLabs.Database.MySql)

This project provides a simple way to create a connection to a MySql database.

## Installation

You can install this package via NuGet or download the sources.

```powershell
Install-Package ZimLabs.Database.MySql
```

## Usage
The usage of the connector is very easy.

The package provides for differen constructors:

```csharp
using ZimLabs.Database.MySql;

// 1: Constructor with server and database name.
var connector = new Connector("Server", "Database", 
    "User", "Password", 3306); // The port is optional. Default value is 3306

// 2: Constructor with server, database, user and password (this time as SecureString)
var connector = new Connector("Server", "Database", 
    "User", "Password".ToSecureString(), 3306);

// 3: Constructor with a defined connection string
var conString = new MySqlConnectionStringBuilder
{
    Server = "Server",
    Database = "Database",
    UserID = "User",
    Password = "Password",
    Port = 3306
}.ConnectionString.ToSecureString();

var connector = new Connector(conString);

// 4: Constructor with settings class
var settings = new DatabaseSettings
{
    Server = "127.0.0.1",
    Database = "MyFancyDatabase",
    UserId = "Username",
    Password = "Password".ToSecureString(),
    Port = 3306
};

var connector = new Connector(settings);
```

> **NOTE**: The extension method `ToSecureString()` is located in the `Helper` class, which is a part of the package.

## Example
Here a small example (with usage of [Dapper](https://dapper-tutorial.net))

```csharp
// The settings
var settings = new DatabaseSettings
{
    Server = "127.0.0.1",
    Database = "MyFancyDatabase",
    UserId = "Username",
    Password = "Password".ToSecureString(),
    Port = 3306
};

var connector = new Connector(settings);

// Perform a query
const string query = "SELECT Id, Name, Mail FROM person AS p";

var personList = connector.Connection.Query<Person>(query).ToList();
```

## Demo project
For more information about the usage of the connector take a look at the demo project: [Demo Project](https://github.com/InvaderZim85/ZimLabs.Database.MySql/tree/master/Demo)