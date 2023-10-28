using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Empresas
    {
        Empresa GetById(int id);
        List<Empresa> GetAll();
        void Insert(Empresa empresa);
        void Update(Empresa empresa);
        void Delete(int id);
    }
}
