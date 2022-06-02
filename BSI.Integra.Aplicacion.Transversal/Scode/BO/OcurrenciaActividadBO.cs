using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class OcurrenciaActividadBO : BaseBO
    {
        public int IdOcurrencia { get; set; }
        public int IdActividadCabecera
        {
            get { return _idActividadCabecera; }
            set
            {
                ValidarValorMayorCeroProperty(this.GetType().Name, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdActividadCabecera").Name,
                                                  "Identificador de la Actividad Cabecera", value);
                _idActividadCabecera = value;
            }
        }
        public bool? PreProgramada { get; set; }
        public int? IdOcurrenciaActividadPadre { get; set; }
        public bool NodoPadre { get; set; }

        private int _idActividadCabecera;

        public OcurrenciaActividadBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
