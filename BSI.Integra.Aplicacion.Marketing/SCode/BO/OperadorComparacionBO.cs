﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class OperadorComparacionBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public string Simbolo { get; set; }
    }
}
