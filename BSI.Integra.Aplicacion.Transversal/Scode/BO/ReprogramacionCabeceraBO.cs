using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ReprogramacionCabeceraBO : BaseBO
    {
        public int Id { get; set; }
        public int IdActividadCabecera { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int MaxReproPorDia { get; set; }
        public int IntervaloSigProgramacionMin { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public ReprogramacionCabeceraBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
