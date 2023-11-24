﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DataAccessLayer.EFModels;

namespace BusinessLayer.IBLs
{
    public interface IBL_OC
    {
        OC ObtenerOCPorId(long id);
        List<OC> ObtenerTodasLasOcs();
        void CrearOC(OC orden);
        void ActualizarOC(OC orden);
        void EliminarOC(long id);
    }
}

