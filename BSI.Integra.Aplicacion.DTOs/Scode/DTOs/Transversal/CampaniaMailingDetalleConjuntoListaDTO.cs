using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CampaniaMailingDetalleConjuntoListaDTO
    {
        public int Id { get; set; }
        public int Prioridad { get; set; }
        public int Tipo { get; set; }
        public int IdRemitenteMailing { get; set; }
        public int IdPersonal { get; set; }
        public string Subject { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int IdHoraEnvio { get; set; }
        public int Proveedor { get; set; }
        public int EstadoEnvio { get; set; }
        public int IdFiltroSegmento { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public string Campania { get; set; }
        public string CodMailing { get; set; }
        public int? CantidadContactos { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public int? IdConjuntoLista { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdCampaniaMailingDetallePrograma { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public string TipoPrograma { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
    }
}
