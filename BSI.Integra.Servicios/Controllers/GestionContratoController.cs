using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/GestionContrato")]
    public class GestionContratoController : ControllerBase
    {       
        private readonly integraDBContext _integraDBContext;

        private readonly DatoContratoPersonalRepositorio _repDatoContratoPersonal;

        private readonly PersonalAreaTrabajoRepositorio _repAreaTrabajo;

        private readonly TipoContratoRepositorio _repTipoContrato;
        private readonly PuestoTrabajoRepositorio _repPuestoTrabajo;
        private readonly PersonalRepositorio _repPersonal;
        private readonly SedeTrabajoRepositorio _repSedeTrabajo;
        private readonly CargoRepositorio _repCargo;
        private readonly TipoPerfilRepositorio _repTipoPerfil;
        private readonly EntidadFinancieraRepositorio _repEntidadFinanciera;
        private readonly TipoPagoRepositorio _repTipoPago;

        private readonly PersonalExperienciaRepositorio _repExperiencia;
        private readonly PersonalFormacionRepositorio _repPersonalFormacion;
        private readonly PersonalIdiomaRepositorio _repPersonalIdioma;
        private readonly ContratoEstadoRepositorio _repContratoEstado;
        private readonly DatoContratoComisionBonoRepositorio _repDatoContratoComisionBono;
        private readonly PuestoTrabajoRemuneracionRepositorio _repPuestoTrabajoRemuneracion;
        private readonly PuestoTrabajoRemuneracionDetalleRepositorio _repPuestoTrabajoRemuneracionVariable;

        public GestionContratoController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;

            _repDatoContratoPersonal = new DatoContratoPersonalRepositorio(_integraDBContext);

            _repAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);

            _repTipoContrato = new TipoContratoRepositorio(_integraDBContext);
            _repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repSedeTrabajo = new SedeTrabajoRepositorio(_integraDBContext);
            _repCargo = new CargoRepositorio(_integraDBContext);
            _repTipoPerfil = new TipoPerfilRepositorio(_integraDBContext);
            _repEntidadFinanciera = new EntidadFinancieraRepositorio(_integraDBContext);
            _repTipoPago = new TipoPagoRepositorio(_integraDBContext);
            _repExperiencia = new PersonalExperienciaRepositorio(_integraDBContext);
            _repPersonalFormacion = new PersonalFormacionRepositorio(_integraDBContext);
            _repPersonalIdioma = new PersonalIdiomaRepositorio(_integraDBContext);
            _repContratoEstado = new ContratoEstadoRepositorio(_integraDBContext);
            _repDatoContratoComisionBono = new DatoContratoComisionBonoRepositorio(_integraDBContext);
            _repPuestoTrabajoRemuneracion = new PuestoTrabajoRemuneracionRepositorio(_integraDBContext);
            _repPuestoTrabajoRemuneracionVariable = new PuestoTrabajoRemuneracionDetalleRepositorio(_integraDBContext);
        }

		[HttpPost]
		[Route("[Action]")]
		public ActionResult GetPersonalAutocomplete([FromBody] Dictionary<string, string> Filtros)
		{
			try
			{
				if (Filtros != null)
				{
					return Ok(_repPersonal.CargarPersonalAutoCompleteContrato(Filtros["valor"].ToString()));
				}
				else
				{
					List<FiltroDTO> lista = new List<FiltroDTO>();
					return Ok(lista);
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerContratosRegistrados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaContratos = _repDatoContratoPersonal.ObtenerContratosRegistrados();
                return Ok(listaContratos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerRemuneracionVariableDisplay([FromBody] int IdPuestoTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var PTRemuneracion = _repPuestoTrabajoRemuneracion.FirstBy(x => x.IdPuestoTrabajo == IdPuestoTrabajo);
                if(PTRemuneracion != null)
                {
                    var ListaPTRemuneracionVariable = _repPuestoTrabajoRemuneracionVariable.GetBy(x => x.IdPuestoTrabajoRemuneracion == PTRemuneracion.Id).ToList();
                    var obj = new
                    {
                        PTRemuneracion,
                        ListaPTRemuneracionVariable,
                    };
                    return Ok(obj);
                }
                else
                {
                    var obj = new
                    {
                        PTRemuneracion,                        
                    };
                    return Ok(obj);
                }                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerContratosHistoricos([FromBody] int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaContratos = _repDatoContratoPersonal.ObtenerContratoHistorico(IdPersonal);
                var agrupado = listaContratos.GroupBy(x => new { x.Id, x.IdPersonal,x.Nombres,x.Apellidos,x.TipoContrato,x.EstadoContrato,x.FechaInicio,x.FechaFin,
                    x.RemuneracionFija,x.PuestoTrabajo,x.SedeTrabajo,x.PersonalAreaTrabajo,x.Cargo,x.ContratoEstado,x.Estado}).Select(g => new ContratoHistoricoRegistroRDTO
                {
                    Id = g.Key.Id.Value,
                    IdPersonal = g.Key.IdPersonal,
                    Nombres = g.Key.Nombres,
                    Apellidos = g.Key.Apellidos,
                    TipoContrato = g.Key.TipoContrato,
                    EstadoContrato = g.Key.EstadoContrato,
                    FechaInicio = g.Key.FechaInicio,
                    FechaFin = g.Key.FechaFin,
                    RemuneracionFija = g.Key.RemuneracionFija,
                    PuestoTrabajo = g.Key.PuestoTrabajo,
                    SedeTrabajo = g.Key.SedeTrabajo,
                    PersonalAreaTrabajo = g.Key.PersonalAreaTrabajo,
                    Cargo = g.Key.Cargo,
                    ContratoEstado = g.Key.ContratoEstado,
                    Estado = g.Key.Estado,
                    ListaRemuneracionVariable = g.GroupBy(y => new { y.Monto, y.Concepto, y.TipoRemuneracionVariable }).Select(y => new ContratoHistoricoRegistroRVDTO
                    {
                        Monto = y.Key.Monto,
                        Concepto = y.Key.Concepto,
                        TipoRemuneracionVariable = y.Key.TipoRemuneracionVariable,
                    }).ToList()

                }).ToList();

                return Ok(agrupado);                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerDataRemuneracionVariable([FromBody] int IdDatoControPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var rpta = _repDatoContratoComisionBono.ObtenerRegistros(IdDatoControPersonal);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarContrato([FromBody] DatoContratoPersonalDTO ContratoPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
				using (TransactionScope scope = new TransactionScope())
				{
					var listaContratoAnterior = _repDatoContratoPersonal.GetBy(x => x.IdPersonal == ContratoPersonal.IdPersonal && x.EstadoContrato == true).ToList();


                    foreach (var item in listaContratoAnterior) 
                    {
                        var listaRemuneracionVariable = _repDatoContratoComisionBono.GetBy(x => x.IdDatoContratoPersonal == item.Id).ToList();

                        foreach (var item2 in listaRemuneracionVariable)
                        {

                            _repDatoContratoComisionBono.Delete(item2.Id,ContratoPersonal.Usuario);
                        }
                        item.EstadoContrato = false;
                        item.UsuarioModificacion = ContratoPersonal.Usuario;
                        item.FechaModificacion = DateTime.Now;
                        _repDatoContratoPersonal.Update(item);
                    }

					DatoContratoPersonalBO contratoPersonal = new DatoContratoPersonalBO()
					{
						IdPersonal = ContratoPersonal.IdPersonal,
						IdTipoContrato = ContratoPersonal.IdTipoContrato,
						EstadoContrato = true,
						FechaInicio = ContratoPersonal.FechaInicio,
						FechaFin = ContratoPersonal.FechaFin,
						RemuneracionFija = ContratoPersonal.RemuneracionFija,
						IdPuestoTrabajo = ContratoPersonal.IdPuestoTrabajo,
						IdSedeTrabajo = ContratoPersonal.IdSedeTrabajo,
						IdPersonalAreaTrabajo = ContratoPersonal.IdPersonalAreaTrabajo,
						IdCargo = ContratoPersonal.IdCargo,
						IdContratoEstado = ContratoPersonal.IdContratoEstado,
						UrlDocumentoContrato = ContratoPersonal.UrlDocumentoContrato,
						Estado = true,
						UsuarioCreacion = ContratoPersonal.Usuario,
						UsuarioModificacion = ContratoPersonal.Usuario,
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now
					};
					var res = _repDatoContratoPersonal.Insert(contratoPersonal);



                    foreach (var item in ContratoPersonal.ListaRemuneracionVariable)
                    {
                        DatoContratoComisionBonoBO remuneracionVariable = new DatoContratoComisionBonoBO()
                        {
                            IdDatoContratoPersonal = contratoPersonal.Id,
                            TipoRemuneracionVariable = item.TipoRemuneracionVariable,
                            Concepto = item.Concepto,
                            Monto = item.Monto,
                            Estado = true,
                            UsuarioCreacion = ContratoPersonal.Usuario,
                            UsuarioModificacion = ContratoPersonal.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _repDatoContratoComisionBono.Insert(remuneracionVariable);
                    }

                    scope.Complete();
					return Ok(res);
				}
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /*
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarContrato([FromBody] CategoriaMoodleDTO CategoriaMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var categoriaMoodle = _repMoodleCategoria.FirstById(CategoriaMoodle.Id);

                categoriaMoodle.IdCategoriaMoodle = CategoriaMoodle.IdCategoriaMoodle;
                categoriaMoodle.NombreCategoria = CategoriaMoodle.NombreCategoria;
                categoriaMoodle.IdMoodleCategoriaTipo = CategoriaMoodle.IdMoodleCategoriaTipo;
                categoriaMoodle.UsuarioModificacion = CategoriaMoodle.Usuario;
                categoriaMoodle.FechaModificacion = DateTime.Now;

                var res = _repMoodleCategoria.Update(categoriaMoodle);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarContrato([FromBody] EliminarDTO CategoriaMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repMoodleCategoria.Exist(CategoriaMoodle.Id))
                {
                    var res = _repMoodleCategoria.Delete(CategoriaMoodle.Id, CategoriaMoodle.NombreUsuario);
                    return Ok(res);
                }
                else
                {
                    return BadRequest("La categoria a eliminar no existe o ya fue eliminada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        */
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var obj = new
                {
                    ListaAreaTrabajo = _repAreaTrabajo.ObtenerTodoFiltro(),
                    ListaTipoContrato = _repTipoContrato.ObtenerListaParaFiltro(),
                    ListaPuestoTrabajo = _repPuestoTrabajo.ObtenerFiltroPuestoTrabajo(),
                    //ListaJefeDirecto = _repPersonal.ObtenerPersonalFiltro(),
                    ListaSede = _repSedeTrabajo.ObtenerTodoFiltro(),
                    ListaCargo = _repCargo.ObtenerCargoFiltro(),
                    //ListaTipoPerfil = _repTipoPerfil.ObtenerListaParaFiltro(),
                    //ListaEntidadPago = _repEntidadFinanciera.ObtenerEntidadFinanciera(),
                    //ListaTipoPago = _repTipoPago.ObtenerFiltroTipoPago(),
                    ListaContratoEstado = _repContratoEstado.ObtenerContratoEstadoRegistrado(),

                };
                return Ok(obj);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Route("[Action]")]
        public ActionResult ObtenerContratosPorFiltro([FromBody] ContratoFiltroDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var contrato = _repDatoContratoPersonal.ObtenerContratoInformacion(Filtro);
                return Ok(contrato);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerPDF([FromBody] string ContratoPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DatoContratoPersonalRepositorio _repDatoContratoPersonal = new DatoContratoPersonalRepositorio();
                DatoContratoPersonalBO contratoPersonal = new DatoContratoPersonalBO();

                List<byte[]> listaPDFbytes = new List<byte[]>();
                //string listaEnterosString = String.Join(",", listaEnteros.ListaEnteros.Select(i => i.ToString()).ToArray());
                //var listaIngresoCajaDTO = repDatoContratoPersonal.ObtenerDatosCajaIngreso(listaEnteros.ListaEnteros.ToArray());

                var pdf = contratoPersonal.GenerarPDFDatoContratoPersonal(ContratoPersonal);

                //listaPDFbytes.Add(3);
                //NotaIngresoCajaDTO datosCajaIngreso=

                return Ok(pdf);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerDataFormulario([FromBody] int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                PersonalExperienciaRepositorio _repPersonalExperiencia = new PersonalExperienciaRepositorio();
                PersonalFormacionRepositorio _repPersonalFormacion = new PersonalFormacionRepositorio();
                PersonalIdiomaRepositorio _repPersonalIdioma = new PersonalIdiomaRepositorio();

                var obj = new
                {
                    ListaPersonal = _repPersonal.ObtenerPorPersonal(IdPersonal),
                    ListaPersonalExperiencia = _repPersonalExperiencia.ObtenerPorPersonal(IdPersonal),
                    ListaPersonalFormacion = _repPersonalFormacion.ObtenerPorPersonal(IdPersonal),
                    ListaPersonalIdioma = _repPersonalIdioma.ObtenerPorPersonal(IdPersonal),
                };
                return Ok(obj);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
