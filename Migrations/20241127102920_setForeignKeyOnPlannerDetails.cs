using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMvcFull.Migrations
{
    /// <inheritdoc />
    public partial class setForeignKeyOnPlannerDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_planner_details_days_daysId",
                table: "planner_details");

            migrationBuilder.DropForeignKey(
                name: "FK_planner_details_planners_plannersId",
                table: "planner_details");

            migrationBuilder.DropIndex(
                name: "IX_planner_details_daysId",
                table: "planner_details");

            migrationBuilder.DropIndex(
                name: "IX_planner_details_plannersId",
                table: "planner_details");

            migrationBuilder.DropColumn(
                name: "daysId",
                table: "planner_details");

            migrationBuilder.DropColumn(
                name: "plannersId",
                table: "planner_details");

            migrationBuilder.CreateIndex(
                name: "IX_planner_details_day_id",
                table: "planner_details",
                column: "day_id");

            migrationBuilder.CreateIndex(
                name: "IX_planner_details_planner_id",
                table: "planner_details",
                column: "planner_id");

            migrationBuilder.AddForeignKey(
                name: "FK_planner_details_days_day_id",
                table: "planner_details",
                column: "day_id",
                principalTable: "days",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_planner_details_planners_planner_id",
                table: "planner_details",
                column: "planner_id",
                principalTable: "planners",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_planner_details_days_day_id",
                table: "planner_details");

            migrationBuilder.DropForeignKey(
                name: "FK_planner_details_planners_planner_id",
                table: "planner_details");

            migrationBuilder.DropIndex(
                name: "IX_planner_details_day_id",
                table: "planner_details");

            migrationBuilder.DropIndex(
                name: "IX_planner_details_planner_id",
                table: "planner_details");

            migrationBuilder.AddColumn<int>(
                name: "daysId",
                table: "planner_details",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "plannersId",
                table: "planner_details",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_planner_details_daysId",
                table: "planner_details",
                column: "daysId");

            migrationBuilder.CreateIndex(
                name: "IX_planner_details_plannersId",
                table: "planner_details",
                column: "plannersId");

            migrationBuilder.AddForeignKey(
                name: "FK_planner_details_days_daysId",
                table: "planner_details",
                column: "daysId",
                principalTable: "days",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_planner_details_planners_plannersId",
                table: "planner_details",
                column: "plannersId",
                principalTable: "planners",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
