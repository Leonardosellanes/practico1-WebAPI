using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_CarritoProducto : IDAL_CarritoProducto
    {
        private DBContextCore _dbContext;

        public DAL_CarritoProducto(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public CarritoProducto ObtenerCarritoProductoPorId(long id)
        {
            return _dbContext.CarritoProducto.Find(id);
        }

        public List<CarritoProducto> ObtenerTodosLosCarritoProductos()
        {
            return _dbContext.CarritoProducto.ToList();
        }

        public void AgregarCarritoProducto(CarritoProducto carritoProducto)
        {
            _dbContext.CarritoProducto.Add(carritoProducto);
            _dbContext.SaveChanges();
        }

        public void ActualizarCarritoProducto(CarritoProducto carritoProducto)
        {
            _dbContext.Entry(carritoProducto).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void EliminarCarritoProducto(long id)
        {
            var carritoProducto = _dbContext.CarritoProducto.Find(id);
            if (carritoProducto != null)
            {
                _dbContext.CarritoProducto.Remove(carritoProducto);
                _dbContext.SaveChanges();
            }
        }
    }
}