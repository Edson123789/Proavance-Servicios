using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PostulanteInformacionDatosDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string ApellidoPaterno { get; set; }
		public string ApellidoMaterno { get; set; }
		public string SedeTrabajo { get; set; }
		public string PuestoTrabajo { get; set; }
		public DateTime FechaExamen { get; set; }
		public string UrlPerfilFacebook { get; set; }
		public string UrlPerfilLinkedin { get; set; }
		public string Email { get; set; }
		public string Ciudad { get; set; }
		public int Edad { get; set; }
		public string Celular { get; set; }
		public bool? TieneHijo { get; set; }
		public int? CantidadHijo { get; set; }
		public int? IdProceso { get; set; }
		public string Proceso { get; set; }
		public string ListaEtapa { get; set; }
		public string ListaEstados { get; set; }
		public int? IdConvocatoriaPersonal { get; set; }
		public string NombreConvocatoriaPersonal { get; set; }
	}

	public class PostulanteReporteFiltroDTO
	{
		public List<int> ListaPostulante { get; set; }
		public List<int> ListaPuestoTrabajo { get; set; }
		public List<int> ListaSede { get; set; }
		public DateTime? FechaInicio { get; set; }
		public DateTime? FechaFin { get; set; }
		public bool Check { get; set; }
	}

	public class PostulanteFormacionDTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public string CentroEstudio { get; set; }
		public string TipoEstudio { get; set; }
		public string AreaFormacion { get; set; }
		public string EstadoEstudio { get; set; }
		public DateTime? FechaInicio { get; set; }
		public DateTime? FechaFin { get; set; }
		public bool? AlaActualidad { get; set; }
		public string TurnoEstudio { get; set; }
	}
	public class PostulanteIdiomaDTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public string Idioma { get; set; }
		public string NivelIdioma { get; set; }
	}
	public class PostulanteExperienciaDTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public string Empresa { get; set; }
		public string Cargo { get; set; }
		public string AreaTrabajo { get; set; }
		public string Industria { get; set; }
		public DateTime? FechaInicio { get; set; }
		public DateTime? FechaFin { get; set; }
		public string NombreJefe { get; set; }
		public string NumeroJefe { get; set; }
		public bool? AlaActualidad { get; set; }
		public bool? EsUltimoEmpleo { get; set; }
		public string Salario { get; set; }
		public string Funcion { get; set; }
		public int MesesExperiencia { get; set; }
	}
	public class PostulanteEquipoComputoDTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public string TipoEquipo { get; set; }
		public string MemoriaRam { get; set; }
		public string SistemaOperativo { get; set; }
		public string Procesador { get; set; }
		public bool Mouse { get; set; }
		public bool Auricular { get; set; }
		public bool Camara { get; set; }
		public bool? EsEquipoTrabajo { get; set; }
	}
	public class PostulanteConexionInternetDTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public string TipoConexion { get; set; }
		public string MedioConexion { get; set; }
		public string VelocidadInternet { get; set; }
		public string ProveedorInternet { get; set; }
		public string CostoInternet { get; set; }
		public string ConexionCompartida { get; set; }
	}

	public class PostulanteInformacionVisualDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string ApellidoPaterno { get; set; }
		public string ApellidoMaterno { get; set; }
		public int? Edad { get; set; }
		public string Celular { get; set; }
		public string Email { get; set; }
		public string Ciudad { get; set; }
		public string UrlPerfilFacebook { get; set; }
		public string UrlPerfilLinkedin { get; set; }
		public bool? TieneHijo { get; set; }
		public int? CantidadHijo { get; set; }
	}
	public class PostulanteFormacionV2DTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public int IdCentroEstudio { get; set; }
		public int IdTipoEstudio { get; set; }
		public int? IdAreaFormacion { get; set; }
		public int? IdEstadoEstudio { get; set; }
		public DateTime? FechaInicio { get; set; }
		public DateTime? FechaFin { get; set; }
		public string OtraInstitucion { get; set; }
		public string OtraCarrera { get; set; }
		public bool? AlaActualidad { get; set; }
		public string TurnoEstudio { get; set; }
		public int? IdPais { get; set; }

	}
	public class PostulanteFormacionFormularioDTO
	{
		public PostulanteFormacionV2DTO FormacionPostulante { get; set; }
		public string Usuario { get; set; }
	}
	public class PostulanteExperienciaV2DTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public int? IdEmpresa { get; set; }
		public string OtraEmpresa { get; set; }
		public int? IdCargo { get; set; }
		public int? IdAreaTrabajo { get; set; }
		public int? IdIndustria { get; set; }
		public DateTime? FechaInicio { get; set; }
		public DateTime? FechaFin { get; set; }
		public string NombreJefe { get; set; }
		public string NumeroJefe { get; set; }
		public int? IdMigracion { get; set; }
		public bool? AlaActualidad { get; set; }
		public bool? EsUltimoEmpleo { get; set; }
		public decimal? Salario { get; set; }
		public string Funcion { get; set; }
		public decimal? SalarioComision { get; set; }
		public int? IdMoneda { get; set; }

	}
	public class PostulanteExperienciaFormularioDTO
	{
		public PostulanteExperienciaV2DTO ExperienciaPostulante { get; set; }
		public string Usuario { get; set; }
	}
	public class PostulanteFormacionLogDTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public string Pais { get; set; }
		public string CentroEstudio { get; set; }
		public string OtraInstitucion { get; set; }
		public string TipoEstudio { get; set; }
		public string EstadoEstudio { get; set; }
		public string AreaFormacion { get; set; }
		public string OtraCarrera { get; set; }
		public string FechaInicio { get; set; }
		public string FechaFin { get; set; }
		public string AlaActualidad { get; set; }
		public string TurnoEstudio { get; set; }
		public string TipoActualizacion { get; set; }
		public string FechaModificacion { get; set; }
		public string UsuarioModificacion { get; set; }
	}
	public class PostulanteExperienciaLogDTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public string Empresa { get; set; }
		public string OtraEmpresa { get; set; }
		public string Cargo { get; set; }
		public string AreaTrabajo { get; set; }
		public string Industria { get; set; }
		public string EsUltimoEmpleo { get; set; }
		public string Salario { get; set; }
		public string SalarioComision { get; set; }
		public string Moneda { get; set; }
		public string FechaInicio { get; set; }
		public string FechaFin { get; set; }
		public string AlaActualidad { get; set; }
		public string NombreJefe { get; set; }
		public string NumeroJefe { get; set; }
		public string Funcion { get; set; }
		public string TipoActualizacion { get; set; }
		public string FechaModificacion { get; set; }
		public string UsuarioModificacion { get; set; }
	}
	public class PostulanteLogDTO
    {
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public string Valor { get; set; }
		public string FechaModificacion { get; set; }
		public string UsuarioModificacion { get; set; }
	}

	public class PostulanteProcesoNuevoDTO
	{
		public int? IdProcesoSeleccionOrigen { get; set; }
		public int? IdProcesoSeleccionDestino { get; set; }
		public int IdPostulante { get; set; }
		public List<string> IdsProcesoSeleccionEtapa { get; set; }
		public string Usuario { get; set; }
	}
}
