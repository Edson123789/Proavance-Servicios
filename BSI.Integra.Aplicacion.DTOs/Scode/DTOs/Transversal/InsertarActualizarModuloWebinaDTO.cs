using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InsertarActualizarModuloWebinaDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPespecificoPadre { get; set; }
        public string Usuario { get; set; }
    }
}
