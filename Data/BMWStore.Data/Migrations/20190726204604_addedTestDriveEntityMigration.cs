using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedTestDriveEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.CreateTable(
                name: "TestDrives",
                columns: table => new
                {
                    CarId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    ScheduleDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 7, 26, 20, 46, 3, 884, DateTimeKind.Utc).AddTicks(2668)),
                    Comment = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDrives", x => new { x.UserId, x.CarId });
                    table.ForeignKey(
                        name: "FK_TestDrives_BaseCars_CarId",
                        column: x => x.CarId,
                        principalTable: "BaseCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestDrives_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestDrives_CarId",
                table: "TestDrives",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestDrives");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    CarId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Address = table.Column<string>(maxLength: 300, nullable: false),
                    Id = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 7, 20, 19, 43, 40, 22, DateTimeKind.Utc).AddTicks(6948))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => new { x.CarId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Orders_BaseCars_CarId",
                        column: x => x.CarId,
                        principalTable: "BaseCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }
    }
}
