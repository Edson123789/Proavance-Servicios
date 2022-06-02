using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CriterioDocSeleccionarDTO
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 
        public bool ModalidadPresencial { get; set; } 
        public bool ModalidadOnline { get; set; } 
        public bool ModalidadAonline { get; set; } 
    }
}
