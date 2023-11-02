﻿using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_Facturas : IDAL_Facturas
    {
        private DBContextCore _dbContext;
        public DAL_Facturas(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            Facturas factura = _dbContext.Facturas.FirstOrDefault(f => f.Id == id);
            if (factura != null)
            {
                _dbContext.Facturas.Remove(factura);
                _dbContext.SaveChanges();
            }
        }

        public List<Factura> GetAll()
        {
            return _dbContext.Facturas
                             .Select(f => new Factura
                             {
                                 Id = f.Id,
                                 TotalComisiones = f.TotalComisiones,
                                 FechaInicio = f.FechaInicio,
                                 FechaFin = f.FechaFin,
                                 EmpresaId = f.EmpresaId,
                                 Empresa = new Empresa
                                 {
                                     Id = f.EmpresaAsociada.Id,
                                     Nombre = f.EmpresaAsociada.Nombre,
                                     RUT = f.EmpresaAsociada.RUT
                                 }
                             })
                             .ToList();
        }

        public Factura GetById(int id)
        {
            Facturas factura = _dbContext.Facturas
                .Include(e => e.EmpresaAsociada)
                .FirstOrDefault(f => f.Id == id);

            return factura == null
                ? throw new Exception($"No se encontró una factura con el ID {id}")
                : new Factura
                {
                    Id = factura.Id,
                    TotalComisiones = factura.TotalComisiones,
                    FechaInicio = factura.FechaInicio,
                    FechaFin = factura.FechaFin,
                    EmpresaId = factura.EmpresaId,
                    Empresa = new Empresa
                    {
                        Id = factura.EmpresaAsociada.Id,
                        Nombre = factura.EmpresaAsociada.Nombre,
                        RUT = factura.EmpresaAsociada.RUT
                    }
                };
        }


        public void Insert(Factura factura)
        {
            _dbContext.Add(new Facturas
            {
                Id = factura.Id,
                TotalComisiones = factura.TotalComisiones,
                FechaInicio = factura.FechaInicio,
                FechaFin = factura.FechaFin,
                EmpresaId = factura.EmpresaId,
            });
            _dbContext.SaveChanges();
        }

        public void Update(Factura factura)
        {
            Facturas fac = _dbContext.Facturas.FirstOrDefault(f => f.Id == factura.Id);
            if (fac != null)
            {
                fac.TotalComisiones = factura.TotalComisiones;
                fac.FechaInicio = factura.FechaInicio;
                fac.FechaFin = factura.FechaFin;
                fac.EmpresaId = factura.EmpresaId;
                _dbContext.SaveChanges();
            }
        }
    }
}
