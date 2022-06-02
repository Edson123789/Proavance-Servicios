using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ListaPersonaTipoPersonaFiltroDTO
    {
        public int IdPersonaTipoPersona { get; set; }//considerando tipo persona, docente,alumno
        public string Nombre { get; set; }
        public int IdPEspecifico { get; set; }
    }
}
