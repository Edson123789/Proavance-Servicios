﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class IntervaloTiempoBO : BaseBO
    {
        public string Nombre { get; set; }        
        public int? IdMigracion { get; set; }
    }
}
