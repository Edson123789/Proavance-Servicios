using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DocumentoConfiguracionRecepcionDatosDTO
    {
        public int Id { get; set; }
        public int IdTipoPersona { get; set; }
        public string NombreTipoPersona { get; set; }
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public int IdDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidadCurso { get; set; }
        public int? Padre { get; set; }
        public bool EsActivo { get; set; }
        public string Usuario { get; set; }
    }
}
