﻿using DataAccessLayer.EFModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccessLayer
{
    public class DBContextCore : IdentityDbContext<Usuarios>
    {
        private string _connectionString = "Server=sqlserver,1433;Database=Practico4;User Id=sa;Password=Abc*123!;Encrypt=False;";

        public DBContextCore()
        { }

        public DBContextCore(DbContextOptions<DBContextCore> options)
        : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Personas> Personas { get; set; }
        public DbSet<Vehiculos> Vehiculos { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Empresas> Empresas { get; set; }


    }
}
