using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class OportunidadPrerequisitoGeneralBO : BaseBO
    {
        public int Id { get; set; }
        public int? IdOportunidadCompetidor { get; set; }
        public int? IdProgramaGeneralPrerequisito { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public OportunidadPrerequisitoGeneralBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
