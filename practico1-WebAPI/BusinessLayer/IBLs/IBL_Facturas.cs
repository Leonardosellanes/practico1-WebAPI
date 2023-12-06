using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Facturas
    {
        List<Factura> Get();

        Factura Get(int id);

        public Factura Generar(int id);

        void Insert(Factura factura);

        void Update(Factura factura);

        void Delete(int id);
    }
}
