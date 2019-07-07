using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedUserPropertiesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "UserOrderedCars",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 7, 17, 35, 19, 824, DateTimeKind.Utc).AddTicks(880),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 4, 19, 26, 36, 145, DateTimeKind.Utc).AddTicks(9657));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "UserOrderedCars",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 4, 19, 26, 36, 145, DateTimeKind.Utc).AddTicks(9657),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 7, 17, 35, 19, 824, DateTimeKind.Utc).AddTicks(880));
        }
    }
}
