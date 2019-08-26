using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class removeExteriorEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseCars_Exteriors_ExteriorId",
                table: "BaseCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Exteriors_ExteriorId",
                table: "Pictures");

            migrationBuilder.DropTable(
                name: "Exteriors");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ExteriorId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ExteriorId",
                table: "Pictures");

            migrationBuilder.RenameColumn(
                name: "ExteriorId",
                table: "BaseCars",
                newName: "ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCars_ExteriorId",
                table: "BaseCars",
                newName: "IX_BaseCars_ColorId");

            migrationBuilder.AddColumn<string>(
                name: "BaseCarId",
                table: "Pictures",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Pictures",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 16, 20, 1, 14, 688, DateTimeKind.Utc).AddTicks(1304),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 16, 19, 25, 41, 555, DateTimeKind.Utc).AddTicks(3289));

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_BaseCarId",
                table: "Pictures",
                column: "BaseCarId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_Colors_ColorId",
                table: "BaseCars",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_BaseCars_BaseCarId",
                table: "Pictures",
                column: "BaseCarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseCars_Colors_ColorId",
                table: "BaseCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_BaseCars_BaseCarId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_BaseCarId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "BaseCarId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Pictures");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "BaseCars",
                newName: "ExteriorId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCars_ColorId",
                table: "BaseCars",
                newName: "IX_BaseCars_ExteriorId");

            migrationBuilder.AddColumn<string>(
                name: "ExteriorId",
                table: "Pictures",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 16, 19, 25, 41, 555, DateTimeKind.Utc).AddTicks(3289),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 16, 20, 1, 14, 688, DateTimeKind.Utc).AddTicks(1304));

            migrationBuilder.CreateTable(
                name: "Exteriors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ColorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exteriors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exteriors_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ExteriorId",
                table: "Pictures",
                column: "ExteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_Exteriors_ColorId",
                table: "Exteriors",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_Exteriors_ExteriorId",
                table: "BaseCars",
                column: "ExteriorId",
                principalTable: "Exteriors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Exteriors_ExteriorId",
                table: "Pictures",
                column: "ExteriorId",
                principalTable: "Exteriors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
