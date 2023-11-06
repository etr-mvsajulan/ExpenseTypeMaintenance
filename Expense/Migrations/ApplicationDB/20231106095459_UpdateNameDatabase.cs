using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense.Migrations.ApplicationDB
{
    /// <inheritdoc />
    public partial class UpdateNameDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_AspNetRoles_RoleId",
                schema: "dbo",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_AspNetRoles_RoleId",
                schema: "dbo",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                schema: "dbo",
                table: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "dbo",
                newName: "Role",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                schema: "dbo",
                table: "Role",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Role_RoleId",
                schema: "dbo",
                table: "RoleClaims",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "dbo",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Role_RoleId",
                schema: "dbo",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "dbo",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                schema: "dbo",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "dbo",
                newName: "AspNetRoles",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                schema: "dbo",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_AspNetRoles_RoleId",
                schema: "dbo",
                table: "RoleClaims",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_AspNetRoles_RoleId",
                schema: "dbo",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
