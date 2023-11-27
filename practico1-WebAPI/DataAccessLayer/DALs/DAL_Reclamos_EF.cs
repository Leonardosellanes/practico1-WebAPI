using DataAccessLayer.EFModels;
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
    public class DAL_Reclamos_EF : IDAL_Reclamos

    {
        private DBContextCore _dbContext;

        public DAL_Reclamos_EF(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }
        public void Delete(int id)
        {
            var reclamo = _dbContext.Reclamos.FirstOrDefault(o => o.Id == id);
            _dbContext.Reclamos.Remove(reclamo);
            _dbContext.SaveChanges();
        }

        public List<Reclamo> Get()
        {
            return _dbContext.Reclamos
                             .Include(e => e.EmpresaAsociada)
                             .Include(o => o.OrdenAsociada)
                             .Select(r => new Reclamo { 
                                 Id = r.Id,
                                 Descripcion = r.Descripcion, 
                                 Fecha = r.Fecha,
                                 EmpresaId = r.EmpresaId,
                                 EmpresaAsociada = new Empresa
                                 {
                                     Id = r.EmpresaAsociada.Id,
                                     Nombre = r.EmpresaAsociada.Nombre,
                                     RUT = r.EmpresaAsociada.RUT
                                 },
                                 OCId = r.OCId,
                                 OrdenAsociada = new Orden
                                 {
                                     Id = r.OrdenAsociada.Id,
                                     DireccionDeEnvio = r.OrdenAsociada.DireccionDeEnvio,
                                     MedioDePago = r.OrdenAsociada.MedioDePago,
                                     Fecha = r.OrdenAsociada.Fecha,
                                     FechaEstimadaEntrega = r.OrdenAsociada.FechaEstimadaEntrega,
                                     Total = r.OrdenAsociada.Total,
                                     EstadoOrden = r.OrdenAsociada.EstadoOrden
                                 }
                             })
                             .ToList();
        }

        public Reclamo Get(int id)
        {
            var reclamo = _dbContext.Reclamos
                .Include(e => e.EmpresaAsociada)
                .FirstOrDefault(o => o.Id == id);

            return reclamo == null
                ? throw new Exception($"No se encontró un reclamo con el ID {id}")
                : new Reclamo
                {
                    Id = reclamo.Id,
                    Descripcion = reclamo.Descripcion,
                    Fecha = reclamo.Fecha,
                    EmpresaId = reclamo.EmpresaId,
                    EmpresaAsociada = new Empresa
                    {
                        Id = reclamo.EmpresaAsociada.Id,
                        Nombre = reclamo.EmpresaAsociada.Nombre,
                        RUT = reclamo.EmpresaAsociada.RUT
                    }
                };
        }


        public void Insert(Reclamo reclamo)
        {
            _dbContext.Reclamos
                .Add(new Reclamos { 
                Id = reclamo.Id, 
                Descripcion = reclamo.Descripcion, 
                Fecha = reclamo.Fecha, 
                EmpresaId = reclamo.EmpresaId,
                OCId = reclamo.OCId,
            });
            _dbContext.SaveChanges();
        }
        public void Update(Reclamo updatedReclamo)
        {
            var existingReclamo = _dbContext.Reclamos.Find(updatedReclamo.Id);

            if (existingReclamo != null)
            {
                if (existingReclamo.Descripcion != updatedReclamo.Descripcion)
                {
                    existingReclamo.Descripcion = updatedReclamo.Descripcion;
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
