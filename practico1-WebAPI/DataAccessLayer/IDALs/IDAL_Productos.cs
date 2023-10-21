﻿using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Productos
    {
        List<Producto> Get();

        Producto Get(int id);

        void Insert(Producto producto);

        void Update(Producto producto);

        void Delete(int id);
    }
}
