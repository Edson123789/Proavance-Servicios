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

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReclamoController : ControllerBase
    {

			private readonly integraDBContext _integraDBContext;

			public ReclamoController(integraDBContext integraDBContext)
			{
				_integraDBContext = integraDBContext;
			}

			[Route("[action]")]
			[HttpPost]
			public IActionResult InsertarReclamo([FromBody] ReclamoDTO Json)
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				try
				{

					ReclamoRepositorio repReclamo = new ReclamoRepositorio();
					ReclamoBO reclamo = new ReclamoBO();
					reclamo.IdMatriculaCabecera = Json.IdMatricula;
					reclamo.Descripcion = Json.Descripcion;
					reclamo.IdReclamoEstado = Json.IdReclamoEstado;
					reclamo.IdOrigen = Json.IdOrigen;
					reclamo.IdTipoReclamoAlumno = Json.IdTipoReclamoAlumno;
					reclamo.UsuarioCreacion = Json.Usuario;
					reclamo.UsuarioModificacion = Json.Usuario;
					reclamo.FechaCreacion = DateTime.Now;
					reclamo.FechaModificacion = DateTime.Now;
					reclamo.Estado = true;
					repReclamo.Insert(reclamo);
					return Ok(reclamo);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}

			//Actualizar Reclamos
			[Route("[action]")]
			[HttpPut]
			public IActionResult ActualizarReclamo([FromBody] ReclamoDTO Json)
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				try
				{
					ReclamoRepositorio repreclamoE = new ReclamoRepositorio();
					ReclamoBO reclamo = new ReclamoBO();
					using (TransactionScope scope = new TransactionScope())
					{
						if (repreclamoE.Exist(Json.Id))
						{
							reclamo = repreclamoE.FirstById(Json.Id);
							reclamo.IdMatriculaCabecera = Json.IdMatricula;
							reclamo.Descripcion = Json.Descripcion;
							reclamo.IdReclamoEstado = Json.IdReclamoEstado;
							reclamo.IdOrigen = Json.IdOrigen;
							reclamo.IdTipoReclamoAlumno = Json.IdTipoReclamoAlumno;
							reclamo.UsuarioCreacion = Json.Usuario;
							reclamo.UsuarioModificacion = Json.Usuario;
							reclamo.FechaModificacion = DateTime.Now;
							repreclamoE.Update(reclamo);
						}
						scope.Complete();
						Json.Id = reclamo.Id;
					}

					return Ok(Json);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
			//Eliminar Reclamos
			[Route("[action]/{Id}/{Usuario}")]
			[HttpGet]
			public ActionResult EliminarReclamo(int Id, string Usuario)
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				try
				{

					ReclamoRepositorio repReclamo = new ReclamoRepositorio();
					ReclamoBO reclamo = new ReclamoBO();									
					var	result = repReclamo.EliminarReclamo(Id, Usuario);					
					return Ok(true);
				}
				catch (Exception e)
				{
					return BadRequest(e.Message);
				}
			}


			//Obtener los origenes de las quejas Dx
			[Route("[action]")]
			[HttpGet]
			public IActionResult ObtenerCombosOrigenReclamo()
			{

				try
				{
					OrigenRepositorio combosOrigen = new OrigenRepositorio();
					var cmbOrigen = new { Origen = combosOrigen.ObtenerCombosOrigen() };
					return Ok(cmbOrigen);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}

		
			//Obtener id Codigo Matricula
			[Route("[action]")]
			[HttpGet]
			public IActionResult ObtenerCombosIdCodigoMatricula()
			{

			try
			{
				MatriculaCabeceraRepositorio _repMatriculaAlumno = new MatriculaCabeceraRepositorio();
				var cmbOrigen = new { idCabeceraMatricula = _repMatriculaAlumno.ObtenerCombosCodigoMatricula() };
				return Ok(cmbOrigen);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		//Obtener datos del alumno
		[Route("[Action]")]
			[HttpPost]
			public ActionResult ObtenerAlumnosMatriculadosFiltro([FromBody] FiltroMatriculaAlumnoDTO Filtro)
			{
				
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				try
				{
					MatriculaCabeceraRepositorio _repMatriculaAlumno = new MatriculaCabeceraRepositorio();
					return Ok(_repMatriculaAlumno.ObtenerAlumnosMatriculados(Filtro.CodigoMatricula, Filtro.DNI));
				}
				catch (Exception e)
				{
					return BadRequest(e.Message);
				}
			}

        //ObtenerDatosDeLaGrilla SEGUN EL ID DE MATRICULA DE UN ALUMNO EN ESPECIFICO
        [Route("[action]/{idMatricula}")]
        [HttpGet]
        public ActionResult ObtenerDatosReclamoAlumnoEspecifico(int IdMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReclamoRepositorio _repReclamoIdMatricula = new ReclamoRepositorio();
                var cmbCE = new { ListaReclamo = _repReclamoIdMatricula.ListarReclamosIdMatricula(IdMatricula)};
                return Ok(cmbCE);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		//OBTIENE TODOS LOS RECLAMOS
		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerReclamos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ReclamoRepositorio _repReclamoIdMatricula = new ReclamoRepositorio();
				var cmbCE = new { ListaReclamo = _repReclamoIdMatricula.ListarReclamos() };
				return Ok(cmbCE);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//Enviar Reclamo (Actualiza la tabla)
		[Route("[action]/{Id}/{Usuario}")]
		[HttpGet]
		public ActionResult EnviarReclamo(int Id, string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				ReclamoRepositorio repReclamo = new ReclamoRepositorio();
				ReclamoBO reclamo = new ReclamoBO();
				var result = repReclamo.EnviarReclamo(Id, Usuario);
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		//Confirmar Reclamo 
		[Route("[action]/{Id}/{Usuario}")]
		[HttpGet]
		public ActionResult ConfirmarReclamo(int Id, string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				ReclamoRepositorio repReclamo = new ReclamoRepositorio();
				ReclamoBO reclamo = new ReclamoBO();
				var result = repReclamo.ConfirmarReclamo(Id, Usuario);
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		//Busca reclamo de alumno especifico por el idMatriculaCabecera)
		[Route("[action]/{idMatriculaCabecera}")]
		[HttpGet]
		public ActionResult ObtenerReclamosAlumno (int idMatriculaCabecera)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ReclamoRepositorio _repReclamoIdMatricula = new ReclamoRepositorio();
				var cmbCE = new { ListaReclamo = _repReclamoIdMatricula.ListarReclamosAlumno(idMatriculaCabecera) };
				return Ok(cmbCE);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}


	}

}
