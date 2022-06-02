﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Scode.BO
{
    public class ConfiguracionHorarioMarcacionBO : BaseBO
    {
        public string Nombre { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public List<ConfiguracionHorarioMarcacionGrupoBO> IdHorarioGrupoPersonal { get;set;}
    }
}
