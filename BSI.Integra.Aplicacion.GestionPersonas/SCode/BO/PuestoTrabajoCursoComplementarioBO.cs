using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///BO: PuestoTrabajoCursoComplementarioBO
	///Autor: Edgar S.
	///Fecha: 27/01/2021
	///<summary>
	///Columnas de la tabla T_PuestoTrabajoCursoComplementario
	///</summary>
	public class PuestoTrabajoCursoComplementarioBO : BaseBO
    {
		///Propiedades					Significado
		///-------------				-----------------------
		///IdPerfilPuestoTrabajo		FK de T_PerfilPuestoTrabajo
		///IdTipoCompetenciaTecnica		FK de T_TipoCompetenciaTecnica	
		///IdCompetenciaTecnica			FK de T_CompetenciaTecnica
		///IdNivelCompetenciaTecnica	FK de T_NivelCompetenciaTecnica
		///IdMigracion					Id de Migración
		public int IdPerfilPuestoTrabajo { get; set; }
		public int IdTipoCompetenciaTecnica { get; set; }
		public int IdCompetenciaTecnica { get; set; }
		public int? IdNivelCompetenciaTecnica { get; set; }
		public int? IdMigracion { get; set; }
	}
}
