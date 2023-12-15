using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MellonBank.Migrations
{
    /// <inheritdoc />
    public partial class account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "MellonnBankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagementBranch",
                table: "MellonnBankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "MellonnBankAccounts");

            migrationBuilder.DropColumn(
                name: "ManagementBranch",
                table: "MellonnBankAccounts");
        }
    }
}
