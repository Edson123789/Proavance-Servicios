﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TipoDatoPrincipalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Prioridad { get; set; }
        public string Usuario { get; set; }

    }
}
