using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: ClasificacionPersonaBO
    ///Autor: Jose Villena. - Gian Miranda
    ///Fecha: 28/04/2021
    ///<summary>
    ///Columnas y funciones de la tabla conf.T_ClasificacionPersona
    ///</summary>
    public class ClasificacionPersonaBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///IdPersona                                Id de persona (PK de la tabla gp.T_Persona)
        ///IdTipoPersona                            Id del tipo de persona (PK de conf.T_TipoPersona)
        ///IdTablaOriginal                          Id de la tabla original
        ///IdMigracion                              Id de migración (campo nullable)

        public int IdPersona { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdTablaOriginal { get; set; }
        public int? IdMigracion { get; set; }

        public ClasificacionPersonaBO()
        {
        }

        public ClasificacionPersonaBO(integraDBContext integraDBContext)
        {
        }
    }
}
