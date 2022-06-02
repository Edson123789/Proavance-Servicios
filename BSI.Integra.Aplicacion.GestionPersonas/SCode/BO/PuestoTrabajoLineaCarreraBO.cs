﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class PuestoTrabajoLineaCarreraBO : BaseBO
    {
        public int IdPuestoTrabajo { get; set; }
        public int Orden { get; set; }
        public string LineaCarrera { get; set; }
        public int? IdMigracion { get; set; }
    }
}
