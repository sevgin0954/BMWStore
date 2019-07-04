using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedUserEntitiesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseCar_Engines_EngineId",
                table: "BaseCar");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseCar_Exteriors_ExteriorId",
                table: "BaseCar");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseCar_FuelTypes_FuelTypeId",
                table: "BaseCar");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseCar_ModelTypes_ModelTypeId",
                table: "BaseCar");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseCar_Series_SeriesId",
                table: "BaseCar");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_BaseCar_BaseCarId",
                table: "Options");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseCar",
                table: "BaseCar");

            migrationBuilder.RenameTable(
                name: "BaseCar",
                newName: "BaseCars");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCar_SeriesId",
                table: "BaseCars",
                newName: "IX_BaseCars_SeriesId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCar_ModelTypeId",
                table: "BaseCars",
                newName: "IX_BaseCars_ModelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCar_FuelTypeId",
                table: "BaseCars",
                newName: "IX_BaseCars_FuelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCar_ExteriorId",
                table: "BaseCars",
                newName: "IX_BaseCars_ExteriorId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCar_EngineId",
                table: "BaseCars",
                newName: "IX_BaseCars_EngineId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "BaseCars",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseCars",
                table: "BaseCars",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserOrderedCars",
                columns: table => new
                {
                    CarId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 7, 4, 19, 26, 36, 145, DateTimeKind.Utc).AddTicks(9657))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrderedCars", x => new { x.CarId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserOrderedCars_BaseCars_CarId",
                        column: x => x.CarId,
                        principalTable: "BaseCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOrderedCars_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOrderedCars_UserId",
                table: "UserOrderedCars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_Engines_EngineId",
                table: "BaseCars",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_Exteriors_ExteriorId",
                table: "BaseCars",
                column: "ExteriorId",
                principalTable: "Exteriors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_FuelTypes_FuelTypeId",
                table: "BaseCars",
                column: "FuelTypeId",
                principalTable: "FuelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_ModelTypes_ModelTypeId",
                table: "BaseCars",
                column: "ModelTypeId",
                principalTable: "ModelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_Series_SeriesId",
                table: "BaseCars",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_BaseCars_BaseCarId",
                table: "Options",
                column: "BaseCarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseCars_Engines_EngineId",
                table: "BaseCars");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseCars_Exteriors_ExteriorId",
                table: "BaseCars");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseCars_FuelTypes_FuelTypeId",
                table: "BaseCars");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseCars_ModelTypes_ModelTypeId",
                table: "BaseCars");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseCars_Series_SeriesId",
                table: "BaseCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_BaseCars_BaseCarId",
                table: "Options");

            migrationBuilder.DropTable(
                name: "UserOrderedCars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseCars",
                table: "BaseCars");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "BaseCars");

            migrationBuilder.RenameTable(
                name: "BaseCars",
                newName: "BaseCar");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCars_SeriesId",
                table: "BaseCar",
                newName: "IX_BaseCar_SeriesId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCars_ModelTypeId",
                table: "BaseCar",
                newName: "IX_BaseCar_ModelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCars_FuelTypeId",
                table: "BaseCar",
                newName: "IX_BaseCar_FuelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCars_ExteriorId",
                table: "BaseCar",
                newName: "IX_BaseCar_ExteriorId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseCars_EngineId",
                table: "BaseCar",
                newName: "IX_BaseCar_EngineId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseCar",
                table: "BaseCar",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCar_Engines_EngineId",
                table: "BaseCar",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCar_Exteriors_ExteriorId",
                table: "BaseCar",
                column: "ExteriorId",
                principalTable: "Exteriors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCar_FuelTypes_FuelTypeId",
                table: "BaseCar",
                column: "FuelTypeId",
                principalTable: "FuelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCar_ModelTypes_ModelTypeId",
                table: "BaseCar",
                column: "ModelTypeId",
                principalTable: "ModelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCar_Series_SeriesId",
                table: "BaseCar",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_BaseCar_BaseCarId",
                table: "Options",
                column: "BaseCarId",
                principalTable: "BaseCar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
