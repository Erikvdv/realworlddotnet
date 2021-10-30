using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Realworlddotnet.Infrastructure.Migrations
{
    public partial class MigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AuthorUsername",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Articles_ArticleId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ArticleId",
                table: "Comment");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId1",
                table: "Comment",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ArticlesId",
                table: "ArticleTag",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorUsername",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Articles",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ArticleId1",
                table: "Comment",
                column: "ArticleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AuthorUsername",
                table: "Articles",
                column: "AuthorUsername",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Articles_ArticleId1",
                table: "Comment",
                column: "ArticleId1",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_AuthorUsername",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Articles_ArticleId1",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ArticleId1",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ArticleId1",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "ArticlesId",
                table: "ArticleTag",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorUsername",
                table: "Articles",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Articles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ArticleId",
                table: "Comment",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_AuthorUsername",
                table: "Articles",
                column: "AuthorUsername",
                principalTable: "Users",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Articles_ArticleId",
                table: "Comment",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
