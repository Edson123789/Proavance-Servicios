using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.SCode.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Feriados")]
    public class FeriadosController : BaseController<TFeriado, ValidadorFeriadoDTO>
    {
        public FeriadosController(IIntegraRepository<TFeriado> repositorio, ILogger<BaseController<TFeriado, ValidadorFeriadoDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {

        }

        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosFeriados()
        {
            try
            {
				TroncalPgeneralRepositorio _repTroncalCiudad = new TroncalPgeneralRepositorio();
                TipoFeriadoRepositorio _repTipoFeriado = new TipoFeriadoRepositorio();
                FrecuenciaFeriadoRepositorio _repFrecuencia = new FrecuenciaFeriadoRepositorio();

                FeriadoComboDTO comboFeriado = new FeriadoComboDTO();

                comboFeriado.Tipos = _repTipoFeriado.ObtenerTipoFeriado();
                comboFeriado.Frecuencia = _repFrecuencia.ObtenerFrecuenciaFeriado();
                comboFeriado.Ciudades = _repTroncalCiudad.ObtenerTroncalCiudadFiltro();


                return Ok(comboFeriado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerFeriadosPorDiaCiudad(int month, int year, int ciudadId)
        {
            try
            {
                FeriadoRepositorio repFeriado = new FeriadoRepositorio();
                
               
                var lista = repFeriado.ObtenerFeriadoDiaCiudad(month, year, ciudadId)
                    .Select(s =>
                                        new {
                                            id = s.Id,
                                            title = s.Motivo,
                                            start = s.Dia,
                                            allDay = true,
                                            editable = false
                                        }
                                    ).ToList(); 

                 
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerFeriadoId(int id)
        {
            try
            {
                FeriadoRepositorio repFeriado = new FeriadoRepositorio();
                DatosFeriadoDTO feriados = new DatosFeriadoDTO();
                FeriadoBO feriado = new FeriadoBO();

                feriado = repFeriado.FirstById(id);

                return Ok(feriado);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("[action]")]
        [HttpPost]

        public ActionResult InsertarFeriado([FromBody]DatosFeriadoDTO Json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeriadoRepositorio repFeriado = new FeriadoRepositorio();
                FeriadoBO feriado = new FeriadoBO();
                List<CiudadTroncalPaisFiltroDTO> listaCiudades = new List<CiudadTroncalPaisFiltroDTO>();
				TroncalPgeneralRepositorio _repTroncalCiudad = new TroncalPgeneralRepositorio();
                List<FeriadoBO> existeFeriado = new List<FeriadoBO>();

                using (TransactionScope scope = new TransactionScope())
                {
                    //Insertar
                    
                    if (Json.EstadoCiudad == true)
                    {
                        listaCiudades = _repTroncalCiudad.ObtenerCiudadPorFeriado(Json.Id, Json.IdCiudad);

                        foreach (var city in listaCiudades)
                        {
                            feriado = new FeriadoBO();
                            feriado.Tipo = Json.Tipo;
                            feriado.Motivo = Json.Motivo;
                            feriado.Dia = Json.Dia;
                            feriado.Frecuencia = Json.Frecuencia;
                            feriado.UsuarioCreacion = Json.Usuario;
                            feriado.UsuarioModificacion = Json.Usuario;
                            feriado.FechaCreacion = DateTime.Now;
                            feriado.FechaModificacion = DateTime.Now;
                            feriado.Estado = true;
                            feriado.IdTroncalCiudad = city.CodigoCiudad;
                            repFeriado.Insert(feriado);
                        }

                    }
                    else
                    {
                        existeFeriado = repFeriado.ObtenerFeriado(Json);
                        if (existeFeriado.Count == 0)
                        {
                            feriado = new FeriadoBO();
                            feriado.Tipo = Json.Tipo;
                            feriado.Motivo = Json.Motivo;
                            feriado.Dia = Json.Dia;
                            feriado.Frecuencia = Json.Frecuencia;
                            feriado.IdTroncalCiudad = Json.IdCiudad ;
                            feriado.UsuarioCreacion = Json.Usuario;
                            feriado.UsuarioModificacion = Json.Usuario;
                            feriado.FechaCreacion = DateTime.Now;
                            feriado.FechaModificacion = DateTime.Now;
                            feriado.Estado = true;
                            repFeriado.Insert(feriado);
                        }
                        else
                        {
                            return Ok("Feriado ya existe para ese id");
                            
                        }
                    }
                  
                    scope.Complete();
                }
                
                return Ok(feriado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarFeriado([FromBody]DatosFeriadoDTO Json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                FeriadoRepositorio repFeriado = new FeriadoRepositorio();
                FeriadoBO feriado;

                if (repFeriado.Exist(Json.Id))
                {
                    feriado = repFeriado.FirstById(Json.Id);
                    feriado.Tipo = Json.Tipo;
                    feriado.Motivo = Json.Motivo;
                    feriado.Dia = Json.Dia;
                    feriado.Frecuencia = Json.Frecuencia;
                    feriado.IdTroncalCiudad = Json.IdCiudad;
                    feriado.FechaModificacion = DateTime.Now;
                    feriado.UsuarioModificacion = Json.Usuario;
                    feriado.Estado = true;
                    repFeriado.Update(feriado);
                    
                }
                
                
                return Ok(true);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult EliminarFeriado([FromBody]DatosFeriadoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeriadoRepositorio repEliminarFeriado = new FeriadoRepositorio();
                bool result = false;
                if (repEliminarFeriado.Exist(Json.Id))
                {
                    result = repEliminarFeriado.Delete(Json.Id, Json.Usuario);
                }

                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


    public class ValidadorFeriadoDTO : AbstractValidator<TFeriado>
    {
        public static ValidadorFeriadoDTO Current = new ValidadorFeriadoDTO();
        public ValidadorFeriadoDTO()
        {
            RuleFor(objeto => objeto.Tipo).NotEmpty().WithMessage("Tipo es Obligatorio");
            RuleFor(objeto => objeto.Dia).NotEmpty().WithMessage("El Codigo es Obligatorio");
            RuleFor(objeto => objeto.Motivo).NotEmpty().WithMessage("El Valor es Obligatorio");

        }
    }
}
