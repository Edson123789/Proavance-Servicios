using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PuestoTrabajoDTO
	{
		public int Id { get; set; }
		public int? IdPersonalAreaTrabajo { get; set; }
		public string PersonalAreaTrabajo { get; set; }
		public string Nombre { get; set; }
		public int? IdPerfilPuestoTrabajo { get; set; }
		public string Objetivo { get; set; }
		public string Descripcion { get; set; }
	}
	public class CompuestoPuestoTrabajo
	{
		public PuestoTrabajoDTO PuestoTrabajo { get; set; }
		public string Usuario { get; set; }
	}


	public class PuestoTrabajoDependenciaDTO
	{
		public int IdPuestoTrabajoDependencia { get; set; }
		public string PuestoTrabajoDependencia { get; set; }
	}
	public class PuestoTrabajoRelacionInternaDTO
	{
		public int IdPuestoTrabajoRelacionInterna { get; set; }
		public string PuestoTrabajoRelacionInterna { get; set; }

    }
	public class PuestoTrabajoPuestoACargoDTO
	{
		public int IdPuestoTrabajoPuestoACargo { get; set; }
		public string PuestoTrabajoPuestoACargo { get; set; }

    }
	public class PuestoTrabajoRelacionCompuestoDTO
	{
		public int Id { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
		public List<FiltroIdNombrePKDTO> ListaPuestoDependencia { get; set; }
		public List<FiltroIdNombrePKDTO> ListaPuestoRelacionInterna { get; set; }
		public List<FiltroIdNombrePKDTO> ListaPuestoACargo { get; set; }
	}
	
	public class PuestoTrabajoRelacionDTO
	{
		public int Id { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
	}
	public class PuestoTrabajoRelacionDetalleDTO
	{
		public int Id { get; set; }
		public int IdPuestoTrabajoRelacionDetalle { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
		public int? IdPuestoTrabajo_Dependencia { get; set; }
		public int? IdPuestoTrabajo_PuestoACargo { get; set; }
		public int? IdPersonalAreaTrabajo { get; set; }

		public string PuestoTrabajo_Dependencia { get; set; }
		public string PuestoTrabajo_PuestoACargo { get; set; }
		public string PersonalAreaTrabajo { get; set; }
	}

	public class PuestoTrabajoFuncionDTO
	{
		public int Id { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
		public int NroOrden { get; set; }
		public string Funcion { get; set; }
		public int IdPersonalTipoFuncion { get; set; }
		public string PersonalTipoFuncion { get; set; }
		public int IdFrecuenciaPuestoTrabajo { get; set; }
		public string FrecuenciaPuestoTrabajo { get; set; }
		public int Version{ get; set; }
	}
	public class PuestoTrabajoReporteDTO
	{
		public int Id { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
		public int NroOrden { get; set; }
		public string Reporte { get; set; }
		public int IdFrecuenciaPuestoTrabajo { get; set; }
		public string FrecuenciaPuestoTrabajo { get; set; }
		public int Version { get; set; }

	}
	public class PuestoTrabajoCursoComplementarioDTO
	{
		public int Id { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
		public int IdTipoCompetenciaTecnica { get; set; }
		public int IdCompetenciaTecnica { get; set; }
		public int IdNivelCompetenciaTecnica { get; set; }
		public string TipoCompetenciaTecnica { get; set; }
		public string CompetenciaTecnica { get; set; }
		public string NivelCompetenciaTecnica { get; set; }
		public int Version { get; set; }
	}
	public class PuestoTrabajoExperienciaDTO
	{
		public int Id { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
		public int IdExperiencia { get; set; }
		public int IdTipoExperiencia { get; set; }
		public string Experiencia { get; set; }
		public string TipoExperiencia { get; set; }
		public int NumeroMinimo { get; set; }
		public string Periodo { get; set; }
		public int Version { get; set; }

	}
	public class PuestoTrabajoCaracteristicaPersonalDTO
	{
		public int Id { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
		public int? EdadMinima { get; set; }
		public int? EdadMaxima { get; set; }
		public int IdSexo { get; set; }
		public int IdEstadoCivil { get; set; }
		public string Sexo { get; set; }
		public string EstadoCivil { get; set; }
		public int Version { get; set; }
	}

	public class PuestoTrabajoFormacionAcademicaFiltroDTO
	{
		public int Id { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
		public string IdTipoFormacion { get; set; }
		public string IdNivelEstudio { get; set; }
		public string IdAreaFormacion { get; set; }
		public string IdCentroEstudio { get; set; }
		public string IdGradoEstudio { get; set; }

		//public string TipoFormacion { get; set; }
		//public string NivelEstudio { get; set; }
		//public string AreaFormacion { get; set; }
		//public string CentroEstudio { get; set; }
		//public string GradoEstudio { get; set; }

	}

	public class PuestoTrabajoFormacionAcademicaDTO
	{
		public int Id { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
		public List<int> IdTipoFormacion { get; set; }
		public List<int> IdNivelEstudio { get; set; }
		public List<int> IdAreaFormacion { get; set; }
		public List<int> IdCentroEstudio { get; set; }
		public List<int> IdGradoEstudio { get; set; }

		//public string TipoFormacion { get; set; }
		//public string NivelEstudio { get; set; }
		//public string AreaFormacion { get; set; }
		//public string CentroEstudio { get; set; }
		//public string GradoEstudio { get; set; }

	}

	public class PuestoTrabajoFormacionAgrupadoDTO
	{
		public int IdPerfilPuestoTrabajo { get; set; }
		public int Version { get; set; }
		public List<TipoFormacionAcademicaDTO> ListaTipoFormacion { get; set; }
		public List<FiltroIdNombreDTO> ListaNivelEstudio { get; set; }
		public List<FiltroIdNombreDTO> ListaAreaFormacion { get; set; }
		public List<FiltroIdNombreDTO> ListaCentroEstudio { get; set; }
		public List<FiltroIdNombreDTO> ListaGradoEstudio { get; set; }
	}

	public class PerfilPuestoTrabajoInsertarActualizarDTO
	{
		public int IdPuestoTrabajo { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
		public string Descripcion { get; set; }
		public string Objetivo { get; set; }

		public bool EstadoPuestoTrabajoCaracteristicaPersonal { get; set; }
		public bool EstadoPuestoTrabajoCursoComplementario { get; set; }
		public bool EstadoPuestoTrabajoExperiencia { get; set; }
		public bool EstadoPuestoTrabajoFormacionAcademica { get; set; }
		public bool EstadoPuestoTrabajoFuncion { get; set; }
		public bool EstadoPuestoTrabajoRelacion { get; set; }
		public bool EstadoPuestoTrabajoReporte { get; set; }

		public List<PuestoTrabajoCaracteristicaPersonalDTO> PuestoTrabajoCaracteristicaPersonal { get; set; }
		public List<PuestoTrabajoCursoComplementarioDTO> PuestoTrabajoCursoComplementario { get; set; }
		public List<PuestoTrabajoExperienciaDTO> PuestoTrabajoExperiencia { get; set; }
		public List<PuestoTrabajoFormacionAcademicaDTO> PuestoTrabajoFormacion { get; set; }
		public List<PuestoTrabajoFuncionDTO> PuestoTrabajoFuncion { get; set; }
		public List<PuestoTrabajoRelacionCompuestoDTO> PuestoTrabajoRelacion { get; set; }
		public List<PuestoTrabajoReporteDTO> PuestoTrabajoReporte { get; set; }
		public PuestoTrabajoPuntajeEvaluacionAgrupadaComponenteDTO Puntaje { get; set; }
		public bool CrearNuevaVersion { get; set; }
		public string Usuario { get; set; }
		public bool EsUsuarioAprobacion { get; set; }
		public int IdPersonal { get; set; }
	}


	public class PuestoTrabajoPorFechaDTO
	{
		public int Id { get; set; }
		public int? IdPersonalAreaTrabajo { get; set; }
		public string PersonalAreaTrabajo { get; set; }
		public string Nombre { get; set; }
		public int? IdPerfilPuestoTrabajo { get; set; }
		public string Objetivo { get; set; }
		public string Descripcion { get; set; }
		public DateTime? PuestoTrabajoFechaModificacion { get; set; }
		public DateTime? PerfilPuestoTrabajoFechaModificacion { get; set; }
		public DateTime? PersonalAreaFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoCaracteristicaPersonalFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoCursoComplementarioFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoExperienciaFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoFormacionAcademicaFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoFuncionFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoRelacionFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoRelacionDetalleFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoReporteFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoPuntajeCalificacionFechaModificacion { get; set; }
		public DateTime? ModuloSistemaPuestoTrabajoFechaModificacion { get; set; }

		public string PuestoTrabajoUsuarioModificacion { get; set; }
		public string PerfilPuestoTrabajoUsuarioModificacion { get; set; }
		public string PersonalAreaUsuarioModificacion { get; set; }
		public string PuestoTrabajoCaracteristicaPersonalUsuarioModificacion { get; set; }
		public string PuestoTrabajoCursoComplementarioUsuarioModificacion { get; set; }
		public string PuestoTrabajoExperienciaUsuarioModificacion { get; set; }
		public string PuestoTrabajoFormacionAcademicaUsuarioModificacion { get; set; }
		public string PuestoTrabajoFuncionUsuarioModificacion { get; set; }
		public string PuestoTrabajoRelacionUsuarioModificacion { get; set; }
		public string PuestoTrabajoRelacionDetalleUsuarioModificacion { get; set; }
		public string PuestoTrabajoReporteUsuarioModificacion { get; set; }
		public string PuestoTrabajoPuntajeCalificacionUsuarioModificacion { get; set; }
		public string ModuloSistemaPuestoTrabajoUsuarioModificacion { get; set; }
	}

	public class PuestoTrabajoEnviarDTO
	{
		public int Id { get; set; }
		public int? IdPersonalAreaTrabajo { get; set; }
		public string PersonalAreaTrabajo { get; set; }
		public string Nombre { get; set; }
		public int? IdPerfilPuestoTrabajo { get; set; }
		public string Objetivo { get; set; }
		public string Descripcion { get; set; }
		public string UsuarioModificacion { get; set; }
		public string  FechaModificacion { get; set; }
	}

	public class PuestoFechaDTO
	{
		public int Id { get; set; }
		public int? IdPersonalAreaTrabajo { get; set; }
		public string PersonalAreaTrabajo { get; set; }
		public string Nombre { get; set; }
		public int? IdPerfilPuestoTrabajo { get; set; }
		public string Objetivo { get; set; }
		public string Descripcion { get; set; }
		public List<FechaModificacionDTO> ListaFechaModificacion { get; set; }
	}

	public class FechaModificacionDTO
	{
		public DateTime? PuestoTrabajoFechaModificacion { get; set; }
		public DateTime? PerfilPuestoTrabajoFechaModificacion { get; set; }
		public DateTime? PersonalAreaFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoCaracteristicaPersonalFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoCursoComplementarioFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoExperienciaFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoFormacionAcademicaFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoFuncionFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoRelacionFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoRelacionDetalleFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoReporteFechaModificacion { get; set; }
		public DateTime? PuestoTrabajoPuntajeCalificacionFechaModificacion { get; set; }
		public DateTime? ModuloSistemaPuestoTrabajoFechaModificacion { get; set; }


		public string PuestoTrabajoUsuarioModificacion { get; set; }
		public string PerfilPuestoTrabajoUsuarioModificacion { get; set; }
		public string PersonalAreaUsuarioModificacion { get; set; }
		public string PuestoTrabajoCaracteristicaPersonalUsuarioModificacion { get; set; }
		public string PuestoTrabajoCursoComplementarioUsuarioModificacion { get; set; }
		public string PuestoTrabajoExperienciaUsuarioModificacion { get; set; }
		public string PuestoTrabajoFormacionAcademicaUsuarioModificacion { get; set; }
		public string PuestoTrabajoFuncionUsuarioModificacion { get; set; }
		public string PuestoTrabajoRelacionUsuarioModificacion { get; set; }
		public string PuestoTrabajoRelacionDetalleUsuarioModificacion { get; set; }
		public string PuestoTrabajoReporteUsuarioModificacion { get; set; }
		public string PuestoTrabajoPuntajeCalificacionUsuarioModificacion { get; set; }
		public string ModuloSistemaPuestoTrabajoUsuarioModificacion { get; set; }
	}

	public class FechaUsuarioDTO
	{
		public DateTime Fecha { get; set; }
		public string Usuario { get; set; }		
	}


	public class PuestoTrabajoModuloSistemaDTO
	{
		public int Id { get; set; }
		public int IdModuloSistema { get; set; }
		public string ModuloSistema { get; set; }
		public string IdModuloSistemaGrupo { get; set; }
		public string ModuloSistemaGrupo { get; set; }
		public bool Estado { get; set; }
		public string Url { get; set; }
		public int? IdTipo { get; set; }
		public string NombreTipo { get; set; }
	}

	public class ModuloSistemaModuloGrupoDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string IdModuloSistemaGrupo { get; set; }
		public string ModuloSistemaGrupo { get; set; }
		public string Url { get; set; }
		public int? IdTipo { get; set; }
		public string NombreTipo { get; set; }		
	}

	public class ValidarAsignacionDTO
	{
		public int Id { get; set; }
		public int IdModuloSistema { get; set; }
		public string ModuloSistema { get; set; }
		public string IdModuloSistemaGrupo { get; set; }
		public string ModuloSistemaGrupo { get; set; }
		public bool Estado { get; set; }
		public bool Modificacion { get; set; }
		public string Url { get; set; }
	}
	public class AsignarInterfazDTO
	{
		public int Id { get; set; }
		public string Usuario { get; set; }
		public List<ValidarAsignacionDTO> ListaAsignacion { get; set; }
	}
}
