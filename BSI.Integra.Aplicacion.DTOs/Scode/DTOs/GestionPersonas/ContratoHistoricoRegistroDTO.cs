using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class ContratoHistoricoRegistroDTO
    {
       public int? Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdPersonal_Jefe { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreDireccion { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public string PaisNacimiento { get; set; }
        public int? IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public int? IdSexo { get; set; }
        public string Sexo { get; set; }
        public int? IdEstadoCivil { get; set; }
        public string EstadoCivil { get; set; }
        public int? IdTipoEstudio { get; set; }
        public string TipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
        public string AreaFormacion { get; set; }
        public int? IdCentroEstudio { get; set; }
        public string CentroEstudio { get; set; }
        public int? IdTipoContrato { get; set; }
        public string TipoContrato { get; set; }
        public bool? EstadoContrato { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? RemuneracionFija { get; set; }
        public int? IdTipoPagoRemuneracion { get; set; }
        public string TipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinanciera_Pago { get; set; }
        public string EntidadFinanciera_Pago { get; set; }
        public string NumeroCuentaPago { get; set; }   
        public int? IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public int? IdSedeTrabajo { get; set; }
        public string SedeTrabajo { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string PersonalAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public int? IdTipoPerfil { get; set; }
        public string TipoPerfil { get; set; }
        public int? IdContratoEstado { get; set; }
        public string ContratoEstado { get; set; }
        public bool? Estado { get; set; }
        public decimal? Monto { get; set; }
        public string Concepto { get; set; }
        public string TipoRemuneracionVariable { get; set; }
    }

    public class ContratoHistoricoRegistroRDTO
    {
        public int? Id { get; set; }
        public int IdPersonal { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int? IdTipoContrato { get; set; }
        public string TipoContrato { get; set; }
        public bool? EstadoContrato { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? RemuneracionFija { get; set; }
        public int? IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public int? IdSedeTrabajo { get; set; }
        public string SedeTrabajo { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string PersonalAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public int? IdContratoEstado { get; set; }
        public string ContratoEstado { get; set; }
        public bool? Estado { get; set; }
        public List<ContratoHistoricoRegistroRVDTO> ListaRemuneracionVariable { get; set; }
    }

    public class ContratoHistoricoRegistroRVDTO
    {   
       // public int? id { get; set; }
        public decimal? Monto { get; set; }
        public string Concepto { get; set; }
        public string TipoRemuneracionVariable { get; set; }
    }
}
