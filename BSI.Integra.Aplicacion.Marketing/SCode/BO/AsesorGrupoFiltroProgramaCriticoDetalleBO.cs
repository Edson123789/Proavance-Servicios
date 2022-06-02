using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    /// BO: Marketing/AsesorGrupoFiltroProgramaCriticoDetalle
    /// Autor: Gian Miranda
    /// Fecha: 21/01/2021
    /// <summary>
    /// BO para la interaccion con mkt.T_AsesorGrupoFiltroProgramaCriticoDetalle, tabla hija de mkt.T_AsesorCentroCosto
    /// </summary>
    public class AsesorGrupoFiltroProgramaCriticoDetalleBO : BaseBO
    {
        /// Propiedades	                    Significado
        /// -----------	                    ------------
        /// IdAsesorCentroCosto		        FK de la tabla mkt.T_AsesorCentroCosto
        /// IdGrupoFiltroProgramaCritico	FK de la tabla pla.T_GrupoFiltroProgramaCritico
        /// Prioridad		                Prioridad del registro en la tabla mkt.T_AsesorCentroCosto
        /// IdMigracion		                Id de Migracion, nulleable y campo obligatorio
        public int IdAsesorCentroCosto { get; set; }
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public int Prioridad { get; set; }
        public Guid? IdMigracion { get; set; }
        public AsesorGrupoFiltroProgramaCriticoDetalleBO()
        {
        }
    }
}
