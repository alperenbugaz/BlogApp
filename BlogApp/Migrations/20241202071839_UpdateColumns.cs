using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_Commentid",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Writers_WriterId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Comments_Commentid",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Writers_WriterId",
                table: "Likes");

            migrationBuilder.DropTable(
                name: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Likes_Commentid",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_WriterId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_Commentid",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_WriterId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Commentid",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Commentid",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "CommentText");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "CommentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "Commentid");

            migrationBuilder.RenameColumn(
                name: "CommentText",
                table: "Comments",
                newName: "Content");

            migrationBuilder.AddColumn<int>(
                name: "Commentid",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Commentid",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Writers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WriterUserName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_Commentid",
                table: "Likes",
                column: "Commentid");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_WriterId",
                table: "Likes",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Commentid",
                table: "Comments",
                column: "Commentid");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_WriterId",
                table: "Comments",
                column: "WriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_Commentid",
                table: "Comments",
                column: "Commentid",
                principalTable: "Comments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Writers_WriterId",
                table: "Comments",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Comments_Commentid",
                table: "Likes",
                column: "Commentid",
                principalTable: "Comments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Writers_WriterId",
                table: "Likes",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
