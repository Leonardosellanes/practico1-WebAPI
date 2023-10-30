using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_Sucursales : IDAL_Sucursales
    {
        private DBContextCore _dbContext;
        public DAL_Sucursales(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public Sucursal GetById(int id)
        {
            Sucursales suc = _dbContext.Sucursales.FirstOrDefault(s => s.Id == id);
            
            return new Sucursal 
                { 
                    Id = suc.Id, 
                    Nombre = suc.Nombre, 
                    Ubicacion = suc.Ubicacion, 
                    TiempoEntrega = suc.TiempoEntrega, 
                    EmpresaId = suc.EmpresaId,
                    Empresa = new Empresa 
                    { 
                        Id = suc.Empresa.Id, 
                        Nombre = suc.Empresa.Nombre,
                        RUT = suc.Empresa.RUT
                    }
                };
        }

        public List<Sucursal> GetAll()
        {
            return _dbContext.Sucursales
                             .Select(suc => new Sucursal
                             {
                                 Id = suc.Id,
                                 Nombre = suc.Nombre,
                                 Ubicacion = suc.Ubicacion,
                                 TiempoEntrega = suc.TiempoEntrega,
                                 EmpresaId = suc.EmpresaId,
                                 Empresa = new Empresa
                                 {
                                     Id = suc.Empresa.Id,
                                     Nombre = suc.Empresa.Nombre,
                                     RUT = suc.Empresa.RUT
                                 }
                             })
                             .ToList();
        }

        public void Insert(Sucursal sucursal)
        {
            _dbContext.Sucursales.Add(new Sucursales
                {
                    Id = sucursal.Id,
                    Nombre = sucursal.Nombre,
                    Ubicacion = sucursal.Ubicacion,
                    TiempoEntrega = sucursal.TiempoEntrega,
                    EmpresaId = sucursal.EmpresaId
                });
            _dbContext.SaveChanges();
        }

        public void Update(Sucursal sucursal)
        {
            var s = _dbContext.Sucursales.FirstOrDefault(x => x.Id == sucursal.Id);

            if (s != null)
            {
                s.Nombre = sucursal.Nombre;
                s.Ubicacion = sucursal.Ubicacion;
                s.TiempoEntrega = sucursal.TiempoEntrega;
                s.EmpresaId = sucursal.EmpresaId;

                _dbContext.SaveChanges();
            }

        }

        public void Delete(int id)
        {
            var sucursal = _dbContext.Sucursales.FirstOrDefault(s => s.Id == id);
            if (sucursal != null)
            {
                _dbContext.Sucursales.Remove(sucursal);
                _dbContext.SaveChanges();
            }
        }
    }
}
