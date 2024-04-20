using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoCratosBot.Db.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_chat_message_views_public_messages_public_message_entity_id",
                schema: "app",
                table: "chat_message_views");

            migrationBuilder.AlterColumn<long>(
                name: "public_message_entity_id",
                schema: "app",
                table: "chat_message_views",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "public_message_id",
                schema: "app",
                table: "chat_message_views",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "fk_chat_message_views_public_messages_public_message_entity_id",
                schema: "app",
                table: "chat_message_views",
                column: "public_message_entity_id",
                principalSchema: "app",
                principalTable: "public_messages",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_chat_message_views_public_messages_public_message_entity_id",
                schema: "app",
                table: "chat_message_views");

            migrationBuilder.DropColumn(
                name: "public_message_id",
                schema: "app",
                table: "chat_message_views");

            migrationBuilder.AlterColumn<long>(
                name: "public_message_entity_id",
                schema: "app",
                table: "chat_message_views",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
    }
}
