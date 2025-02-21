using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CircuitShare.Migrations
{
    /// <inheritdoc />
    public partial class AddedTotalElapsedTimeToCharger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalElapsedTime",
                table: "Chargers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 1,
                column: "TotalElapsedTime",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 2,
                column: "TotalElapsedTime",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 3,
                column: "TotalElapsedTime",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 4,
                column: "TotalElapsedTime",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 5,
                column: "TotalElapsedTime",
                value: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalElapsedTime",
                table: "Chargers");
        }
    }
}
