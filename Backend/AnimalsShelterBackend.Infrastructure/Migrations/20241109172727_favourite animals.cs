using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterBackend.Migrations
{
    /// <inheritdoc />
    public partial class favouriteanimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalUser",
                columns: table => new
                {
                    FavouriteAnimalsId = table.Column<Guid>(type: "uuid", nullable: false),
                    FavouritedByUsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalUser", x => new { x.FavouriteAnimalsId, x.FavouritedByUsersId });
                    table.ForeignKey(
                        name: "FK_AnimalUser_Animals_FavouriteAnimalsId",
                        column: x => x.FavouriteAnimalsId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalUser_Users_FavouritedByUsersId",
                        column: x => x.FavouritedByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalUser_FavouritedByUsersId",
                table: "AnimalUser",
                column: "FavouritedByUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalUser");
        }
    }
}
