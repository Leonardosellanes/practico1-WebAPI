using DataAccessLayer.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_CarritoProducto
    {
        CarritoProducto ObtenerCarritoProductoPorId(long id);
        List<CarritoProducto> ObtenerTodosLosCarritoProductos();
        void AgregarCarritoProducto(CarritoProducto carritoProducto);
        void ActualizarCarritoProducto(CarritoProducto carritoProducto);
        void EliminarCarritoProducto(long id);
    }
}
