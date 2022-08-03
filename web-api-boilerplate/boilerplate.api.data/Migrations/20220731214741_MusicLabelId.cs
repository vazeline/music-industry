using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace boilerplate.api.data.Migrations
{
    public partial class MusicLabelId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicLabelContacts_MusicLabels_MusicianLabelId",
                schema: "dbo",
                table: "MusicLabelContacts");

            migrationBuilder.RenameColumn(
                name: "MusicianLabelId",
                schema: "dbo",
                table: "MusicLabelContacts",
                newName: "MusicLabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicLabelContacts_MusicLabels_MusicLabelId",
                schema: "dbo",
                table: "MusicLabelContacts",
                column: "MusicLabelId",
                principalSchema: "dbo",
                principalTable: "MusicLabels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicLabelContacts_MusicLabels_MusicLabelId",
                schema: "dbo",
                table: "MusicLabelContacts");

            migrationBuilder.RenameColumn(
                name: "MusicLabelId",
                schema: "dbo",
                table: "MusicLabelContacts",
                newName: "MusicianLabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicLabelContacts_MusicLabels_MusicianLabelId",
                schema: "dbo",
                table: "MusicLabelContacts",
                column: "MusicianLabelId",
                principalSchema: "dbo",
                principalTable: "MusicLabels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
