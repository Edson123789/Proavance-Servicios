using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TLogSicronizacionCronograma
    {
        public int Id { get; set; }
        public string TipoPeticion { get; set; }
        public string NombreMetodoEjecutado { get; set; }
        public string Parametros { get; set; }
        public string Mensaje { get; set; }
        public bool EsCorrecto { get; set; }
        public bool EstaCorregido { get; set; }
        public string UsuarioSolicitud { get; set; }
        public DateTime FechaControl { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public string IdMigracion { get; set; }
    }
}
