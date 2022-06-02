using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TOportunidadCompetidor
    {
        public TOportunidadCompetidor()
        {
            TDetalleOportunidadCompetidor = new HashSet<TDetalleOportunidadCompetidor>();
            TOportunidadBeneficio = new HashSet<TOportunidadBeneficio>();
            TOportunidadPrerequisitoEspecifico = new HashSet<TOportunidadPrerequisitoEspecifico>();
            TOportunidadPrerequisitoGeneral = new HashSet<TOportunidadPrerequisitoGeneral>();
        }

        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public string OtroBeneficio { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TOportunidad IdOportunidadNavigation { get; set; }
        public virtual ICollection<TDetalleOportunidadCompetidor> TDetalleOportunidadCompetidor { get; set; }
        public virtual ICollection<TOportunidadBeneficio> TOportunidadBeneficio { get; set; }
        public virtual ICollection<TOportunidadPrerequisitoEspecifico> TOportunidadPrerequisitoEspecifico { get; set; }
        public virtual ICollection<TOportunidadPrerequisitoGeneral> TOportunidadPrerequisitoGeneral { get; set; }
    }
}
