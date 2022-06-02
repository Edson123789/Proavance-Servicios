using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PlantillaFaseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class PlantillaWhatsAppDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public int TipoPlantilla { get; set; }
    }
}
