using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DataCreditoDataCuentaAhorroBO : BaseBO
    {
        public int IdDataCreditoBusqueda { get; set; }
        public bool? Bloqueada { get; set; }
        public string Entidad { get; set; }
        public string Numero { get; set; }
        public DateTime? FechaApertura { get; set; }
        public string Calificacion { get; set; }
        public string SituacionTitular { get; set; }
        public string Oficina { get; set; }
        public string Ciudad { get; set; }
        public string CodigoDaneCiudad { get; set; }
        public int? TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Sector { get; set; }
        public string CaracteristicaClase { get; set; }
        public string ValorMoneda { get; set; }
        public DateTime? ValorFecha { get; set; }
        public string ValorCalificacion { get; set; }
        public string EstadoCodigo { get; set; }
        public DateTime? EstadoFecha { get; set; }
        public string Llave { get; set; }
    }
}
