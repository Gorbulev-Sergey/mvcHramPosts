using Microsoft.EntityFrameworkCore.Migrations;

namespace mvcHramPosts.Migrations
{
    public partial class ImageAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_image_imageAlbum_imageAlbumID",
                table: "image");

            migrationBuilder.DropForeignKey(
                name: "FK_imageAlbum_AspNetUsers_userId1",
                table: "imageAlbum");

            migrationBuilder.DropPrimaryKey(
                name: "PK_imageAlbum",
                table: "imageAlbum");

            migrationBuilder.RenameTable(
                name: "imageAlbum",
                newName: "imageAlbums");

            migrationBuilder.RenameIndex(
                name: "IX_imageAlbum_userId1",
                table: "imageAlbums",
                newName: "IX_imageAlbums_userId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_imageAlbums",
                table: "imageAlbums",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_image_imageAlbums_imageAlbumID",
                table: "image",
                column: "imageAlbumID",
                principalTable: "imageAlbums",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_imageAlbums_AspNetUsers_userId1",
                table: "imageAlbums",
                column: "userId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_image_imageAlbums_imageAlbumID",
                table: "image");

            migrationBuilder.DropForeignKey(
                name: "FK_imageAlbums_AspNetUsers_userId1",
                table: "imageAlbums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_imageAlbums",
                table: "imageAlbums");

            migrationBuilder.RenameTable(
                name: "imageAlbums",
                newName: "imageAlbum");

            migrationBuilder.RenameIndex(
                name: "IX_imageAlbums_userId1",
                table: "imageAlbum",
                newName: "IX_imageAlbum_userId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_imageAlbum",
                table: "imageAlbum",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_image_imageAlbum_imageAlbumID",
                table: "image",
                column: "imageAlbumID",
                principalTable: "imageAlbum",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_imageAlbum_AspNetUsers_userId1",
                table: "imageAlbum",
                column: "userId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
