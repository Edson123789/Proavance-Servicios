using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActualizarHistoricoDTO
    {
        public int Id { get; set; }
        public int IdTipoPago{ get; set; }
        public int IdCondicionPago { get; set; }
        public string Observaciones { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
