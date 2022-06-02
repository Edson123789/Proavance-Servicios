using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAccionFormulario
    {
        public TAccionFormulario()
        {
            TAccionFormularioPorCampoContacto = new HashSet<TAccionFormularioPorCampoContacto>();
            TAccionFormularioPorCategoriaOrigen = new HashSet<TAccionFormularioPorCategoriaOrigen>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int UltimaLlamadaEjecutada { get; set; }
        public bool CamposSinValores { get; set; }
        public int TiempoRedirecionamiento { get; set; }
        public bool CamposSegunProbabilidad { get; set; }
        public bool TodosCampos { get; set; }
        public int? NumeroClics { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid IdMigracion { get; set; }

        public virtual ICollection<TAccionFormularioPorCampoContacto> TAccionFormularioPorCampoContacto { get; set; }
        public virtual ICollection<TAccionFormularioPorCategoriaOrigen> TAccionFormularioPorCategoriaOrigen { get; set; }
    }
}
