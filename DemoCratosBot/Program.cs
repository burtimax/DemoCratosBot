using BotFramework.Extensions;
using BotFramework.Options;
using DemoCratosBot.Db;
using DemoCratosBot.Extensions;
using DemoCratosBot.Resources;
using DemoCratosBot.Services;
using Microsoft.EntityFrameworkCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;
// Регистрируем конфигурации.
services.Configure<BotConfiguration>(builder.Configuration.GetSection("Bot"));
services.Configure<BotOptions>(builder.Configuration.GetSection("BotOptions"));
var botConfig = builder.Configuration.GetSection("Bot").Get<BotConfiguration>();
BotResources botResources = services.ConfigureBotResources(botConfig.ResourcesFilePath);
services.AddBot(botConfig); // Подключаем бота
services.AddControllers().AddNewtonsoftJson(); //Обязательно подключаем NewtonsoftJson
services.AddHttpContextAccessor();
services.AddCors();

// Свои сервисы
services.AddScoped<PublicMessageService>();
services.AddScoped<MessageViewService>();

// Регистрируем контексты к базам данных.

services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(botConfig.DbConnection);
});

var app = builder.Build();

// Миграция контекста приложения.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
//app.UseHttpsRedirection();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();