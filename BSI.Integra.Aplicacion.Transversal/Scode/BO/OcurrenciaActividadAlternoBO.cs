using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class OcurrenciaActividadAlternoBO : BaseBO
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
        public int? IdPlantillaSpeech { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdActividadCabeceraProgramada { get; set; }
        public string Roles { get; set; }

        private int _idActividadCabecera;

        public OcurrenciaActividadAlternoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
