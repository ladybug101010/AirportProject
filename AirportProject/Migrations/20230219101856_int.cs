using Microsoft.EntityFrameworkCore.Migrations;

namespace AirportProject.Migrations
{
    public partial class @int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Number",
                value: "bdcb46a0-e46e-446e-99d1-f6a055802685");

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Number",
                value: "b94abc25-a501-4b0e-9ddb-acbf2cff2cbe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Number",
                value: "a8747008-3d98-4f79-afd7-519564c58572");

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Number",
                value: "e95dae25-087e-4a28-919c-6f296b77f24c");
        }
    }
}
