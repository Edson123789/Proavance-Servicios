using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AprobarRechazarCronogramaDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string IdCambio { get; set; }
        public int Version { get; set; }
        public bool Aprobado { get; set; }
        public bool Cancelado { get; set; }
    }
}
