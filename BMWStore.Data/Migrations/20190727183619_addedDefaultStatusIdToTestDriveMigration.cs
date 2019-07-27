using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedDefaultStatusIdToTestDriveMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "TestDrives",
                nullable: false,
                defaultValue: "d53f130a-9548-4a4d-af08-a98cce96ac6e",
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 27, 18, 36, 18, 162, DateTimeKind.Utc).AddTicks(2215),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 27, 18, 11, 38, 590, DateTimeKind.Utc).AddTicks(6789));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "TestDrives",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "d53f130a-9548-4a4d-af08-a98cce96ac6e");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 27, 18, 11, 38, 590, DateTimeKind.Utc).AddTicks(6789),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 27, 18, 36, 18, 162, DateTimeKind.Utc).AddTicks(2215));
        }
    }
}
