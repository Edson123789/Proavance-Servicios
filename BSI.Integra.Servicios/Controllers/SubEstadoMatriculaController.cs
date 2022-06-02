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
using Google.Api.Ads.AdWords.v201809;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/SubEstadoMatricula")]
    public class SubEstadoMatriculaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public SubEstadoMatriculaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarSubEstadoMatricula([FromBody] TCRM_SubEstadoMatriculaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                string _Nombre = Json.Nombre;
                bool _Estado = true;
                int _IdEstadoMatricula = 19;// 37 o 19 es el id de la matricula generica(no debe ser borrado)
                string _UsuarioCreacion = Json.UsuarioCreacion;
                string _UsuarioModificacion = Json.UsuarioModificacion;
                DateTime _FechaCreacion = DateTime.Now;
                DateTime _FechaModificacion = DateTime.Now;

                //nuevos valores
                int _IdOpcionAvaceAcademico = 0;//Json.IdOpcionAvanceAcademico.Value;
                int _ValorAvanceAcademico1 = 0;//Json.ValorAvanceAcademico1.Value;
                int _ValorAvanceAcademico2 = 0;
                if (_IdOpcionAvaceAcademico == 4)
                {
                    _ValorAvanceAcademico2 = Json.ValorAvanceAcademico2.Value;
                }
                string _IdEstadoPago = Json.IdEstadoPago;
                int _IdOpcionNotaPromedio = 0;// Json.IdOpcionNotaPromedio.Value;
                int _ValorNotaPromedio1 = 0;//Json.ValorNotaPromedio1.Value;
                int _ValorNotaPromedio2 = 0;
                if (_IdOpcionNotaPromedio == 4)
                {
                    _ValorNotaPromedio2 = Json.ValorNotaPromedio2.Value;
                }
                int _TieneDeuda = Json.TieneDeuda.Value == true ? 1 : 0;
                int _ProyectoFinal = 0;// Json.ProyectoFinal.Value == true ? 1 : 0;
                int _RequiereVerificacionInformacion = Json.RequiereVerificacionInformacion.Value == true ? 1 : 0;

                try
                {
                    EstadoMatriculaRepositorio reportes = new EstadoMatriculaRepositorio();
                    var listRpta = reportes.InsertarSubEstado(_Nombre, _Estado, _UsuarioCreacion, _UsuarioModificacion, _FechaCreacion, _FechaModificacion, _IdEstadoMatricula,
                        _IdOpcionAvaceAcademico, _ValorAvanceAcademico1, _ValorAvanceAcademico2, _IdEstadoPago, _IdOpcionNotaPromedio, _ValorNotaPromedio1, _ValorNotaPromedio2, _TieneDeuda, _ProyectoFinal, _RequiereVerificacionInformacion);

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
        [HttpPut]
        public IActionResult ActualizarSubEstadoMatricula([FromBody] TCRM_SubEstadoMatriculaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubEstadoMatriculaDTO result;
                EstadoMatriculaRepositorio repEstadoMatricula = new EstadoMatriculaRepositorio();
                using (TransactionScope scope = new TransactionScope())
                {

                    //nuevos valores
                    int _IdOpcionAvaceAcademico = 0;//Json.IdOpcionAvanceAcademico.Value;
                    int _ValorAvanceAcademico1 = 0;// Json.ValorAvanceAcademico1.Value;
                    int _ValorAvanceAcademico2 = 0;
                    if (_IdOpcionAvaceAcademico == 4)
                    {
                        _ValorAvanceAcademico2 = 0;// Json.ValorAvanceAcademico2.Value;
                    }
                    string _IdEstadoPago = Json.IdEstadoPago;
                    int _IdOpcionNotaPromedio = 0;// Json.IdOpcionNotaPromedio.Value;
                    int _ValorNotaPromedio1 = 0;// Json.ValorNotaPromedio1.Value;
                    int _ValorNotaPromedio2 = 0;
                    if (_IdOpcionNotaPromedio == 4)
                    {
                        _ValorNotaPromedio2 = 0;// Json.ValorNotaPromedio2.Value;
                    }
                    int _TieneDeuda = Json.TieneDeuda.Value == true ? 1 : 0;
                    int _ProyectoFinal = 0;// Json.ProyectoFinal.Value == true ? 1 : 0;
                    int _RequiereVerificacionInformacion = Json.RequiereVerificacionInformacion.Value == true ? 1 : 0;


                    //if (repEstadoMatricula.Exist(Json.Id))
                    //{
                    result = repEstadoMatricula.EditarSubEstado(Json.Nombre, Json.Id, Json.UsuarioModificacion, DateTime.Now,
                        _IdOpcionAvaceAcademico, _ValorAvanceAcademico1, _ValorAvanceAcademico2, _IdEstadoPago, _IdOpcionNotaPromedio, _ValorNotaPromedio1, _ValorNotaPromedio2, _TieneDeuda, _ProyectoFinal, _RequiereVerificacionInformacion);
                    //}
                    scope.Complete();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpDelete]
        public ActionResult EliminarSubEstadoMatricula([FromBody] EstadoMatriculaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubEstadoMatriculaDTO result;
                EstadoMatriculaRepositorio repEstadoMatricula = new EstadoMatriculaRepositorio();
                using (TransactionScope scope = new TransactionScope())
                {
                    //if (repEstadoMatricula.Exist(Json.Id))
                    //{
                    result = repEstadoMatricula.EliminarSubEstado(Json.Id);
                    //}
                    scope.Complete();
                }
                return Ok(result);
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


    }
}