using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonebook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AlterColumn<byte>(
                name: "Role",
                table: "Roles",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
