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
    List<ApplicationUser> Get(int empresaId);
    void Insert(ApplicationUser applicationUser);
    void Update(ApplicationUser applicationUser);
    bool Delete(string userId); // Cambiado para aceptar un string (ID) y devolver un booleano
}
}