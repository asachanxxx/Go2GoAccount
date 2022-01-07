using Microsoft.EntityFrameworkCore.Migrations;

namespace Go2Go.Data.Migrations
{
    public partial class LedgerUser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "UserLedgers");

            migrationBuilder.RenameColumn(
                name: "Debit",
                table: "UserLedgers",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "UserLedgers",
                newName: "TrxDate");

            migrationBuilder.AddColumn<int>(
                name: "Flag",
                table: "UserLedgers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flag",
                table: "UserLedgers");

            migrationBuilder.RenameColumn(
                name: "TrxDate",
                table: "UserLedgers",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "UserLedgers",
                newName: "Debit");

            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                table: "UserLedgers",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
