using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConfiguracionRutinaBncObsoletoDTO
    {
        public int  Id { get; set; }
        public string Nombre { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int IdTipoDato { get; set; }
        public int NumDiasProbabilidadMedia { get; set; }
        public int NumDiasProbabilidadAlta { get; set; }
        public int NumDiasProbabilidadMuyAlta { get; set; }
        public bool EjecutarRutinaProbabilidadMedia { get; set; }
        public bool EjecutarRutinaProbabilidadAlta { get; set; }
        public bool EjecutarRutinaProbabilidadMuyAlta { get; set; }
        public int IdOcurrenciaDestino { get; set; }
        public bool EjecutarRutinaEnviarCorreo { get; set; }
        public int IdPlantillaCorreo { get; set; }
        public int IdPersonalCorreoNoExistente { get; set; }
    }
}
