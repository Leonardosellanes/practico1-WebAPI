using DataAccessLayer.IDALs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EFModels;

namespace DataAccessLayer.DALs
{
    public class DAL_OC : IDAL_OC
    {
        private DBContextCore _dbContext;

        public DAL_OC(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public OC ObtenerOCPorId(long id)
        {
            return _dbContext.OC.Find(id);
        }

        public List<OC> ObtenerTodasLasOcs()
        {
            return _dbContext.OC.ToList();
        }

        public void CrearOC(OC orden)
        {
            _dbContext.OC.Add(orden);
            _dbContext.SaveChanges();
        }

        public void ActualizarOC(OC orden)
        {
            _dbContext.Entry(orden).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void EliminarOC(long id)
        {
            var orden = _dbContext.OC.Find(id);
            if (orden != null)
            {
                _dbContext.OC.Remove(orden);
                _dbContext.SaveChanges();
            }
        }
    }
}
