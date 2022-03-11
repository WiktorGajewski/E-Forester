using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Forester.Data.Migrations
{
    public partial class ChangesToPlanEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Plans_ForestUnitId",
                table: "Plans");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Plans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ForestUnitId_Year",
                table: "Plans",
                columns: new[] { "ForestUnitId", "Year" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Plans_ForestUnitId_Year",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Plans");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ForestUnitId",
                table: "Plans",
                column: "ForestUnitId");
        }
    }
}
