using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CargoDTO
    {
        public int Id { get; set; }
        public string Nombre{ get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
        public int Orden { get; set; }
    }
}
