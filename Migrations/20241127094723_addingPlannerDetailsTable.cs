using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMvcFull.Migrations
{
    /// <inheritdoc />
    public partial class addingPlannerDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "planner_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    planner_id = table.Column<int>(type: "INTEGER", nullable: false),
                    day_id = table.Column<int>(type: "INTEGER", nullable: false),
                    production_total = table.Column<int>(type: "INTEGER", nullable: false),
                    plannersId = table.Column<int>(type: "INTEGER", nullable: false),
                    daysId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planner_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_planner_details_days_daysId",
                        column: x => x.daysId,
                        principalTable: "days",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_planner_details_planners_plannersId",
                        column: x => x.plannersId,
                        principalTable: "planners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_planner_details_daysId",
                table: "planner_details",
                column: "daysId");

            migrationBuilder.CreateIndex(
                name: "IX_planner_details_plannersId",
                table: "planner_details",
                column: "plannersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "planner_details");
        }
    }
}
