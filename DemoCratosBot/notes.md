dotnet ef migrations add ChangePropName -o Db/Migrations --context AppDbContext

dotnet ef database update --context AppDbContext