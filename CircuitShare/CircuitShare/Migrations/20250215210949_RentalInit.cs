using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CircuitShare.Migrations
{
    /// <inheritdoc />
    public partial class RentalInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AmountDue",
                table: "Chargers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalEndTime",
                table: "Chargers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalStartTime",
                table: "Chargers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActiveChargerId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 1,
                columns: new[] { "AmountDue", "RentalEndTime", "RentalStartTime" },
                values: new object[] { 0.0, null, null });

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 2,
                columns: new[] { "AmountDue", "RentalEndTime", "RentalStartTime" },
                values: new object[] { 0.0, null, null });

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 3,
                columns: new[] { "AmountDue", "RentalEndTime", "RentalStartTime" },
                values: new object[] { 0.0, null, null });

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 4,
                columns: new[] { "AmountDue", "RentalEndTime", "RentalStartTime" },
                values: new object[] { 0.0, null, null });

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 5,
                columns: new[] { "AmountDue", "RentalEndTime", "RentalStartTime" },
                values: new object[] { 0.0, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ActiveChargerId",
                table: "AspNetUsers",
                column: "ActiveChargerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Chargers_ActiveChargerId",
                table: "AspNetUsers",
                column: "ActiveChargerId",
                principalTable: "Chargers",
                principalColumn: "ChargerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Chargers_ActiveChargerId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ActiveChargerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AmountDue",
                table: "Chargers");

            migrationBuilder.DropColumn(
                name: "RentalEndTime",
                table: "Chargers");

            migrationBuilder.DropColumn(
                name: "RentalStartTime",
                table: "Chargers");

            migrationBuilder.DropColumn(
                name: "ActiveChargerId",
                table: "AspNetUsers");
        }
    }
}
