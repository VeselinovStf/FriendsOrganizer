using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendsOrganizer.Data.Migrations
{
    public partial class RelationsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgrammingLanguages_Friends_FriendId",
                table: "ProgrammingLanguages");

            migrationBuilder.DropIndex(
                name: "IX_ProgrammingLanguages_FriendId",
                table: "ProgrammingLanguages");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "ProgrammingLanguages");

            migrationBuilder.AddColumn<int>(
                name: "ProgrammingLanguageId",
                table: "Friends",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friends_ProgrammingLanguageId",
                table: "Friends",
                column: "ProgrammingLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_ProgrammingLanguages_ProgrammingLanguageId",
                table: "Friends",
                column: "ProgrammingLanguageId",
                principalTable: "ProgrammingLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_ProgrammingLanguages_ProgrammingLanguageId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_ProgrammingLanguageId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguageId",
                table: "Friends");

            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "ProgrammingLanguages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingLanguages_FriendId",
                table: "ProgrammingLanguages",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgrammingLanguages_Friends_FriendId",
                table: "ProgrammingLanguages",
                column: "FriendId",
                principalTable: "Friends",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
