using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounting.Host.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountEntries_AccountTransactions_AccountTransactionId",
                table: "AccountEntries");

            migrationBuilder.DropIndex(
                name: "IX_AccountEntries_AccountTransactionId",
                table: "AccountEntries");

            migrationBuilder.DropColumn(
                name: "AccountTransactionId",
                table: "AccountEntries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountTransactionId",
                table: "AccountEntries",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AccountEntries_AccountTransactionId",
                table: "AccountEntries",
                column: "AccountTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountEntries_AccountTransactions_AccountTransactionId",
                table: "AccountEntries",
                column: "AccountTransactionId",
                principalTable: "AccountTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
