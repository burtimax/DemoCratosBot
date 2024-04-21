using System.ComponentModel.DataAnnotations.Schema;
using BotFramework.Db.Entity;

namespace DemoCratosBot.Db.Entities;

public class SendedMessagesToUserEntity : BaseBotEntity<long>
{
    [Column("public_message_id")]
    public long PublicMessageId { get; set; }
    
    [Column("telegram_message_id")]
    public int? TelegramMessageId { get; set; }
    
    [Column("telegram_message_thread_id")]
    public int? TelegramMessageThreadId { get; set; }
}