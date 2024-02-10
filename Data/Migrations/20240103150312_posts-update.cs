using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class postsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "BlogPostModel",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostModel_AuthorId",
                table: "BlogPostModel",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostModel_AspNetUsers_AuthorId",
                table: "BlogPostModel",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostModel_AspNetUsers_AuthorId",
                table: "BlogPostModel");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostModel_AuthorId",
                table: "BlogPostModel");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "BlogPostModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
