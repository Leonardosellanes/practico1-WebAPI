using DataAccessLayer;
using DataAccessLayer.EFModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public static class StartUp
    {
        internal static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DBContextCore>())
                {
                        context?.Database.Migrate();
                }
            }
        }

        internal static async void InitializeDatabase(IApplicationBuilder app) 
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var dbContext = serviceScope.ServiceProvider.GetService<DBContextCore>())
                using (var userManager = serviceScope.ServiceProvider.GetService<UserManager<Usuarios>>())
                using (var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>())
                {
                    #region Agregamos Los Roles Por defecto
                    //var userRole = await roleManager.FindByIdAsync("ADMIN");
                    //if (userRole == null)
                    //{
                    //    IdentityRole identityRole = new IdentityRole();
                    //    identityRole = new IdentityRole();
                    //    identityRole.Id = "ADMIN";
                    //    identityRole.NormalizedName = "ADMIN";
                    //    identityRole.Name = "ADMIN";
                    //    identityRole.ConcurrencyStamp = "ADMIN";
                    //    await roleManager.CreateAsync(identityRole);
                    //}

                    //userRole = await roleManager.FindByIdAsync("OTRO");
                    //if (userRole == null)
                    //{
                    //    IdentityRole identityRole = new IdentityRole();
                    //    identityRole = new IdentityRole();
                    //    identityRole.Id = "OTRO";
                    //    identityRole.NormalizedName = "OTRO";
                    //    identityRole.Name = "OTRO";
                    //    identityRole.ConcurrencyStamp = "OTRO";
                    //    await roleManager.CreateAsync(identityRole);
                    //}

                    #endregion

                    #region Agregamos el usuario admin por defecto.

                    // Agregamos el usuario administrador por defecto si no exsite.
                    //var userAdmin = await userManager.FindByEmailAsync("admin@admin.com");
                    //if (userAdmin == null)
                    //{
                    //    Usuarios user = new Usuarios()
                    //    {
                    //        Email = "admin@admin.com",
                    //        SecurityStamp = Guid.NewGuid().ToString(),
                    //        UserName = "admin"
                    //    };

                    //    var result = await userManager.CreateAsync(user, "Abc*123!");

                    //    // Asignar Rol ADMIN
                    //    await userManager.AddToRoleAsync(user, "ADMIN");
                    //}

                    #endregion
                }
            }
        }
    }
}
