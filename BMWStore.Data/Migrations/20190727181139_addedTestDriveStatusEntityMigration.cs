using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedTestDriveStatusEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 27, 18, 11, 38, 590, DateTimeKind.Utc).AddTicks(6789),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 26, 20, 46, 3, 884, DateTimeKind.Utc).AddTicks(2668));

            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "TestDrives",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TestDriveStatuses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDriveStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestDrives_StatusId",
                table: "TestDrives",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_TestDriveStatuses_StatusId",
                table: "TestDrives",
                column: "StatusId",
                principalTable: "TestDriveStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_TestDriveStatuses_StatusId",
                table: "TestDrives");

            migrationBuilder.DropTable(
                name: "TestDriveStatuses");

            migrationBuilder.DropIndex(
                name: "IX_TestDrives_StatusId",
                table: "TestDrives");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "TestDrives");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 26, 20, 46, 3, 884, DateTimeKind.Utc).AddTicks(2668),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 27, 18, 11, 38, 590, DateTimeKind.Utc).AddTicks(6789));
        }
    }
}
