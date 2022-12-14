using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManagement.DBAccess.Migrations
{
    public partial class AddedBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIN = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Vehicles_FIN",
                        column: x => x.FIN,
                        principalTable: "Vehicles",
                        principalColumn: "FIN");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FIN",
                table: "Bookings",
                column: "FIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
