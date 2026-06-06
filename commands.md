# Commands required during the test

## Entity Framework Core

### solution/project compilation
```console
dotnet build
```

### solution/project clean
```console
dotnet clean
```

### solution/project restore
```console
dotnet restore
```

### update Entity Framework Tools to given version
```console
dotnet tool update --global dotnet-ef --version 8.0.2
```

### create migration with name Initial
```console
dotnet ef migrations add Initial --project Kolokwium.DAL --startup-project Kolokwium.Web
```

### remove the newest migration
```console
dotnet ef migrations remove --project Kolokwium.DAL --startup-project Kolokwium.Web
```

### update database to newest migration
```console
dotnet ef database update --project Kolokwium.DAL --startup-project Kolokwium.Web
```

### drop database
```console
dotnet ef database drop --project Kolokwium.DAL --startup-project Kolokwium.Web
```

## Scaffolder

### update ASP.NET Code Generator to given version
```console
dotnet tool update -g dotnet-aspnet-codegenerator --version 8.0.2
```

### Scaffold View 
```console
dotnet aspnet-codegenerator view -p Kolokwium.Web Index List -m SubjectVm -outDir Views/Subject -scripts -udl
```