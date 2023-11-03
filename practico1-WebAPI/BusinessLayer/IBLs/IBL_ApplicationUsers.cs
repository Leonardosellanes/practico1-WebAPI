using DataAccessLayer.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_ApplicationUsers
    {
        List<ApplicationUser> Get();

        void Insert(ApplicationUser ApplicationUsers);

        void Update(ApplicationUser ApplicationUsers);

        void Delete(ApplicationUser ApplicationUsers);
    }
}
