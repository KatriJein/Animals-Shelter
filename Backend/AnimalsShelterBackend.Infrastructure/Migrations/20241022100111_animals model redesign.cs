using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalsShelterBackend.Migrations
{
    /// <inheritdoc />
    public partial class animalsmodelredesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BehaviourFeatures",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "HealthDescription",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "OwnerWishes",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "RequiredConditions",
                table: "Animals");

            migrationBuilder.DropColumn(name: "Color", table: "Animals");

            migrationBuilder.RenameColumn(
                name: "VaccinationsInfo",
                table: "Animals",
                newName: "TemperFeatures");

            migrationBuilder.RenameColumn(
                name: "Temper",
                table: "Animals",
                newName: "ReceiptDate");

            migrationBuilder.RenameColumn(
                name: "SterilizationsInfo",
                table: "Animals",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

			migrationBuilder.AddColumn<int>(
                name: "HealthCondition",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LivingCondition",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthCondition",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "LivingCondition",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "TemperFeatures",
                table: "Animals",
                newName: "VaccinationsInfo");

            migrationBuilder.RenameColumn(
                name: "ReceiptDate",
                table: "Animals",
                newName: "Temper");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Animals",
                newName: "SterilizationsInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Animals",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "BehaviourFeatures",
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
                name: "OwnerWishes",
                table: "Animals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequiredConditions",
                table: "Animals",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
