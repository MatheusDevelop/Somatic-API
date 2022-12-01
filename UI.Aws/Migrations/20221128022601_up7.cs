using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UI.Aws.Migrations
{
    /// <inheritdoc />
    public partial class up7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sequence_Workouts_WorkoutId",
                table: "Sequence");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "Sequence",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Sequence_Workouts_WorkoutId",
                table: "Sequence",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sequence_Workouts_WorkoutId",
                table: "Sequence");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutId",
                table: "Sequence",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sequence_Workouts_WorkoutId",
                table: "Sequence",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");
        }
    }
}
