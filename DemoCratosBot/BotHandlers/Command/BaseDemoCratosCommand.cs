using BotFramework.Base;
using BotFramework.Enums;
using BotFramework.Other;
using DemoCratosBot.Resources;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DemoCratosBot.BotHandlers.Command;

public class BaseDemoCratosCommand : BaseBotCommand
{
    protected IServiceProvider ServiceProvider;
    protected readonly BotResources R;

    public BaseDemoCratosCommand(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
        R = serviceProvider.GetRequiredService<IOptions<BotResources>>().Value;
    }
    

    protected Task Answer(string text, ParseMode parseMode = ParseMode.Html, IReplyMarkup replyMarkup = default)
    {
        if (replyMarkup == default)
        {
            MarkupBuilder<ReplyKeyboardMarkup> reply = new();
            reply.NewRow().Add(R.ButtonNext);
            replyMarkup = reply.Build();
        }
        
        return BotClient.SendTextMessageAsync(Chat.ChatId, text, parseMode: parseMode, replyMarkup: replyMarkup);
    }
    
    public async Task ChangeState(string stateName, ChatStateSetterType setterType = ChatStateSetterType.ChangeCurrent)
    {
        Chat.States.Set(stateName, setterType);
        await BotDbContext.SaveChangesAsync();
    }

    public override Task HandleBotRequest(Update update)
    {
        throw new NotImplementedException();
    }
}