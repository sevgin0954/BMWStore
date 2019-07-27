using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedTestDriveStatusMappingEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_TestDriveStatuses_StatusId",
                table: "TestDrives");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_AspNetUsers_UserId",
                table: "TestDrives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestDrives",
                table: "TestDrives");

            migrationBuilder.DropIndex(
                name: "IX_TestDrives_StatusId",
                table: "TestDrives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestDriveStatuses",
                table: "TestDriveStatuses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TestDrives");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "TestDrives");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TestDriveStatuses");

            migrationBuilder.RenameTable(
                name: "TestDriveStatuses",
                newName: "TestDrivesStatuses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 27, 20, 39, 54, 320, DateTimeKind.Utc).AddTicks(2640),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 27, 18, 36, 18, 162, DateTimeKind.Utc).AddTicks(2215));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "TestDrives",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "TestDrivesStatuses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TestDriveId",
                table: "TestDrivesStatuses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TestDrivesStatuses",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestDrives",
                table: "TestDrives",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestDrivesStatuses",
                table: "TestDrivesStatuses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestDrivesStatuses_StatusId",
                table: "TestDrivesStatuses",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDrivesStatuses_TestDriveId",
                table: "TestDrivesStatuses",
                column: "TestDriveId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDrivesStatuses_UserId",
                table: "TestDrivesStatuses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrivesStatuses_Statuses_StatusId",
                table: "TestDrivesStatuses",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrivesStatuses_TestDrives_TestDriveId",
                table: "TestDrivesStatuses",
                column: "TestDriveId",
                principalTable: "TestDrives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrivesStatuses_AspNetUsers_UserId",
                table: "TestDrivesStatuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestDrivesStatuses_Statuses_StatusId",
                table: "TestDrivesStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrivesStatuses_TestDrives_TestDriveId",
                table: "TestDrivesStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrivesStatuses_AspNetUsers_UserId",
                table: "TestDrivesStatuses");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestDrives",
                table: "TestDrives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestDrivesStatuses",
                table: "TestDrivesStatuses");

            migrationBuilder.DropIndex(
                name: "IX_TestDrivesStatuses_StatusId",
                table: "TestDrivesStatuses");

            migrationBuilder.DropIndex(
                name: "IX_TestDrivesStatuses_TestDriveId",
                table: "TestDrivesStatuses");

            migrationBuilder.DropIndex(
                name: "IX_TestDrivesStatuses_UserId",
                table: "TestDrivesStatuses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TestDrives");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "TestDrivesStatuses");

            migrationBuilder.DropColumn(
                name: "TestDriveId",
                table: "TestDrivesStatuses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TestDrivesStatuses");

            migrationBuilder.RenameTable(
                name: "TestDrivesStatuses",
                newName: "TestDriveStatuses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 27, 18, 36, 18, 162, DateTimeKind.Utc).AddTicks(2215),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 27, 20, 39, 54, 320, DateTimeKind.Utc).AddTicks(2640));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TestDrives",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "TestDrives",
                nullable: false,
                defaultValue: "d53f130a-9548-4a4d-af08-a98cce96ac6e");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TestDriveStatuses",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestDrives",
                table: "TestDrives",
                columns: new[] { "UserId", "CarId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestDriveStatuses",
                table: "TestDriveStatuses",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_AspNetUsers_UserId",
                table: "TestDrives",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
