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
    public class DAL_Empresas : IDAL_Empresas
    {
        private DBContextCore _dbContext;

        public DAL_Empresas(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public Empresa GetById(int id)
        {
            Empresas emp = _dbContext.Empresas.FirstOrDefault(e => e.Id == id);
            return new Empresa { Id = emp.Id, Nombre = emp.Nombre, RUT = emp.RUT };
        }

        public List<Empresa> GetAll()
        {
            return _dbContext.Empresas
                             .Select(e => new Empresa { Id = e.Id, Nombre = e.Nombre, RUT = e.RUT })
                             .ToList();
        }

        public void Insert(Empresa empresa)
        {
            _dbContext.Empresas.Add(new Empresas { Nombre = empresa.Nombre, RUT = empresa.RUT });
            _dbContext.SaveChanges();
        }

        public void Update(Empresa empresa)
        {
            var emp = _dbContext.Empresas.FirstOrDefault(e => e.Id == empresa.Id);

            if (emp != null)
            {
                emp.Nombre = empresa.Nombre;
                emp.RUT = empresa.RUT;

                _dbContext.SaveChanges();
            }

        }

        public void Delete(int id)
        {
            var empresa = _dbContext.Empresas.FirstOrDefault(e => e.Id == id);
            if (empresa != null)
            {
                _dbContext.Empresas.Remove(empresa);
                _dbContext.SaveChanges();
            }
        }
    }
}
