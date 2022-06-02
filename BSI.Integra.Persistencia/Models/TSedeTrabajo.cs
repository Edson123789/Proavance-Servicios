using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSedeTrabajo
    {
        public TSedeTrabajo()
        {
            TConvocatoriaPersonal = new HashSet<TConvocatoriaPersonal>();
            TDatoContratoPersonal = new HashSet<TDatoContratoPersonal>();
            TGrupoComparacionProcesoSeleccion = new HashSet<TGrupoComparacionProcesoSeleccion>();
            TPersonalPuestoSedeHistorico = new HashSet<TPersonalPuestoSedeHistorico>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IpCentral { get; set; }
        public string Comentarios { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TConvocatoriaPersonal> TConvocatoriaPersonal { get; set; }
        public virtual ICollection<TDatoContratoPersonal> TDatoContratoPersonal { get; set; }
        public virtual ICollection<TGrupoComparacionProcesoSeleccion> TGrupoComparacionProcesoSeleccion { get; set; }
        public virtual ICollection<TPersonalPuestoSedeHistorico> TPersonalPuestoSedeHistorico { get; set; }
    }
}
