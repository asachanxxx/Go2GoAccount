using Microsoft.EntityFrameworkCore.Migrations;

namespace Go2Go.Data.Migrations
{
    public partial class changeUserLedger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flag",
                table: "UserLedgers");

            migrationBuilder.AddColumn<string>(
                name: "TrxId",
                table: "UserLedgers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrxId",
                table: "UserLedgers");

            migrationBuilder.AddColumn<int>(
                name: "Flag",
                table: "UserLedgers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
