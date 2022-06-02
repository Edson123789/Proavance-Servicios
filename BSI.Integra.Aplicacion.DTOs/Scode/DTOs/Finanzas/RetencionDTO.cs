using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RetencionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Pais { get; set; }
        public int IdPais { get; set; }
        public decimal Valor { get; set; }
        public string Usuario { get; set; }
    }
}
