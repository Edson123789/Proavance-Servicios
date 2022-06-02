using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class WhatsAppPlantillaDTO
    {
        public string Nombre { get; set; }
        public string EspacioNombre { get; set; }
        public List<ParametrosPlantilla> ListaParametros { get; set; }
    }

    public class ParametrosPlantilla
    {
        public string Codigo { get; set; }
        public string Etiqueta { get; set; }
    }
}
