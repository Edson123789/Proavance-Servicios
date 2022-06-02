using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Text;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionHorarioMarcacionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public ConfiguracionHorarioMarcacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

		[Route("[Action]")]
		[HttpPost]
		public IActionResult InsertarConfiguracion([FromBody] InsertarConfiguracionHorarioDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionHorarioMarcacionRepositorio _repConfiguracionMarcacion = new ConfiguracionHorarioMarcacionRepositorio(_integraDBContext);
				ConfiguracionHorarioMarcacionGrupoRepositorio _repConfiguracionMarcacionGrupo = new ConfiguracionHorarioMarcacionGrupoRepositorio(_integraDBContext);

				List<ConfiguracionHorarioMarcacionGrupoBO> ListIdHorarioGrupoPersonal = new List<ConfiguracionHorarioMarcacionGrupoBO>();

				ConfiguracionHorarioMarcacionBO configuracion = new ConfiguracionHorarioMarcacionBO();
				configuracion.Nombre = Json.Nombre;
				configuracion.HoraInicio = Json.HoraInicio;
				configuracion.HoraFin = Json.HoraFin;
				configuracion.UsuarioCreacion = Json.Usuario;
				configuracion.UsuarioModificacion = Json.Usuario;
				configuracion.FechaCreacion = DateTime.Now;
				configuracion.FechaModificacion = DateTime.Now;
				configuracion.Estado = true;

				_repConfiguracionMarcacion.Insert(configuracion);

				foreach (var item in Json.IdHorarioGrupoPersonal)
				{
					ConfiguracionHorarioMarcacionGrupoBO configuraciongrupo = new ConfiguracionHorarioMarcacionGrupoBO();
					configuraciongrupo.IdConfiguracionHorarioMarcacion = configuracion.Id;
					configuraciongrupo.IdHorarioGrupoPersonal = item;
					configuraciongrupo.UsuarioCreacion = Json.Usuario;
					configuraciongrupo.UsuarioModificacion = Json.Usuario;
					configuraciongrupo.FechaCreacion = DateTime.Now;
					configuraciongrupo.FechaModificacion = DateTime.Now;
					configuraciongrupo.Estado = true;
					ListIdHorarioGrupoPersonal.Add(configuraciongrupo);
					_repConfiguracionMarcacionGrupo.Insert(configuraciongrupo);
				}
				configuracion.IdHorarioGrupoPersonal = ListIdHorarioGrupoPersonal;
				return Ok(configuracion);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[Route("[Action]")]
		[HttpPost]
		public IActionResult InsertarBoton([FromBody] InsertarTipoBotonConfiguracionDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionTipoMarcacionRepositorio repConfiguracionTipo = new ConfiguracionTipoMarcacionRepositorio(_integraDBContext);
				List<BotonesOrdenDTO> ordenbotones = new List<BotonesOrdenDTO>();
				ordenbotones = repConfiguracionTipo.todoOrden();
				int cont = 0;
				foreach(var item in ordenbotones)
				{ 
					if (Json.Orden == item.Valor)
                    {
						cont++;
                    }
				}
				if(cont < 1)
                {
					ConfiguracionTipoMarcacionBO configuraciontipo = new ConfiguracionTipoMarcacionBO();
					configuraciontipo.NombreBoton = Json.NombreBoton;
					configuraciontipo.HoraInicio = Json.HoraInicio;
					configuraciontipo.HoraFin = Json.HoraFin;
					configuraciontipo.Orden = Json.Orden;
					configuraciontipo.UsuarioCreacion = Json.Usuario;
					configuraciontipo.UsuarioModificacion = Json.Usuario;
					configuraciontipo.FechaCreacion = DateTime.Now;
					configuraciontipo.FechaModificacion = DateTime.Now;
					configuraciontipo.Estado = true;

					repConfiguracionTipo.Insert(configuraciontipo);
					return Ok(configuraciontipo);
				}
				else
                {
					return BadRequest("Ya existe un boton con ese orden");

				}
				
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[Route("[Action]")]
		[HttpPut]
		public IActionResult ActualizarConfiguracion([FromBody] InsertarConfiguracionHorarioDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionHorarioMarcacionRepositorio _repConfiguracionMarcacion = new ConfiguracionHorarioMarcacionRepositorio();
				ConfiguracionHorarioMarcacionGrupoRepositorio _repConfiguracionMarcacionGrupo = new ConfiguracionHorarioMarcacionGrupoRepositorio();

				List<ConfiguracionHorarioMarcacionGrupoBO> ListIdHorarioGrupoPersonal = new List<ConfiguracionHorarioMarcacionGrupoBO>();

				ConfiguracionHorarioMarcacionBO configuracion = new ConfiguracionHorarioMarcacionBO();

				_repConfiguracionMarcacionGrupo.DeleteLogicoPorGrupo(Json.Id, Json.Usuario, Json.IdHorarioGrupoPersonal);

				if (_repConfiguracionMarcacion.Exist(Json.Id))
				{
					configuracion = _repConfiguracionMarcacion.FirstById(Json.Id);
					configuracion.Nombre = Json.Nombre;
					configuracion.HoraInicio = Json.HoraInicio;
					configuracion.HoraFin = Json.HoraFin;
					configuracion.UsuarioModificacion = Json.Usuario;
					configuracion.FechaModificacion = DateTime.Now;
					_repConfiguracionMarcacion.Update(configuracion);

				}

				foreach (var tipo in Json.IdHorarioGrupoPersonal)
				{
					ConfiguracionHorarioMarcacionGrupoBO configuraciongrupo;
					configuraciongrupo = _repConfiguracionMarcacionGrupo.FirstBy(x => x.IdHorarioGrupoPersonal == tipo && x.IdConfiguracionHorarioMarcacion == configuracion.Id);
					if (configuraciongrupo != null)
					{
						configuraciongrupo.IdHorarioGrupoPersonal = tipo;
						configuraciongrupo.UsuarioModificacion = Json.Usuario;
						configuraciongrupo.FechaModificacion = DateTime.Now;
						configuraciongrupo.Estado = true;
						ListIdHorarioGrupoPersonal.Add(configuraciongrupo);
						_repConfiguracionMarcacionGrupo.Update(configuraciongrupo);
					}
					else
					{
						configuraciongrupo = new ConfiguracionHorarioMarcacionGrupoBO();						
						configuraciongrupo.IdConfiguracionHorarioMarcacion = configuracion.Id;
						configuraciongrupo.IdHorarioGrupoPersonal = tipo;
						configuraciongrupo.UsuarioCreacion = Json.Usuario;
						configuraciongrupo.UsuarioModificacion = Json.Usuario;
						configuraciongrupo.FechaCreacion = DateTime.Now;
						configuraciongrupo.FechaModificacion = DateTime.Now;
						configuraciongrupo.Estado = true;
						ListIdHorarioGrupoPersonal.Add(configuraciongrupo);
						_repConfiguracionMarcacionGrupo.Insert(configuraciongrupo);						
					}

				}
				configuracion.IdHorarioGrupoPersonal = ListIdHorarioGrupoPersonal;
				Json.Id = configuracion.Id;
				return Ok(configuracion);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[Action]")]
		[HttpPut]
		public IActionResult ActualizarBoton([FromBody] InsertarTipoBotonConfiguracionDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionTipoMarcacionRepositorio repConfiguracionTipo = new ConfiguracionTipoMarcacionRepositorio();
				ConfiguracionTipoMarcacionBO configuraciontipo = new ConfiguracionTipoMarcacionBO();
				List<BotonesOrdenDTO> ordenbotones = new List<BotonesOrdenDTO>();
				ordenbotones = repConfiguracionTipo.todoOrden();
				int cont = 0;
				//foreach (var item in ordenbotones)
				//{
				//	if (Json.Orden == item.Orden)
				//	{
				//		cont++;
				//	}
				//}
				//if (cont < 1)
				//{
					if (repConfiguracionTipo.Exist(Json.Id))
					{
						configuraciontipo = repConfiguracionTipo.FirstById(Json.Id);
						configuraciontipo.NombreBoton = Json.NombreBoton;
						configuraciontipo.HoraInicio = Json.HoraInicio;
						configuraciontipo.Orden = Json.Orden;
						configuraciontipo.HoraFin = Json.HoraFin;
						configuraciontipo.UsuarioModificacion = Json.Usuario;
						configuraciontipo.FechaModificacion = DateTime.Now;
						repConfiguracionTipo.Update(configuraciontipo);

					}
					Json.Id = configuraciontipo.Id;
					return Ok(configuraciontipo);
				//}
				//else
    //            {
				//	return BadRequest("Ya existe un boton con ese orden");
				//}
					
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[Action]")]
		[HttpDelete]
		public ActionResult EliminarConfiguracion([FromBody] CriterioEvaluacionDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				ConfiguracionHorarioMarcacionRepositorio _repConfiguracionMarcacion = new ConfiguracionHorarioMarcacionRepositorio();
				ConfiguracionHorarioMarcacionGrupoRepositorio _repConfiguracionMarcacionGrupo = new ConfiguracionHorarioMarcacionGrupoRepositorio();

				HorarioGrupoBO horariogrupo = new HorarioGrupoBO();
				bool result = false;
				if (_repConfiguracionMarcacion.Exist(Json.Id))
				{
					result = _repConfiguracionMarcacion.Delete(Json.Id, Json.Usuario);
				}

				//if (RepTipoPrograma.Exist(criterio.Id))
				//{
				//	result = RepTipoPrograma.Delete(criterio.Id, Json.Usuario);

				//}

				//if (RepModalidadCurso.Exist(criterio.Id))
				//{
				//	result = RepTipoPrograma.Delete(criterio.Id, Json.Usuario);
				//}

				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpDelete]
		public ActionResult EliminarBoton([FromBody] CriterioEvaluacionDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				ConfiguracionTipoMarcacionRepositorio repConfiguracionTipo = new ConfiguracionTipoMarcacionRepositorio();
				ConfiguracionTipoMarcacionBO configuraciontipo = new ConfiguracionTipoMarcacionBO();

				HorarioGrupoBO horariogrupo = new HorarioGrupoBO();
				bool result = false;
				if (repConfiguracionTipo.Exist(Json.Id))
				{
					result = repConfiguracionTipo.Delete(Json.Id, Json.Usuario);
				}

				//if (RepTipoPrograma.Exist(criterio.Id))
				//{
				//	result = RepTipoPrograma.Delete(criterio.Id, Json.Usuario);
				//}

				//if (RepModalidadCurso.Exist(criterio.Id))
				//{
				//	result = RepTipoPrograma.Delete(criterio.Id, Json.Usuario);
				//}

				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpGet]
		public ActionResult getGrupos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				HorarioGrupoRepositorio repHorarioGrupo = new HorarioGrupoRepositorio();
				var grupos = new { grupos = repHorarioGrupo.getGrupos() };
				return Ok(grupos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[Action]")]
		[HttpGet]
		public ActionResult getConfiguracionGrupos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionHorarioMarcacionRepositorio _repConfiguracionMarcacion = new ConfiguracionHorarioMarcacionRepositorio(_integraDBContext);
				var grupos = new { grupos = _repConfiguracionMarcacion.getConfiguracion() };
				return Ok(grupos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[Action]")]
		[HttpGet]
		public ActionResult getConfiguracionBotones()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionTipoMarcacionRepositorio repConfiguracionTipo = new ConfiguracionTipoMarcacionRepositorio();
				var grupos = new { grupos = repConfiguracionTipo.getConfiguracionBoton() };
				return Ok(grupos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ActualizarMensaje([FromBody] ConfiguracionMarcadorMensajeDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionMarcadorMensajeRepositorio repConfiguracionTipo = new ConfiguracionMarcadorMensajeRepositorio();
				ConfiguracionMarcadorMensajeBO configuraciontipo;
				if (repConfiguracionTipo.Exist(Json.Id))
				{
					configuraciontipo = repConfiguracionTipo.FirstById(Json.Id);
					configuraciontipo.Mensaje = Json.Mensaje;
                    byte[] dataMensaje = Convert.FromBase64String(Json.Mensaje);
                    configuraciontipo.Mensaje = Encoding.UTF8.GetString(dataMensaje);
                    configuraciontipo.FechaCreacion = DateTime.Now;
					configuraciontipo.FechaModificacion = DateTime.Now;
					configuraciontipo.Estado = true;
					repConfiguracionTipo.Update(configuraciontipo);
				}
                else
                {
					configuraciontipo = new ConfiguracionMarcadorMensajeBO();
					configuraciontipo.Mensaje = Json.Mensaje;
					configuraciontipo.UsuarioCreacion = Json.Usuario;
					configuraciontipo.UsuarioModificacion = Json.Usuario;
					configuraciontipo.FechaCreacion = DateTime.Now;
					configuraciontipo.FechaModificacion = DateTime.Now;
					configuraciontipo.Estado = true;

					repConfiguracionTipo.Insert(configuraciontipo);
					
				}
				Json.Id = configuraciontipo.Id;
				return Ok(configuraciontipo);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[Action]/{IdGrupo}")]
		[HttpGet]
		public IActionResult ObtenerDetalles(int IdGrupo)
		{

			try
			{
				ConfiguracionHorarioMarcacionGrupoRepositorio _repConfiguracionMarcacionGrupo = new ConfiguracionHorarioMarcacionGrupoRepositorio();
				HorarioGrupoRepositorio repHorarioGrupo = new HorarioGrupoRepositorio();

				var Detalles = new
				{
					//detallecategoria = repCriterioE.ObtenerModalidadPorId(IdCriterioEvaluacion),
					detalletipoprograma = _repConfiguracionMarcacionGrupo.listargruposconfiguracion(IdGrupo)					
				};
				return Ok(Detalles);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[Action]/{IdGrupo}")]
		[HttpGet]
		public IActionResult ObtenerOrden(int IdGrupo)
		{

			try
			{
				ConfiguracionTipoMarcacionRepositorio repConfiguracionTipo = new ConfiguracionTipoMarcacionRepositorio();
				HorarioGrupoRepositorio repHorarioGrupo = new HorarioGrupoRepositorio();

				var Detalles = new
				{
					//detallecategoria = repCriterioE.ObtenerModalidadPorId(IdCriterioEvaluacion),
					orden = repConfiguracionTipo.conseguirorden(IdGrupo)
				};
				return Ok(Detalles);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[Action]")]
		[HttpGet]
		public IActionResult getMensaje()
		{

			try
			{
				ConfiguracionTipoMarcacionRepositorio repConfiguracionTipo = new ConfiguracionTipoMarcacionRepositorio();
				HorarioGrupoRepositorio repHorarioGrupo = new HorarioGrupoRepositorio();

				var Detalles = new
				{
					//detallecategoria = repCriterioE.ObtenerModalidadPorId(IdCriterioEvaluacion),
					msj = repConfiguracionTipo.getMensaje()
				};
				return Ok(Detalles);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
