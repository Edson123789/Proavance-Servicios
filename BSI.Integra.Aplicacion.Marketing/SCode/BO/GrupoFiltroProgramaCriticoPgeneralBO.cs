using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    ///BO: GrupoFiltroProgramaCriticoPGeneral
    ///Autor: Gian Miranda
    ///Fecha: 23/04/2021
    ///<summary>
    ///BO de GrupoFiltroProgramaCriticoPGeneral, contiene las propiedades y logica de negocios
    ///</summary>
    public class GrupoFiltroProgramaCriticoPgeneralBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///IdGrupoFiltroProgramaCritico             Id del grupo de filtro programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)
        ///IdPGeneral                               Id del programa general (PK de la tabla pla.T_PGeneral)
        ///IdMigracion                              Id de migracion de V3 (campo nullable)
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public int IdPGeneral { get; set; }
        public int? IdMigracion { get; set; }
    }
}
