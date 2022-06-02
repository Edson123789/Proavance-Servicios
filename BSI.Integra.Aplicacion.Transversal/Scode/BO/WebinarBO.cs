using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WebinarBO : BaseBO
    {
        public WebinarBO()
        {
            ListaWebinarCentroCosto = new HashSet<WebinarCentroCostoBO>();
            ListaWebinarDetalle = new HashSet<WebinarDetalleBO>();
        }

        public string Nombre { get; set; }
        public string NombreCursoCompleto { get; set; }
        public int IdExpositor { get; set; }
        public int IdPersonal { get; set; }
        public int IdFrecuencia { get; set; }
        public int? IdWebinarCategoriaConfirmacionAsistencia { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string LinkAulaVirtual { get; set; }
        public bool Activo { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<WebinarCentroCostoBO> ListaWebinarCentroCosto { get; set; }
        public virtual ICollection<WebinarDetalleBO> ListaWebinarDetalle { get; set; }
    }
}
