using DataAccessLayer.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_OC
    {
        OC ObtenerOCPorId(long id);
        List<OC> ObtenerTodasLasOcs();
        void CrearOC(OC orden);
        void ActualizarOC(OC orden);
        void EliminarOC(long id);
    }
}

