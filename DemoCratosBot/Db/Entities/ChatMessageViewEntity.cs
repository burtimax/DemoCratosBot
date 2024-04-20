using System.ComponentModel.DataAnnotations.Schema;
using BotFramework.Db.Entity;

namespace DemoCratosBot.Db.Entities;

public class ChatMessageViewEntity : BaseBotEntity<long>
{
    /// <summary>
    /// ИД чата.
    /// </summary>
    [Column("chat_id")]
    public string ChatId { get; set; }
    
    /// <summary>
    /// ИД сохраненного сообщения.
    /// </summary>
    [Column("public_message_id")]
    public long PublicMessageId { get; set; }
    public PublicMessageEntity? PublicMessageEntity { get; set; }
}