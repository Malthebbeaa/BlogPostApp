using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPostApp.Migrations
{
    /// <inheritdoc />
    public partial class OneToTenRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OneToTen",
                table: "BlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneToTen",
                table: "BlogPosts");
        }
    }
}
