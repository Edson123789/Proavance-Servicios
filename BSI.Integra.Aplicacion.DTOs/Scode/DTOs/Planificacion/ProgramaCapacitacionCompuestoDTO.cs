using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaCapacitacionCompuestoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoTemaProgramaCapacitacion { get; set; }
        //public int? IdPEspecificoAsesor { get; set; }
        public List<int> PGenerales { get; set; }

        public string Usuario { get; set; }
    }
}
