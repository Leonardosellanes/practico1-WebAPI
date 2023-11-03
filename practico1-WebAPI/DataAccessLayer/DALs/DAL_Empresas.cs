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
                        Precio = c.Precio,
                        Tipo_iva = c.Tipo_iva,
                        Pdf = c.Pdf,
                        EmpresaId = c.EmpresaId,
                        CategoriaId = c.CategoriaId
                    }).ToList()
                };
        }


        public List<Empresa> GetAll()
        {
            return _dbContext.Empresas
                             .Include(e => e.CategoriasAsociadas)
                             .Include(p => p.ProductosAsociados)
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
                                 }).ToList()
                             })
                             .ToList();
        }


        public void Insert(Empresa empresa)
        {
            _dbContext.Empresas.Add(new Empresas { Nombre = empresa.Nombre, RUT = empresa.RUT });
            _dbContext.SaveChanges();
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
    }
}
