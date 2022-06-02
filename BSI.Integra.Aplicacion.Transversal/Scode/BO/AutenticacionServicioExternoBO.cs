using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: AutenticacionServicioExterno
    /// Autor: Gian Miranda
    /// Fecha: 30/07/2021
    /// <summary>
    /// BO para la logica de peticiones hacia AutenticacionServicioExterno
    /// </summary>
    public class AutenticacionServicioExternoBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// Nombre		                        Nombre orientativo de la credencial insertada
        /// Descripcion                         Descripcion de la credencial insertada
        /// Valor		                        Valor de la credencial insertada
        /// IdMigracion		                    Id de migracion (campo nullable)
        
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
        public int? IdMigracion { get; set; }
    }
}
