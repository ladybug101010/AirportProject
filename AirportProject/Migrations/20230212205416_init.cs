using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirportProject.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    PassengersCount = table.Column<int>(nullable: false),
                    IsCritical = table.Column<bool>(nullable: false),
                    Brand = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    FlightStatusId = table.Column<long>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_FlightStatuses_FlightStatusId",
                        column: x => x.FlightStatusId,
                        principalTable: "FlightStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Legs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    FlightId = table.Column<long>(nullable: true),
                    IsFirstStop = table.Column<bool>(nullable: false),
                    OrderNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Legs_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LegToLegs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromId = table.Column<long>(nullable: true),
                    ToId = table.Column<long>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegToLegs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegToLegs_Legs_FromId",
                        column: x => x.FromId,
                        principalTable: "Legs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegToLegs_Legs_ToId",
                        column: x => x.ToId,
                        principalTable: "Legs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FlightStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Waiting" },
                    { 2L, "Processing" },
                    { 3L, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Legs",
                columns: new[] { "Id", "Capacity", "FlightId", "IsFirstStop", "Number", "OrderNo", "Type" },
                values: new object[,]
                {
                    { 1L, 1, null, true, 1, 5, 1 },
                    { 2L, 1, null, false, 2, 4, 1 },
                    { 3L, 1, null, false, 3, 3, 1 },
                    { 4L, 1, null, false, 4, 2, 4 },
                    { 5L, 1, null, false, 5, 6, 1 },
                    { 6L, 1, null, true, 6, 8, 4 },
                    { 7L, 1, null, true, 7, 9, 4 },
                    { 8L, 1, null, false, 8, 7, 2 },
                    { 9L, 1, null, false, 9, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "Brand", "FlightStatusId", "IsCritical", "LastUpdate", "Number", "PassengersCount", "Type" },
                values: new object[,]
                {
                    { 1L, "ELAL", 1L, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a8747008-3d98-4f79-afd7-519564c58572", 200, 2 },
                    { 2L, "ELAL", 1L, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e95dae25-087e-4a28-919c-6f296b77f24c", 200, 1 }
                });

            migrationBuilder.InsertData(
                table: "LegToLegs",
                columns: new[] { "Id", "FromId", "ToId", "Type" },
                values: new object[,]
                {
                    { 1L, 1L, 2L, 1 },
                    { 2L, 2L, 3L, 1 },
                    { 3L, 3L, 4L, 1 },
                    { 4L, 4L, 5L, 1 },
                    { 6L, 5L, 6L, 1 },
                    { 7L, 5L, 7L, 1 },
                    { 8L, 6L, 8L, 2 },
                    { 9L, 7L, 8L, 2 },
                    { 10L, 8L, 4L, 2 },
                    { 5L, 4L, 9L, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FlightStatusId",
                table: "Flights",
                column: "FlightStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Legs_FlightId",
                table: "Legs",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_LegToLegs_FromId",
                table: "LegToLegs",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_LegToLegs_ToId",
                table: "LegToLegs",
                column: "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegToLegs");

            migrationBuilder.DropTable(
                name: "Legs");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "FlightStatuses");
        }
    }
}
