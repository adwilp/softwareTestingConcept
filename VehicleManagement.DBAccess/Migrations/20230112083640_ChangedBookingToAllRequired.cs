using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManagement.DBAccess.Migrations
{
    public partial class ChangedBookingToAllRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Vehicles_FIN",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "FIN",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeNumber",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Vehicles_FIN",
                table: "Bookings",
                column: "FIN",
                principalTable: "Vehicles",
                principalColumn: "FIN",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Vehicles_FIN",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "FIN",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeNumber",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Vehicles_FIN",
                table: "Bookings",
                column: "FIN",
                principalTable: "Vehicles",
                principalColumn: "FIN");
        }
    }
}
