using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class changedPictureUrlToPublicIdProperyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Pictures",
                newName: "PublicId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 20, 15, 15, 27, 514, DateTimeKind.Utc).AddTicks(451),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 19, 21, 14, 36, 324, DateTimeKind.Utc).AddTicks(4878));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Pictures",
                newName: "Url");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 19, 21, 14, 36, 324, DateTimeKind.Utc).AddTicks(4878),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 20, 15, 15, 27, 514, DateTimeKind.Utc).AddTicks(451));
        }
    }
}
