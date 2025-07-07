using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenewXControl.Migrations
{
    /// <inheritdoc />
    public partial class AddCapacityToBattery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Capacity",
                table: "Assets",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Assets");
        }
    }
}
