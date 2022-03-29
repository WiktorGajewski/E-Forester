using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Forester.Infrastructure.Migrations
{
    public partial class ChangesToPlanItemAndPlanExecution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeasureUnit",
                table: "PlanItems");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "PlanItems",
                newName: "PlannedHectares");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "PlanExecutions",
                newName: "HarvestedCubicMeters");

            migrationBuilder.AddColumn<double>(
                name: "PlannedCubicMeters",
                table: "PlanItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ExecutedHectares",
                table: "PlanExecutions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlannedCubicMeters",
                table: "PlanItems");

            migrationBuilder.DropColumn(
                name: "ExecutedHectares",
                table: "PlanExecutions");

            migrationBuilder.RenameColumn(
                name: "PlannedHectares",
                table: "PlanItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "HarvestedCubicMeters",
                table: "PlanExecutions",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "MeasureUnit",
                table: "PlanItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
