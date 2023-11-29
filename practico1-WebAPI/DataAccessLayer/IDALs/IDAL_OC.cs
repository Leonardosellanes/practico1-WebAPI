using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_OC
    {
        Orden ObtenerOCPorId(long id);
        Orden obtenerCarrito(string clienteId);
        List<Orden> ObtenerOCPorUserId(string id);
        List<Orden> ObtenerOCPorEmpresaId(int id);
        void CrearOC(Orden orden);
        void ActualizarOC(Orden orden);
        void EliminarOC(long id);
    }
}

