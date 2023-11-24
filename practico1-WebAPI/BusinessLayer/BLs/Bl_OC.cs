using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.IBLs;
using DataAccessLayer.DALs;
using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;

namespace BusinessLayer.BLs
{
    public class BL_OC : IBL_OC
    {
        private readonly IDAL_OC _dalOC;

        public BL_OC(IDAL_OC dalOC)
        {
            _dalOC = dalOC ?? throw new ArgumentNullException(nameof(dalOC));
        }

        public OC ObtenerOCPorId(long id)
        {
            return _dalOC.ObtenerOCPorId(id);
        }

        public List<OC> ObtenerTodasLasOcs()
        {
            return _dalOC.ObtenerTodasLasOcs();
        }

        public void CrearOC(OC orden)
        {
            _dalOC.CrearOC(orden);
        }

        public void ActualizarOC(OC orden)
        {
            _dalOC.ActualizarOC(orden);
        }

        public void EliminarOC(long id)
        {
            _dalOC.EliminarOC(id);
        }
    }
}

