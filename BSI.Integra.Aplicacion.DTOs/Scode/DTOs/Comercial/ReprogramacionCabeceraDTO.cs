using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReprogramacionCabeceraDTO
    {
        public int Id { get; set; }
        public int IdActividadCabecera { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int MaxReproPorDia { get; set; }
        public int IntervaloSigProgramacionMin { get; set; }
        public string Text_IdCategoriaOrigen { get; set; }
    }
}
