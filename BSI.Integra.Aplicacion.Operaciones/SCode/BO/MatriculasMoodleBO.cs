using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.SCode.BO;
using BSI.Integra.Aplicacion.Servicios.BO;
using BSI.Integra.Aplicacion.Servicios.SCode.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Models.AulaVirtual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	///BO: MatriculasMoodleBO
	///Autor: Jose Villena.
	///Fecha: 03/05/2021
	///<summary>
	///Columnas y funciones de la tabla T_MatriculaMoodle
	///</summary>
	public class MatriculasMoodleBO
	{
		public integraDBContext _integraDBContext;
		public MatriculasMoodleBO(integraDBContext integraDbContext)
		{
			_integraDBContext = integraDbContext;
		}
		AulaVirtualContext moodleContext = new AulaVirtualContext();

		public bool QuitarMatricula(int IdMatriculaCabecera, int IdPespecifico, string Usuario)
		{
			try
			{
				MoodleCronogramaEvaluacionBO moodleCronogramaEvaluacion = new MoodleCronogramaEvaluacionBO();
				MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
				MoodleCursoRepositorio _repMatriculaMoodle = new MoodleCursoRepositorio(_integraDBContext);
				AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);

				MdlUserEnrolments userEnrolments = new MdlUserEnrolments();
				MoodleWebService moodleWebService = new MoodleWebService();
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
				PEspecificoMatriculaAlumnoRepositorio _repPespecificoMatriculaAlumno = new PEspecificoMatriculaAlumnoRepositorio(_integraDBContext);
				CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
				PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);

				var pespecifico = _repPespecifico.FirstById(IdPespecifico);

				var matriculaCabecera = _repMatriculaCabecera.FirstById(IdMatriculaCabecera);
				var pEspecificoNuevaAulaVirtual = _repPespecifico.ObtenerPEspecificoNuevaAulaVirtual();

				if (pespecifico.IdCursoMoodle == null && !pEspecificoNuevaAulaVirtual.Exists(x => x.Id == pespecifico.Id))
				{
					throw new Exception("El centro de costo solicitado no tiene Id Curso Moodle");
				}

				var matriculas = _repMatriculaCabecera.ObtenerCursoMoodle(matriculaCabecera.CodigoMatricula);
				int count = 0;
				foreach (var item in matriculas)
				{
					var matricula = moodleContext.MdlUserEnrolments.Where(x => x.Id == item.IdMatriculaMoodle).FirstOrDefault();
					if (matricula != null)
					{
						MoodleWebServiceRegistrarMatriculaDTO moodleWebServiceRegistrarMatriculaDTO = new MoodleWebServiceRegistrarMatriculaDTO();
						DateTime dia_unix = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
						moodleWebServiceRegistrarMatriculaDTO.userid = item.IdUsuario;
						moodleWebServiceRegistrarMatriculaDTO.courseid = item.IdCurso;
						moodleWebServiceRegistrarMatriculaDTO.roleid = 5;
						moodleWebServiceRegistrarMatriculaDTO.timestart = matricula.Timestart;
						moodleWebServiceRegistrarMatriculaDTO.timeend = (long)(DateTime.Now.AddDays(-1).ToUniversalTime() - dia_unix).TotalSeconds;
						var respuesta = moodleWebService.RegistrarMatricula(moodleWebServiceRegistrarMatriculaDTO);
						if (respuesta.Estado)
						{
							_repPespecificoMatriculaAlumno.EliminarTMatAlumnosMoodle(item.IdMatriculaMoodle);
						}
						else
						{
							count++;
						}
					}

				}

				//Elimina cronograma autoevaluaciones de la matricula anterior
				moodleCronogramaEvaluacion.EliminarTodasVersionesCongeladas(matriculaCabecera.Id, Usuario);

				if (pEspecificoNuevaAulaVirtual.Exists(x => x.Id == pespecifico.Id))
					return true;

				if (count == 0)
				{
					bool matriculaNueva;
					int IdUsuarioMoodle;
					if (matriculas.Count > 0)
					{
						matriculaNueva = this.RegistrarMatriculaMoodle(matriculas.FirstOrDefault().IdUsuario, pespecifico.IdCursoMoodle.Value);
						IdUsuarioMoodle = matriculas.FirstOrDefault().IdUsuario;
					}
					else
					{
						var accesos = _repAlumno.ObtenerAccesosInicialesMoodle(matriculaCabecera.IdAlumno);
						if (accesos != null)
						{
							matriculaNueva = this.RegistrarMatriculaMoodle(Convert.ToInt32(accesos.IdMoodle), pespecifico.IdCursoMoodle.Value);
							IdUsuarioMoodle = Convert.ToInt32(accesos.IdMoodle);
						}
						else
						{
							var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);
							var password = "";
							if (string.IsNullOrEmpty(alumno.Dni))
							{
								password = matriculaCabecera.CodigoMatricula + "Bs1";
								password = password.Trim();
							}
							else
							{
								password = alumno.Dni + "Bs1";
								password = password.Trim();
							}
							var moodleUser = moodleContext.MdlUser.Where(x => x.Email.Equals(alumno.Email1)).FirstOrDefault();
							if (moodleUser != null)
							{
								var userPortal = _repMatriculaCabecera.ObtenerDetalleAccesoPortalWebV4(matriculaCabecera.Id);
								if (userPortal != null && userPortal.Usuario != null)
								{
									if (userPortal.Usuario.Equals(moodleUser.Email))
									{
										_repPespecificoMatriculaAlumno.InsertarTAlumno_Moodle(alumno.Id, moodleUser.Id.ToString(), moodleUser.Email, userPortal.Clave);
									}
									else
									{
										MoodleWebServiceActualizarClaveDTO moodleWebServiceActualizarClave = new MoodleWebServiceActualizarClaveDTO();
										moodleWebServiceActualizarClave.IdMoodle = moodleUser.Id;
										moodleWebServiceActualizarClave.Clave = password;

										var actualizarClave = moodleWebService.ActualizarClaveMoodle(moodleWebServiceActualizarClave);
										if (actualizarClave.Estado)
										{
											_repPespecificoMatriculaAlumno.InsertarTAlumno_Moodle(alumno.Id, moodleUser.Id.ToString(), moodleUser.Username, password);
										}

									}
								}
								else
								{
									MoodleWebServiceActualizarClaveDTO moodleWebServiceActualizarClave = new MoodleWebServiceActualizarClaveDTO();
									moodleWebServiceActualizarClave.IdMoodle = moodleUser.Id;
									moodleWebServiceActualizarClave.Clave = password;

									var actualizarClave = moodleWebService.ActualizarClaveMoodle(moodleWebServiceActualizarClave);
									if (actualizarClave.Estado)
									{
										_repPespecificoMatriculaAlumno.InsertarTAlumno_Moodle(alumno.Id, moodleUser.Id.ToString(), moodleUser.Username, password);
									}
								}

								matriculaNueva = this.RegistrarMatriculaMoodle(Convert.ToInt32(moodleUser.Id), pespecifico.IdCursoMoodle.Value);
								IdUsuarioMoodle = Convert.ToInt32(moodleUser.Id);
							}
							else
							{
								if (alumno.IdCiudad.HasValue)
								{
									var ciudad = _repCiudad.FirstById(alumno.IdCiudad.Value);
									if (ciudad != null)
									{
										var pais = _repPais.FirstById(ciudad.IdPais);
										var usuario = string.Concat(alumno.Nombre1, alumno.ApellidoPaterno).ToLower().Trim();
										moodleUser = moodleContext.MdlUser.Where(x => x.Username.Equals(usuario)).FirstOrDefault();
										if (moodleUser != null)
										{
											if (!string.IsNullOrEmpty(alumno.ApellidoMaterno))
											{
												usuario = string.Concat(usuario, alumno.ApellidoMaterno.Substring(0, 2)).ToLower().Trim();
											}
											else
											{
												usuario = string.Concat(usuario, alumno.Nombre1.Substring(0, 2)).ToLower().Trim();
											}
										}
										//Crear Usuario Moodle
										MoodleWebServiceCrearUsuarioDTO usuarioNuevoMoodle = new MoodleWebServiceCrearUsuarioDTO();
										usuarioNuevoMoodle.firstname = alumno.Nombre1;
										usuarioNuevoMoodle.lastname = alumno.ApellidoPaterno;
										usuarioNuevoMoodle.email = alumno.Email1;
										usuarioNuevoMoodle.country = pais.CodigoPaisMoodle;
										usuarioNuevoMoodle.city = ciudad.Nombre;
										usuarioNuevoMoodle.username = usuario;
										usuarioNuevoMoodle.password = password;
										usuarioNuevoMoodle.auth = "manual";
										var rpta = moodleWebService.CrearUsuario(usuarioNuevoMoodle);
										if (rpta.Estado)
										{

											moodleUser = moodleContext.MdlUser.Where(x => x.Email.Equals(alumno.Email1)).FirstOrDefault();
											_repPespecificoMatriculaAlumno.InsertarTAlumno_Moodle(alumno.Id, moodleUser.Id.ToString(), usuario, password);
											matriculaNueva = this.RegistrarMatriculaMoodle(Convert.ToInt32(moodleUser.Id), pespecifico.IdCursoMoodle.Value);

											IdUsuarioMoodle = Convert.ToInt32(moodleUser.Id);
										}
										else
										{
											throw new Exception(rpta.Mensaje);
										}

									}
									else
									{
										throw new Exception("Se intento registrar el alumno al aula virtual pero tiene una ciudad no valida");
									}
								}
								else
								{
									throw new Exception("Se intento registrar el alumno al aula virtual pero no tiene ciudad");
								}
							}
						}
					}

					if (matriculaNueva)
					{
						InsertarActualizarTmpMatriculasMoodle(IdUsuarioMoodle, pespecifico.IdCursoMoodle.Value, matriculaCabecera.CodigoMatricula, matriculaCabecera.IdAlumno, Usuario);
					}

					//genera cronograma de autoevaluaciones de la matricula nueva
					moodleCronogramaEvaluacion.ObtenerCronogramaAutoEvaluacionUltimaVersion(matriculaCabecera.Id);
				}
				else
				{
					return false;
				}
				return true;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}

		public bool RegistrarMatriculaMoodle(int IdUsuarioMoodle, int IdCursoMoodle)
		{
			
			try
			{
				MoodleCursoRepositorio moodleCursoRepositorio = new MoodleCursoRepositorio(_integraDBContext);
				var moodleCurso = moodleCursoRepositorio.ObtenerMoodleCurso(IdCursoMoodle);
				if(moodleCurso == null)
				{
					throw new Exception("El curso moodle " + IdCursoMoodle + " no ha sido registrado en la base de datos");
				}
				int idCursoMoodle;
				if (moodleCurso.IdCategoria == 5) // Categoria especial para los presenciales
				{
					idCursoMoodle = moodleCurso.IdCursoMoodle;
				}
				else
				{
					var categoria = moodleCursoRepositorio.ObtenerCategoriaMoodleCurso(moodleCurso.IdCategoria);

					if (categoria == null)
					{
						throw new Exception("El curso no tiene categoria correcta.");
					}
					if (categoria.TipoCategoria.Equals("Diplomado")) //si es diplomado
					{
						var moodleCursoPorCategoria = moodleCursoRepositorio.ObtenerMoodleCursosPorCategoria(categoria.IdCategoria).FirstOrDefault();
						idCursoMoodle = moodleCursoPorCategoria.IdCursoMoodle;
					}
					else
					{
						idCursoMoodle = moodleCurso.IdCursoMoodle;
					}
				}

				MoodleWebServiceRegistrarMatriculaDTO moodleWebServiceRegistrarMatriculaDTO = new MoodleWebServiceRegistrarMatriculaDTO();
				MoodleWebService moodleWebService = new MoodleWebService();

				DateTime dia_unix = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				moodleWebServiceRegistrarMatriculaDTO.userid = IdUsuarioMoodle;
				moodleWebServiceRegistrarMatriculaDTO.courseid = idCursoMoodle;
				moodleWebServiceRegistrarMatriculaDTO.roleid = 5;
				moodleWebServiceRegistrarMatriculaDTO.timestart = (long)(DateTime.Now.ToUniversalTime() - dia_unix).TotalSeconds;
				moodleWebServiceRegistrarMatriculaDTO.timeend = (long)(DateTime.Now.AddYears(1).ToUniversalTime() - dia_unix).TotalSeconds;

				var respuesta = moodleWebService.RegistrarMatricula(moodleWebServiceRegistrarMatriculaDTO);
				if (!respuesta.Estado)
				{
					return false;
				}
				else
				{
					return true;
					//throw new Exception("No se pudo matricular al alumno");
				}

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool InsertarActualizarTmpMatriculasMoodle(int IdUsuarioMoodle, int IdCursoMoodle, string CodigoMatricula, int IdAlumno, string Usuario)
		{
			try
			{
				MoodleCursoRepositorio moodleCursoRepositorio = new MoodleCursoRepositorio(_integraDBContext);
				PEspecificoMatriculaAlumnoRepositorio pEspecificoMatriculaAlumnoRepositorio = new PEspecificoMatriculaAlumnoRepositorio(_integraDBContext);
				var datosMoodle = (from matriculas_usuario in moodleContext.MdlUserEnrolments
								   join mat in moodleContext.MdlEnrol on matriculas_usuario.Enrolid equals mat.Id
								   where matriculas_usuario.Userid == IdUsuarioMoodle &&
										 mat.Courseid == IdCursoMoodle
								   select new { matriculas_usuario, mat }).FirstOrDefault();
				var matriculaMoodle = moodleCursoRepositorio.ObtenerCursoMoodlePorMatricula(Convert.ToInt32(datosMoodle.matriculas_usuario.Id), CodigoMatricula);
				if (matriculaMoodle.Count == 0)
				{
					pEspecificoMatriculaAlumnoRepositorio.InsertarTMatAlumnosMoodle(CodigoMatricula, IdAlumno, Convert.ToInt32(datosMoodle.matriculas_usuario.Id), IdUsuarioMoodle, IdCursoMoodle, Usuario);
				}
				var listaCursosMatriculados = moodleCursoRepositorio.ObtenerDatosMatriculaMoodlePorIdUsuarioMoodle(IdUsuarioMoodle);
				if (listaCursosMatriculados.Count > 0)
				{
					var listaCursosMatriculadosMoodle = (from matriculas_usuario in moodleContext.MdlUserEnrolments
														 join mat in moodleContext.MdlEnrol on matriculas_usuario.Enrolid equals mat.Id
														 where matriculas_usuario.Userid == IdUsuarioMoodle
														 select new { matriculas_usuario, mat }).ToList();

					foreach (var item in listaCursosMatriculados)
					{
						var element = listaCursosMatriculadosMoodle.Where(x => x.matriculas_usuario.Id == item.IdMatriculaMoodle).FirstOrDefault();
						if (element != null)
						{
							DateTime timestart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
							timestart = timestart.AddSeconds(element.matriculas_usuario.Timestart).ToLocalTime();
							DateTime timeend = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
							timeend = timeend.AddSeconds(element.matriculas_usuario.Timeend).ToLocalTime();
							pEspecificoMatriculaAlumnoRepositorio.ActualizarTmpMatriculasMoodle(Convert.ToInt32(element.matriculas_usuario.Userid), timestart, timeend, Convert.ToBoolean(element.matriculas_usuario.Status), Convert.ToInt32(element.mat.Courseid), Convert.ToInt32(element.matriculas_usuario.Enrolid), Convert.ToInt32(element.matriculas_usuario.Id));
						}
						else
						{
							pEspecificoMatriculaAlumnoRepositorio.EliminarTmpMatriculasMoodle(Convert.ToInt32(element.matriculas_usuario.Id));
						}
					}
					DateTime fechaIniciof = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
					fechaIniciof = fechaIniciof.AddSeconds(datosMoodle.matriculas_usuario.Timestart).ToLocalTime();
					DateTime fechaFinf = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
					fechaFinf = fechaFinf.AddSeconds(datosMoodle.matriculas_usuario.Timeend).ToLocalTime();
					var matriculaTmpMoodle = moodleCursoRepositorio.ObtenerDatosMatriculaMoodlePorMatriculaMoodle(datosMoodle.matriculas_usuario.Id);
					if (matriculaTmpMoodle.Count == 0)
						pEspecificoMatriculaAlumnoRepositorio.InsertarTmpMatriculasMoodle(Convert.ToInt32(datosMoodle.matriculas_usuario.Userid), fechaIniciof, fechaFinf, Convert.ToBoolean(datosMoodle.matriculas_usuario.Status), Convert.ToInt32(datosMoodle.mat.Courseid), Convert.ToInt32(datosMoodle.matriculas_usuario.Enrolid), Convert.ToInt32(datosMoodle.matriculas_usuario.Id));
				}
				else
				{
					DateTime fechaInicio = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
					fechaInicio = fechaInicio.AddSeconds(datosMoodle.matriculas_usuario.Timestart).ToLocalTime();

					DateTime fechaFin = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
					fechaFin = fechaFin.AddSeconds(datosMoodle.matriculas_usuario.Timeend).ToLocalTime();

					pEspecificoMatriculaAlumnoRepositorio.InsertarTmpMatriculasMoodle(Convert.ToInt32(datosMoodle.matriculas_usuario.Userid), fechaInicio, fechaFin, Convert.ToBoolean(datosMoodle.matriculas_usuario.Status), Convert.ToInt32(datosMoodle.mat.Courseid), Convert.ToInt32(datosMoodle.matriculas_usuario.Enrolid), Convert.ToInt32(datosMoodle.matriculas_usuario.Id));
				}
				return true;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}



		/// Autor: Jose Villena
		/// Fecha: 03/05/2021
		/// Version: 1.0
		/// <summary>
		/// Deshabilita una matricula en especifico en moodle, el campo timeend se pone con fecha de ayer
		/// </summary>
		/// <param name="idMatriculaMoodle"> Id Matricula Moodle </param>
		/// <param name="idUsuarioMoodle"> Id Usuario Moodle </param>
		/// <param name="idCursoMoodle"> Id Curso Moodle </param>		
		/// <returns>bool true / false</returns> 
		public bool DeshabilitarMatriculaPorIdMatriculaMoodle(int idMatriculaMoodle, int idUsuarioMoodle, int idCursoMoodle)
		{
			try
			{
				MoodleWebServiceRegistrarMatriculaDTO moodleWebServiceRegistrarMatriculaDTO = new MoodleWebServiceRegistrarMatriculaDTO();
				MoodleWebService moodleWebService = new MoodleWebService();
				PEspecificoMatriculaAlumnoRepositorio _repPespecificoMatriculaAlumno = new PEspecificoMatriculaAlumnoRepositorio(_integraDBContext);
				var matricula = moodleContext.MdlUserEnrolments.Where(x => x.Id == idMatriculaMoodle).FirstOrDefault();
				DateTime dia_unix = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				moodleWebServiceRegistrarMatriculaDTO.userid = idUsuarioMoodle;
				moodleWebServiceRegistrarMatriculaDTO.courseid = idCursoMoodle;
				moodleWebServiceRegistrarMatriculaDTO.roleid = 5;
				moodleWebServiceRegistrarMatriculaDTO.timestart = matricula.Timestart;
				moodleWebServiceRegistrarMatriculaDTO.timeend = (long)(DateTime.Now.AddDays(-1).ToUniversalTime() - dia_unix).TotalSeconds;
				var respuesta = moodleWebService.RegistrarMatricula(moodleWebServiceRegistrarMatriculaDTO);
				if (respuesta.Estado)
				{
					_repPespecificoMatriculaAlumno.EliminarTMatAlumnosMoodle(idMatriculaMoodle);
				}
				return true;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}



		/// Autor: , Jose Villena
		/// Fecha: 22/03/2021
		/// Version: 1.0
		/// <summary>
		/// Habilita una matricula en especifico en moodle, el campo timeend se pone con fecha de ayer  
		/// </summary>
		/// <param name="idUsuarioMoodle"> Id Usuario Moodle </param>
		/// <param name="idCursoMoodle"> Id Curso Moodle </param>
		/// <param name="fechaInicio"> Fecha de Inicio </param>
		/// <param name="fechaFin"> Fecha Fin </param>
		/// <param name="codigoMatricula"> Codigo Matricula </param>
		/// <param name="idAlumno"> Id Alumno </param>
		/// <param name="usuario"> Usuario </param>
		/// <returns>bool true / false</returns> 
		public bool RegistrarHabilitarMatriculaMoodleConFechas(int idUsuarioMoodle, int idCursoMoodle, DateTime fechaInicio, DateTime fechaFin, string codigoMatricula, int idAlumno, string usuario)
		{
			try
			{
				MoodleWebServiceRegistrarMatriculaDTO moodleWebServiceRegistrarMatriculaDTO = new MoodleWebServiceRegistrarMatriculaDTO();
				MoodleCursoRepositorio _moodleCursoRepositorio = new MoodleCursoRepositorio(_integraDBContext);
				MoodleWebService moodleWebService = new MoodleWebService();
				PEspecificoMatriculaAlumnoRepositorio _repPespecificoMatriculaAlumno = new PEspecificoMatriculaAlumnoRepositorio(_integraDBContext);

				DateTime dia_unix = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				moodleWebServiceRegistrarMatriculaDTO.userid = idUsuarioMoodle;
				moodleWebServiceRegistrarMatriculaDTO.courseid = idCursoMoodle;
				moodleWebServiceRegistrarMatriculaDTO.roleid = 5;
				moodleWebServiceRegistrarMatriculaDTO.timestart = (long)(fechaInicio.ToUniversalTime() - dia_unix).TotalSeconds;
				moodleWebServiceRegistrarMatriculaDTO.timeend = (long)(fechaFin.ToUniversalTime() - dia_unix).TotalSeconds;
				var respuesta = moodleWebService.RegistrarMatricula(moodleWebServiceRegistrarMatriculaDTO);
				if (respuesta.Estado)
				{
					var datosMoodle = (from matriculas_usuario in moodleContext.MdlUserEnrolments
									   join mat in moodleContext.MdlEnrol on matriculas_usuario.Enrolid equals mat.Id
									   where matriculas_usuario.Userid == idUsuarioMoodle &&
											 mat.Courseid == idCursoMoodle
									   select new { matriculas_usuario, mat }).FirstOrDefault();
					var matriculaMoodle = _moodleCursoRepositorio.ObtenerCursoMoodlePorMatricula(Convert.ToInt32(datosMoodle.matriculas_usuario.Id), codigoMatricula);
					if (matriculaMoodle.Count == 0)
					{
						_repPespecificoMatriculaAlumno.InsertarTMatAlumnosMoodle(codigoMatricula, idAlumno, Convert.ToInt32(datosMoodle.matriculas_usuario.Id), idUsuarioMoodle, idCursoMoodle, usuario);
					}
					return true;
				}
				else
				{
					return false;
				}

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


		/// Autor: , Jose Villena
		/// Fecha: 22/03/2021
		/// Version: 1.0
		/// <summary>
		/// Regulariza la tabla TmpMatriculasMoodle
		/// </summary>
		/// <param name="idUsuarioMoodle"> Id Usuario Moodle </param>
		/// <param name="idCursoMoodle"> Id Curso Moodle </param>
		/// <param name="codigoMatricula"> Codigo Matricula </param>
		/// <param name="idAlumno"> Id Alumno </param>
		/// <param name="usuario"> Usuario </param>
		/// <returns>bool true / false</returns> 
		public bool RegularizarTmpMatriculasMoodle(int idUsuarioMoodle, int idCursoMoodle, string codigoMatricula, int idAlumno, string usuario)
		{
			try
			{
				MoodleCursoRepositorio moodleCursoRepositorio = new MoodleCursoRepositorio(_integraDBContext);
				PEspecificoMatriculaAlumnoRepositorio pEspecificoMatriculaAlumnoRepositorio = new PEspecificoMatriculaAlumnoRepositorio(_integraDBContext);
				var datosMoodle = (from matriculas_usuario in moodleContext.MdlUserEnrolments
								   join mat in moodleContext.MdlEnrol on matriculas_usuario.Enrolid equals mat.Id
								   where matriculas_usuario.Userid == idUsuarioMoodle &&
										 mat.Courseid == idCursoMoodle
								   select new { matriculas_usuario, mat }).FirstOrDefault();
				var matriculaMoodle = moodleCursoRepositorio.ObtenerCursoMoodlePorMatricula(Convert.ToInt32(datosMoodle.matriculas_usuario.Id), codigoMatricula);
				if (matriculaMoodle.Count == 0)
				{
					//pEspecificoMatriculaAlumnoRepositorio.InsertarTMatAlumnosMoodle(CodigoMatricula, IdAlumno, Convert.ToInt32(datosMoodle.matriculas_usuario.Id), IdUsuarioMoodle, idCursoMoodle, Usuario);
				}
				var listaCursosMatriculados = moodleCursoRepositorio.ObtenerDatosMatriculaMoodlePorIdUsuarioMoodle(idUsuarioMoodle);
				if (listaCursosMatriculados.Count > 0)
				{
					var listaCursosMatriculadosMoodle = (from matriculas_usuario in moodleContext.MdlUserEnrolments
														 join mat in moodleContext.MdlEnrol on matriculas_usuario.Enrolid equals mat.Id
														 where matriculas_usuario.Userid == idUsuarioMoodle
														 select new { matriculas_usuario, mat }).ToList();

					foreach (var item in listaCursosMatriculados)
					{
						var element = listaCursosMatriculadosMoodle.Where(x => x.matriculas_usuario.Id == item.IdMatriculaMoodle).FirstOrDefault();
						if (element != null)
						{
							DateTime timestart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
							timestart = timestart.AddSeconds(element.matriculas_usuario.Timestart).ToLocalTime();
							DateTime timeend = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
							timeend = timeend.AddSeconds(element.matriculas_usuario.Timeend).ToLocalTime();
							pEspecificoMatriculaAlumnoRepositorio.ActualizarTmpMatriculasMoodle(Convert.ToInt32(element.matriculas_usuario.Userid), timestart, timeend, Convert.ToBoolean(element.matriculas_usuario.Status), Convert.ToInt32(element.mat.Courseid), Convert.ToInt32(element.matriculas_usuario.Enrolid), Convert.ToInt32(element.matriculas_usuario.Id));
						}
						else
						{
							pEspecificoMatriculaAlumnoRepositorio.EliminarTmpMatriculasMoodle(Convert.ToInt32(element.matriculas_usuario.Id));
						}
					}
					var matriculaCursoMoodle = listaCursosMatriculadosMoodle.Where(x => x.matriculas_usuario.Userid == idUsuarioMoodle && x.mat.Courseid == idCursoMoodle).OrderByDescending(x => x.matriculas_usuario.Timecreated).FirstOrDefault();
					var matriculaCursoIntegra = listaCursosMatriculados.Where(x => x.IdUsuarioMoodle == idUsuarioMoodle && x.IdCursoMoodle == idCursoMoodle).FirstOrDefault();
					if (matriculaCursoMoodle != null && matriculaCursoIntegra == null)
					{
						DateTime timestart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
						timestart = timestart.AddSeconds(matriculaCursoMoodle.matriculas_usuario.Timestart).ToLocalTime();
						DateTime timeend = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
						timeend = timeend.AddSeconds(matriculaCursoMoodle.matriculas_usuario.Timeend).ToLocalTime();
						pEspecificoMatriculaAlumnoRepositorio.InsertarTmpMatriculasMoodle(idUsuarioMoodle, timestart, timeend, Convert.ToBoolean(matriculaCursoMoodle.matriculas_usuario.Status), idCursoMoodle, Convert.ToInt32(matriculaCursoMoodle.matriculas_usuario.Enrolid), Convert.ToInt32(matriculaCursoMoodle.matriculas_usuario.Id));
					}
				}
				
				return true;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			
		}

	}
}
