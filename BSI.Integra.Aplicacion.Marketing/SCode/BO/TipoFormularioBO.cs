﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class TipoFormularioBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NroCampos { get; set; }
    }
}
