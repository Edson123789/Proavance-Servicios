using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class TipoCambioMonedaBO : BaseBO
    {
        private double _monedaAdolar { get; set; }
        private double _dolarAmoneda { get; set; }
        private DateTime _fecha { get; set; }
        private int _idMoneda { get; set; }
        public double MonedaAdolar { get { return _monedaAdolar; } set { ClearErrorFromProperty("MonedaADolar", ErrorInfo.Codigos.Obligatorio); _monedaAdolar = value; } }
        public double DolarAmoneda { get { return _dolarAmoneda; } set { ClearErrorFromProperty("DolarAMoneda", ErrorInfo.Codigos.Obligatorio); _dolarAmoneda = value; } }
        public DateTime Fecha { get { return _fecha; } set { ClearErrorFromProperty("Fecha", ErrorInfo.Codigos.Obligatorio); _fecha = value; } }
        public int IdMoneda { get { return _idMoneda; } set { ClearErrorFromProperty("IdMoneda", ErrorInfo.Codigos.Obligatorio); _idMoneda = value; } }
        public int? IdTipoCambioCol { get; set; }
        public int? IdTipoCambio { get; set; }
        public int? IdMigracion { get; set; }

        public TipoCambioMonedaBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            this.InicializarValidadoresGenerales(this.GetType().Name, this.GetType());
        }

        public void InicializarValidadoresGenerales(string nombreClass, Type entidad)
        {
            //ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("MonedaAdolar").Name, "Moneda a dolar");
            //ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("DolarAMoneda").Name, "Dolar a moneda");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("Fecha").Name, "Fecha");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdMoneda").Name, "Id Moneda");
        }
    }
}
