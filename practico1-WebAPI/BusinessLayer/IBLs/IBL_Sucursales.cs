using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Sucursales
    {
        List<Sucursal> Get();

        Sucursal Get(int id);

        void Insert(Sucursal sucursal);

        void Update(Sucursal sucursal);

        void Delete(int id);
    }
}
