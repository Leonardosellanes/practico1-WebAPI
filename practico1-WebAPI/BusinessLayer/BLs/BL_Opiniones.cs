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
    public class BL_Opiniones : IBL_Opiniones
    {
        private IDAL_Opiniones _opiniones;

            public BL_Opiniones(IDAL_Opiniones opiniones)
        {
            _opiniones = opiniones;
        }


        public void Delete(int id)
        {
            _opiniones.Delete(id);
        }

        public List<Opinion> Get()
        {
            return _opiniones.Get();
        }

        public Opinion Get(int id)
        {
            return _opiniones.Get(id);
        }

        public void Insert(Opinion opinion)
        {
            _opiniones.Insert(opinion);
        }

        public void Update(Opinion opinion)
        {
            _opiniones.Update(opinion);
        }
    }


}
