using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Forester.Infrastructure.Migrations
{
    public partial class UniqueAddressForestUnitsDivisionsSubareas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Subareas_Address",
                table: "Subareas",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ForestUnits_Address",
                table: "ForestUnits",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_Address",
                table: "Divisions",
                column: "Address",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subareas_Address",
                table: "Subareas");

            migrationBuilder.DropIndex(
                name: "IX_ForestUnits_Address",
                table: "ForestUnits");

            migrationBuilder.DropIndex(
                name: "IX_Divisions_Address",
                table: "Divisions");
        }
    }
}
