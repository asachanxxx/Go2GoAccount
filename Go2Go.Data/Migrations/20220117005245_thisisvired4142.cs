using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Go2Go.Data.Migrations
{
    public partial class thisisvired4142 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Salt",
                table: "GUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "GUsers");
        }
    }
}
