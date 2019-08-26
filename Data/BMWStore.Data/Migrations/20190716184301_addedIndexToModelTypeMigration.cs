using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedIndexToModelTypeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 16, 18, 43, 0, 111, DateTimeKind.Utc).AddTicks(7205),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 16, 18, 3, 14, 417, DateTimeKind.Utc).AddTicks(1218));

            migrationBuilder.CreateIndex(
                name: "IX_ModelTypes_Name",
                table: "ModelTypes",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ModelTypes_Name",
                table: "ModelTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 16, 18, 3, 14, 417, DateTimeKind.Utc).AddTicks(1218),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 16, 18, 43, 0, 111, DateTimeKind.Utc).AddTicks(7205));
        }
    }
}
