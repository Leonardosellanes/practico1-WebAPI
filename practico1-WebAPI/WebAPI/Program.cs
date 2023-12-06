using BusinessLayer.BLs;
using BusinessLayer.IBLs;
using DataAccessLayer;
using DataAccessLayer.DALs;
using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI;
using WebAPI.Models;

Console.WriteLine("################################################################");
Console.WriteLine("# Init Main                                                   #");
Console.WriteLine("################################################################");

Console.WriteLine("Esperando 3 segundos para que la base de datos se inicie correctamente");
System.Threading.Thread.Sleep(3000);
Console.WriteLine("Espera finalizada");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Define Configuration Manager to access appsettings.json
    ConfigurationManager configuration = builder.Configuration;

    // For Entity Framework
    builder.Services.AddDbContext<DBContextCore>();

    // For Identity
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
        // Número máximo de intentos de inicio de sesión fallidos
        options.Lockout.MaxFailedAccessAttempts = 5;
        // Tiempo que la cuenta queda bloqueada después de alcanzar el número máximo de intentos de inicio de sesión fallidos
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        // Requerimientos de la password
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<DBContextCore>()
    .AddDefaultTokenProviders();


    #region Authentication
    // Adding Authentication
    string? JWT_SECRET = Environment.GetEnvironmentVariable("JWT_SECRET");
    if (string.IsNullOrEmpty(JWT_SECRET))
        JWT_SECRET = configuration["JWT:Secret"] == null ? "JWTKEY" : configuration["JWT:Secret"];

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    // Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECRET == null ? "" : JWT_SECRET)),
            ClockSkew = TimeSpan.FromMinutes(5)
        };
    });
    #endregion

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Listen only on HTTP
    builder.WebHost.UseUrls("http://*:80"); 

    /********************************************************************************************************/
    /** Add Dependencies                                                                                   **/
    /********************************************************************************************************/
    #region  Inyección de dependencias

    // DALs
    builder.Services.AddTransient<IDAL_ApplicationUsers, DAL_ApplicationUsers>();

    // BLs
    builder.Services.AddTransient<IBL_ApplicationUsers, BL_ApplicationUsers>();

    #endregion

    builder.Services.AddTransient<IDAL_Categorias, DAL_Categorias_EF>();
    builder.Services.AddTransient<IBL_Categorias, BL_Categorias>();

    builder.Services.AddTransient<IDAL_Productos, DAL_Productos_EF>();
    builder.Services.AddTransient<IBL_Productos, BL_Productos>();

    builder.Services.AddTransient<IDAL_Empresas, DAL_Empresas>();
    builder.Services.AddTransient<IBL_Empresas, BL_Empresas>();

    builder.Services.AddTransient<IDAL_Sucursales, DAL_Sucursales>();
    builder.Services.AddTransient<IBL_Sucursales, BL_Sucursales>();

    builder.Services.AddTransient<IDAL_Facturas, DAL_Facturas>();
    builder.Services.AddTransient<IBL_Facturas, BL_Facturas>();
    builder.Services.AddTransient<IDAL_Opiniones, DAL_Opiniones_EF>();
    builder.Services.AddTransient<IBL_Opiniones, BL_Opiniones>();

    builder.Services.AddTransient<IDAL_Reclamos, DAL_Reclamos_EF>();
    builder.Services.AddTransient<IBL_Reclamos, BL_Reclamos>();

    builder.Services.AddTransient<IDAL_OC, DAL_OC>();
    builder.Services.AddTransient<IBL_OC, BL_OC>();

    builder.Services.AddTransient<IDAL_CarritoProducto, DAL_CarritoProducto>();
    builder.Services.AddTransient<IBL_CarritoProducto, BL_CarritoProducto>();

    //builder.Services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));

    //builder.Services.AddTransient<IEmailSender, EmailSender>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors(
        options => options.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
    );
    }

    StartUp.InitializeDatabase(app);

    app.UseAuthorization();

    app.MapControllers();

    UpdateDatabase();

    app.Run();

}
catch(Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}


void UpdateDatabase()
{
    using (var context = new DataAccessLayer.DBContextCore())
    {
        context?.Database.Migrate();
    }
}