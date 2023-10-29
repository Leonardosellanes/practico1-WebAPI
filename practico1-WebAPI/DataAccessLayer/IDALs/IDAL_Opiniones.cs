using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Opiniones
    {
        List<Opinion> Get();

        Opinion Get(int id);

        void Insert(Opinion opinion);

        void Update(Opinion opinion);

        void Delete(int id);
    }
}
