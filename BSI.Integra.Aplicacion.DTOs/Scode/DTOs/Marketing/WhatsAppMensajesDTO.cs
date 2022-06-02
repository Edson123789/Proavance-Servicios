using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class WhatsAppMensajesDTO
    {
        public string Numero { get; set; }
        public string Mensaje { get; set; }
        public int? IdPersonal { get; set; }
        public int IdPais { get; set; }
        public int IdAlumno { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombreAlumno { get; set; }
    }
    public class WhatsAppMensajesPostulanteDTO
    {
        public string Numero { get; set; }
        public string Mensaje { get; set; }
        public int? IdPersonal { get; set; }
        public int IdPais { get; set; }
        public int IdPostulante { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombrePostulante { get; set; }
    }

    public class WhatsAppHistorialMensajesDTO
    {
        public string Numero { get; set; }
        public int Tipo { get; set; }
        public int IdPais { get; set; }
        public string Mensaje { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public int? Registro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombrePersonal { get; set; }
        public int? EstadoMensaje { get; set; }
    }
    public class WhatsAppHistorialMensajesPostulanteDTO
    {
        public string Numero { get; set; }
        public int Tipo { get; set; }
        public int IdPais { get; set; }
        public string Mensaje { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPostulante { get; set; }
        public int? Registro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombrePersonal { get; set; }
    }
    public class WhatsAppMensajesRecibidosOperacionesDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPersonal_Asignado { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdActividadCabecera { get; set; }
        public string ActividadCabecera { get; set; }
        public int IdTipoDato { get; set; }
        public bool ReprogramacionManual { get; set; }
        public bool ReprogramacionAutomatica { get; set; }
        public string Asesor { get; set; }
        public string NombrePersonal { get; set; }
        public string Contacto { get; set; }
        public string CentroCosto { get; set; }
        public string Celular { get; set; }
        public int? IdPadre { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public string UltimoComentario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdClasificacionPersona { get; set; }
        public int? DiasAtrasoCuotaPago { get; set; }
        public string EstadoMatricula { get; set; }

        public int IdMatriculaCabecera { get; set; }
        public int IdEstadoMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public string DNI { get; set; }
        public int? GrupoCurso { get; set; }
        public string SubEstadoMatricula { get; set; }

        public int TipoMensaje { get; set; }
    }

    public class SmsMensajesDTO
    {
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public int IdAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public int IdPersonal { get; set; }
    }

    public class SmsPlantillaClaveValorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPlantillaBase { get; set; }
        public string Contenido { get; set; }
    }

    public class SmsHistorialMensajeIndividualDTO
    {
        public string Celular { get; set; }
        public string NombreRemitente { get; set; }
        public string Mensaje { get; set; }
        public int ParteMensae { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Tipo { get; set; }
    }

    public class PlantillaSmsComboDTO
    {
        public int IdPlantilla { get; set; }
        public string NombrePlantilla { get; set; }
    }
}
