using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class changeCarMileageValidationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 20, 19, 43, 40, 22, DateTimeKind.Utc).AddTicks(6948),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 20, 15, 15, 27, 514, DateTimeKind.Utc).AddTicks(451));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 20, 15, 15, 27, 514, DateTimeKind.Utc).AddTicks(451),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 20, 19, 43, 40, 22, DateTimeKind.Utc).AddTicks(6948));
        }
    }
}
