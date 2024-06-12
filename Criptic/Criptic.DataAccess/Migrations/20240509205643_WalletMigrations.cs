using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Criptic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class WalletMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sum",
                table: "Wallets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sum",
                table: "Wallets");
        }
    }
}
