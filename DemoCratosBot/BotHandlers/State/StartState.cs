using BotFramework;
using BotFramework.Attributes;
using BotFramework.Db.Entity;
using BotFramework.Enums;
using BotFramework.Extensions;
using BotFramework.Other;
using BotFramework.Services;
using DemoCratosBot.Db.Entities;
using DemoCratosBot.Resources;
using DemoCratosBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DemoCratosBot.BotHandlers.State;

[BotState(stateName:"StartState", version: 2.0f)]
public class StartState : BaseDemoCratosState
{
    private readonly ISavedMessageService _savedMessageService;
    private readonly PublicMessageService _publicMessageService;
    private readonly MessageViewService _messageViewService;
    
    
    public StartState(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _savedMessageService = serviceProvider.GetRequiredService<ISavedMessageService>();
        _publicMessageService = serviceProvider.GetRequiredService<PublicMessageService>();
        _messageViewService = serviceProvider.GetRequiredService<MessageViewService>();
    }

    public override async Task HandleBotRequest(Update update)
    {
        if (update.Type != Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            return;
        }

        if (GetSupportedMessageTypes()
                .Contains(update.Message.Type) == false)
        {
            return;
        }

        if (update.Message.Type == MessageType.Text
            && update.Message.Text == R.ButtonNext)
        {
            // Отправить случайное сообщение.
            await SendRandomSavedMessage();
            return;
        }

        if (update.Message.Type == MessageType.Text)
        {
            await Answer(R.SendMeNoTextMessage);
            return;
        }
        
        // Сохраняем сообщение пользователя
        await SaveMessage(update.Message!);
        await Answer(R.MessageSaved);
        return;
    }

    /// <summary>
    /// Отправить случайное сообщение.
    /// </summary>
    public async Task SendRandomSavedMessage()
    {
        var next = await _publicMessageService.GetNext(Chat.ChatId);

        if (next is null)
        {
            await Answer("Нет еще сообщений");
            return;
        }

        var view = await _messageViewService.SaveView(Chat.ChatId.ToString(), next.Id);
        await BotClient.SendSavedMessage(chatId: Chat.ChatId, BotDbContext, next.SavedMessageId);
    }

    public async Task<PublicMessageEntity> SaveMessage(Message message)
    {
        BotSavedMessage savedMessage = await _savedMessageService.SaveMessageFromUpdate(Chat, User, message);

        var newMes = new PublicMessageEntity()
        {
            Id = savedMessage.Id,
            SavedMessageId = savedMessage.Id,
            Approved = true,
            SenderChatId = Chat.ChatId.ToString(),
        };
        Db.PublicMessages.Add(newMes);
        await Db.SaveChangesAsync();

        await SendModeratorToApprove(newMes);
        
        return newMes;
    }

    private async Task SendModeratorToApprove(PublicMessageEntity mes)
    {
        var approveDeclineInline = new MarkupBuilder<InlineKeyboardMarkup>();
        approveDeclineInline.NewRow()
            .Add(R.ButtonApprove, BotResources.ButtonApproveCallbackKey + mes.SavedMessageId)
            .Add(R.ButtonDecline, BotResources.ButtonDeclineCallbackKey + mes.SavedMessageId);
        
        await BotHelper.ExecuteFor(BotDbContext, BotConstants.BaseBotClaims.BotUserBlock, async tuple =>
        {
            await BotClient.SendSavedMessage(tuple.chat.ChatId, BotDbContext, mes.SavedMessageId);
            await Answer(string.Format(R.NeedApprove, mes.SavedMessageId.ToString()), replyMarkup: approveDeclineInline.Build());
        });
    }
    
    
    private List<MessageType> GetSupportedMessageTypes() => new List<MessageType>()
    {
        MessageType.Animation,
        MessageType.Audio,
        MessageType.Text,
        MessageType.Photo,
        MessageType.Voice,
        MessageType.Sticker,
        MessageType.Video,
        MessageType.VideoNote,
        MessageType.Document,
    };
}