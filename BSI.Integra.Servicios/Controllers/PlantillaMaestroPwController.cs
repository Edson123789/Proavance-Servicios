using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PlantillaMaestroPw")]
    public class PlantillaMaestroPwController : BaseController<TPlantillaMaestroPw, ValidadoPlantillaMaestroPwDTO>
    {
        public PlantillaMaestroPwController(IIntegraRepository<TPlantillaMaestroPw> repositorio, ILogger<BaseController<TPlantillaMaestroPw, ValidadoPlantillaMaestroPwDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaMaestroPwRepositorio _repPlantillaMaestroPw = new PlantillaMaestroPwRepositorio();
                return Ok(_repPlantillaMaestroPw.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPlantillaMaestro}")]
        [HttpGet]
        public ActionResult ObtenerSeccionMaestraPorIdPlantillaMaestro(int IdPlantillaMaestro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SeccionMaestraPwRepositorio _repSeccionMaestraPw = new SeccionMaestraPwRepositorio();
                var listaSeccionMaestra = _repSeccionMaestraPw.GetBy(x => x.IdPlantillaMaestroPw == IdPlantillaMaestro && x.Estado == true).ToList();
                return Ok(listaSeccionMaestra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarPlantillaMaestro([FromBody] CompuestoPlantillaMaestroDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaMaestroPwRepositorio _repPlantillaMaestroPw = new PlantillaMaestroPwRepositorio();
                PlantillaMaestroPwBO plantillaMaestroBO = new PlantillaMaestroPwBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    plantillaMaestroBO.Nombre = Json.ObjetoPlantillaMaestro.Nombre;
                    plantillaMaestroBO.Repeticion = Json.ObjetoPlantillaMaestro.Repeticion;
                    plantillaMaestroBO.Estado = true;
                    plantillaMaestroBO.UsuarioCreacion = Json.Usuario;
                    plantillaMaestroBO.UsuarioModificacion = Json.Usuario;
                    plantillaMaestroBO.FechaCreacion = DateTime.Now;
                    plantillaMaestroBO.FechaModificacion = DateTime.Now;

                    plantillaMaestroBO.SeccionMaestra = new List<SeccionMaestraPwBO>();

                    foreach (var item in Json.ListaSeccionMaestra)
                    {
                        SeccionMaestraPwBO seccionMaestraBO = new SeccionMaestraPwBO();
                        seccionMaestraBO.Nombre = item.Nombre;
                        seccionMaestraBO.Descripcion = item.Descripcion;
                        seccionMaestraBO.Contenido = item.Contenido;
                        seccionMaestraBO.Estado = true;
                        seccionMaestraBO.UsuarioCreacion = Json.Usuario;
                        seccionMaestraBO.UsuarioModificacion = Json.Usuario;
                        seccionMaestraBO.FechaCreacion = DateTime.Now;
                        seccionMaestraBO.FechaModificacion = DateTime.Now;

                        plantillaMaestroBO.SeccionMaestra.Add(seccionMaestraBO);
                    }
                    _repPlantillaMaestroPw.Insert(plantillaMaestroBO);
                    scope.Complete();
                }
                return Ok(plantillaMaestroBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarPlantillaMaestro([FromBody] CompuestoPlantillaMaestroDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();

                PlantillaMaestroPwRepositorio _repPlantillaMaestroPw = new PlantillaMaestroPwRepositorio(contexto);
                SeccionMaestraPwRepositorio _repSeccionMaestraPw = new SeccionMaestraPwRepositorio(contexto);

                PlantillaMaestroPwBO plantillaMaestroBO = new PlantillaMaestroPwBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    _repSeccionMaestraPw.EliminacionLogicoPorIdPlantillaMaestro(Json.ObjetoPlantillaMaestro.Id, Json.Usuario, Json.ListaSeccionMaestra);

                    plantillaMaestroBO = _repPlantillaMaestroPw.FirstById(Json.ObjetoPlantillaMaestro.Id);

                    plantillaMaestroBO.Nombre = Json.ObjetoPlantillaMaestro.Nombre;
                    plantillaMaestroBO.Repeticion = Json.ObjetoPlantillaMaestro.Repeticion;
                    plantillaMaestroBO.UsuarioModificacion = Json.Usuario;
                    plantillaMaestroBO.FechaModificacion = DateTime.Now;

                    plantillaMaestroBO.SeccionMaestra = new List<SeccionMaestraPwBO>();

                    foreach (var item in Json.ListaSeccionMaestra)
                    {
                        SeccionMaestraPwBO seccionMaestraBO;
                        if (_repSeccionMaestraPw.Exist(x => x.Id == item.Id && x.IdPlantillaMaestroPw == Json.ObjetoPlantillaMaestro.Id))
                            {
                            seccionMaestraBO = _repSeccionMaestraPw.FirstBy(x => x.Id == item.Id && x.IdPlantillaMaestroPw == Json.ObjetoPlantillaMaestro.Id);
                            seccionMaestraBO.Nombre = item.Nombre;
                            seccionMaestraBO.Descripcion = item.Descripcion;
                            seccionMaestraBO.Contenido = item.Contenido;
                            seccionMaestraBO.UsuarioModificacion = Json.Usuario;
                            seccionMaestraBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            seccionMaestraBO = new SeccionMaestraPwBO();
                            seccionMaestraBO.Nombre = item.Nombre;
                            seccionMaestraBO.Descripcion = item.Descripcion;
                            seccionMaestraBO.Contenido = item.Contenido;

                            seccionMaestraBO.UsuarioCreacion = Json.Usuario;
                            seccionMaestraBO.UsuarioModificacion = Json.Usuario;
                            seccionMaestraBO.FechaCreacion = DateTime.Now;
                            seccionMaestraBO.FechaModificacion = DateTime.Now;
                            seccionMaestraBO.Estado = true;
                        }
                        plantillaMaestroBO.SeccionMaestra.Add(seccionMaestraBO);
                    }
                    _repPlantillaMaestroPw.Update(plantillaMaestroBO);
                    scope.Complete();
                }
                return Ok(plantillaMaestroBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarPlantillaMaestro(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PlantillaMaestroPwRepositorio _repPlantillaMaestroPw = new PlantillaMaestroPwRepositorio(contexto);
                SeccionMaestraPwRepositorio _repSeccionMaestraPw = new SeccionMaestraPwRepositorio(contexto);

                PlantillaMaestroPwBO plantillaMaestroBO = new PlantillaMaestroPwBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repPlantillaMaestroPw.Exist(Id))
                    {
                        _repPlantillaMaestroPw.Delete(Id, Usuario);
                        var hijosSeccionMaestra = _repSeccionMaestraPw.GetBy(x => x.IdPlantillaMaestroPw == Id);
                        foreach (var hijo in hijosSeccionMaestra)
                        {
                            _repSeccionMaestraPw.Delete(hijo.Id, Usuario);
                        }
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class ValidadoPlantillaMaestroPwDTO : AbstractValidator<TPlantillaMaestroPw>
    {
        public static ValidadoPlantillaMaestroPwDTO Current = new ValidadoPlantillaMaestroPwDTO();
        public ValidadoPlantillaMaestroPwDTO()
        {
        }
    }
}
