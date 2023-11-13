using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Reclamos : IBL_Reclamos
    {
        private IDAL_Reclamos _reclamos;

        public BL_Reclamos(IDAL_Reclamos reclamos)
        {
            _reclamos = reclamos;
        }
        public void Delete(int id)
        {
            _reclamos.Delete(id);
        }

        public List<Reclamo> Get()
        {
            return _reclamos.Get();
        }

        public Reclamo Get(int id)
        {
            return _reclamos.Get(id);
        }

        public void Insert(Reclamo reclamo)
        {
            _reclamos.Insert(reclamo);
        }

        public void Update(Reclamo reclamo)
        {
            _reclamos.Update(reclamo);
        }
    }
}
