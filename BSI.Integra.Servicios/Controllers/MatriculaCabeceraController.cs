using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MatriculaCabecera
    /// Autor: Jose Villena
    /// Fecha: 01/05/2021        
    /// <summary>
    /// Controlador de Matricula Cabecera
    /// </summary>
    [Route("api/MatriculaCabecera")]
    public class MatriculaCabeceraController : Controller
    {
        /// TipoFuncion: POST
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registro Matricula Cabecera por Codigo de Matricula
        /// </summary>
        /// <returns> Retorna Objeto recibido filtrado: FiltroCodigoMatriculaDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoMatricula([FromBody] FiltroCodigoMatriculaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                return Ok(_repMatriculaCabecera.GetBy(x => x.CodigoMatricula.Contains(Filtro.CodigoMatricula) && (x.EstadoMatricula.Equals("matriculado") || x.EstadoMatricula.Equals("pormatricular")), x => new { Id = x.CodigoMatricula }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda Fecha de Compromiso 
        /// </summary>
        /// <returns> Retorna Objeto recibido: MatriculaCabeceraFiltroDTO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GuardarFechaCompromiso([FromBody]MatriculaCabeceraFiltroDTO FechaCompromiso)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            try
            {
                CompromisoAlumnoRepositorio _repCompromisoAlumno = new CompromisoAlumnoRepositorio();
                CompromisoAlumnoBO compromisoNuevo = new CompromisoAlumnoBO();
                var fecha = Convert.ToDateTime(FechaCompromiso.FechaCompromiso);
                var fechaActualizada = fecha.AddHours(-5);
                DateTime fechaActual = DateTime.Now;
                compromisoNuevo.IdCronogramaPagoDetalleFinal = FechaCompromiso.Id;
                compromisoNuevo.FechaCompromiso = fechaActualizada;
                compromisoNuevo.FechaGeneracionCompromiso = fechaActual;
                compromisoNuevo.Monto = FechaCompromiso.MontoCompromiso;
                compromisoNuevo.IdMoneda = FechaCompromiso.IdMoneda;
                compromisoNuevo.Version = FechaCompromiso.Version;
                compromisoNuevo.Estado = true;
                compromisoNuevo.UsuarioCreacion = FechaCompromiso.Usuario;
                compromisoNuevo.UsuarioModificacion = FechaCompromiso.Usuario;
                compromisoNuevo.FechaCreacion = fechaActual;
                compromisoNuevo.FechaModificacion = fechaActual;
                FechaCompromiso.Flag = _repCompromisoAlumno.Insert(compromisoNuevo);

                return Ok(FechaCompromiso);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cronograma Detalle Pago
        /// </summary>
        /// <returns> Retorna Objeto recibido: CronogramaPagoDetalleFinal</returns>
        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCronogramaDetallePagoFinal(string CodigoMatricula)
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
                CronogramaPagoDetalleOriginalRepositorio _repCronogramaPagoDetalleOriginal = new CronogramaPagoDetalleOriginalRepositorio();
                var cronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Version == versionAprobada.Version, x => new { x.Id, x.Cancelado, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version, x.FechaDeposito }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota);
                return Ok( cronogramaPagoDetalleFinal );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener versiones de Fecha de Compromiso
        /// </summary>
        /// <returns> Retorna Objeto recibido: ResultadoFechaCompromiso</returns>
        [Route("[action]/{IdCuota}")]
        [HttpGet]
        public ActionResult ObtenerVersionesFechaCompromiso(int IdCuota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();

                var lista = _repCronogramaPagoDetalleFinal.ObtenerVersionesFechaCompromiso(IdCuota);

                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Codigo de matricula y programa por idAlumno
        /// </summary>
        /// <returns> Retorna Objeto recibido: CodigoMatriculaPEspecificoDTO</returns>
        [Route("[action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerCodigoMatriculaPEspecificoPorAlumno(int IdAlumno)
        {
            try
            {
                    MatriculaCabeceraRepositorio _repCabeceraRepositorio = new MatriculaCabeceraRepositorio();
                var rpta = _repCabeceraRepositorio.ObtenerCodigoMatriculaPEspecificoPorAlumno(IdAlumno);
                var rpta2 = rpta.Select(w => new { w.CodigoMatricula, PEspecifico = w.PEspecifico +" - "+w.CodigoMatricula, w.VersionPrograma });
                    return Ok(rpta2);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Devuelve los identificadores importantes por matricula de alumno
        /// </summary>
        /// <returns> Retorna Objeto recibido: IdentificadorMatriculaComboDTO</returns>
        [Route("[action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerIdentificadoresMatriculaComboPorAlumno(int IdAlumno)
        {
            try
            {
                MatriculaCabeceraRepositorio _repCabeceraRepositorio = new MatriculaCabeceraRepositorio();
                var rpta = _repCabeceraRepositorio.ObtenerIdentificadoresMatriculaComboPorAlumno(IdAlumno);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Estado Matriculado
        /// </summary>
        /// <returns> Retorna Objeto recibido: List</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEstadoMatriculado()
        {
            try
            {
                    
                MatriculaCabeceraRepositorio _repCabeceraRepositorio = new MatriculaCabeceraRepositorio();
                var rpta = _repCabeceraRepositorio.ObtenerEstadoMatriculado();                
                return Ok(rpta);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener observacion de la matricula
        /// </summary>
        /// <returns> Retorna Objeto recibido: List</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerObservacionMatricula()
        {
            try
            {

                MatriculaCabeceraRepositorio _repCabeceraRepositorio = new MatriculaCabeceraRepositorio();
                var rpta = _repCabeceraRepositorio.ObtenerObservacionMatricula();
                return Ok(rpta);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener subestado matricula
        /// </summary>
        /// <returns> Retorna Objeto recibido: SubEstadoMatriculaFiltroDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerSubEstadoMatricula()
        {
            try
            {
                    
                MatriculaCabeceraRepositorio _repCabeceraRepositorio = new MatriculaCabeceraRepositorio();
                var rpta = _repCabeceraRepositorio.ObtenerSubEstadoMatricula();                
                return Ok(rpta);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Codigo de matricula por Oportunidad
        /// </summary>
        /// <returns> Retorna Objeto recibido: CodigoMatricula</returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerCodigoMatriculaPorOportunidad(int IdOportunidad)
        {
            try
            {
                    
                MatriculaCabeceraRepositorio _repCabeceraRepositorio = new MatriculaCabeceraRepositorio();
                var rpta = _repCabeceraRepositorio.ObtenerCodigoPorIdOportunidad(IdOportunidad);                
                return Ok(rpta);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Lisbeth Ortogorin
        /// Fecha: 10/02/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Estado Matricula In House
        /// </summary>
        /// <returns> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerListaInHouse()
        {
            try
            {

                MatriculaCabeceraRepositorio _repCabeceraRepositorio = new MatriculaCabeceraRepositorio();

                var matricula = _repCabeceraRepositorio.ObtenerListaInHouse();
                return Ok(matricula);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Lisbeth Ortogorin
        /// Fecha: 10/02/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Estado Matricula In House
        /// </summary>
        /// <returns> </returns>
        [Route("[action]/{IdMatriculaCabecera}/{EsInHouse}")]
        [HttpGet]
        public ActionResult ActualizarEstadoInHouseMatriculado(int IdMatriculaCabecera, int EsInHouse)
        {
            try
            {

                MatriculaCabeceraRepositorio _repCabeceraRepositorio = new MatriculaCabeceraRepositorio();

                var matricula = _repCabeceraRepositorio.ActualizarEstadoInHouseMatricula(IdMatriculaCabecera, EsInHouse);
                return Ok(matricula);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jose Villena
        /// Fecha: 01/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Estado Matricula
        /// </summary>
        /// <returns> Retorna Objeto recibido: MatriculaCabeceraBO</returns>
        [Route("[action]/{IdMatriculaCabecera}/{IdEstadoMatriculado}/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ActualizarEstadoMatriculado(int IdMatriculaCabecera, int IdEstadoMatriculado, string CodigoMatricula)
        {
            try
            {
                    
                MatriculaCabeceraRepositorio _repCabeceraRepositorio = new MatriculaCabeceraRepositorio();
                MatriculaCabeceraBO matriculaCabecera = new MatriculaCabeceraBO();

                var matricula = _repCabeceraRepositorio.ActualizarEstadoMatricula(IdMatriculaCabecera,IdEstadoMatriculado,CodigoMatricula);
                return Ok(matricula);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerComboCursosMoodlePorMatricula(int IdMatriculaCabecera)
        {
            try
            {
                MoodleCronogramaEvaluacionRepositorio repoCronograma = new MoodleCronogramaEvaluacionRepositorio();

                var rpta = repoCronograma.ObtenerComboCursosMoodlePorMatricula(IdMatriculaCabecera);
                
                //selecciona el ultimo curso que tiene actividad pendiente o sino el ultimo curso
                int? IdCursoSeleccionado =
                    repoCronograma.ObtenerIdCursoMoodlePrimeraActividadPendiente(IdMatriculaCabecera);
                if (IdCursoSeleccionado == null)
                {
                    IdCursoSeleccionado = (rpta != null && rpta.Any())
                        ? rpta.OrderByDescending(o => o.NombreCurso).FirstOrDefault().IdCursoMoodle
                        : IdCursoSeleccionado;
                }

                return Ok(new {ComboCursos = rpta.OrderBy(o => o.NombreCurso), IdCursoSeleccionado = IdCursoSeleccionado });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]/{IdPespecifico}/{Grupo}")]
        [HttpGet]
        public ActionResult ObtenerListaPerfilAlumno(int IdPespecifico, int Grupo)
        {
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();                

                return Ok(_repMatriculaCabecera.ListadoPerfilAlumno(IdPespecifico, Grupo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

