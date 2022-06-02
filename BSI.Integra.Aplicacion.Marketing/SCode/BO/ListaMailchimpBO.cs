using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class ListaMailchimpBO : BaseBO
    {
        public string MailchimpId { get; set; }
        public int MailchimpWebId { get; set; }
        public string MailchimpNombre { get; set; }
        public string NombreCompania { get; set; }
        public string Direccion1 { get; set; }
        public string Direccion2 { get; set; }
        public string DireccionCiudad { get; set; }
        public string DireccionEstado { get; set; }
        public string CodigoZip { get; set; }
        public string Pais { get; set; }
        public string Telefono { get; set; }
        public string NombreRemitente { get; set; }
        public string CorreoRemitente { get; set; }
        public string MailchimpAsunto { get; set; }
        public string MailchimpLenguaje { get; set; }
        public DateTime MailchimpFechaCreacion { get; set; }
        public int? RatingLista { get; set; }
        public int CantidadMiembro { get; set; }
        public int CantidadDesuscrito { get; set; }
        public int CantidadLimpiado { get; set; }
        public int CantidadDesuscritoDesdeEnvio { get; set; }
        public int CantidadLimpiadoDesdeEnvio { get; set; }
        public int CantidadCampania { get; set; }
        public DateTime? FechaUltimoEnvioCampania { get; set; }
        public decimal PromedioSuscrito { get; set; }
        public decimal PromedioDesuscrito { get; set; }
        public decimal PromedioObjetivoSuscrito { get; set; }
        public decimal PromedioApertura { get; set; }
        public decimal PromedioClic { get; set; }
        public DateTime? FechaUltimoSuscrito { get; set; }
        public DateTime? FechaUltimoDesuscrito { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public ListaMailchimpBO()
        {
        }

        public ListaMailchimpBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
