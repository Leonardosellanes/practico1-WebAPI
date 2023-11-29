using BusinessLayer.IBLs;
using DataAccessLayer;
using DataAccessLayer.IDALs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Empresas : IBL_Empresas
    {
        private IDAL_Empresas _empresas;

        public BL_Empresas(IDAL_Empresas empresas)
        {
            _empresas = empresas;
        }

        public void Delete(int id)
        {
            _empresas.Delete(id);
        }

        public List<Empresa> Get()
        {
            return _empresas.GetAll();
        }

        public Empresa Get(int id)
        {
            return _empresas.GetById(id);
        }

        public async Task<int> InsertAsync(Shared.Empresa empresa)
        {
            var empresaInsertada = await _empresas.InsertAndGetAsync(empresa);

            // Imprimir el valor de empresa.Id después de la inserción
            Console.WriteLine($"ID de la empresa después de la inserción en BL: {empresaInsertada.Id}");

            return empresaInsertada.Id;
        }

        public void Update(Empresa empresa)
        {
            _empresas.Update(empresa);
        }
    }
}
