using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Go2Go.Data.Migrations
{
    public partial class thisisvired2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "GUsers",
                type: "binary(64)",
                fixedLength: true,
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "GUsers",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(64)",
                oldFixedLength: true,
                oldMaxLength: 64,
                oldNullable: true);
        }
    }
}
