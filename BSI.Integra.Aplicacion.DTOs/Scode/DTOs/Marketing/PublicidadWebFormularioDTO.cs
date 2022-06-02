using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PublicidadWebFormularioDTO
    {
        public int Id { get; set; }
        public int? IdPublicidadWeb { get; set; }
        public int IdFormularioSolicitudTextoBoton { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string TextoBoton { get; set; }
    }
}
