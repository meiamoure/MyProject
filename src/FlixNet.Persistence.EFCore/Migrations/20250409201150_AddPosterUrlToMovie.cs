using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixNet.Persistence.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddPosterUrlToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PosterUrl",
                schema: "flixnet",
                table: "Movies",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterUrl",
                schema: "flixnet",
                table: "Movies");
        }
    }
}
