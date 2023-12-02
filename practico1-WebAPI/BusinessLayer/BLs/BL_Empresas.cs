using BusinessLayer.IBLs;
using DataAccessLayer;
using DataAccessLayer.IDALs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Empresas : IBL_Empresas
    {
        private IDAL_Empresas _empresas;

        public BL_Empresas(IDAL_Empresas empresas)
        {
            _empresas = empresas;
        }

        public void Delete(int id)
        {
            _empresas.Delete(id);
        }

        public List<Empresa> Get()
        {
            return _empresas.GetAll();
        }

        public Empresa Get(int id)
        {
            return _empresas.GetById(id);
        }

        public void Insert(Shared.Empresa empresa)
        {
            _empresas.Insert(empresa);
        }

        public void Update(Empresa empresa)
        {
            _empresas.Update(empresa);
        }
    }
}
