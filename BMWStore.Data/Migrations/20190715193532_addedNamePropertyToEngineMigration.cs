using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedNamePropertyToEngineMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 15, 19, 35, 31, 945, DateTimeKind.Utc).AddTicks(8015),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 14, 23, 28, 23, 574, DateTimeKind.Utc).AddTicks(4589));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Engines",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Engines");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 14, 23, 28, 23, 574, DateTimeKind.Utc).AddTicks(4589),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 15, 19, 35, 31, 945, DateTimeKind.Utc).AddTicks(8015));
        }
    }
}
