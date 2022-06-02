using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaestroPersonalDTO
    {
        public int Id { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public string FijoReferencia { get; set; }
        public string MovilReferencia { get; set; }
        public string EmailReferencia { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdCiudad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPaisDireccion { get; set; }
        public int? IdRegionDireccion { get; set; }
        public string DistritoDireccion { get; set; }
        public string NombreDireccion { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdSexo { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string CodigoAfiliado { get; set; }
        public int? IdEntidadSeguroSalud { get; set; }
        public string TipoPersonal { get; set; }
        //=======================================
        public string Email { get; set; }
        public int? IdJefe { get; set; }
        public string Central { get; set; }
        public string Anexo3CX { get; set; }
        public string UrlFirmaCorreos { get; set; }
        public bool Activo { get; set; }

        public int? IdTipoSangre { get; set; }
    }
    public class MaestroPersonalCompuestoDTO
    {
        public DatosMaestroPersonalDTO Personal { get; set; }
        public DatosPersonalCeseDTO PersonalCese { get; set; }
        public DatosPersonalDescansoDTO PersonalDescanso { get; set; }
        public PersonalRemuneracionDTO PersonalRemuneracion { get; set; }
        public List<PersonalCertificacionDTO> PersonalCertificacion { get; set; }
        public List<PersonalExperienciaDTO> PersonalExperiencia { get; set; }
        public List<PersonalFamiliarDTO> PersonalFamiliar { get; set; }
        public List<PersonalFormacionDTO> PersonalFormacion { get; set; }
        public List<PersonalHistorialMedicoDTO> PersonalHistorialMedico { get; set; }
        public List<PersonalIdiomasDTO> PersonalIdiomas { get; set; }
        public List<PersonalInformacionMedicaDTO> PersonalInformacionMedica { get; set; }
        public List<PersonalInformaticaDTO> PersonalInformatica { get; set; }
        public PersonalSeguroSaludDTO PersonalSeguroSalud { get; set; }
        public PersonalSistemaPensionarioDTO PersonalSistemaPensionario { get; set; }
        public PersonalDireccionDTO PersonalDireccion { get; set; }
        public string Usuario { get; set; }
    }
    public class DatosMaestroPersonalDTO
    {
        public int Id { get; set; }
        public string Apellidos { get; set; }
        public string DistritoDireccion { get; set; }
        public string EmailReferencia { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdCiudadNacimiento { get; set; }
        public int? IdCiudadReferencia { get; set; }
        public int? IdEstadocivil { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdPaisReferencia { get; set; }
        public int? IdSexo { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string NombreDireccion { get; set; }
        public string Nombres { get; set; }
        public string NumeroDocumento { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        //=============================================
        public string Email { get; set; }
        public int? IdJefe { get; set; }
        public string TipoPersonal { get; set; }
        public string Central { get; set; }
        public string Anexo3CX { get; set; }
        public string UrlFirmaCorreos { get; set; }
        public bool Activo { get; set; }

        public string Area { get; set; }
        public string AreaAbrev { get; set; }
        public int? IdPuestoTrabajo { get; set; }
        public int? IdSede { get; set; }
        public int? IdTipoSangre { get; set; }
        public bool? EsCerrador { get; set; }
        public int? IdAsesorAsociado { get; set; }
        public int? IdPuestoTrabajoNivel { get; set; }
        public int? IdPersonalArchivo { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdTableroComercialCategoriaAsesor { get; set; }
    }

    public class DatosPersonalCeseDTO
    {
        public int? IdContratoEstado { get; set; }
        public int? IdMotivoCese { get; set; }
        public DateTime? FechaCese { get; set; }
        public bool EsModificado { get; set; }
    }

    public class DatosPersonalDescansoDTO
    {
        public DateTime? FechaInicioDescanso { get; set; }
        public DateTime? FechaFinDescanso { get; set; }
        public int? IdMotivoInactividad { get; set; }
        public bool EsModificado { get; set; }
    }
    public class PersonalCertificacionDTO
    {
        public int Id { get; set; }
        public DateTime FechaCertificacion { get; set; }
        public string Institucion { get; set; }
        public int IdPersonal { get; set; }
        public string Programa { get; set; }
        public int? IdPersonalArchivo { get; set; }
        public int? IdCentroEstudio { get; set; }
    }
    public class PersonalExperienciaDTO
    {
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public int Id { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public int? IdEmpresa { get; set; }
        public int IdPersonal { get; set; }
        public string MotivoRetiro { get; set; }
        public string NombreJefeInmediato { get; set; }
        public string TelefonoJefeInmediato { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
    public class PersonalFamiliarDTO
    {
        public string Apellidos { get; set; }
        public bool DerechoHabiente { get; set; }
        public bool EsContactoInmediato { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Id { get; set; }
        public int IdParentescoPersonal { get; set; }
        public int IdPersonal { get; set; }
        public int IdSexo { get; set; }
        public int IdTipoDocumentoPersonal { get; set; }
        public string Nombres { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroReferencia { get; set; }
    }
    public class PersonalFormacionDTO
    {
        public bool AlaActualidad { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaInicio { get; set; }
        public int Id { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int IdCentroEstudio { get; set; }
        public int? IdEstadoEstudio { get; set; }
        public int IdPersonal { get; set; }
        public int IdTipoEstudio { get; set; }
        public string Logro { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
    public class PersonalHistorialMedicoDTO
    {
        public string DetalleEnfermedad { get; set; }
        public string Enfermedad { get; set; }
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string Periodo { get; set; }
    }
    public class PersonalIdiomasDTO
    {
        public int Id { get; set; }
        public int IdCentroEstudio { get; set; }
        public int IdIdioma { get; set; }
        public int IdNivelIdioma { get; set; }
        public int IdPersonal { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
    public class PersonalInformacionMedicaDTO
    {
        public string Alergia { get; set; }
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        //public int IdTipoSangre { get; set; }
        public string Precaucion { get; set; }
    }
    public class PersonalInformaticaDTO
    {
        public int Id { get; set; }
        public int IdCentroEstudio { get; set; }
        public int IdNivelCompetenciaTecnica { get; set; }
        public int IdPersonal { get; set; }
        public string Programa { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
    public class PersonalSeguroSaludDTO
    {
        public int? IdEntidadSeguroSalud { get; set; }
        public bool EsModificado { get; set; }
        public bool Activo { get; set; }

        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
    public class PersonalSistemaPensionarioDTO
    {
        public string CodigoAfiliado { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public bool EsModificado { get; set; }
        public bool Activo { get; set; }

        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
    public class PersonalPuestoTrabajoDTO
    {
        public int Id { get; set; }
        public string Rol { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class PersonalRemuneracionDTO
    {
        public int? IdTipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinanciera { get; set; }
        public string NumeroCuenta { get; set; }
        public bool EsModificado { get; set; }
        public bool Activo { get; set; }

        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

    }


    public class PersonalDireccionDTO
    {
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public string Distrito { get; set; }
        public int? Lote { get; set; }
        public string Manzana { get; set; }
        public string NombreVia { get; set; }
        public string NombreZonaUrbana { get; set; }
        public string TipoVia { get; set; }
        public string TipoZonaUrbana { get; set; }
        public bool EsModificado { get; set; }
    }

    public class MaestroPersonalAccesoTemporalDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string NombreProgramaPadre { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public int IdPEspecificoHijo { get; set; }
        public double Avance { get; set; }
        public double Nota { get; set; }
        public bool EvaluacionHabilitada { get; set; }
        public int CantidadPreguntaConfigurada { get; set; }
        public int CantidadCrucigramaConfigurado { get; set; }
        public int CantidadPreguntaResuelta { get; set; }
        public int CantidadCrucigramaResuelta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class MaestroPersonalGrupoAccesoTemporalDTO
    {
        public int IdPersonal { get; set; }
        public string NombreProgramaPadre { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public List<int> IdPEspecificoHijo { get; set; }
        public double Avance { get; set; }
        public double Nota { get; set; }
        public bool EvaluacionHabilitada { get; set; }
        public int CantidadPreguntaConfigurada { get; set; }
        public int CantidadCrucigramaConfigurado { get; set; }
        public int CantidadPreguntaResuelta { get; set; }
        public int CantidadCrucigramaResuelta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class MaestroPersonalPuestoSedeDTO
    {
        public int Id { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public string FijoReferencia { get; set; }
        public string MovilReferencia { get; set; }
        public string EmailReferencia { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdCiudad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPaisDireccion { get; set; }
        public int? IdRegionDireccion { get; set; }
        public string DistritoDireccion { get; set; }
        public string NombreDireccion { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdSexo { get; set; }
        public int? IdPuestoTrabajo { get; set; }
        public int? IdSedeTrabajo { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string CodigoAfiliado { get; set; }
        public int? IdEntidadSeguroSalud { get; set; }
        public string TipoPersonal { get; set; }
        //=======================================
        public string Email { get; set; }
        public int? IdJefe { get; set; }
        public string Central { get; set; }
        public string Anexo3CX { get; set; }
        public string UrlFirmaCorreos { get; set; }
        public bool Activo { get; set; }

        public int? IdTipoSangre { get; set; }
        public bool? EsCerrador { get; set; }
        public int? IdCerrador { get; set; }
        public int? IdPuestoTrabajoNivel { get; set; }
        public int? IdPersonalArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public string RutaArchivoHtml { get; set; }
        public bool? EsImagen { get; set; }
        public int? IdTableroComercialCategoriaAsesor { get; set; }
    }

    public class PersonalTipoAsesorDTO
    {
        public int Id { get; set; }
        public int? IdCerrador { get; set; }
        public string AsesorAsociado { get; set; }
        public bool? EsCerrador { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class PersonalJefeInmediatoDTO
    {
        public int Id { get; set; }
        public int IdJefe { get; set; }
        public string DatosJefe { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class PersonalTiempoInactivoHistoricoDTO
    {
        public int Id { get; set; }
        public int IdMotivoInactividad { get; set; }
        public string MotivoInactividad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool Estado { get; set; }
    }
    public class ArchivoPersonalDTO
    {
        public IFormFile File { get; set; }
        public string Usuario { get; set; }
        public int? Id { get; set; }

    }
    public class AsesorTableroComercialDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Categoria { get; set; }
    }

    public class VentaAsesorTableroComercialDTO
    {
        public float? Venta { get; set; }
        public int? Mes { get; set; }
        public int? Asesor { get; set; }
        public int? Anio { get; set; }
    }

    public class SueldoAsesorTableroComercialDTO
    {
        public int Id { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public float? RemuneracionFija { get; set; }
        public float? ValorMaximo { get; set; }
        public string TipoRemuneracionVariable { get; set; }
        public int? IdMoneda { get; set; }
    }

    public class SueldoSemanalAsesorDTO
    {
        public float? Venta { get; set; }
        public int? NumeroSemana { get; set; }
    }

    public class RankingAsesorDTO
    {
        public int Orden { get; set; }
        public int IdPersonal { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public float Monto { get; set; }
    }

    public class MejorMesAsesorDTO
    {
        public int IdPersonal { get; set; }
        public decimal? Monto { get; set; }
    }

    public class ComisionesPasadasDTO
    {
        public int IdPersonal { get; set; }
        public decimal? MontoSoles { get; set; }
        public decimal? MontoDolares { get; set; }
    }

    public class ComisionActualDTO
    {
        public decimal? Valor { get; set; }
        public int IdPersonal { get; set; }
    }
}
