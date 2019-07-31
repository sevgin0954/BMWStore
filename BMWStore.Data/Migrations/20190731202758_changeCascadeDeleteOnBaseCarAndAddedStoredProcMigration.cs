using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class changeCascadeDeleteOnBaseCarAndAddedStoredProcMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createType =
                "CREATE TYPE [dbo].[BaseCars] AS TABLE ( " +
                    "[Price] DECIMAL(18, 2) NOT NULL);";
            migrationBuilder.Sql(createType);

            var createStoredProcedure =
                "CREATE PROCEDURE [dbo].[usp_GetCarPriceRangesCount] " +
                "	@cars BaseCars READONLY " +
                "AS " +
                "SELECT Range AS Value, COUNT(*) AS CarsCount, Range AS Text FROM ( " +
                "	SELECT " +
                "		CASE " +
                "			WHEN Price >= 10.000 AND Price <= 39.999 THEN '10.000 - 39.999' " +
                "			WHEN Price >= 40.000 AND Price <= 49.999 THEN '40.000 - 49.999' " +
                "			WHEN Price >= 50.000 AND Price <= 59.999 THEN '50.000 - 59.999' " +
                "			WHEN Price >= 60.000 AND Price <= 69.999 THEN '60.000 - 69.999' " +
                "			WHEN Price >= 70.000 AND Price <= 79.999 THEN '70.000 - 79.999' " +
                "			WHEN Price >= 80.000 AND Price <= 89.999 THEN '80.000 - 89.999' " +
                "			WHEN Price >= 100.000 AND Price <= 149.999 THEN '100.000 - 149.999' " +
                "		END AS Range " +
                "	FROM @cars " +
                ") AS t " +
                "GROUP BY Range " +
                "RETURN 0 ";
            migrationBuilder.Sql(createStoredProcedure);

            migrationBuilder.DropForeignKey(
                name: "FK_BaseCars_Engines_EngineId",
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
                name: "FK_CarsOptions_BaseCars_CarId",
                table: "CarsOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 31, 20, 27, 57, 924, DateTimeKind.Utc).AddTicks(5399),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 27, 21, 24, 59, 177, DateTimeKind.Utc).AddTicks(6578));

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_Engines_EngineId",
                table: "BaseCars",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_FuelTypes_FuelTypeId",
                table: "BaseCars",
                column: "FuelTypeId",
                principalTable: "FuelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_ModelTypes_ModelTypeId",
                table: "BaseCars",
                column: "ModelTypeId",
                principalTable: "ModelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_Series_SeriesId",
                table: "BaseCars",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarsOptions_BaseCars_CarId",
                table: "CarsOptions",
                column: "CarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives",
                column: "CarId",
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
                name: "FK_BaseCars_FuelTypes_FuelTypeId",
                table: "BaseCars");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseCars_ModelTypes_ModelTypeId",
                table: "BaseCars");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseCars_Series_SeriesId",
                table: "BaseCars");

            migrationBuilder.DropForeignKey(
                name: "FK_CarsOptions_BaseCars_CarId",
                table: "CarsOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 27, 21, 24, 59, 177, DateTimeKind.Utc).AddTicks(6578),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 31, 20, 27, 57, 924, DateTimeKind.Utc).AddTicks(5399));

            migrationBuilder.AddForeignKey(
                name: "FK_BaseCars_Engines_EngineId",
                table: "BaseCars",
                column: "EngineId",
                principalTable: "Engines",
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
                name: "FK_CarsOptions_BaseCars_CarId",
                table: "CarsOptions",
                column: "CarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives",
                column: "CarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
