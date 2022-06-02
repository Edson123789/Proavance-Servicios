using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DocumentoRecepcionadoDTO
    {
        public int Id { get; set; }
        public int IdPersonaTipoPersona { get; set; }
        public int IdDocumento { get; set; }
        public int IdPespecifico { get; set; }
        public string Usuario { get; set; }
    }
}
