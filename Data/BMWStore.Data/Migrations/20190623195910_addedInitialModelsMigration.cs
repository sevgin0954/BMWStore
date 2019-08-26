using Microsoft.EntityFrameworkCore.Migrations;

namespace BMWStore.Data.Migrations
{
    public partial class addedInitialModelsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "FuelTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModelTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transmissions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transmissions", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    TransmissionId = table.Column<string>(nullable: false),
                    Weight_Kg = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engines_Transmissions_TransmissionId",
                        column: x => x.TransmissionId,
                        principalTable: "Transmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ExteriorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Exteriors_ExteriorId",
                        column: x => x.ExteriorId,
                        principalTable: "Exteriors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseCar",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Acceleration_0_100Km = table.Column<double>(nullable: false),
                    CO2Emissions = table.Column<int>(nullable: false),
                    Displacement = table.Column<double>(nullable: false),
                    DoorsCount = table.Column<int>(nullable: false),
                    EngineId = table.Column<string>(nullable: false),
                    ExteriorId = table.Column<string>(nullable: false),
                    FuelConsumation_City_Litres_100Km = table.Column<double>(nullable: false),
                    FuelConsumation_Highway_Litres_100Km = table.Column<double>(nullable: false),
                    FuelTypeId = table.Column<string>(nullable: false),
                    HoursePower = table.Column<double>(nullable: false),
                    ModelTypeId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    SeriesId = table.Column<string>(nullable: false),
                    Torque = table.Column<decimal>(nullable: false),
                    Vin = table.Column<string>(nullable: false),
                    WarrantyMonthsLeft = table.Column<int>(nullable: false),
                    Weight_Kg = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Mileage = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseCar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseCar_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseCar_Exteriors_ExteriorId",
                        column: x => x.ExteriorId,
                        principalTable: "Exteriors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseCar_FuelTypes_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalTable: "FuelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseCar_ModelTypes_ModelTypeId",
                        column: x => x.ModelTypeId,
                        principalTable: "ModelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseCar_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    BaseCarId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_BaseCar_BaseCarId",
                        column: x => x.BaseCarId,
                        principalTable: "BaseCar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseCar_EngineId",
                table: "BaseCar",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseCar_ExteriorId",
                table: "BaseCar",
                column: "ExteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseCar_FuelTypeId",
                table: "BaseCar",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseCar_ModelTypeId",
                table: "BaseCar",
                column: "ModelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseCar_SeriesId",
                table: "BaseCar",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_TransmissionId",
                table: "Engines",
                column: "TransmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Exteriors_ColorId",
                table: "Exteriors",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_BaseCarId",
                table: "Options",
                column: "BaseCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ExteriorId",
                table: "Pictures",
                column: "ExteriorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "BaseCar");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "Exteriors");

            migrationBuilder.DropTable(
                name: "FuelTypes");

            migrationBuilder.DropTable(
                name: "ModelTypes");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Transmissions");

            migrationBuilder.DropTable(
                name: "Colors");
        }
    }
}
