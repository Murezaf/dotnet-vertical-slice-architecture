using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelInspiration.API.Shared.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsSuggestedByAIToStop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSuggestedByAI",
                table: "Stops",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 19, 19, 1, 13, 260, DateTimeKind.Utc).AddTicks(6578));

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 19, 19, 1, 13, 260, DateTimeKind.Utc).AddTicks(6585));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 19, 19, 1, 13, 260, DateTimeKind.Utc).AddTicks(7138));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 19, 19, 1, 13, 260, DateTimeKind.Utc).AddTicks(7158));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 19, 19, 1, 13, 260, DateTimeKind.Utc).AddTicks(7172));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 19, 19, 1, 13, 260, DateTimeKind.Utc).AddTicks(7186));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 19, 19, 1, 13, 260, DateTimeKind.Utc).AddTicks(7202));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 19, 19, 1, 13, 260, DateTimeKind.Utc).AddTicks(7218));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuggestedByAI",
                table: "Stops");

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 17, 7, 59, 54, 661, DateTimeKind.Utc).AddTicks(7878));

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 17, 7, 59, 54, 661, DateTimeKind.Utc).AddTicks(7880));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 17, 7, 59, 54, 661, DateTimeKind.Utc).AddTicks(8094));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 17, 7, 59, 54, 661, DateTimeKind.Utc).AddTicks(8100));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 17, 7, 59, 54, 661, DateTimeKind.Utc).AddTicks(8105));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 17, 7, 59, 54, 661, DateTimeKind.Utc).AddTicks(8109));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 17, 7, 59, 54, 661, DateTimeKind.Utc).AddTicks(8114));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 17, 7, 59, 54, 661, DateTimeKind.Utc).AddTicks(8118));
        }
    }
}
