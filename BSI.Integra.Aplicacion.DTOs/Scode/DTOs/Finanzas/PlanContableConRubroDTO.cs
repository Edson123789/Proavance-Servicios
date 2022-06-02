using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PlanContableConRubroDTO
    {
        public int Id { get; set; }
        public long Cuenta { get; set; }
        public string Descripcion { get; set; }
        public int? IdFurTipoSolicitud { get; set; }
        public string NombreFurTipoSolicitud { get; set; }
    }
}
