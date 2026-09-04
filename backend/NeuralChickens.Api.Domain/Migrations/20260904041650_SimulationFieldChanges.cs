using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuralChickens.Api.Domain.Migrations
{
    /// <inheritdoc />
    public partial class SimulationFieldChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Contestants",
                table: "Simulations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Simulations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contestants",
                table: "Simulations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Simulations");
        }
    }
}
