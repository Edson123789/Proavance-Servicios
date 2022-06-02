using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ExpositorDTO
    {
        public int Id { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPaisProcedencia { get; set; }
        public int? IdCiudadProcedencia { get; set; }
        public int? IdReferidoPor { get; set; }
        public string TelfCelular1 { get; set; }
        public string TelfCelular2 { get; set; }
        public string TelfCelular3 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Domicilio { get; set; }
        public int? IdPaisDomicilio { get; set; }
        public int? IdCiudadDomicilio { get; set; }
        public string LugarTrabajo { get; set; }
        public int? IdPaisLugarTrabajo { get; set; }
        public int? IdCiudadLugarTrabajo { get; set; }
        public string AsistenteNombre { get; set; }
        public string AsistenteTelefono { get; set; }
        public string AsistenteCelular { get; set; }
        public string HojaVidaResumidaPerfil { get; set; }
        public string HojaVidaResumidaSpeech { get; set; }
        public string FormacionAcademica { get; set; }
        public string ExperienciaProfesional { get; set; }
        public string Publicaciones { get; set; }
        public string PremiosDistinciones { get; set; }
        public string OtraInformacion { get; set; }
        public string Usuario { get; set; }
        public int? IdPersonalAsignado { get; set; }

        public string FotoDocente { get; set; }
        public string UrlFotoDocente { get; set; }
    }
}
