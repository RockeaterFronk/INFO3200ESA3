using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CircuitShare.Migrations
{
    /// <inheritdoc />
    public partial class AddedRentalTrackingToCharger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RentedByUserId",
                table: "Chargers",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 1,
                column: "RentedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 2,
                column: "RentedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 3,
                column: "RentedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 4,
                column: "RentedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Chargers",
                keyColumn: "ChargerId",
                keyValue: 5,
                column: "RentedByUserId",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentedByUserId",
                table: "Chargers");
        }
    }
}
