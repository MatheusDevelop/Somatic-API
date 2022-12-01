using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI.Aws.Migrations
{
    /// <inheritdoc />
    public partial class Up2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sequence_Workouts_WorkoutId",
                table: "Sequence");

            migrationBuilder.AddForeignKey(
                name: "FK_Sequence_Workouts_WorkoutId",
                table: "Sequence",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sequence_Workouts_WorkoutId",
                table: "Sequence");

            migrationBuilder.AddForeignKey(
                name: "FK_Sequence_Workouts_WorkoutId",
                table: "Sequence",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");
        }
    }
}
