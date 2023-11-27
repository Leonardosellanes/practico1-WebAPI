using DataAccessLayer.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_CarritoProducto
    {
        Carrito ObtenerCarritoProductoPorId(long id);
        //List<Carrito> ObtenerTodosLosCarritoProductos();
        void AgregarCarritoProducto(Carrito carrito);
        void ActualizarCarritoProducto(Carrito carrito);
        void EliminarCarritoProducto(long id);
    }
}
