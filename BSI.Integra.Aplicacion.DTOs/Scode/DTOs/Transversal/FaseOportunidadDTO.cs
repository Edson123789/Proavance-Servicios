﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FaseOportunidadDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int? NroMinutos { get; set; }
        public int? IdActividad { get; set; } 
        public int? MaxNumDias { get; set; }
        public int? MinNumDias { get; set; }
        public int? TasaConversionEsperada { get; set; }
        public int? Meta { get; set; }
        public bool? Final { get; set; }
        public bool? ReporteMeta { get; set; }
        public bool? EnSeguimiento { get; set; }
        public bool? EsCierre { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        public bool? VisibleEnReporte { get; set; }
    }
}
