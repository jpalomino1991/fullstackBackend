This backend project is using .net 8 to create api endpoint necesary for the assessment.

The development is using MYSQL as a database. It's necesary to create a database and add the connection string to appSettings.json.

Need to run dotnet restore before running the project.

Migration is located in Infraestructure project just need to run update-database to setup tables and there seed file to generate some data.

Project is using an hexagonal architecture. With CQRS, Repository pattern, fluentvalidation. Also add some basic unit testing to the project.