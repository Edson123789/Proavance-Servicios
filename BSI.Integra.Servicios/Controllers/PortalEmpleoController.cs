using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PortalEmpleo")]
    public class PortalEmpleoController : BaseController<TPortalEmpleo, ValidadorPortalEmpleoDTO>
    {
        public PortalEmpleoController(IIntegraRepository<TPortalEmpleo> repositorio, ILogger<BaseController<TPortalEmpleo, ValidadorPortalEmpleoDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerRegistrosPortalEmpleo()
        {
            try
            {
                PortalEmpleoRepositorio _repPortalEmpleo = new PortalEmpleoRepositorio();
                var PortalEmpleo = _repPortalEmpleo.ObtenerTodoPortalEmpleo();                
                return Ok(PortalEmpleo );
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarPortalEmpleo([FromBody]FiltroInsertarPortalEmpleoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext(); 
                PortalEmpleoRepositorio _repPortalEmpleo = new PortalEmpleoRepositorio();
                PortalEmpleoPaisRepositorio _repPortalEmpleoPais = new PortalEmpleoPaisRepositorio();
                PortalEmpleoBO portalEmpleo = new PortalEmpleoBO();

                portalEmpleo.Nombre = Json.Objeto.Nombre;
                portalEmpleo.Url = Json.Objeto.Nombre;
                portalEmpleo.Estado = true;
                portalEmpleo.FechaCreacion = DateTime.Now;
                portalEmpleo.FechaModificacion = DateTime.Now;
                portalEmpleo.UsuarioCreacion = Json.Objeto.Usuario;
                portalEmpleo.UsuarioModificacion = Json.Objeto.Usuario;

                _repPortalEmpleo.Insert(portalEmpleo);

                List<PortalEmpleoPaisBO> ListaPortalEmpleoPais = new List<PortalEmpleoPaisBO>();
                foreach (var item in Json.Paises)
                {
                    PortalEmpleoPaisBO portalEmpleoPais = new PortalEmpleoPaisBO();
                    portalEmpleoPais.IdPortalEmpleo = portalEmpleo.Id;
                    portalEmpleoPais.IdPais = item;
                    portalEmpleoPais.Estado = true;
                    portalEmpleoPais.FechaCreacion = DateTime.Now;
                    portalEmpleoPais.FechaModificacion = DateTime.Now;
                    portalEmpleoPais.UsuarioCreacion = Json.Objeto.Usuario;
                    portalEmpleoPais.UsuarioModificacion = Json.Objeto.Usuario;

                    ListaPortalEmpleoPais.Add(portalEmpleoPais);
                }
                _repPortalEmpleoPais.Insert(ListaPortalEmpleoPais);


                return Ok(portalEmpleo);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarPortalEmpleo([FromBody]FiltroInsertarPortalEmpleoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PortalEmpleoRepositorio _repPortalEmpleo = new PortalEmpleoRepositorio(contexto);
                PortalEmpleoPaisRepositorio _repPortalEmpleoPais = new PortalEmpleoPaisRepositorio(contexto);

                var portalEmpleo = _repPortalEmpleo.FirstById(Json.Objeto.Id);
                portalEmpleo.Nombre = Json.Objeto.Nombre;
                portalEmpleo.Url = Json.Objeto.Nombre;
                portalEmpleo.Estado = true;
                portalEmpleo.FechaCreacion = DateTime.Now;
                portalEmpleo.FechaModificacion = DateTime.Now;
                portalEmpleo.UsuarioCreacion = Json.Objeto.Usuario;
                portalEmpleo.UsuarioModificacion = Json.Objeto.Usuario;

                List<PortalEmpleoPaisBO> ListaportalEmpleoPais = _repPortalEmpleoPais.GetBy(w => w.IdPortalEmpleo == Json.Objeto.Id && w.Estado == true).ToList();
                var ListaPaises = Json.Paises;
                List<PortalEmpleoPaisBO> ListaActualizar = new List<PortalEmpleoPaisBO>();
                List<int> ListaEliminar = new List<int>();

                foreach (var item in Json.Paises)
                {
                    if (ListaportalEmpleoPais.Exists(w => w.IdPais == item && w.IdPortalEmpleo == Json.Objeto.Id))
                    {
                        ListaActualizar.Add(ListaActualizar.Where(w => w.IdPais == item && w.IdPortalEmpleo == Json.Objeto.Id).FirstOrDefault());
                        ListaportalEmpleoPais.RemoveAll(w => w.IdPais == item && w.IdPortalEmpleo == Json.Objeto.Id);
                        ListaPaises.RemoveAll(w => w.Equals(item));
                        
                    }                    
                }

                if(ListaportalEmpleoPais.Count != 0)
                {
                    ListaEliminar = ListaportalEmpleoPais.Select(w => w.Id).ToList();
                    _repPortalEmpleo.Delete(ListaEliminar, Json.Objeto.Usuario);
                }
                if (ListaPaises.Count != 0)
                {
                    foreach (var item in ListaPaises)
                    {
                        PortalEmpleoPaisBO portalEmpleoPais = new PortalEmpleoPaisBO();

                        portalEmpleoPais.IdPortalEmpleo = Json.Objeto.Id;
                        portalEmpleoPais.IdPais = item;
                        portalEmpleoPais.Estado = true;
                        portalEmpleoPais.FechaCreacion = DateTime.Now;
                        portalEmpleoPais.FechaModificacion = DateTime.Now;
                        portalEmpleoPais.UsuarioCreacion = Json.Objeto.Usuario;
                        portalEmpleoPais.UsuarioModificacion = Json.Objeto.Usuario;

                        _repPortalEmpleoPais.Insert(portalEmpleoPais);
                    }
                }

                if (ListaActualizar.Count != 0)
                {
                    foreach (var item in ListaActualizar)
                    {
                        item.FechaModificacion = DateTime.Now;
                        item.UsuarioModificacion = Json.Objeto.Usuario;

                        _repPortalEmpleoPais.Update(item);
                    }
                }
                return Ok(portalEmpleo);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult EliminarPortalEmpleo([FromBody]EliminarDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PortalEmpleoRepositorio _repPortalEmpleo = new PortalEmpleoRepositorio(contexto);
                PortalEmpleoPaisRepositorio _repPortalEmpleoPais = new PortalEmpleoPaisRepositorio(contexto);

                _repPortalEmpleo.Delete(Objeto.Id, Objeto.NombreUsuario);

                List<PortalEmpleoPaisBO> ListaportalEmpleoPais = _repPortalEmpleoPais.GetBy(w => w.IdPortalEmpleo == Objeto.Id && w.Estado == true).ToList();
                if (ListaportalEmpleoPais.Count > 0)
                {
                    var eliminar =ListaportalEmpleoPais.Select(w => w.Id).ToList();
                    _repPortalEmpleoPais.Delete(eliminar,Objeto.NombreUsuario);
                }

                return Ok(true);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/{IdPortalEmpleo}")]
        [HttpGet]
        public ActionResult ObtenerPortalEmpleoPais(int IdPortalEmpleo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PortalEmpleoPaisRepositorio _repPortalEmpleoPais = new PortalEmpleoPaisRepositorio();

                var rpta=_repPortalEmpleoPais.GetBy(w => w.IdPortalEmpleo == IdPortalEmpleo && w.Estado == true, y => new { y.IdPais });
                return Ok(rpta.Select(w => w.IdPais).ToList());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


    }
    public class ValidadorPortalEmpleoDTO : AbstractValidator<TPortalEmpleo>
    {
        public static ValidadorPortalEmpleoDTO Current = new ValidadorPortalEmpleoDTO();
        public ValidadorPortalEmpleoDTO()
        {

            //RuleFor(objeto => objeto.IdMeseengerUsuario).NotEmpty().WithMessage("IdMeseengerUsuario es Obligatorio");

            //RuleFor(objeto => objeto.IdPersonal).NotEmpty().WithMessage("IdPersonal es Obligatorio");

            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombrekkkkkkkkkkk debe tener 1 caracter minimo y 100 maximo");

        }
    }
}
