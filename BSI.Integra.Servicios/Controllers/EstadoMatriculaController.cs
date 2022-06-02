using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/EstadoMatricula")]
    public class EstadoMatriculaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public EstadoMatriculaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarEstadoMatricula([FromBody] TCRM_EstadoMatriculaInsertarDTO Json)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string _Nombre = Json.EstadoMatricula;
                bool _Estado = Json.Estado;
                string _UsuarioCreacion = Json.UsuarioCreacion;
                string _UsuarioModificacion = Json.UsuarioModificacion;
                DateTime _FechaCreacion = DateTime.Now;
                DateTime _FechaModificacion = DateTime.Now;
                string _IdEstadoMatricula = ListIntToString(Json.IdSubEstados);
                try
                {
                    EstadoMatriculaRepositorio reportes = new EstadoMatriculaRepositorio();
                    var listRpta = reportes.InsertarEstadoSubestado(_Nombre, _Estado, _UsuarioCreacion, _UsuarioModificacion, _FechaCreacion, _FechaModificacion, _IdEstadoMatricula);

                    return Ok(new { Records = listRpta });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        private string ListIntToString(IList<int> datos)
        {
            if (datos == null)
                datos = new List<int>();
            int NumberElements = datos.Count;
            string rptaCadena = string.Empty;
            for (int i = 0; i < NumberElements - 1; i++)
                rptaCadena += Convert.ToString(datos[i]) + ",";
            if (NumberElements > 0)
                rptaCadena += Convert.ToString(datos[NumberElements - 1]);
            return rptaCadena;
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarEstadoMatricula([FromBody] TCRM_EstadoMatriculaInsertarDTO Json)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int _Id = Json.Id;
                string _Nombre = Json.EstadoMatricula;
                string _UsuarioModificacion = Json.UsuarioModificacion;
                DateTime _FechaModificacion = DateTime.Now;
                string _IdEstadoMatricula = ListIntToString(Json.IdSubEstados);
                try
                {
                    EstadoMatriculaRepositorio reportes = new EstadoMatriculaRepositorio();
                    var listRpta = reportes.EditarEstado(_Id, _Nombre, _UsuarioModificacion, _FechaModificacion, _IdEstadoMatricula);

                    return Ok(new { Records = listRpta });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult EliminarEstadoMatricula([FromBody] EstadoMatriculaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                EstadoMatriculaRepositorio repEstadoMatricula = new EstadoMatriculaRepositorio();
                EstadoMatriculaBO criterio = new EstadoMatriculaBO();
                bool result = false;
                if (repEstadoMatricula.Exist(Json.Id))
                {
                    result = repEstadoMatricula.Delete(Json.Id, Json.UsuarioModificacion);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosSubEstadoMatricula()
        {

            try
            {
                EstadoMatriculaRepositorio repEstadoMatricula = new EstadoMatriculaRepositorio();
                var cmbTP = new { SubEstadoMatricula = repEstadoMatricula.ObtenerComboSubEstadoMatricula() };
                return Ok(cmbTP);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerEstadosMatricula()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EstadoMatriculaRepositorio repEstadoMatricula = new EstadoMatriculaRepositorio();
                var lista = repEstadoMatricula.ObtenerEstadosMatricula();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerSubEstadosMatricula()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EstadoMatriculaRepositorio repEstadoMatricula = new EstadoMatriculaRepositorio();
                var lista = repEstadoMatricula.ObtenerSubEstadoMatricula();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerSubEstadosMatriculaIndividual([FromBody] TCRM_SubEstadoMatriculaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EstadoMatriculaRepositorio repEstadoMatricula = new EstadoMatriculaRepositorio();
                var lista = repEstadoMatricula.ObtenerSubEstadoIndividual(Json.IdEstadoMatricula);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// FUNCION QUE DEVUELVE LOS SUBESTADOS DE UN ID DE MATRICULA ESTADO
        /// </summary>
        /// <param name="IdEstadoMatricula"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult FiltroObtenerSubEstadosMatricula(List<int> IdEstadoMatricula)//LPPG
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<TCRM_SubEstadoMatriculaDTO> listEstadosMatricula = new List<TCRM_SubEstadoMatriculaDTO>();
                foreach (var item in IdEstadoMatricula)
                {
                    EstadoMatriculaRepositorio repEstadoMatricula = new EstadoMatriculaRepositorio();
                    var lista = repEstadoMatricula.ObtenerFiltroSubEstadoMatricula(item);
                    listEstadosMatricula.AddRange(lista);
                }


                if (IdEstadoMatricula.Contains(1))
                {

                    TCRM_SubEstadoMatriculaDTO pagoaldia = new TCRM_SubEstadoMatriculaDTO()
                    {
                        Id = 13,
                        IdAgendaTab = 16,
                        Nombre = "Pago al dia"
                    };
                    TCRM_SubEstadoMatriculaDTO pagoatrasado = new TCRM_SubEstadoMatriculaDTO()
                    {
                        Id = 14,
                        IdAgendaTab = 15,
                        Nombre = "Pago atrasado"
                    };
                    TCRM_SubEstadoMatriculaDTO seguimientoacademico = new TCRM_SubEstadoMatriculaDTO()
                    {
                        Id = 15,
                        IdAgendaTab = 17,
                        Nombre = "Seguimiento academico"
                    };
                    listEstadosMatricula.Add(pagoaldia);
                    listEstadosMatricula.Add(pagoatrasado);
                    listEstadosMatricula.Add(seguimientoacademico);
                }

                return Ok(listEstadosMatricula);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// FUNCION QUE DEVUELVE LOS ESTADOS DE MATRICULA
        /// Agenda-BandejaCorreosOperaciones.js
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerFiltroEstadosMatricula()//LPPG
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EstadoMatriculaRepositorio repEstadoMatricula = new EstadoMatriculaRepositorio();
                var lista = repEstadoMatricula.ObtenerEstadosMatricula();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}