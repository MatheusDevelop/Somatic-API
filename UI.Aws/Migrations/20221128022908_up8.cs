using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI.Aws.Migrations
{
    /// <inheritdoc />
    public partial class up8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeanerWorkouts");

            migrationBuilder.CreateTable(
                name: "LeanerWorkout",
                columns: table => new
                {
                    LeanersId = table.Column<int>(type: "int", nullable: false),
                    WorkoutsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeanerWorkout", x => new { x.LeanersId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_LeanerWorkout_Users_LeanersId",
                        column: x => x.LeanersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeanerWorkout_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeanerWorkout_WorkoutsId",
                table: "LeanerWorkout",
                column: "WorkoutsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeanerWorkout");

            migrationBuilder.CreateTable(
                name: "LeanerWorkouts",
                columns: table => new
                {
                    LeanersId = table.Column<int>(type: "int", nullable: false),
                    WorkoutsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeanerWorkouts", x => new { x.LeanersId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_LeanerWorkouts_Users_LeanersId",
                        column: x => x.LeanersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeanerWorkouts_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeanerWorkouts_WorkoutsId",
                table: "LeanerWorkouts",
                column: "WorkoutsId");
        }
    }
}
