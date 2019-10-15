using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounting.Host.Migrations
{
    public partial class V7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { new Guid("6d452541-7c1d-4ba9-b480-d21ac3b30b83"), "system@gmail.com", "SYSTEM", "SYSTEM" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Currency", "UserId" },
                values: new object[,]
                {
                    { new Guid("eb7ab20e-3130-47fc-a6a7-dbcd30a60a53"), 1, new Guid("6d452541-7c1d-4ba9-b480-d21ac3b30b83") },
                    { new Guid("b4db2e93-4b3b-4172-ba2d-568b7674b1d5"), 2, new Guid("6d452541-7c1d-4ba9-b480-d21ac3b30b83") },
                    { new Guid("4eb3ae16-18ce-4fd9-a0e3-ccc1b87bfc11"), 3, new Guid("6d452541-7c1d-4ba9-b480-d21ac3b30b83") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("4eb3ae16-18ce-4fd9-a0e3-ccc1b87bfc11"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("b4db2e93-4b3b-4172-ba2d-568b7674b1d5"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("eb7ab20e-3130-47fc-a6a7-dbcd30a60a53"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6d452541-7c1d-4ba9-b480-d21ac3b30b83"));
        }
    }
}
