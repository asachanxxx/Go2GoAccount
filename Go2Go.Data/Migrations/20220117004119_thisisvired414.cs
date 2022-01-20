﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Go2Go.Data.Migrations
{
    public partial class thisisvired414 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "GRoles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GUsers_LoginName",
                table: "GUsers",
                column: "LoginName",
                unique: true,
                filter: "[LoginName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GRoles_RoleName",
                table: "GRoles",
                column: "RoleName",
                unique: true,
                filter: "[RoleName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GUsers_LoginName",
                table: "GUsers");

            migrationBuilder.DropIndex(
                name: "IX_GRoles_RoleName",
                table: "GRoles");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "GRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
