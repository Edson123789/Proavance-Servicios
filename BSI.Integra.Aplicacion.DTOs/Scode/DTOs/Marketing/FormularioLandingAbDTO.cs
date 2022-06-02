using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FormularioLandingAbDTO
    {
        public int? Id { get; set; }
        public string TextoFormulario { get; set; }
        public string NombrePrograma { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
        public List <SeccionFormularioAbDTO> ListaSeccionFormularioAB { get; set; }
    }
}
