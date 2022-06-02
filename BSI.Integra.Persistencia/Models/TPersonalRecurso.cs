using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPersonalRecurso
    {
        public TPersonalRecurso()
        {
            TEventoCalendarioProyectoPersonal = new HashSet<TEventoCalendarioProyectoPersonal>();
            TPersonalRecursoHabilidad = new HashSet<TPersonalRecursoHabilidad>();
        }

        public int Id { get; set; }
        public string NombrePersonal { get; set; }
        public string ApellidosPersonal { get; set; }
        public string DescripcionPersonal { get; set; }
        public string UrlfotoPersonal { get; set; }
        public int CostoHorario { get; set; }
        public int IdMoneda { get; set; }
        public int Productividad { get; set; }
        public bool? EsDisponible { get; set; }
        public int IdTipoDisponibilidadPersonal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMoneda IdMonedaNavigation { get; set; }
        public virtual TTipoDisponibilidadPersonal IdTipoDisponibilidadPersonalNavigation { get; set; }
        public virtual ICollection<TEventoCalendarioProyectoPersonal> TEventoCalendarioProyectoPersonal { get; set; }
        public virtual ICollection<TPersonalRecursoHabilidad> TPersonalRecursoHabilidad { get; set; }
    }
}
