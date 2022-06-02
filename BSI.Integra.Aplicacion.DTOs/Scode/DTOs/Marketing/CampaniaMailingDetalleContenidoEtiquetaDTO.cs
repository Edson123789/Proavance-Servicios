using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public partial class CampaniaMailingDetalleContenidoEtiquetaDTO
    {
        public string Contenido { get; set; }
        public string Etiqueta { get; set; }
    }

    public class CampaniaMailingDetalleDTO
    {
        public int IdCampaniaMailingDetalle { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string Contenido { get; set; }
        public string Etiqueta { get; set; }
    }

    public class CampaniaMailingDetalleActualizacionDTO
    {   
        public int Id { get; set; }
        public int CantidadContactos { get; set; }
        public string UsuarioModificacion{ get; set; }
    }
}
