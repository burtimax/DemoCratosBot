using System.ComponentModel.DataAnnotations.Schema;
using BotFramework.Db.Entity;
using Telegram.Bot.Types;

namespace DemoCratosBot.Db.Entities;

/// <summary>
/// Сообщение от народа, для показа.
/// </summary>
public class PublicMessageEntity : BaseBotEntity<long>
{
    [Column("approved")]
    public bool Approved { get; set; }
    
    [Column("sender_chat_id")]
    public string SenderChatId { get; set; }

    [Column("telegram_message_id")]
    public int? TelegramMessageId { get; set; }
    
    [Column("telegram_message_thread_id")]
    public int? TelegramMessageThreadId { get; set; }
    
    [Column("saved_message_id")]
    public long SavedMessageId { get; set; }
    
    public List<ChatMessageViewEntity> Views { get; set; }
}