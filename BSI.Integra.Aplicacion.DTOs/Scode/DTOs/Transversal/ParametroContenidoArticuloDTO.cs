﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ParametroContenidoArticuloDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NumeroCaracteres { get; set; }
        public string Descripcion { get; set; }
    }
}
