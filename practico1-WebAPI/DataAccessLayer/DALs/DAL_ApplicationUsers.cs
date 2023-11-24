using DataAccessLayer;
using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DAL_ApplicationUsers : IDAL_ApplicationUsers
{
    private readonly DBContextCore _dbContext;

    public DAL_ApplicationUsers(DBContextCore dbContext)
    {
        _dbContext = dbContext;
    }

    public void Delete(ApplicationUser applicationUser)
    {
        _dbContext.Users.Remove(applicationUser);
        _dbContext.SaveChanges();
    }

    public List<ApplicationUser> Get()
    {
        return _dbContext.Users.ToList();
    }

    public void Insert(ApplicationUser applicationUser)
    {
        _dbContext.Users.Add(applicationUser);
        _dbContext.SaveChanges();
    }

    public void Update(ApplicationUser applicationUser)
    {
        _dbContext.Users.Update(applicationUser);
        _dbContext.SaveChanges();
    }
}

