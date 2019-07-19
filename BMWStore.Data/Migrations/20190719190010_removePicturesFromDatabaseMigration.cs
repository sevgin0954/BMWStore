using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class removePicturesFromDatabaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Pictures");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Pictures",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 19, 19, 0, 9, 316, DateTimeKind.Utc).AddTicks(6217),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 18, 22, 55, 54, 962, DateTimeKind.Utc).AddTicks(5943));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Pictures");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Pictures",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 18, 22, 55, 54, 962, DateTimeKind.Utc).AddTicks(5943),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 19, 19, 0, 9, 316, DateTimeKind.Utc).AddTicks(6217));
        }
    }
}
