# ProjectAthenaBackend


Everytime you update the database consider using the following instructions


```
	Add-Migration migrationName -Project "AthenaBackend.Infrastructure" -Context ReadDbContext
	Add-Migration migrationName -Project "AthenaBackend.Infrastructure" -Context WriteDbContext
```

In order to create migrations with code-first method you need to add two kinds of construtors to the DbContext, a pratical example is used as follow


```
	// just add this two constructors to addMigrations
	public WriteDbContext()
        {
        }
        
	public WriteDbContext(DbContextOptions options) : base(options)
        {
        }
```

