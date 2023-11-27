using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Version_40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_AspNetUsers_ApplicationUserId",
                table: "Empresas");

            migrationBuilder.DropForeignKey(
                name: "FK_Opiniones_AspNetUsers_ApplicationUserId",
                table: "Opiniones");

            migrationBuilder.DropIndex(
                name: "IX_OC_ReclamoId",
                table: "OC");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_ApplicationUserId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Empresas");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Opiniones",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Opiniones_ApplicationUserId",
                table: "Opiniones",
                newName: "IX_Opiniones_ClienteId");

            migrationBuilder.AddColumn<long>(
                name: "OCId",
                table: "Reclamos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmpresasId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reclamos_OCId",
                table: "Reclamos",
                column: "OCId");

            migrationBuilder.CreateIndex(
                name: "IX_OC_ReclamoId",
                table: "OC",
                column: "ReclamoId",
                unique: true,
                filter: "[ReclamoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmpresasId",
                table: "AspNetUsers",
                column: "EmpresasId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresasId",
                table: "AspNetUsers",
                column: "EmpresasId",
                principalTable: "Empresas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Opiniones_AspNetUsers_ClienteId",
                table: "Opiniones",
                column: "ClienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamos_OC_OCId",
                table: "Reclamos",
                column: "OCId",
                principalTable: "OC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresasId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Opiniones_AspNetUsers_ClienteId",
                table: "Opiniones");

            migrationBuilder.DropForeignKey(
                name: "FK_Reclamos_OC_OCId",
                table: "Reclamos");

            migrationBuilder.DropIndex(
                name: "IX_Reclamos_OCId",
                table: "Reclamos");

            migrationBuilder.DropIndex(
                name: "IX_OC_ReclamoId",
                table: "OC");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmpresasId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OCId",
                table: "Reclamos");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmpresasId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Opiniones",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Opiniones_ClienteId",
                table: "Opiniones",
                newName: "IX_Opiniones_ApplicationUserId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Empresas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OC_ReclamoId",
                table: "OC",
                column: "ReclamoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_ApplicationUserId",
                table: "Empresas",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_AspNetUsers_ApplicationUserId",
                table: "Empresas",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Opiniones_AspNetUsers_ApplicationUserId",
                table: "Opiniones",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
