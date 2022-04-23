# ProjectAthenaBackend


Everytime you update the database consider using the following instructions


```
	Add-Migration migrationName -Project "AthenaBackend.Infrastructure" -Context ReadDbContext
	Add-Migration migrationName -Project "AthenaBackend.Infrastructure" -Context WriteDbContext
```