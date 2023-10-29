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
    public class BL_Sucursales : IBL_Sucursales
    {
        private IDAL_Sucursales _sucursales;

        public BL_Sucursales(IDAL_Sucursales sucursales)
        {
            _sucursales = sucursales;
        }

        public List<Sucursal> Get()
        {
            return _sucursales.GetAll();
        }

        public Sucursal Get(int id)
        {
            return _sucursales.GetById(id);
        }

        public void Insert(Sucursal sucursal)
        {
            _sucursales.Insert(sucursal);
        }

        public void Update(Sucursal sucursal)
        {
            _sucursales.Update(sucursal);
        }

        public void Delete(int id)
        {
            _sucursales.Delete(id);
        }
    }
}
