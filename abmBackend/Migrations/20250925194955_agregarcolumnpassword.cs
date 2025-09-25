using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace abm.Migrations
{
    /// <inheritdoc />
    public partial class agregarcolumnpassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "Usuarios");
        }
    }
}
