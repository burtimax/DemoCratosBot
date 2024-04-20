using BotFramework.Attributes;
using BotFramework.Base;
using DemoCratosBot.Resources;
using DemoCratosBot.Services;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DemoCratosBot.BotHandlers.UpdateType;

[BotPriorityHandler(Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)]
public class CallbackUpdateHandler : BaseBotPriorityHandler
{
    private readonly PublicMessageService _publicMessageService;
    protected readonly BotResources R;
    
    public CallbackUpdateHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _publicMessageService = serviceProvider.GetRequiredService<PublicMessageService>();
        R = serviceProvider.GetRequiredService<IOptions<BotResources>>().Value;
    }

    public override async Task HandleBotRequest(Update update)
    {
        if (update.Type != Telegram.Bot.Types.Enums.UpdateType.CallbackQuery) return;

        var callback = update.CallbackQuery;

        if (callback.Data.StartsWith(BotResources.ButtonApproveCallbackKey))
        {
            long savedMesId = long.Parse(callback.Data.Replace(BotResources.ButtonApproveCallbackKey, ""));
            await _publicMessageService.ApproveDeclinePublicMessage(savedMesId, true); // одобрить
            await BotClient.AnswerCallbackQueryAsync(callback.Id, string.Format(R.MessageApprovedByModerator, savedMesId.ToString()));
            return;
        }
        
        if (callback.Data.StartsWith(BotResources.ButtonDeclineCallbackKey))
        {
            long savedMesId = long.Parse(callback.Data.Replace(BotResources.ButtonDeclineCallbackKey, ""));
            await _publicMessageService.ApproveDeclinePublicMessage(savedMesId, false); // отклонить 
            await BotClient.AnswerCallbackQueryAsync(callback.Id, string.Format(R.MessageDeclinedByModerator, savedMesId.ToString()));
            return;
        }

        
    }
}