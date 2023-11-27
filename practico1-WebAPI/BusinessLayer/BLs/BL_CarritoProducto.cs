using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using DataAccessLayer.EFModels;

namespace BusinessLayer.BLs
{
    public class BL_CarritoProducto : IBL_CarritoProducto
    {
        private IDAL_CarritoProducto _carrito;

        public BL_CarritoProducto (IDAL_CarritoProducto carrito)
        {
            _carrito = carrito;
        }

        public void Delete(int id)
        {
            _carrito.EliminarCarritoProducto(id);
        }

        public void Insert(Carrito carrito)
        {
            _carrito.AgregarCarritoProducto(carrito);
        }

        public void Update(Carrito carrito)
        {
            _carrito.ActualizarCarritoProducto(carrito);
        }

        public Carrito Get(int id)
        {
            return _carrito.ObtenerCarritoProductoPorId(id);
        }
    }
}
