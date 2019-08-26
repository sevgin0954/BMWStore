using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedCascadeDeleteOnBaseCarTestDriveMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 20, 17, 46, 44, 627, DateTimeKind.Utc).AddTicks(1094),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 17, 18, 1, 55, 471, DateTimeKind.Utc).AddTicks(8846));

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives",
                column: "CarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 17, 18, 1, 55, 471, DateTimeKind.Utc).AddTicks(8846),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 20, 17, 46, 44, 627, DateTimeKind.Utc).AddTicks(1094));

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives",
                column: "CarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
