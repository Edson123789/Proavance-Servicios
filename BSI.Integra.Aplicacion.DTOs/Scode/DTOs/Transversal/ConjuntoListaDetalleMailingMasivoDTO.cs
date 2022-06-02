using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConjuntoListaDetalleMailingMasivoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdConjuntoListaDetalle { get; set; }//Id
        public int? IdPlantilla { get; set; }
    }
}
