using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ListaCrepsDTO
    {
        public List<CrepListaCuotasSeleccionadasDTO> lista { get; set; }
        public CrepCabeceraDTO objeto { get; set; }
        public List<CrepListaAlumnosDTO> listaalumnos { get; set; }
    }
}
