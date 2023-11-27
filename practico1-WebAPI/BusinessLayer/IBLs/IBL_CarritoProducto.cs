using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_CarritoProducto
    {
        public void Delete(int id);
        public void Insert(Carrito carrito);
        public void Update(Carrito carrito);
        public Carrito Get(int id);
    }
}
