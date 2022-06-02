using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoTagPwDTO
    {
        public int Id { get; set; }
        public List<TagPwDTO> ListaTag { get; set; }
        public string Usuario { get; set; }
    }
}

