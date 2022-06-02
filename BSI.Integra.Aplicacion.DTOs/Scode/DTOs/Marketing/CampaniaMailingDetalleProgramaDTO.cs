using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CampaniaMailingDetalleProgramaDTO
    {
        public int? Id { get; set; }
        public int? IdCampaniaMailingDetalle { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public int? Orden { get; set; }
    }
    public class CampaniaGeneralDetalleProgramaDTO
    {
        public int? Id { get; set; }
        public int? IdCampaniaGeneralDetalle { get; set; }
        public int? IdPgeneral { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public int? Orden { get; set; }
    }
    public class CampaniaGeneralDetalleResponsableDTO
    {
        public int? Id { get; set; }
        public int? IdCampaniaGeneralDetalle { get; set; }
        public int? IdResponsable { get; set; }
        public int? Dia1 { get; set; }
        public int? Dia2 { get; set; }
        public int? Dia3 { get; set; }
        public int? Dia4 { get; set; }
        public int? Dia5 { get; set; }
        public int? Total { get; set; }

    }
}
