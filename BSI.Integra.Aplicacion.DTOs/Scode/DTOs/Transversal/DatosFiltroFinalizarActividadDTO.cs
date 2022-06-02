using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosFiltroFinalizarActividadDTO
    {
        public int IdOcurrencia { get; set; }
        public string Tipo { get; set; }
        public int IdActividadCabecera { get; set; }
        public int IdCategoria { get; set; }
        public int? IdPersonal { get; set; }
        public string Usuario { get; set; }
    }
}
