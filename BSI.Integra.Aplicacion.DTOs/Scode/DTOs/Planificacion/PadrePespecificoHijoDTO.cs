using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PadrePespecificoHijoDTO
    {
        public int Id { get; set; }
        public int IdPespecificoPadre { get; set; }
        public int IdPespecificoHijo { get; set; }
    }
}
