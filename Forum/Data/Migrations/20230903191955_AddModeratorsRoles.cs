using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Data.Migrations
{
    public partial class AddModeratorsRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReplys_Posts_PostId",
                table: "PostReplys");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "PostReplys",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReplys_Posts_PostId",
                table: "PostReplys",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReplys_Posts_PostId",
                table: "PostReplys");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "PostReplys",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReplys_Posts_PostId",
                table: "PostReplys",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
