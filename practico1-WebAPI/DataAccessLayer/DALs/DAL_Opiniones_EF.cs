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

            _dbContext.Opiniones.Remove(opinion);
            _dbContext.SaveChanges();
        }

        public List<Opinion> Get()
        {
            return _dbContext.Opiniones
                             .Select(p => new Opinion { Titulo = p.Titulo, Descripcion = p.Descripcion, Id = p.Id, Estrellas = p.Estrellas, ProductoId = p.ProductoId})
                             .ToList();
        }

        public Opinion Get(int id)
        {
            var opinion = _dbContext.Opiniones.FirstOrDefault(o => o.Id == id);
            return new Opinion
            {
                Id = opinion.Id,
                Titulo = opinion.Titulo,
                Descripcion = opinion.Descripcion,
                Estrellas = opinion.Estrellas,
                ProductoId = opinion.ProductoId
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
