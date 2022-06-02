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
	public class TipoDocumentoAlumnoController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;

		public TipoDocumentoAlumnoController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarTipoDocumentoAlumno([FromBody] TipoDocumentoAlumnoInsertarDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				TipoDocumentoAlumnoRepositorio _repTipoDocumentoAlumno = new TipoDocumentoAlumnoRepositorio(_integraDBContext);
				TipoDocumentoAlumnoModalidadCursoRepositorio _repTipoDocumentoAlumnoModalidadCurso = new TipoDocumentoAlumnoModalidadCursoRepositorio(_integraDBContext);
				TipoDocumentoAlumnoEstadoMatriculaRepositorio _repTipoDocumentoAlumnoEstadoMatricula = new TipoDocumentoAlumnoEstadoMatriculaRepositorio(_integraDBContext);
				TipoDocumentoAlumnoSubEstadoMatriculaRepositorio _repTipoDocumentoAlumnoSubEstadoMatricula = new TipoDocumentoAlumnoSubEstadoMatriculaRepositorio(_integraDBContext);
				TipoDocumentoAlumnoPGeneralRepositorio _repTipoDocumentoAlumnoPGeneral = new TipoDocumentoAlumnoPGeneralRepositorio(_integraDBContext);

				List<TipoDocumentoAlumnoModalidadCursoBO> listamodalidadcurso = new List<TipoDocumentoAlumnoModalidadCursoBO>();
				List<TipoDocumentoAlumnoEstadoMatriculaBO> listaEstadoMatricula = new List<TipoDocumentoAlumnoEstadoMatriculaBO>();
				List<TipoDocumentoAlumnoSubEstadoMatriculaBO> listaSubEstadoMatricula = new List<TipoDocumentoAlumnoSubEstadoMatriculaBO>();
				List<TipoDocumentoAlumnoPGeneralBO> listaPgeneral = new List<TipoDocumentoAlumnoPGeneralBO>();

				TipoDocumentoAlumnoBO tipodocumento = new TipoDocumentoAlumnoBO();

				using (TransactionScope scope = new TransactionScope())
				{

					tipodocumento.Nombre = Json.Nombre;
					tipodocumento.IdPlantillaFrontal = Json.IdPlantillaFrontal;
					tipodocumento.IdPlantillaPosterior = (int)Json.IdPlantillaPosterior;
					tipodocumento.UsuarioCreacion = Json.Usuario;
					tipodocumento.UsuarioModificacion = Json.Usuario;
					tipodocumento.IdOperadorComparacion = Json.OperadorComparacion;
					tipodocumento.FechaCreacion = DateTime.Now;
					tipodocumento.FechaModificacion = DateTime.Now;
					tipodocumento.Estado = true;
					foreach (var item in Json.ListaCondiciones)
					{
						//tipodocumento.IdOperadorComparacion = item.OperadorComparador;
						tipodocumento.TieneDeuda = item.TieneDeuda;
					}
					_repTipoDocumentoAlumno.Insert(tipodocumento);

					foreach (var item in Json.ListaCondiciones)
					{
						foreach (var modalidad in item.ModalidadCurso)
						{
							TipoDocumentoAlumnoModalidadCursoBO modalidadcursotd = new TipoDocumentoAlumnoModalidadCursoBO();
							modalidadcursotd.IdTipoDocumentoAlumno = tipodocumento.Id;
							modalidadcursotd.IdModalidad = modalidad.Id;
							modalidadcursotd.UsuarioCreacion = Json.Usuario;
							modalidadcursotd.UsuarioModificacion = Json.Usuario;
							modalidadcursotd.FechaCreacion = DateTime.Now;
							modalidadcursotd.FechaModificacion = DateTime.Now;
							modalidadcursotd.Estado = true;
							listamodalidadcurso.Add(modalidadcursotd);
							_repTipoDocumentoAlumnoModalidadCurso.Insert(modalidadcursotd);
						}

						foreach (var estado in item.EstadoMatricula)
						{
							TipoDocumentoAlumnoEstadoMatriculaBO estadomatricula = new TipoDocumentoAlumnoEstadoMatriculaBO();
							estadomatricula.IdTipoDocumentoAlumno = tipodocumento.Id;
							estadomatricula.IdEstadoMatricula = estado.Id;
							estadomatricula.UsuarioCreacion = Json.Usuario;
							estadomatricula.UsuarioModificacion = Json.Usuario;
							estadomatricula.FechaCreacion = DateTime.Now;
							estadomatricula.FechaModificacion = DateTime.Now;
							estadomatricula.Estado = true;
							listaEstadoMatricula.Add(estadomatricula);
							_repTipoDocumentoAlumnoEstadoMatricula.Insert(estadomatricula);
						}

						if (item.SubEstadoMatricula == null)
						{
							TipoDocumentoAlumnoSubEstadoMatriculaBO subestadomatricula = new TipoDocumentoAlumnoSubEstadoMatriculaBO();
							subestadomatricula.IdTipoDocumentoAlumno = tipodocumento.Id;
							subestadomatricula.IdSubEstadoMatricula = 0;
							subestadomatricula.UsuarioCreacion = Json.Usuario;
							subestadomatricula.UsuarioModificacion = Json.Usuario;
							subestadomatricula.FechaCreacion = DateTime.Now;
							subestadomatricula.FechaModificacion = DateTime.Now;
							subestadomatricula.Estado = true;
							listaSubEstadoMatricula.Add(subestadomatricula);
							_repTipoDocumentoAlumnoSubEstadoMatricula.Insert(subestadomatricula);
						}
						else
						{
							foreach (var subestado in item.SubEstadoMatricula)
							{

								TipoDocumentoAlumnoSubEstadoMatriculaBO subestadomatricula = new TipoDocumentoAlumnoSubEstadoMatriculaBO();
								subestadomatricula.IdTipoDocumentoAlumno = tipodocumento.Id;
								subestadomatricula.IdSubEstadoMatricula = subestado.Id;
								subestadomatricula.UsuarioCreacion = Json.Usuario;
								subestadomatricula.UsuarioModificacion = Json.Usuario;
								subestadomatricula.FechaCreacion = DateTime.Now;
								subestadomatricula.FechaModificacion = DateTime.Now;
								subestadomatricula.Estado = true;
								listaSubEstadoMatricula.Add(subestadomatricula);
								_repTipoDocumentoAlumnoSubEstadoMatricula.Insert(subestadomatricula);

							}
						}

					}
					if(Json.IdPGeneral == null)
                    {
						TipoDocumentoAlumnoPGeneralBO pgeneral = new TipoDocumentoAlumnoPGeneralBO();
						pgeneral.IdTipoDocumentoAlumno = tipodocumento.Id;
						pgeneral.IdPgeneral = 0;
						pgeneral.UsuarioCreacion = Json.Usuario;
						pgeneral.UsuarioModificacion = Json.Usuario;
						pgeneral.FechaCreacion = DateTime.Now;
						pgeneral.FechaModificacion = DateTime.Now;
						pgeneral.Estado = true;
						listaPgeneral.Add(pgeneral);
						_repTipoDocumentoAlumnoPGeneral.Insert(pgeneral);
					}
					else
                    {
						foreach (var item in Json.IdPGeneral)
						{
							TipoDocumentoAlumnoPGeneralBO pgeneral = new TipoDocumentoAlumnoPGeneralBO();
							pgeneral.IdTipoDocumentoAlumno = tipodocumento.Id;
							pgeneral.IdPgeneral = item;
							pgeneral.UsuarioCreacion = Json.Usuario;
							pgeneral.UsuarioModificacion = Json.Usuario;
							pgeneral.FechaCreacion = DateTime.Now;
							pgeneral.FechaModificacion = DateTime.Now;
							pgeneral.Estado = true;
							listaPgeneral.Add(pgeneral);
							_repTipoDocumentoAlumnoPGeneral.Insert(pgeneral);
						}
					}
					

					//tipodocumento.IdModalidadCurso = listamodalidadcurso;
					//tipodocumento.IdEstadoMatricula = listaEstadoMatricula;
					//tipodocumento.IdSubEstadoMatricula = listaSubEstadoMatricula;
					//tipodocumento.IdPGeneral = listaPgeneral;

					scope.Complete();
				}


				return Ok(tipodocumento);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[Route("[action]")]
		[HttpPut]
		public IActionResult ActualizarTipoDocumentoAlumno([FromBody] TipoDocumentoAlumnoInsertarDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				TipoDocumentoAlumnoRepositorio _repTipoDocumentoAlumno = new TipoDocumentoAlumnoRepositorio();
				TipoDocumentoAlumnoModalidadCursoRepositorio _repTipoDocumentoAlumnoModalidadCurso = new TipoDocumentoAlumnoModalidadCursoRepositorio();
				TipoDocumentoAlumnoEstadoMatriculaRepositorio _repTipoDocumentoAlumnoEstadoMatricula = new TipoDocumentoAlumnoEstadoMatriculaRepositorio();
				TipoDocumentoAlumnoSubEstadoMatriculaRepositorio _repTipoDocumentoAlumnoSubEstadoMatricula = new TipoDocumentoAlumnoSubEstadoMatriculaRepositorio();
				TipoDocumentoAlumnoPGeneralRepositorio _repTipoDocumentoAlumnoPGeneral = new TipoDocumentoAlumnoPGeneralRepositorio();
				OperadorComparacionRepositorio _repOperadorComparacionRepositorio = new OperadorComparacionRepositorio();


				List<TipoDocumentoAlumnoModalidadCursoBO> listamodalidadcurso = new List<TipoDocumentoAlumnoModalidadCursoBO>();
				List<TipoDocumentoAlumnoEstadoMatriculaBO> listaEstadoMatricula = new List<TipoDocumentoAlumnoEstadoMatriculaBO>();
				List<TipoDocumentoAlumnoSubEstadoMatriculaBO> listaSubEstadoMatricula = new List<TipoDocumentoAlumnoSubEstadoMatriculaBO>();
				List<TipoDocumentoAlumnoPGeneralBO> listaPgeneral = new List<TipoDocumentoAlumnoPGeneralBO>();
				TipoDocumentoAlumnoBO tipodocumento = new TipoDocumentoAlumnoBO();

				List<int> listamodalidadcursoid = new List<int>();
				List<int> listaestadomatriculaid = new List<int>();
				List<int> subestadomatriculaid = new List<int>();
				foreach (var item in Json.ListaCondiciones)
				{
					foreach (var id in item.ModalidadCurso)
					{
						listamodalidadcursoid.Add(id.Id);
					}
					foreach (var id in item.EstadoMatricula)
					{
						listamodalidadcursoid.Add(id.Id);
					}

					if (item.SubEstadoMatricula != null)
					{
						foreach (var id in item.SubEstadoMatricula)
						{
							listamodalidadcursoid.Add(id.Id);
						}
					}


					_repTipoDocumentoAlumnoModalidadCurso.DeleteLogicoPorTipoDocumento(Json.Id, Json.Usuario, listamodalidadcursoid);
					_repTipoDocumentoAlumnoEstadoMatricula.DeleteLogicoPorTipoDocumento(Json.Id, Json.Usuario, listamodalidadcursoid);
					_repTipoDocumentoAlumnoSubEstadoMatricula.DeleteLogicoPorTipoDocumento(Json.Id, Json.Usuario, listamodalidadcursoid);
				}

				_repTipoDocumentoAlumnoPGeneral.DeleteLogicoPorTipoDocumento(Json.Id, Json.Usuario, Json.IdPGeneral);

				if (_repTipoDocumentoAlumno.Exist(Json.Id))
				{
					tipodocumento = _repTipoDocumentoAlumno.FirstById(Json.Id);
					tipodocumento.Nombre = Json.Nombre;
					tipodocumento.IdPlantillaFrontal = Json.IdPlantillaFrontal;
					tipodocumento.IdPlantillaPosterior = (int)Json.IdPlantillaPosterior;
					tipodocumento.UsuarioModificacion = Json.Usuario;
					tipodocumento.FechaModificacion = DateTime.Now;
					tipodocumento.IdOperadorComparacion = Json.OperadorComparacion;
					foreach (var item in Json.ListaCondiciones)
					{
						//tipodocumento.IdOperadorComparacion = item.OperadorComparador;						
						tipodocumento.TieneDeuda = item.TieneDeuda;
					}
					_repTipoDocumentoAlumno.Update(tipodocumento);
				}

				foreach (var item in Json.ListaCondiciones)
				{
					foreach (var modalidad in item.ModalidadCurso)
					{
						TipoDocumentoAlumnoModalidadCursoBO modalidadcurso;
						modalidadcurso = _repTipoDocumentoAlumnoModalidadCurso.FirstBy(x => x.IdModalidad == modalidad.Id && x.IdTipoDocumentoAlumno == tipodocumento.Id);
						if (modalidadcurso != null)
						{
							if (modalidad.IdModalidad != 0) modalidadcurso.IdModalidad = modalidad.Id;
							else modalidadcurso.IdModalidad = modalidad.IdModalidad;
							modalidadcurso.UsuarioModificacion = Json.Usuario;
							modalidadcurso.FechaModificacion = DateTime.Now;
							modalidadcurso.Estado = true;
							_repTipoDocumentoAlumnoModalidadCurso.Update(modalidadcurso);
						}
						else
						{
							modalidadcurso = new TipoDocumentoAlumnoModalidadCursoBO();
							modalidadcurso.IdTipoDocumentoAlumno = tipodocumento.Id;
							if (modalidad.Id != 0) modalidadcurso.IdModalidad = modalidad.Id;
							else modalidadcurso.IdModalidad = modalidad.IdModalidad;
							modalidadcurso.UsuarioCreacion = Json.Usuario;
							modalidadcurso.UsuarioModificacion = Json.Usuario;
							modalidadcurso.FechaCreacion = DateTime.Now;
							modalidadcurso.FechaModificacion = DateTime.Now;
							modalidadcurso.Estado = true;
							_repTipoDocumentoAlumnoModalidadCurso.Insert(modalidadcurso);
						}
						listamodalidadcurso.Add(modalidadcurso);
					}

					foreach (var estado in item.EstadoMatricula)
					{
						TipoDocumentoAlumnoEstadoMatriculaBO estadomatricula;
						estadomatricula = _repTipoDocumentoAlumnoEstadoMatricula.FirstBy(x => x.IdEstadoMatricula == estado.Id && x.IdTipoDocumentoAlumno == tipodocumento.Id);
						if (estadomatricula != null)
						{
							if (estado.Id!= 0) estadomatricula.IdEstadoMatricula = estado.Id;
							else estadomatricula.IdEstadoMatricula = estado.IdEstadoMatricula;
							estadomatricula.IdEstadoMatricula = estado.Id;
							estadomatricula.UsuarioModificacion = Json.Usuario;
							estadomatricula.FechaModificacion = DateTime.Now;
							estadomatricula.Estado = true;
							_repTipoDocumentoAlumnoEstadoMatricula.Update(estadomatricula);
						}
						else
						{
							estadomatricula = new TipoDocumentoAlumnoEstadoMatriculaBO();
							estadomatricula.IdTipoDocumentoAlumno = tipodocumento.Id;
							if (estado.Id != 0) estadomatricula.IdEstadoMatricula = estado.Id;
							else estadomatricula.IdEstadoMatricula = estado.IdEstadoMatricula;
							estadomatricula.UsuarioCreacion = Json.Usuario;
							estadomatricula.UsuarioModificacion = Json.Usuario;
							estadomatricula.FechaCreacion = DateTime.Now;
							estadomatricula.FechaModificacion = DateTime.Now;
							estadomatricula.Estado = true;
							_repTipoDocumentoAlumnoEstadoMatricula.Insert(estadomatricula);
						}
						listaEstadoMatricula.Add(estadomatricula);

					}

					if (item.SubEstadoMatricula != null)
					{
						foreach (var subestado in item.SubEstadoMatricula)
						{
							TipoDocumentoAlumnoSubEstadoMatriculaBO subestadomatricula;
							subestadomatricula = _repTipoDocumentoAlumnoSubEstadoMatricula.FirstBy(x => x.IdSubEstadoMatricula == subestado.Id && x.IdTipoDocumentoAlumno == tipodocumento.Id);
							if (subestadomatricula != null)
							{
								if (subestado.Id != 0) subestadomatricula.IdSubEstadoMatricula = subestado.Id;
								else subestadomatricula.IdSubEstadoMatricula = subestado.IdSubEstadoMatricula;
								subestadomatricula.IdSubEstadoMatricula = subestado.IdSubEstadoMatricula;
								subestadomatricula.UsuarioModificacion = Json.Usuario;
								subestadomatricula.FechaModificacion = DateTime.Now;
								subestadomatricula.Estado = true;
								_repTipoDocumentoAlumnoSubEstadoMatricula.Update(subestadomatricula);
							}
							else
							{
								subestadomatricula = new TipoDocumentoAlumnoSubEstadoMatriculaBO();
								subestadomatricula.IdTipoDocumentoAlumno = tipodocumento.Id;
								if (subestado.Id != 0) subestadomatricula.IdSubEstadoMatricula = subestado.Id;
								else subestadomatricula.IdSubEstadoMatricula = subestado.IdSubEstadoMatricula;
								subestadomatricula.UsuarioCreacion = Json.Usuario;
								subestadomatricula.UsuarioModificacion = Json.Usuario;
								subestadomatricula.FechaCreacion = DateTime.Now;
								subestadomatricula.FechaModificacion = DateTime.Now;
								subestadomatricula.Estado = true;
								_repTipoDocumentoAlumnoSubEstadoMatricula.Insert(subestadomatricula);
							}
							listaSubEstadoMatricula.Add(subestadomatricula);

						}
					}
					else
                    {
						TipoDocumentoAlumnoSubEstadoMatriculaBO subestadomatricula = new TipoDocumentoAlumnoSubEstadoMatriculaBO();
						subestadomatricula.IdTipoDocumentoAlumno = tipodocumento.Id;
						subestadomatricula.IdSubEstadoMatricula = 0;
						subestadomatricula.UsuarioCreacion = Json.Usuario;
						subestadomatricula.UsuarioModificacion = Json.Usuario;
						subestadomatricula.FechaCreacion = DateTime.Now;
						subestadomatricula.FechaModificacion = DateTime.Now;
						subestadomatricula.Estado = true;
						listaSubEstadoMatricula.Add(subestadomatricula);
						_repTipoDocumentoAlumnoSubEstadoMatricula.Insert(subestadomatricula);
					}

				}
				if(Json.IdPGeneral == null )
                {
					TipoDocumentoAlumnoPGeneralBO pgeneral;
					pgeneral = new TipoDocumentoAlumnoPGeneralBO();
					pgeneral.IdTipoDocumentoAlumno = tipodocumento.Id;
					pgeneral.IdPgeneral = 0;
					pgeneral.UsuarioCreacion = Json.Usuario;
					pgeneral.UsuarioModificacion = Json.Usuario;
					pgeneral.FechaCreacion = DateTime.Now;
					pgeneral.FechaModificacion = DateTime.Now;
					pgeneral.Estado = true;
					_repTipoDocumentoAlumnoPGeneral.Insert(pgeneral);					
				}
				else
                {
					foreach (var item in Json.IdPGeneral)
					{
						TipoDocumentoAlumnoPGeneralBO pgeneral;
						pgeneral = _repTipoDocumentoAlumnoPGeneral.FirstBy(x => x.IdPgeneral == item && x.IdTipoDocumentoAlumno == tipodocumento.Id);
						if (pgeneral != null)
						{
							pgeneral.IdPgeneral = item;
							pgeneral.UsuarioModificacion = Json.Usuario;
							pgeneral.FechaModificacion = DateTime.Now;
							pgeneral.Estado = true;
							_repTipoDocumentoAlumnoPGeneral.Update(pgeneral);
						}
						else
						{
							pgeneral = new TipoDocumentoAlumnoPGeneralBO();
							pgeneral.IdTipoDocumentoAlumno = tipodocumento.Id;
							pgeneral.IdPgeneral = item;
							pgeneral.UsuarioCreacion = Json.Usuario;
							pgeneral.UsuarioModificacion = Json.Usuario;
							pgeneral.FechaCreacion = DateTime.Now;
							pgeneral.FechaModificacion = DateTime.Now;
							pgeneral.Estado = true;
							_repTipoDocumentoAlumnoPGeneral.Insert(pgeneral);
						}
						listaPgeneral.Add(pgeneral);
					}
				}

				//tipodocumento.IdModalidadCurso = listamodalidadcurso;
				//tipodocumento.IdEstadoMatricula = listaEstadoMatricula;
				//tipodocumento.IdSubEstadoMatricula = listaSubEstadoMatricula;
				//tipodocumento.IdPGeneral = listaPgeneral;
				Json.Id = tipodocumento.Id;

				return Ok(tipodocumento);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpDelete]
		public ActionResult EliminarTipoDocumento([FromBody] TipoDocumentoAlumnoDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				TipoDocumentoAlumnoRepositorio _repTipoDocumentoAlumno = new TipoDocumentoAlumnoRepositorio();

				TipoDocumentoAlumnoBO tipodocumento = new TipoDocumentoAlumnoBO();

				bool result = false;
				if (_repTipoDocumentoAlumno.Exist(Json.Id))
				{
					result = _repTipoDocumentoAlumno.Delete(Json.Id, Json.Usuario);
				}

				return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerCombosPlantillas()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();

				var detalles = new
				{
					filtroPlantilla = _repPlantilla.ObtenerListaPlantillasCertificadoConstancia()
				};

				return Ok(detalles);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult cargarCombosTipoDocumento()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio();
				EstadoMatriculaRepositorio _repEstadoMatricula = new EstadoMatriculaRepositorio();
				MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
				OperadorComparacionRepositorio _repOperadorComparacion = new OperadorComparacionRepositorio();

				var detalles = new
				{
					filtroModalidadCurso = _repModalidadCurso.ObtenerTodoFiltro(),
					filtroEstadoMatricula = _repEstadoMatricula.ObtenerTodoFiltro(),
					filtroOperadorComparacion = _repOperadorComparacion.ObtenerListaOperadorComparacion(),
					filtroSubEstadoMatricula = _repMatriculaCabecera.ObtenerSubEstadoMatricula(),
				};
				return Ok(detalles);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//Trae todos los documentos de los Alumnos
		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerTipoDocumentoAlumno()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				TipoDocumentoAlumnoRepositorio _repTipoDocumentoAlumno = new TipoDocumentoAlumnoRepositorio();
				var tipodocumento = new { ListaTipoDocumentoAlumno = _repTipoDocumentoAlumno.ListarTipoDocumentoAlumno() };
				return Ok(tipodocumento);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]/{IdTipoDocumento}")]
		[HttpGet]
		public IActionResult ObtenerDetallesTipoDocumento(int IdTipoDocumento)
		{

			try
			{
				TipoDocumentoAlumnoModalidadCursoRepositorio _repTipoDocumentoAlumnoModalidadCurso = new TipoDocumentoAlumnoModalidadCursoRepositorio();
				TipoDocumentoAlumnoEstadoMatriculaRepositorio _repTipoDocumentoAlumnoEstadoMatricula = new TipoDocumentoAlumnoEstadoMatriculaRepositorio();
				TipoDocumentoAlumnoSubEstadoMatriculaRepositorio _repTipoDocumentoAlumnoSubEstadoMatricula = new TipoDocumentoAlumnoSubEstadoMatriculaRepositorio();
				TipoDocumentoAlumnoPGeneralRepositorio _repTipoDocumentoAlumnoPGeneral = new TipoDocumentoAlumnoPGeneralRepositorio();
				var Detalles = new
				{
					detallemodalidad = _repTipoDocumentoAlumnoModalidadCurso.ListarTipoDocumentoAlumnoModalidadCursoPorId(IdTipoDocumento),
					detalleestado = _repTipoDocumentoAlumnoEstadoMatricula.ListarTipoDocumentoAlumnoEstadoPorId(IdTipoDocumento),
					detallesubestado = _repTipoDocumentoAlumnoSubEstadoMatricula.ListarTipoDocumentoAlumnoSubEstadoPorId(IdTipoDocumento),
					detallepgeneral = _repTipoDocumentoAlumnoPGeneral.ListarTipoDocumentoAlumnoPGeneralPorId(IdTipoDocumento)
				};
				return Ok(Detalles);


			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        //[Route("[action]/{IdTipoDocumento}")]
        //[HttpGet]
        //public IActionResult ObtenerDatosParaCondicionesDocumento(int IdTipoDocumento)
        //{

        //    try
        //    {

        //        List<TipoDocumentoDetalleDTO> ListaGrupo;
        //        if (IdTipoDocumento > 0)
        //        {
        //            TipoDocumentoAlumnoRepositorio _repTipoDocumentoAlumno = new TipoDocumentoAlumnoRepositorio();

        //            var listaGruposdetalle = _repTipoDocumentoAlumno.ObtenerGrupoDetallesGrilla(IdTipoDocumento); //

        //            ListaGrupo = listaGruposdetalle.GroupBy(u => new { u.Id }).Select(group =>
        //                              new TipoDocumentoDetalleDTO
        //                              {
        //                                  Id = group.Select(x => x.Id).FirstOrDefault()
        //                                  ,
        //                                  ModalidadCurso = group.Select(x => new TipoDocumentoAlumnoModalidadCursoDTO { IdModalidad = x.ModalidadCurso }).ToArray()
        //                                  ,
        //                                  EstadoMatricula = group.Select(x => new TipoDocumentoAlumnoEstadoMatriculaDTO { IdEstadoMatricula = x.EstadoMatricula }).ToArray()
        //                                  ,
        //                                  SubEstadoMatricula = group.Select(x => new TipoDocumentoAlumnoSubEstadoMatriculaDTO { IdSubEstadoMatricula = x.SubEstadoMatricula }).ToArray()
        //                                  ,
        //                                  OperadorComparacion = group.Select(x => x.OperadorComparacion).FirstOrDefault()
        //                                  ,
        //                                  TieneDeuda = group.Select(x => x.TieneDeuda).FirstOrDefault(),

        //                              }).ToList();
        //        }
        //        else
        //        {
        //            ListaGrupo = new List<TipoDocumentoDetalleDTO>();
        //        }
        //        return Ok(ListaGrupo);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [Route("[action]/{IdTipoDocumento}")]
        [HttpGet]
        public IActionResult ObtenerDatosParaCondicionesDocumento(int IdTipoDocumento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDocumentoAlumnoRepositorio _repTipoDocumentoAlumno = new TipoDocumentoAlumnoRepositorio();
                var configuracion = _repTipoDocumentoAlumno.ObtenerGrupoDetallesGrilla(IdTipoDocumento);
                var agrupado = configuracion.GroupBy(x => new { x.Id, x.OperadorComparacion, x.TieneDeuda }).Select(g => new TipoDocumentoDetalleDTO
                {
                    Id = g.Key.Id,
                    OperadorComparacion = g.Key.OperadorComparacion,
                    TieneDeuda = g.Key.TieneDeuda,
                    ModalidadCurso = g.GroupBy(y => new { y.ModalidadCurso }).Select(y => new TipoDocumentoAlumnoModalidadCursoDTO
                    {
                        IdModalidad = y.Key.ModalidadCurso
                    }).ToList(),

                    EstadoMatricula = g.GroupBy(y => new { y.EstadoMatricula }).Select(y => new TipoDocumentoAlumnoEstadoMatriculaDTO
                    {
                        IdEstadoMatricula = y.Key.EstadoMatricula
                    }).ToList(),

                    SubEstadoMatricula = g.GroupBy(y => new { y.SubEstadoMatricula }).Select(y => new TipoDocumentoAlumnoSubEstadoMatriculaDTO
                    {
                        IdSubEstadoMatricula = y.Key.SubEstadoMatricula
                    }).ToList()
                }).ToList();


                return Ok(new { ListaGrupo = agrupado });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
