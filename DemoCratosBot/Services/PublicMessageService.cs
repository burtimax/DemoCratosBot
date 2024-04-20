using DemoCratosBot.Db;
using DemoCratosBot.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Telegram.Bot.Types;

namespace DemoCratosBot.Services;

public class PublicMessageService
{
    private readonly AppDbContext db;
    
    public PublicMessageService(AppDbContext db)
    {
        this.db = db;
    }

    public async Task<PublicMessageEntity?> GetNext(ChatId chatId)
    {
        string chatIdParam = chatId.ToString();
        
        var publicMes = (await db.PublicMessages.FromSqlRaw(@$"
            select m.* from app.public_messages m
            left join app.chat_message_views v 
	            on m.id = v.public_message_id and v.chat_id = '{chatIdParam}'
            where m.sender_chat_id != '{chatIdParam}'
                and m.deleted_at is null 
	            and v.deleted_at is null
	            and m.approved = true
            order by v.created_at asc nulls first, m.created_at asc
            limit 1
        ").ToListAsync()).FirstOrDefault();
            
        return publicMes;
    }

    public async Task ApproveDeclinePublicMessage(long savedMessageId, bool approved)
    {
        var messages = await db.PublicMessages
                .Where(m => m.SavedMessageId == savedMessageId)
                .ToListAsync();

        if(messages is null || messages.Any() == false) return;
        
        foreach (var message in messages)
        {
            message.Approved = approved;
        }

        db.PublicMessages.UpdateRange(messages);
        await db.SaveChangesAsync();
    }
}