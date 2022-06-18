using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHeroApi.Migrations
{
    public partial class SeedsLevel3Admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4f9c41ec-780e-4a18-8de1-4f3d5b23ef31", "48193b13-0d22-4a68-9266-bfd617637c6d", "Level3Admin", "LEVEL3ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f9c41ec-780e-4a18-8de1-4f3d5b23ef31");
        }
    }
}
