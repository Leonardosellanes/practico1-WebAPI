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
        void Insert(Empresa empresa);
        List<Empresa> GetAll();
        void Update(Empresa empresa);
        void Delete(int id);
    }
}
