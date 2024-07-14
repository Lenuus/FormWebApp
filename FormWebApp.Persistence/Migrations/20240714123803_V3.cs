using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormWebApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataType",
                table: "Fields");

            migrationBuilder.AddColumn<int>(
                name: "FieldType",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldType",
                table: "Fields");

            migrationBuilder.AddColumn<string>(
                name: "DataType",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
