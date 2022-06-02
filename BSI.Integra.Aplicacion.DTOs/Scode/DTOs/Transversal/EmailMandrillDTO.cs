using Mandrill.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EmailMandrillDTO
    {
        public string Asunto { get; set; }
        public string EtiquetaArchivoAdjunto { get; set; }
        public string CuerpoHTML { get; set; }
        public int IdConjuntoListaResultado { get; set; }
    }

    public class PlantillaEmailMandrillDTO
    {
        public string Asunto { get; set; }
        public string CuerpoHTML { get; set; }
        public List<EmailAttachment> ListaArchivosAdjuntos { get; set; }

        public PlantillaEmailMandrillDTO() {
            Asunto = "";
            CuerpoHTML = "";
            ListaArchivosAdjuntos = new List<EmailAttachment>();
        }
    }

    public class PlantillaWhatsAppCalculadoDTO
    {
        public string Plantilla { get; set; }
        public List<datoPlantillaWhatsApp> ListaEtiquetas { get; set; }
        public PlantillaWhatsAppCalculadoDTO() {
            Plantilla = "";
            ListaEtiquetas = new List<datoPlantillaWhatsApp>();
        }
    }

    public class PlantillaSmsCalculadoDTO
    {
        public string Cuerpo { get; set; }
        public PlantillaSmsCalculadoDTO()
        {
            Cuerpo = string.Empty;
        }
    }

    public class ContenidoSmsUsuarioDTO
    {
        public int IdOportunidad { get; set; }
        public string Cuerpo { get; set; }
        public string Usuario { get; set; }
    }
}
