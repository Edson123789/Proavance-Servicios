using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCalidadProcesamiento
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int PerfilCamposLlenos { get; set; }
        public int PerfilCamposTotal { get; set; }
        public bool Dni { get; set; }
        public int PgeneralValidados { get; set; }
        public int PgeneralTotal { get; set; }
        public int PespecificoValidados { get; set; }
        public int PespecificoTotal { get; set; }
        public int BeneficiosValidados { get; set; }
        public int BeneficiosTotales { get; set; }
        public bool CompetidoresVerificacion { get; set; }
        public int ProblemaSeleccionados { get; set; }
        public int ProblemaSolucionados { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TOportunidad IdOportunidadNavigation { get; set; }
    }
}
