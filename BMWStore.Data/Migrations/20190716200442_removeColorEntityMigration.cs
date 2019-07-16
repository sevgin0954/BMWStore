using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class removeColorEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseCars_Colors_ColorId",
                table: "BaseCars");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_BaseCars_ColorId",
                table: "BaseCars");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "BaseCars",
                newName: "ColorName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 16, 20, 4, 40, 521, DateTimeKind.Utc).AddTicks(8770),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 16, 20, 1, 14, 688, DateTimeKind.Utc).AddTicks(1304));

            migrationBuilder.AlterColumn<string>(
                name: "ColorName",
                table: "BaseCars",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ColorName",
                table: "BaseCars",
                newName: "ColorId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 16, 20, 1, 14, 688, DateTimeKind.Utc).AddTicks(1304),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 16, 20, 4, 40, 521, DateTimeKind.Utc).AddTicks(8770));

            migrationBuilder.AlterColumn<string>(
                name: "ColorId",
                table: "BaseCars",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseCars_ColorId",
                table: "BaseCars",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_Colors_ColorId",
                table: "BaseCars",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
