using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatoPlanillaCreacionFurDTO
    {
        public int IdPersonal { get; set; }
        public int IdContrato { get; set; }
        public  string NombrePersonal { get; set; }
        public string AreaTrabajo { get; set; }
        public  decimal RemuneracionFija { get; set; }
        public DateTime? FechaInicioContratoAnterior { get; set; }
        public DateTime? FechaFinContratoAnterior { get; set; }
        public DateTime FechaInicioContrato { get; set; }
        public DateTime? FechaFinContrato  { get; set; }
        public bool EsTrabajadorContinuo { get; set; }
        public bool RecibeBeneficios { get; set; }
        public bool RecibeBeneficiosDesdeContratoAnterior { get; set; }
        public int? IdTipoContratoAnterior { get; set; }
        public int IdTipoContrato { get; set; }
        public  string TipoContrato { get; set; }
        public  bool TieneContratoVigente { get; set; }
        public  int NroMesesBeneficio { get; set; }
        public int NroDiasBeneficio { get; set; }
        public bool EsCesado { get; set; }
        public bool TieneBono { get; set; }
        public bool TieneAsignacionFamiliar { get; set; }
        public bool TieneSistemaPensionario { get; set; }
        public decimal BonoTotal { get; set; }
        public int IdProveedor { get; set; }
        public int IdSedeTrabajo { get; set; }
        public decimal PorcentajeSistemaPensionario { get; set; } 

    }
}
