using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Forester.Data.Migrations
{
    public partial class AddModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AppUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ForestUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForestUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    ForestUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Divisions_ForestUnits_ForestUnitId",
                        column: x => x.ForestUnitId,
                        principalTable: "ForestUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForestUnitUser",
                columns: table => new
                {
                    AssignedForestUnitsId = table.Column<int>(type: "int", nullable: false),
                    AssignedUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForestUnitUser", x => new { x.AssignedForestUnitsId, x.AssignedUsersId });
                    table.ForeignKey(
                        name: "FK_ForestUnitUser_AppUsers_AssignedUsersId",
                        column: x => x.AssignedUsersId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForestUnitUser_ForestUnits_AssignedForestUnitsId",
                        column: x => x.AssignedForestUnitsId,
                        principalTable: "ForestUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ForestUnitId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plans_AppUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plans_ForestUnits_ForestUnitId",
                        column: x => x.ForestUnitId,
                        principalTable: "ForestUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    DivisionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subareas_Divisions_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    MeasureUnit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Assortments = table.Column<int>(type: "int", nullable: false),
                    ActionGroup = table.Column<int>(type: "int", nullable: false),
                    DifficultyLevel = table.Column<int>(type: "int", nullable: false),
                    Factor = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    SubareaId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanItems_AppUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanItems_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanItems_Subareas_SubareaId",
                        column: x => x.SubareaId,
                        principalTable: "Subareas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlanExecutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanItemId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanExecutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanExecutions_AppUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanExecutions_PlanItems_PlanItemId",
                        column: x => x.PlanItemId,
                        principalTable: "PlanItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanExecutions_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_ForestUnitId",
                table: "Divisions",
                column: "ForestUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ForestUnitUser_AssignedUsersId",
                table: "ForestUnitUser",
                column: "AssignedUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanExecutions_CreatorId",
                table: "PlanExecutions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanExecutions_PlanId",
                table: "PlanExecutions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanExecutions_PlanItemId",
                table: "PlanExecutions",
                column: "PlanItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanItems_CreatorId",
                table: "PlanItems",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanItems_PlanId",
                table: "PlanItems",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanItems_SubareaId",
                table: "PlanItems",
                column: "SubareaId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_CreatorId",
                table: "Plans",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ForestUnitId",
                table: "Plans",
                column: "ForestUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Subareas_DivisionId",
                table: "Subareas",
                column: "DivisionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForestUnitUser");

            migrationBuilder.DropTable(
                name: "PlanExecutions");

            migrationBuilder.DropTable(
                name: "PlanItems");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Subareas");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "ForestUnits");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "AppUsers");
        }
    }
}
