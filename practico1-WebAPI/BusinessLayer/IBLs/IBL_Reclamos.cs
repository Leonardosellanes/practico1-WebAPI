using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBLs
{
    public interface IBL_Reclamos
    {
        List<Reclamo> Get();

        Reclamo Get(int id);

        void Insert(Reclamo reclamo);

        void Update(Reclamo reclamo);

        void Delete(int id);
    }
}
