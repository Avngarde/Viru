using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViruBackend.Migrations
{
    /// <inheritdoc />
    public partial class ColorBalanceMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Wallets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalBalance",
                table: "Wallets",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "Payments",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "TotalBalance",
                table: "Wallets");

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Payments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
