using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConjuntoListaDetalleMailingDTO
    {
        public int? Id { get; set; }
        public int? IdRemitenteMailing { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public string Campania { get; set; }
        public string CodMailing { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPlantilla { get; set; }
        public int? Tipo { get; set; }
        public string Subject { get; set; }
        public List<FiltroPGeneralDTO> ProgramaPrincipal { get; set; }
        public List<FiltroPGeneralDTO> ProgramaSecundario { get; set; }
    }
}
