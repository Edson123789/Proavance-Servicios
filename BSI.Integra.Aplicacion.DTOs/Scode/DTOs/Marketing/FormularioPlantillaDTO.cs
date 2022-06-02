using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FormularioPlantillaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public int IdPlantillaLandingPage { get; set; }
        public int IdFormularioSolicitud { get; set; }
        public int IdFormularioLandingPage { get; set; }
        public string NombrePlantillaLandingPage { get; set; }
        public int IdFormularioSolicitudTextoBoton { get; set; }
        public string TextoFormulario { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public bool? TituloProgramaAutomatico { get; set; }
        public bool? DescripcionWebAutomatico { get; set; }
        public List<CampoFormularioDTO> Campos { get; set; }
    }
}
