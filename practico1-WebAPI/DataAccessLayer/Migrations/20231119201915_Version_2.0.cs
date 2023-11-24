using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Version_20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Opiniones",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Empresas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", maxLength: 128, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reclamos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reclamos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reclamos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OC",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedioDePago = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DireccionDeEnvio = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FechaEstimadaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstadoOrden = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReclamoId = table.Column<int>(type: "int", nullable: true),
                    FacturaId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OC_ApplicationUser_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OC_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OC_Reclamos_ReclamoId",
                        column: x => x.ReclamoId,
                        principalTable: "Reclamos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarritoProducto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    OCId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarritoProducto_OC_OCId",
                        column: x => x.OCId,
                        principalTable: "OC",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarritoProducto_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opiniones_ApplicationUserId",
                table: "Opiniones",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_ApplicationUserId",
                table: "Empresas",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoProducto_OCId",
                table: "CarritoProducto",
                column: "OCId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoProducto_ProductoId",
                table: "CarritoProducto",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_OC_ClienteId",
                table: "OC",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_OC_FacturaId",
                table: "OC",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_OC_ReclamoId",
                table: "OC",
                column: "ReclamoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reclamos_EmpresaId",
                table: "Reclamos",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_ApplicationUser_ApplicationUserId",
                table: "Empresas",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Opiniones_ApplicationUser_ApplicationUserId",
                table: "Opiniones",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_ApplicationUser_ApplicationUserId",
                table: "Empresas");

            migrationBuilder.DropForeignKey(
                name: "FK_Opiniones_ApplicationUser_ApplicationUserId",
                table: "Opiniones");

            migrationBuilder.DropTable(
                name: "CarritoProducto");

            migrationBuilder.DropTable(
                name: "OC");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "Reclamos");

            migrationBuilder.DropIndex(
                name: "IX_Opiniones_ApplicationUserId",
                table: "Opiniones");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_ApplicationUserId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Opiniones");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Empresas");
        }
    }
}
