using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Forester.Infrastructure.Migrations
{
    public partial class NewIndexForPlanItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlanItems_PlanId",
                table: "PlanItems");

            migrationBuilder.CreateIndex(
                name: "IX_PlanItems_PlanId_SubareaId_ActionGroup",
                table: "PlanItems",
                columns: new[] { "PlanId", "SubareaId", "ActionGroup" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlanItems_PlanId_SubareaId_ActionGroup",
                table: "PlanItems");

            migrationBuilder.CreateIndex(
                name: "IX_PlanItems_PlanId",
                table: "PlanItems",
                column: "PlanId");
        }
    }
}
