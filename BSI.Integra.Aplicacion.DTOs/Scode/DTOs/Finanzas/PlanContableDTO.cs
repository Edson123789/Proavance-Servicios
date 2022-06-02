using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PlanContableDTO
    {
        public int Id { get; set; }
        public long Cuenta { get; set; }
        public string Descripcion { get; set; }
        public int Padre { get; set; }
        public bool Univel { get; set; }
        public string Cbal { get; set; }
        public string Debe { get; set; }
        public string Haber { get; set; }
        public int IdTipoCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public string Analisis { get; set; }
        public string CentroCosto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }        
    }
}
