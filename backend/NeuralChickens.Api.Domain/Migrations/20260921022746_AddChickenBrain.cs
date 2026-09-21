using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuralChickens.Api.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddChickenBrain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChickenBrains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SimulationType = table.Column<int>(type: "int", nullable: false),
                    BrainPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChickenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChickenBrains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChickenBrains_Chickens_ChickenId",
                        column: x => x.ChickenId,
                        principalTable: "Chickens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChickenBrains_ChickenId_SimulationType_CreatedAt",
                table: "ChickenBrains",
                columns: new[] { "ChickenId", "SimulationType", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChickenBrains");
        }
    }
}
