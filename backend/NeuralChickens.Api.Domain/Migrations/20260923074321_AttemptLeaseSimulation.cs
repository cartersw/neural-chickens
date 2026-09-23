using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuralChickens.Api.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AttemptLeaseSimulation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingClaimId",
                table: "Simulations");

            migrationBuilder.DropColumn(
                name: "TrainingLeaseExpiresAt",
                table: "Simulations");
        }
    }
}
