using BotFramework.Db.Entity;
using BotFramework.Extensions;
using DemoCratosBot.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoCratosBot.Db;

public class AppDbContext : DbContext
{
    private const string AppSchema = "app";
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ChatMessageViewEntity> MessageViews { get; set; }
    public DbSet<PublicMessageEntity> PublicMessages { get; set; }
    public DbSet<SendedMessagesToUserEntity> SendedMessagesToUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatMessageViewEntity>().ToTable("chat_message_views", AppSchema);
        modelBuilder.Entity<PublicMessageEntity>().ToTable("public_messages", AppSchema);
        modelBuilder.Entity<SendedMessagesToUserEntity>().ToTable("sended_message_to_user", AppSchema);
        
        modelBuilder.SetAllToSnakeCase();
        modelBuilder.SetFilters();
        
    }

    private void ConfigureEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PublicMessageEntity>()
            .HasAlternateKey(c => c.SavedMessageId).HasName("ix_unique_saved_message_id");
        
        modelBuilder.Entity<ChatMessageViewEntity>().HasAlternateKey(c => new { PublicMessageEntityId = c.PublicMessageId, c.ChatId}).HasName("IX_MultipleColumns");
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        foreach (var e in
                 ChangeTracker.Entries<BaseBotEntity<long>>())
        {
            switch (e.State)
            {
                case EntityState.Added:
                    e.Entity.CreatedAt = DateTimeOffset.Now;
                    break;
                case EntityState.Modified:
                    e.Entity.UpdatedAt = DateTimeOffset.Now;
                    break;
                case EntityState.Deleted:
                    e.Entity.DeletedAt = DateTimeOffset.Now;
                    e.State = EntityState.Modified;
                    break;
            }
        }

        return base.SaveChangesAsync(ct);
    }
}