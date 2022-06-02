using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSesionConfigurarVideo
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int Minuto { get; set; }
        public int IdTipoVista { get; set; }
        public int? NroDiapositiva { get; set; }
        public int? IdEvaluacion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public bool? ConLogoVideo { get; set; }
        public bool? ConLogoDiapositiva { get; set; }

        public virtual TConfigurarVideoPrograma IdConfigurarVideoProgramaNavigation { get; set; }
        public virtual TTipoVista IdTipoVistaNavigation { get; set; }
    }
}
