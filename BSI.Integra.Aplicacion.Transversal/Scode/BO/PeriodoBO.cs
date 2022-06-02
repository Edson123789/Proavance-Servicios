using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PeriodoBO : BaseBO
    {
        private string _nombre { get; set; }
        private DateTime _fechaInicial { get; set; }
        private DateTime _fechaFin { get; set; }
        private DateTime _fechaInicialFinanzas { get; set; }
        private DateTime _fechaFinFinanzas { get; set; }
        public string Nombre { get { return _nombre; } set { ClearErrorFromProperty("Nombre", ErrorInfo.Codigos.Obligatorio); _nombre = value; } }
        //public DateTime FechaInicial { get { return _fechaInicial; } set => ClearErrorFromProperty("FechaInicial", ErrorInfo.Codigos.Obligatorio); }
        public DateTime FechaInicial { get { return _fechaInicial; } set { ClearErrorFromProperty("FechaInicial", ErrorInfo.Codigos.Obligatorio); _fechaInicial = value; } }
        public DateTime FechaFin { get { return _fechaFin; } set { ClearErrorFromProperty("FechaFin", ErrorInfo.Codigos.Obligatorio); _fechaFin = value; } }
        public DateTime FechaInicialFinanzas { get { return _fechaInicialFinanzas; } set { ClearErrorFromProperty("FechaInicialFinanzas", ErrorInfo.Codigos.Obligatorio); _fechaInicialFinanzas = value; } }
        public DateTime FechaFinFinanzas { get { return _fechaFinFinanzas; } set { ClearErrorFromProperty("FechaFinFinanzas", ErrorInfo.Codigos.Obligatorio); _fechaFinFinanzas = value; } }
        public Guid? IdMigracion { get; set; }

        public PeriodoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            this.InicializarValidadoresGenerales(this.GetType().Name, this.GetType());
        }
        public void InicializarValidadoresGenerales(string nombreClass, Type entidad)
        {
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("Nombre").Name, "Nombre periodo");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("FechaInicial").Name, "Fecha inicial");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("FechaFin").Name, "Fecha Fin");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("FechaInicialFinanzas").Name, "Fecha Inicial Finanzas");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("FechaFinFinanzas").Name, "Fecha Fin Finanzas");
        }
    }
}
