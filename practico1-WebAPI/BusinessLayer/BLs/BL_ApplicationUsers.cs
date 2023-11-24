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
        private readonly IDAL_ApplicationUsers _applicationUsers;

        public BL_ApplicationUsers(IDAL_ApplicationUsers applicationUsers)
        {
            _applicationUsers = applicationUsers ?? throw new ArgumentNullException(nameof(applicationUsers));
        }

        public List<ApplicationUser> Get()
        {
            try
            {
                return _applicationUsers.Get();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed.
                throw new ApplicationException("Error in BL_ApplicationUsers.Get", ex);
            }
        }

        public void Insert(ApplicationUser applicationUser)
        {
            try
            {
                _applicationUsers.Insert(applicationUser);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed.
                throw new ApplicationException("Error in BL_ApplicationUsers.Insert", ex);
            }
        }

        public void Update(ApplicationUser applicationUser)
        {
            try
            {
                _applicationUsers.Update(applicationUser);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed.
                throw new ApplicationException("Error in BL_ApplicationUsers.Update", ex);
            }
        }

        public void Delete(ApplicationUser applicationUser)
        {
            try
            {
                _applicationUsers.Delete(applicationUser);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed.
                throw new ApplicationException("Error in BL_ApplicationUsers.Delete", ex);
            }
        }
    }

}

    
