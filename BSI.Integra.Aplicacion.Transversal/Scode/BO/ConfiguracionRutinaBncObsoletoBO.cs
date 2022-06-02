using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: ConfiguracionRutinaBncObsoletoBO
    /// Autor: Jose Villena.
    /// Fecha: 09/02/2021
    /// <summary>
    /// Metodos para Cerrar Bnc a RN5
    /// </summary>

    public class ConfiguracionRutinaBncObsoletoBO : BaseBO
    {
        public string Nombre { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int IdTipoDato { get; set; }
        public int NumDiasProbabilidadMedia { get; set; }
        public int NumDiasProbabilidadAlta { get; set; }
        public int NumDiasProbabilidadMuyAlta { get; set; }
        public bool EjecutarRutinaProbabilidadMedia { get; set; }
        public bool EjecutarRutinaProbabilidadAlta { get; set; }
        public bool EjecutarRutinaProbabilidadMuyAlta { get; set; }
        public int IdOcurrenciaDestino { get; set; }
        public bool EjecutarRutinaEnviarCorreo { get; set; }
        public int IdPlantillaCorreo { get; set; }
        public int IdPersonalCorreoNoExistente { get; set; }
        public Guid? IdMigracion { get; set; }

        public ConfiguracionRutinaBncObsoletoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

        /// Autor: Jose Villena
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        /// Verifica Oportunidades para el paso a la fase RN5
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void VerificarOportunidadesParaFaseX()
        {
            ConfiguracionRutinaBncObsoletoRepositorio repoConfiguracionRutinaBncObsoleto = new ConfiguracionRutinaBncObsoletoRepositorio();
            var configuracion = repoConfiguracionRutinaBncObsoleto.GetAll().FirstOrDefault();
            IList<OportunidadesBNCObsoletasDTO> listaOportunidades = repoConfiguracionRutinaBncObsoleto.ObtenerOportunidadesBNC_RN5();
            List<int> listIdOprtunidades = new List<int>();
            foreach (var oportunidad in listaOportunidades)
            {
                try
                {
                    if (oportunidad.IdPersonal_Asignado == 0)
                    {
                        oportunidad.IdPersonal_Asignado = configuracion.IdPersonalCorreoNoExistente;
                    }

                    listIdOprtunidades.Add(oportunidad.Id);

                    if (configuracion.EjecutarRutinaEnviarCorreo)
                    {
                        try
                        {
                            int idOportunidad = oportunidad.Id;
                            int idPlantilla = 827;
                            string uri = $"https://integrav4-servicios.bsginstitute.com/api/MailingEnvioAutomatico/EnvioCorreoOportunidadPlantillaAutomatico/{idOportunidad}/{idPlantilla}";

                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                wc.DownloadString(uri);
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }

                    try
                    {
                        string usuarioRn5 = "UsuarioFaseRN5";
                        int idOportunidad = oportunidad.Id;
                        int idOcurrencia = configuracion.IdOcurrenciaDestino;

                        string uri = $"https://integrav4-servicios.bsginstitute.com/api/OportunidadRN5/CerrarOportunidadRN5Automatico/{idOportunidad}/{usuarioRn5}/{idOcurrencia}";
                        //string uri = $"http://localhost:63048/api/OportunidadRN5/CerrarOportunidadRN5Automatico/{idOportunidad}/{usuarioRn5}/{idOcurrencia}";

                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(uri);
                        }
                    }
                    catch (Exception e)
                    {
                        //Enviar correo a sistemas
                        try
                        {
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "jvillena@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = "Error CerrarOportunidadRN5Automatico",
                                Message = "IdOportunidad: " + oportunidad.Id.ToString() + "<br/>IdOcurrenciaDestino:" + configuracion.IdOcurrenciaDestino.ToString() + e.Message + (e.InnerException != null ? (" - " + e.InnerException.Message) : "") + " <br/> Mensaje toString <br/> " + e.ToString(),
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                        }
                        catch (Exception ex)
                        {

                        }

                    }

                    
                    
                }
                catch (Exception ex)
                {

                }
            }            
            
        }

    }
}
