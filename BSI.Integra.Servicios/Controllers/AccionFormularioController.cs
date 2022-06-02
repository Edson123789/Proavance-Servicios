using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/AccionFormulario")]
    public class AccionFormularioController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public AccionFormularioController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPanel()
        {
            try
            {
                AccionFormularioRepositorio accionFormularioRepositorio = new AccionFormularioRepositorio(_integraDBContext);

                return Ok(accionFormularioRepositorio.ObtenerTodoPanel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCategoriaOrigen()
        {
            try
            {
                CategoriaOrigenRepositorio categoriaOrigenRepositorio = new CategoriaOrigenRepositorio(_integraDBContext);

                return Ok(categoriaOrigenRepositorio.ObtenerCategoriaFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCampoContacto()
        {
            try
            {
                CampoContactoRepositorio campoContactoRepositorio = new CampoContactoRepositorio(_integraDBContext);

                return Ok(campoContactoRepositorio.ObtenerCamposContactoFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{IdAccionFormulario}")]
        [HttpGet]
        public ActionResult ObtenerDetalle(int IdAccionFormulario)
        {
            try
            {
                AccionFormularioPorCategoriaOrigenRepositorio accionFormularioPorCategoriaOrigenRepositorio = new AccionFormularioPorCategoriaOrigenRepositorio(_integraDBContext);
                AccionFormularioPorCampoContactoRepositorio accionFormularioPorCampoContactoRepositorio = new AccionFormularioPorCampoContactoRepositorio(_integraDBContext);

                var CategoriaOrigenes = accionFormularioPorCategoriaOrigenRepositorio.ObtenerPorIdAccionFormulario(IdAccionFormulario);
                List<int> ListaCategoriaOrigen = new List<int>();
                foreach(var item in CategoriaOrigenes) {
                    ListaCategoriaOrigen.Add(item.IdCategoriaOrigen);
                }

                var ListaCampoContacto = accionFormularioPorCampoContactoRepositorio.ObtenerPorIdAccionFormulario(IdAccionFormulario);

                return Ok(new {ListaCategoriaOrigen=ListaCategoriaOrigen, ListaCampoContacto=ListaCampoContacto});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] AccionFormularioDTO accionFormularioDTO)
        {
            try
            {
                AccionFormularioRepositorio accionFormularioRepositorio = new AccionFormularioRepositorio(_integraDBContext);

                AccionFormularioBO accionFormularioBO = new AccionFormularioBO();
                accionFormularioBO.Nombre = accionFormularioDTO.Nombre;
                accionFormularioBO.UltimaLlamadaEjecutada = accionFormularioDTO.UltimaLlamadaEjecutada;
                accionFormularioBO.CamposSinValores = accionFormularioDTO.CamposSinValores;
                accionFormularioBO.TiempoRedirecionamiento = accionFormularioDTO.TiempoRedirecionamiento;
                accionFormularioBO.CamposSegunProbabilidad = accionFormularioDTO.CamposSegunProbabilidad;
                accionFormularioBO.TodosCampos = accionFormularioDTO.TodosCampos;
                accionFormularioBO.NumeroClics = accionFormularioDTO.NumeroClics;
                accionFormularioBO.Estado = true;
                accionFormularioBO.UsuarioCreacion = accionFormularioDTO.Usuario;
                accionFormularioBO.UsuarioModificacion = accionFormularioDTO.Usuario;
                accionFormularioBO.FechaCreacion = DateTime.Now;
                accionFormularioBO.FechaModificacion = DateTime.Now;

                if (accionFormularioBO.HasErrors)
                {
                    return BadRequest(accionFormularioBO.ActualesErrores);
                }

                accionFormularioBO.ListaCategoriaOrigen = new List<AccionFormularioPorCategoriaOrigenBO>();
                AccionFormularioPorCategoriaOrigenBO accionFormularioPorCategoriaOrigenBO;
                foreach (var item in accionFormularioDTO.ListaCategoriaOrigen)
                {
                    accionFormularioPorCategoriaOrigenBO = new AccionFormularioPorCategoriaOrigenBO();
                    accionFormularioPorCategoriaOrigenBO.IdCategoriaOrigen = item;
                    accionFormularioPorCategoriaOrigenBO.Estado = true;
                    accionFormularioPorCategoriaOrigenBO.UsuarioCreacion = accionFormularioDTO.Usuario;
                    accionFormularioPorCategoriaOrigenBO.UsuarioModificacion = accionFormularioDTO.Usuario;
                    accionFormularioPorCategoriaOrigenBO.FechaCreacion = DateTime.Now;
                    accionFormularioPorCategoriaOrigenBO.FechaModificacion = DateTime.Now;

                    if (accionFormularioPorCategoriaOrigenBO.HasErrors) return BadRequest(accionFormularioPorCategoriaOrigenBO.ActualesErrores);
                    else accionFormularioBO.ListaCategoriaOrigen.Add(accionFormularioPorCategoriaOrigenBO);
                }

                accionFormularioBO.ListaCampoContacto = new List<AccionFormularioPorCampoContactoBO>();
                AccionFormularioPorCampoContactoBO accionFormularioPorCampoContactoBO;
                foreach (var item in accionFormularioDTO.ListaCampoContacto)
                {
                    accionFormularioPorCampoContactoBO = new AccionFormularioPorCampoContactoBO();
                    accionFormularioPorCampoContactoBO.IdCampoContacto = item.IdCampoContacto;
                    accionFormularioPorCampoContactoBO.Orden = item.Orden;
                    accionFormularioPorCampoContactoBO.Campo = item.Campo;
                    accionFormularioPorCampoContactoBO.EsSiempreVisible = item.EsSiempreVisible;
                    accionFormularioPorCampoContactoBO.EsInteligente = item.EsInteligente;
                    accionFormularioPorCampoContactoBO.TieneProbabilidad = item.TieneProbabilidad;
                    accionFormularioPorCampoContactoBO.Estado = true;
                    accionFormularioPorCampoContactoBO.UsuarioCreacion = accionFormularioDTO.Usuario;
                    accionFormularioPorCampoContactoBO.UsuarioModificacion = accionFormularioDTO.Usuario;
                    accionFormularioPorCampoContactoBO.FechaCreacion = DateTime.Now;
                    accionFormularioPorCampoContactoBO.FechaModificacion = DateTime.Now;

                    if (accionFormularioPorCampoContactoBO.HasErrors) return BadRequest(accionFormularioPorCampoContactoBO.ActualesErrores);
                    else accionFormularioBO.ListaCampoContacto.Add(accionFormularioPorCampoContactoBO);
                }

                accionFormularioRepositorio.Insert(accionFormularioBO);

                return Ok(accionFormularioBO.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] AccionFormularioDTO accionFormularioDTO)
        {
            try
            {
                AccionFormularioRepositorio accionFormularioRepositorio = new AccionFormularioRepositorio(_integraDBContext);
                AccionFormularioPorCategoriaOrigenRepositorio accionFormularioPorCategoriaOrigenRepositorio = new AccionFormularioPorCategoriaOrigenRepositorio(_integraDBContext);
                AccionFormularioPorCampoContactoRepositorio accionFormularioPorCampoContactoRepositorio = new AccionFormularioPorCampoContactoRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    //accionFormularioPorCategoriaOrigenRepositorio.DeleteLogicoPorAccionFormulario(accionFormularioDTO.Id, accionFormularioDTO.Usuario, accionFormularioDTO.ListaCategoriaOrigen);
                    List<AccionFormularioPorCategoriaOrigenBO> ListaEliminar = accionFormularioPorCategoriaOrigenRepositorio.GetBy(x=>x.IdAccionFormulario == accionFormularioDTO.Id && x.Estado==true).ToList();
                    foreach (var item in ListaEliminar)
                    {
                        item.Estado = false;
                        item.FechaModificacion = DateTime.Now;
                        item.UsuarioModificacion = accionFormularioDTO.Usuario;
                    }
                    accionFormularioPorCategoriaOrigenRepositorio.Update(ListaEliminar);
                    accionFormularioPorCampoContactoRepositorio.DeleteLogicoPorAccionFormulario(accionFormularioDTO.Id, accionFormularioDTO.Usuario, accionFormularioDTO.ListaCampoContacto);

                    AccionFormularioBO accionFormularioBO = accionFormularioRepositorio.FirstById(accionFormularioDTO.Id);
                    accionFormularioBO.Nombre = accionFormularioDTO.Nombre;
                    accionFormularioBO.UltimaLlamadaEjecutada = accionFormularioDTO.UltimaLlamadaEjecutada;
                    accionFormularioBO.CamposSinValores = accionFormularioDTO.CamposSinValores;
                    accionFormularioBO.TiempoRedirecionamiento = accionFormularioDTO.TiempoRedirecionamiento;
                    accionFormularioBO.CamposSegunProbabilidad = accionFormularioDTO.CamposSegunProbabilidad;
                    accionFormularioBO.TodosCampos = false;
                    accionFormularioBO.NumeroClics = accionFormularioDTO.NumeroClics;
                    accionFormularioBO.UsuarioModificacion = accionFormularioDTO.Usuario;
                    accionFormularioBO.FechaModificacion = DateTime.Now;

                    if (accionFormularioBO.HasErrors)
                    {
                        return BadRequest(accionFormularioBO.ActualesErrores);
                    }

                    accionFormularioBO.ListaCategoriaOrigen = new List<AccionFormularioPorCategoriaOrigenBO>();
                    AccionFormularioPorCategoriaOrigenBO accionFormularioPorCategoriaOrigenBO;
                    foreach (var item in accionFormularioDTO.ListaCategoriaOrigen)
                    {
                        if (accionFormularioPorCategoriaOrigenRepositorio.FirstBy(x => x.IdAccionFormulario == accionFormularioDTO.Id && x.IdCategoriaOrigen == item) != null)
                            accionFormularioPorCategoriaOrigenBO = accionFormularioPorCategoriaOrigenRepositorio.FirstBy(x => x.IdAccionFormulario == accionFormularioDTO.Id && x.IdCategoriaOrigen == item);
                        else
                        {
                            accionFormularioPorCategoriaOrigenBO = new AccionFormularioPorCategoriaOrigenBO();
                            accionFormularioPorCategoriaOrigenBO.Estado = true;
                            accionFormularioPorCategoriaOrigenBO.UsuarioCreacion = accionFormularioDTO.Usuario;
                            accionFormularioPorCategoriaOrigenBO.FechaCreacion = DateTime.Now;
                        }
                        accionFormularioPorCategoriaOrigenBO.IdCategoriaOrigen = item;
                        accionFormularioPorCategoriaOrigenBO.UsuarioModificacion = accionFormularioDTO.Usuario;
                        accionFormularioPorCategoriaOrigenBO.FechaModificacion = DateTime.Now;

                        if (accionFormularioPorCategoriaOrigenBO.HasErrors) return BadRequest(accionFormularioPorCategoriaOrigenBO.ActualesErrores);
                        else accionFormularioBO.ListaCategoriaOrigen.Add(accionFormularioPorCategoriaOrigenBO);
                    }

                    accionFormularioBO.ListaCampoContacto = new List<AccionFormularioPorCampoContactoBO>();
                    AccionFormularioPorCampoContactoBO accionFormularioPorCampoContactoBO;
                    foreach (var item in accionFormularioDTO.ListaCampoContacto)
                    {
                        if (accionFormularioPorCampoContactoRepositorio.Exist(item.Id)) accionFormularioPorCampoContactoBO = accionFormularioPorCampoContactoRepositorio.FirstById(item.Id);
                        else
                        {
                            accionFormularioPorCampoContactoBO = new AccionFormularioPorCampoContactoBO();
                            accionFormularioPorCampoContactoBO.Estado = true;
                            accionFormularioPorCampoContactoBO.UsuarioCreacion = accionFormularioDTO.Usuario;
                            accionFormularioPorCampoContactoBO.FechaCreacion = DateTime.Now;
                        }
                        accionFormularioPorCampoContactoBO.IdCampoContacto = item.IdCampoContacto;
                        accionFormularioPorCampoContactoBO.Orden = item.Orden;
                        accionFormularioPorCampoContactoBO.Campo = "";
                        accionFormularioPorCampoContactoBO.EsSiempreVisible = item.EsSiempreVisible;
                        accionFormularioPorCampoContactoBO.EsInteligente = item.EsInteligente;
                        accionFormularioPorCampoContactoBO.TieneProbabilidad = item.TieneProbabilidad;
                        accionFormularioPorCampoContactoBO.UsuarioModificacion = accionFormularioDTO.Usuario;
                        accionFormularioPorCampoContactoBO.FechaModificacion = DateTime.Now;

                        if (accionFormularioPorCampoContactoBO.HasErrors) return BadRequest(accionFormularioPorCampoContactoBO.ActualesErrores);
                        else accionFormularioBO.ListaCampoContacto.Add(accionFormularioPorCampoContactoBO);
                    }

                    accionFormularioRepositorio.Update(accionFormularioBO);
                    scope.Complete();
                }
                return Ok(accionFormularioDTO.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO Json)
        {
            try
            {
                AccionFormularioRepositorio accionFormularioRepositorio = new AccionFormularioRepositorio(_integraDBContext);
                AccionFormularioPorCategoriaOrigenRepositorio accionFormularioPorCategoriaOrigenRepositorio = new AccionFormularioPorCategoriaOrigenRepositorio(_integraDBContext);
                AccionFormularioPorCampoContactoRepositorio accionFormularioPorCampoContactoRepositorio = new AccionFormularioPorCampoContactoRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    accionFormularioRepositorio.Delete(Json.Id, Json.NombreUsuario);

                    List<AccionFormularioPorCategoriaOrigenBO> listaCategoriaOrigen = accionFormularioPorCategoriaOrigenRepositorio.GetBy(x => x.IdAccionFormulario == Json.Id).ToList();
                    foreach(var item in listaCategoriaOrigen)
                    {
                        accionFormularioPorCategoriaOrigenRepositorio.Delete(item.Id, Json.NombreUsuario);
                    }

                    List<AccionFormularioPorCampoContactoBO> listaCampoContacto = accionFormularioPorCampoContactoRepositorio.GetBy(x => x.IdAccionFormulario == Json.Id).ToList();
                    foreach (var item in listaCampoContacto)
                    {
                        accionFormularioPorCampoContactoRepositorio.Delete(item.Id, Json.NombreUsuario);
                    }

                    scope.Complete();
                }
                return Ok(Json.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}