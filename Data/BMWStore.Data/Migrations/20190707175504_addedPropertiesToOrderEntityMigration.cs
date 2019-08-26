using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedPropertiesToOrderEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOrderedCars");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    CarId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 300, nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 7, 7, 17, 55, 2, 932, DateTimeKind.Utc).AddTicks(9828))
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserOrderedCars",
                columns: table => new
                {
                    CarId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 7, 7, 17, 35, 19, 824, DateTimeKind.Utc).AddTicks(880))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrderedCars", x => new { x.CarId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserOrderedCars_BaseCars_CarId",
                        column: x => x.CarId,
                        principalTable: "BaseCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOrderedCars_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOrderedCars_UserId",
                table: "UserOrderedCars",
                column: "UserId");
        }
    }
}
