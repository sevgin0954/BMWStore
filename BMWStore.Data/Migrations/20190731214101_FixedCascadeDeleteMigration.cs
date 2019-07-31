using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class FixedCascadeDeleteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

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
                name: "FK_CarsOptions_Options_OptionId",
                table: "CarsOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Engines_Transmissions_TransmissionId",
                table: "Engines");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_Statuses_StatusId",
                table: "TestDrives");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_AspNetUsers_UserId",
                table: "TestDrives");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 31, 21, 41, 0, 75, DateTimeKind.Utc).AddTicks(2793),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 31, 21, 32, 49, 43, DateTimeKind.Utc).AddTicks(2381));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_CarsOptions_Options_OptionId",
                table: "CarsOptions",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Engines_Transmissions_TransmissionId",
                table: "Engines",
                column: "TransmissionId",
                principalTable: "Transmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives",
                column: "CarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_Statuses_StatusId",
                table: "TestDrives",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_AspNetUsers_UserId",
                table: "TestDrives",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

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
                name: "FK_CarsOptions_Options_OptionId",
                table: "CarsOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Engines_Transmissions_TransmissionId",
                table: "Engines");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_Statuses_StatusId",
                table: "TestDrives");

            migrationBuilder.DropForeignKey(
                name: "FK_TestDrives_AspNetUsers_UserId",
                table: "TestDrives");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduleDate",
                table: "TestDrives",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 31, 21, 32, 49, 43, DateTimeKind.Utc).AddTicks(2381),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 31, 21, 41, 0, 75, DateTimeKind.Utc).AddTicks(2793));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_CarsOptions_Options_OptionId",
                table: "CarsOptions",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Engines_Transmissions_TransmissionId",
                table: "Engines",
                column: "TransmissionId",
                principalTable: "Transmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_BaseCars_CarId",
                table: "TestDrives",
                column: "CarId",
                principalTable: "BaseCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_Statuses_StatusId",
                table: "TestDrives",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestDrives_AspNetUsers_UserId",
                table: "TestDrives",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
