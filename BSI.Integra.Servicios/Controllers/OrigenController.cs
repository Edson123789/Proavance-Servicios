using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static BSI.Integra.Servicios.Controllers.OrigenController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: OrigenController
	/// Autor: Edgar S.
	/// Fecha: 30/04/2021
	/// <summary>
	/// Gestión de Origen de Oportunidades
	/// </summary>
	[Route("api/Origen")]
	public class OrigenController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		public OrigenController()
		{
			_integraDBContext = new integraDBContext();
		}

		[Route("[action]")]
		[HttpGet]
		public ActionResult ObtenerTodoFiltro()
		{
			try
			{
				OrigenBO origen = new OrigenBO();
				return Ok(origen.ObtenerTodoFiltro());
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult ObtenerOrigenes([FromBody] FiltroPaginadorDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				OrigenRepositorio _repOrigen = new OrigenRepositorio();
				var valor = true;
				var lista = _repOrigen.ListarOrigenes(Filtro);
				var registro = lista.FirstOrDefault();

				return Ok(new { Data = lista, Total = registro.Total });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerTarifarios()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenRepositorio _repOrigen = new OrigenRepositorio();
                var lista = _repOrigen.ObtenerTarifarios();
                
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{IdTarifario}")]
        [HttpGet]
        public IActionResult ObtenerTarifariosDetalles(int IdTarifario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenRepositorio _repOrigen = new OrigenRepositorio();
                var lista = _repOrigen.ObtenerTarifariosDetalles(IdTarifario);
				var agrupado = lista.GroupBy(x => new { x.IdTarifario,x.Concepto, x.Descripcion, x.Estados,x.SubEstados,x.TipoCantidad }).Select(g => new ConfiguracionTarifarioDetalleDTO
				{
					IdTarifario = g.Key.IdTarifario,
					Concepto = g.Key.Concepto,
					Descripcion =g.Key.Descripcion,
					Estados = g.Key.Estados,
					SubEstados = g.Key.SubEstados,
					TipoCantidad = g.Key.TipoCantidad,
					DetallePais = g.GroupBy(y => new { y.Id, y.IdPais, y.NombrePais, y.IdMoneda, y.NombrePlural, y.Monto,y.Simbolo }).Select(y => new ConfiguracionTarifarioDetallePorPaisDTO
					{
						Id = y.Key.Id,
						IdPais = y.Key.IdPais,
						NombrePais = y.Key.NombrePais,
						IdMoneda = y.Key.IdMoneda,
						NombrePlural = y.Key.NombrePlural,
						Monto = y.Key.Monto,
						Simbolo = y.Key.Simbolo
					}).ToList(),					

				}).ToList();
				return Ok(agrupado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerPaisesAsociados([FromBody] int IdTarifarioDetalle)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				OrigenRepositorio _repOrigen = new OrigenRepositorio();
				var lista = _repOrigen.ObtenerTarifariosDetallesPais(IdTarifarioDetalle);				
				return Ok(lista);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
        [HttpPost]
        public ActionResult InsertarTarifario([FromBody]TarifarioNuevoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string UsuarioTemp = Json.Usuario;
                OrigenRepositorio _repOrigen = new OrigenRepositorio(_integraDBContext);
				TarifarioDetalleAlternoRepositorio _repTarifarioDetalleAlterno = new TarifarioDetalleAlternoRepositorio(_integraDBContext);
                var lista = _repOrigen.InsertarTarifario(Json);

                foreach (var item in Json.Detalles)
                {
					

					foreach (var tmp in item.ListaIdPaises)
					{
						TarifarioDetalleAlternoBO tarifarioDetalle = new TarifarioDetalleAlternoBO();
						item.IdTarifario = lista.FirstOrDefault().Id;
						tarifarioDetalle.IdTarifario = item.IdTarifario;
						tarifarioDetalle.Concepto = item.Concepto;
						tarifarioDetalle.IdPais = tmp.IdPais;
						tarifarioDetalle.Monto = tmp.Monto;
						tarifarioDetalle.IdMoneda = tmp.IdMoneda;
						tarifarioDetalle.Descripcion = item.Descripcion;
						tarifarioDetalle.TipoCantidad = item.TipoCantidad;
						tarifarioDetalle.Estados = item.Estados;
						tarifarioDetalle.SubEstados = item.SubEstados;
						tarifarioDetalle.Estado = true;
						tarifarioDetalle.UsuarioCreacion = Json.Usuario;
						tarifarioDetalle.UsuarioModificacion = Json.Usuario;
						tarifarioDetalle.FechaCreacion = DateTime.Now;
						tarifarioDetalle.FechaModificacion = DateTime.Now;
						tarifarioDetalle.VisualizarPortalWeb = true;
						_repTarifarioDetalleAlterno.Insert(tarifarioDetalle);
					}
					//item.Usuario = UsuarioTemp;
                    //var detalles = _repOrigen.InsertarTarifarioDetalles(item);
                }

                //return Ok(lista);
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }



		[Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarTarifario([FromBody]TarifarioNuevoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int IdTarifarioTemp = Json.Id;
                string UsuarioTemp = Json.Usuario;
                OrigenRepositorio _repOrigen = new OrigenRepositorio();
				TarifarioDetalleAlternoRepositorio _repTarifarioDetalleAlterno = new TarifarioDetalleAlternoRepositorio(_integraDBContext);

				var lista = _repOrigen.ActualizarTarifario(Json);

                foreach (var item in Json.Detalles)
                {
					foreach (var tmp in item.ListaIdPaises)
					{
						if (tmp.Id > 0)
						{
							var tarifarioDetalle = _repTarifarioDetalleAlterno.FirstById(tmp.Id);
							tarifarioDetalle.IdTarifario = item.IdTarifario;
							tarifarioDetalle.Concepto = item.Concepto;
							tarifarioDetalle.IdPais = tmp.IdPais;
							tarifarioDetalle.Monto = tmp.Monto;
							tarifarioDetalle.IdMoneda = tmp.IdMoneda;
							tarifarioDetalle.Descripcion = item.Descripcion;
							tarifarioDetalle.TipoCantidad = item.TipoCantidad;
							tarifarioDetalle.Estados = item.Estados;
							tarifarioDetalle.SubEstados = item.SubEstados;														
							tarifarioDetalle.UsuarioModificacion = Json.Usuario;							
							tarifarioDetalle.FechaModificacion = DateTime.Now;
							_repTarifarioDetalleAlterno.Update(tarifarioDetalle);
						}
						else
						{
							TarifarioDetalleAlternoBO tarifarioDetalle = new TarifarioDetalleAlternoBO();							
							tarifarioDetalle.IdTarifario = IdTarifarioTemp;
							tarifarioDetalle.Concepto = item.Concepto;
							tarifarioDetalle.IdPais = tmp.IdPais;
							tarifarioDetalle.Monto = tmp.Monto;
							tarifarioDetalle.IdMoneda = tmp.IdMoneda;
							tarifarioDetalle.Descripcion = item.Descripcion;
							tarifarioDetalle.TipoCantidad = item.TipoCantidad;
							tarifarioDetalle.Estados = item.Estados;
							tarifarioDetalle.SubEstados = item.SubEstados;
							tarifarioDetalle.Estado = true;
							tarifarioDetalle.UsuarioCreacion = Json.Usuario;
							tarifarioDetalle.UsuarioModificacion = Json.Usuario;
							tarifarioDetalle.FechaCreacion = DateTime.Now;
							tarifarioDetalle.FechaModificacion = DateTime.Now;
							tarifarioDetalle.VisualizarPortalWeb = true;
							_repTarifarioDetalleAlterno.Insert(tarifarioDetalle);
						}
					}

                }
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]/{IdTarifario}/{Usuario}")]
        [HttpPost]
        public IActionResult EliminarTarifario(int IdTarifario, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenRepositorio _repOrigen = new OrigenRepositorio();
                var lista = _repOrigen.EliminarTarifario(IdTarifario, Usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

		[Route("[action]/{Concepto}/{Usuario}")]
		[HttpPost]
		public IActionResult EliminarTarifarioDetalle(string Concepto, string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				OrigenRepositorio _repOrigen = new OrigenRepositorio();
				var lista = _repOrigen.EliminarTarifarioDetalle(Concepto, Usuario);
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[Route("[action]/{Id}/{Usuario}")]
		[HttpPost]
		public IActionResult EliminarTarifarioDetallePais(int Id, string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				OrigenRepositorio _repOrigen = new OrigenRepositorio();
				var lista = _repOrigen.EliminarTarifarioDetallePais(Id, Usuario);
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerCombosOrigen()
		{

			try
			{
				CategoriaOrigenRepositorio repCategoriaOrigen = new CategoriaOrigenRepositorio();
				ComboOrigenDTO combos = new ComboOrigenDTO();
				combos.CategoriasOrigen = repCategoriaOrigen.ObtenerCategoriaFiltro();
				return Ok(combos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		public class ValidadoOrigenDTO : AbstractValidator<TOrigen>
		{
			public static ValidadoOrigenDTO Current = new ValidadoOrigenDTO();
			public ValidadoOrigenDTO()
			{
			}
		}

		[Route("[action]")]
		[HttpPut]
		public IActionResult ActualizarOrigen([FromBody] CompuestoOrigenDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				OrigenRepositorio repOrigen = new OrigenRepositorio();
				OrigenBO origen = new OrigenBO();
				using (TransactionScope scope = new TransactionScope())
				{
					if (repOrigen.Exist(Json.Origen.Id))
					{
						origen = repOrigen.FirstById(Json.Origen.Id);
						origen.Nombre = Json.Origen.Nombre;
						origen.Descripcion = Json.Origen.Descripcion;
						origen.IdTipodato = Json.Origen.IdTipoDato;
						origen.Prioridad = Json.Origen.Prioridad;
						origen.UsuarioModificacion = Json.Usuario;
						origen.FechaModificacion = DateTime.Now;
						repOrigen.Update(origen);
					}
					scope.Complete();
					Json.Origen.Id = origen.Id;
				}

				return Ok(Json.Origen);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]/{IdOrigen}/{Usuario}")]
		[HttpDelete]
		public IActionResult EliminarOrigen(int IdOrigen, string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				OrigenRepositorio repOrigen = new OrigenRepositorio();
				using (TransactionScope scope = new TransactionScope())
				{
					if (repOrigen.Exist(IdOrigen))
					{
						repOrigen.Delete(IdOrigen, Usuario);
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

		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarOrigen([FromBody] CompuestoOrigenDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				OrigenRepositorio repOrigen = new OrigenRepositorio();
				OrigenBO origen = new OrigenBO();
				using (TransactionScope scope = new TransactionScope())
				{
					
					origen.Nombre = Json.Origen.Nombre;
					origen.Descripcion = Json.Origen.Descripcion;
					origen.Prioridad = Json.Origen.Prioridad;
					origen.IdCategoriaOrigen = Json.Origen.IdCategoriaOrigen;
					origen.UsuarioCreacion = Json.Usuario;
					origen.UsuarioModificacion = Json.Usuario;
					origen.FechaCreacion = DateTime.Now;
					origen.FechaModificacion = DateTime.Now;
					origen.Estado = true;
					repOrigen.Insert(origen);
					scope.Complete();
				}
				Json.Origen.Id = origen.Id;

				return Ok(Json.Origen);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// TipoFuncion: GET
		/// Autor: Edgar S.
		/// Fecha: 30/04/2021
		/// Versión: 1.0
		/// <summary>
		/// Guarda Log de Accesos
		/// </summary>
		/// <param name="Ip"> Ip de Usuario </param>
		/// <param name="Modulo"> Módulo al cual se accede </param>
		/// <param name="Personal"> Nombre de personal</param>
		/// <returns> List<LogAccesosDTO> </returns>
		[Route("[action]/{Personal}/{Modulo}/{Ip}")]
        [HttpGet]
        public ActionResult GuardarLogAccesos(string Personal, string Modulo,string Ip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrigenRepositorio _repOrigen = new OrigenRepositorio();
                var lista = _repOrigen.InsertarLogAccesos(Personal, Modulo, Ip);

                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}