using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PostgresDatabase.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "session",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false),
                    start_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    stop_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("session_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "player",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    join_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    leave_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    session_id = table.Column<int>(type: "integer", nullable: true),
                    discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("player_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_player_game_session_session_id",
                        column: x => x.session_id,
                        principalSchema: "main",
                        principalTable: "session",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_player_session_id",
                schema: "main",
                table: "player",
                column: "session_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "player",
                schema: "main");

            migrationBuilder.DropTable(
                name: "session",
                schema: "main");
        }
    }
}
