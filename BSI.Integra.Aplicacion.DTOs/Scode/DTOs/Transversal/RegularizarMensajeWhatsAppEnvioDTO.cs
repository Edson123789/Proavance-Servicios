using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegularizarMensajeWhatsAppEnvioDTO
    {
        public int Id { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int IdAlumno { get; set; }
        public int IdPersonal { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public int IdPlantilla { get; set; }
        public int IdPgeneral { get; set; }
    }
}
