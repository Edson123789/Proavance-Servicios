using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    /// BO: Marketing/AsesorCentroCostoBO
    /// Autor: Gian Miranda
    /// Fecha: 25/01/2021
    /// <summary>
    /// BO para la interaccion con mkt.T_AsesorCentroCosto
    /// </summary>
    public partial class AsesorCentroCostoBO : BaseBO
    {
        /// Propiedades	            Significado
        /// -----------	            ------------
        /// IdPersonal		        PK de la tabla mkt.T_AsesorCentroCosto
        /// AsignacionMax           Cantidad Maxima posible para asignacion
        /// Habilitado		        Verifica si un registro se encuentra habilitado
        /// IdMigracion		        Id de Migracion, nulleable y campo obligatorio
        /// AsignacionMin		    Cantidad Minima posible para asignacion
        /// AsignacionMaxBnc		Cantidad Maxima BNC posible para asignacion
        public int IdPersonal { get; set; }
        public int AsignacionMax { get; set; }
        public bool Habilitado { get; set; }
        public Guid? IdMigracion { get; set; }
        public int AsignacionMin { get; set; }
        public int AsignacionMaxBnc { get; set; }
        public string AsignacionPais { get; set; }

        public AsesorCentroCostoBO() {

        }
    }
}
