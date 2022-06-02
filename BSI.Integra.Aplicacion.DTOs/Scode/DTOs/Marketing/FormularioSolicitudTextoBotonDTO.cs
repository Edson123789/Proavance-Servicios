using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FormularioSolicitudTextoBotonDTO
    {
        public int Id { get; set; }
        public string TextoBoton { get; set; }
        public string Descripcion { get; set; }
        public bool PorDefecto { get; set; }
        public string Usuario { get; set; }

    }
}
