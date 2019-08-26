using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedIndexesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 14, 13, 22, 22, 263, DateTimeKind.Utc).AddTicks(9671),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 14, 10, 9, 40, 392, DateTimeKind.Utc).AddTicks(9190));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OptionTypes",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transmissions_Name",
                table: "Transmissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Name",
                table: "Statuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OptionTypes_Name",
                table: "OptionTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_Name",
                table: "Options",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transmissions_Name",
                table: "Transmissions");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_Name",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_OptionTypes_Name",
                table: "OptionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Options_Name",
                table: "Options");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 14, 10, 9, 40, 392, DateTimeKind.Utc).AddTicks(9190),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 14, 13, 22, 22, 263, DateTimeKind.Utc).AddTicks(9671));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OptionTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
