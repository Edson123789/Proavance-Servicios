﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public partial class FurSubFaseAprobacionBO : BaseBO
    {
        public string Nombre { get; set; }
        public int IdFurFaseAprobacion { get; set; }
    }
}
