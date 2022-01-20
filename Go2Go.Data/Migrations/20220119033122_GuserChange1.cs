using Microsoft.EntityFrameworkCore.Migrations;

namespace Go2Go.Data.Migrations
{
    public partial class GuserChange1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "GUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FKey",
                table: "GUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "GUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "GUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "GUsers");

            migrationBuilder.DropColumn(
                name: "FKey",
                table: "GUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "GUsers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "GUsers");
        }
    }
}
