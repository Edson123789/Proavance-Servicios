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

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalRecursoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public PersonalRecursoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarPersonalRecurso([FromBody] PersonalRecursoDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalRecursoRepositorio _repPersonalRecurso = new PersonalRecursoRepositorio(_integraDBContext);
				PersonalRecursoHabilidadRepositorio _PersonalRecursoHabilidadRepositorio = new PersonalRecursoHabilidadRepositorio(_integraDBContext);					

				List<PersonalRecursoHabilidadBO> _listPersonalRecursoHabilidad = new List<PersonalRecursoHabilidadBO>();


				PersonalRecursoBO precurso = new PersonalRecursoBO();

				using (TransactionScope scope = new TransactionScope())
				{

					precurso.NombrePersonal = Json.NombrePersonal;
					precurso.ApellidosPersonal = Json.ApellidosPersonal;
					precurso.DescripcionPersonal = Json.DescripcionPersonal;
					precurso.UrlfotoPersonal = Json.UrlfotoPersonal;
					precurso.CostoHorario = Json.CostoHorario;
					precurso.IdMoneda = Json.IdMoneda;
					precurso.Productividad = Json.Productividad;
					precurso.IdTipoDisponibilidadPersonal = Json.IdTipoDisponibilidadPersonal;
					precurso.UsuarioCreacion = Json.Usuario;
					precurso.UsuarioModificacion = Json.Usuario;					
					precurso.FechaCreacion = DateTime.Now;
					precurso.FechaModificacion = DateTime.Now;
					precurso.Estado = true;

					_repPersonalRecurso.Insert(precurso);

					foreach (var habilidad in Json.PersonalRecursoHabilidad)
					{
						PersonalRecursoHabilidadBO precursoHabilidad = new PersonalRecursoHabilidadBO();
						precursoHabilidad.IdHabilidadSimulador = habilidad.IdHabilidadSimulador;
						precursoHabilidad.IdPersonalRecurso = precurso.Id;
						precursoHabilidad.Puntaje = habilidad.Puntaje;
						precursoHabilidad.UsuarioCreacion = Json.Usuario;
						precursoHabilidad.UsuarioModificacion = Json.Usuario;
						precursoHabilidad.FechaCreacion = DateTime.Now;
						precursoHabilidad.FechaModificacion = DateTime.Now;
						precursoHabilidad.Estado = true;
						_listPersonalRecursoHabilidad.Add(precursoHabilidad);
						_PersonalRecursoHabilidadRepositorio.Insert(precursoHabilidad);
					}

					scope.Complete();
				}


				return Ok(precurso);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult ActualizarPersonalRecurso([FromBody] PersonalRecursoDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalRecursoRepositorio _repPersonalRecurso = new PersonalRecursoRepositorio(_integraDBContext);
				PersonalRecursoHabilidadRepositorio _PersonalRecursoHabilidadRepositorio = new PersonalRecursoHabilidadRepositorio(_integraDBContext);

				List<PersonalRecursoHabilidadBO> _listPersonalRecursoHabilidad = new List<PersonalRecursoHabilidadBO>();


				PersonalRecursoBO precurso = new PersonalRecursoBO();


				_PersonalRecursoHabilidadRepositorio.DeleteLogicoPorHabilidad(Json.Id, Json.Usuario, Json.PersonalRecursoHabilidad);

				if (_repPersonalRecurso.Exist(Json.Id))
				{
					precurso = _repPersonalRecurso.FirstById(Json.Id);
					precurso.NombrePersonal = Json.NombrePersonal;
					precurso.ApellidosPersonal = Json.ApellidosPersonal;
					precurso.DescripcionPersonal = Json.DescripcionPersonal;
					precurso.UrlfotoPersonal = Json.UrlfotoPersonal;
					precurso.CostoHorario = Json.CostoHorario;
					precurso.IdMoneda = Json.IdMoneda;
					precurso.Productividad = Json.Productividad;
					precurso.IdTipoDisponibilidadPersonal = Json.IdTipoDisponibilidadPersonal;					
					precurso.UsuarioModificacion = Json.Usuario;					
					precurso.FechaModificacion = DateTime.Now;

					_repPersonalRecurso.Update(precurso);
				}

				foreach (var item in Json.PersonalRecursoHabilidad)
				{
					PersonalRecursoHabilidadBO recursohabilidad;
					recursohabilidad = _PersonalRecursoHabilidadRepositorio.FirstBy(x => x.IdPersonalRecurso == precurso.Id && x.IdHabilidadSimulador == item.IdHabilidadSimulador);
					if (recursohabilidad != null)
					{
						if (recursohabilidad.IdHabilidadSimulador != 0) recursohabilidad.IdPersonalRecurso = precurso.Id;
						else recursohabilidad.IdHabilidadSimulador = item.IdHabilidadSimulador;
						recursohabilidad.Puntaje = item.Puntaje;
						recursohabilidad.UsuarioModificacion = Json.Usuario;
						recursohabilidad.FechaModificacion = DateTime.Now;
						recursohabilidad.Estado = true;
						_PersonalRecursoHabilidadRepositorio.Update(recursohabilidad);
					}
					else
					{
						recursohabilidad = new PersonalRecursoHabilidadBO();
						recursohabilidad.IdPersonalRecurso = precurso.Id;
						if (recursohabilidad.Id != 0) recursohabilidad.IdHabilidadSimulador = item.IdHabilidadSimulador;
						else recursohabilidad.IdHabilidadSimulador = item.IdHabilidadSimulador;
						recursohabilidad.Puntaje = item.Puntaje;
						recursohabilidad.UsuarioCreacion = Json.Usuario;
						recursohabilidad.UsuarioModificacion = Json.Usuario;
						recursohabilidad.FechaCreacion = DateTime.Now;
						recursohabilidad.FechaModificacion = DateTime.Now;
						recursohabilidad.Estado = true;
						_PersonalRecursoHabilidadRepositorio.Insert(recursohabilidad);
					}
					_listPersonalRecursoHabilidad.Add(recursohabilidad);					

				}
				

				
				Json.Id = precurso.Id;

				return Ok(precurso);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerCombosPersonalRecursos()
		{
			try
			{
				MonedaRepositorio _repMonedaRepositorio = new MonedaRepositorio();
				HabilidadSimuladorRepositorio _repHabilidadSimulador = new HabilidadSimuladorRepositorio();
				TipoDisponibilidadPersonalRepositorio _repDisponibilidad = new TipoDisponibilidadPersonalRepositorio();


				var combosPersonalRecursos = new
				{
					Moneda = _repMonedaRepositorio.ObtenerFiltroMoneda(),
					HabilidadSimuladorRepositorio = _repHabilidadSimulador.ObtenerHabilidadesSimulador(),
					Disponibilidad = _repDisponibilidad.ObtenerTipoDisponibilidadPersonal()
				};

				return Ok(combosPersonalRecursos);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerPersonalRecursos()
		{
			try
			{
				PersonalRecursoRepositorio _repPersonalRecurso = new PersonalRecursoRepositorio();
				MonedaRepositorio _repMonedaRepositorio = new MonedaRepositorio();
				HabilidadSimuladorRepositorio _repHabilidadSimulador = new HabilidadSimuladorRepositorio();
				TipoDisponibilidadPersonalRepositorio _repDisponibilidad = new TipoDisponibilidadPersonalRepositorio();


				var combosPersonalRecursos = new
				{
					PersonalRecursos= _repPersonalRecurso.ObtenerHabilidadesSimulador(),
					Moneda = _repMonedaRepositorio.ObtenerFiltroMoneda(),
					HabilidadSimuladorRepositorio = _repHabilidadSimulador.ObtenerHabilidadesSimulador(),
					Disponibilidad = _repDisponibilidad.ObtenerTipoDisponibilidadPersonal()
				};
				
				return Ok(combosPersonalRecursos);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]/{Id}")]
		[HttpGet]
		public IActionResult ObtenerPersonaRecursosHabilidad(int Id)
		{
			try
			{
				PersonalRecursoHabilidadRepositorio _repPersonalRecurso = new PersonalRecursoHabilidadRepositorio();
				var combosPersonalRecursos = _repPersonalRecurso.ObtenerPersonalRecursoHabilidadPorId(Id);
				return Ok(combosPersonalRecursos);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpDelete]
		public ActionResult EliminarPersonalRecurso([FromBody] TipoDocumentoAlumnoDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				PersonalRecursoRepositorio _repPersonalRecurso = new PersonalRecursoRepositorio();

				//TipoDocumentoAlumnoBO tipodocumento = new TipoDocumentoAlumnoBO();

				bool result = false;
				if (_repPersonalRecurso.Exist(Json.Id))
				{
					result = _repPersonalRecurso.Delete(Json.Id, Json.Usuario);
				}

				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]/{Id}")]
		[HttpGet]
		public ActionResult HabilidadesPorPersonal(int Id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalRecursoHabilidadRepositorio _repPersonalRecurso = new PersonalRecursoHabilidadRepositorio();
				var habilidades = _repPersonalRecurso.ObtenerPersonalRecursoHabilidadDetalle(Id);
				var datosAgrupado = (from p in habilidades
									 group p by p.IdHabilidad into grupo
									 select new {g = grupo.Key, l = grupo }).ToList();

				return Ok(datosAgrupado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

	}

}
