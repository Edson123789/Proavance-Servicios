using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TipoVistaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class InformacionConfigurarVideoDTO
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int IdPGeneral { get; set; }
        public int Minuto { get; set; }
        public int IdTipoVista { get; set; }
        public int NroDiapositiva { get; set; }
        public bool ConLogoVideo { get; set; }
        public bool ConLogoDiapositiva { get; set; }
        
    }

    public class IdConfigurarVideoDTO
    {
        public int Id { get; set; }        
    }

    public class SesionConfiguracionVideoDTO
    {
        public int Minuto { get; set; }
        public int IdTipoVista { get; set; }
        public int NroDiapositiva { get; set; }
        public bool ConLogoVideo { get; set; }
        public bool ConLogoDiapositiva { get; set; }
    }
}
