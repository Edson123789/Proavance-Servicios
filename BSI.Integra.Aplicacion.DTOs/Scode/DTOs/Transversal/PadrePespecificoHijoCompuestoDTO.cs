﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PadrePespecificoHijoCompuestoDTO
    {
        public int Id { get; set; }
        public int IdSesion { get; set; }
        public int PEspecificoHijoId { get; set; }
        public int PEspecificoPadreId { get; set; }
    }
}
