using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_Categorias_EF : IDAL_Categorias
    {
        private DBContextCore _dbContext;

        public DAL_Categorias_EF(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var categoria = _dbContext.Categorias
                .Include(p => p.ProductosAsociados)
                .FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                throw new Exception($"No se encontró una categoría con el ID {categoria.Id}");
            }

            //if (categoria.ProductosAsociados != null)
            //{
            //    throw new Exception("No se puede eliminar una categoria con productos asociados");
            //}

            _dbContext.Categorias.Remove(categoria);
            _dbContext.SaveChanges();
    
        }


        public List<Categoria> Get(int empresaId)
        {
            return _dbContext.Categorias
                .Include(p => p.ProductosAsociados)
                .Where(x => x.EmpresaId == empresaId)
                .Select(c => new Categoria
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    CategoriaId = c.CategoriaId,
                    EmpresaId = c.EmpresaId,
                    CategoriaAsociada = c.CategoriaAsociada != null ? new Categoria
                    {
                        Id = c.CategoriaAsociada.Id,
                        Nombre = c.CategoriaAsociada.Nombre,
                        EmpresaId = c.EmpresaId,
                        CategoriaId = c.CategoriaAsociada.CategoriaId
                    } : null,
                    Productos = c.ProductosAsociados.Select(c => new Producto
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
                    }).ToList()
                })
                .ToList();
        }

        public Categoria GetbyId(int id)
        {
            Categorias x = _dbContext.Categorias
                .Include(p => p.ProductosAsociados)
                .Include(a => a.CategoriaAsociada)
                .FirstOrDefault(x => x.Id == id);

            if (x != null)
            {
                return new Categoria
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    CategoriaId = x.CategoriaId,
                    EmpresaId = x.EmpresaId,
                    CategoriaAsociada = x.CategoriaAsociada != null ? new Categoria
                    {
                        Id = x.CategoriaAsociada.Id,
                        Nombre = x.CategoriaAsociada.Nombre,
                        EmpresaId = x.EmpresaId,
                        CategoriaId = x.CategoriaAsociada.CategoriaId
                    } : null,
                    Productos = x.ProductosAsociados.Select(c => new Producto
                    {
                        Id = c.Id,
                        Titulo = c.Titulo,
                        Descripcion = c.Descripcion,
                        Foto = c.Foto,
                        Base64 = GetImage(c.Foto),
                        Precio = c.Precio,
                        Tipo_iva = c.Tipo_iva,
                        Pdf = c.Pdf,
                        EmpresaId = c.EmpresaId,
                        CategoriaId = c.CategoriaId
                    }).ToList()
                };
            }
            else {  
            
                return null;
            }
        }


        public void Insert(Categoria categoria)
        {
            if (categoria != null)
            {
                // Crear una instancia de Categorias a partir de la Categoria
                Categorias categoriaEntity = new Categorias
                {
                    Id = categoria.Id,
                    Nombre = categoria.Nombre,
                    CategoriaId = categoria.CategoriaId,
                    EmpresaId = categoria.EmpresaId,
                };

                _dbContext.Categorias.Add(categoriaEntity);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("categoria", "La categoría no puede ser nula.");
            }
        }

        public void Update(Categoria categoria)
        {
            if (categoria != null)
            {
                
                var categoriaExistente = _dbContext.Categorias.FirstOrDefault(c => c.Id == categoria.Id);

                if (categoriaExistente != null)
                {
                   
                    categoriaExistente.Nombre = categoria.Nombre;
                    categoriaExistente.CategoriaId = categoria.CategoriaId;
                    
                    _dbContext.SaveChanges();
                }
                else
                {
                   
                    throw new Exception($"No se encontró una categoría con el ID {categoria.Id}");
                }
            }
            else
            {
               
                throw new ArgumentNullException("categoria", "La categoría no puede ser nula.");
            }
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
