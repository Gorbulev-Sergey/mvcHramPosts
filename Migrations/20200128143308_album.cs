using Microsoft.EntityFrameworkCore.Migrations;

namespace mvcHramPosts.Migrations
{
    public partial class album : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "postID",
                table: "imageAlbums");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "postID",
                table: "imageAlbums",
                nullable: false,
                defaultValue: 0);
        }
    }
}
