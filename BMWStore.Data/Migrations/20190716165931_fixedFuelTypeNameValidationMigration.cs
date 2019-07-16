using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class fixedFuelTypeNameValidationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 16, 16, 59, 30, 347, DateTimeKind.Utc).AddTicks(6708),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 15, 19, 35, 31, 945, DateTimeKind.Utc).AddTicks(8015));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FuelTypes",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 15, 19, 35, 31, 945, DateTimeKind.Utc).AddTicks(8015),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 16, 16, 59, 30, 347, DateTimeKind.Utc).AddTicks(6708));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FuelTypes",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 15);
        }
    }
}
