using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterBackend.Migrations
{
    /// <inheritdoc />
    public partial class healthconditionfieldupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthCondition",
                table: "Animals");

            migrationBuilder.AddColumn<string>(
                name: "HealthConditions",
                table: "Animals",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthConditions",
                table: "Animals");

            migrationBuilder.AddColumn<int>(
                name: "HealthCondition",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
