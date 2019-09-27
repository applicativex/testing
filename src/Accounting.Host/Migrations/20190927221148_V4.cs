using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounting.Host.Migrations
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_AccountEntries_FromEntryId",
                table: "AccountTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_AccountEntries_ToEntryId",
                table: "AccountTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "AccountTransactions");

            migrationBuilder.RenameColumn(
                name: "ToEntryId",
                table: "AccountTransactions",
                newName: "DebitEntryId");

            migrationBuilder.RenameColumn(
                name: "FromEntryId",
                table: "AccountTransactions",
                newName: "CreditEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountTransactions_ToEntryId",
                table: "AccountTransactions",
                newName: "IX_AccountTransactions_DebitEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountTransactions_FromEntryId",
                table: "AccountTransactions",
                newName: "IX_AccountTransactions_CreditEntryId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "AccountEntries",
                newName: "EntryDate");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_AccountEntries_CreditEntryId",
                table: "AccountTransactions",
                column: "CreditEntryId",
                principalTable: "AccountEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_AccountEntries_DebitEntryId",
                table: "AccountTransactions",
                column: "DebitEntryId",
                principalTable: "AccountEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_AccountEntries_CreditEntryId",
                table: "AccountTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_AccountEntries_DebitEntryId",
                table: "AccountTransactions");

            migrationBuilder.RenameColumn(
                name: "DebitEntryId",
                table: "AccountTransactions",
                newName: "ToEntryId");

            migrationBuilder.RenameColumn(
                name: "CreditEntryId",
                table: "AccountTransactions",
                newName: "FromEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountTransactions_DebitEntryId",
                table: "AccountTransactions",
                newName: "IX_AccountTransactions_ToEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountTransactions_CreditEntryId",
                table: "AccountTransactions",
                newName: "IX_AccountTransactions_FromEntryId");

            migrationBuilder.RenameColumn(
                name: "EntryDate",
                table: "AccountEntries",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "AccountTransactions",
                nullable: false,
                defaultValue: 0);

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
    }
}
