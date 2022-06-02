using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TipoDatoMetaDTO
    {
        public int Id { get; set; }
        public int IdFaseOportunidadDestino { get; set; }
        public int IdFaseOportunidadOrigen { get; set; }
        public int IdTipoDato { get; set; }
        public int Meta { get; set; }
        public int MetaGlobal { get; set; }
        public string Usuario { get; set; }
    }
}
