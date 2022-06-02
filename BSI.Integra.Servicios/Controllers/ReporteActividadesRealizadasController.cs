using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteActividadesRealizadas")]
    [ApiController]
    public class ReporteActividadesRealizadasController : ControllerBase
    {
        /// TipoFuncion: GET
        /// Autor: ,Edgar S.
        /// Fecha: 01/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información para combos de interfaz
        /// </summary>
        /// <returns> objeto Agrupado <returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombos(int IdPersonal)
        {
            try
               {
                PersonalRepositorio repPersonal = new PersonalRepositorio();
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio();
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio();
                ProbabilidadRegistroPwRepositorio _repProbabilidadRegistro = new ProbabilidadRegistroPwRepositorio();
                TipoCategoriaOrigenRepositorio _repTipoCategoriaOrigen = new TipoCategoriaOrigenRepositorio();

                EstadoOcurrenciaRepositorio _repEstadoOcurrencia = new EstadoOcurrenciaRepositorio();

                FiltroReporteActividadRealizadaDTO filtros = new FiltroReporteActividadRealizadaDTO();

                List<PersonalAsignadoDTO> asistentes = repPersonal.GetPersonalAsignadoVentas(IdPersonal);



                filtros.EstadoOcurrencia = _repEstadoOcurrencia.ObtenerEstadoOcurrenciasParaFiltro();
                filtros.FaseOportunidad = _repFaseOportunidad.ObtenerTodoFiltro();
                filtros.TipoDato = _repTipoDato.ObtenerFiltro();
                filtros.Probabilidad = _repProbabilidadRegistro.ObtenerTodoFiltro();
                filtros.CategoriaOrigen = _repTipoCategoriaOrigen.ObtenerTodoFiltro();
                filtros.Asesores = repPersonal.GetPersonalAsignadoVentas(IdPersonal);

                filtros.AsistentesActivos = asistentes.Where(w => w.Activo == true).ToList();
                //todos
                filtros.AsistentesTotales = asistentes;
                //inactivo
                filtros.AsistentesInactivos = asistentes.Where(w => w.Activo == false).ToList();
                return Ok(filtros);
         
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        /// TipoFuncion: GET
        /// Autor: ,Edgar S.
        /// 
        /// Fecha: 01/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información para combos de interfaz Operaciones y Asesores por IdPersonal
        /// </summary>
        /// <returns> objeto Agrupado <returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosOperaciones(int IdPersonal)
        {
            try
               {
                PersonalRepositorio repPersonal = new PersonalRepositorio();
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio();
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio();
                ProbabilidadRegistroPwRepositorio _repProbabilidadRegistro = new ProbabilidadRegistroPwRepositorio();
                TipoCategoriaOrigenRepositorio _repTipoCategoriaOrigen = new TipoCategoriaOrigenRepositorio();

                EstadoOcurrenciaRepositorio _repEstadoOcurrencia = new EstadoOcurrenciaRepositorio();

                FiltroReporteActividadRealizadaDTO filtros = new FiltroReporteActividadRealizadaDTO();

                

                filtros.EstadoOcurrencia = _repEstadoOcurrencia.ObtenerEstadoOcurrenciasParaFiltro();
                filtros.FaseOportunidad = _repFaseOportunidad.ObtenerTodoFiltro();
                filtros.TipoDato = _repTipoDato.ObtenerFiltro();
                filtros.Probabilidad = _repProbabilidadRegistro.ObtenerTodoFiltro();
                filtros.CategoriaOrigen = _repTipoCategoriaOrigen.ObtenerTodoFiltro();

                List<PersonalAsignadoDTO> asistentes = repPersonal.ObtenerPersonalAsignadoOperacionesTotal(IdPersonal);
                //activos
                filtros.AsistentesActivos = asistentes.Where(w => w.Activo==true).ToList();
                //todos
                filtros.AsistentesTotales = asistentes;
                //inactivo
                filtros.AsistentesInactivos = asistentes.Where(w => w.Activo == false).ToList();


                return Ok(filtros);
         
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        /// TipoFuncion: POST
        /// Autor: ,Edgar S.
        /// Fecha: 01/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte Actividades Realizadas Operaciones
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<ProcesadoDataActividadesRealizadasDTO> <returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteOperaciones([FromBody] ReporteActividadesRealizadasFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();
                var result = reporte.ReporteActividadesRealizadasOperaciones(Filtros);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: ,Edgar S.
        /// Fecha: 24/02/2021
        /// Versión: 1.1
        /// <summary>
        /// Genera Reporte de Actividades Realizadas
        /// </summary>
        /// <returns> Información de Actividades Realizadas: List<ProcesadoDataActividadesRealizadasDTO> <returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteActividadesRealizadasFiltrosDTO  Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();
                var result = reporte.ReporteActividadesRealizadas(Filtros);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: ,Edgar S.
        /// Fecha: 01/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida un patrón de palabras
        /// </summary>
        /// <returns> Confirmación de Patrón: Bool <returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult WordPattern()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                bool flag = true;
                string pattern = "abba";
                string str = "dog cat cat dog";
                string[] arrStr = str.Split(" ");
                string temporalStr = "";
                string patternStr = "";
                if (pattern.Length == arrStr.Length)
                {
                    for (int i = 0; i < arrStr.Length; i++)
                    {
                        string letter = pattern.Substring(i, 1);
                        if (!dic.ContainsKey(letter))
                        {
                            dic.Add(letter, arrStr[i]);
                        }
                        temporalStr = arrStr[i];
                        patternStr = dic[letter];
                        if (temporalStr != patternStr)
                        {
                            flag = false;
                            break;
                        }


                    }
                }
                else
                {
                    flag = false;
                }
               
                return Ok(flag);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
