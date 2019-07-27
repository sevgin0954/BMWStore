using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedRequiredConstraintToTestDriveStatusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestDrivesStatuses_AspNetUsers_UserId",
                table: "TestDrivesStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TestDrivesStatuses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 27, 20, 43, 31, 651, DateTimeKind.Utc).AddTicks(1154),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 27, 20, 39, 54, 320, DateTimeKind.Utc).AddTicks(2640));

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrivesStatuses_AspNetUsers_UserId",
                table: "TestDrivesStatuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestDrivesStatuses_AspNetUsers_UserId",
                table: "TestDrivesStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TestDrivesStatuses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 27, 20, 39, 54, 320, DateTimeKind.Utc).AddTicks(2640),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 27, 20, 43, 31, 651, DateTimeKind.Utc).AddTicks(1154));

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrivesStatuses_AspNetUsers_UserId",
                table: "TestDrivesStatuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
