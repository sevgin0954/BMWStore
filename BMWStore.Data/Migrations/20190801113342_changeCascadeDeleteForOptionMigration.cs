using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class changeCascadeDeleteForOptionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsOptions_Options_OptionId",
                table: "CarsOptions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 1, 11, 33, 41, 120, DateTimeKind.Utc).AddTicks(3364),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 31, 21, 41, 0, 75, DateTimeKind.Utc).AddTicks(2793));

            migrationBuilder.AddForeignKey(
                name: "FK_CarsOptions_Options_OptionId",
                table: "CarsOptions",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsOptions_Options_OptionId",
                table: "CarsOptions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 31, 21, 41, 0, 75, DateTimeKind.Utc).AddTicks(2793),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 1, 11, 33, 41, 120, DateTimeKind.Utc).AddTicks(3364));

            migrationBuilder.AddForeignKey(
                name: "FK_CarsOptions_Options_OptionId",
                table: "CarsOptions",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
