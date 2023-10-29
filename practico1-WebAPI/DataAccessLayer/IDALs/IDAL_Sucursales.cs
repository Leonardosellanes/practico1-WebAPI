using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Sucursales
    {
        Sucursal GetById(int id);
        List<Sucursal> GetAll();
        void Insert(Sucursal sucursal);
        void Update(Sucursal sucursal);
        void Delete(int id);
    }
}
