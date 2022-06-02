using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PlanContableConRubroRequestDTO
    {
        public int IdPlanContable { get; set; }
        public int? IdFurTipoSolicitud { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
