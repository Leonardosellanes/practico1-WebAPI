using DataAccessLayer;
using DataAccessLayer.EFModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI
{
    public class StartUp
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
                    async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
                    {
                        var existingRole = await roleManager.FindByNameAsync(roleName);

                        if (existingRole == null)
                        {
                            var newRole = new IdentityRole
                            {
                                Id = roleName,
                                Name = roleName,
                                NormalizedName = roleName.ToUpper(),
                                ConcurrencyStamp = roleName
                            };

                            await roleManager.CreateAsync(newRole);
                        }
                    }

                    // Llamar a la función para crear los roles
                    await CreateRoleIfNotExists(roleManager, "ADMIN");
                    await CreateRoleIfNotExists(roleManager, "MANAGER");
                    await CreateRoleIfNotExists(roleManager, "USER");
                    #endregion
                }
            }
        }

        
    }
}
