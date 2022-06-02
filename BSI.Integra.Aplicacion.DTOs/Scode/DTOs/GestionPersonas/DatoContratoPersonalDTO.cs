using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class DatoContratoPersonalDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdTipoContrato { get; set; }
        public bool EstadoContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal RemuneracionFija { get; set; }
        public int IdTipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinancieraPago { get; set; }
        public string NumeroCuentaPago { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdSedeTrabajo { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdCargo { get; set; }
        public int IdTipoPerfil { get; set; }
        public int? IdPersonalJefe { get; set; }
        public int? IdEntidadFinancieraCts { get; set; }
        public string NumeroCuentaCts { get; set; }
        public bool? EsPeridoPrueba { get; set; }
        public DateTime? FechaFinPeriodoPrueba { get; set; }
        public int? IdContratoEstado { get; set; }
        public int? IdMigracion { get; set; }
        public string Usuario { get; set; }
        public string UrlDocumentoContrato { get; set; }
        public List<RemuneracionVariableDTO> ListaRemuneracionVariable {get; set;}
    }

    public class RemuneracionVariableDTO
    { 
        public string TipoRemuneracionVariable { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }

    }
}
