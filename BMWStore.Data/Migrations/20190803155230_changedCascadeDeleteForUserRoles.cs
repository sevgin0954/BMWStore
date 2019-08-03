using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class changedCascadeDeleteForUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 3, 15, 52, 28, 945, DateTimeKind.Utc).AddTicks(1105),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 1, 11, 33, 41, 120, DateTimeKind.Utc).AddTicks(3364));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 1, 11, 33, 41, 120, DateTimeKind.Utc).AddTicks(3364),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 3, 15, 52, 28, 945, DateTimeKind.Utc).AddTicks(1105));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
