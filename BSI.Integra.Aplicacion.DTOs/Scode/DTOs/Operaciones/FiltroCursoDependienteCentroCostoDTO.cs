﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroCursoDependienteCentroCostoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdRaCentroCosto { get; set; }
    }
}
