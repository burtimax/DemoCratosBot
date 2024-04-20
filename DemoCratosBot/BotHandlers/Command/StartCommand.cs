using BotFramework.Attributes;
using BotFramework.Base;
using Telegram.Bot.Types;

namespace DemoCratosBot.BotHandlers.Command;

[BotCommand(command:"/start", version: 2.0f)]
public class StartCommand : BaseDemoCratosCommand
{
    public StartCommand(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task HandleBotRequest(Update update)
    {
        await Answer("Привет");
    }
}