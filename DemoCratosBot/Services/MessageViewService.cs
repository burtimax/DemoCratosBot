using DemoCratosBot.Db;
using DemoCratosBot.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoCratosBot.Services;

public class MessageViewService
{
    private readonly AppDbContext db;
    
    public MessageViewService(AppDbContext db)
    {
        this.db = db;
    }

    public async Task<ChatMessageViewEntity> SaveView(string chatId, long publicMessageEntityId)
    {
        var view = await GetView(chatId, publicMessageEntityId);

        if (view is null)
        {
            var newView = new ChatMessageViewEntity()
            {
                ChatId = chatId,
                PublicMessageId = publicMessageEntityId
            };
            db.MessageViews.Add(newView);
            await db.SaveChangesAsync();
            return newView;
        }
        
        view.CreatedAt = DateTimeOffset.Now;
        db.MessageViews.Update(view);
        await db.SaveChangesAsync();
        return view;
    }

    private async Task<ChatMessageViewEntity?> GetView(string chatId, long publicMessageEntityId)
    {
        return await db.MessageViews.FirstOrDefaultAsync(v =>
            v.ChatId == chatId && v.PublicMessageId == publicMessageEntityId);
    }
}