using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViruBackend.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTypeToPaymentMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "Payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "Payments");
        }
    }
}
