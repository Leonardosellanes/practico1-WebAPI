using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace DataAccessLayer.DALs
{
    public class DAL_CarritoProducto : IDAL_CarritoProducto
    {
        private DBContextCore _dbContext;

        public DAL_CarritoProducto(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public Carrito ObtenerCarritoProductoPorId(long id)
        {
            CarritoProducto x = _dbContext.CarritoProducto
                .Include(p => p.POs)
                .FirstOrDefault(x => x.Id == id);

            if (x != null)
            {
                return new Carrito
                {
                    Id = x.Id,
                    Cantidad = x.Cantidad,
                    ProductoId = x.ProductoId,
                    POs = new Producto
                    {
                        Id = x.POs.Id,
                        Titulo = x.POs.Titulo,
                        Descripcion = x.POs.Descripcion,
                        Foto = x.POs.Foto,
                        Precio = x.POs.Precio,
                        Tipo_iva = x.POs.Tipo_iva,
                        EmpresaId = x.POs.EmpresaId,
                        CategoriaId = x.POs.CategoriaId
                    },
                    OCId = x.OCId
                };
            }
            else
            {

                return null;
            }
        }

        /*public List<CarritoProducto> ObtenerTodosLosCarritoProductos()
        {
            return _dbContext.CarritoProducto.ToList();
        }*/

        public void AgregarCarritoProducto(Carrito carrito)
        {
            OC o = _dbContext.OC.Include(c => c.Cliente).FirstOrDefault(x => x.Id == carrito.OCId);


            Productos p = _dbContext.Productos.Find(carrito.ProductoId);
            if (o != null && p != null) { 
                CarritoProducto c = new CarritoProducto 
                { 
                    Id = carrito.Id,
                    Cantidad = carrito.Cantidad,
                    ProductoId = carrito.ProductoId,
                    OCId = carrito.OCId,
                    OCs = o,
                    POs = p
                };

                _dbContext.CarritoProducto.Add(c);
                _dbContext.SaveChanges();
            }
        }

        public void ActualizarCarritoProducto(Carrito carrito)
        {
            CarritoProducto c = _dbContext.CarritoProducto.FirstOrDefault(c => c.Id == carrito.Id);

            if (c != null)
            {
                c.Cantidad = carrito.Cantidad;
                c.ProductoId = carrito.ProductoId;
                c.OCId = carrito.OCId;

                _dbContext.SaveChanges();
            }
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