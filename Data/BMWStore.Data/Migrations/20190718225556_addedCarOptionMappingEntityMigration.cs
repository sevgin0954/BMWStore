using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedCarOptionMappingEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_BaseCars_BaseCarId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_BaseCarId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "BaseCarId",
                table: "Options");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 18, 22, 55, 54, 962, DateTimeKind.Utc).AddTicks(5943),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 16, 20, 4, 40, 521, DateTimeKind.Utc).AddTicks(8770));

            migrationBuilder.CreateTable(
                name: "CarsOptions",
                columns: table => new
                {
                    CarId = table.Column<string>(nullable: false),
                    OptionId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsOptions", x => new { x.CarId, x.OptionId });
                    table.ForeignKey(
                        name: "FK_CarsOptions_BaseCars_CarId",
                        column: x => x.CarId,
                        principalTable: "BaseCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarsOptions_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarsOptions_OptionId",
                table: "CarsOptions",
                column: "OptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarsOptions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 16, 20, 4, 40, 521, DateTimeKind.Utc).AddTicks(8770),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 18, 22, 55, 54, 962, DateTimeKind.Utc).AddTicks(5943));

            migrationBuilder.AddColumn<string>(
                name: "BaseCarId",
                table: "Options",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_BaseCarId",
                table: "Options",
                column: "BaseCarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_BaseCars_BaseCarId",
                table: "Options",
                column: "BaseCarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
