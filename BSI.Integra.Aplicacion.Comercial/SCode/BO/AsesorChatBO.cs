using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    /// BO: AsesorChat
    /// Autor: Gian Miranda
    /// Fecha: 15/04/2021
    /// <summary>
    /// BO para el mapeo y logica de negocios de la tabla com.T_AsesorChat
    /// </summary>
    public class AsesorChatBO : BaseBO
    {
        /// Propiedades	             Significado
        /// -----------	             ------------
        /// IdPersonal               Id del personal asignado al chat (PK de la tabla gp.T_Personal)
        /// NombreAsesor             Nombre del personal asignado al chat
        /// IdMigracion              Id de la migracion de V3 (campo nullable)
        public int? IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public Guid? IdMigracion { get; set; }

        public AsesorChatBO() {
        }
    }
}
