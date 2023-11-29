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

    //Roles y empresaId
    public List<ApplicationUser> Get(int empresaId)
    {
        var usersWithRole = from user in _dbContext.Users
                            join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                            join role in _dbContext.Roles on userRole.RoleId equals role.Id
                            where role.Name == "MANAGER" && user.EmpresaId == empresaId
                            select user;

        return usersWithRole.ToList();
    }


    public ApplicationUser GetById(string userId)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
    }

    public ApplicationUser GetById(string userId)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
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


