using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostgresDatabase.Migrations
{
    /// <inheritdoc />
    public partial class relationsAndConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "fk_player_game_session_session_id",
            //    schema: "main",
            //    table: "player");

            migrationBuilder.AddColumn<int>(
                name: "service_id",
                schema: "main",
                table: "session",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "fk_session_service_service_id",
                schema: "main",
                table: "session",
                column: "service_id",
                principalSchema: "config",
                principalTable: "service",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AlterColumn<string>(
                name: "exe_args",
                schema: "config",
                table: "service",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "config",
                table: "service",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "session_id",
                schema: "main",
                table: "player",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_session_service_id",
                schema: "main",
                table: "session",
                column: "service_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_player_service_session_session_id",
                schema: "main",
                table: "player");

            migrationBuilder.DropForeignKey(
                name: "fk_session_service_service_id",
                schema: "main",
                table: "session");

            migrationBuilder.DropIndex(
                name: "ix_session_service_id",
                schema: "main",
                table: "session");

            migrationBuilder.DropColumn(
                name: "service_id",
                schema: "main",
                table: "session");

            migrationBuilder.AlterColumn<string>(
                name: "exe_args",
                schema: "config",
                table: "service",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "config",
                table: "service",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "session_id",
                schema: "main",
                table: "player",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_player_game_session_session_id",
                schema: "main",
                table: "player",
                column: "session_id",
                principalSchema: "main",
                principalTable: "session",
                principalColumn: "id");
        }
    }
}
