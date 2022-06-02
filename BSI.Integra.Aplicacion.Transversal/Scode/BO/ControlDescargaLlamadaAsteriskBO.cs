using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/AsteriskCdrBO
    /// Autor: Ansoli Deyvis
    /// Fecha: 26-01-2021
    /// <summary>
    /// BO de la tabla T_ControlDescargaLlamadaAsterisk
    /// </summary>
    public class ControlDescargaLlamadaAsteriskBO : BaseBO
    {
        /// Propiedades	Significado
        /// -----------	------------
        /// IdLlamadaInicial		Id del CDR Inicial
        /// IdLlamadaFinal		Id del CDR Final
        /// DescargaCorrecta    Indica si la descarga fue correcta
        /// DescargaEnProceso    Indica si la descarga esta en progreso
        /// TieneError    Indica si la descarga tuvo un error
        /// MensajeError    Mensaje de Error

        public int IdLlamadaInicial { get; set; }
        public int IdLlamadaFinal { get; set; }
        public bool DescargaCorrecta { get; set; }
        public bool DescargaEnProceso { get; set; }
        public bool TieneError { get; set; }
        public string MensajeError { get; set; }
    }
}
