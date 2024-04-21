﻿// <auto-generated />
using System;
using DemoCratosBot.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DemoCratosBot.Db.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240420160120_AddPropsToPublicMessage")]
    partial class AddPropsToPublicMessage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DemoCratosBot.Db.Entities.ChatMessageViewEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasComment("Идентификатор сущности.");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ChatId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("chat_id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<long?>("PublicMessageEntityId")
                        .HasColumnType("bigint")
                        .HasColumnName("public_message_entity_id");

                    b.Property<long>("PublicMessageId")
                        .HasColumnType("bigint")
                        .HasColumnName("public_message_id");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_chat_message_views");

                    b.HasIndex("PublicMessageEntityId")
                        .HasDatabaseName("ix_chat_message_views_public_message_entity_id");

                    b.ToTable("chat_message_views", "app");
                });

            modelBuilder.Entity("DemoCratosBot.Db.Entities.PublicMessageEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasComment("Идентификатор сущности.");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Approved")
                        .HasColumnType("boolean")
                        .HasColumnName("approved");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<long>("SavedMessageId")
                        .HasColumnType("bigint")
                        .HasColumnName("saved_message_id");

                    b.Property<string>("SenderChatId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("sender_chat_id");

                    b.Property<int?>("TelegramMessageId")
                        .HasColumnType("integer")
                        .HasColumnName("telegram_message_id");

                    b.Property<int?>("TelegramMessageThreadId")
                        .HasColumnType("integer")
                        .HasColumnName("telegram_message_thread_id");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_public_messages");

                    b.ToTable("public_messages", "app");
                });

            modelBuilder.Entity("DemoCratosBot.Db.Entities.ChatMessageViewEntity", b =>
                {
                    b.HasOne("DemoCratosBot.Db.Entities.PublicMessageEntity", "PublicMessageEntity")
                        .WithMany("Views")
                        .HasForeignKey("PublicMessageEntityId")
                        .HasConstraintName("fk_chat_message_views_public_messages_public_message_entity_id");

                    b.Navigation("PublicMessageEntity");
                });

            modelBuilder.Entity("DemoCratosBot.Db.Entities.PublicMessageEntity", b =>
                {
                    b.Navigation("Views");
                });
#pragma warning restore 612, 618
        }
    }
}
