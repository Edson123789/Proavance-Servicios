using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroBandejaCorreoDTO
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int IdAsesor { get; set; }
        public string Folder { get; set; }
        public string TipoCorreos { get; set; }
        public GridFiltersDTO FiltroKendo { get; set; }
    }

    public class PersonalBandejaCorreoDTO
    {
        public int IdAsesor { get; set; }
        public string Folder { get; set; }
        public List<gmailCredenciales> ListaCorreos { get; set; }
    }
    public class CriterioReporteSeguimientoDTO
    {
        public int Id { get; set; }
        public string Sigla { get; set; }
    }
}
