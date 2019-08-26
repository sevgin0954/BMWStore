using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedBaseCarPictureCascadeDeleteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_BaseCars_BaseCarId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_BaseCarId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "BaseCarId",
                table: "Pictures");

            migrationBuilder.AddColumn<string>(
                name: "CarId",
                table: "Pictures",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 19, 21, 14, 36, 324, DateTimeKind.Utc).AddTicks(4878),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 19, 19, 0, 9, 316, DateTimeKind.Utc).AddTicks(6217));

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_CarId",
                table: "Pictures",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_BaseCars_CarId",
                table: "Pictures",
                column: "CarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_BaseCars_CarId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_CarId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Pictures");

            migrationBuilder.AddColumn<string>(
                name: "BaseCarId",
                table: "Pictures",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 19, 19, 0, 9, 316, DateTimeKind.Utc).AddTicks(6217),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 19, 21, 14, 36, 324, DateTimeKind.Utc).AddTicks(4878));

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_BaseCarId",
                table: "Pictures",
                column: "BaseCarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_BaseCars_BaseCarId",
                table: "Pictures",
                column: "BaseCarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
