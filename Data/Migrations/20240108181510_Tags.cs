using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class Tags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostModel_AspNetUsers_AuthorId",
                table: "BlogPostModel");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "BlogPostModel",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Tag",
                table: "BlogPostModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "BlogPostModel");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "BlogPostModel",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostModel_AspNetUsers_AuthorId",
                table: "BlogPostModel",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
