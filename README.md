# zup
A simple project management platform designed for ease-of-use. Part of a semester project for a database class.

## Building the Web API locally
This guide assumes use of a UNIX based operating system. While it is possible to run the **Zup Web API** on a Windows device, it will not be covered in detail here and success of that is not guarenteed.

### Installation
Ensure the latest version of the **Zup Web API** has been received using `$ git pull`. The API also requires both `PostgreSQL v16` and the `.NET Entity Framework` which may need to be installed, or updated. 

#### .NET and Entity Framework
For instructions on how to install `.NET`, follow the guide on [Microsoft's site](https://learn.microsoft.com/en-us/dotnet/core/install/linux) to install `.NET 8` for Linux. Some distributions have it available within their own official package repositories. 

Confirm `.NET` has been properly installed by running the following commands:
``` 
$ dotnet --list-sdks
$ dotnet --list-runtimes
```

Both commands should return at least one respective path with a version 8.x.x. With `.NET` installed, the `.NET Entity Framework` and project packages must also be installed. It is recommended to install `.NET Entity Framework` globally, which can be done with:
```
$ dotnet tool install --global dotnet-ef
```

Alternatively, it can be installed in only the project's scope by removing the 
`--global` option.

The project is setup in a way that no packages should need to be installed manually. If for some reason missing packages are creating a problem, the following should be installed:
```
   > Microsoft.AspNetCore.Authentication.JwtBearer      8.0.4
   > Microsoft.EntityFrameworkCore.Tools                8.0.4
   > Npgsql.EntityFrameworkCore.PostgreSQL              8.0.2
   ----------------------------------------------------------
   > Swashbuckle.AspNetCore                             6.4.0
   > DotNetEnv                                          3.0.0
```

Use `$ dotnet list package`, then for any packages not present, install them with `$ dotnet add package <package name>`.

#### Postgres
The **Zup Web API** uses `PostgreSQL v16` for data storage and management. For the project to be run locally, `Postgres` must be installed. In most mainline Linux distributions, it can be installed through their official package repositories, or it can be installed directly from source on the [PostgreSQL Downloads page](https://www.postgresql.org/download/). For help with the installation process, follow their [installation tutorial](https://www.postgresql.org/docs/current/tutorial-install.html).

### Setup

#### Environment Variables
The API relies on a `.env` file to hold senstive information seperate from the source code of the project. To create this file run:
```
$ nano /path/to/zup/webapi/.env
```

At the time of writing, the `.env` file needs two variables: `DEFAULT_POSTGRES` and `JWT_KEY`. 
    
- The `DEFAULT_POSTGRES` variable should contain the connection string for the `Postgres` database. Ensure the user id and password are correct, as failing to connect to the database will prevent the API from starting. 
- The `JWT_KEY` is a key used to generate JWTs for authentication. The minimum requirement for this key is **256 bits** or the encryption will fail, preventing the API from starting.

An example is provided in `zup/webapi/example.env`.

#### Migrating the database
Migration files should be included in the repository, but if any changes have been made to the source code since those migrations were created, a new migration must be generated with `.NET Entity Framework`.
```
$ dotnet ef migrations add <MigrationName>
```
**Note:** `C#` expects `CamelCase` names for migrations and will produce a warning if other cases are used, such as `pascal_case`.

With an up-to-date migration, the database can be updated with the following command:
```
$ dotnet ef database update
```

If it is not already, `PostgreSQL` needs to be running for this to work.

### Building
With all the proper software installed, variables set, and the database configured, the project is ready to be built. While in the project directory `zup/webapi`:
```
$ dotnet build 
$ dotnet run
```

This will build the project and run it. The output after it has run will show the port the API is using, most likely `5001`.
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5001 <--- find the port here
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /path/to/zup/webapi
```

The **Zup Web API** is now running and ready to be accessed. For reference on routes, navigate to `http://localhost:5001/swagger/` to view the OpenAPI schema.