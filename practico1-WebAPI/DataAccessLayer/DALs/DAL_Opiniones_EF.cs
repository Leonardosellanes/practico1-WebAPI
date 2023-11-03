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
    public class DAL_Opiniones_EF : IDAL_Opiniones
    {
        private DBContextCore _dbContext;

        public DAL_Opiniones_EF(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var opinion = _dbContext.Opiniones.FirstOrDefault(o => o.Id == id);

            if (opinion != null)
            {
                _dbContext.Opiniones.Remove(opinion);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"No se encontró una opinión con el ID {id}");
            }
        }


        public List<Opinion> Get()
        {
            return _dbContext.Opiniones
                             .Include(p => p.ProductoAsociados)
                             .Select(p => new Opinion 
                             { 
                                 Titulo = p.Titulo, 
                                 Descripcion = p.Descripcion, 
                                 Id = p.Id, 
                                 Estrellas = p.Estrellas, 
                                 ProductoId = p.ProductoId,
                                 ProductoAsociado =  new Producto
                                 {
                                     Id = p.ProductoAsociados.Id,
                                     Titulo = p.ProductoAsociados.Titulo,
                                     Descripcion = p.ProductoAsociados.Descripcion,
                                     Foto = p.ProductoAsociados.Foto,
                                     Precio = p.ProductoAsociados.Precio,
                                     Tipo_iva = p.ProductoAsociados.Tipo_iva,
                                     EmpresaId = p.ProductoAsociados.EmpresaId,
                                     CategoriaId = p.ProductoAsociados.CategoriaId

                                 }
                             })
                                
                             .ToList();
        }

        public Opinion Get(int id)
        {
            var opinion = _dbContext.Opiniones
                .Include(p => p.ProductoAsociados)
                .FirstOrDefault(o => o.Id == id);

            return opinion == null
                ? throw new Exception($"No se encontró una opinión con el ID {id}")
                : new Opinion
                {
                    Id = opinion.Id,
                    Titulo = opinion.Titulo,
                    Descripcion = opinion.Descripcion,
                    Estrellas = opinion.Estrellas,
                    ProductoId = opinion.ProductoId,
                    ProductoAsociado = new Producto
                    {
                        Id = opinion.ProductoAsociados.Id,
                        Titulo = opinion.ProductoAsociados.Titulo,
                        Descripcion = opinion.ProductoAsociados.Descripcion,
                        Foto = opinion.ProductoAsociados.Foto,
                        Precio = opinion.ProductoAsociados.Precio,
                        Tipo_iva = opinion.ProductoAsociados.Tipo_iva,
                        EmpresaId = opinion.ProductoAsociados.EmpresaId,
                        CategoriaId = opinion.ProductoAsociados.CategoriaId

                    }
                };
        }


        public void Insert(Opinion opinion)
        {
            _dbContext.Opiniones.Add(new Opiniones{ Titulo = opinion.Titulo, Descripcion = opinion.Descripcion, ProductoId = opinion.ProductoId, Estrellas = opinion.Estrellas});
            _dbContext.SaveChanges();
        }

        public void Update(Opinion opinion)
        {
            var existingOpinion = _dbContext.Opiniones.FirstOrDefault(o => o.Id == opinion.Id);

            if (existingOpinion != null)
            {
                existingOpinion.Titulo = opinion.Titulo;
                existingOpinion.Descripcion = opinion.Descripcion;
                existingOpinion.Estrellas = opinion.Estrellas;

                _dbContext.SaveChanges();
            }
        }
    }
}
