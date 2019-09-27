using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounting.Host.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_FromEntryId",
                table: "AccountTransactions",
                column: "FromEntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_ToEntryId",
                table: "AccountTransactions",
                column: "ToEntryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_AccountEntries_FromEntryId",
                table: "AccountTransactions",
                column: "FromEntryId",
                principalTable: "AccountEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_AccountEntries_ToEntryId",
                table: "AccountTransactions",
                column: "ToEntryId",
                principalTable: "AccountEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_AccountEntries_FromEntryId",
                table: "AccountTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_AccountEntries_ToEntryId",
                table: "AccountTransactions");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransactions_FromEntryId",
                table: "AccountTransactions");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransactions_ToEntryId",
                table: "AccountTransactions");
        }
    }
}
