using System;
using System.Collections.Generic;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Planificacion/SubAreaCapacitacion
    /// Autor: Gian Miranda
    /// Fecha: 15/04/2021
    /// <summary>
    /// Configura las acciones para todo lo relacionado con las subareas de capacitacion
    /// </summary>
    [Route("api/SubAreaCapacitacion")]
    public class SubAreaCapacitacionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public SubAreaCapacitacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las areas de capacitacion habilitadas en una lista de objetos de clase SubAreaCapacitacionFiltroDTO
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase SubAreaCapacitacionFiltroDTO, caso contrario response 400 con el mensaje de error respectivo</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                SubAreaCapacitacionBO subAreaCapacitacion = new SubAreaCapacitacionBO();
                return Ok(subAreaCapacitacion.ObtenerTodoFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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
                SubAreaCapacitacionRepositorio _subAreaCapacitacionRepositorio = new SubAreaCapacitacionRepositorio(_integraDBContext);
                return Ok(_subAreaCapacitacionRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoAreaCapacitacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaCapacitacionRepositorio _areaCapacitacionRepositorio = new AreaCapacitacionRepositorio(_integraDBContext);
                return Ok(_areaCapacitacionRepositorio.ObtenerTodoGridArea());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoParametroSeo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ParametroSeoPwRepositorio _parametroSeoRepositorio = new ParametroSeoPwRepositorio(_integraDBContext);
                return Ok(_parametroSeoRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdSubAreaCapacitacion}")]
        [HttpGet]
        public ActionResult ObtenerTodoParametroContenido(int IdSubAreaCapacitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubAreaCapacitacionRepositorio _repSubAreaParametroSeo = new SubAreaCapacitacionRepositorio(_integraDBContext);
                return Ok(_repSubAreaParametroSeo.ObtenerTodoPorIdSubAreaCapacitacion(IdSubAreaCapacitacion));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarSubAreaCapacitacion([FromBody] CompuestoSubAreaCapacitacionDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubAreaCapacitacionRepositorio _subAreaCapacitacionRepositorio = new SubAreaCapacitacionRepositorio(_integraDBContext);
                SubAreaCapacitacionBO subAreaCapacitacionBO = new SubAreaCapacitacionBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    subAreaCapacitacionBO.Nombre = Json.ObjetoSubArea.Nombre;
                    subAreaCapacitacionBO.Descripcion = Json.ObjetoSubArea.Descripcion;
                    subAreaCapacitacionBO.IdAreaCapacitacion = Json.ObjetoSubArea.IdAreaCapacitacion;
                    subAreaCapacitacionBO.EsVisibleWeb = Json.ObjetoSubArea.EsVisibleWeb;
                    subAreaCapacitacionBO.IdSubArea = Json.ObjetoSubArea.IdSubArea;
                    subAreaCapacitacionBO.DescripcionHtml = Json.ObjetoSubArea.DescripcionHtml;
                    subAreaCapacitacionBO.AliasFacebook = "";

                    subAreaCapacitacionBO.Estado = true;
                    subAreaCapacitacionBO.UsuarioCreacion = Json.Usuario;
                    subAreaCapacitacionBO.UsuarioModificacion = Json.Usuario;
                    subAreaCapacitacionBO.FechaCreacion = DateTime.Now;
                    subAreaCapacitacionBO.FechaModificacion = DateTime.Now;

                    subAreaCapacitacionBO.SubAreaParametroSeo = new List<SubAreaParametroSeoBO>();

                    foreach (var item in Json.ListaParametro)
                    {
                        SubAreaParametroSeoBO subAreaParametroSeoBO = new SubAreaParametroSeoBO();
                        if (String.IsNullOrEmpty(item.Contenido))
                            subAreaParametroSeoBO.Descripcion = "<!--vacio-->";
                        else
                            subAreaParametroSeoBO.Descripcion = item.Contenido;
                        subAreaParametroSeoBO.IdParametroSeoPw = item.Id;
                        subAreaParametroSeoBO.Estado = true;
                        subAreaParametroSeoBO.UsuarioCreacion = Json.Usuario;
                        subAreaParametroSeoBO.UsuarioModificacion = Json.Usuario;
                        subAreaParametroSeoBO.FechaCreacion = DateTime.Now;
                        subAreaParametroSeoBO.FechaModificacion = DateTime.Now;

                        subAreaCapacitacionBO.SubAreaParametroSeo.Add(subAreaParametroSeoBO);
                    }
                    _subAreaCapacitacionRepositorio.Insert(subAreaCapacitacionBO);
                    scope.Complete();
                }
                return Ok(subAreaCapacitacionBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarSubAreaCapacitacion([FromBody] CompuestoSubAreaCapacitacionDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubAreaCapacitacionRepositorio _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext); 
                SubAreaParametroSeoRepositorio _repSubAreaParametroSeo = new SubAreaParametroSeoRepositorio(_integraDBContext); 

                SubAreaCapacitacionBO subAreaCapacitacionBO = new SubAreaCapacitacionBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    _repSubAreaParametroSeo.EliminacionLogicoPorSubAreaCapacitacion(Json.ObjetoSubArea.Id, Json.Usuario, Json.ListaParametro);

                    subAreaCapacitacionBO = _repSubAreaCapacitacion.FirstById(Json.ObjetoSubArea.Id);

                    subAreaCapacitacionBO.Nombre = Json.ObjetoSubArea.Nombre;
                    subAreaCapacitacionBO.Descripcion = Json.ObjetoSubArea.Descripcion;
                    subAreaCapacitacionBO.IdAreaCapacitacion = Json.ObjetoSubArea.IdAreaCapacitacion;
                    subAreaCapacitacionBO.EsVisibleWeb = Json.ObjetoSubArea.EsVisibleWeb;
                    subAreaCapacitacionBO.IdSubArea = Json.ObjetoSubArea.IdSubArea;
                    subAreaCapacitacionBO.DescripcionHtml = Json.ObjetoSubArea.DescripcionHtml;
                    subAreaCapacitacionBO.AliasFacebook = "";

                    subAreaCapacitacionBO.UsuarioModificacion = Json.Usuario;
                    subAreaCapacitacionBO.FechaModificacion = DateTime.Now;

                    subAreaCapacitacionBO.SubAreaParametroSeo = new List<SubAreaParametroSeoBO>();

                    foreach (var item in Json.ListaParametro)
                    {
                        SubAreaParametroSeoBO subAreaParametroSeoBO;
                        if (_repSubAreaParametroSeo.Exist(x => x.IdParametroSeoPw == item.Id && x.IdSubAreaCapacitacion == Json.ObjetoSubArea.Id))
                        {
                            subAreaParametroSeoBO = _repSubAreaParametroSeo.FirstBy(x => x.IdParametroSeoPw == item.Id && x.IdSubAreaCapacitacion == Json.ObjetoSubArea.Id);
                            subAreaParametroSeoBO.IdParametroSeoPw = item.Id;
                            if (String.IsNullOrEmpty(item.Contenido))
                                subAreaParametroSeoBO.Descripcion = "<!--vacio-->";
                            else
                                subAreaParametroSeoBO.Descripcion = item.Contenido;
                            subAreaParametroSeoBO.UsuarioModificacion = Json.Usuario;
                            subAreaParametroSeoBO.FechaModificacion = DateTime.Now;

                        }
                        else
                        {
                            subAreaParametroSeoBO = new SubAreaParametroSeoBO();
                            subAreaParametroSeoBO.IdParametroSeoPw = item.Id;
                            if (String.IsNullOrEmpty(item.Contenido))
                                subAreaParametroSeoBO.Descripcion = "<!--vacio-->";
                            else
                                subAreaParametroSeoBO.Descripcion = item.Contenido;
                            subAreaParametroSeoBO.UsuarioCreacion = Json.Usuario;
                            subAreaParametroSeoBO.UsuarioModificacion = Json.Usuario;
                            subAreaParametroSeoBO.FechaCreacion = DateTime.Now;
                            subAreaParametroSeoBO.FechaModificacion = DateTime.Now;
                            subAreaParametroSeoBO.Estado = true;
                        }
                        subAreaCapacitacionBO.SubAreaParametroSeo.Add(subAreaParametroSeoBO);
                    }
                    _repSubAreaCapacitacion.Update(subAreaCapacitacionBO);
                    scope.Complete();
                }

                return Ok(subAreaCapacitacionBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarSubAreaCapacitacion([FromBody] SubAreaCapacitacionPrincipalDTO objetoSubArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SubAreaCapacitacionRepositorio _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext);
                SubAreaParametroSeoRepositorio _repSubAreaParametroSeo = new SubAreaParametroSeoRepositorio(_integraDBContext);

                SubAreaCapacitacionBO tagPwBO = new SubAreaCapacitacionBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repSubAreaCapacitacion.Exist(objetoSubArea.Id))
                    {
                        _repSubAreaCapacitacion.Delete(objetoSubArea.Id, objetoSubArea.Usuario);
                        var hijoSubAreaParametroSeo = _repSubAreaParametroSeo.GetBy(x => x.IdSubAreaCapacitacion == objetoSubArea.Id);
                        foreach (var hijo in hijoSubAreaParametroSeo)
                        {
                            _repSubAreaParametroSeo.Delete(hijo.Id, objetoSubArea.Usuario);
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

    public class ValidadorSubAreaCapacitacionDTO : AbstractValidator<TSubAreaCapacitacion>
    {
        public static ValidadorSubAreaCapacitacionDTO Current = new ValidadorSubAreaCapacitacionDTO();
        public ValidadorSubAreaCapacitacionDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
        }
    }
}
