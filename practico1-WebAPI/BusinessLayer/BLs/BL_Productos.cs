using BusinessLayer.IBLs;
using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Productos : IBL_Productos
    {
        private IDAL_Productos _productos;

        public BL_Productos(IDAL_Productos productos)
        {
            _productos = productos;
        }

        public List<Producto> Get()
        {
            return _productos.Get();
        }

        public Producto Get(int id)
        {
            return _productos.Get(id);
        }

        public void Insert(Producto producto)
        {
            _productos.Insert(producto);
        }

        public void Update(Producto producto)
        {
            _productos.Update(producto);
        }

        public void Delete(int id)
        {
            _productos.Delete(id);
        }
    }
}
