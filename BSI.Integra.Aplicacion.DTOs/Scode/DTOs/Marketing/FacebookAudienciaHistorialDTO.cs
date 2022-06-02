﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FacebookAudienciaHistorialDTO
    {
        public string NombreCuentaPublicitaria { get; set; }
        public string FacebookIdCuentaPublicitaria { get; set; }
        public string FacebookIdAudiencia { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string Subtipo { get; set; }
    }
}
