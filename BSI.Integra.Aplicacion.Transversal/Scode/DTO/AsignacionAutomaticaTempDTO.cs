using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.DTO
{
    public class AsignacionAutomaticaTempDTO
    {
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Fijo { get; set; }
        public string Movil { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public string Pais { get; set; }//Codigo Pais
        public string Ciudad { get; set; }
        public string Empresa { get; set; }
        public string Ip { get; set; }
        public string FechaRegistro { get; set; }
        public string HoraRegistro { get; set; }
        public string NombrePrograma { get; set; }
        public string CentroCosto { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdTipoDato { get; set; }
        public string IdOrigen { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public string Campania { get; set; }
        public int? IdCampania { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public string IdFaseOportunidadPortal { get; set; }//Guid
        public string ProveedorAsignado { get; set; }
        public int? IdTiempoCapacitacion { get; set; }

        public int? IdCategoriaOrigen { get; set; }//categoria Dato
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string UrlOrigen { get; set; }

        //public int? FKPagina { get; set; }
        public int? IdPagina { get; set; }
    }

    public class NombreCampaniaAsiAsignacionAutomaticaTempDTO
    {
        public string IdFaseOportunidadPortal { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public string NombreCampania { get; set; }
    }
}
