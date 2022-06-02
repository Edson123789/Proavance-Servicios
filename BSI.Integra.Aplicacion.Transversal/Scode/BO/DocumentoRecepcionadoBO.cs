using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DocumentoRecepcionadoBO : BaseBO
    {
        public int IdPersonaTipoPersona { get; set; }
        public int IdDocumento { get; set; }
        public int IdPespecifico { get; set; }
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public string MimeTypeArchivo { get; set; }
        public int? IdMigracion { get; set; }

        public DocumentoRecepcionadoBO()
        {

        }
    }
}
