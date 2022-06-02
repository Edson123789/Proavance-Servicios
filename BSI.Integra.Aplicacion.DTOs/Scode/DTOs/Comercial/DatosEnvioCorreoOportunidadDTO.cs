using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.DTOs.Comercial
{
    public class DatosEnvioCorreoOportunidadDTO
    {
        public int Id { get; set; }
        public string Nombre_Campanha { get; set; }
        public string Asunto { get; set; }
        public string Plantilla { get; set; }
        public string EmailAsesor { get; set; }
        public string Email1Contacto { get; set; }
        public string Email2Contacto { get; set; }
        public int id_categoria { get; set; }
        public int IdOportunidad { get; set; }
        public int CentrocostoId { get; set; }
        public int ActividadDetalleId { get; set; }
        public int ProgramaGeneralId { get; set; }
        public int ProgramaEspecificoId { get; set; }
        public string NombreProgEspecifico { get; set; }
        public string Remitente { get; set; }
        public int IdAsesorActual { get; set; }
        public int IdAsesorOriginal { get; set; }
    }
}
