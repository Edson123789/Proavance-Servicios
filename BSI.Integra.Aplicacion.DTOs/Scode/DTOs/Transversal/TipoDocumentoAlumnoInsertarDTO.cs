using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class TipoDocumentoAlumnoInsertarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int IdPlantillaPosterior { get; set; }        
        public List<int> IdPGeneral { get; set; }
        public List<TipoDocumentoAlumnoCombinadoDTO> ListaCondiciones { get; set; }
        public int OperadorComparacion { get; set; }
        public string Usuario { get; set; }
    }
}
