using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
//using DataAccessLayer.EFModels;
using Shared;

namespace BusinessLayer.IBLs
{
    public interface IBL_OC
    {
        Orden ObtenerOCPorId(long id);
        Orden obtenerCarrito(string clienteId);
        List<Orden> ObtenerTodasLasOcs();
        void CrearOC(Orden orden);
        void ActualizarOC(Orden orden);
        void EliminarOC(long id);
    }
}

