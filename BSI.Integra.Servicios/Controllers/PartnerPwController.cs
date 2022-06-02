using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
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
using System.Threading.Tasks;
using System.Transactions;
using static BSI.Integra.Servicios.Controllers.PartnerPwController;
using static BSI.Integra.Servicios.Controllers.ProveedorCampaniaIntegraController;
using static BSI.Integra.Servicios.Controllers.TerminoUsoSitioWebPwController;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PartnerPw")]
    [ApiController]
    public class PartnerPwController : BaseController <TPartnerPw, ValidadorPartnerPwDTO>
    {
        public PartnerPwController(IIntegraRepository<TPartnerPw> repositorio, ILogger<BaseController<TPartnerPw, ValidadorPartnerPwDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {

        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                PartnerPwRepositorio partnerPwRepositorio = new PartnerPwRepositorio();
                return Ok(partnerPwRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPartner}")]
        [HttpGet]
        public ActionResult ObtenerRegistroPartner(int IdPartner)
        {
            try
            {
                PartnerRegistrosPwDTO _registros = new PartnerRegistrosPwDTO();

                PartnerBeneficioPwRepositorio partnerBeneficioPwRepositorio = new PartnerBeneficioPwRepositorio();
                PartnerContactoPwRepositorio partnerContactoPwRepositorio = new PartnerContactoPwRepositorio();

                var _beneficio = partnerBeneficioPwRepositorio.ObtenerPartnerBeneficio(IdPartner);
                var _contacto = partnerContactoPwRepositorio.ObtenerPartnerContacto(IdPartner);

                if (_beneficio != null)
                {
                    _registros.listaPartnerBeneficio = _beneficio;
                }
                if (_contacto != null)
                {
                    _registros.listaPartnerContacto = _contacto;
                }

                return Ok(_registros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar(PartnerPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PartnerPwRepositorio partnerPwRepositorio = new PartnerPwRepositorio();
                PartnerBeneficioPwRepositorio partnerBeneficioPwRepositorio = new PartnerBeneficioPwRepositorio();
                PartnerContactoPwRepositorio partnerContactoPwRepositorio = new PartnerContactoPwRepositorio();

                PartnerPwBO partnerPwBO = new PartnerPwBO();
                partnerPwBO.Nombre = Json.Nombre;
                partnerPwBO.ImgPrincipal = Json.ImgPrincipal;
                partnerPwBO.ImgPrincipalAlf = Json.ImgPrincipalAlf;
                partnerPwBO.ImgSecundaria = Json.ImgSecundaria;
                partnerPwBO.ImgSecundariaAlf = Json.ImgSecundariaAlf;
                partnerPwBO.Descripcion = Json.Descripcion;
                partnerPwBO.DescripcionCorta = Json.DescripcionCorta;
                partnerPwBO.Preguntas = Json.Preguntas;
                partnerPwBO.Posicion = Json.Posicion;
                partnerPwBO.IdPartner = Json.IdPartner;
                partnerPwBO.EncabezadoCorreoPartner = Json.EncabezadoCorreoPartner;
                partnerPwBO.Estado = true;
                partnerPwBO.UsuarioCreacion = Json.Usuario;
                partnerPwBO.UsuarioModificacion = Json.Usuario;
                partnerPwBO.FechaCreacion = DateTime.Now;
                partnerPwBO.FechaModificacion = DateTime.Now;

                var respuesta = partnerPwRepositorio.Insert(partnerPwBO);

                if (respuesta)
                {
                    foreach (var item in Json.listaPartnerBeneficio)
                    {
                        PartnerBeneficioPwBO partnerBenficio = new PartnerBeneficioPwBO();
                        partnerBenficio.IdPartner = partnerPwBO.Id;
                        partnerBenficio.Descripcion = item.Descripcion;
                        partnerBenficio.Estado = true;
                        partnerBenficio.UsuarioCreacion = Json.Usuario;
                        partnerBenficio.UsuarioModificacion = Json.Usuario;
                        partnerBenficio.FechaCreacion = DateTime.Now;
                        partnerBenficio.FechaModificacion = DateTime.Now;
                        partnerBeneficioPwRepositorio.Insert(partnerBenficio);
                    }

                    foreach (var item in Json.listaPartnerContacto)
                    {
                        PartnerContactoPwBO partnerContacto = new PartnerContactoPwBO();
                        partnerContacto.IdPartner = partnerPwBO.Id;
                        partnerContacto.Nombres = item.Nombres;
                        partnerContacto.Apellidos = item.Apellidos;
                        partnerContacto.Email1 = item.Email1;
                        partnerContacto.Email2 = item.Email2;
                        partnerContacto.Telefono1 = item.Telefono1;
                        partnerContacto.Telefono2 = item.Telefono2;
                        partnerContacto.Estado = true;
                        partnerContacto.UsuarioCreacion = Json.Usuario;
                        partnerContacto.UsuarioModificacion = Json.Usuario;
                        partnerContacto.FechaCreacion = DateTime.Now;
                        partnerContacto.FechaModificacion = DateTime.Now;
                        partnerContactoPwRepositorio.Insert(partnerContacto);
                    }
                }

                

                //return Ok(partnerPwRepositorio.Insert(partnerPwBO));
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar(PartnerPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PartnerPwRepositorio partnerPwRepositorio = new PartnerPwRepositorio();
                PartnerBeneficioPwRepositorio partnerBeneficioPwRepositorio = new PartnerBeneficioPwRepositorio();
                PartnerContactoPwRepositorio partnerContactoPwRepositorio = new PartnerContactoPwRepositorio();

                PartnerPwBO partnerPwBO = partnerPwRepositorio.FirstById(Json.Id);
                partnerPwBO.Nombre = Json.Nombre;
                partnerPwBO.ImgPrincipal = Json.ImgPrincipal;
                partnerPwBO.ImgPrincipalAlf = Json.ImgPrincipalAlf;
                partnerPwBO.ImgSecundaria = Json.ImgSecundaria;
                partnerPwBO.ImgSecundariaAlf = Json.ImgSecundariaAlf;
                partnerPwBO.Descripcion = Json.Descripcion;
                partnerPwBO.DescripcionCorta = Json.DescripcionCorta;
                partnerPwBO.Preguntas = Json.Preguntas;
                partnerPwBO.Posicion = Json.Posicion;
                partnerPwBO.IdPartner = Json.IdPartner;
                partnerPwBO.EncabezadoCorreoPartner = Json.EncabezadoCorreoPartner;
                partnerPwBO.UsuarioModificacion = Json.Usuario;
                partnerPwBO.FechaModificacion = DateTime.Now;

                var respuesta = partnerPwRepositorio.Update(partnerPwBO);

                if (respuesta)
                {
                    foreach (var item in Json.listaPartnerBeneficio)
                    {
                        if (item.IdPartner == 0) //nuevo registro
                        {
                            PartnerBeneficioPwBO partnerBenficio = new PartnerBeneficioPwBO();
                            partnerBenficio.IdPartner = partnerPwBO.Id;
                            partnerBenficio.Descripcion = item.Descripcion;
                            partnerBenficio.Estado = true;
                            partnerBenficio.UsuarioCreacion = Json.Usuario;
                            partnerBenficio.UsuarioModificacion = Json.Usuario;
                            partnerBenficio.FechaCreacion = DateTime.Now;
                            partnerBenficio.FechaModificacion = DateTime.Now;
                            partnerBeneficioPwRepositorio.Insert(partnerBenficio);
                        }
                        else
                        {
                            if (item.Estado == false) //eliminar registro
                            {
                                if (partnerBeneficioPwRepositorio.Exist(item.Id))
                                {
                                    partnerBeneficioPwRepositorio.Delete(item.Id, Json.Usuario);
                                }
                            }
                            else //actualizar
                            {
                                PartnerBeneficioPwBO partnerBenficio = partnerBeneficioPwRepositorio.FirstById(item.Id);
                                partnerBenficio.Descripcion = item.Descripcion;
                                partnerBenficio.Estado = true;
                                partnerBenficio.UsuarioModificacion = Json.Usuario;
                                partnerBenficio.FechaModificacion = DateTime.Now;
                                partnerBeneficioPwRepositorio.Update(partnerBenficio);
                            }
                        }

                    }

                    foreach (var item in Json.listaPartnerContacto)
                    {
                        if (item.IdPartner == 0) //nuevo registro
                        {
                            PartnerContactoPwBO partnerContacto = new PartnerContactoPwBO();
                            partnerContacto.IdPartner = partnerPwBO.Id;
                            partnerContacto.Nombres = item.Nombres;
                            partnerContacto.Apellidos = item.Apellidos;
                            partnerContacto.Email1 = item.Email1;
                            partnerContacto.Email2 = item.Email2;
                            partnerContacto.Telefono1 = item.Telefono1;
                            partnerContacto.Telefono2 = item.Telefono2;
                            partnerContacto.Estado = true;
                            partnerContacto.UsuarioCreacion = Json.Usuario;
                            partnerContacto.UsuarioModificacion = Json.Usuario;
                            partnerContacto.FechaCreacion = DateTime.Now;
                            partnerContacto.FechaModificacion = DateTime.Now;
                            partnerContactoPwRepositorio.Insert(partnerContacto);
                        }
                        else
                        {
                            if (item.Estado == false) //eliminar registro
                            {
                                if (partnerContactoPwRepositorio.Exist(item.Id))
                                {
                                    partnerContactoPwRepositorio.Delete(item.Id, Json.Usuario);
                                }
                                
                            }
                            else //actualizar
                            {
                                PartnerContactoPwBO partnerContacto = partnerContactoPwRepositorio.FirstById(item.Id);
                                partnerContacto.Nombres = item.Nombres;
                                partnerContacto.Apellidos = item.Apellidos;
                                partnerContacto.Email1 = item.Email1;
                                partnerContacto.Email2 = item.Email2;
                                partnerContacto.Telefono1 = item.Telefono1;
                                partnerContacto.Telefono2 = item.Telefono2;
                                partnerContacto.Estado = true;
                                partnerContacto.UsuarioModificacion = Json.Usuario;
                                partnerContacto.FechaModificacion = DateTime.Now;
                                partnerContactoPwRepositorio.Update(partnerContacto);
                            }
                        }

                    }
                }

                

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar(PartnerPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PartnerPwRepositorio partnerPwRepositorio = new PartnerPwRepositorio();
                PartnerBeneficioPwRepositorio partnerBeneficioPwRepositorio = new PartnerBeneficioPwRepositorio();
                PartnerContactoPwRepositorio partnerContactoPwRepositorio = new PartnerContactoPwRepositorio();

                var _beneficio = partnerBeneficioPwRepositorio.ObtenerPartnerBeneficio(Json.Id);
                var _contacto = partnerContactoPwRepositorio.ObtenerPartnerContacto(Json.Id);

                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (partnerPwRepositorio.Exist(Json.Id))
                    {
                        
                        estadoEliminacion = partnerPwRepositorio.Delete(Json.Id, Json.Usuario);
                        
                    }
                    
                    scope.Complete();
                }
                if (estadoEliminacion)
                {
                    foreach (var item in _beneficio)
                    {
                        partnerBeneficioPwRepositorio.Delete(item.Id, Json.Usuario);
                    }

                    foreach (var item in _contacto)
                    {
                        partnerContactoPwRepositorio.Delete(item.Id, Json.Usuario);
                    }
                }
                return Ok(estadoEliminacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public class ValidadorPartnerPwDTO : AbstractValidator<TPartnerPw>
        {
            public static ValidadorPartnerPwDTO Current = new ValidadorPartnerPwDTO();
            public ValidadorPartnerPwDTO()
            {
                RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio");

            }
        }
    }
}
