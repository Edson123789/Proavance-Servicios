using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComentariosCajaPorRendirDTO
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public string Observacion { get; set; }
        public decimal MontoDevolucion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
