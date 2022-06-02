using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosMatriculaDTO
    {
        public string Id { get; set; }
        public int IdPEspecifico { get; set; }
        public string Moneda { get; set; }
        public decimal? TipoCambio { get; set; }
        public decimal TotalAPagar { get; set; }
        public int NroCuotas { get; set; }
        public string EstadoMatricula { get; set; }
        public int? Periodo { get; set; }
        public string Programa { get; set; }
        public string Coordinador { get; set; }
        public string Asesor { get; set; }
        public int? Paquete { get; set; }
        public string Titulo { get; set; }
        public string Observaciones { get; set; }
        public string EmpresaPaga { get; set; }
        public string EmpresaNombre { get; set; }
        public int IdCoordinador { get; set; }
        public int IdAsesor { get; set; }
    }
}
