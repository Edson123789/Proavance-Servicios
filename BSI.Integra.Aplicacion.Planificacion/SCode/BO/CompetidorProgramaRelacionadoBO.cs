﻿using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class CompetidorProgramaRelacionadoBO : BaseBO
    {
        public int Id { get; set; }
        public int? IdCompetidor { get; set; }
        public int IdPrograma { get; set; }

    }
}
