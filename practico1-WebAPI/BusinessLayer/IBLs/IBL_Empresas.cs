using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Empresas
    {
        List<Empresa> Get();

        Empresa Get(int id);

        Task<int> InsertAsync(Empresa empresa);

        void Update(Empresa empresa);

        void Delete(int id);
    }
}
