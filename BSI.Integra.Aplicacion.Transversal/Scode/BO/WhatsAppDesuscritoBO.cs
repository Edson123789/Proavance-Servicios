using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: WhatsAppDesuscrito
    ///Autor: Fischer Valdez
    ///Fecha: 07/05/2021
    ///<summary>
    ///Columnas y funciones de la tabla mkt.T_WhatsAppDesuscrito
    ///</summary>
    public class WhatsAppDesuscritoBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///NumeroTelefono                           Numero de telefono desuscrito
        ///Descripcion                              Descripcion de desuscripcion del WhatsApp
        ///EsActivo                                 Flag para determinar si el registro se encuentra activo segun las validaciones
        ///EsMigracion                              Flag para determinar si el registro resulta de la migracion
        ///IdMigracion                              Id de la migracion (campo nullable)

        public string NumeroTelefono { get; set; }
        public string Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
