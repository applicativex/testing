using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounting.Host.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountEntries_AccountTransactionId",
                table: "AccountEntries");

            migrationBuilder.CreateIndex(
                name: "IX_AccountEntries_AccountTransactionId",
                table: "AccountEntries",
                column: "AccountTransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountEntries_AccountTransactionId",
                table: "AccountEntries");

            migrationBuilder.CreateIndex(
                name: "IX_AccountEntries_AccountTransactionId",
                table: "AccountEntries",
                column: "AccountTransactionId",
                unique: true);
        }
    }
}
