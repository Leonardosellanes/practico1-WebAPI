using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_Empresas : IDAL_Empresas
    {
        private DBContextCore _dbContext;

        public DAL_Empresas(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public Empresa GetById(int id)
        {
            Empresas emp = _dbContext.Empresas
                .Include(e => e.CategoriasAsociadas)
                .Include(p => p.ProductosAsociados)
                .Include(s => s.SucursalesAsociadas)
                .Include(r => r.Reclamos)
                .FirstOrDefault(e => e.Id == id);

            return emp == null
                ? throw new Exception($"No se encontró una empresa con el ID {id}")
                : new Empresa
                {
                    Id = emp.Id,
                    Nombre = emp.Nombre,
                    RUT = emp.RUT,
                    Categorias = emp.CategoriasAsociadas.Select(c => new Categoria
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                    }).ToList(),
                    Productos = emp.ProductosAsociados.Select(c => new Producto
                    {
                        Id = c.Id,
                        Titulo = c.Titulo,
                        Descripcion = c.Descripcion,
                        Foto = c.Foto,
                        Base64 = GetImage(c.Foto),
                        Precio = c.Precio,
                        Tipo_iva = c.Tipo_iva,
                        Pdf = c.Pdf,
                        Base64pdf = GetPdf(c.Pdf),
                        EmpresaId = c.EmpresaId,
                        CategoriaId = c.CategoriaId
                    }).ToList(),
                    Sucursales = emp.SucursalesAsociadas.Select(s => new Sucursal
                    {
                        Id = s.Id,
                        Nombre = s.Nombre,
                        Ubicacion = s.Ubicacion,
                        TiempoEntrega = s.TiempoEntrega
                    }).ToList(),
                    Reclamos = emp.Reclamos.Select(s => new Reclamo
                    {
                        Id = s.Id,
                        Descripcion = s.Descripcion,
                        Fecha = s.Fecha,
                        EmpresaId = s.EmpresaId,
                        OCId = s.OCId,
                    }).ToList(),
                };
        }


        public List<Empresa> GetAll()
        {
            return _dbContext.Empresas
                             .Include(e => e.CategoriasAsociadas)
                             .Include(p => p.ProductosAsociados)
                             .Include(s => s.SucursalesAsociadas)
                             .Include(r => r.Reclamos)
                             .Select(e => new Empresa
                             {
                                 Id = e.Id,
                                 Nombre = e.Nombre,
                                 RUT = e.RUT,
                                 Categorias = e.CategoriasAsociadas.Select(c => new Categoria
                                 {
                                     Id = c.Id,
                                     Nombre = c.Nombre
                                 }).ToList(),
                                 Productos = e.ProductosAsociados.Select(c => new Producto
                                 {
                                     Id = c.Id,
                                     Titulo = c.Titulo,
                                     Descripcion = c.Descripcion,
                                     Foto = c.Foto,
                                     Precio = c.Precio,
                                     Tipo_iva = c.Tipo_iva,
                                     Pdf = c.Pdf,
                                     EmpresaId = c.EmpresaId,
                                     CategoriaId = c.CategoriaId
                                 }).ToList(),
                                 Sucursales = e.SucursalesAsociadas.Select(s => new Sucursal
                                 {
                                     Id = s.Id,
                                     Nombre = s.Nombre,
                                     Ubicacion = s.Ubicacion,
                                     TiempoEntrega = s.TiempoEntrega
                                 }).ToList(),
                                 Reclamos = e.Reclamos.Select(s => new Reclamo
                                 {
                                     Id = s.Id,
                                     Descripcion = s.Descripcion,
                                     Fecha = s.Fecha,
                                     EmpresaId = s.EmpresaId
                                 }).ToList(),
                             })
                             .ToList();
        }

        public Empresa Insert(Empresa empresa)
        {
            Empresas e = new Empresas{ Nombre = empresa.Nombre, RUT = empresa.RUT };
            _dbContext.Empresas.Add(e);
            _dbContext.SaveChanges();
            empresa.Id = e.Id;
            return empresa;
        }
        public void Update(Empresa empresa)
        {
            var emp = _dbContext.Empresas.FirstOrDefault(e => e.Id == empresa.Id);

            if (emp != null)
            {
                emp.Nombre = empresa.Nombre;
                emp.RUT = empresa.RUT;

                _dbContext.SaveChanges();
            }

        }

        public void Delete(int id)
        {
            var empresa = _dbContext.Empresas.FirstOrDefault(e => e.Id == id);
            if (empresa != null)
            {
                _dbContext.Empresas.Remove(empresa);
                _dbContext.SaveChanges();
            }
        }

        public List<string> Reportes(int empresaId)
        {
            List<string> list = new();
            try
            {
                string connectionString = "Server=sqlserver,1433;Database=Practico4;User Id=sa;Password=Abc*123!;Encrypt=False;"; // Reemplaza con tu cadena de conexión

                using SqlConnection connection = new(connectionString);
                connection.Open();

                // Consulta SQL
                string sqlQuery = @"
                SELECT ISNULL(SUM(total), 0) AS SumaTotal
                FROM OC
                WHERE EmpresaId = @EmpresaId
                  AND EstadoOrden != 'activo'
                  AND fecha BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE();";

                // Ejecuta la consulta y mapea el resultado a un array
                decimal resultado = connection.Query<decimal>(sqlQuery, new { EmpresaId = empresaId }).First();
                list.Add(resultado.ToString());

                string mesQuery = @"
                    SELECT ISNULL(SUM(total), 0) AS SumaTotal
                    FROM OC
                    WHERE EmpresaId = @EmpresaId
                    AND EstadoOrden != 'activo'
                    AND fecha BETWEEN DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0) AND DATEADD(DAY, 0, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0));";

                decimal resultadoMes = connection.Query<decimal>(mesQuery, new { EmpresaId = empresaId }).First();
                list.Add(resultadoMes.ToString());

                string medioDePagoQuery = @"
                    SELECT TOP 1 MedioDePago
                    FROM OC
                    WHERE EmpresaId = @EmpresaId
                    AND EstadoOrden != 'activo'
                    GROUP BY MedioDePago
                    ORDER BY COUNT(*) DESC;";

                // Ejecuta la consulta para contar MedioDePago y agrega el resultado al array
                string? medioDePagoMasComun = connection.Query<string>(medioDePagoQuery, new { EmpresaId = empresaId }).FirstOrDefault();
                list.Add(medioDePagoMasComun ?? "N/A"); // Si no hay resultados, agrega "N/A"
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el reporte: {ex.Message}");
            }
            return list;
        }

        public static string GetImage(string fileName)
        {
            string filePath = Path.Combine("Archivos", "Imagenes", fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    return Convert.ToBase64String(fileBytes);
                }
                catch (Exception ex)
                {
                    return $"Error al leer el archivo: {ex.Message}";
                }
            }
            else
            {
                return "El archivo no fue encontrado.";
            }
        }

        public static string GetPdf(string fileName)
        {
            string filePath = Path.Combine("Archivos", "Pdf", fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    return Convert.ToBase64String(fileBytes);
                }
                catch (Exception ex)
                {
                    return $"Error al leer el archivo: {ex.Message}";
                }
            }
            else
            {
                return "El archivo no fue encontrado.";
            }
        }
    }
}
