using BusinessLayer.IBLs;
using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_ApplicationUsers : IBL_ApplicationUsers
    {
        private IDAL_ApplicationUsers _ApplicationUsers;

        public BL_ApplicationUsers(IDAL_ApplicationUsers ApplicationUsers)
        {
            _ApplicationUsers = ApplicationUsers;
        }


        public List<ApplicationUser> Get()
        {
            return _ApplicationUsers.Get();
        }

        public void Insert(ApplicationUser ApplicationUsers)
        {
            _ApplicationUsers.Insert(ApplicationUsers);
        }

        public void Update(ApplicationUser ApplicationUsers)
        {
            _ApplicationUsers.Update(ApplicationUsers);
        }

        public void Delete(ApplicationUser ApplicationUsers)
        {
            _ApplicationUsers.Delete(ApplicationUsers);
        }
    }
}

