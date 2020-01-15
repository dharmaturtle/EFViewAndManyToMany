dotnet ef dbcontext scaffold "Server=localhost;Database=EFViewAndManyToMany;User Id=localsa;" Microsoft.EntityFrameworkCore.SqlServer --context EFViewAndManyToManyDb --force --use-database-names

mssql-scripter --connection-string "Server=localhost;Database=EFViewAndManyToMany;User Id=localsa;" --schema-and-data --file-path ./InitializeDatabase.sql