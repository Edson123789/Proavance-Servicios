using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComboSubcontenedorArchivoDTO
    {
        public int IdSubcontenedor { get; set; }
        public string Subcontenedor { get; set; }
        public int IdContenedor { get; set; }
    }

    public class ComboTipoSubcontenedorArchivoDTO
    {
        public int Id { get; set; }
        public int IdContenedor { get; set; }
        public int IdUrlSubContenedor { get; set; }
        public string Tipo { get; set; }
        public string Ruta { get; set; }
    }
}
