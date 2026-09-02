using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuralChickens.Api.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chickens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chickens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Simulations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SimulationType = table.Column<int>(type: "int", nullable: false),
                    SimulationStatus = table.Column<int>(type: "int", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VideoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FindSimulationConfigurations",
                columns: table => new
                {
                    SimulationId = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindSimulationConfigurations", x => x.SimulationId);
                    table.ForeignKey(
                        name: "FK_FindSimulationConfigurations_Simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "Simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FindSimulationResults",
                columns: table => new
                {
                    SimulationId = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindSimulationResults", x => x.SimulationId);
                    table.ForeignKey(
                        name: "FK_FindSimulationResults_Simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "Simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceSimulationConfigurations",
                columns: table => new
                {
                    SimulationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceSimulationConfigurations", x => x.SimulationId);
                    table.ForeignKey(
                        name: "FK_RaceSimulationConfigurations_Simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "Simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceSimulationResults",
                columns: table => new
                {
                    SimulationId = table.Column<int>(type: "int", nullable: false),
                    WinnerChickenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceSimulationResults", x => x.SimulationId);
                    table.ForeignKey(
                        name: "FK_RaceSimulationResults_Simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "Simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationChickens",
                columns: table => new
                {
                    SimulationId = table.Column<int>(type: "int", nullable: false),
                    ChickenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationChickens", x => new { x.SimulationId, x.ChickenId });
                    table.ForeignKey(
                        name: "FK_SimulationChickens_Chickens_ChickenId",
                        column: x => x.ChickenId,
                        principalTable: "Chickens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SimulationChickens_Simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "Simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimulationChickens_ChickenId",
                table: "SimulationChickens",
                column: "ChickenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FindSimulationConfigurations");

            migrationBuilder.DropTable(
                name: "FindSimulationResults");

            migrationBuilder.DropTable(
                name: "RaceSimulationConfigurations");

            migrationBuilder.DropTable(
                name: "RaceSimulationResults");

            migrationBuilder.DropTable(
                name: "SimulationChickens");

            migrationBuilder.DropTable(
                name: "Chickens");

            migrationBuilder.DropTable(
                name: "Simulations");
        }
    }
}
