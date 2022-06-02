using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class HojaActividadesDTO
    {
        public int Id { get; set; }
        public string TipoActividad { get; set; }
        public string Actividad { get; set; }
        public string FechaProgramada { get; set; }
        public int IdOcurrencia { get; set; }
        public int OcurrenciaPadre { get; set; }
    }
}
