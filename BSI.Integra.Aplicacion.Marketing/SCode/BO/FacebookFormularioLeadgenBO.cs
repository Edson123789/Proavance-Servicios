using System;
using BSI.Integra.Aplicacion.Base.BO;
using System.Net;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Marketing.Repositorio;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    ///BO: FacebookFormularioLeadgen
    ///Autor: Gian Miranda
    ///Fecha: 04/06/2021
    ///<summary>
    ///Columnas y funciones de la tabla mkt.T_FacebookFormularioLeadgen
    ///</summary>
    public class FacebookFormularioLeadgenBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///IdLeadgenFacebook                        Id original de leadgen de Facebook
        ///FechaCreacionFacebook                    Fecha de creacion original del lead de Facebook
        ///IdCampanhaFacebook                       Id original de la campania de Facebook
        ///NombreCampaniaFacebook                   Nombre original de la campania de Facebook
        ///FacebookAnuncioId                        Id original del anuncio de Facebook
        ///FacebookAnuncioNombre                    Nombre original del anuncio de Facebook
        ///Email                                    Email del lead
        ///NombreCompleto                           Nombre completo original del alumno en Facebook
        ///AreaFormacion                            Area de formacion registrado en Facebook
        ///Cargo                                    Cargo original registrado en Facebook
        ///AreaTrabajo                              Area de trabajo registrada en Facebook
        ///Ciudad                                   Ciudad original registrada en Facebook
        ///Telefono                                 Telefono original registada en Facebook
        ///EsProcesado                              Flag de Facebook
        ///Industria                                Industria original registrada en Facebook
        ///InicioCapacitacion                       Inicio de capacitacion original registrada en Facebook
        ///Excepcion                                Excepcion saltado en el proceso
        public string IdLeadgenFacebook { get; set; }
        public DateTime FechaCreacionFacebook { get; set; }
        public string IdCampanhaFacebook { get; set; }
        public string NombreCampaniaFacebook { get; set; }
        public string FacebookAnuncioId { get; set; }
        public string FacebookAnuncioNombre { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string AreaFormacion { get; set; }
        public string Cargo { get; set; }
        public string AreaTrabajo { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public bool EsProcesado { get; set; }
        public string Industria { get; set; }
        public string InicioCapacitacion { get; set; }
        public string Excepcion { get; set; }

        private readonly integraDBContext _integraDBContext;

        private FacebookFormularioLeadgenRepositorio _repFacebookFormularioLeadgen;

        public FacebookFormularioLeadgenBO()
        {
            _repFacebookFormularioLeadgen = new FacebookFormularioLeadgenRepositorio();
        }

        public FacebookFormularioLeadgenBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repFacebookFormularioLeadgen = new FacebookFormularioLeadgenRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Procesa los leads erroneos
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio del procesamiento de Leads</param>
        /// <param name="fechaFin">Fecha de fin del procesamiento de Leads</param>
        /// <returns>bool</returns>
        public bool ProcesarDatosLeadErroneos(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                string cadenaFechaInicio = fechaInicio.ToString("yyyyMMddHHmmss");
                string cadenaFechaFinal = fechaFin.ToString("yyyyMMddHHmmss");

                string url = $"https://integrav4-webhook.bsginstitute.com/api/ErrorFabookLeads/ProcesarLeads?FechaInicio={cadenaFechaInicio}&FechaFin={cadenaFechaFinal}";
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    wc.DownloadString(url);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
