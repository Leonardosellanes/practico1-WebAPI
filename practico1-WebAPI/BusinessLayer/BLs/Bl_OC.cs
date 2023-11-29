using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.IBLs;
using DataAccessLayer.DALs;
//using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Shared;

namespace BusinessLayer.BLs
{
    public class BL_OC : IBL_OC
    {
        private readonly IDAL_OC _dalOC;

        public BL_OC(IDAL_OC dalOC)
        {
            _dalOC = dalOC ?? throw new ArgumentNullException(nameof(dalOC));
        }

        public Orden ObtenerOCPorId(long id)
        {
            return _dalOC.ObtenerOCPorId(id);
        }
        public Orden obtenerCarrito(string clienteId)
        {
            return _dalOC.obtenerCarrito(clienteId);
        }
        public List<Orden> ObtenerOCPorUserId(string id)
        {
            return _dalOC.ObtenerOCPorUserId(id);
        }
        public List<Orden> ObtenerOCPorEmpresaId(int id)
        {
            return _dalOC.ObtenerOCPorEmpresaId(id);
        }

        public void CrearOC(Orden orden)
        {
            _dalOC.CrearOC(orden);
        }

        public void ActualizarOC(Orden orden)
        {
            _dalOC.ActualizarOC(orden);
        }

        public void EliminarOC(long id)
        {
            _dalOC.EliminarOC(id);
        }
    }
}

