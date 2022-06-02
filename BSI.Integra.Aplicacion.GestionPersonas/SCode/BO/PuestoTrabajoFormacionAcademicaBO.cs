using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///BO: PuestoTrabajoFormacionAcademicaBO
	///Autor: Luis H, Edgar S.
	///Fecha: 27/01/2021
	///<summary>
	///Columnas de la tabla T_PuestoTrabajoFormacionAcademica
	///</summary>
	public class PuestoTrabajoFormacionAcademicaBO : BaseBO
    {
		///Propiedades				Significado
		///-------------			-----------------------
		///IdPerfilPuestoTrabajo    FK de T_PerfilPuestoTrabajo 
		///IdTipoFormacion			FK de T_TipoFormacion
		///IdNivelEstudio			FK de T_NivelEstudio
		///IdAreaFormacion			FK de T_AreaFormacion
		///IdCentroEstudio			FK de T_CentroEstudio
		///IdGradoEstudio			FK de T_GradoEstudio
		///IdMigracion				Ide de Migración
		public int IdPerfilPuestoTrabajo { get; set; }
		public string IdTipoFormacion { get; set; }
		public string IdNivelEstudio { get; set; }
		public string IdAreaFormacion { get; set; }
		public string IdCentroEstudio { get; set; }
		public string IdGradoEstudio { get; set; }
		public int? IdMigracion { get; set; }
	}
}
