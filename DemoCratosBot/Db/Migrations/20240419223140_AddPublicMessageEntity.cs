using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DemoCratosBot.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddPublicMessageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "saved_message_id",
                schema: "app",
                table: "chat_message_views",
                newName: "public_message_entity_id");

            migrationBuilder.AlterColumn<string>(
                name: "chat_id",
                schema: "app",
                table: "chat_message_views",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "public_messages",
                schema: "app",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Идентификатор сущности.")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    approved = table.Column<bool>(type: "boolean", nullable: false),
                    sender_chat_id = table.Column<string>(type: "text", nullable: false),
                    saved_message_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_public_messages", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_chat_message_views_public_message_entity_id",
                schema: "app",
                table: "chat_message_views",
                column: "public_message_entity_id");

            migrationBuilder.AddForeignKey(
                name: "fk_chat_message_views_public_messages_public_message_entity_id",
                schema: "app",
                table: "chat_message_views",
                column: "public_message_entity_id",
                principalSchema: "app",
                principalTable: "public_messages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_chat_message_views_public_messages_public_message_entity_id",
                schema: "app",
                table: "chat_message_views");

            migrationBuilder.DropTable(
                name: "public_messages",
                schema: "app");

            migrationBuilder.DropIndex(
                name: "ix_chat_message_views_public_message_entity_id",
                schema: "app",
                table: "chat_message_views");

            migrationBuilder.RenameColumn(
                name: "public_message_entity_id",
                schema: "app",
                table: "chat_message_views",
                newName: "saved_message_id");

            migrationBuilder.AlterColumn<long>(
                name: "chat_id",
                schema: "app",
                table: "chat_message_views",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
