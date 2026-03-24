using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolRegister.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ParentId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_ParentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_Parent",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_Parent",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ParentId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Parent",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "ParentId1",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ParentId",
                table: "AspNetUsers",
                column: "ParentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Groups_GroupId",
                table: "AspNetUsers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AspNetUsers_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ParentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_StudentId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_StudentId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Parent",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Parent",
                table: "Grades",
                column: "Parent");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParentId1",
                table: "AspNetUsers",
                column: "ParentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ParentId1",
                table: "AspNetUsers",
                column: "ParentId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Groups_ParentId",
                table: "AspNetUsers",
                column: "ParentId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AspNetUsers_Parent",
                table: "Grades",
                column: "Parent",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
