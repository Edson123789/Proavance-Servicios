using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCategoriaOrigen
    {
        public TCategoriaOrigen()
        {
            TAccionFormularioPorCategoriaOrigen = new HashSet<TAccionFormularioPorCategoriaOrigen>();
            TCampaniaGeneral = new HashSet<TCampaniaGeneral>();
            TDatoOportunidadAreaVenta = new HashSet<TDatoOportunidadAreaVenta>();
            TModeloGeneralCategoriaDato = new HashSet<TModeloGeneralCategoriaDato>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoDato { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int Meta { get; set; }
        public int? IdProveedorCampaniaIntegra { get; set; }
        public int? IdFormularioProcedencia { get; set; }
        public bool Considerar { get; set; }
        public string CodigoOrigen { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TAccionFormularioPorCategoriaOrigen> TAccionFormularioPorCategoriaOrigen { get; set; }
        public virtual ICollection<TCampaniaGeneral> TCampaniaGeneral { get; set; }
        public virtual ICollection<TDatoOportunidadAreaVenta> TDatoOportunidadAreaVenta { get; set; }
        public virtual ICollection<TModeloGeneralCategoriaDato> TModeloGeneralCategoriaDato { get; set; }
    }
}
