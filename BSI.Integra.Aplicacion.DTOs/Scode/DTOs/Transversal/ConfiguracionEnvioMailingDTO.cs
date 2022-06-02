using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConfiguracionEnvioMailingDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int IdPlantilla { get; set; }
        public string NombreUsuario { get; set; }
    }
    public class ConfiguracionEnvioMailingMasivoDTO
    {
        public List<ConfiguracionEnvioMailingDTO> ListaConfiguracionEnvioMailing { get; set; }
    }
}
