using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Equipos.Migrations
{
    /// <inheritdoc />
    public partial class TablasRelacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Jugadores_ClubID",
                table: "Jugadores",
                column: "ClubID");

            migrationBuilder.AddForeignKey(
                name: "FK_Jugadores_Clubes_ClubID",
                table: "Jugadores",
                column: "ClubID",
                principalTable: "Clubes",
                principalColumn: "ClubID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jugadores_Clubes_ClubID",
                table: "Jugadores");

            migrationBuilder.DropIndex(
                name: "IX_Jugadores_ClubID",
                table: "Jugadores");
        }
    }
}
