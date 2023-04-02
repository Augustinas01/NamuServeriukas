using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PostgresDatabase.Migrations
{
    /// <inheritdoc />
    public partial class InitialTest : Migration
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    StartTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StopTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("session_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "player",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JoinTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LeaveTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SessionId = table.Column<int>(type: "integer", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("player_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_player_session_SessionId",
                        column: x => x.SessionId,
                        principalSchema: "main",
                        principalTable: "session",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_player_SessionId",
                schema: "main",
                table: "player",
                column: "SessionId");
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
