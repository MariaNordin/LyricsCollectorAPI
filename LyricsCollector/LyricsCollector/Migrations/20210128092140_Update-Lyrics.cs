using Microsoft.EntityFrameworkCore.Migrations;

namespace LyricsCollector.Migrations
{
    public partial class UpdateLyrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "Lyrics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpotifyLink",
                table: "Lyrics",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Lyrics");

            migrationBuilder.DropColumn(
                name: "SpotifyLink",
                table: "Lyrics");
        }
    }
}
