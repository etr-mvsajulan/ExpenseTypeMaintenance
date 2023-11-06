using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense.Migrations.ApplicationDB
{
    /// <inheritdoc />
    public partial class AddColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CostUnitCode",
                schema: "dbo",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CostUnitName",
                schema: "dbo",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostUnitCode",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CostUnitName",
                schema: "dbo",
                table: "User");
        }
    }
}
