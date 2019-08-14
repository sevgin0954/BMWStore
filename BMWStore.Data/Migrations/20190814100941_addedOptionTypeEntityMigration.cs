using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedOptionTypeEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 14, 10, 9, 40, 392, DateTimeKind.Utc).AddTicks(9190),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 4, 20, 5, 8, 388, DateTimeKind.Utc).AddTicks(1194));

            migrationBuilder.AddColumn<string>(
                name: "OptionTypeId",
                table: "Options",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BaseCars",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OptionTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionTypeId",
                table: "Options",
                column: "OptionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_OptionTypes_OptionTypeId",
                table: "Options",
                column: "OptionTypeId",
                principalTable: "OptionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_OptionTypes_OptionTypeId",
                table: "Options");

            migrationBuilder.DropTable(
                name: "OptionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Options_OptionTypeId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "OptionTypeId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BaseCars");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 4, 20, 5, 8, 388, DateTimeKind.Utc).AddTicks(1194),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 14, 10, 9, 40, 392, DateTimeKind.Utc).AddTicks(9190));
        }
    }
}
