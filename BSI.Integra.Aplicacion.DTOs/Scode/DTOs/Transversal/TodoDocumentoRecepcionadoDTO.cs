using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TodoDocumentoRecepcionadoDTO
    {
        public int Id { get; set; }
        public int IdPersonaTipoPersona { get; set; }
        public string Nombre { get; set; }
        public int IdDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public string NombreArchivo { get; set; }
        public bool ExisteArchivo { get; set; }
    }
}
