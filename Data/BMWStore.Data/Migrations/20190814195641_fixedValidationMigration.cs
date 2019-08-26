using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class fixedValidationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 14, 19, 56, 39, 874, DateTimeKind.Utc).AddTicks(3556),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 14, 13, 22, 22, 263, DateTimeKind.Utc).AddTicks(9671));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 14, 13, 22, 22, 263, DateTimeKind.Utc).AddTicks(9671),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 14, 19, 56, 39, 874, DateTimeKind.Utc).AddTicks(3556));
        }
    }
}
