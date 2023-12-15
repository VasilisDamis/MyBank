using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MellonBank.Migrations
{
    /// <inheritdoc />
    public partial class datachange1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MellonnBankAccounts_AspNetUsers_AFM",
                table: "MellonnBankAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "AFM",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_AFM",
                table: "AspNetUsers",
                column: "AFM");

            migrationBuilder.AddForeignKey(
                name: "FK_MellonnBankAccounts_AspNetUsers_AFM",
                table: "MellonnBankAccounts",
                column: "AFM",
                principalTable: "AspNetUsers",
                principalColumn: "AFM",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MellonnBankAccounts_AspNetUsers_AFM",
                table: "MellonnBankAccounts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_AFM",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AFM",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_MellonnBankAccounts_AspNetUsers_AFM",
                table: "MellonnBankAccounts",
                column: "AFM",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
