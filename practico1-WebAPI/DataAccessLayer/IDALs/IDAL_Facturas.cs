using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Facturas
    {
        Factura GetById(int id);
        Factura Generar(int id);
        List<Factura> GetAll();
        void Insert(Factura factura);
        void Update(Factura factura);
        void Delete(int id);
    }
}
