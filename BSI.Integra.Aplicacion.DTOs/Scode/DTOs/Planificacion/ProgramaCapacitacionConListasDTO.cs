using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaCapacitacionConListasDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoTemaProgramaCapacitacion { get; set; }
        //public int? IdPEspecificoAsesor { get; set; }

        public int? NumeroOfertas { get; set; }
        public string NombreProgramaGeneral {get; set;}
        public string NombreAreaCapacitacion {get; set;}
        public string NombreSubAreaCapacitacion {get; set;}

        public string Usuario { get; set; }
    }
}
