using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PostulanteFormularioDTO
	{
		public PostulanteInformacionDTO InformacionPostulante { get; set; }
		public string Usuario { get; set; }
	}
	public class PostulanteInformacionDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string ApellidoPaterno { get; set; }
		public string ApellidoMaterno { get; set; }
		public string NroDocumento { get; set; }
		public string Celular { get; set; }
		public string Email { get; set; }
		public int? IdTipoDocumento { get; set; }
		public int? IdPais { get; set; }
		public int? IdCiudad { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public int? IdPostulanteProcesoSeleccion { get; set; }
		public int? IdEstadoProcesoSeleccion { get; set; }
		public int? IdProcesoSeleccion { get; set; }
		public string ProcesoSeleccion { get; set; }
		public DateTime? FechaRendicionExamen { get; set; }
		public DateTime? FechaEnvioAccesos { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdPersonal_OperadorProceso { get; set; }
        public int? IdConvocatoriaPersonal { get; set; }
        public int? IdProcesoSeleccionEtapa { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public string IdRespuestas { get; set; }
        public List<int> ListaRespuestaDesaprobatoria { get; set; }
        public int? IdSexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? CantidadHijo { get; set; }
        public string UrlPerfilFacebook { get; set; }
        public string UrlPerfilLinkedin { get; set; }
    }

    public class PostulanteAccesoProcesoSeleccionDTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public string Postulante { get; set; }
		public string Dni { get; set; }
		public string Email { get; set; }
		public string ProcesoSeleccion { get; set; }
		public string Token { get; set; }
		public Guid? GuidAccess { get; set; }
		public int? IdPais { get; set; }
		public string Celular { get; set; }
	}
    public class PostulanteFormularioV2DTO
    {
        public PostulanteInformacionV2DTO InformacionPostulante { get; set; }
        public string Usuario { get; set; }
    }

    public class PostulanteInformacionV2DTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NroDocumento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdPostulanteProcesoSeleccion { get; set; }
        public int? IdEstadoProcesoSeleccion { get; set; }
        public int? IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }
        public DateTime? FechaRendicionExamen { get; set; }
        public DateTime? FechaEnvioAccesos { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdPersonal_OperadorProceso { get; set; }
        public int? IdConvocatoriaPersonal { get; set; }
        public int? IdProcesoSeleccionEtapa { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public string IdRespuestas { get; set; }
        public int? IdSexo { get; set; }
		public int? CantidadHijo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string UrlPerfilFacebook { get; set; }
        public string UrlPerfilLinkedin { get; set; }
        public List<FiltroInsertarDTO> ListaRespuestaDesaprobatoria { get; set; }

    }

    public class FiltroInsertarDTO
    {
        public int IdRespuestaDesaprobatoria { get; set; }
        public string Nombre { get; set; }
        public int Id { get; set; }
    }

}
