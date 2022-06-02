using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/BeneficiosAlumnoPEspecifico")] //Este controlador hace referencia al BO principal AsignacionAutomaticaError
    public class BeneficiosAlumnoPEspecificoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public BeneficiosAlumnoPEspecificoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarBeneficios([FromBody]OportunidadCodigoMatriculaDTO OportunidadVerificada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDbContext = new integraDBContext();

                BeneficiosAlumnoPEspecificoRepositorio repBeneficiosAlumnoPEspecifico = new BeneficiosAlumnoPEspecificoRepositorio(_integraDbContext);
                BeneficiosAlumnoPEspecificoBO beneficiosAlumnoPEspecifico = new BeneficiosAlumnoPEspecificoBO();

                var datosCodigoMatricula = repBeneficiosAlumnoPEspecifico.ObtenerDatosPorCodigoMatricula(OportunidadVerificada.CodigoMatricula).FirstOrDefault();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (datosCodigoMatricula == null)
                    {
                        var datosCodigoMatricula2 = repBeneficiosAlumnoPEspecifico.ObtenerDatosPorCodigoMatriculaSinPaquete(OportunidadVerificada.CodigoMatricula).FirstOrDefault();

                        beneficiosAlumnoPEspecifico.IdAlumno = datosCodigoMatricula2.IdAlumno;
                        beneficiosAlumnoPEspecifico.IdPgeneral = datosCodigoMatricula2.IdPGeneral;
                        beneficiosAlumnoPEspecifico.IdPespecifico = datosCodigoMatricula2.IdPEspecifico;
                        beneficiosAlumnoPEspecifico.IdMatriculaCabecera = datosCodigoMatricula2.IdMatriculaCabecera;
                        beneficiosAlumnoPEspecifico.Beneficios = "Sin beneficio (regularizar)";
                        beneficiosAlumnoPEspecifico.Estado = true;
                        beneficiosAlumnoPEspecifico.UsuarioCreacion = "Regularizado"; //OportunidadVerificada.Usuario;
                        beneficiosAlumnoPEspecifico.UsuarioModificacion = "Regularizado"; //OportunidadVerificada.Usuario;
                        beneficiosAlumnoPEspecifico.FechaCreacion = DateTime.Now;
                        beneficiosAlumnoPEspecifico.FechaModificacion = DateTime.Now;

                        repBeneficiosAlumnoPEspecifico.Insert(beneficiosAlumnoPEspecifico);
                    }
                    else if (datosCodigoMatricula.Paquete == null && datosCodigoMatricula.IdPEspecifico >= 0)
                    {
                        var beneficiosTipo2 = repBeneficiosAlumnoPEspecifico.ObtenerBeneficiosProgramaTipo2(datosCodigoMatricula.IdPGeneral).FirstOrDefault();

                        beneficiosAlumnoPEspecifico.IdAlumno = datosCodigoMatricula.IdAlumno;
                        beneficiosAlumnoPEspecifico.IdPgeneral = datosCodigoMatricula.IdPGeneral;
                        beneficiosAlumnoPEspecifico.IdPespecifico = datosCodigoMatricula.IdPEspecifico;
                        beneficiosAlumnoPEspecifico.IdMatriculaCabecera = datosCodigoMatricula.IdMatriculaCabecera;
                        beneficiosAlumnoPEspecifico.Beneficios = beneficiosTipo2.Titulo;
                        beneficiosAlumnoPEspecifico.Estado = true;
                        beneficiosAlumnoPEspecifico.UsuarioCreacion = "Regularizado"; //OportunidadVerificada.Usuario;
                        beneficiosAlumnoPEspecifico.UsuarioModificacion = "Regularizado"; //OportunidadVerificada.Usuario;
                        beneficiosAlumnoPEspecifico.FechaCreacion = DateTime.Now;
                        beneficiosAlumnoPEspecifico.FechaModificacion = DateTime.Now;

                        repBeneficiosAlumnoPEspecifico.Insert(beneficiosAlumnoPEspecifico);
                    }

                    else
                    {
                        var beneficiosTipo1 = repBeneficiosAlumnoPEspecifico.ObtenerBeneficiosProgramaTipo1(datosCodigoMatricula.IdPGeneral, datosCodigoMatricula.IdPais, datosCodigoMatricula.Paquete);


                        foreach (var beneficio in beneficiosTipo1)
                        {
                            BeneficiosAlumnoPEspecificoBO beneficiosAlumnoPEspecifico2 = new BeneficiosAlumnoPEspecificoBO();

                            beneficiosAlumnoPEspecifico2.IdAlumno = datosCodigoMatricula.IdAlumno;
                            beneficiosAlumnoPEspecifico2.IdPgeneral = datosCodigoMatricula.IdPGeneral;
                            beneficiosAlumnoPEspecifico2.IdPespecifico = datosCodigoMatricula.IdPEspecifico;
                            beneficiosAlumnoPEspecifico2.IdMatriculaCabecera = datosCodigoMatricula.IdMatriculaCabecera;
                            beneficiosAlumnoPEspecifico2.Beneficios = beneficio.Descripcion;
                            beneficiosAlumnoPEspecifico2.Estado = true;
                            beneficiosAlumnoPEspecifico2.UsuarioCreacion = "Regularizado"; //OportunidadVerificada.Usuario;
                            beneficiosAlumnoPEspecifico2.UsuarioModificacion = "Regularizado"; //OportunidadVerificada.Usuario;
                            beneficiosAlumnoPEspecifico2.FechaCreacion = DateTime.Now;
                            beneficiosAlumnoPEspecifico2.FechaModificacion = DateTime.Now;

                            repBeneficiosAlumnoPEspecifico.Insert(beneficiosAlumnoPEspecifico2);
                        }
                    }
                    scope.Complete();
                }
                return Ok(beneficiosAlumnoPEspecifico);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorBeneficiosAlumnoPEspecificoDTO : AbstractValidator<TAsignacionAutomaticaError>
    {
        public static ValidadorBeneficiosAlumnoPEspecificoDTO Current = new ValidadorBeneficiosAlumnoPEspecificoDTO();
        public ValidadorBeneficiosAlumnoPEspecificoDTO()
        {

            RuleFor(objeto => objeto.Campo).NotEmpty().WithMessage("Campo es Obligatorio")
                                            .NotNull().WithMessage("Campo es Obligatorio");
            RuleFor(objeto => objeto.Descripcion).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .NotNull().WithMessage("Descripcion es Obligatorio");
            RuleFor(objeto => objeto.IdContacto).NotEmpty().WithMessage("IdContacto es Obligatorio")
                                            .NotNull().WithMessage("IdContacto es Obligatorio");
            RuleFor(objeto => objeto.IdAsignacionAutomatica).NotEmpty().WithMessage("IdAsignacionAutomatica es Obligatorio")
                                            .NotNull().WithMessage("IdAsignacionAutomatica es Obligatorio");
            RuleFor(objeto => objeto.IdAsignacionAutomaticaTipoError).NotEmpty().WithMessage("IdTipoError es Obligatorio")
                                            .NotNull().WithMessage("IdTipoError es Obligatorio");

        }

    }
}
