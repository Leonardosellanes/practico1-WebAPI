using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Facturas : IBL_Facturas
    {
        private IDAL_Facturas _facturas;

        public BL_Facturas(IDAL_Facturas facturas)
        {
            _facturas = facturas;
        }
        public void Delete(int id)
        {
            _facturas.Delete(id);
        }

        public List<Factura> Get()
        {
            return _facturas.GetAll();
        }

        public Factura Get(int id)
        {
            return _facturas.GetById(id);
        }

        public Factura Generar(int id)
        {
            return _facturas.Generar(id);
        }

        public void Insert(Factura factura)
        {
            _facturas.Insert(factura);
        }

        public void Update(Factura factura)
        {
            _facturas.Update(factura);
        }
    }
}
