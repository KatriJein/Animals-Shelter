using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterBackend.Migrations
{
    /// <inheritdoc />
    public partial class mainimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImageName",
                table: "Animals",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageName",
                table: "Animals");
        }
    }
}
