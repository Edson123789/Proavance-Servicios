using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CategoriaProgramaDTO
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public bool Visible { get; set; }
        public string Usuario { get; set; }
    }
}
