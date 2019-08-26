using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class changeValidationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 17, 18, 1, 55, 471, DateTimeKind.Utc).AddTicks(8846),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 16, 20, 25, 6, 471, DateTimeKind.Utc).AddTicks(2664));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Options",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 16, 20, 25, 6, 471, DateTimeKind.Utc).AddTicks(2664),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 17, 18, 1, 55, 471, DateTimeKind.Utc).AddTicks(8846));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Options",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
