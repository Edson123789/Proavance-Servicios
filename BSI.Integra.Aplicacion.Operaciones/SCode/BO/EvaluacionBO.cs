using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class EvaluacionBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPespecifico { get; set; }
        public int Grupo { get; set; }
        public int Porcentaje { get; set; }
        public bool Aprobado { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string UsuarioAprobacion { get; set; }
        public int? IdCriterioEvaluacion { get; set; }
    }
}
