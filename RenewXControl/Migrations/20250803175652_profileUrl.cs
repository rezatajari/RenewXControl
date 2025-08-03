using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenewXControl.Migrations
{
    /// <inheritdoc />
    public partial class profileUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");
        }
    }
}
