using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/OportunidadTasaConversionHistorica")]
    public class OportunidadTasaConversionHistoricaController : BaseController<TOportunidadTasaConversionHistorica, ValidadorOportunidadTasaConversionHistoricaDTO>
    {
        public OportunidadTasaConversionHistoricaController(IIntegraRepository<TOportunidadTasaConversionHistorica> repositorio, ILogger<BaseController<TOportunidadTasaConversionHistorica, ValidadorOportunidadTasaConversionHistoricaDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorOportunidadTasaConversionHistoricaDTO : AbstractValidator<TOportunidadTasaConversionHistorica>
    {
        public static ValidadorOportunidadTasaConversionHistoricaDTO Current = new ValidadorOportunidadTasaConversionHistoricaDTO();
        public ValidadorOportunidadTasaConversionHistoricaDTO()
        {
            RuleFor(objeto => objeto.IdArea).NotEmpty().WithMessage("IdOportunidadCompetidor es Obligatorio")
                                                    .NotNull().WithMessage("IdOportunidadCompetidor no permite datos nulos");

            RuleFor(objeto => objeto.IdSubArea).NotEmpty().WithMessage("IdSubArea es Obligatorio")
                                                    .NotNull().WithMessage("IdSubArea no permite datos nulos");

            RuleFor(objeto => objeto.IdPgeneral).NotEmpty().WithMessage("IdPgeneral es Obligatorio")
                                                    .NotNull().WithMessage("IdPgeneral no permite datos nulos");

            RuleFor(objeto => objeto.IdPespecifico).NotEmpty().WithMessage("IdPespecifico es Obligatorio")
                                                    .NotNull().WithMessage("IdPespecifico no permite datos nulos");

            RuleFor(objeto => objeto.NombreContacto).NotEmpty().WithMessage("NombreContacto es Obligatorio")
                                                    .NotNull().WithMessage("NombreContacto no permite datos nulos");

            RuleFor(objeto => objeto.IdcategoriaDato).NotEmpty().WithMessage("IdcategoriaDato es Obligatorio")
                                                    .NotNull().WithMessage("IdcategoriaDato no permite datos nulos");

            RuleFor(objeto => objeto.ValorCategoriaD).NotEmpty().WithMessage("ValorCategoriaD es Obligatorio")
                                                    .NotNull().WithMessage("ValorCategoriaD no permite datos nulos");

            RuleFor(objeto => objeto.IdAformacion).NotEmpty().WithMessage("IdAformacion es Obligatorio")
                                                    .NotNull().WithMessage("IdAformacion no permite datos nulos");

            RuleFor(objeto => objeto.ValorAformacion).NotEmpty().WithMessage("ValorAformacion es Obligatorio")
                                                    .NotNull().WithMessage("ValorAformacion no permite datos nulos");

            RuleFor(objeto => objeto.IdCargo).NotEmpty().WithMessage("IdCargo es Obligatorio")
                                                    .NotNull().WithMessage("IdCargo no permite datos nulos");

            RuleFor(objeto => objeto.ValorCargo).NotEmpty().WithMessage("ValorCargo es Obligatorio")
                                                    .NotNull().WithMessage("ValorCargo no permite datos nulos");

            RuleFor(objeto => objeto.IdAtrabajo).NotEmpty().WithMessage("IdAtrabajo es Obligatorio")
                                                    .NotNull().WithMessage("IdAtrabajo no permite datos nulos");

            RuleFor(objeto => objeto.ValorAtrabajo).NotEmpty().WithMessage("ValorAtrabajo es Obligatorio")
                                                    .NotNull().WithMessage("ValorAtrabajo no permite datos nulos");

            RuleFor(objeto => objeto.IdIndustria).NotEmpty().WithMessage("IdIndustria es Obligatorio")
                                                    .NotNull().WithMessage("IdIndustria no permite datos nulos");

            RuleFor(objeto => objeto.ValorIndustria).NotEmpty().WithMessage("ValorIndustria es Obligatorio")
                                                    .NotNull().WithMessage("ValorIndustria no permite datos nulos");

            RuleFor(objeto => objeto.IdPais).NotEmpty().WithMessage("IdPais es Obligatorio")
                                                    .NotNull().WithMessage("IdPais no permite datos nulos");

            RuleFor(objeto => objeto.ValorPais).NotEmpty().WithMessage("ValorPais es Obligatorio")
                                                    .NotNull().WithMessage("ValorPais no permite datos nulos");

            RuleFor(objeto => objeto.SumaModelo).NotEmpty().WithMessage("SumaModelo es Obligatorio")
                                                    .NotNull().WithMessage("SumaModelo no permite datos nulos");

            RuleFor(objeto => objeto.Probabilidad).NotEmpty().WithMessage("Probabilidad es Obligatorio")
                                                    .NotNull().WithMessage("Probabilidad no permite datos nulos");

            RuleFor(objeto => objeto.ProbabilidaDesc).NotEmpty().WithMessage("ProbabilidaDesc es Obligatorio")
                                                    .NotNull().WithMessage("ProbabilidaDesc no permite datos nulos");
        }
    }

}
