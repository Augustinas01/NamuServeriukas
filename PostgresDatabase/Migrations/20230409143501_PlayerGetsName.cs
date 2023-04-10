using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostgresDatabase.Migrations
{
    /// <inheritdoc />
    public partial class PlayerGetsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "discriminator",
                schema: "main",
                table: "player",
                newName: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                schema: "main",
                table: "player",
                newName: "discriminator");
        }
    }
}
