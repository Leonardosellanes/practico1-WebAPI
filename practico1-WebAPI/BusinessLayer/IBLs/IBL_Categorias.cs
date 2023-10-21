using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Categorias
    {
        List<Categoria> Get();

        Categoria Get(int id);

        void Insert(Categoria categoria);

        void Update(Categoria categoria);

        void Delete(int id);
    }
}
