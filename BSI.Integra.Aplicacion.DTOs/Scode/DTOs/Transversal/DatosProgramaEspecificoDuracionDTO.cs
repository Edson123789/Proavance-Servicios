﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosProgramaEspecificoDuracionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Duracion { get; set; }
        public int? IdProgramaGeneral { get; set; }
    }
}
