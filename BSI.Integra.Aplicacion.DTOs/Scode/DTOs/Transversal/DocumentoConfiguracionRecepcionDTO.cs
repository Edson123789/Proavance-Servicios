using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DocumentoConfiguracionRecepcionDTO
    {
        public int Id { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdPais { get; set; }
        public int IdDocumento { get; set; }
        public int IdModalidadCurso { get; set; }
        public int? Padre { get; set; }
        public bool EsActivo { get; set; }
        public string Usuario { get; set; }
    }
}
