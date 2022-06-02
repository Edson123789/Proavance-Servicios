using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Persistencia.SCode.Repository;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Text.RegularExpressions;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class InformacionProgramaBO
    {
        public int IdCentroCosto { get; set; }
        public int CodigoPais { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPGeneral { get; set; }
        public Dictionary<string, string> Filtros { get; set; }

        public List<PreguntaFrecuenteSecciones> _PreguntasFrecuentes;
        public List<PreguntaFrecuenteSecciones> _PreguntasFrecuentesChange;
        public List<ResumenProgramaDTO> ResumenProgramas;
		public List<ResumenProgramaV2DTO> ResumenProgramasV2;
		public List<BeneficioDTO> ListaBeneficios;
        public string InformacionPrograma;
        public string EtiquetaDuracionHorarios;
        public string EtiquetaExpositores;
        public string EtiquetaBeneficiosInversion;
        public string EtiquetaFormasPago;
        public string EtiquetaTarifarios;
        private PgeneralRepositorio _repPGeneral;
        private ExpositorRepositorio _repExpositor;
        private OrigenRepositorio _repOrigen;
		private DocumentoSeccionPwRepositorio _repDocumentoSeccionPw;
        private readonly PgeneralConfiguracionBeneficioRepositorio _repPgeneralConfiguracionBeneficios;

        public InformacionProgramaBO()
        {
            _PreguntasFrecuentes = new List<PreguntaFrecuenteSecciones>();
            ListaBeneficios = new List<BeneficioDTO>();
            ResumenProgramas = new List<ResumenProgramaDTO>();
            _PreguntasFrecuentesChange = new List<PreguntaFrecuenteSecciones>();
            _repPGeneral = new PgeneralRepositorio();
            _repOrigen = new OrigenRepositorio();
            _repExpositor = new ExpositorRepositorio();
            _repPgeneralConfiguracionBeneficios = new PgeneralConfiguracionBeneficioRepositorio();
			_repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio();
            Filtros = new Dictionary<string, string>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdCentroCosto"></param>
        
        public List<PreguntaFrecuenteSeccionesDTO> CargarInformacionProgramaChange(List<PreguntaFrecuentePgeneralDTO> repositorioPreguntaFrecuente)
        {
            List<PreguntaFrecuenteSeccionesDTO> finalPreguntas = new List<PreguntaFrecuenteSeccionesDTO>();
            var a = repositorioPreguntaFrecuente;
            if (repositorioPreguntaFrecuente != null)
            {
                finalPreguntas = (from p in repositorioPreguntaFrecuente
                                  group p by new
                                  {
                                      IdSeccion = p.IdSeccion,
                                      Nombre = p.Nombre
                                  } into g
                                  select new PreguntaFrecuenteSeccionesDTO
                                  {
                                      IdSeccion = g.Key.IdSeccion.Value,
                                      Nombre = g.Key.Nombre,

                                      Contenido = g.Select(o => new PreguntaFrecuenteRespuestasDTO
                                      {
                                          Pregunta = o.Pregunta,
                                          Respuesta = o.Respuesta
                                      }).ToList()
                                  }).ToList();

            }
            return finalPreguntas;
        }
        public List<PreguntaFrecuenteSeccionesDTO> CargarInformacionPrograma(List<PreguntaFrecuentePgeneralDTO> repositorioPreguntaFrecuente)
        {
            List<PreguntaFrecuenteSeccionesDTO> finalPreguntas = new List<PreguntaFrecuenteSeccionesDTO>();
            var a = repositorioPreguntaFrecuente;
            if (repositorioPreguntaFrecuente != null)
            {
                finalPreguntas = (from p in repositorioPreguntaFrecuente
                                  group p by new
                                  {
                                      IdPrograma = p.IdPrograma,
                                      IdSeccion = p.IdSeccion,
                                      Nombre = p.Nombre
                                  } into g
                                  select new PreguntaFrecuenteSeccionesDTO
                                  {
                                      IdPrograma = g.Key.IdPrograma.Value,
                                      IdSeccion = g.Key.IdSeccion.Value,
                                      Nombre = g.Key.Nombre,
                                      
                                      Contenido = g.Select(o => new PreguntaFrecuenteRespuestasDTO
                                      {
                                          Pregunta = o.Pregunta,
                                          Respuesta = o.Respuesta
                                      }).ToList()
                                  }).ToList();

            }
            return finalPreguntas;
        }



        /// Autor: , Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Cargar Informacion Programa de manera Automatica
        /// </summary>	
        /// <returns></returns> 
        public void CargarInformacionProgramaAutomatico()
        {
            var data = _repPGeneral.ObtenerPGeneralPEspecificoPorCentroCosto(this.IdCentroCosto);

            if (data == null)
            {
                data = new ProgramaDTO();
                data.IdProgramaGeneral = 0;
            }

            this.IdPGeneral = data.IdProgramaGeneral;
            if (this.IdPGeneral != 0)
            {
                this.CargarInformacionPrograma();
            }
            if (data.IdArea != 0 && data.IdSubArea != 0)
            {
                this.Filtros = new Dictionary<string, string>
                {
                    { "idArea", data.IdArea.ToString() },
                    { "idSubArea", data.IdSubArea.ToString() },
                    { "codigoPais", this.CodigoPais.ToString() }
                };
                //this.CargarResumenProgramas();
				this.CargarResumenProgramasV2();
			}
        }





        /// Autor: , Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion del programa 
        /// </summary>	
        /// <returns></returns> 
        public void CargarInformacionPrograma()//IdPGeneral, CodigoPais
        {
            var beneficiosV2 = _repPgeneralConfiguracionBeneficios.ObtenerPgeneralConfiuracionBeneficios(this.IdPGeneral);
            var beneficios = beneficiosV2.Where(x => x.IdPais.Any(y => y.Id == this.CodigoPais)).ToList();

            DocumentosBO documento = new DocumentosBO();
            ListadoEtiquetaBO ListadoEtiqueta = new ListadoEtiquetaBO();
            var lista = documento.ObtenerInformacionProgramaGeneral(this.IdPGeneral);
            var listaPiePagina = lista.Where(x => x.Seccion.ToLower().Equals("beneficios")).FirstOrDefault();
            var addPiePagina = "";
            if(listaPiePagina != null)
            {
                addPiePagina = "<p>" + listaPiePagina.DetalleSeccion[0].PiePagina + "</p>";
                lista.Remove(listaPiePagina);
            }
            //Obtiene HTML y adapta los datos
            var seccionesV2Ordenado = documento.GenerarHTMLProgramaGeneralDocumentoSeccion(lista);
            foreach (var item in seccionesV2Ordenado)
            {
                string temporal = Regex.Replace(item.Contenido, "&bull;", "");
                string temporal2 = Regex.Replace(temporal, "&nbsp;", "");

                if (item.Seccion.ToLower() != "estructura curricular" && item.Seccion.ToLower() != "beneficios" && item.Seccion.ToLower() != "certificacion" && item.Seccion.ToLower() != "certificación" && item.Seccion.ToLower() != "prerrequisitos" && item.Seccion.ToLower() != "expositores")
                {
                    string temp3 = Regex.Replace(temporal2, "<br />", "<br /></li>");
                    string temp4 = Regex.Replace(temp3, "<br/>", "<br /></li>");
                    string temp5 = Regex.Replace(temp4, "<ul type='disc'><li>", "");                   

                    if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                    {
                        string temp6 = Regex.Replace(temp5, "<h5><strong><b>" + item.Seccion + "</b></strong></h5>", "");
                        item.Contenido = temp6;
                    }
                    
                }                
                else
                {
                    if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                    {
                        string temp3 = Regex.Replace(temporal2, "<br />", "<br /></li>");
                        string temp4 = Regex.Replace(temp3, "<br/>", "<br /></li>");
                        string temp5 = Regex.Replace(temp4, "<ul type='disc'><li>", "");

                        if (item != null && item.Contenido.Contains("<h5><strong><b>" + item.Seccion + "</b></strong></h5>"))
                        {
                            string temp6 = Regex.Replace(temp5, "<h5><strong><b>" + item.Seccion + "</b></strong></h5>", "");
                            item.Contenido = temp6;
                        }
                    }
                    else
                    {
                        string temp3 = Regex.Replace(temporal2, "<h5><strong><b>", "<h6>");
                        string temp4 = Regex.Replace(temp3, "</b></strong></h5>", "</h6>");

                        item.Contenido = temp4;
                    }
                    
                }
                if (item.Contenido == null || item.Contenido == "")
                {
                    item.Seccion = "";
                }
            }

            var estructura = seccionesV2Ordenado.Where(x => x.Seccion.ToLower().Equals("estructura curricular")).FirstOrDefault();
            var certificacion = seccionesV2Ordenado.Where(x => x.Seccion.ToLower().Equals("certificación") || x.Seccion.ToLower().Equals("certificacion")).FirstOrDefault();
            
            //Seccion Descripcion Estructura Curricular y Certificacion
            var concatenarEstructura = seccionesV2Ordenado.Where(x => x.Seccion.ToLower().Equals("descripci&#243;n estructura")).FirstOrDefault();
            var concatenarCertificacion = seccionesV2Ordenado.Where(x => x.Seccion.ToLower().Equals("descripci&#243;n certificacion")).FirstOrDefault();
            if (concatenarEstructura != null && estructura != null)
            {
                estructura.Contenido = estructura.Contenido + concatenarEstructura.Contenido;
                seccionesV2Ordenado.Remove(concatenarEstructura);
            }            
            if (concatenarCertificacion != null && certificacion != null)
            {
                certificacion.Contenido = certificacion.Contenido + concatenarCertificacion.Contenido;
                seccionesV2Ordenado.Remove(concatenarCertificacion);
            }

            // Elimina Data de Video innecesarios
            var deleteVideos = seccionesV2Ordenado.Where(x => x.Seccion.ToLower().Equals("video") || x.Seccion.ToLower().Equals("vista previa") || x.Seccion.ToLower().Equals("video de presentacion")).ToList();
            if(deleteVideos != null)
            {
                foreach (var item in deleteVideos)
                {
                    seccionesV2Ordenado.Remove(item);
                }
            }

            //Ordena data para hacer el display de forma predeterminada
            List<ProgramaGeneralSeccionAnexosHTMLDTO> seccionesV2 = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
          
            string[] listaTituloV1Orden = { "presentación", "objetivos", "público objetivo", "pre-requisitos", "estructura curricular", "duración y horarios", "certificacion", "expositores", "metodología online de este programa",      "material del curso", "pautas complementarias", "bibliografía"};
            string[] listaTituloV2Orden = { "presentacion", "objetivos", "publico objetivo", "prerrequisitos", "estructura curricular", "duracion y horarios", "certificación", "expositores", "metodolog&#237;a online de este programa", "material del curso", "pautas complementarias", "bibliografia" };

            for (var i = 0; i < listaTituloV1Orden.Length; i++)
            {
                var ordenarTempV2 = seccionesV2Ordenado.Where(x => x.Seccion.ToLower() == listaTituloV2Orden[i]).FirstOrDefault();
                if (ordenarTempV2 != null)
                {
                    seccionesV2.Add(ordenarTempV2);
                }
                else
                {
                    var ordenarTempV1 = seccionesV2Ordenado.Where(x => x.Seccion.ToLower() == listaTituloV1Orden[i]).FirstOrDefault();
                    if (ordenarTempV1 != null)
                    {
                        seccionesV2.Add(ordenarTempV1);
                    }
                }
            }

            // VALIDACION Y CREACION DE Items en Objeto para que no caiga en NULL
            var secciones = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
            ProgramaGeneralSeccionAnexosHTMLDTO modalidadAdd = new ProgramaGeneralSeccionAnexosHTMLDTO()
            {
                Seccion = "Modalidades",
                Contenido = "",
            };
            secciones.Add(modalidadAdd);
            var flagBeneficio = false;
            var flagInversion = false;
            foreach (var item in seccionesV2)
            {
                if (item.Seccion == "Inversion")
                {
                    flagInversion = true;
                }
                if (item.Seccion == "Beneficios")
                {
                    flagBeneficio = true;
                }
                if (item.Seccion == "Inversión")
                {
                    item.Seccion = "Inversion";
                    flagInversion = true;
                }

            }
            if (flagInversion == false)
            {
                ProgramaGeneralSeccionAnexosHTMLDTO inversionAdd = new ProgramaGeneralSeccionAnexosHTMLDTO()
                {
                    Seccion = "Inversion",
                    Contenido = "",
                };
                secciones.Add(inversionAdd);
            }
            if (flagBeneficio == false)
            {
                ProgramaGeneralSeccionAnexosHTMLDTO beneficiosAdd = new ProgramaGeneralSeccionAnexosHTMLDTO()
                {
                    Seccion = "Beneficios",
                    Contenido = "",
                };
                secciones.Add(beneficiosAdd);
            }
            secciones.AddRange(seccionesV2);
            //Logica de Montos de V1
            var seccionesBeneficiosInversion = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
            var montos = this.ObtenerMontos2();
            var contador = 0;
            if (montos.Count() < 1) montos = this.ObtenerMontos();
            foreach (var item in montos)
            {
                if (item.Beneficios == "<ul></ul><br>" || item.Beneficios == null || item.Beneficios == "null")
                {
                    contador++;
                }
            }
            if (contador == montos.Count())
            {
                montos = this.ObtenerMontos();
            }
            var montosb = montos.Where(s => s.Beneficios != null).ToArray();
            var montopagado = this.ObtenerMontoPagado();

            //Obtencion de Modalidades VPortal
            ModalidadProgramaDTO cambiarModelo;
            List<ModalidadProgramaDTO> modalidadGeneral = new List<ModalidadProgramaDTO>();
            var modalidadGeneralPortal = ListadoEtiqueta.ObtenerFechaInicioProgramaTodos(this.IdPGeneral);
            var pwDuracion = _repPGeneral.GetBy(x => x.Id == this.IdPGeneral).Select(x => new { x.PwDuracion, x.Nombre }).FirstOrDefault();
            //Obtencion de Modalidades V2
            //var modalidadGeneral = _repPGeneral.ObtenerModalidadesPorProgramaGeneral(this.IdPGeneral);
            foreach (var modalidadPortal in modalidadGeneralPortal)
            {
                cambiarModelo = new ModalidadProgramaDTO()
                {
                    Tipo = modalidadPortal.Tipo,
                    Ciudad = modalidadPortal.Ciudad,
                    TipoCiudad = "",
                    FechaHoraInicio = modalidadPortal.FechaInicioTexto,
                    NombrePG = pwDuracion.Nombre,
                    IdPEspecifico = modalidadPortal.Id,
                    NombreESP = modalidadPortal.Nombre,
                    Duracion = modalidadPortal.Duracion,
                    Pw_duracion = pwDuracion.PwDuracion,
                    FechaReal = modalidadPortal.FechaInicio
                };
                modalidadGeneral.Add(cambiarModelo);
            }

            var modalidadAsincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Asincronica") && s.Ciudad.Equals("LIMA")).ToList();
            var modalidadSincronica = modalidadGeneral.Where(s => s.Tipo.Equals("Online Sincronica")).ToList();
            List<ModalidadProgramaDTO> PruebaModalidad = new List<ModalidadProgramaDTO>();
            PruebaModalidad.AddRange(modalidadAsincronica);
            PruebaModalidad.AddRange(modalidadSincronica);

            var modalidades = new List<ModalidadProgramaDTO>();
            modalidades.AddRange(PruebaModalidad);

            var modalidadesV2 = new List<ModalidadProgramaSincronicoDTO>();

            var tarifarios = _repOrigen.ObtenerTarifariosDetallesAgenda(this.IdMatriculaCabecera);

            var seccionMontos = secciones.Where(s => s.Seccion.ToLower().Equals("inversion")).FirstOrDefault();
            var seccionExpositores = secciones.Where(s => s.Seccion.ToLower().Equals("expositores")).FirstOrDefault();

            seccionMontos.Contenido = "<table class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Precio Contado</strong></td><td><strong>Precio Credito</strong></td></tr>"
                + string.Join("", montos.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.InversionContado + "</td><td>" + s.InversionCredito + "</td></tr>").ToArray()) + "</table>";

            seccionesBeneficiosInversion.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
            {
                Seccion = seccionMontos.Seccion,
                Contenido = "<table id=\"tablebeneficios\" class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Precio Contado</strong></td><td><strong>Precio Credito</strong></td></tr>"
                + string.Join("", montos.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.InversionContado + "</td><td>" + s.InversionCredito + "</td></tr>").ToArray()) + "</table>"
            });

            var seccionBeneficios = secciones.Where(s => s.Seccion.ToLower().Equals("beneficios")).FirstOrDefault();           
            string inicio = "<table class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Beneficios</strong></td></tr>";
            string fila = "";
            string descripcionSinVersion = "";
            string descripcionBasica = "";
            string descripcionProfesional = "";
            string descripcionGerencial = "";
            bool flagSinVersion = false;
            bool flagVersionBasica = false;
            bool flagVersionProfesional = false;
            bool flagVersionGerencial = false;
            if (beneficios != null)
            {
                foreach (var item in beneficios)
                {
                    foreach (var item2 in item.IdVersion)
                    {
                        if (item2.IdVersion == null) // SIN VERSION
                        {
                            flagSinVersion = true;
                            descripcionSinVersion += "<li class='beneficioDetallePrograma' onclick='MostrarInformacionAdicionalBeneficio("+item.IdBeneficio+","+ item.IdPGeneral + ")'>" + item.Descripcion + "</li>";
                        }
                        if (item2.IdVersion == 1) // BASICA
                        {
                            flagVersionBasica = true;
                            descripcionBasica += "<li class='beneficioDetallePrograma' onclick='MostrarInformacionAdicionalBeneficio(" + item.IdBeneficio+","+item.IdPGeneral+")'>" + item.Descripcion + "</li>";
                        }
                        if (item2.IdVersion == 2) // PROFESIONAL
                        {
                            flagVersionProfesional = true;
                            descripcionProfesional += "<li class='beneficioDetallePrograma' onclick='MostrarInformacionAdicionalBeneficio("+ item.IdBeneficio + ","+item.IdPGeneral+")'>" + item.Descripcion + "</li>";
                        }
                        if (item2.IdVersion == 3) // GERENCIAL
                        {
                            flagVersionGerencial = true;
                            descripcionGerencial += "<li class='beneficioDetallePrograma' onclick='MostrarInformacionAdicionalBeneficio("+item.IdBeneficio+","+item.IdPGeneral+")'>" + item.Descripcion + "</li>";
                        }
                    }
                }
                if (flagSinVersion == true)
                {
                    fila += "<tr><td>Sin Versión</td><td>" + descripcionSinVersion + "</td></tr>";
                }
                if (flagVersionBasica == true)
                {
                    fila += "<tr><td>Básica</td><td>" + descripcionBasica + "</td></tr>";
                }
                if (flagVersionProfesional == true)
                {
                    fila += "<tr><td>Profesional</td><td>" + descripcionProfesional + "</td></tr>";
                }
                if (flagVersionGerencial == true)
                {
                    fila += "<tr><td>Gerencial</td><td>" + descripcionGerencial + "</td></tr>";
                }
            }
            seccionBeneficios.Contenido = inicio + fila + "</table>" + addPiePagina;

            //Seccion BENEFICIOS Operaciones
            seccionesBeneficiosInversion.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
            {
                Seccion = seccionBeneficios.Seccion,
                Contenido = "<table id=\"tableinversion\" class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Beneficios</strong></td></tr>" + fila + "</table>"
            });

            //Montos Complementarios
            seccionesBeneficiosInversion.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
            {
                Seccion = "Monto Actual",
                Contenido = "<table id=\"tablemontomatricula\" class=\"table table-hover \"><tr><td><strong>Moneda</strong></td><td><strong>Costo Original</strong></td><td><strong>Descuento</strong></td><td><strong>Porcentaje Descuento</strong></td><td><strong>Costo Final</strong></td></tr>"
                + string.Join("", montopagado.Select(s => "<tr><td>" + s.Moneda + "</td><td>" + s.CostoOriginal + "</td><td>" + s.Descuento + "</td><td>" + s.PorcentajeDescuento + "</td><td>" + s.CostoFinal + "</td></tr>").ToArray()) + "</table>"
            });

            EtiquetaFormasPago = "<table style='font-family:arial,sans-serif;border-collapse:collapse;width:100%'><tr><th style='background-color:#4584a7;color:#fff;border:1px solid #d7d7d7;padding:10px'>Formas de Pago</th></tr><tr><td style='border:1px solid #d7d7d7;padding:10px'><h2>PERU</h2><p><span style='font-size:13.3333px'><strong>1.</strong>Pago por Tarjeta Visa, Mastercard, Amex, Diners a trav&eacute;s de&nbsp;</span><span style='font-size:13.3333px'>nuestra pagina web:&nbsp;</span><a href='https://bsginstitute.com/Cuenta' style='font-size:13.3333px'>https://bsginstitute.com/</a></p><p>&nbsp;</p><p><strong>2.</strong><span style='font-size:13.3333px'>Pago&nbsp;</span><span style='font-size:13.3333px'>mediante una transferencia interbancaria(Per&uacute;) de su banco a nuestro banco.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Raz&oacute;n social: BS Grupo SAC</span></li><li style='text-align:left'><span style='font-size:10pt'>Raz&oacute;n Comercial: BSG INSTITUTE</span></li><li style='text-align:left'><span style='font-size:10pt'>Ruc: 20454870591</span></li><li style='text-align:left'><span style='font-size:10pt'>Direcci&oacute;n: Pasaje Romaña-Calle 2 Nro 107 Urb. Le&oacute;n XIII Arequipa-Arequipa-Cayma</span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>215-1863341-0-42*</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001863341042-20</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta corriente en D&oacute;lares</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>215-1870934-1-48*</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001870934148-23</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco BBVA Continental</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>0011-0220-01-00131737</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>011 220 000100131737 17</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco Scotiabank</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>000-4654102</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>009-313-000004654102-85</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>* N&uacute;mero de cuenta autorizada solo para transferencias de empresas, no est&aacute; habilitado para dep&oacute;sitos en ventanilla.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>&nbsp;</span></p><p><h2>EXTRANJERO</h2><strong>1.</strong><span style='font-size:13.3333px'>Pago&nbsp;</span><span style='font-size:13.3333px'>mediante una transferencia bancaria Internacional de su banco a nuestro banco.</span></p><p style='margin-left:30px'><span style='font-size:10pt'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BSG Institute S.A.C</span></li><li><span style='font-size:10pt'>RUC: 20454870591</span></li><li><span style='font-size:10pt'>Address: Pasaje Romaña-Calle 2 Nro 107 Urb. Le&oacute;n XIII Arequipa-Arequipa-Cayma</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos del banco</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Bank: Banco de Cr&eacute;dito del Per&uacute;</span></li><li><span style='font-size:10pt'>Address: Street San Juan de Dios 123, Arequipa, Per&uacute;</span></li><li><span style='font-size:10pt'>Swift Code: BCPLPEPL</span></li><li><span style='font-size:10pt'>Account Number USD$: 215-1870934-1-48</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>COLOMBIA</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>Para transferencias Nacionales Colombia:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BS GRUPO COLOMBIA SAS</span></li><li><span style='font-size:10pt'>NIT: 900776296</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuenta de Ahorro en Pesos</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Datos del banco: Bancolombia</span></li><li><span style='font-size:10pt'>Numero de Cuenta: 65231918412</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>BOLIVIA</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>Para transferencias Nacionales Bolivia:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BSG INSTITUTE BOLIVIA</span></li><li><span style='font-size:10pt'>NIT: 376053024</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuenta corriente en Bolivianos</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Datos del banco: Banco de Credito Bolivia</span></li><li><span style='font-size:10pt'>Numero de Cuenta Bolivianos: 701-5051921-3-41</span></li><li><span style='font-size:10pt'>Numero de Cuenta D&oacute;lares: 701-5041553-2-04</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>MEXICO</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>1.	Pagos aceptado con Tarjeta Visa, Mastercard, American Express y Carnet (Débito y Crédito) a través de nuestra página web<a href='https://bsginstitute.com/Cuenta' style='font-size:13.3333px'>https://bsginstitute.com/</a></span></p><p style='margin-left:30px'><span style='font-size:10pt'>2. Pagos mediante D&eacute;positos Bancarios y Transferencia por SPEI*</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Raz&oacute;n Social: BSG Institute M&eacute;xico S.A. de C.V.</span></li><li><span style='font-size:10pt'>Raz&oacute;n Comercial: BSG Institute</span></li><li><span style='font-size:10pt'>RFC: BIM210209H26</span></li><li><span style='font-size:10pt'>Direcci&oacute;n: Montecito No. 38, Piso 33, Of. 4.</span></li><li><span style='font-size:10pt'>Edificio: World Trade Center – WTC</span></li><li><span style='font-size:10pt'>Colonia N&aacute;poles</span></li><li><span style='font-size:10pt'>Ciudad de M&eacute;xico</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuentas</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Cuenta en Pesos Mexicanos en el BBVA Bancomer</span></li><li><span style='font-size:10pt'>N&uacute;mero de Cuenta: 0116490468</span></li><li><span style='font-size:10pt'>Cuenta CLABE Interbancaria: 012180001164904687</span></li></ul><ul style='margin-left:30px'><li><span style='font-size:10pt'>Cuenta en Dólares Americanos en el BBVA Bancomer</span></li><li><span style='font-size:10pt'>N&uacute;mero de Cuenta: 0116490522</span></li><li><span style='font-size:10pt'>Cuenta CLABE Interbancaria: 012180001164905220</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>3. Liga (Enlace) de Pago</span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Deber&aacute; solicitar a su Asesor el envío de la liga de pago por correo electrónico, una vez recibido lo conduce a una p&aacute;gina web para completar el pago.</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* El pago podr&iacute;a est&aacute;r sujeto al cobro de comisiones del banco emisor, por favor verificar previamente.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p></td></tr></table>";
            EtiquetaTarifarios = this.GetContenidoTarifario(tarifarios);
            var seccionModalidades = secciones.Where(s => s.Seccion.ToLower().Equals("modalidades")).FirstOrDefault();

            foreach(var item in modalidades)
            {
                if (item.FechaReal != null)
                {
                    if (item.Tipo.ToLower() == "online asincronica")
                    {
                        var fechaReal = item.FechaReal?.ToString("MMM yyy", CultureInfo.CreateSpecificCulture("es-ES"));
                        fechaReal = fechaReal.Substring(0, 1).ToUpper() + fechaReal.Substring(1);
                        item.FechaHoraInicio = fechaReal;
                    }                    
                }
                else
                {
                    item.FechaHoraInicio = "Por definir";
                }
            }            

            seccionModalidades.Contenido = "<table class=\"table table-hover \"><tr><td><strong>Modalidad</strong></td><td><strong>Fecha de Inicio</strong></td></tr>"
           + string.Join("", modalidades.Select(s => "<tr><td>" + s.Tipo + "</td><td>" + s.FechaHoraInicio + "</td></tr>").ToArray()) + "</table>";

            //Duracion y Horarios
            string contenido2 = secciones.Where(w => w.Seccion.ToLower().Equals("duracion y horarios")).Select(w => w.Contenido).FirstOrDefault();
            string contenido = "";
            if (contenido2 != null)
            {
                if (contenido2.Contains("<ul><li>"))
                {
                    contenido = contenido2 + "</ul></ul>";
                }
                else contenido = contenido2;
            }           

            string newcontenido = this.GetContenidoHorarios(modalidades, contenido);
            var seccionContenido = secciones.Where(s => s.Seccion.ToLower().Equals("duracion y horarios")).FirstOrDefault();

            if (contenido == null)
            {
                ProgramaGeneralSeccionAnexosHTMLDTO programaNuevo = new ProgramaGeneralSeccionAnexosHTMLDTO();
                programaNuevo.Seccion = "Duración y Horarios";
                programaNuevo.Contenido = newcontenido;
                secciones.Add(programaNuevo);
            }
            else
            {
                if(seccionContenido != null)
                {
                    seccionContenido.Contenido = newcontenido;
                }                
            }

            string[] listaTituloV1 = { "estructura curricular", "beneficios", "prerrequisitos", "certificacion", "certificación", "duracion y horarios", "duración y horarios", "evaluacion", "evaluación", "bibliografia", "bibliografía", "material del curso", "pautas complementarias", "descripción certificación", "descripcion certificacion", "objetivos", "presentacion", "presentación", "público objetivo", "publico objetivo", "metodolog&#237;a online de este programa" };
            string[] listaTituloV2 = { "Estructura Curricular", "Beneficios", "Prerrequisitos", "Certificación", "Certificación", "Duración y Horarios", "Duración y Horarios", "Evaluación", "Evaluación", "Bibliografía", "Bibliografía", "Material del Curso", "Pautas Complementarias", "Descripción Certificación", "Descripción Certificación", "Objetivos", "Presentación", "Presentación", "Público Objetivo", "Público Objetivo", "Metodolog&#237;a Online de este Programa" };

            for (var i = 0; i < listaTituloV2.Length; i++)
            {
                var pendienteTildes = secciones.Where(x => x.Seccion.ToLower() == listaTituloV1[i]).FirstOrDefault();
                if (pendienteTildes != null)
                {
                    if (pendienteTildes.Contenido == null || pendienteTildes.Contenido != "")
                    {
                        pendienteTildes.Seccion = listaTituloV2[i];
                    }
                }
            }


            var html = string.Join("", secciones.Select(s => "<h4>" + s.Seccion + "</h4>" + s.Contenido + "<br />").ToArray());

            if (seccionesBeneficiosInversion != null)
            {
                EtiquetaBeneficiosInversion = string.Join("", seccionesBeneficiosInversion.Where(w => w.Seccion.ToLower().Equals("inversion") || w.Seccion.ToLower().Equals("beneficios") || w.Seccion.ToLower().Equals("monto actual")).Select(s => "<br><h4 class='col-sm-10' Id='IdTabla" + s.Seccion + "'>" + s.Seccion + "</h4><br>" + s.Contenido).ToArray());
            }
            else EtiquetaBeneficiosInversion = "";

            if (seccionExpositores != null)
            {
                EtiquetaExpositores = seccionExpositores.Contenido;
            }
            else EtiquetaExpositores = "";
            
            if (newcontenido != null)
            {
                EtiquetaDuracionHorarios = newcontenido;
            }
            else EtiquetaDuracionHorarios = "";

            if (html != null)
            {
                InformacionPrograma = html;
            }
            else InformacionPrograma = "";
            
        }




        /// <summary>
        /// Obtiene la informacion del programa basandose en el programa general y el codigo pais VERSION ANTIGUA POR SI SE NECESITA REVERTIR colocar CargarInformacionPrograma como nombre de la función
        /// </summary>
        public void CargarInformacionProgramaV1()//IdPGeneral, CodigoPais
        {
            var secciones = _repPGeneral.ObtenerSeccionesInformacionProgramaPorProgramaGeneral(this.IdPGeneral);
            var seccionesBeneficiosInversion = new List<InformacionProgramaDTO>();
            var expositores = _repExpositor.ObtenerExpositoresPorProgramaGeneral(this.IdPGeneral);

            var montos = this.ObtenerMontos2();

            var contador=0;
            if (montos.Count() < 1) montos = this.ObtenerMontos();
            foreach (var item in montos)
            {
                if (item.Beneficios == "<ul></ul><br>" || item.Beneficios == null || item.Beneficios == "null")
                {
                    contador++;
                }
            }
            if (contador == montos.Count())
            {
                montos = this.ObtenerMontos();
            }

            var montosb = montos.Where(s => s.Beneficios != null).ToArray();
            

            var montopagado = this.ObtenerMontoPagado();
            var modalidades = _repPGeneral.ObtenerModalidadesPorProgramaGeneral(this.IdPGeneral);
            var seccionExpositor = secciones.Where(s => s.Titulo.ToLower().Equals("expositores")).FirstOrDefault();

            var tarifarios = _repOrigen.ObtenerTarifariosDetallesAgenda(this.IdMatriculaCabecera);

            var seccionMontos = secciones.Where(s => s.Titulo.ToLower().Equals("inversion")).FirstOrDefault();
            seccionExpositor.Contenido = string.Join("", expositores.Select(s => "<h5>" + s.Nombres + "</h5><br><p>" + s.HojaVida + "</p>").ToArray());
            seccionMontos.Contenido = "<table class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Precio Contado</strong></td><td><strong>Precio Credito</strong></td></tr>"
                + string.Join("", montos.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.InversionContado + "</td><td>" + s.InversionCredito + "</td></tr>").ToArray()) + "</table>";

            seccionesBeneficiosInversion.Add(new InformacionProgramaDTO
            {
                Titulo = seccionMontos.Titulo,
                Contenido = "<table id=\"tablebeneficios\" class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Precio Contado</strong></td><td><strong>Precio Credito</strong></td></tr>"
                + string.Join("", montos.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.InversionContado + "</td><td>" + s.InversionCredito + "</td></tr>").ToArray()) + "</table>"
            });

            var seccionBeneficios = secciones.Where(s => s.Titulo.ToLower().Equals("beneficios")).FirstOrDefault();
            seccionBeneficios.Contenido = "<table class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Beneficios</strong></td></tr>"
             + string.Join("", montosb.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.Beneficios + "</td></tr>").ToArray()) + "</table>";


            seccionesBeneficiosInversion.Add(new InformacionProgramaDTO
            {
                Titulo = seccionBeneficios.Titulo,
                Contenido = "<table id=\"tableinversion\" class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Beneficios</strong></td></tr>"
                + string.Join("", montosb.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.Beneficios + "</td></tr>").ToArray()) + "</table>"
            });


            //Montos Complementarios
            seccionesBeneficiosInversion.Add(new InformacionProgramaDTO
            {
                Titulo = "Monto Actual",
                Contenido = "<table id=\"tablemontomatricula\" class=\"table table-hover \"><tr><td><strong>Moneda</strong></td><td><strong>Costo Original</strong></td><td><strong>Descuento</strong></td><td><strong>Porcentaje Descuento</strong></td><td><strong>Costo Final</strong></td></tr>"
                + string.Join("", montopagado.Select(s => "<tr><td>" + s.Moneda + "</td><td>"+s.CostoOriginal+ "</td><td>" + s.Descuento +  "</td><td>" + s.PorcentajeDescuento + "</td><td>" + s.CostoFinal + "</td></tr>").ToArray()) + "</table>"
                //+ "<table id=\"tablemontoscomplementarios\" class=\"table table-hover \"><tr><td><strong>Version</strong></td><td><strong>Costo Total</strong></td><td><strong>Monto Descuento Otorgado</strong></td><td><strong>Porcentaje Descuento Otorgado</strong></td><td><strong>Costo Total con Descuento</strong></td><td><strong>Diferencia Pagar</strong></td></tr></table>"
            });

            //EtiquetaFormasPago = "<table style='font-family: arial, sans-serif;  border-collapse: collapse;  width: 100%;'> <tr> <th style='background-color: #4584a7;color: white;  border: 1px solid #d7d7d7; padding: 10px;'>Formas de Pago</th> </tr> <tr> <td style=' border: 1px solid #d7d7d7;padding: 10px;'> <p><span style='font-size:13.3333px;'><strong>1. </strong>Pago por Tarjeta Visa, Mastercard, Amex, Diners a trav&eacute;s de&nbsp;</span><span style='font-size:13.3333px;'>nuestra pagina web:&nbsp;</span><a href='https://bsginstitute.com/Cuenta' style='font-size:13.3333px;'>https://bsginstitute.com/</a></p><p>&nbsp;</p><p><strong>2. </strong><span style='font-size:13.3333px;'>Pago&nbsp;</span><span style='font-size:13.3333px;'>mediante una transferencia interbancaria(Per&uacute;) de su banco a nuestro banco.</span></p><p style='text-align:left;margin-left:30px;'><span style='font-size:10pt;'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='text-align:left;margin-left:30px;'><span style='font-size:10pt;'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px;'><li style='text-align:left;'><span style='font-size:10pt;'>Raz&oacute;n social: bsginstitute S.A.C</span></li><li style='text-align:left;'><span style='font-size:10pt;'>Ruc: 20454870591</span></li><li style='text-align:left;'><span style='font-size:10pt;'>Direcci&oacute;n: Urb. Atlas A-9, Av. V&iacute;ctor Andr&eacute;s Belaunde, Umacollo, Arequipa,Per&uacute;</span></li></ul><p style='text-align:left;margin-left:30px;'><span style='font-size:10pt;'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px;'><li style='text-align:left;'><span style='font-size:10pt;'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left;'><span style='font-size:10pt;'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001863341042-20</strong></span></li></ul><p style='text-align:left;margin-left:30px;'><span style='font-size:10pt;'><strong>Cuenta corriente en D&oacute;lares</strong></span></p><ul style='margin-left:30px;'><li style='text-align:left;'><span style='font-size:10pt;'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left;'><span style='font-size:10pt;'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001870934148-23</strong></span></li></ul><p style='text-align:left;margin-left:30px;'><span style='font-size:10pt;'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p style='text-align:left;margin-left:30px;'><span style='font-size:10pt;'>&nbsp;</span></p><p><strong>3. </strong></span><span style='font-size:13.3333px;'>Pago&nbsp;</span><span style='font-size:13.3333px;'>mediante una transferencia bancaria Internacional de su banco a nuestro banco.</span></p><p style='margin-left:30px;'><span style='font-size:10pt;'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='margin-left:30px;'><span style='font-size:10pt;'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px;'><li><span style='font-size:10pt;'>Razon Social: BSG Institute S.A.C</span></li><li><span style='font-size:10pt;'>RUC: 20454870591</span></li><li><span style='font-size:10pt;'>Address: Urb. Atlas A-9, Av. V&iacute;ctor Andr&eacute;s Belaunde, Umacollo, Arequipa, Per&uacute;</span></li></ul><p style='margin-left:30px;'><span style='font-size:10pt;'><strong>Datos del banco</strong></span></p><ul style='margin-left:30px;'><li><span style='font-size:10pt;'>Bank: Banco de Cr&eacute;dito del Per&uacute;</span></li><li><span style='font-size:10pt;'>Address: Street San Juan de Dios 123, Arequipa, Per&uacute;</span></li><li><span style='font-size:10pt;'>Swift Code: BCPLPEPL</span></li><li><span style='font-size:10pt;'>Account Number USD$: 215-1870934-1-48</span></li></ul><p style='margin-left:30px;'><span style='font-size:10pt;'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt;'>&nbsp;</span></p> </td> </tr> </table>";
            EtiquetaFormasPago = "<table style='font-family:arial,sans-serif;border-collapse:collapse;width:100%'><tr><th style='background-color:#4584a7;color:#fff;border:1px solid #d7d7d7;padding:10px'>Formas de Pago</th></tr><tr><td style='border:1px solid #d7d7d7;padding:10px'><h2>PERU</h2><p><span style='font-size:13.3333px'><strong>1.</strong>Pago por Tarjeta Visa, Mastercard, Amex, Diners a trav&eacute;s de&nbsp;</span><span style='font-size:13.3333px'>nuestra pagina web:&nbsp;</span><a href='https://bsginstitute.com/Cuenta' style='font-size:13.3333px'>https://bsginstitute.com/</a></p><p>&nbsp;</p><p><strong>2.</strong><span style='font-size:13.3333px'>Pago&nbsp;</span><span style='font-size:13.3333px'>mediante una transferencia interbancaria(Per&uacute;) de su banco a nuestro banco.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Raz&oacute;n social: BS Grupo SAC</span></li><li style='text-align:left'><span style='font-size:10pt'>Raz&oacute;n Comercial: BSG INSTITUTE</span></li><li style='text-align:left'><span style='font-size:10pt'>Ruc: 20454870591</span></li><li style='text-align:left'><span style='font-size:10pt'>Direcci&oacute;n: Pasaje Romaña-Calle 2 Nro 107 Urb. Le&oacute;n XIII Arequipa-Arequipa-Cayma</span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>215-1863341-0-42*</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001863341042-20</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta corriente en D&oacute;lares</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco de Cr&eacute;dito del Per&uacute;</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>215-1870934-1-48*</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>002-215-001870934148-23</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco BBVA Continental</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>0011-0220-01-00131737</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>011 220 000100131737 17</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'><strong>Cuenta Corriente en Soles</strong></span></p><ul style='margin-left:30px'><li style='text-align:left'><span style='font-size:10pt'>Datos del Banco: Banco Scotiabank</span></li><li style='text-align:left'><span style='font-size:10pt'>N&uacute;mero de cuenta:&nbsp;<strong>000-4654102</strong></span></li><li style='text-align:left'><span style='font-size:10pt'>C&oacute;digo de cuenta interbancario (CCI):&nbsp;<strong>009-313-000004654102-85</strong></span></li></ul><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>* N&uacute;mero de cuenta autorizada solo para transferencias de empresas, no est&aacute; habilitado para dep&oacute;sitos en ventanilla.</span></p><p style='text-align:left;margin-left:30px'><span style='font-size:10pt'>&nbsp;</span></p><p><h2>EXTRANJERO</h2><strong>1.</strong><span style='font-size:13.3333px'>Pago&nbsp;</span><span style='font-size:13.3333px'>mediante una transferencia bancaria Internacional de su banco a nuestro banco.</span></p><p style='margin-left:30px'><span style='font-size:10pt'>Los datos para que realice esta transferencia son los siguientes:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BSG Institute S.A.C</span></li><li><span style='font-size:10pt'>RUC: 20454870591</span></li><li><span style='font-size:10pt'>Address: Pasaje Romaña-Calle 2 Nro 107 Urb. Le&oacute;n XIII Arequipa-Arequipa-Cayma</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos del banco</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Bank: Banco de Cr&eacute;dito del Per&uacute;</span></li><li><span style='font-size:10pt'>Address: Street San Juan de Dios 123, Arequipa, Per&uacute;</span></li><li><span style='font-size:10pt'>Swift Code: BCPLPEPL</span></li><li><span style='font-size:10pt'>Account Number USD$: 215-1870934-1-48</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>COLOMBIA</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>Para transferencias Nacionales Colombia:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BS GRUPO COLOMBIA SAS</span></li><li><span style='font-size:10pt'>NIT: 900776296</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuenta de Ahorro en Pesos</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Datos del banco: Bancolombia</span></li><li><span style='font-size:10pt'>Numero de Cuenta: 65231918412</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>BOLIVIA</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>Para transferencias Nacionales Bolivia:</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Razon Social: BSG INSTITUTE BOLIVIA</span></li><li><span style='font-size:10pt'>NIT: 376053024</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuenta corriente en Bolivianos</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Datos del banco: Banco de Credito Bolivia</span></li><li><span style='font-size:10pt'>Numero de Cuenta Bolivianos: 701-5051921-3-41</span></li><li><span style='font-size:10pt'>Numero de Cuenta D&oacute;lares: 701-5041553-2-04</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* Considerar que al costo indicado en el cronograma de pagos se debe incluir las comisiones bancarias por transferencia.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p><p><h2>MEXICO</h2></p><p style='margin-left:30px'><span style='font-size:10pt'>1.	Pagos aceptado con Tarjeta Visa, Mastercard, American Express y Carnet (Débito y Crédito) a través de nuestra página web<a href='https://bsginstitute.com/Cuenta' style='font-size:13.3333px'>https://bsginstitute.com/</a></span></p><p style='margin-left:30px'><span style='font-size:10pt'>2. Pagos mediante D&eacute;positos Bancarios y Transferencia por SPEI*</span></p><p style='margin-left:30px'><span style='font-size:10pt'><strong>Datos de la empresa</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Raz&oacute;n Social: BSG Institute M&eacute;xico S.A. de C.V.</span></li><li><span style='font-size:10pt'>Raz&oacute;n Comercial: BSG Institute</span></li><li><span style='font-size:10pt'>RFC: BIM210209H26</span></li><li><span style='font-size:10pt'>Direcci&oacute;n: Montecito No. 38, Piso 33, Of. 4.</span></li><li><span style='font-size:10pt'>Edificio: World Trade Center – WTC</span></li><li><span style='font-size:10pt'>Colonia N&aacute;poles</span></li><li><span style='font-size:10pt'>Ciudad de M&eacute;xico</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'><strong>Cuentas</strong></span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Cuenta en Pesos Mexicanos en el BBVA Bancomer</span></li><li><span style='font-size:10pt'>N&uacute;mero de Cuenta: 0116490468</span></li><li><span style='font-size:10pt'>Cuenta CLABE Interbancaria: 012180001164904687</span></li></ul><ul style='margin-left:30px'><li><span style='font-size:10pt'>Cuenta en Dólares Americanos en el BBVA Bancomer</span></li><li><span style='font-size:10pt'>N&uacute;mero de Cuenta: 0116490522</span></li><li><span style='font-size:10pt'>Cuenta CLABE Interbancaria: 012180001164905220</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>3. Liga (Enlace) de Pago</span></p><ul style='margin-left:30px'><li><span style='font-size:10pt'>Deber&aacute; solicitar a su Asesor el envío de la liga de pago por correo electrónico, una vez recibido lo conduce a una p&aacute;gina web para completar el pago.</span></li></ul><p style='margin-left:30px'><span style='font-size:10pt'>* El pago podr&iacute;a est&aacute;r sujeto al cobro de comisiones del banco emisor, por favor verificar previamente.</span></p><p><span style='font-size:10pt'>&nbsp;</span></p></td></tr></table>";
            EtiquetaTarifarios = this.GetContenidoTarifario(tarifarios);
            var seccionModalidades = secciones.Where(s => s.Titulo.ToLower().Equals("modalidades")).FirstOrDefault();
            seccionModalidades.Contenido = "<table class=\"table table-hover \"><tr><td><strong>Modalidad</strong></td><td><strong>Fecha de Inicio</strong></td></tr>"
            + string.Join("", modalidades.Select(s => "<tr><td>" + s.TipoCiudad + "</td> <td>" + s.FechaHoraInicio + "</td></tr>").ToArray()) + "</table>";
            //Duracion y Horarios
            string contenido = secciones.Where(w => w.Titulo.ToLower().Equals("duración y horarios")).Select(w => w.Contenido).FirstOrDefault();
            //string newcontenido = this.GetContenidoHorarios(modalidades, this.IdPGeneral, contenido);
            string newcontenido = this.GetContenidoHorarios(modalidades, contenido);
            var seccionContenido = secciones.Where(s => s.Titulo.ToLower().Equals("duración y horarios")).FirstOrDefault();

            if (contenido == null)
            {
                InformacionProgramaDTO programaNuevo = new InformacionProgramaDTO();
                programaNuevo.Titulo = "Duración y Horarios";
                programaNuevo.Contenido = newcontenido;
                secciones.Add(programaNuevo);
            }
            else
            {
                seccionContenido.Contenido = newcontenido;
            }

            var html = string.Join("", secciones.Select(s => "<br><h4>" + s.Titulo + "</h4><br>" + s.Contenido).ToArray());
            EtiquetaBeneficiosInversion = string.Join("", seccionesBeneficiosInversion.Where(w=>w.Titulo.ToLower().Equals("inversion") || w.Titulo.ToLower().Equals("beneficios") || w.Titulo.ToLower().Equals("monto actual")).Select(s => "<br><h4 class='col-sm-10' Id='IdTabla"+ s.Titulo +"'>" + s.Titulo + "</h4><br>" + s.Contenido).ToArray());
            EtiquetaExpositores = seccionExpositor.Contenido;
            EtiquetaDuracionHorarios = newcontenido;
            InformacionPrograma = html;
        }

        /// <summary>
        /// Obtiene el resumen de programas filtrados por area, subarea y pais
        /// </summary>
        /// <param name="filtros"></param>
        public void CargarResumenProgramas()
        {
            this.ResumenProgramas = _repPGeneral.ObtenerResumenPrograma(this.Filtros);
            var programasInicio = ResumenProgramas;
            var programas = programasInicio.Where(x => x.Orden == 1).ToList();
            int id = -1;
            List<ResumenProgramaDTO> lista = new List<ResumenProgramaDTO>();
            bool flag = false;
            int count = 0;
            foreach (var pro in programas)
            {
                if (pro.IdPGeneral != id)
                {
                    if (!flag && count != 0)
                    {
                        var agregado = programas.Where(x => x.Pais.Equals(0) && x.IdPGeneral == id).FirstOrDefault();
                        if (agregado != null)
                        {
                            lista.Add(agregado);                            
                        }
                        flag = false;

                    }
                    if (pro.Pais.Equals(this.CodigoPais))
                    {
                        lista.Add(pro);
                        flag = true;
                    }
                    id = pro.IdPGeneral;
                    count++;
                }
                else
                {
                    if (pro.Pais.Equals(this.CodigoPais))
                    {
                        lista.Add(pro);
                        flag = true;
                    }
                    count++;
                }
                if (count == programas.Count() - 1 && flag == false)
                {
                    var agregado2 = programas.Where(x => x.Pais.Equals(0) && x.IdPGeneral == pro.IdPGeneral).FirstOrDefault();
                    if(agregado2 != null)
                    lista.Add(agregado2);
                }
            }
            lista = lista.OrderByDescending(x => x.ContadoDolares).ToList();
            foreach (var lis in lista)
            {
                lis.Versiones = programasInicio.Where(x => x.Pais.Equals(lis.Pais) && x.IdPGeneral == lis.IdPGeneral).Select(x => new ProgramaVersionDTO
                {
                    NombreVersion = x.NombreVersion,
                    Contado = x.InversionContado,
                    Credito = x.InversionCredito
                }).ToList();
            }
            ResumenProgramas = lista;
        }

        /// Autor: , Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Cargar Resumen Programas version 2
        /// </summary>	
        /// <returns></returns> 
        public void CargarResumenProgramasV2()
		{
			try
			{
				DocumentosBO documento = new DocumentosBO();
				
				var listaResumenPrograma = _repPGeneral.ObtenerResumenProgramaV2(this.Filtros);
				int idPais = Convert.ToInt32(this.Filtros["codigoPais"]);
				var listaResumenProgramatemporal = listaResumenPrograma.Where(x => x.IdPais == idPais).ToList();
                if (listaResumenProgramatemporal.Count == 0)
                {
                    listaResumenPrograma = listaResumenPrograma.Where(x => x.IdPais == 0).ToList(); //Internacional
                }
                else
                {
                    listaResumenPrograma = listaResumenProgramatemporal;
                }
				var listaResumenProgramaAgrupado = listaResumenPrograma.GroupBy(x => new { x.IdPrograma, x.NombrePrograma, x.DuracionPrograma, x.IdArea, x.IdSubArea }).Select(x => new MontoProgramaAgrupadoDTO
				{
					IdPrograma = x.Key.IdPrograma,
					IdArea = x.Key.IdArea,
					IdSubArea = x.Key.IdSubArea,
					NombrePrograma = x.Key.NombrePrograma,
					Duracion = x.Key.DuracionPrograma,
					MontoDetalle = x.GroupBy(y => y.Descripcion).Select(y => new MontoProgramaDetalleDTO
					{
						Version = y.Key,
						VersionDetalle = y.Select(z => new MontoProgramaVersionDetalle
						{
							IdTipoPago = z.IdTipoPago,
							TipoPago = z.TipoPago,
							SimboloMoneda = z.SimboloMoneda,
							Matricula = z.Matricula,
							Cuotas = z.Cuotas,
							NroCuotas = z.NroCuotas
						}).OrderByDescending(a => a.TipoPago).ToList()
					}).ToList()
				}).ToList();

				foreach (var item in listaResumenProgramaAgrupado)
				{
					var certificacionV2 = documento.ObtenerListaSeccionDocumentoProgramaGeneral(item.IdPrograma);
					//item.SeccionCertificadoV2 = documento.ObtenerListaSeccionDocumentoProgramaGeneral(item.IdPrograma).Where(a => a.Seccion.Contains("Certificacion")).FirstOrDefault();
					if (certificacionV2 == null || certificacionV2.Count == 0 || !certificacionV2.Any(a => a.Seccion.Contains("Certificacion")))
					{
						var certificacionV1 = _repDocumentoSeccionPw.ObtenerSecciones(item.IdPrograma);
						if(certificacionV1 != null && certificacionV1.Count > 0)
							item.SeccionCertificadoV1 = certificacionV1.Where(x => x.Titulo.Contains("Certificación")).FirstOrDefault();
					}
					else
					{
						item.SeccionCertificadoV2 = certificacionV2.Where(a => a.Seccion.Contains("Certificacion")).FirstOrDefault();
					}
				}
				this.ResumenProgramasV2 = ObtenerResumenProgramaHTML(listaResumenProgramaAgrupado);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}


        /// Autor: , Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los montos/precios de un programa general
        /// </summary>	
        /// <returns>Retorna Montos Por Pais:List<MontoPagoModalidadDTO></returns> 
        private List<MontoPagoModalidadDTO> ObtenerMontos()
        {
            var montos = _repPGeneral.ObtenerMontosPorId(this.IdPGeneral);
            var montosPorPais = montos.Where(s => s.Pais.Equals(this.CodigoPais)).OrderBy(x => x.Paquete).ToList();
            if (montosPorPais.Count() == 0)
            {
                var result1 = montos.Where(s => s.Pais.Equals(0)).OrderBy(x => x.Paquete).ToList();
                if (result1.Where(x => x.Paquete == 0).ToList().Count() == 0)
                {
                    //tipo 1
                    var beneficios = _repPGeneral.ObtenerBeneficiosPGeneralTipo1(this.IdPGeneral);
                    foreach (var item in result1)
                    {
                        var items = beneficios.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                        string Detalles = "<ul>";
                        foreach (var item2 in items)
                        {
                            Detalles += "<li>" + item2 + "</li>";
                        }
                        Detalles += "</ul>";
                        item.Beneficios = Detalles;

                    }
                    ListaBeneficios = beneficios;
                }
                else
                {
                    //tipo 2
                    var beneficio = _repPGeneral.ObtenerBeneficiosPGeneralTipo2(this.IdPGeneral); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB).FirstOrDefault();
                    foreach (var item2 in result1)
                    {
                        item2.Beneficios = beneficio.Titulo;
                    }
                    ListaBeneficios.Add(beneficio);
                }
                return result1;
            }
            else
            {
                if (montosPorPais.Where(x => x.Paquete == 0).ToList().Count() == 0)
                {
                    var beneficios = _repPGeneral.ObtenerBeneficiosPGeneralTipo1(this.IdPGeneral, this.CodigoPais); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB);
                    foreach (var item in montosPorPais)
                    {
                        var items = beneficios.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                        string detalles = "<ul>";
                        foreach (var _item in items)
                        {
                            detalles += "<li>" + _item + "</li>";
                        }
                        detalles += "</ul><br>";
                        item.Beneficios = detalles;
                    }
                    ListaBeneficios = beneficios;
                }
                else
                {
                    var beneficio = _repPGeneral.ObtenerBeneficiosPGeneralTipo2(this.IdPGeneral); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB).FirstOrDefault();
                    foreach (var item2 in montosPorPais)
                    {
                        item2.Beneficios = beneficio.Titulo;
                    }
                    ListaBeneficios.Add(beneficio);
                }
                return montosPorPais;
            }
        }

        /// Autor: , Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener monto version 2
        /// </summary>		
        /// <returns>Retorna Montos por pais: List<MontoPagoModalidadDTO></returns> 
        private List<MontoPagoModalidadDTO> ObtenerMontos2()
        {
            var montos = _repPGeneral.ObtenerMontosPorId(this.IdPGeneral);
            var montosPorPais = montos.Where(s => s.Pais.Equals(this.CodigoPais)).OrderBy(x => x.Paquete).ToList();
            if (montosPorPais.Count() == 0)
            {
                var result1 = montos.Where(s => s.Pais.Equals(0)).OrderBy(x => x.Paquete).ToList();
                //if (result1.Where(x => x.Paquete == 0).ToList().Count() == 0)
                //{
                //    //tipo 1
                //    //var beneficios = _repPGeneral.ObtenerBeneficiosPGeneralTipo1(this.IdPGeneral);
                //    //foreach (var item in result1)
                //    //{
                //    //    var items = beneficios.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                //    //    string Detalles = "<ul>";
                //    //    foreach (var item2 in items)
                //    //    {
                //    //        Detalles += "<li>" + item2 + "</li>";
                //    //    }
                //    //    Detalles += "</ul>";
                //    //    item.Beneficios = Detalles;

                //    //}
                //    //ListaBeneficios = beneficios;
                //    var beneficio = _repPGeneral.ObtenerBeneficiosPGeneralTipo1Internacional(this.IdPGeneral);
                //    foreach (var item in result1)
                //    {
                //        var items = beneficio.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                //        string detalles = "<ul>";
                //        foreach (var _item in items)
                //        {
                //            detalles += "<li>" + _item + "</li>";
                //        }
                //        detalles += "</ul><br>";
                //        item.Beneficios = detalles;
                //    }
                //    ListaBeneficios = beneficio;
                //}
                //else
                //{
                //    //tipo 2
                //    //var beneficio = _repPGeneral.ObtenerBeneficiosPGeneralTipo2(this.IdPGeneral); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB).FirstOrDefault();
                //    //foreach (var item2 in result1)
                //    //{
                //    //    item2.Beneficios = beneficio.Titulo;
                //    //}
                //    //ListaBeneficios.Add(beneficio);
                //    var beneficio = _repPGeneral.ObtenerBeneficiosPGeneralTipo1Internacional(this.IdPGeneral);
                //    foreach (var item in result1)
                //    {
                //        var items = beneficio.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                //        string detalles = "<ul>";
                //        foreach (var _item in items)
                //        {
                //            detalles += "<li>" + _item + "</li>";
                //        }
                //        detalles += "</ul><br>";
                //        item.Beneficios = detalles;
                //    }
                //    ListaBeneficios = beneficio;
                //}
                var beneficio = _repPGeneral.ObtenerBeneficiosPGeneralTipo1V2Internacional(this.IdPGeneral);
                if (beneficio.Count() > 0)
                {
                    foreach (var item in result1)
                    {
                        var items = beneficio.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.Titulo).Select(w => w.Titulo).ToList();
                        string detalles = "<ul>";
                        foreach (var _item in items)
                        {
                            detalles += "<li>" + _item + "</li>";
                        }
                        detalles += "</ul><br>";
                        item.Beneficios = detalles;
                    }
                }
                    
                ListaBeneficios = beneficio;
                return result1;
            }
            else
            {
                //if (montosPorPais.Where(x => x.Paquete == 0).ToList().Count() == 0)
                //{
                //    var beneficios = _repPGeneral.ObtenerBeneficiosPGeneralTipo1(this.IdPGeneral, this.CodigoPais); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB);
                //    foreach (var item in montosPorPais)
                //    {
                //        var items = beneficios.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                //        string detalles = "<ul>";
                //        foreach (var _item in items)
                //        {
                //            detalles += "<li>" + _item + "</li>";
                //        }
                //        detalles += "</ul><br>";
                //        item.Beneficios = detalles;
                //    }
                //    ListaBeneficios = beneficios;
                //}
                //else
                //{
                //    //var beneficio = _repPGeneral.ObtenerBeneficiosPGeneralTipo2(this.IdPGeneral); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB).FirstOrDefault();
                //    //foreach (var item2 in montosPorPais)
                //    //{
                //    //    item2.Beneficios = beneficio.Titulo;
                //    //}
                //    //ListaBeneficios.Add(beneficio);
                //    var beneficios = _repPGeneral.ObtenerBeneficiosPGeneralTipo1(this.IdPGeneral, this.CodigoPais); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB);
                //    foreach (var item in montosPorPais)
                //    {
                //        var items = beneficios.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                //        string detalles = "<ul>";
                //        foreach (var _item in items)
                //        {
                //            detalles += "<li>" + _item + "</li>";
                //        }
                //        detalles += "</ul><br>";
                //        item.Beneficios = detalles;
                //    }
                //    ListaBeneficios = beneficios;

                //}
                
                var beneficios = _repPGeneral.ObtenerBeneficiosPGeneralTipo1V2(this.IdPGeneral, this.CodigoPais); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB);
                if (beneficios.Count() > 0)
                {
                    foreach (var item in montosPorPais)
                    {                    
                        var items = beneficios.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                        string detalles = "<ul>";
                        if(items.Count() >0)
                        {
                            foreach (var _item in items)
                            {
                                detalles += "<li>" + _item + "</li>";
                            }
                            detalles += "</ul><br>";
                            item.Beneficios = detalles;
                        }     
                    }                    
                }
                ListaBeneficios = beneficios;
                return montosPorPais;
            }
        }



        /// Autor: Jose Villena
        /// Fecha: 03/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene montos pagados
        /// </summary>	
        /// <returns>Lista de montos pagados:List<MontoPagadoDTO></returns> 
        private List<MontoPagadoDTO> ObtenerMontoPagado()
        {
            var monto = _repPGeneral.ObtenerMontoPagado(this.IdMatriculaCabecera,this.IdOportunidad);
            return monto;
        }

 
        /// Autor: Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtienen los contenidos de los programas generales por modalidad
        /// </summary>
        /// <param name="modalidades"> Objeto Lista de tipo ModalidadProgramaDTO </param>
        /// <param name="initContenido"> Contenido Inicial </param>        
        /// <returns>Contenido Programa general: initContenido </returns> 
        public string GetContenidoHorarios(List<ModalidadProgramaDTO> modalidades, string initContenido)
        {
            PGeneralParametroSeoPwRepositorio _repPGeneralParametroSeoPw = new PGeneralParametroSeoPwRepositorio();
            ExcepcionFrecuenciaPwRepositorio _repExcepcionFrecuenciaPw = new ExcepcionFrecuenciaPwRepositorio();
            string _filtroVista = string.Empty;

            var parametrosSEO = _repPGeneralParametroSeoPw.ObtenerParametrosSEOPorIdPGeneral(this.IdPGeneral);
            var excepcionesFrecuencia = _repExcepcionFrecuenciaPw.ObtenerTodoProgramaGeneral();
            //var padreEspecificoHijo = _repPGeneral.ObtenerPadreHijoEspecifico(this.IdPGeneral); VERSION ANTERIOR
            var padreEspecificoHijo = _repPGeneral.ObtenerPadreHijoEspecificoV2(this.IdPGeneral); //VERSION NUEVA
            var obtenerFrecuencia = _repPGeneral.ObtenerFrecuenciasPorIdPGeneral(this.IdPGeneral);
            var especificoSesion = _repPGeneral.ObtenerSesionesProgramaGeneralValidadoVisualizacionAgenda(this.IdPGeneral);
            var especificoSesionSinValidacionVisualizacion = _repPGeneral.ObtenerSesionesPorProgramaGeneral(this.IdPGeneral);

            var tipo = parametrosSEO.Where(w => w.Nombre.Equals("description")).FirstOrDefault();
            string presencialDatos = "";
            string sincronicoDatos = "";
            string extraPresencial = "";
            string extraSincrono = "";
            
            var programaGeneral = modalidades.FirstOrDefault() == null && modalidades.Count()!=0 ? new ModalidadProgramaDTO { Pw_duracion = "0",NombrePG=""} : modalidades.FirstOrDefault();
            //programaGeneral = programaGeneral == null ? new ModalidadProgramaDTO { } : programaGeneral;
            if (programaGeneral != null)
            {
                if (programaGeneral.NombrePG.Contains("Curso") || programaGeneral.NombrePG.Contains("Programa"))
                {
                    //plantilla
                    presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p>El ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                    sincronicoDatos = "<p><strong>En la modalidad online sincrónica (clases en vivo):</strong></p><p> El ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                    extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                    extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicita tu cronograma.</i><p/>";
                }
                else
                {
                    if (tipo.Descripcion.Contains("Curso"))
                    {
                        //plantilla
                        presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p> El Curso ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                        sincronicoDatos = "<p><strong>En la modalidad online sincrónica (clases en vivo):</strong></p><p> El Curso ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                        extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                        extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicita tu cronograma.</i><p/>";
                    }
                    if (tipo.Descripcion.Contains("Programa"))
                    {
                        //plantilla
                        presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p> El Programa ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                        sincronicoDatos = "<p><strong>En la modalidad online sincrónica (clases en vivo):</strong></p><p> El Programa ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                        extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                        extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicita tu cronograma.</i><p/>";
                    }
                }
            }

            var programaEspecifico= modalidades;
            string frecuencia = "";
            string todohtmlpresencial = this.ObtenerContenidoPresencial(programaEspecifico.Where(w => w.Tipo == "Presencial").ToList(), true, excepcionesFrecuencia, padreEspecificoHijo , obtenerFrecuencia, especificoSesion);
            if (todohtmlpresencial == "" || todohtmlpresencial == "NotFound")
            {
                presencialDatos = "";
                extraPresencial = "";
            }
            string todohtmlsincrono = this.ObtenerContenidoPresencial(programaEspecifico.Where(w => w.Tipo == "Online Sincronica").ToList(), false, excepcionesFrecuencia, padreEspecificoHijo, obtenerFrecuencia, especificoSesion);
            //Se creó la condición de retorno NotFound para validar en caso que no se haya configurado ninguna sesión con visualización en el portal para obtener todos los programas específicos
            if (todohtmlsincrono == "NotFound")
            {
                //En caso de no encontrar ningun horario buscamos con todos los programas específicos asociados
                todohtmlsincrono = this.ObtenerContenidoPresencial(programaEspecifico.Where(w => w.Tipo == "Online Sincronica").ToList(), false, excepcionesFrecuencia, padreEspecificoHijo, obtenerFrecuencia, especificoSesionSinValidacionVisualizacion);
            }
            if (todohtmlsincrono == "" || todohtmlsincrono == "NotFound")
            {
                sincronicoDatos = "";
                extraSincrono = "";
            }
            else
            {
                var sincrona = modalidades.Where(x => x.Tipo == "Online Sincronica").FirstOrDefault();
                if (sincrona.NombreESP.Contains("Curso"))
                {
                    var padre = padreEspecificoHijo.Where(w => w.IdPespecificoHijo == sincrona.IdPEspecifico).FirstOrDefault();
                    if (padre != null)
                        frecuencia = obtenerFrecuencia.Where(w => w.IdPEspecifico == padre.IdPespecificoHijo).Select(w => w.Nombre).FirstOrDefault();//this.ObtenerFrecuencia(padre.PEspecificoPadreId);
                    else
                        frecuencia = obtenerFrecuencia.Where(w => w.IdPEspecifico == sincrona.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();//this.ObtenerFrecuencia(sincrona.id);
                }
                else
                    frecuencia = frecuencia = obtenerFrecuencia.Where(w => w.IdPEspecifico == sincrona.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
            }
            string seContenido = initContenido;
            seContenido = presencialDatos + todohtmlpresencial + extraPresencial + sincronicoDatos + todohtmlsincrono + extraSincrono + seContenido;

            if (programaEspecifico.Count() != 0)
            {
                var sincronico = programaEspecifico.Where(x => x.Tipo.ToLower().Equals("online sincronica")).FirstOrDefault();
                if (sincronico != null)
                {
                    string temppe = seContenido.Replace("##DURACIONPE##", Convert.ToInt32(double.Parse(programaEspecifico.Where(x => x.Tipo.ToLower().Equals("online sincronica")).FirstOrDefault().Duracion, CultureInfo.InvariantCulture)) + " Horas").Replace("##DURACIONPG##", programaGeneral.Pw_duracion).Replace("##CURSODIPLOMA##", programaGeneral.NombrePG).Replace("##FRECUENCIA##", frecuencia);
                    temppe = temppe.Replace("®", "<sup>®</sup>");
                    seContenido = temppe;
                }
                else
                {
                    string temppe = seContenido.Replace("##DURACIONPE##", Convert.ToInt32(double.Parse(programaEspecifico.FirstOrDefault().Duracion, CultureInfo.InvariantCulture)) + " Horas").Replace("##DURACIONPG##", programaGeneral.Pw_duracion).Replace("##CURSODIPLOMA##", programaGeneral.NombrePG).Replace("##FRECUENCIA##", frecuencia); //Version 1 Para revertir
                    temppe = temppe.Replace("®", "<sup>®</sup>");
                    seContenido = temppe;
                }
                //Version Anterior
                //string temppe = seContenido.Replace("##DURACIONPE##", Convert.ToInt32(double.Parse(programaEspecifico.FirstOrDefault().Duracion, CultureInfo.InvariantCulture)) + " Horas").Replace("##DURACIONPG##", programaGeneral.Pw_duracion).Replace("##CURSODIPLOMA##", programaGeneral.NombrePG).Replace("##FRECUENCIA##", frecuencia); //Version 1 Para revertir
                //temppe = temppe.Replace("®", "<sup>®</sup>");
                //seContenido = temppe;
            }
            else
            {
                if (seContenido != ""  && programaGeneral !=null)
                {
                    string temppe = seContenido.Replace("##DURACIONPE##", "" + " Horas").Replace("##DURACIONPG##", programaGeneral.Pw_duracion).Replace("##CURSODIPLOMA##", programaGeneral.NombrePG).Replace("##FRECUENCIA##", frecuencia);
                    temppe = temppe.Replace("®", "<sup>®</sup>");
                    seContenido = temppe;
                }
            }
            return seContenido;
        }

        /// Autor: Jose Villena
        /// Fecha: 03/0/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener contenido tarifario
        /// </summary>	
        /// <param name="tarifarios"> Objeto tipo Lista TarifarioDetalleAgendaDTO </param>	
        /// <returns>Lista contenido Tarifario:List<TarifarioDetalleAgendaDTO></returns> 
        public string GetContenidoTarifario(List<TarifarioDetalleAgendaDTO> tarifarios)
        {
            string contenido = "<h3>Tarifario de Tasas Academicas y Administrativas</h3><br><table class='table table-bordered'><thead class='text-center' style='background: #ffd800;'><tr><th>Nº</th><th>CONCEPTO</th><th>DESCRIPCION</th><th> MONTO PERU </th><th>MONTO COLOMBIA</th><th>MONTO BOLIVIA</th><th>MONTO MEXICO</th><th>MONTO EXTRANJERO</th></tr></thead>";
            contenido = contenido + "<tbody>";
            int contador = 1;
            foreach (var item in tarifarios)
            {
                contenido = contenido + "<tr>";

                contenido = contenido + "<td>"+ contador + "</td>";
                contenido = contenido + "<td>"+ item.Concepto + "</td>";
                contenido = contenido + "<td>"+ item.Descripcion + "</td>";
                contenido = contenido + "<td class='text-center'><b><span>"+ item.MontoPeru+ "</span></b></td>";
                contenido = contenido + "<td class='text-center'><b><span>" + item.MontoColombia + "</span></b></td>";
                contenido = contenido + "<td class='text-center'><b><span>" + item.MontoBolivia + "</span></b></td>";
                contenido = contenido + "<td class='text-center'><b><span>" + item.MontoMexico + "</span></b></td>";
                contenido = contenido + "<td class='text-center'><b><span>" + item.MontoExtranjero + "</span></b></td>";
                contenido = contenido + "</tr>";
                contador++;
            }

            contenido = contenido + "</tbody> </table>";
            return contenido;

        }

        /// Autor: Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener contenido presencial
        /// </summary>
        /// <param name="pEspecificos"> Objeto tipo ModalidadProgramaDTO </param>
        /// <param name="presencial"> Presencion </param>
        /// <param name="excepcionesFrecuencia"> Objeto tipo ExcepcionFrecuenciaPGeneralDTO </param>		
        /// <param name="padreEspecificoHijo"> Objeto tipo PadrePespecificoHijoDTO </param>		
        /// <param name="frecuenciaProgramas"> Objeto tipo FrecuenciaProgramaGeneralDTO </param>		
        /// <param name="especificoSesion"> Objeto tipo PEspecificoSesionDTO </param>		
        /// <returns>Contenido Presencial: strHTML </returns> 
        private string ObtenerContenidoPresencial(List<ModalidadProgramaDTO> pEspecificos, bool presencial, List<ExcepcionFrecuenciaPGeneralDTO> excepcionesFrecuencia, List<PadrePespecificoHijoDTO> padreEspecificoHijo, List<FrecuenciaProgramaGeneralDTO> frecuenciaProgramas, List<PEspecificoSesionDTO> especificoSesion)
        {
            string horariosP1 = "<li>##DIA##: ##HORAINICIO## a ##HORAFIN## horas.</li>";
            string horariosP2 = "<li>##DIA##: ##HORAINICIO## a ##HORAFIN## - ##HORAINICIO2## a ##HORAFIN2## horas.</li>";
            string datosCiudad = "<p>En ##CIUDAD## con una frecuencia ##FRECUENCIA##:</p> <ul class='list'>##HORARIOS##</ul>";
            string datosSinCiudad = "<ul class='list'>##HORARIOS##</ul>";
            string[] arreglos = new string[pEspecificos.ToList().Count];
            int contador = 0;
            string strHTML = "";
            foreach (var item in pEspecificos)
            {
                //trae la lista de idpespecifico de excepciones
                var listaExcepciones = excepcionesFrecuencia.Select(w => w.IdPEspecifico).ToList();
                string frecuencia = "";
                if (listaExcepciones.Contains(item.IdPEspecifico))//lee tabla de excepciones
                {
                    frecuencia = "Diaria";
                }
                else //sigue flujo normal
                {
                    if (item.NombreESP.Contains("Curso"))
                    {
                        var padre = padreEspecificoHijo.Where(w => w.IdPespecificoHijo == item.IdPEspecifico).FirstOrDefault();
                        if (padre != null)
                            frecuencia = frecuenciaProgramas.Where(w => w.IdPEspecifico == padre.IdPespecificoPadre).Select(w => w.Nombre).FirstOrDefault();
                        else
                        {
                            frecuencia = frecuenciaProgramas.Where(w => w.IdPEspecifico == item.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
                        }
                    }
                    else
                    {
                        frecuencia = frecuenciaProgramas.Where(w => w.IdPEspecifico == item.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
                    }
                }
                List<PEspecificoSesionDTO> sesionestemp = new List<PEspecificoSesionDTO>();
                if (item.NombreESP.Contains("Curso"))
                {
                    var hijo = item;
                    //obtener las sesiones del pespcifico
                    sesionestemp = especificoSesion.Where(w => w.IdPEspecifico == hijo.IdPEspecifico && w.Predeterminado == true).ToList();
                    if (!sesionestemp.Any())
                    {
                        return "NotFound";
                    }
                }
                else
                {
                    //obtenemos un pespecifico
                    var hijo = padreEspecificoHijo.Where(w => w.IdPespecificoPadre == item.IdPEspecifico).ToList();
                    if (hijo.Count() == 0)
                    {
                        var hijotemp = item;
                        sesionestemp = especificoSesion.Where(w => w.IdPEspecifico == hijotemp.IdPEspecifico && w.Predeterminado == true).ToList();
                    }
                    else
                    {
                        foreach (var itemHijo in hijo)
                        {
                            var temporal = especificoSesion.Where(w => w.IdPEspecifico == itemHijo.IdPespecificoHijo && w.Predeterminado == true && w.Estado == true).ToList();
                            sesionestemp.AddRange(temporal);
                        }
                    }
                }
                //lista donde se almacenara las sesiones
                List<SesionTempDTO> lista = new List<SesionTempDTO>();
                //llenas las sesiones  dia horainicio horafin
                foreach (var item3 in sesionestemp.OrderBy(w => w.FechaHoraInicio))
                {
                    SesionTempDTO itemlista = new SesionTempDTO();

                    itemlista.Dia = new CultureInfo("es-PE", false).TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd"));
                    //itemlista.Dia
                    if (itemlista.Dia == "Monday")
                    {
                        itemlista.Dia = "Lunes";
                    }
                    else if (itemlista.Dia == "Tuesday")
                    {
                        itemlista.Dia = "Martes";
                    }
                    else if (itemlista.Dia == "Wednesday")
                    {
                        itemlista.Dia = "Miércoles";
                    }
                    else if (itemlista.Dia == "Thursday")
                    {
                        itemlista.Dia = "Jueves";
                    }
                    else if (itemlista.Dia == "Friday")
                    {
                        itemlista.Dia = "Viernes";
                    }
                    else if (itemlista.Dia == "Saturday")
                    {
                        itemlista.Dia = "Sábado";
                    }
                    else if (itemlista.Dia == "Sunday")
                    {
                        itemlista.Dia = "Domingo";
                    }
                    //Dia = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd"));
                    itemlista.Idciudad = this.GetIdDia(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd")));
                    itemlista.Horainicio = item3.FechaHoraInicio.Value.ToString("HH:mm");
                    itemlista.Veces = 1;
                    itemlista.Horafin = item3.FechaHoraInicio.Value.AddHours(Convert.ToDouble(item3.Duracion)).ToString("HH:mm");
                    itemlista.Ciudad = item.Ciudad;
                    
                    lista.Add(itemlista);
                }
                lista = lista.Distinct().ToList();

                List<SesionTempDTO> listaFinal = new List<SesionTempDTO>();
                SesionTempDTO itemTempListaFinal = new SesionTempDTO();
                foreach (var item_lista in lista)
                {
                    if (!listaFinal.Any(w => w.Dia == item_lista.Dia && w.Ciudad == item_lista.Ciudad && w.Horainicio == item_lista.Horainicio && w.Horafin == item_lista.Horafin))
                        listaFinal.Add(item_lista);
                    else
                    {
                        itemTempListaFinal = listaFinal.Where(w => w.Dia == item_lista.Dia && w.Ciudad == item_lista.Ciudad && w.Horainicio == item_lista.Horainicio && w.Horafin == item_lista.Horafin).FirstOrDefault();
                        listaFinal.Remove(itemTempListaFinal);
                        itemTempListaFinal.Veces++;
                        listaFinal.Add(itemTempListaFinal);
                    }

                }
                lista = listaFinal;

                List<SesionTempDTO> listafinal = new List<SesionTempDTO>();

                foreach (var item_ in lista.OrderBy(w => w.Idciudad))
                {
                    var tempFechas = lista.Where(w => w.Dia == item_.Dia).ToList();
                    //valida
                    var contadorlista = lista.Where(w => w.Veces == 1).ToList().Count();
                    if (!listafinal.Any(w => w.Dia == tempFechas.FirstOrDefault().Dia))
                    {
                        if (tempFechas.Count() == 1)
                        {
                            if (listafinal == null)
                            {
                                listafinal = tempFechas;
                                listafinal.FirstOrDefault().Tipo = false;//tipo normal
                            }
                            else
                            {
                                tempFechas.FirstOrDefault().Tipo = false;
                                listafinal.Add(tempFechas.FirstOrDefault());
                            }
                        }
                        if (tempFechas.Count() == 2)
                        {
                            tempFechas = tempFechas.OrderByDescending(w => w.Veces).ToList();

                            if (Math.Abs(Convert.ToDateTime(tempFechas.First().Horainicio).Hour - Convert.ToDateTime(tempFechas.Last().Horainicio).Hour) < 2)
                            {
                                listafinal.Add(tempFechas.First());
                                continue;
                            }

                            if (tempFechas.First().Veces - tempFechas.Last().Veces > 5)
                            {
                                SesionTempDTO listafinaltempfinal = new SesionTempDTO();
                                listafinaltempfinal = tempFechas.First();
                                listafinaltempfinal.Tipo = false;
                                listafinal.Add(listafinaltempfinal);
                            }
                            else
                            {
                                tempFechas = tempFechas.OrderBy(w => w.Horainicio).ToList();

                                if (tempFechas.First().Dia == tempFechas.Last().Dia)
                                {
                                    SesionTempDTO listafinaltempfinal = new SesionTempDTO();

                                    listafinaltempfinal.Dia = tempFechas.First().Dia;
                                    listafinaltempfinal.Horainicio = tempFechas.First().Horainicio;
                                    listafinaltempfinal.Horafin = tempFechas.First().Horafin;
                                    listafinaltempfinal.Horainicio2 = tempFechas.Last().Horainicio;
                                    listafinaltempfinal.Horafin2 = tempFechas.Last().Horafin;
                                    listafinaltempfinal.Tipo = true;//tipo doble horario
                                    listafinal.Add(listafinaltempfinal);
                                }
                                else
                                {
                                    SesionTempDTO listafinaltempfinal = new SesionTempDTO();
                                    SesionTempDTO listafinaltemp2final = new SesionTempDTO();

                                    listafinaltempfinal.Dia = tempFechas.First().Dia;
                                    listafinaltempfinal.Horainicio = tempFechas.First().Horainicio;
                                    listafinaltempfinal.Horafin = tempFechas.First().Horafin;
                                    listafinaltempfinal.Horainicio2 = tempFechas.Last().Horainicio;
                                    listafinaltempfinal.Horafin2 = tempFechas.Last().Horafin;
                                    listafinaltempfinal.Tipo = false;//tipo doble horario

                                    listafinaltemp2final.Dia = tempFechas.Last().Dia;
                                    listafinaltemp2final.Horainicio = tempFechas.Last().Horainicio;
                                    listafinaltemp2final.Horafin = tempFechas.Last().Horafin;
                                    listafinaltemp2final.Horainicio2 = tempFechas.Last().Horainicio;
                                    listafinaltemp2final.Horafin2 = tempFechas.Last().Horafin;
                                    listafinaltemp2final.Tipo = false;//tipo doble horario

                                    listafinal.Add(listafinaltempfinal);
                                    listafinal.Add(listafinaltemp2final);
                                }
                            }
                        }
                        if (tempFechas.Count() == 3)
                        {
                            tempFechas = tempFechas.OrderByDescending(w => w.Veces).ToList();
                            if (tempFechas.First().Veces - tempFechas.Skip(1).Take(1).First().Veces > 5)
                            {
                                var _tempFechas = tempFechas;
                                tempFechas = new List<SesionTempDTO>();
                                tempFechas.Add(_tempFechas.First());
                            }
                            else
                            {
                                tempFechas.Remove(tempFechas.Last());
                            }
                            SesionTempDTO listafinaltemp = new SesionTempDTO();
                            SesionTempDTO listafinaltemp2 = new SesionTempDTO();
                            string day = tempFechas.First().Dia;
                            var tempa = tempFechas.Where(w => w.Dia == day).ToList();

                            if (tempa.Count == 1)
                            {
                                listafinaltemp.Dia = tempa.First().Dia;
                                listafinaltemp.Horainicio = tempa.First().Horainicio;
                                listafinaltemp.Horafin = tempa.First().Horafin;
                                listafinaltemp.Tipo = false;//tipo doble horario
                                listafinal.Add(listafinaltemp);
                            }
                            if (tempa.Count == 2)
                            {
                                if (Math.Abs(Convert.ToDateTime(tempa.First().Horainicio).Hour - Convert.ToDateTime(tempa.Last().Horainicio).Hour) < 2)
                                {
                                    tempa = tempa.OrderBy(w => w.Veces).ThenBy(w => w.Horainicio).ToList();
                                    listafinaltemp.Dia = tempa.First().Dia;
                                    listafinaltemp.Horainicio = tempa.First().Horainicio;
                                    listafinaltemp.Horafin = tempa.First().Horafin;
                                    listafinaltemp.Tipo = false;//tipo doble horario
                                    listafinal.Add(listafinaltemp);
                                }
                                else
                                {
                                    tempa = tempa.OrderBy(w => w.Horainicio).ToList();
                                    listafinaltemp.Dia = tempa.First().Dia;
                                    listafinaltemp.Horainicio = tempa.First().Horainicio;
                                    listafinaltemp.Horafin = tempa.First().Horafin;
                                    listafinaltemp.Horainicio2 = tempa.Last().Horainicio;
                                    listafinaltemp.Horafin2 = tempa.Last().Horafin;
                                    listafinaltemp.Tipo = true;//tipo doble horario
                                    listafinal.Add(listafinaltemp);
                                }
                            }
                        }
                        if (tempFechas.Count() > 3)
                        {
                            tempFechas = tempFechas.OrderByDescending(w => w.Veces).ToList();
                            SesionTempDTO listafinaltemp = new SesionTempDTO();

                            if (tempFechas.First().Veces - tempFechas.Skip(1).Take(1).First().Veces > 5)
                            {
                                SesionTempDTO listafinaltempfinal = new SesionTempDTO();
                                listafinaltempfinal = tempFechas.First();
                                listafinaltempfinal.Tipo = false;
                                listafinal.Add(listafinaltempfinal);
                            }
                            else
                            {
                                List<SesionTempDTO> tempvarios = new List<SesionTempDTO>();
                                tempvarios.Add(tempFechas.First());
                                tempvarios.Add(tempFechas.Skip(1).Take(1).First());
                                tempvarios = tempvarios.OrderBy(w => w.Horainicio).ToList();
                                listafinaltemp.Dia = tempvarios.First().Dia;
                                listafinaltemp.Horainicio = tempvarios.First().Horainicio;
                                listafinaltemp.Horafin = tempvarios.First().Horafin;
                                listafinaltemp.Horainicio2 = tempvarios.Skip(1).Take(1).First().Horainicio;
                                listafinaltemp.Horafin2 = tempvarios.Skip(1).Take(1).First().Horafin;
                                listafinaltemp.Tipo = true;//tipo doble horario
                                listafinal.Add(listafinaltemp);
                            }
                        }
                    }
                }
                foreach (var variable in listafinal)
                {
                    if (variable.Tipo == true)
                    {
                        arreglos[contador] += horariosP2.Replace("##DIA##", variable.Dia).Replace("##HORAINICIO##", variable.Horainicio).Replace("##HORAFIN##", variable.Horafin).Replace("##HORAINICIO2##", variable.Horainicio2).Replace("##HORAFIN2##", variable.Horafin2);
                    }
                    else
                    {
                        arreglos[contador] += horariosP1.Replace("##DIA##", variable.Dia).Replace("##HORAINICIO##", variable.Horainicio).Replace("##HORAFIN##", variable.Horafin);
                    }
                }
                if (presencial == true)
                    strHTML += datosCiudad.Replace("##CIUDAD##", item.Ciudad).Replace("##FRECUENCIA##", frecuencia).Replace("##HORARIOS##", arreglos[contador]);
                else
                    strHTML += datosSinCiudad.Replace("##HORARIOS##", arreglos[contador]);
                contador++;
            }
            return strHTML;
        }

        /// <summary>
        /// Obtiene el id del dia
        /// </summary>
        /// <param name="dia"></param>
        /// <returns>id</returns>
        private int GetIdDia(string dia)
        {
            switch (dia)
            {
                case "Lunes":
                    return 1;
                case "Martes":
                    return 2;
                case "Miércoles":
                    return 3;
                case "Jueves":
                    return 4;
                case "Viernes":
                    return 5;
                case "Sábado":
                    return 6;
                case "Domingo":
                    return 7;
                default:
                    return 0;
            }
        }



        /// Autor: Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas V2
        /// </summary>
        /// <param name="lista"> Objeto tipo MontoProgramaAgrupadoDTO </param>	
        /// <returns>Lista Resumen Programa: List<ResumenProgramaV2DTO></returns> 
        private List<ResumenProgramaV2DTO> ObtenerResumenProgramaHTML(List<MontoProgramaAgrupadoDTO> lista)
		{
			try
			{
				List<ResumenProgramaV2DTO> listaNueva = new List<ResumenProgramaV2DTO>();
				foreach (var item in lista)
				{
					ResumenProgramaV2DTO obj = new ResumenProgramaV2DTO();
					obj.IdArea = item.IdArea;
					obj.IdSubArea = item.IdSubArea;
					obj.NombrePrograma = item.NombrePrograma;
					obj.Duracion = item.Duracion;
					var inversion = "";
					foreach (var inv in item.MontoDetalle)
					{
						inversion += "<strong>" + inv.Version + "</strong>";
						foreach (var det in inv.VersionDetalle)
						{
							inversion += "<ul>";
							inversion += "<li><strong>" + det.TipoPago + ": </strong>";
							if (det.TipoPago.Equals("Contado"))
							{
								inversion += det.SimboloMoneda + " " + det.Matricula + ".";
							}
							if (det.TipoPago.Equals("Crédito"))
							{
								if (det.NroCuotas != null && det.Cuotas != null)
								{
									inversion += "1 Cuota inicial de " + det.SimboloMoneda + " " + det.Matricula + " y " + det.NroCuotas + " cuotas mensuales desde " + det.SimboloMoneda + " " + det.Cuotas + ".";
								}
							}
							inversion += "</li>";
							inversion += "</ul>";
						}
					}
					obj.Inversion = inversion;
					var certificacion = "";
					if (item.SeccionCertificadoV2 != null)
					{
						foreach (var contenido in item.SeccionCertificadoV2.DetalleSeccion)
						{
							certificacion += "<h5><strong><b>" + contenido.Titulo + "</b></strong></h5>";
							certificacion += "<p>" + contenido.Cabecera + "</p>";
							certificacion += "<ul type='disc'>";
							foreach (var contenidoSeccion in contenido.DetalleContenido)
							{
								certificacion += "<li>" + contenidoSeccion + "</li>";
							}
							certificacion += "</ul>";
							certificacion += "<p>" + contenido.PiePagina + "</p>";
						}
					}
					else
					{
						if(item.SeccionCertificadoV1 != null)
						{
							certificacion += item.SeccionCertificadoV1.Contenido;
						}
						
					}
					obj.Certificacion = certificacion;
					listaNueva.Add(obj);
				}
				return listaNueva;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public class PreguntaFrecuente
        {
            public int IdPrograma { get; set; }
            public int Id { get; set; }
            public string Pregunta { get; set; }
            public string Respuesta { get; set; }
            public int IdSeccion { get; set; }
            public string Nombre { get; set; }
        }

        public class PreguntaFrecuenteSecciones
        {
            public int IdPrograma { get; set; }
            public int IdSeccion { get; set; }
            public string Nombre { get; set; }
            public List<PreguntaFrecuenteRespuestas> Contenido { get; set; }
        }

        public class PreguntaFrecuenteRespuestas
        {
            public string Pregunta { get; set; }
            public string Respuesta { get; set; }
        }

        public class ContenidoPreguntas
        {
            public string Titulo { get; set; }
            public List<PreguntaFrecuente> contenidoPregunta { get; set; }
        }
    }
}
