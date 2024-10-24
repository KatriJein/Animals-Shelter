using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterBackend.Migrations
{
    /// <inheritdoc />
    public partial class animalentityfieldsrename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainImageName",
                table: "Animals",
                newName: "MainImageSource");

            migrationBuilder.RenameColumn(
                name: "ImagesNames",
                table: "Animals",
                newName: "ImagesSources");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MainImageSource",
                table: "Animals",
                newName: "MainImageName");

            migrationBuilder.RenameColumn(
                name: "ImagesSources",
                table: "Animals",
                newName: "ImagesNames");
        }
    }
}
