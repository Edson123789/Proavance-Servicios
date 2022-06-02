using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralHijoDTO
    {
        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Pg_titulo { get; set; }
        public string pw_duracion { get; set; }
        public List<SeccionDocumentoDTO> ListaSeccion { get; set; }
    }
}
