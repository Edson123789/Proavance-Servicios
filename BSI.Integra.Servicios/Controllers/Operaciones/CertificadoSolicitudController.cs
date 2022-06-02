using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Mandrill.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/CertificadoSolicitud")]
    public class CertificadoSolicitudController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CertificadoSolicitudController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Obtener([FromBody] FiltroCertificadoEnvioGrillaDTO Obj)
        {
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);

                //var rpta = _repCertificadoDetalle.ObtenerTodo();
                return Ok(_repCertificadoDetalle.ObtenerTodo(Obj.paginador, Obj.filter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltro()
        {
            try
            {
                CertificadoTipoRepositorio _repCertificadoTipo = new CertificadoTipoRepositorio(_integraDBContext);
                CertificadoTipoProgramaRepositorio _repRertificadoTipoPrograma = new CertificadoTipoProgramaRepositorio(_integraDBContext);
                var filtros = new
                {
                    TipoCertificado = _repCertificadoTipo.ObtenerFiltro(),
                    TipoPrograma = _repRertificadoTipoPrograma.ObtenerFiltro()
                };

                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] CertificadoDetalleDTO CertificadoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
                CertificadoDetalleBO certificadoDetalleBO = new CertificadoDetalleBO()
                {
                    IdCertificadoBrochure = CertificadoDetalle.IdCertificadoBrochure,
                    IdCertificadoSolicitud = CertificadoDetalle.IdCertificadoSolicitud,
                    IdCertificadoTipo = CertificadoDetalle.IdCertificadoTipo,
                    EsDiploma = CertificadoDetalle.EsDiploma,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = CertificadoDetalle.NombreUsuario,
                    UsuarioModificacion = CertificadoDetalle.NombreUsuario
                };
                if (!certificadoDetalleBO.HasErrors)
                {
                    _repCertificadoDetalle.Insert(certificadoDetalleBO);
                }
                else
                {
                    return BadRequest(certificadoDetalleBO.ActualesErrores);
                }
                return Ok(certificadoDetalleBO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] CertificadoDetalleDTO CertificadoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);

                var certificadoDetalleBO = _repCertificadoDetalle.FirstById(CertificadoDetalle.Id);
                
                certificadoDetalleBO.IdCertificadoTipo = CertificadoDetalle.IdCertificadoTipo;
                certificadoDetalleBO.IdCertificadoTipoPrograma = CertificadoDetalle.IdCertificadoTipoPrograma;
                certificadoDetalleBO.CodigoCertificado = CertificadoDetalle.CodigoCertificado;
                certificadoDetalleBO.Nota = CertificadoDetalle.Nota;
                certificadoDetalleBO.EscalaCalificacion = CertificadoDetalle.EscalaCalificacion;
                certificadoDetalleBO.TamanioFuenteNombreAlumno = CertificadoDetalle.TamanioFuenteNombreAlumno;
                certificadoDetalleBO.TamanioFuenteNombrePrograma = CertificadoDetalle.TamanioFuenteNombrePrograma;
                certificadoDetalleBO.DireccionEntrega = CertificadoDetalle.DireccionEntrega;
                certificadoDetalleBO.EsDiploma = CertificadoDetalle.EsDiploma;
                certificadoDetalleBO.EsAsistenciaPartner = CertificadoDetalle.EsAsistenciaPartner;
                certificadoDetalleBO.AplicaPartner = CertificadoDetalle.AplicaPartner;
                certificadoDetalleBO.FechaModificacion = DateTime.Now;
                certificadoDetalleBO.UsuarioModificacion = CertificadoDetalle.NombreUsuario;

                _repCertificadoDetalle.Update(certificadoDetalleBO);


                return Ok(certificadoDetalleBO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]/{IdCertificadoDetalle}/{EsImpresion}/{IdCertificadoBrochure}")]
        [HttpGet]
        public ActionResult VisualizarFrontal(int IdCertificadoDetalle, bool EsImpresion,int IdCertificadoBrochure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
                CertificadoDetalleBO certificadoDetalleBO = new CertificadoDetalleBO(_integraDBContext);
                //certificadoDetalleBO = _repCertificadoDetalle.FirstById(IdCertificadoDetalle);
                var certificado = certificadoDetalleBO.CalcularCertificadoFrontal(IdCertificadoDetalle, EsImpresion , IdCertificadoBrochure);

                return Ok(certificado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]/{IdCertificadoDetalle}/{IdCentroCosto}")]
        [HttpGet]
        public ActionResult ObtenerEnvioFisicoDigital(int IdCertificadoDetalle , int IdCentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoEnvioLogRepositorio _repCertificadoEnvioLog = new CertificadoEnvioLogRepositorio(_integraDBContext);
                CertificadoEnvioRepositorio _repCertificadoEnvio = new CertificadoEnvioRepositorio(_integraDBContext);
                CentroCostoCertificadoRepositorio _repCentroCostoCertificado = new CentroCostoCertificadoRepositorio(_integraDBContext);

                var certificadoEnvio = new
                {
                    brochure = _repCentroCostoCertificado.ObtenerBrochurePorCentroCosto(IdCentroCosto),
                    EnvioFisico = _repCertificadoEnvio.ObtenerEnvioFisico(IdCertificadoDetalle),
                    EnvioDigital = _repCertificadoEnvioLog.ObtenerEnvioDigital(IdCertificadoDetalle),
                };

                return Ok(certificadoEnvio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{CodigoAlumno}")]
        [HttpGet]
        public ActionResult ObtenerAlumnosSinSolicitud(string CodigoAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoEnvioLogRepositorio _repCertificadoEnvioLog = new CertificadoEnvioLogRepositorio(_integraDBContext);
                CertificadoEnvioRepositorio _repCertificadoEnvio = new CertificadoEnvioRepositorio(_integraDBContext);
                CertificadoSolicitudBO certificadoSolicitudBO = new CertificadoSolicitudBO(_integraDBContext);

                var certificadoEnvio = certificadoSolicitudBO.ObtenerAlumnosSinSolicitud(CodigoAlumno);

                return Ok(certificadoEnvio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]/{IdCentroCosto}")]
        [HttpGet]
        public ActionResult ObtenerAlumnosSinSolicitudCC(int IdCentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoEnvioLogRepositorio _repCertificadoEnvioLog = new CertificadoEnvioLogRepositorio(_integraDBContext);
                CertificadoEnvioRepositorio _repCertificadoEnvio = new CertificadoEnvioRepositorio(_integraDBContext);
                CertificadoSolicitudBO certificadoSolicitudBO = new CertificadoSolicitudBO(_integraDBContext);

                var certificadoEnvio = certificadoSolicitudBO.ObtenerAlumnosSinSolicitudCC(IdCentroCosto);

                return Ok(certificadoEnvio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarSolicitud([FromBody] List<InsertarSolicitudCertificadoDTO> ListaSolicitud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
                CertificadoSolicitudRepositorio _repCertificadoSolicitud = new CertificadoSolicitudRepositorio(_integraDBContext);

                CertificadoSolicitudBO certificadoSolicitud = new CertificadoSolicitudBO(_integraDBContext);
                CertificadoDetalleBO certificadoDetalle = new CertificadoDetalleBO(_integraDBContext);
                //return Ok(true);
                foreach (var item in ListaSolicitud)
                {
                    if (item.Solicitado == true)
                    {
                        var _solicitudExistente = _repCertificadoDetalle.FirstBy(w => w.IdMatriculaCabecera == item.Id && w.IdPespecifico == item.IdPespecifico);

                        _solicitudExistente.AplicaPartner = item.SolicitadoPartner;
                        _solicitudExistente.FechaModificacion = DateTime.Now;
                        _solicitudExistente.UsuarioModificacion = item.NombreUsuario;

                        _repCertificadoDetalle.Update(_solicitudExistente);
                    }
                    else
                    {
                        var _certificadoSolicitud = new CertificadoSolicitudBO();

                        _certificadoSolicitud.NumeroSolicitud = certificadoSolicitud.ObtenerNumeroSolictud(DateTime.Now);
                        _certificadoSolicitud.Estado = true;
                        _certificadoSolicitud.FechaCreacion = DateTime.Now;
                        _certificadoSolicitud.FechaModificacion = DateTime.Now;
                        _certificadoSolicitud.UsuarioCreacion = item.NombreUsuario;
                        _certificadoSolicitud.UsuarioModificacion = item.NombreUsuario;

                        _repCertificadoSolicitud.Insert(_certificadoSolicitud);

                        var _certificadoDetalle = new CertificadoDetalleBO();

                        _certificadoDetalle.IdCertificadoSolicitud = _certificadoSolicitud.Id;
                        _certificadoDetalle.IdCertificadoTipo = item.IdCertificadoTipo;
                        _certificadoDetalle.IdMatriculaCabecera = item.IdMatriculaCabecera;
                        _certificadoDetalle.CodigoCertificado = item.CodigoMatricula + item.FechaInicio.Value.ToString("MM") + item.FechaInicio.Value.ToString("yy")+"P"+"0";
                        _certificadoDetalle.FechaInicio = item.FechaInicio.Value;
                        _certificadoDetalle.FechaTermino = item.FechaFin.Value;
                        _certificadoDetalle.Nota = item.Nota;
                        _certificadoDetalle.EscalaCalificacion = item.EscalaCalificacion;
                        _certificadoDetalle.IdPespecifico = item.IdPespecifico;
                        _certificadoDetalle.DireccionEntrega = item.DireccionEnvio;
                        _certificadoDetalle.AplicaPartner = item.SolicitadoPartner;
                        _certificadoDetalle.IdCertificadoTipoPrograma = 1;
                        _certificadoDetalle.Estado = true;
                        _certificadoDetalle.FechaCreacion = DateTime.Now;
                        _certificadoDetalle.FechaModificacion = DateTime.Now;
                        _certificadoDetalle.UsuarioCreacion = item.NombreUsuario;
                        _certificadoDetalle.UsuarioModificacion = item.NombreUsuario;
                        _repCertificadoDetalle.Insert(_certificadoDetalle);
                    }
                
                }

                return Ok(certificadoSolicitud);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCertificados([FromBody]InsertarCertificadoDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
                var certificadoDetalleBO = _repCertificadoDetalle.FirstById(Obj.IdCertificadoDetalle);
                DateTime fechaActual = DateTime.Now;

                string fechaTexto = $@"{fechaActual.Year.ToString()}{fechaActual.Month.ToString()}{fechaActual.Day.ToString()}{fechaActual.Hour.ToString()}";

                string NombreArchivoFrontal = $"{Obj.IdCertificadoDetalle}-{Obj.CodigoMatricula}-{fechaTexto}-frontal.pdf";
                string NombreArchivoReverso = $"{Obj.IdCertificadoDetalle}-{Obj.CodigoMatricula}-{fechaTexto}-reverso.pdf";
                string NombreArchivoFrontalImpresion = $"{Obj.IdCertificadoDetalle}-{Obj.CodigoMatricula}-{fechaTexto}-frontal-impresion.pdf";
                string NombreArchivoPosteriorImpresion = $"{Obj.IdCertificadoDetalle}-{Obj.CodigoMatricula}-{fechaTexto}-reverso-impresion.pdf";

                //string rutaArchivo = @"E:\";
                string rutaArchivo = @"C:\Temp\RegistroAcademico\Presencial\Certificados\";
                string contenType = "application/pdf";

                System.IO.File.WriteAllBytes(rutaArchivo + NombreArchivoFrontal, Obj.PdfFrontal);
                System.IO.File.WriteAllBytes(rutaArchivo + NombreArchivoReverso, Obj.PdfReverso);
                System.IO.File.WriteAllBytes(rutaArchivo + NombreArchivoFrontalImpresion, Obj.PdfFrontalImpresion);
                System.IO.File.WriteAllBytes(rutaArchivo + NombreArchivoPosteriorImpresion, Obj.PdfReversoImpresion);

                var urlArchivoFrontal = _repCertificadoDetalle.guardarArchivoCertificado(Obj.PdfFrontal, contenType, NombreArchivoFrontal);
                var urlArchivoReverso = _repCertificadoDetalle.guardarArchivoCertificado(Obj.PdfFrontal, contenType, NombreArchivoReverso);
                var urlArchivoFrontalImpresion = _repCertificadoDetalle.guardarArchivoCertificado(Obj.PdfFrontal, contenType, NombreArchivoFrontalImpresion);
                var urlArchivoReversoImpresion = _repCertificadoDetalle.guardarArchivoCertificado(Obj.PdfFrontal, contenType, NombreArchivoPosteriorImpresion);

                certificadoDetalleBO.FechaEmision = fechaActual;
                certificadoDetalleBO.RutaArchivo = rutaArchivo;
                certificadoDetalleBO.NombreArchivoFrontal = NombreArchivoFrontal;
                certificadoDetalleBO.NombreArchivoFrontalImpresion = NombreArchivoFrontalImpresion;
                certificadoDetalleBO.NombreArchivoReverso = NombreArchivoReverso;
                certificadoDetalleBO.NombreArchivoReversoImpresion = NombreArchivoPosteriorImpresion;
                certificadoDetalleBO.ContentType = contenType;
                certificadoDetalleBO.IdCertificadoBrochure = Obj.IdCertificadoBrochure;
                certificadoDetalleBO.IdUrlBlockStorage = 1;
                certificadoDetalleBO.FechaModificacion = DateTime.Now;
                certificadoDetalleBO.UsuarioModificacion = Obj.Usuario;

                _repCertificadoDetalle.Update(certificadoDetalleBO);

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarAdjunto(InsertarCertificadoDTO Obj, [FromForm] IList<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
                CertificadoDetallePartnerRepositorio _repCertificadoDetallePartner = new CertificadoDetallePartnerRepositorio(_integraDBContext);
                var certificadoDetalleBO = _repCertificadoDetalle.FirstById(Obj.IdCertificadoDetalle);
                CertificadoDetallePartnerBO certificadoDetallePartnerBO = new CertificadoDetallePartnerBO();
                DateTime fechaActual = DateTime.Now;
                string NombreArchivo = "";
                string ContentType = "";
                byte[] archivo ;
                string Url = "";

                string fechaTexto = $@"{fechaActual.Year.ToString()}{fechaActual.Month.ToString()}{fechaActual.Day.ToString()}{fechaActual.Hour.ToString()}";
                string NombreArchivoFrontal = $"{Obj.IdCertificadoDetalle}-{Obj.CodigoMatricula}-{fechaTexto}.pdf";

                
                foreach (var file in Files)
                {
                    ContentType = file.ContentType;
                    NombreArchivo = NombreArchivoFrontal;
                    Obj.PdfFrontal = file.ConvertToByte();
                    Url = _repCertificadoDetalle.guardarArchivoCertificado(file.ConvertToByte(), file.ContentType, NombreArchivoFrontal);
                }

                

                
                //string NombreArchivoReverso = $"{Obj.IdCertificadoDetalle}-{Obj.CodigoMatricula}-{fechaTexto}-reverso.pdf";
                //string NombreArchivoFrontalImpresion = $"{Obj.IdCertificadoDetalle}-{Obj.CodigoMatricula}-{fechaTexto}-frontal-impresion.pdf";
                //string NombreArchivoPosteriorImpresion = $"{Obj.IdCertificadoDetalle}-{Obj.CodigoMatricula}-{fechaTexto}-reverso-impresion.pdf";
                
                //string rutaArchivo = @"E:\";
                //string rutaArchivo = @"C:\Temp\RegistroAcademico\Presencial\Certificados\";
                //string contenType = "application/pdf";

                //System.IO.File.WriteAllBytes(rutaArchivo + NombreArchivoFrontal, Obj.PdfFrontal);
                //System.IO.File.WriteAllBytes(rutaArchivo + NombreArchivoReverso, Obj.PdfReverso);
                //System.IO.File.WriteAllBytes(rutaArchivo + NombreArchivoFrontalImpresion, Obj.PdfFrontalImpresion);
                //System.IO.File.WriteAllBytes(rutaArchivo + NombreArchivoPosteriorImpresion, Obj.PdfReversoImpresion);

                //var urlArchivoFrontal = _repCertificadoDetalle.guardarArchivoCertificado(archivo, contenType, NombreArchivoFrontal);
                //var urlArchivoReverso = _repCertificadoDetalle.guardarArchivoCertificado(Obj.PdfFrontal, contenType, NombreArchivoReverso);
                //var urlArchivoFrontalImpresion = _repCertificadoDetalle.guardarArchivoCertificado(Obj.PdfFrontal, contenType, NombreArchivoFrontalImpresion);
                //var urlArchivoReversoImpresion = _repCertificadoDetalle.guardarArchivoCertificado(Obj.PdfFrontal, contenType, NombreArchivoPosteriorImpresion);

                certificadoDetalleBO.FechaEmision = fechaActual;
                //certificadoDetalleBO.RutaArchivo = rutaArchivo;
                //certificadoDetalleBO.NombreArchivoFrontal = NombreArchivoFrontal;
                //certificadoDetalleBO.NombreArchivoFrontalImpresion = NombreArchivoFrontal;
                //certificadoDetalleBO.NombreArchivoReverso = NombreArchivoFrontal;
                //certificadoDetalleBO.NombreArchivoReversoImpresion = NombreArchivoFrontal;
                //certificadoDetalleBO.ContentType = contenType;
                if (Obj.IdCertificadoBrochure != 0 && Obj.IdCertificadoBrochure != null)
                {
                    certificadoDetalleBO.IdCertificadoBrochure = Obj.IdCertificadoBrochure;
                }
                
                certificadoDetalleBO.IdUrlBlockStorage = 1;
                certificadoDetalleBO.FechaModificacion = fechaActual;
                certificadoDetalleBO.UsuarioModificacion = Obj.Usuario;

                _repCertificadoDetalle.Update(certificadoDetalleBO);

                certificadoDetallePartnerBO.IdCertificadoDetalle = certificadoDetalleBO.Id;
                certificadoDetallePartnerBO.IdUrlBlockStorage = 1;
                certificadoDetallePartnerBO.ContentType = NombreArchivo;
                certificadoDetallePartnerBO.Estado = true;
                certificadoDetallePartnerBO.FechaCreacion = fechaActual;
                certificadoDetallePartnerBO.FechaModificacion = fechaActual;
                certificadoDetallePartnerBO.UsuarioCreacion = Obj.Usuario;
                certificadoDetallePartnerBO.UsuarioModificacion = Obj.Usuario;

                _repCertificadoDetallePartner.Insert(certificadoDetallePartnerBO);
                return Ok(Url);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        
        }

        [Route("[Action]/{IdCertificadoDetalle}")]
        [HttpGet]
        public ActionResult DescargarFrontal(int IdCertificadoDetalle)
        {
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio();

                var certificado_detalle = _repCertificadoDetalle.FirstById(IdCertificadoDetalle);
                byte[] pdf ;
                if (System.IO.File.Exists(certificado_detalle.RutaArchivo + certificado_detalle.NombreArchivoFrontal))
                {
                    pdf = System.IO.File.ReadAllBytes(certificado_detalle.RutaArchivo + certificado_detalle.NombreArchivoFrontal);
                    //return File(pdf, certificado_detalle.ContentType, certificado_detalle.NombreArchivoFrontal);
                    return Ok(new { pdf, NombreArchivo = certificado_detalle.NombreArchivoFrontal });
                }
                return Ok(null);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [Route("[Action]/{IdCertificadoDetalle}")]
        [HttpGet]
        public ActionResult DescargarReverso(int IdCertificadoDetalle)
        {
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio();

                var certificado_detalle = _repCertificadoDetalle.FirstById(IdCertificadoDetalle);
                byte[] pdf ;
                if (System.IO.File.Exists(certificado_detalle.RutaArchivo + certificado_detalle.NombreArchivoReverso))
                {
                    pdf = System.IO.File.ReadAllBytes(certificado_detalle.RutaArchivo + certificado_detalle.NombreArchivoReverso);
                    //return File(pdf, certificado_detalle.ContentType, certificado_detalle.NombreArchivoFrontal);
                    return Ok(new { pdf, NombreArchivo = certificado_detalle.NombreArchivoReverso });
                }
                return Ok(null);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [Route("[Action]/{IdCertificadoDetalle}")]
        [HttpGet]
        public ActionResult DescargarFrontalImpresion(int IdCertificadoDetalle)
        {
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio();

                var certificado_detalle = _repCertificadoDetalle.FirstById(IdCertificadoDetalle);
                byte[] pdf ;
                if (System.IO.File.Exists(certificado_detalle.RutaArchivo + certificado_detalle.NombreArchivoFrontalImpresion))
                {
                    pdf = System.IO.File.ReadAllBytes(certificado_detalle.RutaArchivo + certificado_detalle.NombreArchivoFrontalImpresion);
                    //return File(pdf, certificado_detalle.ContentType, certificado_detalle.NombreArchivoFrontal);
                    return Ok(new { pdf,NombreArchivo= certificado_detalle.NombreArchivoFrontalImpresion });
                }
                return Ok(null);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [Route("[Action]/{IdCertificadoDetalle}")]
        [HttpGet]
        public ActionResult DescargarReversoImpresion(int IdCertificadoDetalle)
        {
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio();

                var certificado_detalle = _repCertificadoDetalle.FirstById(IdCertificadoDetalle);
                byte[] pdf ;
                if (System.IO.File.Exists(certificado_detalle.RutaArchivo + certificado_detalle.NombreArchivoReversoImpresion))
                {
                    pdf = System.IO.File.ReadAllBytes(certificado_detalle.RutaArchivo + certificado_detalle.NombreArchivoReversoImpresion);
                    //return File(pdf, certificado_detalle.ContentType, certificado_detalle.NombreArchivoFrontal);
                    return Ok(new { pdf, NombreArchivo = certificado_detalle.NombreArchivoReversoImpresion });
                }
                return Ok(null);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [Route("[Action]/{IdCertificadoDetalle}/{Usuario}")]
        [HttpGet]
        public ActionResult RevertirEmision(int IdCertificadoDetalle,string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
                CertificadoReversionEmisionRepositorio _repCertificadoReversionEmision = new CertificadoReversionEmisionRepositorio(_integraDBContext);

                CertificadoReversionEmisionBO certificadoReversionEmisionBO = new CertificadoReversionEmisionBO();
                var certificadoDetalleBO = _repCertificadoDetalle.FirstById(IdCertificadoDetalle);

                certificadoDetalleBO.FechaEmision = null;
                certificadoDetalleBO.FechaModificacion = DateTime.Now;
                certificadoDetalleBO.UsuarioModificacion = Usuario;
                
                if (!certificadoDetalleBO.HasErrors)
                {
                    _repCertificadoDetalle.Update(certificadoDetalleBO);
                }
                else
                {
                    return BadRequest(certificadoDetalleBO.ActualesErrores);
                }

                certificadoReversionEmisionBO.IdCertificadoDetalle = IdCertificadoDetalle;
                certificadoReversionEmisionBO.Estado = true;
                certificadoReversionEmisionBO.FechaCreacion = DateTime.Now;                
                certificadoReversionEmisionBO.FechaModificacion = DateTime.Now;                
                certificadoReversionEmisionBO.UsuarioCreacion = Usuario;
                certificadoReversionEmisionBO.UsuarioModificacion = Usuario;
                if (!certificadoReversionEmisionBO.HasErrors)
                {
                    _repCertificadoReversionEmision.Update(certificadoReversionEmisionBO);
                }
                else
                {
                    return BadRequest(certificadoReversionEmisionBO.ActualesErrores);
                }

                return Ok(certificadoDetalleBO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        
        }
        [Route("[Action]/{IdCertificadoDetalle}/{Usuario}")]
        [HttpGet]
        public ActionResult EnviarCertificadoDigital(int IdCertificadoDetalle,string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
                CertificadoEnvioLogRepositorio _repCertificadoEnvioLog = new CertificadoEnvioLogRepositorio(_integraDBContext);
                EnvioMasivoPlantillaBO envioMasivoPlantillaBO = new EnvioMasivoPlantillaBO();
                CertificadoEnvioLogBO envioDigital = new CertificadoEnvioLogBO();
                string contenType = "application/pdf";

                var certificadoDetalleBO = _repCertificadoDetalle.FirstById(IdCertificadoDetalle);

                var DatosAlumno =_repCertificadoDetalle.ObtenerDatosAlumnoEnvio(IdCertificadoDetalle);

                var cuerpo = envioDigital.GenerarMensajeEnvioDigital(DatosAlumno);

                var listaArchivosAdjuntos = new List<EmailAttachment>();

                if (certificadoDetalleBO.AplicaPartner)
                {
                    var archivoAdjunto = envioDigital.GenerarArchivoAdjunto(DatosAlumno.UrlDocumento + certificadoDetalleBO.NombreArchivoFrontal, " ", contenType);

                    listaArchivosAdjuntos.Add(archivoAdjunto);
                }
                else
                {
                    var archivoAdjunto = envioDigital.GenerarArchivoAdjunto(DatosAlumno.UrlDocumento + certificadoDetalleBO.NombreArchivoFrontal, " ", contenType);

                    listaArchivosAdjuntos.Add(archivoAdjunto);

                    var archivoAdjunto2 = envioDigital.GenerarArchivoAdjunto(DatosAlumno.UrlDocumento + certificadoDetalleBO.NombreArchivoReverso, " ", contenType);

                    listaArchivosAdjuntos.Add(archivoAdjunto2);
                }          

                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = DatosAlumno.Email,
                    Recipient = string.Join(",", "fvaldez@bsginstitute.com"),
                    Subject = $"CERTIFICADO DIGITAL - {DatosAlumno.NombreCentroCosto} - {DatosAlumno.CodigoMatricula}",
                    Message = cuerpo,
                    Cc = "",
                    //Bcc = string.Join(",", "fvaldez@bsginstitute.com"),
                    Bcc = "",
                    AttachedFiles = listaArchivosAdjuntos
                };
                var mailServie = new TMK_MailServiceImpl();

                mailServie.SetData(mailDataPersonalizado);
                var listaIdsMailChimp = mailServie.SendMessageTask();

                certificadoDetalleBO.FechaUltimoEnvioAlumno = DateTime.Now;
                certificadoDetalleBO.UsuarioModificacion = Usuario;
                certificadoDetalleBO.FechaModificacion = DateTime.Now;

                envioDigital.IdCertificadoDetalle = IdCertificadoDetalle;
                envioDigital.FechaEnvio = DateTime.Now;
                envioDigital.SoloDigital = true;

                envioDigital.Estado = true;
                envioDigital.FechaCreacion = DateTime.Now;
                envioDigital.FechaModificacion = DateTime.Now;
                envioDigital.UsuarioCreacion = Usuario;
                envioDigital.UsuarioModificacion = Usuario;

                _repCertificadoDetalle.Update(certificadoDetalleBO);
                _repCertificadoEnvioLog.Insert(envioDigital);

                return Ok(certificadoDetalleBO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        
        }
    }
}
