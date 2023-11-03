using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Version_13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresasId",
                table: "Categorias",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "empresaId",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_EmpresasId",
                table: "Categorias",
                column: "EmpresasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Empresas_EmpresasId",
                table: "Categorias",
                column: "EmpresasId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Empresas_EmpresasId",
                table: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_EmpresasId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "EmpresasId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "empresaId",
                table: "Categorias");
        }
    }
}
