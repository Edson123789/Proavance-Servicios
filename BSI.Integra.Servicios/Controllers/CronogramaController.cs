using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MatriculaCabeceraRepositorio = BSI.Integra.Aplicacion.Transversal.Repositorio.MatriculaCabeceraRepositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{

    [Route("api/cronograma")]
    public class CronogramaController : Controller
    {
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// retorna el nombre completo del alumno y su id , por el valor del parametro
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnoPorValor([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                    var alumnosTemp = _repAlumno.ObtenerTodoFiltroAutoComplete(Valor["filtro"]);
                    foreach (var item in alumnosTemp)
                    {
                        item.NombreCompleto = string.Concat(item.NombreCompleto, " (", item.Id, ") ");
                    }
                    return Ok(alumnosTemp);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene programas especificos por el nombre del centro de costo
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerProgramasEspecificosPorNombreCentroCosto([FromBody] Dictionary<string, string> Filtro)
        {

            try
            {
                if (Filtro != null && Filtro.Count > 0)
                {
                    PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                    return Ok(_repPEspecifico.GetBy(x => x.Nombre.Contains(Filtro["Nombre"]), x => new { x.Id, x.Nombre }).ToList().OrderBy(x => x.Nombre));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene todos los estados de matricula 
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTodoEstadoMatricula()
        {
            try
            {
                EstadoPagoMatriculaRepositorio _repEstadoPagoMatricula = new EstadoPagoMatriculaRepositorio();
                return Ok(_repEstadoPagoMatricula.GetBy(x => x.Estado == true, x => new { x.Id, EstadoMatricula = x.Nombre }).ToList());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene los datos del asesor por el apellido
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerAsesorPorApellidos([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    PersonalRepositorio _repPersonal = new PersonalRepositorio();
                    var personal = _repPersonal.GetBy(x => x.Rol == "Ventas" && x.Apellidos.Contains(Valor["filtro"]), x => new { x.Id, x.Nombres, x.Apellidos, x.Email, NombreCompleto = x.Nombres + " " + x.Apellidos });
                    return Ok(personal);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// onbtiene los datos del coordinador por el apellido
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCoordinadorPorApellidos([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    PersonalRepositorio _repPersonal = new PersonalRepositorio();
                    if (Valor["filtro"] == "ninguno")
                    {
                        return Ok(_repPersonal.GetBy(x => x.Id == 126, x => new { x.Id, NombreCompleto = x.Nombres + " " + x.Apellidos }));
                    }
                    else
                    {
                        return Ok(_repPersonal.GetBy(x => x.TipoPersonal.Contains("coor") && x.Apellidos.Contains(Valor["filtro"]), x => new { x.Id, NombreCompleto = x.Nombres + " " + x.Apellidos }));
                    }
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 12/01/2021
        /// Versión: 1.0
        /// <summary>
        /// onbtiene los datos de tasas academicas con sus detalles y precios
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerDetalleTasasAcademicas([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    OrigenRepositorio _repTasasAcademicas = new OrigenRepositorio();
                    return Ok(_repTasasAcademicas.ObtenerTarifariosDetallesMonto(Valor["filtro"]));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 12/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Agrega las tasas academicas con los datos consignados desde la vista.
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult AgregarTasasAcademicas([FromBody] TasasAcademicasDetalleDTO Json)
        {
            try
            {
                if (Json != null)
                {
                    OrigenRepositorio _repTasasAcademicas = new OrigenRepositorio();
                    return Ok(_repTasasAcademicas.AgregarTasasAcademicasProcedimiento(
                        Json.CodigoMatricula, 
                        Json.IdConcepto, 
                        Json.Monto,
                        Json.Moneda,
                        Json.Usuario,
                        Json.FechaPago)
                        );
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene programas especificos por el nombre del centro de costo
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPEspecificoPorCentroCosto([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                    return Ok(_repPespecifico.ObtenerPEspecificoPorCentroCosto(Valor["filtro"]));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        ///obtiene el codigo de matricula por codigo de alumno
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoMatriculaPEspecificoPorAlumno([FromBody] Dictionary<string, int> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    MatriculaCabeceraRepositorio _repCabeceraRepositorio = new MatriculaCabeceraRepositorio();
                    return Ok(_repCabeceraRepositorio.ObtenerCodigoMatriculaPEspecificoPorAlumno(Valor["alumno"]));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene los detos de matricula en base al parametro recibido
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoMatricula([FromBody] Dictionary<string, string> Valor) //CodigoMatricula
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                    //(_repMatriculaCabecera.GetBy(x => x.CodigoMatricula.Contains(Valor["filtro"]), x => new { Id = x.CodigoMatricula }));

                    return Ok(_repMatriculaCabecera.ObtenerCodigoMatricula(Valor["filtro"]));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene los detos de matricula en base al parametro recibido
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoMatriculaInHouse([FromBody] Dictionary<string, string> Valor) //CodigoMatricula
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                    //(_repMatriculaCabecera.GetBy(x => x.CodigoMatricula.Contains(Valor["filtro"]), x => new { Id = x.CodigoMatricula }));

                    return Ok(_repMatriculaCabecera.ObtenerIdMatriculaPorCodigo(Valor["filtro"]));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene todas las formas de pago
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFormasPago()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormaPagoRepositorio _repFormaPagoRepositorio = new FormaPagoRepositorio();
                return Ok(_repFormaPagoRepositorio.GetBy(x => x.Estado == true, x => new { x.Id, x.Descripcion }).OrderBy(x => x.Descripcion).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene las cuentas de pago
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCuentasCorrientes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CuentaCorrienteRepositorio _repCuentaCorriente = new CuentaCorrienteRepositorio();
                var listadoCuentasCorrientes = _repCuentaCorriente.ObtenerCuentasCorrientes();
                var listadoCuentasCorrientesFinal = new List<DatosCuentaCorrienteDTO>();
                foreach (var item in listadoCuentasCorrientes)
                {
                    var tempCuentaCorriente = new DatosCuentaCorrienteDTO
                    {
                        IdCta = item.IdCta,
                        Id = string.Concat(item.NumeroCuenta, "-", (item.Ciudad == "LIMA Y CALLAO" ? "LIMA" : item.Ciudad)),
                        Cuenta = String.Concat(item.NombreEntidadFinanciera, " ", item.NumeroCuenta)
                    };
                    listadoCuentasCorrientesFinal.Add(tempCuentaCorriente);
                }
                return Ok(listadoCuentasCorrientesFinal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los datos de la matricula en base a su codigo
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]/{CodigoMatricula}")]
        [HttpPost]
        public ActionResult ObtenerDatosMatriculaPorCodigoMatricula(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                //     var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var matriculaCabeceraTemp = _repMatriculaCabecera.ObtenerIdMatriculaPorCodigo(CodigoMatricula);
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();

                var resultado = _repMatriculaCabecera.ObtenerDatosMatriculaPorCodigoMatriculaVersion(CodigoMatricula, versionAprobada.Version);
                var beneficiosmatricula = _repMatriculaCabecera.ObtenerBeneficiosMatriculaCabecera(CodigoMatricula);

                return Ok(new { resultado, beneficiosmatricula });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene los datos del alumno en base al codigo de matricula
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]/{CodigoMatricula}")]
        [HttpPost]
        public ActionResult ObtenerAlumnoProgramaEspecifico(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                // var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var matriculaCabeceraTemp = _repMatriculaCabecera.ObtenerIdMatriculaPorCodigo(CodigoMatricula);
                if (matriculaCabeceraTemp.EstadoMatricula.Equals("matriculado") || matriculaCabeceraTemp.EstadoMatricula.Equals("pormatricular"))
                {
                    return Ok(_repMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(matriculaCabeceraTemp.Id));
                }
                else
                {
                    List<AlumnoProgramaEspecificoDTO> lista = new List<AlumnoProgramaEspecificoDTO>();
                    return Ok(lista);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene los beneficios de una matricula en base a su codigo
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerBeneficiosPorCodigoMatricula(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repBeneficiosCodigoMatricula = new MatriculaCabeceraRepositorio();
                return Ok(_repBeneficiosCodigoMatricula.ObtenerBeneficiosCodigoMatricula(CodigoMatricula));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene los datos del perdonal
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTodoPersonal()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repPersonalRepositorio = new PersonalRepositorio();
                return Ok(_repPersonalRepositorio.GetBy(x => x.Activo == true, x => new { x.Id, NombreCompleto = string.Concat(x.Apellidos, " ", x.Nombres), x.Rol }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los documentos de una matricula en base a su codigo y su tipo de modalidad
        /// </summary>
        /// <returns>Json/returns>
        /// 
        [Route("[action]/{CodigoMatricula}/{IdPEspecifico}")]
        [HttpPost]
        public ActionResult ObtenerDocumentosMatricula(string CodigoMatricula, int IdPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                ControlDocRepositorio _repControlDoc = new ControlDocRepositorio();
                var listaDocumentoAlumno = _repControlDoc.ObtenerDocumentosPorMatriculaCabecera(CodigoMatricula);
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                var modalidad = _repPEspecifico.GetBy(x => x.Id == IdPEspecifico, x => new { x.Tipo }).FirstOrDefault();
                CriterioDocRepositorio _repCriterioDoc = new CriterioDocRepositorio();
                List<CriterioDocDTO> listaDocumentos = new List<CriterioDocDTO>();
                if (modalidad.Tipo == "Presencial")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadPresencial == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                if (modalidad.Tipo == "Online Asincronica")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadAonline == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                if (modalidad.Tipo == "Online Sincronica")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadOnline == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                for (int i = 0; i < listaDocumentos.Count(); i++)
                {
                    listaDocumentos[i].Estado = 0;
                    for (int j = 0; j < listaDocumentoAlumno.Count(); j++)
                    {
                        if (listaDocumentos[i].IdCriterioDocs == listaDocumentoAlumno[j].IdCriterioDocs)
                        {
                            listaDocumentos[i].Estado = 1;
                            break;
                        }
                    }
                }
                return Ok(new { listaDocumentos, listaDocumentoAlumno });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza lod coucmentos de una matricula
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarEntregaControlDocs([FromBody] ListaControlDocumentosDTO Json)
        {
            try
            {
                ControlDocAlumnoRepositorio _repControlDocAlumnoRep = new ControlDocAlumnoRepositorio();
                ControlDocRepositorio _repControlDocRep = new ControlDocRepositorio();
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == Json.matricula, x => new { x.Id }).FirstOrDefault();

                for (int i = 0; i < Json.ListaDocumentos.Count(); i++)
                {
                    if (Json.ListaDocumentos[i].Ingresar)
                    {
                        var Lista = _repControlDocAlumnoRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).ToList();
                        if (Lista.Count() == 0)
                        {
                            ControlDocAlumnoBO documentoAlumno = new ControlDocAlumnoBO();
                            documentoAlumno.IdMatriculaCabecera = matriculaCabeceraTemp.Id;
                            documentoAlumno.IdCriterioCalificacion = 0;
                            documentoAlumno.FechaCreacion = DateTime.Now;
                            documentoAlumno.FechaModificacion = DateTime.Now;
                            documentoAlumno.UsuarioCreacion = Json.ListaDocumentos[i].usuario;
                            documentoAlumno.UsuarioModificacion = Json.ListaDocumentos[i].usuario;
                            documentoAlumno.Estado = true;
                            documentoAlumno.ComisionableEditable = "Ninguno";
                            documentoAlumno.MontoComisionable = 0;
                            documentoAlumno.PagadoComisionable = 0;
                            _repControlDocAlumnoRep.Insert(documentoAlumno);
                        }

                        var listaActualizarControlDocumentos = _repControlDocRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.IdCriterioDoc == Json.ListaDocumentos[i].IdCriterioDocs);

                        foreach (var item in listaActualizarControlDocumentos)
                        {
                            item.EstadoDocumento = true;
                            item.FechaModificacion = DateTime.Now;
                            item.UsuarioModificacion = Json.ListaDocumentos[i].usuario;
                            _repControlDocRep.Update(item);
                        }
                    }
                    else
                    {
                        var listaEliminarControlDocumentos = _repControlDocRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.IdCriterioDoc == Json.ListaDocumentos[i].IdCriterioDocs);
                        foreach (var item in listaEliminarControlDocumentos)
                        {
                            item.EstadoDocumento = false;
                            item.FechaModificacion = DateTime.Now;
                            item.UsuarioModificacion = Json.ListaDocumentos[i].usuario;
                            _repControlDocRep.Update(item);
                        }
                    };
                }

                return Ok(new { Message = "Se Actualizo Correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene las cuotas de pago en base al codigo de matricula
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCuotasCrepPorCodigoMatricula(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                return Ok(_repCronogramaPagoDetalleFinal.ObtenerCuotasCrepPorCodigoMatricula(matriculaCabeceraTemp.Id, versionAprobada.Version));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtieneel personal vcalidado en base al apellido
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPersonalAprobadoPorApellido([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    PersonalRepositorio _repPersonal = new PersonalRepositorio();
                    var tempAprobados = new int[] { 13, 24, 213, 10, 74 };
                    return Ok(_repPersonal.GetBy(x => x.Activo == true && x.Apellidos.Contains(Valor["filtro"]) && tempAprobados.Contains(x.Id), x => new { x.Id, NombreCompleto = string.Concat(x.Apellidos, " ", x.Nombres) }));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene todos los datos necesarios para un pago
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerDatosPago()
        {
            try
            {
                FormaPagoRepositorio _repFormaPago = new FormaPagoRepositorio();
                var listaFormaPago = _repFormaPago.GetBy(x => x.Estado == true, x => new { x.Id, x.Descripcion }).OrderBy(x => x.Descripcion);

                DocumentoPagoRepositorio _repDocumentoPago = new DocumentoPagoRepositorio();
                var listaDocumentoPago = _repDocumentoPago.GetBy(x => x.Estado == true, x => new { x.Id, Documento = x.Nombre }).OrderBy(x => x.Documento);

                CuentaCorrienteRepositorio _repCuentaCorriente = new CuentaCorrienteRepositorio();
                var listadoCuentasCorrientes = _repCuentaCorriente.ObtenerCuentasCorrientes();
                var listadoCuentasCorrientesFinal = new List<DatosCuentaCorrienteDTO>();

                foreach (var item in listadoCuentasCorrientes)
                {
                    var tempCuentaCorriente = new DatosCuentaCorrienteDTO
                    {
                        IdCta = item.IdCta,
                        Id = string.Concat(item.NumeroCuenta, "-", item.IdCiudad),
                        Cuenta = String.Concat(item.NombreEntidadFinanciera, " ", item.NumeroCuenta)
                    };
                    listadoCuentasCorrientesFinal.Add(tempCuentaCorriente);
                }
                return Ok(new { listaFormaPago, listaDocumentoPago, listadoCuentasCorrientesFinal });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene las ccuotas no pagadas en base a la version final del cronograma del codigo de matricula
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]/{CodigoMatricula}/{Version}")]
        [HttpGet]
        public ActionResult ObtenerCuotasNoPagadas(string CodigoMatricula, int Version)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                return Ok(_repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == Version && x.Cancelado == false, x => new { x.Id, NroCuotaSubCuota = string.Concat(x.NroCuota, " - ", x.NroSubCuota), x.Cuota }));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene el cronograma valido de una matricula en base a su codigo
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]/{CodigoMatricula}")]
        [HttpPost]
        public ActionResult ObtenerCronograma(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                //var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var matriculaCabeceraTemp = _repMatriculaCabecera.ObtenerIdMatriculaPorCodigo(CodigoMatricula);
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                CronogramaPagoDetalleOriginalRepositorio _repCronogramaPagoDetalleOriginal = new CronogramaPagoDetalleOriginalRepositorio();

                var cronogramaOriginal = _repCronogramaPagoDetalleOriginal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id, x => new { x.Id, x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.TotalPagar, x.Cuota, x.FechaVencimiento, x.Moneda });
                var cronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobada.Version, x => new { x.Id, x.Cancelado, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version, x.FechaDeposito }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota);

                var flagSinAprobar = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == false).Count() > 0 ? true : false;
                return Ok(new { cronogramaOriginal, cronogramaPagoDetalleFinal, flagSinAprobar });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene la lista de alumnos matriculados por pograma o por codigo de alumno o por ambos filtros
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]/{Idprograma}/{Idalumno}/{Tipo}")]
        [HttpPost]
        public ActionResult ObtenerListadoAlumnosMatricula(int Idprograma, int Idalumno, int Tipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var AlumnoMatricula = new { Tipo = Tipo, IdAlumno = Idalumno, IdProgramaEspecifico = Idprograma };
            try
            {
                MatriculaCabeceraRepositorio _repListadoAlumnosMatricula = new MatriculaCabeceraRepositorio();

                var listadoAlumnoMatricula = new List<AlumnoMatriculaDTO>();
                if (AlumnoMatricula.Tipo == 1)//por programa
                {
                    listadoAlumnoMatricula = (_repListadoAlumnosMatricula.ObtenerListadoAlumnosMatriculaCodigoPEspecifico(AlumnoMatricula.IdProgramaEspecifico));
                }
                else if (AlumnoMatricula.Tipo == 2)//por alumno
                {
                    listadoAlumnoMatricula = (_repListadoAlumnosMatricula.ObtenerListadoAlumnosMatriculaIdAlumno(AlumnoMatricula.IdAlumno));
                }
                else if (AlumnoMatricula.Tipo == 3)//por ambos
                {
                    listadoAlumnoMatricula = (_repListadoAlumnosMatricula.ObtenerListadoAlumnosMatricula());
                }
                return Ok(listadoAlumnoMatricula);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza los datos de una matricula
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarMatricula([FromBody] MatriculaActualizarDTO Json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                CronogramaPagoRepositorio _repCronogramaPago = new CronogramaPagoRepositorio();

                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == Json.Codigomatricula).FirstOrDefault();

                if (matriculaCabeceraTemp != null)
                {
                    matriculaCabeceraTemp.FechaModificacion = DateTime.Now;
                    matriculaCabeceraTemp.UsuarioModificacion = Json.usuario;

                    if (Json.Estado != null)
                    {
                        if (matriculaCabeceraTemp.EstadoMatricula == "pormatricular" && Json.Estado == "matriculado" && (matriculaCabeceraTemp.EmpresaPaga == "SI" || Json.EmpresaPaga == true))
                        {
                            matriculaCabeceraTemp.FechaMatricula = DateTime.Now;
                            matriculaCabeceraTemp.IdEstadoPagoMatricula = 2;
                        }
                        matriculaCabeceraTemp.EstadoMatricula = Json.Estado;
                    }

                    if (Json.Periodo != null && Json.Periodo != 0)
                        matriculaCabeceraTemp.IdPeriodo = Json.Periodo.Value;
                    if (Json.Programa != null && Json.Programa != 0)
                        matriculaCabeceraTemp.IdPespecifico = Json.Programa.Value;
                    if (Json.Asesor != null && Json.Asesor != 0)
                        matriculaCabeceraTemp.IdAsesor = Json.Asesor.Value;
                    if (Json.Coordinador != null && Json.Coordinador != 0)
                        matriculaCabeceraTemp.IdCoordinador = Json.Coordinador.Value;

                    var cronogramaPagosTemp = _repCronogramaPago.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).FirstOrDefault();
                    if (cronogramaPagosTemp != null)
                    {
                        cronogramaPagosTemp.FechaModificacion = DateTime.Now;
                        cronogramaPagosTemp.UsuarioModificacion = Json.usuario;
                        if (Json.Observaciones != null)
                            cronogramaPagosTemp.Observaciones = Json.Observaciones;
                    }

                    if (Json.EmpresaNombre != null)
                        matriculaCabeceraTemp.EmpresaNombre = Json.EmpresaNombre;

                    matriculaCabeceraTemp.EmpresaPaga = Json.EmpresaPaga == true ? "SI" : "NO";

                    //Actualizamos
                    _repMatriculaCabecera.Update(matriculaCabeceraTemp);
                    _repCronogramaPago.Update(cronogramaPagosTemp);

                    return Ok(new { Message = "Se Actualizo Correctamente" });
                }
                else
                {
                    return BadRequest("Error, Matricula no Existe");
                }



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda una pago , y modifica las tablas de cronograma a pagado
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarPago([FromBody] PagoCuotaCronogramaDTO Json)
        {

            try
            {
                Json.Fecha = Json.Fecha.AddHours(-5);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == Json.CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                var CronogramaActual = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobada.Version).OrderBy(w => w.NroCuota).ThenBy(w => w.NroSubCuota).ToList();

                CronogramaPagoDetalleFinalBO CronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalBO();
                CronogramaPagoDetalleFinal.GuardarPago(Json, CronogramaActual, Json.NroCuota, Json.NroSubCuota);
                return Ok(new { Message = "Se Realizo el pago correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la fecha de pago
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarFechaPago([FromBody] PagoActualizadoFechaDTO Json)
        {

            try
            {
                Json.FechaPago = Json.FechaPago != null ? Json.FechaPago.Value.AddHours(-5) : Json.FechaPago;
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                CronogramaPagoDetalleFinalBO CronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalBO();
                CronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.FirstById(Json.IdCuota);
                CronogramaPagoDetalleFinal.FechaPago = Json.FechaPago;
                CronogramaPagoDetalleFinal.FechaModificacion = DateTime.Now;
                CronogramaPagoDetalleFinal.UsuarioModificacion = Json.Usuario;
                _repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinal);

                return Ok(new { Message = "Se Actualizo correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        /// Tipo Función: GET
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la forma de pago
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]/{IdCuota}/{IdFormaPago}/{Usuario}")]
        [HttpGet]
        public ActionResult ActualizarFormaPago(int IdCuota, int? IdFormaPago, string Usuario)
        {

            try
            {
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                CronogramaPagoDetalleFinalBO CronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalBO();
                CronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.FirstById(IdCuota);
                CronogramaPagoDetalleFinal.IdFormaPago = IdFormaPago == 0 ? null : IdFormaPago;
                CronogramaPagoDetalleFinal.FechaModificacion = DateTime.Now;
                CronogramaPagoDetalleFinal.UsuarioModificacion = Usuario;
                _repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinal);

                return Ok(new { Message = "Se Actualizo correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la fecha de deposito
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarFechaDeposito([FromBody] PagoActualizadoFechaDepositoDTO Json)
        {

            try
            {
                Json.FechaDeposito = Json.FechaDeposito != null ? Json.FechaDeposito.Value.AddHours(-5) : Json.FechaDeposito;
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                CronogramaPagoDetalleFinalBO CronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalBO();
                CronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.FirstById(Json.IdCuota);
                CronogramaPagoDetalleFinal.FechaDeposito = Json.FechaDeposito;
                CronogramaPagoDetalleFinal.FechaModificacion = DateTime.Now;
                CronogramaPagoDetalleFinal.UsuarioModificacion = Json.Usuario;
                _repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinal);

                return Ok(new { Message = "Se Actualizo correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        /// Tipo Función: POST 
        /// Autor: Lisbeth, Miguel
        /// Fecha: 18/05/2021
        /// Versión: 2.0
        /// <summary>
        /// Inhabilita la matrocula y guarda todos los cambios del cronograma asi como sus detalles y también los log de este
        /// modificacion : se agrego el campo  UsuarioCoordinadorAcademico a la tabla de cronograma de la matricula
        /// </summary>
        /// <returns>Json</returns>
        [Route("[action]/{CodigoMatricula}/{Modoeliminacion}/{Usuario}")]
        [HttpPost]
        public ActionResult EliminarMatricula(string CodigoMatricula, int Modoeliminacion, string Usuario)
        {
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula).FirstOrDefault();
                ControlDocRepositorio _repControlDoc = new ControlDocRepositorio();
                ControlDocAlumnoRepositorio _repControlDocAlumno = new ControlDocAlumnoRepositorio();
                CronogramaPagoDetalleModRepositorio repCronogramaPagoDetalleMod = new CronogramaPagoDetalleModRepositorio();
                CronogramaPagoDetalleRepositorio _repCronogramaPagoDetalle = new CronogramaPagoDetalleRepositorio();
                CronogramaPagoRepositorio _repCronogramaPago = new CronogramaPagoRepositorio();
                MatriculaDetalleRepositorio _repMatriculaDetalle = new MatriculaDetalleRepositorio();
                CronogramaPagoDetalleOriginalRepositorio _repCronogramaPagoDetalleOriginal = new CronogramaPagoDetalleOriginalRepositorio();
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                PagoFinalRepositorio _repPagoFinal = new PagoFinalRepositorio();
                PagoRepositorio _repPago = new PagoRepositorio();
                CronogramaDetalleCambioRepositorio _repCronogramaDetalleCambio = new CronogramaDetalleCambioRepositorio();



                if (Modoeliminacion == 0)//con devolucion
                {
                    matriculaCabeceraTemp.EstadoMatricula = "condevolucion";
                    matriculaCabeceraTemp.FechaModificacion = DateTime.Now;
                    matriculaCabeceraTemp.UsuarioModificacion = Usuario;
                    matriculaCabeceraTemp.FechaRetiro = DateTime.Now;
                    _repMatriculaCabecera.Update(matriculaCabeceraTemp);

                    var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                    var nroCuota = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true && x.Version == versionAprobada.Version, x => new { x.NroCuota }).OrderByDescending(x => x.NroCuota).FirstOrDefault();
                    var monedaOriginal = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true && x.Version == versionAprobada.Version, x => new { x.Moneda }).FirstOrDefault();
                    var montoDevolver = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true && x.Version == versionAprobada.Version && x.Cancelado == true).Sum(x => x.Cuota);
                    var UsuarioCoordinadorAcademico = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true && x.Version == versionAprobada.Version, x => new { x.UsuarioCoordinadorAcademico }).FirstOrDefault().ToString();
                    montoDevolver = montoDevolver * -1;

                    CronogramaPagoDetalleFinalBO nuevaCuota = new CronogramaPagoDetalleFinalBO()
                    {
                        Id = 0,
                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                        NroCuota = nroCuota.NroCuota + 1,
                        NroSubCuota = 1,
                        FechaVencimiento = DateTime.Now,
                        TotalPagar = 0,
                        Cuota = montoDevolver,
                        Mora = 0,
                        MontoPagado = 0,
                        Saldo = 0,
                        Cancelado = true,
                        TipoCuota = "CUOTA",
                        Moneda = monedaOriginal.Moneda,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        UsuarioCreacion = Usuario,
                        FechaModificacion = DateTime.Now,
                        UsuarioModificacion = Usuario,
                        Enviado = false,
                        Version = versionAprobada.Version,
                        Aprobado = true,
                        UsuarioCoordinadorAcademico = UsuarioCoordinadorAcademico
                    };
                    _repCronogramaPagoDetalleFinal.Insert(nuevaCuota);

                }
                else if (Modoeliminacion == 1)//sin devolucion
                {
                    matriculaCabeceraTemp.EstadoMatricula = "sindevolucion";
                    matriculaCabeceraTemp.FechaModificacion = DateTime.Now;
                    matriculaCabeceraTemp.UsuarioModificacion = Usuario;
                    matriculaCabeceraTemp.FechaRetiro = DateTime.Now;
                    _repMatriculaCabecera.Update(matriculaCabeceraTemp);
                }
                else if (Modoeliminacion == 2)//eliminar
                {
                    matriculaCabeceraTemp.EstadoMatricula = "eliminado";
                    matriculaCabeceraTemp.FechaModificacion = DateTime.Now;
                    matriculaCabeceraTemp.UsuarioModificacion = Usuario;
                    matriculaCabeceraTemp.FechaRetiro = DateTime.Now;
                    _repMatriculaCabecera.Update(matriculaCabeceraTemp);

                    var listaDocumentos = _repControlDoc.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                    _repControlDoc.Delete(listaDocumentos, Usuario);
                    var listaDocumentosAlumno = _repControlDocAlumno.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                    _repControlDocAlumno.Delete(listaDocumentosAlumno, Usuario);
                    var listacronogramaPagoDetalleMod = repCronogramaPagoDetalleMod.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                    repCronogramaPagoDetalleMod.Delete(listacronogramaPagoDetalleMod, Usuario);
                    var listacronogramaPagoDetalle = _repCronogramaPagoDetalle.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                    _repCronogramaPagoDetalle.Delete(listacronogramaPagoDetalle, Usuario);
                    var listaPago = _repPago.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                    _repPago.Delete(listaPago, Usuario);
                    var listacronogramaPago = _repCronogramaPago.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                    _repCronogramaPago.Delete(listacronogramaPago, Usuario);
                    var listaMatriculaDetalle = _repMatriculaDetalle.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                    _repMatriculaDetalle.Delete(listaMatriculaDetalle, Usuario);
                    _repMatriculaCabecera.Delete(matriculaCabeceraTemp.Id, Usuario);

                    CronogramaPagoDetalleModLogFinalRepositorio _repCronogramaPagoDetalleModLogFinal = new CronogramaPagoDetalleModLogFinalRepositorio();
                    List<CronogramaPagoDetalleModLogFinalBO> listaLogs = new List<CronogramaPagoDetalleModLogFinalBO>();
                    var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                    var listaCronogramaFinal = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version).ToList();
                    foreach (var item in listaCronogramaFinal)
                    {
                        CronogramaPagoDetalleModLogFinalBO Log = new CronogramaPagoDetalleModLogFinalBO()
                        {
                            Id = 0,
                            Fecha = DateTime.Now,
                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                            NroCuota = item.NroCuota,
                            NroSubCuota = item.NroSubCuota,
                            FechaVencimiento = item.FechaVencimiento,
                            TotalPagar = item.TotalPagar,
                            Cuota = item.Cuota,
                            Mora = item.Mora,
                            MontoPagado = item.MontoPagado,
                            Saldo = item.Saldo,
                            Cancelado = item.Cancelado,
                            TipoCuota = item.TipoCuota,
                            Moneda = item.Moneda,
                            FechaPago = item.FechaPago,
                            IdFormaPago = item.IdFormaPago,
                            Estado = item.Estado,
                            Estado2 = item.Estado,
                            FechaPagoBanco = item.FechaPagoBanco,
                            Observaciones = item.Observaciones,
                            IdDocumentoPago = item.IdDocumentoPago,
                            NroDocumento = item.NroDocumento,
                            MonedaPago = item.MonedaPago,
                            TipoCambio = item.TipoCambio,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = Usuario,
                            FechaModificacion = DateTime.Now,
                            UsuarioModificacion = Usuario,
                            MensajeSistema = "ESTA CUOTA SE HA ELIMINADO",
                            FechaProcesoPago = item.FechaProcesoPago,
                            Version = item.Version,
                            Aprobado = true
                        };
                        listaLogs.Add(Log);
                    }
                    _repCronogramaPagoDetalleModLogFinal.Insert(listaLogs);

                    var listaCronogramaPagoDetalleOriginal = _repCronogramaPagoDetalleOriginal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                    _repCronogramaPagoDetalleOriginal.Delete(listaCronogramaPagoDetalleOriginal, Usuario);
                    var listaCronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                    _repCronogramaPagoDetalleFinal.Delete(listaCronogramaPagoDetalleFinal, Usuario);
                    var listaPagoFinal = _repPagoFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                    _repPagoFinal.Delete(listaPagoFinal, Usuario);
                    var listaCronogramaDetalleCambio = _repCronogramaDetalleCambio.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id).Select(w => w.Id).ToList();
                    _repCronogramaDetalleCambio.Delete(listaCronogramaDetalleCambio, Usuario);
                }
                var estado = matriculaCabeceraTemp.EstadoMatricula;

                return Ok(new { Message = "Se Elimino correctamente", CodMatricula = CodigoMatricula, Estado = estado });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult EliminarMatricula_Sincronizacion(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                var res = _repMatriculaCabecera.EliminarMatricula_Sincronizacion(idMatriculaCabecera);

                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// descarga un archivo en base al nombre recibido
        /// </summary>
        /// <returns>Json/returns>


        //escritura crep
        [HttpGet("[action]/{file}")]
        public virtual ActionResult Download(string file)
        {
            string destino = @"C:\Temp\Creps\" + file;
            byte[] reporte_facebook = System.IO.File.ReadAllBytes(destino);
            return File(reporte_facebook, "text/plain", file);
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera un crep de pago
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarCrep([FromBody] ListaCrepsDTO Json)
        {
            try
            {

                bool _tienemora = false;
                Int16 tipo = 0;
                foreach (var item in Json.lista)
                {
                    if (Convert.ToDouble(item.Mora) > 0)
                    {
                        _tienemora = true;
                    }
                    if (_tienemora)
                    {
                        tipo = 2;
                    }
                    else
                    {
                        tipo = 1;
                    }
                    switch (tipo)
                    {
                        case 1:
                            item.CodigoEspecial = "1" + item.nroCuota.ToString().PadLeft(2, '0') + item.nroSubcuota.ToString().PadLeft(2, '0') + "XXXXXX";
                            break;
                        case 2:
                            item.CodigoEspecial = "2" + item.nroCuota.ToString().PadLeft(2, '0') + item.nroSubcuota.ToString().PadLeft(2, '0') + item.Mora.ToString("n2").Replace(".", "").PadLeft(6, '0');
                            break;
                        default:
                            break;
                    }
                    _tienemora = false;
                }

                CronogramaPagoDetalleFinalBO CronogramaPagDetalleFinal = new CronogramaPagoDetalleFinalBO();
                var Result = CronogramaPagDetalleFinal.GenerarCrep(Json.objeto, Json.lista, Json.listaalumnos);
                string crep = Json.objeto.NombreArchivo;
                string destino = @"C:\Temp\Creps\" + crep + @".txt";

                MemoryStream ms = new MemoryStream(Result);
                FileStream file = new FileStream(destino, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();

                return Ok(new { crep });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Lee un archivo de texto y envia sus valores
        /// </summary>
        /// <returns>Json/returns>

        //lectura crep
        [Route("[action]")]
        [HttpPost]
        public ActionResult ProcesarCDPGFinanzas([FromForm] IFormFile files)
        {
            try
            {
                List<PagoBancoDTO> listaCuotas = new List<PagoBancoDTO>();
                StreamReader objReader = new StreamReader(files.OpenReadStream());
                string sLine = "";
                ArrayList detalle = new ArrayList();

                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        detalle.Add(sLine);
                }
                objReader.Close();
                int total = detalle.Count;
                for (int x = 1; x < total; x++)
                {
                    PagoBancoDTO pb = new PagoBancoDTO();
                    pb._codigousuario = detalle[x].ToString().Substring(13, 14);
                    pb._codigoespecial = detalle[x].ToString().Substring(27, 30);
                    pb._fechapago = detalle[x].ToString().Substring(57, 8);
                    pb._fechavencimiento = detalle[x].ToString().Substring(65, 8);
                    pb._montopago = (detalle[x].ToString().Substring(73, 15).Insert(13, ".")).TrimStart('0');
                    var mora = ((detalle[x].ToString().Substring(88, 15).Insert(13, ".")).TrimStart('0'));
                    pb._montomora = mora == ".00" ? "0.00" : mora;
                    pb._montototal = (detalle[x].ToString().Substring(103, 15).Insert(13, ".")).TrimStart('0');

                    pb._moneda = (detalle[x].ToString().Substring(5, 1) == "0" ? "soles" : "dolares");
                    pb._cuenta = detalle[x].ToString().Substring(124, 6);

                    //VALIDACIONES///////////////////////////////
                    CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinalRep = new CronogramaPagoDetalleFinalRepositorio();
                    MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                    var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == pb._codigousuario.Trim(), y => new { y.Id }).FirstOrDefault();
                    var versionAprobada = _repCronogramaPagoDetalleFinalRep.GetBy(y => y.IdMatriculaCabecera == matriculaCabeceraTemp.Id && y.Aprobado == true, y => new { y.Version }).OrderByDescending(y => y.Version).FirstOrDefault();
                    var validarAprobacion = _repCronogramaPagoDetalleFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version).Select(q => new { q.Aprobado }).FirstOrDefault();//lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);

                    if (validarAprobacion != null)
                    {
                        if (validarAprobacion.Aprobado.Value)
                        {
                            CronogramaPagoDetalleFinalBO CronogramaPagDetalleFinal = new CronogramaPagoDetalleFinalBO();
                            if (CronogramaPagDetalleFinal.ValidarCuota(pb._codigousuario.Trim(), pb._codigoespecial) == true)
                            {
                                pb._observaciones = "Nro Cuota Correcta";
                                //validamos el monto de la cuota                                
                                if (!CronogramaPagDetalleFinal.ValidarMonto(pb._codigousuario.Trim(), pb._codigoespecial.Trim().Substring(1, 2), pb._codigoespecial.Trim().Substring(3, 2), Convert.ToDouble(pb._montopago), pb._codigoespecial.Trim().Substring(0, 4)))
                                {
                                    pb._observaciones += ", Monto no coincide";
                                }
                            }
                            else
                            {
                                pb._observaciones = "Nro Cuota no coincide";
                            }
                        }
                        else
                        {
                            pb._observaciones = "El cronograma tiene cambios pendientes o matricula no existe";
                        }
                    }

                    listaCuotas.Add(pb);
                }
                var Nregistros = total - 1;
                return Ok(new { listaCuotas, Nregistros });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 02/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera los apgos en base a la lista enviada por los creps
        /// </summary>
        /// <returns>Json/returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ProcesarPagos([FromBody] PagoBancoListDTO Json)
        {
            try
            {
                string idmat;
                int nrosubcuota = 0, nrocuota = 0;
                DateTime fechapago;
                double montopagado, morabanco;
                string moneda;
                string nroDoc;
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();

                for (int i = 0; i < Json.listaPagosBanco.Count; i++)
                {
                    idmat = Json.listaPagosBanco[i]._codigousuario.Trim();
                    if (Json.listaPagosBanco[i]._codigoespecial.Trim().Substring(0, 2) == "1C") //código anterior
                    {
                        nrocuota = int.Parse(Json.listaPagosBanco[i]._codigoespecial.Trim().Substring(2, 2));
                        nrosubcuota = 1;
                    }
                    else
                    {
                        nrocuota = int.Parse(Json.listaPagosBanco[i]._codigoespecial.Trim().Substring(1, 2));
                        nrosubcuota = int.Parse(Json.listaPagosBanco[i]._codigoespecial.Trim().Substring(3, 2));
                    }
                    //fechapago = Convert.ToDateTime(Json.listaPagosBanco[i]._fechapago.Substring(6, 2) + "/" + Json.listaPagosBanco[i]._fechapago.Substring(4, 2) + "/" + Json.listaPagosBanco[i]._fechapago.Substring(0, 4));
                    fechapago = new DateTime(Convert.ToInt32(Json.listaPagosBanco[i]._fechapago.Substring(0, 4)), Convert.ToInt32(Json.listaPagosBanco[i]._fechapago.Substring(4, 2)), Convert.ToInt32(Json.listaPagosBanco[i]._fechapago.Substring(6, 2)));

                    //Buscar el periodo
                    int IdPeriodo = 0;

                    try
                    {
                        IdPeriodo = _repPeriodo.GetBy(y => y.FechaInicialFinanzas.Date <= fechapago.Date && y.FechaFinFinanzas.Date >= fechapago.Date).OrderByDescending(y => y.FechaCreacion).Select(y => y.Id).FirstOrDefault();
                    }
                    catch (Exception Ex)
                    {
                        IdPeriodo = 0;
                    }
                    //End Buscar periodo
                    montopagado = Convert.ToDouble(Json.listaPagosBanco[i]._montototal);
                    morabanco = Convert.ToDouble(Json.listaPagosBanco[i]._montomora);
                    moneda = Json.listaPagosBanco[i]._moneda;
                    nroDoc = Json.listaPagosBanco[i]._cuenta;
                    CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinalRep = new CronogramaPagoDetalleFinalRepositorio();
                    var resultado = _repCronogramaPagoDetalleFinalRep.PagarCuotaCDPG_CtoFinal(idmat, nrocuota, nrosubcuota, fechapago, montopagado, morabanco, moneda, nroDoc, IdPeriodo, Json.Usuario);

                }
                return Ok(new { Message = "Se Proceso correctamente" });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST 
        /// Autor: Lisbeth, Miguel
        /// Fecha: 28/04/2021
        /// Versión: 3.0
        /// <summary>
        /// Guarda todos los cambios del cronograma asi como sus detalles y también los log de este
        /// modificacion : se agrego el campo  UsuarioCoordinadorAcademico a la tabla de cronograma de la matricula
        /// </summary>
        /// <returns>Json</returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarCronograma([FromBody] CronogramaModificadoDTO Json)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                integraDBContext _integraDbContext = new integraDBContext();

                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDbContext);
                CronogramaDetalleCambioRepositorio _repCronogramaDetalleCambio = new CronogramaDetalleCambioRepositorio(_integraDbContext);
                CronogramaCabeceraCambioRepositorio _repCronogramaCabeceraCambio = new CronogramaCabeceraCambioRepositorio(_integraDbContext);
                CronogramaPagoDetalleModLogFinalRepositorio _repCronogramaPagoDetalleModLogFinal = new CronogramaPagoDetalleModLogFinalRepositorio(_integraDbContext);
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDbContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDbContext);

                var IdMatriculaCabeceraSincronizacion = 0;

                using (TransactionScope scope = new TransactionScope())
                {
                    var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(w => w.CodigoMatricula == Json.Objeto.CodigoMatricula).FirstOrDefault();
                    var versionAprobadoTemp = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                    var cronogramaActual = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobadoTemp.Version, x => new { x.Id, x.Cancelado, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.MoraTarifario, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version, x.FechaDeposito, x.FechaEfectivoDisponible, x.FechaIngresoEnCuenta, x.FechaProcesoPagoReal, x.FechaCompromiso1, x.FechaCompromiso2, x.FechaCompromiso3, x.UsuarioCoordinadorAcademico }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota).ToList();
                    IdMatriculaCabeceraSincronizacion = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula.Equals(matriculaCabeceraTemp.CodigoMatricula)).Select(x => x.Id).FirstOrDefault();
                    var ExisteVersionNoAprobada = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Aprobado == false).ToList();
                    if (ExisteVersionNoAprobada.Count > 0)
                    {
                        return BadRequest("Existe Versiones sin Aprobar");
                    }
                    else
                    {
                        //Insert Ultima Version

                        //inserto el cronograma como quedo despues de los cambios con la version +1
                        decimal totalMonto = Json.ListaCronograma.Sum(x => x.Cuota);
                        var versionNueva = cronogramaActual.FirstOrDefault().Version + 1;
                        string _UsuarioCoordinadorAcademico = "";
                        foreach (var itemCronograma in Json.ListaCronograma)
                        {
                            var existe = cronogramaActual.Where(w => w.Id.ToString() == itemCronograma.Id).FirstOrDefault();
                            Nullable<decimal> _montoPagado, _tipoCambio, _cuotaDolares, _moraTarifario;
                            Nullable<DateTime> _fechaPago, _fechaPagoBanco, _fechaProcesoPago, _fechaDeposito, _fechaEfectivoDisponible, _fechaIngresoEnCuenta, _fechaProcesoPagoReal, _fechaCompromiso1, _fechaCompromiso2, _fechaCompromiso3;
                            Nullable<int> _idFormaPago, _idCuenta, _idDocumentoPago;
                            Nullable<bool> _enviado;
                            string _observaciones, _nroDocumento, _monedaPago;

                            if (existe != null)
                            {
                                _montoPagado = existe.MontoPagado;
                                _tipoCambio = existe.TipoCambio;
                                _cuotaDolares = existe.CuotaDolares;
                                _fechaPago = existe.FechaPago;
                                _fechaPagoBanco = existe.FechaPagoBanco;
                                _fechaProcesoPago = existe.FechaProcesoPago;
                                _idFormaPago = existe.IdFormaPago;
                                _idCuenta = existe.IdCuenta;
                                _idDocumentoPago = existe.IdDocumentoPago;
                                _observaciones = existe.Observaciones;
                                _nroDocumento = existe.NroDocumento;
                                _monedaPago = existe.MonedaPago;
                                _fechaDeposito = existe.FechaDeposito;
                                _fechaEfectivoDisponible = existe.FechaEfectivoDisponible;
                                _fechaIngresoEnCuenta = existe.FechaIngresoEnCuenta;
                                _fechaProcesoPagoReal = existe.FechaProcesoPagoReal;
                                _moraTarifario = existe.MoraTarifario;
                                _fechaCompromiso1 = existe.FechaCompromiso1;
                                _fechaCompromiso2 = existe.FechaCompromiso2;
                                _fechaCompromiso3 = existe.FechaCompromiso3;
                                _UsuarioCoordinadorAcademico = existe.UsuarioCoordinadorAcademico;
                                Console.WriteLine(_montoPagado);
                            }
                            else
                            {

                                _montoPagado = 0;
                                _tipoCambio = null;
                                _cuotaDolares = null;
                                _fechaPago = null;
                                _fechaPagoBanco = null;
                                _fechaProcesoPago = null;
                                _idFormaPago = null;
                                _idCuenta = null;
                                _idDocumentoPago = null;
                                _enviado = false;
                                _observaciones = null;
                                _nroDocumento = null;
                                _monedaPago = null;
                                _fechaDeposito = null;
                                _fechaEfectivoDisponible = null;
                                _fechaIngresoEnCuenta = null;
                                _fechaProcesoPagoReal = null;
                                _moraTarifario = null;
                                _fechaCompromiso1 = null;
                                _fechaCompromiso2 = null;
                                _fechaCompromiso3 = null;
                            }

                            CronogramaPagoDetalleFinalBO cuota = new CronogramaPagoDetalleFinalBO()
                            {
                                Id = 0,
                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                NroCuota = itemCronograma.NroCuota,
                                NroSubCuota = itemCronograma.NroSubCuota,
                                FechaVencimiento = itemCronograma.FechaVencimiento,
                                TotalPagar = totalMonto,
                                Cuota = itemCronograma.Cuota,
                                Saldo = totalMonto - itemCronograma.Cuota,
                                Mora = itemCronograma.Mora,
                                MoraTarifario = _moraTarifario,
                                FechaCompromiso1 = _fechaCompromiso1,
                                FechaCompromiso2 = _fechaCompromiso2,
                                FechaCompromiso3 = _fechaCompromiso3,
                                Cancelado = itemCronograma.Cancelado,
                                TipoCuota = itemCronograma.TipoCuota,
                                Moneda = itemCronograma.Moneda,
                                TipoCambio = _tipoCambio,
                                Version = versionNueva,
                                FechaPago = _fechaPago,
                                FechaDeposito = _fechaDeposito,
                                IdFormaPago = _idFormaPago,
                                IdCuenta = _idCuenta,
                                FechaPagoBanco = _fechaPagoBanco,
                                Enviado = itemCronograma.Enviado,
                                Observaciones = _observaciones,
                                IdDocumentoPago = _idDocumentoPago,
                                NroDocumento = _nroDocumento,
                                MonedaPago = _monedaPago,
                                FechaProcesoPago = _fechaProcesoPago,
                                MontoPagado = _montoPagado,
                                Aprobado = false,//Se convierte en true cuando aprueba los cambios
                                FechaEfectivoDisponible = _fechaEfectivoDisponible,
                                FechaIngresoEnCuenta = _fechaIngresoEnCuenta,
                                FechaProcesoPagoReal = _fechaProcesoPagoReal,
                                Estado = true,
                                UsuarioCreacion = Json.Usuario,
                                UsuarioModificacion = Json.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCoordinadorAcademico = _UsuarioCoordinadorAcademico
                            };
                            _repCronogramaPagoDetalleFinal.Insert(cuota);
                            totalMonto = totalMonto - itemCronograma.Cuota;//actualizo el nuevo montototal
                        }

                        //Insert Cambios Log
                        var listaCambiosOrdenAgrupado = (from m in Json.ListaCambiosOrden
                                                         group m by new { m.Orden } into grupo
                                                         select new { g = grupo.Key, l = grupo }).ToList();

                        List<CronogramaPagoDetalleModLogFinalBO> listaLog = new List<CronogramaPagoDetalleModLogFinalBO>();//aqui se guarda el log de los cambios
                        foreach (var orden in listaCambiosOrdenAgrupado.OrderBy(w => w.g.Orden))
                        {
                            if (orden.l.FirstOrDefault().TipoCambio == "Fraccion" || orden.l.FirstOrDefault().TipoCambio == "Fraccion Reemplazado")
                            {
                                //ttiposcambios=4E186D5A-0076-48E0-AF54-01210B243BBD
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambioBO CambioFraccion = new CronogramaCabeceraCambioBO()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 1,
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };

                                var CambioInsertadoFraccion = _repCronogramaCabeceraCambio.Insert(CambioFraccion);

                                //ahora insertamos los detalles
                                var idPadre = orden.l.FirstOrDefault(w => w.TipoCambio == "Fraccion").id;//el id del padre
                                var padre = Json.ListaCronograma.Where(w => w.Id == idPadre).FirstOrDefault();
                                if (padre == null)
                                {
                                    //nunca se debe dar ya esta validado por javascript
                                    //el padre fue eliminado por ende los hijos tamcpo deben existir
                                }
                                else
                                {
                                    CronogramaDetalleCambioBO padreTemp = new CronogramaDetalleCambioBO()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        IdCronogramaCabeceraCambio = CambioFraccion.Id,
                                        NroCuota = padre.NroCuota,
                                        NroSubcuota = padre.NroSubCuota,
                                        FechaVencimiento = padre.FechaVencimiento,
                                        Cuota = padre.Cuota,
                                        Mora = padre.Mora,
                                        Moneda = padre.Moneda,
                                        Version = versionNueva.Value,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = Json.Usuario,
                                        UsuarioModificacion = Json.Usuario

                                    };
                                    _repCronogramaDetalleCambio.Insert(padreTemp);

                                    //INSERTO EN LOG
                                    if (listaLog.Where(w => w.NroCuota == padre.NroCuota && w.NroSubCuota == padre.NroSubCuota).FirstOrDefault() != null)//NUCNA DEBERIA ENTRAR AQUI PORQ ES AGREGADO SOLO ESE CAMBIO
                                    {
                                        var antiguo = listaLog.Where(w => w.NroCuota == padre.NroCuota && w.NroSubCuota == padre.NroSubCuota).FirstOrDefault();
                                        antiguo.MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA (" + padre.NroCuota + "," + padre.NroSubCuota + "), " + padre.Cuota;
                                    }
                                    else//es nuevo
                                    {
                                        CronogramaPagoDetalleModLogFinalBO log = new CronogramaPagoDetalleModLogFinalBO()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            Fecha = DateTime.Now,
                                            NroCuota = padre.NroCuota,
                                            NroSubCuota = padre.NroSubCuota,
                                            FechaVencimiento = padre.FechaVencimiento,
                                            TotalPagar = padre.TotalPagar,
                                            Cuota = padre.Cuota,
                                            Mora = padre.Mora,
                                            MontoPagado = 0,
                                            Saldo = padre.Saldo,
                                            Cancelado = padre.Cancelado,
                                            TipoCuota = padre.TipoCuota,
                                            Moneda = padre.Moneda,
                                            FechaPago = null,
                                            IdFormaPago = null,
                                            FechaPagoBanco = null,
                                            Ultimo = false,
                                            IdDocumentoPago = null,
                                            NroDocumento = null,
                                            MonedaPago = null,
                                            TipoCambio = null,
                                            MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA  (" + padre.NroCuota + "," + padre.NroSubCuota + "), MONTO " + padre.Cuota,
                                            FechaProcesoPago = null,
                                            EstadoPrimerLog = null
                                        };
                                        listaLog.Add(log);

                                        var fechaoriginal = cronogramaActual.Where(w => w.NroCuota == padre.NroCuota && w.NroSubCuota == padre.NroSubCuota).FirstOrDefault();
                                        string fechaoriginaldate = fechaoriginal != null ? fechaoriginal.FechaVencimiento.Value.ToString("dd/MM/yyyy") : "";
                                        if (fechaoriginaldate != padre.FechaVencimiento.ToString("dd/MM/yyyy"))
                                        {
                                            log = new CronogramaPagoDetalleModLogFinalBO()
                                            {
                                                Id = 0,
                                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                                Fecha = DateTime.Now,
                                                NroCuota = padre.NroCuota,
                                                NroSubCuota = padre.NroSubCuota,
                                                FechaVencimiento = padre.FechaVencimiento,
                                                TotalPagar = padre.TotalPagar,
                                                Cuota = padre.Cuota,
                                                Mora = padre.Mora,
                                                MontoPagado = 0,
                                                Saldo = padre.Saldo,
                                                Cancelado = padre.Cancelado,
                                                TipoCuota = padre.TipoCuota,
                                                Moneda = padre.Moneda,
                                                FechaPago = null,
                                                IdFormaPago = null,
                                                FechaPagoBanco = null,
                                                Ultimo = false,
                                                IdDocumentoPago = null,
                                                NroDocumento = null,
                                                MonedaPago = null,
                                                TipoCambio = null,
                                                MensajeSistema = "FECHA DE CUOTA (" + padre.NroCuota + "," + padre.NroSubCuota + ") HA VARIADO DE " + fechaoriginaldate + " A " + padre.FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss"),
                                                FechaProcesoPago = null,
                                                EstadoPrimerLog = null
                                            };

                                            listaLog.Add(log);
                                        }
                                    }

                                    foreach (var hijos in orden.l.Where(w => w.TipoCambio == "Fraccion").OrderBy(w => w.Cuota).ThenBy(w => w.SubCuota))
                                    {
                                        var hijo = Json.ListaCronograma.Where(w => w.NroCuota == hijos.Cuota && w.NroSubCuota == hijos.SubCuota).FirstOrDefault();
                                        CronogramaDetalleCambioBO hijoTemp = new CronogramaDetalleCambioBO()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            IdCronogramaCabeceraCambio = CambioFraccion.Id,
                                            NroCuota = hijo.NroCuota,
                                            NroSubcuota = hijo.NroSubCuota,
                                            FechaVencimiento = hijo.FechaVencimiento,
                                            Cuota = hijo.Cuota,
                                            Mora = hijo.Mora,
                                            Moneda = hijo.Moneda,
                                            Version = versionNueva.Value,
                                            Estado = true,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            UsuarioCreacion = Json.Usuario,
                                            UsuarioModificacion = Json.Usuario

                                        };
                                        _repCronogramaDetalleCambio.Insert(hijoTemp);

                                        //INSERTO EN LOG
                                        if (listaLog.Where(w => w.NroCuota == hijo.NroCuota && w.NroSubCuota == hijo.NroSubCuota).FirstOrDefault() != null)//NUCNA DEBERIA ENTRAR AQUI PORQ ES AGREGADO SOLO ESE CAMBIO
                                        {
                                            var antiguo = listaLog.Where(w => w.NroCuota == hijo.Cuota && w.NroSubCuota == hijo.NroSubCuota).FirstOrDefault();
                                            antiguo.MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA (" + hijo.NroCuota + "," + hijo.NroSubCuota + "),  " + hijo.Cuota;
                                        }
                                        else//es nuevo
                                        {
                                            CronogramaPagoDetalleModLogFinalBO log = new CronogramaPagoDetalleModLogFinalBO()
                                            {
                                                Id = 0,
                                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                                Fecha = DateTime.Now,
                                                NroCuota = hijo.NroCuota,
                                                NroSubCuota = hijo.NroSubCuota,
                                                FechaVencimiento = hijo.FechaVencimiento,
                                                TotalPagar = hijo.TotalPagar,
                                                Cuota = hijo.Cuota,
                                                Mora = hijo.Mora,
                                                MontoPagado = 0,
                                                Saldo = hijo.Saldo,
                                                Cancelado = hijo.Cancelado,
                                                TipoCuota = hijo.TipoCuota,
                                                Moneda = hijo.Moneda,
                                                FechaPago = null,
                                                IdFormaPago = null,
                                                FechaPagoBanco = null,
                                                Ultimo = false,
                                                IdDocumentoPago = null,
                                                NroDocumento = null,
                                                MonedaPago = null,
                                                TipoCambio = null,
                                                MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA  (" + hijo.NroCuota + "," + hijo.NroSubCuota + "), MONTO " + hijo.Cuota,
                                                FechaProcesoPago = null,
                                                EstadoPrimerLog = null
                                            };
                                            listaLog.Add(log);
                                            var fechaoriginal = cronogramaActual.Where(w => w.NroCuota == padre.NroCuota && w.NroSubCuota == padre.NroSubCuota).FirstOrDefault();
                                            string fechaoriginaldate = fechaoriginal != null ? fechaoriginal.FechaVencimiento.Value.ToString("dd/MM/yyyy") : "";
                                            if (fechaoriginaldate != hijo.FechaVencimiento.ToString("dd/MM/yyyy"))
                                            {
                                                log = new CronogramaPagoDetalleModLogFinalBO()
                                                {
                                                    Id = 0,
                                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                                    Fecha = DateTime.Now,
                                                    NroCuota = hijo.NroCuota,
                                                    NroSubCuota = hijo.NroSubCuota,
                                                    FechaVencimiento = hijo.FechaVencimiento,
                                                    TotalPagar = hijo.TotalPagar,
                                                    Cuota = hijo.Cuota,
                                                    Mora = hijo.Mora,
                                                    MontoPagado = 0,
                                                    Saldo = hijo.Saldo,
                                                    Cancelado = hijo.Cancelado,
                                                    TipoCuota = hijo.TipoCuota,
                                                    Moneda = hijo.Moneda,
                                                    FechaPago = null,
                                                    IdFormaPago = null,
                                                    FechaPagoBanco = null,
                                                    Ultimo = false,
                                                    IdDocumentoPago = null,
                                                    NroDocumento = null,
                                                    MonedaPago = null,
                                                    TipoCambio = null,
                                                    MensajeSistema = "FECHA DE CUOTA (" + hijo.NroCuota + "," + hijo.NroSubCuota + ") HA VARIADO DE " + fechaoriginaldate + " A " + hijo.FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss"),
                                                    FechaProcesoPago = null,
                                                    EstadoPrimerLog = null
                                                };

                                                listaLog.Add(log);
                                            }

                                        }


                                    }
                                    foreach (var hijos in orden.l.Where(w => w.TipoCambio == "Fraccion Reemplazado").OrderBy(w => w.Cuota).ThenBy(w => w.SubCuota))
                                    {
                                        var hijo = Json.ListaCronograma.Where(w => w.Id == hijos.id).FirstOrDefault();

                                        CronogramaDetalleCambioBO hijoTemp = new CronogramaDetalleCambioBO()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            IdCronogramaCabeceraCambio = CambioFraccion.Id,
                                            NroCuota = hijo.NroCuota,
                                            NroSubcuota = hijo.NroSubCuota,
                                            FechaVencimiento = hijo.FechaVencimiento,
                                            Cuota = hijo.Cuota,
                                            Mora = hijo.Mora,
                                            Moneda = hijo.Moneda,
                                            Version = versionNueva.Value,
                                            Estado = true,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            UsuarioCreacion = Json.Usuario,
                                            UsuarioModificacion = Json.Usuario

                                        };
                                        _repCronogramaDetalleCambio.Insert(hijoTemp);

                                    }
                                }

                            }

                            if (orden.l.FirstOrDefault().TipoCambio == "Agregar")
                            {
                                //ttiposcambios=A62C8DCD-7AF1-49B1-8A23-C359E7A0D9DB
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambioBO CambioAgregar = new CronogramaCabeceraCambioBO()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 9,
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                var cambioinsertadoagregado = _repCronogramaCabeceraCambio.Insert(CambioAgregar);

                                var agregado = Json.ListaCronograma.Where(w => w.NroCuota == orden.l.FirstOrDefault().Cuota && w.NroSubCuota == orden.l.FirstOrDefault().SubCuota).FirstOrDefault();

                                CronogramaDetalleCambioBO agregadoTemp = new CronogramaDetalleCambioBO()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = CambioAgregar.Id,
                                    NroCuota = agregado.NroCuota,
                                    NroSubcuota = agregado.NroSubCuota,
                                    FechaVencimiento = agregado.FechaVencimiento,
                                    Cuota = agregado.Cuota,
                                    Mora = agregado.Mora,
                                    Moneda = agregado.Moneda,
                                    Version = versionNueva.Value,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario

                                };
                                _repCronogramaDetalleCambio.Insert(agregadoTemp);

                                //INSERTO EN LOG
                                if (listaLog.Where(w => w.NroCuota == agregado.NroCuota && w.NroSubCuota == agregado.NroSubCuota).FirstOrDefault() != null)//NUCNA DEBERIA ENTRAR AQUI PORQ ES AGREGADO SOLO ESE CAMBIO
                                {
                                    var antiguo = listaLog.Where(w => w.NroCuota == agregado.NroCuota && w.NroSubCuota == agregado.NroSubCuota).FirstOrDefault();
                                    antiguo.MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA (" + agregado.NroCuota + "," + agregado.NroSubCuota + "), MONTO " + agregado.Cuota + " Y FECHA " + agregado.FechaVencimiento;
                                }
                                else//es nuevo
                                {
                                    CronogramaPagoDetalleModLogFinalBO log = new CronogramaPagoDetalleModLogFinalBO()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        Fecha = DateTime.Now,
                                        NroCuota = agregado.NroCuota,
                                        NroSubCuota = agregado.NroSubCuota,
                                        FechaVencimiento = agregado.FechaVencimiento,
                                        TotalPagar = agregado.TotalPagar,
                                        Cuota = agregado.Cuota,
                                        Mora = agregado.Mora,
                                        MontoPagado = 0,
                                        Saldo = agregado.Saldo,
                                        Cancelado = agregado.Cancelado,
                                        TipoCuota = agregado.TipoCuota,
                                        Moneda = agregado.Moneda,
                                        FechaPago = null,
                                        IdFormaPago = null,
                                        FechaPagoBanco = null,
                                        Ultimo = false,
                                        IdDocumentoPago = null,
                                        NroDocumento = null,
                                        MonedaPago = null,
                                        TipoCambio = null,
                                        MensajeSistema = "SE AGREGÓ UNA NUEVA CUOTA  (" + agregado.NroCuota + "," + agregado.NroSubCuota + "), MONTO " + agregado.Cuota + " Y FECHA " + agregado.FechaVencimiento,
                                        FechaProcesoPago = null,
                                        EstadoPrimerLog = null
                                    };
                                    listaLog.Add(log);
                                }
                            }

                            if (orden.l.FirstOrDefault().TipoCambio == "Eliminado")
                            {
                                //ttiposcambios=5DF6C4B0-23FC-4F7F-9E22-CAE3DA4F7FC2
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambioBO CambioEliminar = new CronogramaCabeceraCambioBO()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 11,
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                var cambioinsertadoeliminado = _repCronogramaCabeceraCambio.Insert(CambioEliminar);

                                //como no esta en listaCronograma proque fue eliminado //lo busco en el original
                                var eliminado = cronogramaActual.Where(w => w.NroCuota == orden.l.FirstOrDefault().Cuota && w.NroSubCuota == orden.l.FirstOrDefault().SubCuota).FirstOrDefault();

                                CronogramaDetalleCambioBO eliminadoTemp = new CronogramaDetalleCambioBO()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    IdCronogramaCabeceraCambio = CambioEliminar.Id,
                                    NroCuota = eliminado.NroCuota.Value,
                                    NroSubcuota = eliminado.NroSubCuota.Value,
                                    FechaVencimiento = eliminado.FechaVencimiento.Value,
                                    Cuota = 0,
                                    Mora = eliminado.Mora.Value,
                                    Moneda = eliminado.Moneda,
                                    Version = versionNueva.Value,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now

                                };
                                _repCronogramaDetalleCambio.Insert(eliminadoTemp);

                                //INSERTO EN LOG
                                if (listaLog.Where(w => w.NroCuota == eliminado.NroCuota && w.NroSubCuota == eliminado.NroSubCuota).FirstOrDefault() != null)//ya esta en la lista
                                {
                                    var antiguo = listaLog.Where(w => w.NroCuota == eliminado.NroCuota && w.NroSubCuota == eliminado.NroSubCuota).FirstOrDefault();
                                    antiguo.MensajeSistema = "CUOTA ELIMINADA (" + eliminado.NroCuota + "," + eliminado.NroSubCuota + ")";
                                }
                                else//es nuevo
                                {
                                    CronogramaPagoDetalleModLogFinalBO log = new CronogramaPagoDetalleModLogFinalBO()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        Fecha = DateTime.Now,
                                        NroCuota = eliminado.NroCuota,
                                        NroSubCuota = eliminado.NroSubCuota,
                                        FechaVencimiento = eliminado.FechaVencimiento,
                                        TotalPagar = eliminado.TotalPagar,
                                        Cuota = eliminado.Cuota,
                                        Mora = eliminado.Mora,
                                        MontoPagado = 0,
                                        Saldo = eliminado.Saldo,
                                        Cancelado = eliminado.Cancelado,
                                        TipoCuota = eliminado.TipoCuota,
                                        Moneda = eliminado.Moneda,
                                        FechaPago = null,
                                        IdFormaPago = eliminado.IdFormaPago,
                                        FechaPagoBanco = null,
                                        Ultimo = false,
                                        IdDocumentoPago = null,
                                        NroDocumento = null,
                                        MonedaPago = null,
                                        TipoCambio = null,
                                        MensajeSistema = "CUOTA ELIMINADA (" + eliminado.NroCuota + "," + eliminado.NroSubCuota + ")",
                                        FechaProcesoPago = null,
                                        EstadoPrimerLog = null
                                    };
                                    listaLog.Add(log);
                                }

                            }

                            if (orden.l.FirstOrDefault().TipoCambio == "Fecha")
                            {
                                //ttiposcambios=EFEFEA69-38BF-493D-97C8-014F8642BD37
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambioBO CambioFechaTodoCronograma = new CronogramaCabeceraCambioBO()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 13,//una cuota
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now

                                };
                                var cambiofechainsertado = _repCronogramaCabeceraCambio.Insert(CambioFechaTodoCronograma);

                                foreach (var detalles in orden.l.OrderBy(w => w.Cuota).ThenBy(w => w.SubCuota))//solo deberia ser 1
                                {
                                    var valoritem = Json.ListaCronograma.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();//saca del ultimo con cambios
                                    CronogramaDetalleCambioBO cambioTemp = new CronogramaDetalleCambioBO()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        IdCronogramaCabeceraCambio = CambioFechaTodoCronograma.Id,
                                        NroCuota = valoritem.NroCuota,
                                        NroSubcuota = valoritem.NroSubCuota,
                                        FechaVencimiento = valoritem.FechaVencimiento,
                                        Cuota = valoritem.Cuota,
                                        Mora = valoritem.Mora,
                                        Moneda = valoritem.Moneda,
                                        Version = versionNueva.Value,
                                        Estado = true,
                                        UsuarioCreacion = Json.Usuario,
                                        UsuarioModificacion = Json.Usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now

                                    };
                                    _repCronogramaDetalleCambio.Insert(cambioTemp);

                                    //INSERTO EN LOG
                                    var fechaoriginal = cronogramaActual.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();//deberia existir
                                    string fechaoriginaldate = fechaoriginal != null ? fechaoriginal.FechaVencimiento.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";

                                    if (listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault() != null)//ya esta en la lista
                                    {
                                        var antiguo = listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault();
                                        antiguo.MensajeSistema = antiguo.MensajeSistema + ", FECHA DE CUOTA (" + valoritem.NroCuota + "," + valoritem.NroSubCuota + ") HA VARIADO DE " + fechaoriginaldate + " A " + valoritem.FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss");
                                    }
                                    else//es nuevo
                                    {
                                        //traigo la fech original


                                        CronogramaPagoDetalleModLogFinalBO log = new CronogramaPagoDetalleModLogFinalBO()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            Fecha = DateTime.Now,
                                            NroCuota = valoritem.NroCuota,
                                            NroSubCuota = valoritem.NroSubCuota,
                                            FechaVencimiento = valoritem.FechaVencimiento,
                                            TotalPagar = valoritem.TotalPagar,
                                            Cuota = valoritem.Cuota,
                                            Mora = valoritem.Mora,
                                            MontoPagado = 0,
                                            Saldo = valoritem.Saldo,
                                            Cancelado = valoritem.Cancelado,
                                            TipoCuota = valoritem.TipoCuota,
                                            Moneda = valoritem.Moneda,
                                            FechaPago = null,
                                            IdFormaPago = null,
                                            FechaPagoBanco = null,
                                            Ultimo = false,
                                            IdDocumentoPago = null,
                                            NroDocumento = null,
                                            MonedaPago = null,
                                            TipoCambio = null,
                                            MensajeSistema = "FECHA DE CUOTA (" + valoritem.NroCuota + "," + valoritem.NroSubCuota + ") HA VARIADO DE " + fechaoriginaldate + " A " + valoritem.FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss"),
                                            FechaProcesoPago = null,
                                            EstadoPrimerLog = null

                                        };
                                        listaLog.Add(log);

                                    }
                                }
                            }

                            if (orden.l.FirstOrDefault().TipoCambio == "Monto")
                            {
                                //ttiposcambios=E970D10D-00BF-401C-991A-32F7A32B4C52
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambioBO CambioMonto = new CronogramaCabeceraCambioBO()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 4,//una cuota
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };

                                var cambiomontoinsertado = _repCronogramaCabeceraCambio.Insert(CambioMonto);

                                foreach (var detalles in orden.l.OrderBy(w => w.Cuota).ThenBy(w => w.SubCuota))//solo deberia ser 1
                                {
                                    var valoritem = Json.ListaCronograma.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();
                                    CronogramaDetalleCambioBO cambioTemp = new CronogramaDetalleCambioBO()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        IdCronogramaCabeceraCambio = CambioMonto.Id,
                                        NroCuota = valoritem.NroCuota,
                                        NroSubcuota = valoritem.NroSubCuota,
                                        FechaVencimiento = valoritem.FechaVencimiento,
                                        Cuota = valoritem.Cuota,
                                        Mora = valoritem.Mora,
                                        Moneda = valoritem.Moneda,
                                        Version = versionNueva.Value,
                                        Estado = true,
                                        UsuarioCreacion = Json.Usuario,
                                        UsuarioModificacion = Json.Usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now

                                    };
                                    _repCronogramaDetalleCambio.Insert(cambioTemp);

                                    //INSERTO EN LOG
                                    var montooriginal = cronogramaActual.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();//deberia existir
                                    string montooriginaltexto = montooriginal != null ? montooriginal.Cuota.ToString() : "";

                                    if (listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault() != null)//ya esta en la lista
                                    {
                                        var antiguo = listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault();
                                        antiguo.MensajeSistema = antiguo.MensajeSistema + ", CUOTA HA VARIADO DE " + montooriginaltexto + " A " + valoritem.Cuota;
                                    }
                                    else//es nuevo
                                    {
                                        CronogramaPagoDetalleModLogFinalBO log = new CronogramaPagoDetalleModLogFinalBO()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            Fecha = DateTime.Now,
                                            NroCuota = valoritem.NroCuota,
                                            NroSubCuota = valoritem.NroSubCuota,
                                            FechaVencimiento = valoritem.FechaVencimiento,
                                            TotalPagar = valoritem.TotalPagar,
                                            Cuota = valoritem.Cuota,
                                            Mora = valoritem.Mora,
                                            MontoPagado = 0,
                                            Saldo = valoritem.Saldo,
                                            Cancelado = valoritem.Cancelado,
                                            TipoCuota = valoritem.TipoCuota,
                                            Moneda = valoritem.Moneda,
                                            FechaPago = null,
                                            IdFormaPago = null,
                                            FechaPagoBanco = null,
                                            Ultimo = false,
                                            IdDocumentoPago = null,
                                            NroDocumento = null,
                                            MonedaPago = null,
                                            TipoCambio = null,
                                            MensajeSistema = "CUOTA (" + valoritem.NroCuota + "," + valoritem.NroSubCuota + ") HA VARIADO DE " + montooriginaltexto + " A " + valoritem.Cuota,
                                            FechaProcesoPago = null,
                                            EstadoPrimerLog = null

                                        };
                                        listaLog.Add(log);

                                    }
                                }

                            }

                            if (orden.l.FirstOrDefault().TipoCambio == "Mora")
                            {
                                //ttiposcambios=7FC068ED-F677-4D66-BEA1-5403721A851D
                                //creo un nuevo tcambios con tipocambio de arriba

                                CronogramaCabeceraCambioBO CambioMora = new CronogramaCabeceraCambioBO()
                                {
                                    Id = 0,
                                    IdCronogramaTipoModificacion = 5,//una cuota
                                    SolicitadoPor = Json.Objeto.SolicitadoPorId,
                                    AprobadoPor = Json.Objeto.AprobadoPorId,
                                    Aprobado = false,
                                    Cancelado = false,
                                    Observacion = Json.Objeto.Comentario,
                                    Estado = true,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };

                                var cambiomorainsertado = _repCronogramaCabeceraCambio.Insert(CambioMora);

                                foreach (var detalles in orden.l.OrderBy(w => w.Cuota).ThenBy(w => w.SubCuota))//solo deberia ser 1
                                {
                                    var valoritem = Json.ListaCronograma.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();
                                    CronogramaDetalleCambioBO cambioTemp = new CronogramaDetalleCambioBO()
                                    {
                                        Id = 0,
                                        IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                        IdCronogramaCabeceraCambio = CambioMora.Id,
                                        NroCuota = valoritem.NroCuota,
                                        NroSubcuota = valoritem.NroSubCuota,
                                        FechaVencimiento = valoritem.FechaVencimiento,
                                        Cuota = valoritem.Cuota,
                                        Mora = valoritem.Mora,
                                        Moneda = valoritem.Moneda,
                                        Version = versionNueva.Value,
                                        Estado = true,
                                        UsuarioCreacion = Json.Usuario,
                                        UsuarioModificacion = Json.Usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now

                                    };
                                    _repCronogramaDetalleCambio.Insert(cambioTemp);

                                    //INSERTO EN LOG
                                    var moraoriginal = cronogramaActual.Where(w => w.NroCuota == detalles.Cuota && w.NroSubCuota == detalles.SubCuota).FirstOrDefault();//deberia existir
                                    string moraoriginaltexto = moraoriginal != null ? moraoriginal.Mora.ToString() : "";

                                    if (listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault() != null)//ya esta en la lista
                                    {
                                        var antiguo = listaLog.Where(w => w.NroCuota == valoritem.NroCuota && w.NroSubCuota == valoritem.NroSubCuota).FirstOrDefault();
                                        antiguo.MensajeSistema = antiguo.MensajeSistema + ", MORA HA VARIADO DE " + moraoriginaltexto + " A " + valoritem.Mora;
                                    }
                                    else//es nuevo
                                    {
                                        CronogramaPagoDetalleModLogFinalBO log = new CronogramaPagoDetalleModLogFinalBO()
                                        {
                                            Id = 0,
                                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                            Fecha = DateTime.Now,
                                            NroCuota = valoritem.NroCuota,
                                            NroSubCuota = valoritem.NroSubCuota,
                                            FechaVencimiento = valoritem.FechaVencimiento,
                                            TotalPagar = valoritem.TotalPagar,
                                            Cuota = valoritem.Cuota,
                                            Mora = valoritem.Mora,
                                            MontoPagado = 0,
                                            Saldo = valoritem.Saldo,
                                            Cancelado = valoritem.Cancelado,
                                            TipoCuota = valoritem.TipoCuota,
                                            Moneda = valoritem.Moneda,
                                            FechaPago = null,
                                            IdFormaPago = null,
                                            FechaPagoBanco = null,
                                            Ultimo = false,
                                            IdDocumentoPago = null,
                                            NroDocumento = null,
                                            MonedaPago = null,
                                            TipoCambio = null,
                                            MensajeSistema = "CUOTA (" + valoritem.NroCuota + "," + valoritem.NroSubCuota + ") SU MORA HA VARIADO DE " + moraoriginaltexto + " A " + valoritem.Mora,
                                            FechaProcesoPago = null,
                                            EstadoPrimerLog = null

                                        };
                                        listaLog.Add(log);

                                    }

                                }
                            }
                        }
                        foreach (var itemlog in listaLog)
                        {
                            itemlog.Version = versionNueva.Value;
                            itemlog.Aprobado = false;
                            itemlog.Estado2 = true;

                            itemlog.Estado = true;
                            itemlog.FechaCreacion = DateTime.Now;
                            itemlog.FechaModificacion = DateTime.Now;
                            itemlog.UsuarioCreacion = Json.Usuario;
                            itemlog.UsuarioModificacion = Json.Usuario;

                            if (itemlog.MensajeSistema.IndexOf("ELIMINADA") > -1)//si es de eliminada
                            {
                                //le cambio el estado2 a 0
                                itemlog.Estado2 = false;
                            }
                            var insertlog = _repCronogramaPagoDetalleModLogFinal.Insert(itemlog);

                        }
                    }
                    scope.Complete();
                }


                //    return true;
                //}
                //var cronogramaactual = _tcrm_CentroCostoService.GetCronogramaFinal(matricula).ToList();
                //var per = _tcrm_CentroCostoService.GetTpersonalsByUserName(User.Identity.Name);
                //var Rpta = _tCronogramaPagosDetalle_FinalService.InsertCambioMoneda(matricula, per[0].id, per[0].nombre_completo, listaCronograma, cronogramaactual);
                //var jsonResult = Json(new { Result = "OK", Records = "Prueba" }, JsonRequestBehavior.AllowGet);
                return Ok(new { Message = "Se Guardo correctamente", IdMatriculaCabeceraSincronizacion });
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: --,Miguel
        /// Fecha: 19/04/2021
        /// Versión: 2.0
        /// <summary>
        /// Guarda el cambio de moneda en todas las tablas respectivas
        /// modificacion : ahora modifica la moneda en la tabla T_CronogramaPago siempre 
        /// </summary>
        /// <returns>Json</returns>


        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarMoraCAdelanto([FromBody] MoraActualizadoDTO Json)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                integraDBContext _integraDbContext = new integraDBContext();

                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDbContext);
                CronogramaDetalleCambioRepositorio _repCronogramaDetalleCambio = new CronogramaDetalleCambioRepositorio(_integraDbContext);
                CronogramaCabeceraCambioRepositorio _repCronogramaCabeceraCambio = new CronogramaCabeceraCambioRepositorio(_integraDbContext);
                CronogramaPagoDetalleModLogFinalRepositorio _repCronogramaPagoDetalleModLogFinal = new CronogramaPagoDetalleModLogFinalRepositorio(_integraDbContext);
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDbContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDbContext);

                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(w => w.CodigoMatricula == Json.Objeto.CodigoMatricula).FirstOrDefault();
                var versionAprobadoTemp = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                var cronogramaActual = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobadoTemp.Version, x => new { x.Id, x.Cancelado, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version, x.FechaDeposito, x.FechaEfectivoDisponible, x.FechaIngresoEnCuenta, x.FechaProcesoPagoReal, x.UsuarioCoordinadorAcademico }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota).ToList();

                var ExisteVersionNoAprobada = _repCronogramaPagoDetalleFinal.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Aprobado == false).ToList();
                if (ExisteVersionNoAprobada.Count > 0)
                {
                    return BadRequest("Existe Versiones sin Aprobar");
                }
                else
                {
                    decimal totalmonto = Json.ListaCronograma.Sum(w => w.Cuota);
                    var versionnueva = cronogramaActual.FirstOrDefault().Version + 1;
                    CronogramaCabeceraCambioBO CambioFraccion = new CronogramaCabeceraCambioBO()
                    {
                        Id = 0,
                        IdCronogramaTipoModificacion = 6,
                        SolicitadoPor = 0,
                        AprobadoPor = 0,
                        Aprobado = true,
                        Cancelado = false,
                        Observacion = "",
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = Json.Usuario,
                        UsuarioModificacion = Json.Usuario
                    };
                    var cambioinsertadofraccion = _repCronogramaCabeceraCambio.Insert(CambioFraccion);

                    for (int i = 0; i < Json.ListaCronograma.Count(); i++)
                    {
                        CronogramaPagoDetalleFinalBO Cuota = new CronogramaPagoDetalleFinalBO()
                        {
                            Id = 0,
                            IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                            NroCuota = Json.ListaCronograma[i].NroCuota,
                            NroSubCuota = Json.ListaCronograma[i].NroSubCuota,
                            FechaVencimiento = Json.ListaCronograma[i].FechaVencimiento,
                            TotalPagar = totalmonto,
                            Cuota = Json.ListaCronograma[i].Cuota,
                            Saldo = totalmonto - Json.ListaCronograma[i].Cuota,
                            Mora = Json.ListaCronograma[i].Mora,
                            Cancelado = Json.ListaCronograma[i].Cancelado,
                            TipoCuota = Json.ListaCronograma[i].TipoCuota,
                            Moneda = Json.ListaCronograma[i].Moneda,
                            TipoCambio = cronogramaActual[i].TipoCambio,
                            Version = versionnueva,
                            FechaPago = cronogramaActual[i].FechaPago,
                            FechaDeposito = cronogramaActual[i].FechaDeposito,
                            IdFormaPago = cronogramaActual[i].IdFormaPago,
                            IdCuenta = cronogramaActual[i].IdCuenta,
                            FechaPagoBanco = cronogramaActual[i].FechaPagoBanco,
                            Enviado = cronogramaActual[i].Enviado,
                            Observaciones = cronogramaActual[i].Observaciones,
                            IdDocumentoPago = cronogramaActual[i].IdDocumentoPago,
                            NroDocumento = cronogramaActual[i].NroDocumento,
                            MonedaPago = cronogramaActual[i].MonedaPago,
                            FechaProcesoPago = cronogramaActual[i].FechaProcesoPago,
                            MontoPagado = cronogramaActual[i].MontoPagado,
                            Aprobado = true,//Se convierte en true cuando aprueba los cambios                          
                            FechaProcesoPagoReal = cronogramaActual[i].FechaProcesoPagoReal,
                            FechaIngresoEnCuenta = cronogramaActual[i].FechaIngresoEnCuenta,
                            FechaEfectivoDisponible = cronogramaActual[i].FechaEfectivoDisponible,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = Json.Usuario,
                            UsuarioModificacion = Json.Usuario,
                            UsuarioCoordinadorAcademico = cronogramaActual[i].UsuarioCoordinadorAcademico
                        };
                        _repCronogramaPagoDetalleFinal.Insert(Cuota);

                        if (Json.Objeto.NroCuota == Json.ListaCronograma[i].NroCuota && Json.Objeto.NroSubCuota == Json.ListaCronograma[i].NroSubCuota)
                        {
                            CronogramaDetalleCambioBO detalleCambio = new CronogramaDetalleCambioBO()
                            {
                                Id = 0,
                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                IdCronogramaCabeceraCambio = CambioFraccion.Id,
                                NroCuota = Json.ListaCronograma[i].NroCuota,
                                NroSubcuota = Json.ListaCronograma[i].NroSubCuota,
                                FechaVencimiento = Json.ListaCronograma[i].FechaVencimiento,
                                Cuota = Json.ListaCronograma[i].Cuota,
                                Mora = Json.ListaCronograma[i].Mora,
                                Moneda = Json.ListaCronograma[i].Moneda,
                                Version = versionnueva.Value,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = Json.Usuario,
                                UsuarioModificacion = Json.Usuario
                            };
                            _repCronogramaDetalleCambio.Insert(detalleCambio);
                        }
                        if (Json.Objeto.Id.ToString().ToUpper() == Json.ListaCronograma[i].Id.ToUpper())
                        {
                            CronogramaDetalleCambioBO detalleCambio = new CronogramaDetalleCambioBO()
                            {
                                Id = 0,
                                IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                IdCronogramaCabeceraCambio = CambioFraccion.Id,
                                NroCuota = Json.ListaCronograma[i].NroCuota,
                                NroSubcuota = Json.ListaCronograma[i].NroSubCuota,
                                FechaVencimiento = Json.ListaCronograma[i].FechaVencimiento,
                                Cuota = Json.ListaCronograma[i].Cuota,
                                Mora = Json.ListaCronograma[i].Mora,
                                Moneda = Json.ListaCronograma[i].Moneda,
                                Version = versionnueva.Value,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = Json.Usuario,
                                UsuarioModificacion = Json.Usuario
                            };
                            _repCronogramaDetalleCambio.Insert(detalleCambio);

                            if (cronogramaActual[i].Cancelado == false)
                            {
                                CronogramaPagoDetalleModLogFinalBO log = new CronogramaPagoDetalleModLogFinalBO()
                                {
                                    Id = 0,
                                    IdMatriculaCabecera = matriculaCabeceraTemp.Id,
                                    Fecha = DateTime.Now,
                                    NroCuota = Json.ListaCronograma[i].NroCuota,
                                    NroSubCuota = Json.ListaCronograma[i].NroSubCuota,
                                    FechaVencimiento = Json.ListaCronograma[i].FechaVencimiento,
                                    TotalPagar = totalmonto,
                                    Cuota = Json.ListaCronograma[i].Cuota,
                                    Mora = Json.ListaCronograma[i].Mora,
                                    MontoPagado = 0,
                                    Saldo = totalmonto - Json.ListaCronograma[i].Cuota,
                                    Cancelado = Json.ListaCronograma[i].Cancelado,
                                    TipoCuota = Json.ListaCronograma[i].TipoCuota,
                                    Moneda = Json.ListaCronograma[i].Moneda,
                                    FechaPago = null,
                                    IdFormaPago = null,
                                    FechaPagoBanco = null,
                                    Ultimo = false,
                                    IdDocumentoPago = null,
                                    NroDocumento = null,
                                    MonedaPago = null,
                                    TipoCambio = null,
                                    MensajeSistema = "CUOTA (" + Json.ListaCronograma[i].NroCuota + "," + Json.ListaCronograma[i].NroSubCuota + ") HA VARIADO DE " + cronogramaActual[i].Cuota + " A " + Json.ListaCronograma[i].Cuota,
                                    FechaProcesoPago = null,
                                    EstadoPrimerLog = null,
                                    Version = versionnueva,
                                    Aprobado = true,
                                    Estado2 = true,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario

                                };
                                var insertLog = _repCronogramaPagoDetalleModLogFinal.Insert(log);
                            }
                        }

                        totalmonto = totalmonto - Json.ListaCronograma[i].Cuota;
                    }
                    return Ok(new { Message = "Se Modifico correctamente" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: --
        /// Fecha: 19/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la mora de adelanto
        /// </summary>
        /// <returns>Json/returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarCambioMonedaCronograma([FromBody] CambioMonedaCronogramaModificadoDTO Json)
        {
            try
            {
                integraDBContext _integraDbContext = new integraDBContext();

                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDbContext);
                CronogramaDetalleCambioRepositorio _repCronogramaDetalleCambio = new CronogramaDetalleCambioRepositorio(_integraDbContext);
                CronogramaCabeceraCambioRepositorio _repCronogramaCabeceraCambio = new CronogramaCabeceraCambioRepositorio(_integraDbContext);
                CronogramaPagoDetalleModLogFinalRepositorio _repCronogramaPagoDetalleModLogFinal = new CronogramaPagoDetalleModLogFinalRepositorio(_integraDbContext);
                CronogramaPagoRepositorio _repCronogramaPago = new CronogramaPagoRepositorio(_integraDbContext);
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDbContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDbContext);


                var matricula = _repMatriculaCabecera.FirstBy(w => w.CodigoMatricula == Json.CodigoMatricula);
                if (!matricula.EstadoMatricula.Equals("matriculado") && !matricula.EstadoMatricula.Equals("pormatricular"))
                {
                    return BadRequest("No se realizo la modificacion debido a que la matricula no se encuentra en estado matriculado o por matricular.");
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(w => w.CodigoMatricula == Json.CodigoMatricula).FirstOrDefault();
                    Json.IdMatriculaCabecera = Convert.ToInt32(matriculaCabeceraTemp.Id);
                    var versionAprobadoTemp = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();

                    var cronogramaActual = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobadoTemp.Version, x => new { x.Id, x.Cancelado, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version, x.FechaDeposito, x.FechaEfectivoDisponible, x.FechaIngresoEnCuenta, x.FechaProcesoPagoReal, x.UsuarioCoordinadorAcademico }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota).ToList();
                    //public bool InsertCambioMoneda(string matricula, int idPer, string NombreC, List<Cronograma_FinalModificadoDTO> listaCronograma, List<Cronograma_FinalDTO> cronogramaactual)
                    //{
                    decimal totalMonto = Json.ListaCronograma.Sum(x => x.Cuota);//cuota era monto   
                    /*int*/
                    var versionNueva = cronogramaActual.FirstOrDefault().Version + 1;

                    //tCambiosDTO cambiofraccion = new tCambiosDTO()
                    CronogramaCabeceraCambioBO cronogramaCabeceraCambio = new CronogramaCabeceraCambioBO()
                    {
                        IdCronogramaTipoModificacion = ValorEstatico.IdCronogramaTipoModificacionCuota,
                        SolicitadoPor = Json.IdPersonal,
                        AprobadoPor = Json.IdPersonal,
                        Aprobado = true,
                        Cancelado = false,
                        Observacion = "",
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        UsuarioCreacion = Json.UsuarioNombre,
                        FechaModificacion = DateTime.Now,
                        UsuarioModificacion = Json.UsuarioNombre
                    };

                    //var cambioinsertadofraccion = _tCambiosService.Insert(cambiofraccion);
                    var cambioinsertadofraccion = _repCronogramaCabeceraCambio.Insert(cronogramaCabeceraCambio);

                    for (int i = 0; i < Json.ListaCronograma.Count(); i++)
                    {
                        CronogramaPagoDetalleFinalBO cuota = new CronogramaPagoDetalleFinalBO()
                        {
                            //  tCronogramaPagosDetalle_FinalDTO cuota = new tCronogramaPagosDetalle_FinalDTO()
                            //{
                            //Id = "00000000-0000-0000-0000-000000000000",
                            IdMatriculaCabecera = Json.IdMatriculaCabecera,
                            NroCuota = Json.ListaCronograma[i].NroCuota,
                            NroSubCuota = Json.ListaCronograma[i].NroSubCuota,
                            FechaVencimiento = DateTime.ParseExact(Json.ListaCronograma[i].FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss"), "d/MM/yyyy HH:mm:ss", null),
                            TotalPagar = totalMonto,
                            Cuota = Json.ListaCronograma[i].Cuota,
                            Saldo = totalMonto - Json.ListaCronograma[i].Cuota,
                            Mora = Json.ListaCronograma[i].Mora,
                            Cancelado = Json.ListaCronograma[i].Cancelado,
                            TipoCuota = Json.ListaCronograma[i].TipoCuota,
                            Moneda = Json.ListaCronograma[i].Moneda,
                            TipoCambio = cronogramaActual[i].Cancelado == true ? Json.ListaCronograma[i].TipoCambio : Json.ListaCronograma[i].TipoCambio,
                            Version = versionNueva,
                            FechaPago = cronogramaActual[i].FechaPago != null ? cronogramaActual[i].FechaPago : null,
                            FechaDeposito = cronogramaActual[i].FechaDeposito != null ? cronogramaActual[i].FechaDeposito : null,
                            IdFormaPago = cronogramaActual[i].IdFormaPago,
                            IdCuenta = cronogramaActual[i].IdCuenta,
                            FechaPagoBanco = cronogramaActual[i].FechaPagoBanco != null ? cronogramaActual[i].FechaPagoBanco : null,
                            Enviado = cronogramaActual[i].Enviado,
                            Observaciones = cronogramaActual[i].Observaciones,
                            IdDocumentoPago = cronogramaActual[i].IdDocumentoPago,
                            NroDocumento = cronogramaActual[i].NroDocumento,
                            MonedaPago = cronogramaActual[i].MonedaPago,
                            FechaProcesoPago = cronogramaActual[i].FechaProcesoPago != null ? cronogramaActual[i].FechaProcesoPago : null,
                            MontoPagado = cronogramaActual[i].MontoPagado,
                            FechaEfectivoDisponible = cronogramaActual[i].FechaEfectivoDisponible,
                            FechaIngresoEnCuenta = cronogramaActual[i].FechaIngresoEnCuenta,
                            FechaProcesoPagoReal = cronogramaActual[i].FechaProcesoPagoReal,
                            //corregir *********** IdCambio = "00000000-0000-0000-0000-000000000000",
                            Aprobado = true,//Se convierte en true cuando aprueba los cambios
                                            //};
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = Json.UsuarioNombre,
                            FechaModificacion = DateTime.Now,
                            UsuarioModificacion = Json.UsuarioNombre,
                            UsuarioCoordinadorAcademico = cronogramaActual[i].UsuarioCoordinadorAcademico
                        };
                        _repCronogramaPagoDetalleFinal.Insert(cuota);

                        //tCronogramaPagosDetalle_mod_CambiosDTO DettalleCambio = new tCronogramaPagosDetalle_mod_CambiosDTO()
                        CronogramaDetalleCambioBO detalleCambio = new CronogramaDetalleCambioBO()
                        {
                            //Id = "00000000-0000-0000-0000-000000000000",
                            IdMatriculaCabecera = Json.IdMatriculaCabecera.Value,//??
                            IdCronogramaCabeceraCambio = cronogramaCabeceraCambio.Id,
                            NroCuota = Json.ListaCronograma[i].NroCuota,
                            NroSubcuota = Json.ListaCronograma[i].NroSubCuota,
                            FechaVencimiento = DateTime.ParseExact(Json.ListaCronograma[i].FechaVencimiento.ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", null),
                            Cuota = Json.ListaCronograma[i].Cuota,
                            Mora = Json.ListaCronograma[i].Mora,
                            TipoCambio = Json.ListaCronograma[i].TipoCambio,
                            Moneda = Json.ListaCronograma[i].Moneda,
                            Version = versionNueva.Value, //??   
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = Json.UsuarioNombre,
                            FechaModificacion = DateTime.Now,
                            UsuarioModificacion = Json.UsuarioNombre
                        };
                        _repCronogramaDetalleCambio.Insert(detalleCambio);
                        //_tCronogramaPagosDetalle_mod_CambiosService.Insert(DettalleCambio);


                        if (cronogramaActual[i].Cancelado == false)
                        {
                            CronogramaPagoDetalleModLogFinalBO log = new CronogramaPagoDetalleModLogFinalBO()
                            {
                                //tCronogramaPagosDetalle_mod_log_FinalDTO log = new tCronogramaPagosDetalle_mod_log_FinalDTO()
                                //Id = "00000000-0000-0000-0000-000000000000",
                                IdMatriculaCabecera = Json.IdMatriculaCabecera,
                                Fecha = DateTime.Now,
                                NroCuota = Json.ListaCronograma[i].NroCuota,
                                NroSubCuota = Json.ListaCronograma[i].NroSubCuota,
                                FechaVencimiento = Json.ListaCronograma[i].FechaVencimiento,
                                TotalPagar = totalMonto,
                                Cuota = Json.ListaCronograma[i].Cuota,
                                Mora = Json.ListaCronograma[i].Mora,
                                MontoPagado = 0,
                                Saldo = totalMonto - Json.ListaCronograma[i].Cuota,
                                Cancelado = Json.ListaCronograma[i].Cancelado,
                                TipoCuota = Json.ListaCronograma[i].TipoCuota,
                                Moneda = Json.ListaCronograma[i].Moneda,
                                FechaPago = null,
                                IdFormaPago = null,
                                FechaPagoBanco = null,
                                Ultimo = false,
                                IdDocumentoPago = null,
                                NroDocumento = null,
                                MonedaPago = null,
                                TipoCambio = null,
                                MensajeSistema = "CUOTA (" + Json.ListaCronograma[i].NroCuota + "," + Json.ListaCronograma[i].NroSubCuota + ") HA VARIADO DE " + cronogramaActual[i].Cuota + " A " + Json.ListaCronograma[i].Cuota,
                                FechaProcesoPago = null,
                                EstadoPrimerLog = null,
                                Version = versionNueva,
                                Aprobado = true,
                                Estado2 = true,

                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                UsuarioCreacion = Json.UsuarioNombre,
                                FechaModificacion = DateTime.Now,
                                UsuarioModificacion = Json.UsuarioNombre
                            };

                            _repCronogramaPagoDetalleModLogFinal.Insert(log);


                            //var insertlog = _tCronogramaPagosDetalle_mod_log_FinalService.Insert(log);GuardarCambioMonedaCronograma
                        }
                        if (Json.ListaCronograma[i].NroCuota == 1 && Json.ListaCronograma[i].NroSubCuota == 1)
                        {

                            string webMoneda = "0";
                            if (Json.ListaCronograma[i].Moneda == "soles")
                            {
                                webMoneda = "0";
                            }
                            else if (Json.ListaCronograma[i].Moneda == "dolares")
                            {
                                webMoneda = "1";
                            }
                            else if (Json.ListaCronograma[i].Moneda == "colombianos")
                            {
                                webMoneda = "2";
                            }
                            else if (Json.ListaCronograma[i].Moneda == "bolivianos")
                            {
                                webMoneda = "3";
                            }
                            else if (Json.ListaCronograma[i].Moneda == "pesos mexicanos")
                            {
                                webMoneda = "4";
                            }

                            CronogramaPagoBO cronogramaPago;
                            cronogramaPago = _repCronogramaPago.FirstBy(x => x.IdMatriculaCabecera == Json.IdMatriculaCabecera);

                            //Id = "00000000-0000-0000-0000-000000000000",
                            cronogramaPago.Moneda = Json.ListaCronograma[i].Moneda;
                            cronogramaPago.TipoCambio = decimal.ToDouble(Json.ListaCronograma[i].TipoCambio);
                            cronogramaPago.TotalPagar = decimal.ToDouble(totalMonto);
                            cronogramaPago.CuotaInicial = Json.ListaCronograma[i].Cuota;
                            cronogramaPago.WebMoneda = webMoneda;
                            cronogramaPago.WebTipoCambio = decimal.ToDouble(Json.ListaCronograma[i].TipoCambio);
                            cronogramaPago.WebTotalPagar = decimal.ToDouble(totalMonto);
                            cronogramaPago.WebTotalPagarConv = decimal.ToDouble(totalMonto);
                            cronogramaPago.Estado = true;
                            cronogramaPago.FechaModificacion = DateTime.Now;
                            cronogramaPago.UsuarioModificacion = Json.UsuarioNombre;


                            _repCronogramaPago.Update(cronogramaPago);
                        }
                        totalMonto = totalMonto - Json.ListaCronograma[i].Cuota;
                    }

                    scope.Complete();
                }
                //    return true;
                //}
                //var cronogramaactual = _tcrm_CentroCostoService.GetCronogramaFinal(matricula).ToList();
                //var per = _tcrm_CentroCostoService.GetTpersonalsByUserName(User.Identity.Name);
                //var Rpta = _tCronogramaPagosDetalle_FinalService.InsertCambioMoneda(matricula, per[0].id, per[0].nombre_completo, listaCronograma, cronogramaactual);
                //var jsonResult = Json(new { Result = "OK", Records = "Prueba" }, JsonRequestBehavior.AllowGet);
                return Ok(true);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
