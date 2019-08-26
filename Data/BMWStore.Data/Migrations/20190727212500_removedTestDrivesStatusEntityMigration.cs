using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class removedTestDrivesStatusEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestDrivesStatuses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 27, 21, 24, 59, 177, DateTimeKind.Utc).AddTicks(6578),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 27, 20, 43, 31, 651, DateTimeKind.Utc).AddTicks(1154));

            migrationBuilder.AddColumn<string>(
                name: "StatusId",
                table: "TestDrives",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TestDrives",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TestDrives_StatusId",
                table: "TestDrives",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TestDrives_UserId",
                table: "TestDrives",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_Statuses_StatusId",
                table: "TestDrives",
                column: "StatusId",
                principalTable: "Statuses",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_Statuses_StatusId",
                table: "TestDrives");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_AspNetUsers_UserId",
                table: "TestDrives");

            migrationBuilder.DropIndex(
                name: "IX_TestDrives_StatusId",
                table: "TestDrives");

            migrationBuilder.DropIndex(
                name: "IX_TestDrives_UserId",
                table: "TestDrives");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "TestDrives");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TestDrives");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 27, 20, 43, 31, 651, DateTimeKind.Utc).AddTicks(1154),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 27, 21, 24, 59, 177, DateTimeKind.Utc).AddTicks(6578));

            migrationBuilder.CreateTable(
                name: "TestDrivesStatuses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    StatusId = table.Column<string>(nullable: false),
                    TestDriveId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDrivesStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestDrivesStatuses_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestDrivesStatuses_TestDrives_TestDriveId",
                        column: x => x.TestDriveId,
                        principalTable: "TestDrives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestDrivesStatuses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
        }
    }
}
