dotnet ef migrations add AddSendedMessagesToUserEntity -o Db/Migrations --context AppDbContext

dotnet ef database update --context AppDbContext