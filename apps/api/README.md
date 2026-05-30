## Docker

```bash
docker build -f apps/api/Dockerfile -t api .
docker run -p 8080:8080 api
```

## Postgres

### Setup

1. Dependência

```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

2. Adicionar `ConnectionStrings.DefaultConnetion` em `appsettings.json`, e conectar no código:

```cs
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
```

### Migrations

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```
