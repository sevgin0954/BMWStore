using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class removeBaseEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarsOptions",
                table: "CarsOptions");

            migrationBuilder.DropIndex(
                name: "IX_CarsOptions_OptionId",
                table: "CarsOptions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 16, 20, 25, 6, 471, DateTimeKind.Utc).AddTicks(2664),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 14, 19, 56, 39, 874, DateTimeKind.Utc).AddTicks(3556));

            migrationBuilder.AlterColumn<string>(
                name: "OptionId",
                table: "CarsOptions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CarId",
                table: "CarsOptions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "CarsOptions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarsOptions",
                table: "CarsOptions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarsOptions_CarId",
                table: "CarsOptions",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsOptions_OptionId_CarId",
                table: "CarsOptions",
                columns: new[] { "OptionId", "CarId" },
                unique: true,
                filter: "[OptionId] IS NOT NULL AND [CarId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarsOptions",
                table: "CarsOptions");

            migrationBuilder.DropIndex(
                name: "IX_CarsOptions_CarId",
                table: "CarsOptions");

            migrationBuilder.DropIndex(
                name: "IX_CarsOptions_OptionId_CarId",
                table: "CarsOptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CarsOptions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 14, 19, 56, 39, 874, DateTimeKind.Utc).AddTicks(3556),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 16, 20, 25, 6, 471, DateTimeKind.Utc).AddTicks(2664));

            migrationBuilder.AlterColumn<string>(
                name: "OptionId",
                table: "CarsOptions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CarId",
                table: "CarsOptions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarsOptions",
                table: "CarsOptions",
                columns: new[] { "CarId", "OptionId" });

            migrationBuilder.CreateIndex(
                name: "IX_CarsOptions_OptionId",
                table: "CarsOptions",
                column: "OptionId");
        }
    }
}
