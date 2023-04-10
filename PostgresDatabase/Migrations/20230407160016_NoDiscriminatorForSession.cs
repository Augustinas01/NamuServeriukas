using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostgresDatabase.Migrations
{
    /// <inheritdoc />
    public partial class NoDiscriminatorForSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_player_service_session_session_id",
                schema: "main",
                table: "player");

            migrationBuilder.DropColumn(
                name: "type",
                schema: "main",
                table: "session");

            migrationBuilder.AddForeignKey(
                name: "fk_player_sessions_session_id",
                schema: "main",
                table: "player",
                column: "session_id",
                principalSchema: "main",
                principalTable: "session",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_player_sessions_session_id",
                schema: "main",
                table: "player");

            migrationBuilder.AddColumn<string>(
                name: "type",
                schema: "main",
                table: "session",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "fk_player_service_session_session_id",
                schema: "main",
                table: "player",
                column: "session_id",
                principalSchema: "main",
                principalTable: "session",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
