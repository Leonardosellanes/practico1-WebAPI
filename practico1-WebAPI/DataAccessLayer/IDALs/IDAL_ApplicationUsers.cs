using DataAccessLayer.EFModels;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.IDALs
{
    public interface IDAL_ApplicationUsers
    {
        void Delete(ApplicationUser applicationUsers);
        List<ApplicationUser> Get(int empresaId);
        ApplicationUser GetById(string userId);
        void Insert(ApplicationUser applicationUsers);
        void Update(ApplicationUser applicationUsers);
    }
}
