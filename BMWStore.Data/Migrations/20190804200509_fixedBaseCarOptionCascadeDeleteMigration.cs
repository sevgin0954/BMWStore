using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class fixedBaseCarOptionCascadeDeleteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsOptions_BaseCars_CarId",
                table: "CarsOptions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 4, 20, 5, 8, 388, DateTimeKind.Utc).AddTicks(1194),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 3, 15, 52, 28, 945, DateTimeKind.Utc).AddTicks(1105));

            migrationBuilder.AddForeignKey(
                name: "FK_CarsOptions_BaseCars_CarId",
                table: "CarsOptions",
                column: "CarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsOptions_BaseCars_CarId",
                table: "CarsOptions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 3, 15, 52, 28, 945, DateTimeKind.Utc).AddTicks(1105),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 4, 20, 5, 8, 388, DateTimeKind.Utc).AddTicks(1194));

            migrationBuilder.AddForeignKey(
                name: "FK_CarsOptions_BaseCars_CarId",
                table: "CarsOptions",
                column: "CarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
