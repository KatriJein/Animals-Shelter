using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterBackend.Migrations
{
    /// <inheritdoc />
    public partial class updatearticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_User_UserId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Articles",
                newName: "authorId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                newName: "IX_Articles_authorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_User_authorId",
                table: "Articles",
                column: "authorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_User_authorId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "authorId",
                table: "Articles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_authorId",
                table: "Articles",
                newName: "IX_Articles_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_User_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
