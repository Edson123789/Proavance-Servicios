using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class ObtenerTipoDocumentoAlumnoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int IdPlantillaPosterior { get; set; }
        public int IdOperadorComparacion { get; set; }
        public bool TieneDeuda { get; set; }



    }
}
