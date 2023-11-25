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
    public class DAL_Productos_EF : IDAL_Productos
    {
        private DBContextCore _dbContext;

        public DAL_Productos_EF(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var producto = _dbContext.Productos.FirstOrDefault(p => p.Id == id);

            if (producto != null)
            {
                
                _dbContext.Productos.Remove(producto);
                _dbContext.SaveChanges();
            }
            else
            {
                
                throw new Exception($"No se encontró un producto con el ID {id}");
            }
        }

        public List<Producto> Get()
        {
            return _dbContext.Productos
                             .Include(c => c.CategoriaAsociada)
                             .Include(o => o.OpinionesAsociadas)
                             .Select(
                                p => new Producto
                                {
                                    Id = p.Id,
                                    Titulo = p.Titulo,
                                    Descripcion = p.Descripcion,
                                    Foto = p.Foto,
                                    Base64 = GetImage(p.Foto),
                                    Precio = p.Precio,
                                    Tipo_iva = p.Tipo_iva,
                                    Pdf = p.Pdf,
                                    Base64pdf = GetPdf(p.Pdf),
                                    EmpresaId = p.EmpresaId,
                                    CategoriaId = p.CategoriaId,
                                    Categoria = new Categoria
                                    {
                                        Id = p.CategoriaAsociada.Id,
                                        Nombre = p.CategoriaAsociada.Nombre
                                    },
                                    OpinionesAsociadas = p.OpinionesAsociadas.Select(o => new Opinion
                                    {
                                        Titulo = o.Titulo,
                                        Descripcion = o.Descripcion,
                                        Id = o.Id,
                                        Estrellas = o.Estrellas
                                    }).ToList()
                                })
                             .ToList();
        }

        public Producto Get(int id)
        {
            Productos producto = _dbContext.Productos
                .Include(c => c.CategoriaAsociada)
                .Include(o => o.OpinionesAsociadas)
                .FirstOrDefault(p => p.Id == id);

            return producto == null
                ? throw new Exception($"No se encontró un producto con el ID {id}")
                : new Producto
                {
                    Id = producto.Id,
                    Titulo = producto.Titulo,
                    Descripcion = producto.Descripcion,
                    Foto = producto.Foto,
                    Base64 = GetImage(producto.Foto),
                    Precio = producto.Precio,
                    Tipo_iva = producto.Tipo_iva,
                    Pdf = producto.Pdf,
                    Base64pdf = GetPdf(producto.Pdf),
                    EmpresaId = producto.EmpresaId,
                    CategoriaId = producto.CategoriaId,
                    Categoria = new Categoria
                    {
                        Id = producto.CategoriaAsociada.Id,
                        Nombre = producto.CategoriaAsociada.Nombre
                    },
                    OpinionesAsociadas = producto.OpinionesAsociadas.Select(o => new Opinion
                    {
                        Titulo = o.Titulo,
                        Descripcion = o.Descripcion,
                        Id = o.Id,
                        Estrellas = o.Estrellas
                    }).ToList()
                };
        }


        public void Insert(Producto producto)
        {
            if (producto != null)
            {
                _dbContext.Productos.Add(new Productos()
                {

                    Id = producto.Id,
                    Titulo = producto.Titulo,
                    Descripcion = producto.Descripcion,
                    Foto = producto.Foto,
                    Precio = producto.Precio,
                    Tipo_iva = producto.Tipo_iva,
                    EmpresaId = producto.EmpresaId,
                    CategoriaId = producto.CategoriaId,
                    Pdf = producto.Pdf
                });
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("producto", "El producto no puede ser nulo.");
            }
        }

        public void Update(Producto producto)
        {
            if (producto != null)
            {
                
                var productoExistente = _dbContext.Productos.FirstOrDefault(p => p.Id == producto.Id);

                if (productoExistente != null)
                {
                    
                    productoExistente.Titulo = producto.Titulo;
                    productoExistente.Descripcion = producto.Descripcion;
                    productoExistente.Foto = producto.Foto;
                    productoExistente.Precio = producto.Precio;
                    productoExistente.Tipo_iva = producto.Tipo_iva;
                    productoExistente.Pdf = producto.Pdf;

                    if (producto.CategoriaId != 0 && productoExistente.CategoriaId != 0)
                    {
                        if (producto.CategoriaId != productoExistente.CategoriaId)
                        {
                            productoExistente.CategoriaId = producto.CategoriaId;
                        }
                    }
                    else if (producto.CategoriaId != 0 && productoExistente.CategoriaId == 0)
                    {
                        productoExistente.CategoriaId = producto.CategoriaId;
                    }

                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception($"No se encontró un producto con el ID {producto.Id}");
                }
            }
            else
            {
                throw new ArgumentNullException("producto", "El producto no puede ser nulo.");
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
