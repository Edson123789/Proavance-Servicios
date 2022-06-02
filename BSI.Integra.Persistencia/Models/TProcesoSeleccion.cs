using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProcesoSeleccion
    {
        public TProcesoSeleccion()
        {
            TConvocatoriaPersonal = new HashSet<TConvocatoriaPersonal>();
            TCuerpoConvocatoria = new HashSet<TCuerpoConvocatoria>();
            TExamenAsignadoEvaluador = new HashSet<TExamenAsignadoEvaluador>();
            TPostulanteInformacionImportacion = new HashSet<TPostulanteInformacionImportacion>();
            TPostulanteInformacionImportacionLog = new HashSet<TPostulanteInformacionImportacionLog>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string Codigo { get; set; }
        public string Url { get; set; }
        public bool? Activo { get; set; }
        public int? IdSede { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public DateTime? FechaInicioProceso { get; set; }
        public DateTime? FechaFinProceso { get; set; }

        public virtual ICollection<TConvocatoriaPersonal> TConvocatoriaPersonal { get; set; }
        public virtual ICollection<TCuerpoConvocatoria> TCuerpoConvocatoria { get; set; }
        public virtual ICollection<TExamenAsignadoEvaluador> TExamenAsignadoEvaluador { get; set; }
        public virtual ICollection<TPostulanteInformacionImportacion> TPostulanteInformacionImportacion { get; set; }
        public virtual ICollection<TPostulanteInformacionImportacionLog> TPostulanteInformacionImportacionLog { get; set; }
    }
}
