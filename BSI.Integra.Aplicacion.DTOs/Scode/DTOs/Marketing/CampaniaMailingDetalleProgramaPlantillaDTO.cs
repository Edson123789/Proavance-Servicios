using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public partial class CampaniaMailingDetalleProgramaPlantillaDTO
    {
        public int IdCampaniaMailingDetalle { get; set; }
        public int IdCampaniaMailing { get; set; }
        public string Subject { get; set; }
        public string CorreoElectronico { get; set; }
        public int IdPersonal { get; set; }
        public string NombreCompletoPersonal { get; set; }
        public string CentralPersonal { get; set; }
        public string AnexoPersonal { get; set; }
        public string Contenido { get; set; }
        public string Asunto { get; set; }
        public string NombreCampania { get; set; }
    }
}
