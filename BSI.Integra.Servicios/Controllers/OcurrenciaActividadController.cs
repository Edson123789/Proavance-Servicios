using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/OcurrenciaActividad")]
    public class OcurrenciaActividadController : BaseController<TOcurrenciaActividad, ValidadorOcurrenciaActividadDTO>
    {
        public OcurrenciaActividadController(IIntegraRepository<TOcurrenciaActividad> repositorio, ILogger<BaseController<TOcurrenciaActividad, ValidadorOcurrenciaActividadDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarOcurrenciaActividad([FromBody]OcurrenciaActividadDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OcurrenciaActividadRepositorio _repOcurrenciaActividad = new OcurrenciaActividadRepositorio();
                OcurrenciaActividadBO ocurrenciaActividadBO = new OcurrenciaActividadBO();

                ocurrenciaActividadBO.IdOcurrencia = Objeto.IdOcurrencia.Value;
                ocurrenciaActividadBO.IdActividadCabecera = Objeto.IdActividadCabecera.Value;
                ocurrenciaActividadBO.IdOcurrenciaActividadPadre = Objeto.IdOcurrenciaActividadPadre.Value;
                ocurrenciaActividadBO.NodoPadre = Objeto.NodoPadre.Value;
                ocurrenciaActividadBO.UsuarioModificacion = Objeto.Usuario;
                ocurrenciaActividadBO.UsuarioCreacion = Objeto.Usuario;
                ocurrenciaActividadBO.FechaCreacion = DateTime.Now;
                ocurrenciaActividadBO.FechaModificacion = DateTime.Now;
                ocurrenciaActividadBO.Estado = true;
                if (!ocurrenciaActividadBO.HasErrors)
                    _repOcurrenciaActividad.Insert(ocurrenciaActividadBO);
                else
                    return BadRequest(ocurrenciaActividadBO.GetErrors(null));
                if (Objeto.IdOcurrenciaActividadPadre.Value != 0)
                {
                    var OcurrenciaPadre = _repOcurrenciaActividad.FirstById(Objeto.IdOcurrenciaActividadPadre.Value);
                    OcurrenciaPadre.NodoPadre = true;
                    _repOcurrenciaActividad.Update(OcurrenciaPadre);
                }
                

                return Ok(ocurrenciaActividadBO);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarOcurrenciaActividad([FromBody]EliminarDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                OcurrenciaActividadRepositorio _repOcurrenciaActividad = new OcurrenciaActividadRepositorio(contexto);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(contexto);
                using (TransactionScope scope = new TransactionScope())
                {
                    var Registro = _repOcurrenciaActividad.FirstById(Objeto.Id);
                    _repOcurrenciaActividad.Delete(Objeto.Id, Objeto.NombreUsuario);
                    if (_repOcurrenciaActividad.Exist(w => w.Id == Registro.IdOcurrenciaActividadPadre))
                    {
                        var OcurrenciaPadre = _repOcurrenciaActividad.FirstBy(w => w.Id == Registro.IdOcurrenciaActividadPadre);
                        var lista = _repOcurrencia.ObtenerTodasOcurrenciasActividadPadre(OcurrenciaPadre.Id, OcurrenciaPadre.IdActividadCabecera);
                        if (lista.Count() == 0)
                        {
                            OcurrenciaPadre.NodoPadre = false;
                            _repOcurrenciaActividad.Update(OcurrenciaPadre);
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

    public class ValidadorOcurrenciaActividadDTO : AbstractValidator<TOcurrenciaActividad>
    {
        public static ValidadorOcurrenciaActividadDTO Current = new ValidadorOcurrenciaActividadDTO();
        public ValidadorOcurrenciaActividadDTO()
        {
            RuleFor(objeto => objeto.IdOcurrencia).NotEmpty().WithMessage("IdOcurrencia es Obligatorio")
                                                    .NotNull().WithMessage("IdOcurrencia no permite datos nulos");

            RuleFor(objeto => objeto.IdActividadCabecera).NotEmpty().WithMessage("IdActividadCabecera es Obligatorio")
                                                    .NotNull().WithMessage("IdActividadCabecera no permite datos nulos");

            RuleFor(objeto => objeto.IdOcurrenciaActividadPadre).NotEmpty().WithMessage("IdOcurrenciaActividadPadre es Obligatorio")
                                                    .NotNull().WithMessage("IdOcurrenciaActividadPadre no permite datos nulos");

            RuleFor(objeto => objeto.NodoPadre).NotEmpty().WithMessage("NodoPadre es Obligatorio");
        }
    }
}
