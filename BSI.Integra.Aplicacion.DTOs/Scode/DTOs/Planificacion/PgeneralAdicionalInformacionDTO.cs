using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralAdicionalInformacionDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Descripcion { get; set; }
        public int IdTitulo { get; set; }

        public string NombreImagen { get; set; }
        public string NombreTitulo { get; set; }
    }
}
