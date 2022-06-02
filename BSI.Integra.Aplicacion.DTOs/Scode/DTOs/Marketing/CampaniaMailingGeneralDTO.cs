using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CampaniaMailingGeneralDTO
    {
    }

    public class CampaniaGeneralDetalleProgramaPlantillaDTO
    {
        public int IdCampaniaGeneralDetalle { get; set; }
        public int IdCampaniaGeneral { get; set; }
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

    public class CampaniaGeneralDetalleContenidoEtiquetaDTO
    {
        public string Contenido { get; set; }
        public string Etiqueta { get; set; }
    }

    public class CampaniaGeneralDetalleActualizacionDTO
    {
        public int Id { get; set; }
        public int? CantidadContactosMailing { get; set; }
        public int? CantidadContactosWhatsapp { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    public class CampaniaGeneralDetalleResultadoDTO
    {
        public int IdCampaniaGeneralDetalle { get; set; }
        public int CantidadContactosMailing { get; set; }
        public int CantidadContactosWhatsApp { get; set; }
        public bool EnEjecucion { get; set; }
    }

    public class CampaniaGeneralDetalleConCantidadDTO
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
    }
}
