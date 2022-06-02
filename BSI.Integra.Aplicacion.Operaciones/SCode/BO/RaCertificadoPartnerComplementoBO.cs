using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class RaCertificadoPartnerComplementoBO : BaseBO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Categoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string MencionEnCertificado { get; set; }
        public string FrontalCentral { get; set; }
        public string FrontalInferiorIzquierda { get; set; }
        public string PosteriorCentral { get; set; }
        public string PosteriorInferiorIzquierda { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
