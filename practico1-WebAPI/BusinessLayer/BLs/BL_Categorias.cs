using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Categorias : IBL_Categorias
    {
        private IDAL_Categorias _categorias;

        public BL_Categorias(IDAL_Categorias categorias)
        {
            _categorias = categorias;
        }

        public void Delete(int id)
        {
            _categorias.Delete(id);
        }

        public List<Categoria> Get(int empresaId)
        {
            return _categorias.Get(empresaId);
        }

        public Categoria GetbyId(int id)
        {
            return _categorias.GetbyId(id);
        }

        public void Insert(Categoria categoria)
        {
            _categorias.Insert(categoria);
        }

        public void Update(Categoria categoria)
        {
            _categorias.Update(categoria);
        }
    }
}
