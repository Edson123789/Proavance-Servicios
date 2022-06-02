using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.SCode.Repositorio;
using BSI.Integra.Aplicacion.Finanzas.BO;
using System.Transactions;
using System.Reflection;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/BeneficioLaboralVentas")]
    public class BeneficioLaboralVentasController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public BeneficioLaboralVentasController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarBeneficioLaboralPorPeriodo([FromBody] ListaBeneficioLaboralDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BeneficioLaboralPorPeriodoRepositorio _repBeneficioLaboralPeriodoRep = new BeneficioLaboralPorPeriodoRepositorio(_integraDBContext);
                BeneficioLaboralTipoRepositorio _repTipoBeneficioLaboralRep = new BeneficioLaboralTipoRepositorio(_integraDBContext);

                bool ExistePeriodoEnBeneficioLaboral = _repBeneficioLaboralPeriodoRep.GetBy(x => x.IdPeriodo == ObjetoDTO.IdPeriodo).ToList().Count() > 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var itemBeneficio in ObjetoDTO.ListaBeneficiados)
                    {
                        if (!ExistePeriodoEnBeneficioLaboral)
                        {

                            foreach (var _property in itemBeneficio.GetType().GetProperties()
                                .Where(x => x.PropertyType == typeof(decimal)))
                            {
                                var prop = itemBeneficio.GetType().GetProperty(_property.Name);
                                Decimal MontoDecimal = Convert.ToDecimal(prop.GetValue(itemBeneficio, null).ToString());

                                var IdBeneficio = _repTipoBeneficioLaboralRep.GetBy(x => x.Nombre.Replace(" ", "").Equals(_property.Name)).Select(x => x.Id).FirstOrDefault();

                                BeneficioLaboralPorPeriodoBO beneficioBO = new BeneficioLaboralPorPeriodoBO();

                                beneficioBO.Estado = true;
                                beneficioBO.UsuarioCreacion = ObjetoDTO.UsuarioModificacion;
                                beneficioBO.UsuarioModificacion = ObjetoDTO.UsuarioModificacion;
                                beneficioBO.FechaCreacion = DateTime.Now;
                                beneficioBO.FechaModificacion = DateTime.Now;
                                beneficioBO.IdAgendaTipoUsuario = itemBeneficio.IdAgendaTipoUsuario;
                                beneficioBO.IdPeriodo = ObjetoDTO.IdPeriodo;
                                beneficioBO.IdBeneficioLaboralTipo = IdBeneficio;
                                beneficioBO.Monto = MontoDecimal;

                                _repBeneficioLaboralPeriodoRep.Insert(beneficioBO);
                            }
                        }
                        else {
                            foreach (var _property in itemBeneficio.GetType().GetProperties()
                                .Where(x => x.PropertyType == typeof(decimal)))
                            {
                                var prop = itemBeneficio.GetType().GetProperty(_property.Name);
                                Decimal MontoDecimal = Convert.ToDecimal(prop.GetValue(itemBeneficio, null).ToString());

                                var IdBeneficio = _repTipoBeneficioLaboralRep.GetBy(x => x.Nombre.Replace(" ", "").Equals(_property.Name)).Select(x => x.Id).FirstOrDefault();

                                BeneficioLaboralPorPeriodoBO beneficioBO = _repBeneficioLaboralPeriodoRep.GetBy(x=>x.IdPeriodo==ObjetoDTO.IdPeriodo && x.IdAgendaTipoUsuario==itemBeneficio.IdAgendaTipoUsuario && x.IdBeneficioLaboralTipo==IdBeneficio).FirstOrDefault();

                                beneficioBO.Estado = true;
                                beneficioBO.UsuarioModificacion = ObjetoDTO.UsuarioModificacion;
                                beneficioBO.FechaModificacion = DateTime.Now;
                                beneficioBO.Monto = MontoDecimal;

                                _repBeneficioLaboralPeriodoRep.Update(beneficioBO);
                            }
                        }
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]/{IdPeriodo}")]
        [HttpGet]
        public ActionResult ObtenerBeneficioLaboralSegunPeriodo(int IdPeriodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                BeneficioLaboralPorPeriodoRepositorio repBeneficioLaboralPeriodoRep = new BeneficioLaboralPorPeriodoRepositorio(_integraDBContext);

                var listadoBeneficioLaboralVentas = repBeneficioLaboralPeriodoRep.ObtenerBeneficioLaboralVentasPorPeriodo(IdPeriodo);
                if (listadoBeneficioLaboralVentas.Count()==0)
                {
                    return Ok(null);
                }
                return Ok(listadoBeneficioLaboralVentas);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

    }
}