using BotFramework.Attributes;
using BotFramework.Base;
using BotFramework.Extensions;
using BotFramework.Other;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace DemoCratosBot.BotHandlers.Command;

[BotCommand(command:"/start", version: 2.0f)]
public class StartCommand : BaseDemoCratosCommand
{
    public StartCommand(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task HandleBotRequest(Update update)
    {
        var savedMessage = await BotDbContext.SavedMessages.FirstOrDefaultAsync(m => m.Id == R.IntroductionSavedMessageId);

        if (savedMessage is null)
        {
            await Answer(R.Introduction);
            return;
        }
        
        MarkupBuilder<ReplyKeyboardMarkup> reply = new();
        reply.NewRow().Add(R.ButtonNext);
        await BotClient.SendSavedMessage(Chat.ChatId, BotDbContext, R.IntroductionSavedMessageId, replyMarkup: reply.Build());
    }
}