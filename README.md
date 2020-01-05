.Net Core 3.0 Web API Design
============================

.Net Core 3.0 Web Api design versioning, Jwt bearer authentication, swagger documentation, serilog logger,
 EF Core Repository pattern, Db Migration, xUnit Tests, dependency injection and project layers 
 

 Project layers
 --------------

<img src="./screenshot/solution.JPG" max-height="400" alt="solution" />
<img src="./screenshot/solution-2.JPG" max-height="400" alt="solution" />


Web Api versioning
 -----------------
v1.0
<br />
<img src="./screenshot/versioning-v1.JPG" max-height="400" alt="versioning" />
<br />

 v1.1
<br />
<img src="./screenshot/versioning-v11.JPG" max-height="400" alt="versioning" />
 
Jwt bearer authentication
-------------------------
<img src="./screenshot/jwt-bearer-token.JPG" max-height="400" alt="Jwt token" />


Swagger documentation
---------------------
<img src="./screenshot/swagger-method-doc.JPG" max-height="400" alt="Swagger documentation" />


Migration
---------
edit ```var connectionString =""; ``` in ```DbContextFactory.cs ``` and 
go ```.....\Blog\src\Libraries\Blog.Data ``` path
open on CLI and run commands
```sh
#restore Blog.Data project
...Blog.Data_> dotnet restore

#build Blog.Data project
...Blog.Data_> dotnet build

#add Initial name migrations 
...Blog.Data_> dotnet ef migrations add Initial

#Update migrations on database
...Blog.Data_> dotnet ef database update
#dotnet ef database update Initial

```

EF Core Repository
------------------

mapping
<br />
<img src="./screenshot/post-mapping.JPG" max-height="400" alt="mapping" /> 
<br />

DbContext
<br />
<img src="./screenshot/ef-db-context.JPG" max-height="400" alt="db-context" />
<br />

Generic repository
<br />
<img src="./screenshot/repository.JPG" max-height="400" alt="repository" />
<br />

Dependency injection
<br />
<img src="./screenshot/DI-DbContext-coniguration.JPG" max-height="400" alt="DI" />


xUnit Tests
-----------
Service test
<br />
<img src="./screenshot/post-service-test.JPG" max-height="400" alt="xUnit service Tests" />
<br />

Integration test
<br />
<img src="./screenshot/post-integration-test.JPG" max-height="400" alt="xUnit integration Tests" />
<br />

Passed Tests
<br />
<img src="./screenshot/passed-tests.JPG" max-height="400" alt="xUnit integration Tests" />


Serilog Logger
-----------
<img src="./screenshot/serilog-configurion.JPG" max-height="400" alt="serilog" />
