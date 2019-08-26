using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedIndexToEnginesFuelTypesAndSeriesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 16, 18, 45, 20, 369, DateTimeKind.Utc).AddTicks(5492),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 16, 18, 43, 0, 111, DateTimeKind.Utc).AddTicks(7205));

            migrationBuilder.CreateIndex(
                name: "IX_Series_Name",
                table: "Series",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_FuelTypes_Name",
                table: "FuelTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_Name",
                table: "Engines",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Series_Name",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_FuelTypes_Name",
                table: "FuelTypes");

            migrationBuilder.DropIndex(
                name: "IX_Engines_Name",
                table: "Engines");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 16, 18, 43, 0, 111, DateTimeKind.Utc).AddTicks(7205),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 16, 18, 45, 20, 369, DateTimeKind.Utc).AddTicks(5492));
        }
    }
}
