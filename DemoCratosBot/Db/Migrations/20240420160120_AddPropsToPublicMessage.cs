using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoCratosBot.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddPropsToPublicMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "telegram_message_id",
                schema: "app",
                table: "public_messages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "telegram_message_thread_id",
                schema: "app",
                table: "public_messages",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "telegram_message_id",
                schema: "app",
                table: "public_messages");

            migrationBuilder.DropColumn(
                name: "telegram_message_thread_id",
                schema: "app",
                table: "public_messages");
        }
    }
}
