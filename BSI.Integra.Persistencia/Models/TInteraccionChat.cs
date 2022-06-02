using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionChat
    {
        public int Id { get; set; }
        public string ZopimIdChatCompleto { get; set; }
        public int? IdAlumno { get; set; }
        public Guid IdCookie { get; set; }
        public int IdTipoInteraccion { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSubAreaCapcitacion { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdUsuarioZopim { get; set; }
        public string CorreoAgente { get; set; }
        public string Ip { get; set; }
        public string Pais { get; set; }
        public string Region { get; set; }
        public string Ciudad { get; set; }
        public int Duracion { get; set; }
        public int? NroMensaje { get; set; }
        public int? NroPalabraVisitante { get; set; }
        public int? NroPalabraAgente { get; set; }
        public int UsuarioTiempoRespuestaMaximo { get; set; }
        public int UsuarioTiempoRespuestaPromedio { get; set; }
        public bool EsEntranteSaliente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Valoracion { get; set; }
        public string EsNoLeido { get; set; }
        public string ZopimIdVisitante { get; set; }
        public string ZopimAgente { get; set; }
        public string Plataforma { get; set; }
        public string Navegador { get; set; }
        public string ZopimNombreVisitante { get; set; }
        public string ZopimCorreoVisitante { get; set; }
        public string ZopimApellidoVisitante { get; set; }
        public string ZopimTelefono { get; set; }
        public string TituloUrl { get; set; }
        public string Urldesde { get; set; }
        public string Urlhacia { get; set; }
        public string Tipo { get; set; }
        public string ZopimIdchat { get; set; }
        public string IdConjuntoAnuncio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
