using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuralChickens.Api.Domain.Migrations
{
    /// <inheritdoc />
    public partial class BigQueueChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingClaimId",
                table: "Simulations");

            migrationBuilder.DropColumn(
                name: "TrainingLeaseExpiresAt",
                table: "Simulations");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "FindSimulationConfigurations");

            migrationBuilder.AddColumn<int>(
                name: "SimulationRenderingJobId",
                table: "Simulations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "FindSimulationConfigurations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BodyDesign",
                table: "Chickens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BrainProcessingJobId",
                table: "ChickenBrains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BrainProcessingJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobStatus = table.Column<int>(type: "int", nullable: false),
                    BrainProcessingClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BrainProcessLeaseExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChickenId = table.Column<int>(type: "int", nullable: false),
                    SimulationType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrainProcessingJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrainProcessingJobs_Chickens_ChickenId",
                        column: x => x.ChickenId,
                        principalTable: "Chickens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationRenderingJob",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobStatus = table.Column<int>(type: "int", nullable: false),
                    SimulationProcessClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SimulationProcessLeaseExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationRenderingJob", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_SimulationRenderingJobId",
                table: "Simulations",
                column: "SimulationRenderingJobId");

            migrationBuilder.CreateIndex(
                name: "IX_ChickenBrains_BrainProcessingJobId",
                table: "ChickenBrains",
                column: "BrainProcessingJobId");

            migrationBuilder.CreateIndex(
                name: "IX_BrainProcessingJobs_ChickenId",
                table: "BrainProcessingJobs",
                column: "ChickenId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChickenBrains_BrainProcessingJobs_BrainProcessingJobId",
                table: "ChickenBrains",
                column: "BrainProcessingJobId",
                principalTable: "BrainProcessingJobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Simulations_SimulationRenderingJob_SimulationRenderingJobId",
                table: "Simulations",
                column: "SimulationRenderingJobId",
                principalTable: "SimulationRenderingJob",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChickenBrains_BrainProcessingJobs_BrainProcessingJobId",
                table: "ChickenBrains");

            migrationBuilder.DropForeignKey(
                name: "FK_Simulations_SimulationRenderingJob_SimulationRenderingJobId",
                table: "Simulations");

            migrationBuilder.DropTable(
                name: "BrainProcessingJobs");

            migrationBuilder.DropTable(
                name: "SimulationRenderingJob");

            migrationBuilder.DropIndex(
                name: "IX_Simulations_SimulationRenderingJobId",
                table: "Simulations");

            migrationBuilder.DropIndex(
                name: "IX_ChickenBrains_BrainProcessingJobId",
                table: "ChickenBrains");

            migrationBuilder.DropColumn(
                name: "SimulationRenderingJobId",
                table: "Simulations");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "FindSimulationConfigurations");

            migrationBuilder.DropColumn(
                name: "BodyDesign",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "BrainProcessingJobId",
                table: "ChickenBrains");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingClaimId",
                table: "Simulations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TrainingLeaseExpiresAt",
                table: "Simulations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Speed",
                table: "FindSimulationConfigurations",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
