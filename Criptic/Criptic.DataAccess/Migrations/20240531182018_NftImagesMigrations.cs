using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Criptic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NftImagesMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Nfts",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Nfts");
        }
    }
}
