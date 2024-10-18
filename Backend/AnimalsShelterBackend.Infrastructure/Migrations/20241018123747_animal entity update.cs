using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterBackend.Migrations
{
    /// <inheritdoc />
    public partial class animalentityupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BehaviourFeatures",
                table: "Animals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "Animals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Animals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HealthDescription",
                table: "Animals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagesNames",
                table: "Animals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerWishes",
                table: "Animals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequiredConditions",
                table: "Animals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SterilizationsInfo",
                table: "Animals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Temper",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VaccinationsInfo",
                table: "Animals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Wool",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "BehaviourFeatures",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Breed",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "HealthDescription",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ImagesNames",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "OwnerWishes",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "RequiredConditions",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SterilizationsInfo",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Temper",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "VaccinationsInfo",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Wool",
                table: "Animals");
        }
    }
}
