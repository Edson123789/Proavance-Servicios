using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PrioridadesDTO
    {
        public int Id { get; set; }
        public int? IdCampaniaMailing { get; set; }
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
        public int? CantidadSubidosMailChimp { get; set; }
        public int? IdCentroCosto { get; set; }
        public bool? EsSubidaManual { get; set; }

        public List<CampaniaMailingDetalleProgramaDTO> ProgramasPrincipales { get; set; }
        public List<CampaniaMailingDetalleProgramaDTO> ProgramasSecundarios { get; set; }
        public List<CampaniaMailingDetalleProgramaDTO> ProgramasFiltro { get; set; }
        public List<int> Areas { get; set; }
        public List<int> SubAreas { get; set; }
    }

    public class WhatsAppEstadoRecuperacionDTO
    {
        public int Id { get; set; }
        public int IdModuloSistema { get; set; }
        public string Tipo { get; set; }
        public bool Habilitado { get; set; }
    }

    public class CampaniaGeneralDetalleEstadoEnEjecucionDTO
    {
        public int IdCampaniaGeneral { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public bool EnEjecucion { get; set; }
    }

    public class PrioridadesCampaniaGeneralDetalleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdCampaniaGeneral { get; set; }
        public int Prioridad { get; set; }
        public string Asunto { get; set; }
        public int IdPersonal { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? CantidadContactosMailing { get; set; }
        public int? CantidadContactosWhatsapp { get; set; }
        public int? CantidadSubidosMailChimp { get; set; }
        public bool? EnEjecucion { get; set; }
        public bool? NoIncluyeWhatsaap { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public List<CampaniaGeneralDetalleProgramaDTO> ProgramasFiltro { get; set; }
        public List<CampaniaGeneralDetalleResponsableDTO> Responsables { get; set; }
        public List<int> Areas { get; set; }
        public List<int> SubAreas { get; set; }
    }

    public class PrioridadCampaniaGeneralConjuntoEjecucionDTO
    {
        public int IdCampaniaGeneral { get; set; }
        public string Usuario { get; set; }
    }

    public class PrioridadMailingEjecucionDTO
    {
        public int IdCampaniaGeneralDetalle { get; set; }
        public string Usuario { get; set; }
    }

    public class PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO
    {
        public int IdCampaniaGeneralDetalle { get; set; }
        public string Usuario { get; set; }
        public List<InformacionPreprocesamientoWhatsAppCampaniaGeneralDTO> ListaResponsableReal { get; set; }
    }

    public class InformacionPreprocesamientoWhatsAppCampaniaGeneralDTO
    {
        public int IdResponsable { get; set; }
        public int Total { get; set; }
    }
}
