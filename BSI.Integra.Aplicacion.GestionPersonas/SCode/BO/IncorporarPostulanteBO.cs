using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Helpers;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    public class IncorporarPostulanteBO
    {
		private readonly integraDBContext _integraDBContext;
		private readonly ProveedorRepositorio _repProveedor;
		private readonly PersonalRepositorio _repPersonal;		

		public IncorporarPostulanteBO(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
		}

		public bool IncorporarPostulantePersonal(int IdPostulante)
        {
			PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
			PersonalLogRepositorio _repPersonalLog = new PersonalLogRepositorio(_integraDBContext);
			PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
			PersonalIdiomaRepositorio _repPersonalIdioma = new PersonalIdiomaRepositorio(_integraDBContext);
			PersonalFormacionRepositorio _repPersonalFormacion = new PersonalFormacionRepositorio(_integraDBContext);
			PersonalExperienciaRepositorio _repPersonalExperiencia = new PersonalExperienciaRepositorio(_integraDBContext);
			IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio(_integraDBContext);
			UsuarioRepositorio _repUsuario = new UsuarioRepositorio(_integraDBContext);
			PostulanteIdiomaRepositorio _repPostulanteIdioma = new PostulanteIdiomaRepositorio(_integraDBContext);
			PostulanteFormacionRepositorio _repPostulanteFormacion = new PostulanteFormacionRepositorio(_integraDBContext);
			PostulanteExperienciaRepositorio _repPostulanteExperiencia = new PostulanteExperienciaRepositorio(_integraDBContext);			

			PersonaBO persona = new PersonaBO(_integraDBContext);

			using (TransactionScope scope = new TransactionScope())
			{
				int? IdPersonaClasificacion = null;
				var postulante = _repPostulante.FirstById(IdPostulante);
				var postulanteFormacion = _repPostulanteFormacion.GetBy(x => x.IdPostulante == IdPostulante);
				var postulanteIdioma = _repPostulanteIdioma.GetBy(x => x.IdPostulante == IdPostulante);
				var postulanteExperiencia = _repPostulanteExperiencia.GetBy(x => x.IdPostulante == IdPostulante);
				var listaRolArea = _repPersonal.ObtenerCodigoRolPersonal(IdPostulante);
				var flagCrear = false;
				var flagModificar = false;
				var flagGenerarCorreo = false;
				var validar = "";
				var salida = 0;

				var usuariogenerado = string.Concat(postulante.Nombre.Substring(0, 1).Trim(), postulante.ApellidoPaterno.Trim()).ToLower();
				var correogenerado = string.Concat(usuariogenerado, "@bsginstitute.com").ToLower();

				var IdPersonalEmailRepetido = _repPersonal.ObtenerEmailEstadoFalse(correogenerado);
				var ListEmailRepetidoValido = _repPersonal.ObtenerListaEmailRepetidosActivos(correogenerado);


				if (ListEmailRepetidoValido.Count > 0 && IdPersonalEmailRepetido == 0)
				{
					flagGenerarCorreo = true;
				}

				if (ListEmailRepetidoValido.Count > 0 && IdPersonalEmailRepetido > 0)
				{
					validar = _repPersonal.ObtenerNumeroDocumento(IdPersonalEmailRepetido);
					if (!string.IsNullOrEmpty(validar) && validar == postulante.NroDocumento)
					{
						string errorMessage = ("El postulante ya es un personal activo: " + postulante.Id + " " + postulante.Email);
						return false;
					}
					flagGenerarCorreo = true;
				}

				if (ListEmailRepetidoValido.Count == 0 && IdPersonalEmailRepetido > 0)
				{
					validar = _repPersonal.ObtenerNumeroDocumento(IdPersonalEmailRepetido);
					if (!string.IsNullOrEmpty(validar) && validar == postulante.NroDocumento)
					{
						_repPersonal.ActivarPersonal(IdPersonalEmailRepetido);
						flagModificar = true;
						salida = IdPersonalEmailRepetido;
					}
					else
					{
						flagGenerarCorreo = true;
					}
				}

				if (flagGenerarCorreo == true)
				{
					var i = 1;
					while (ListEmailRepetidoValido.Count > 0 || IdPersonalEmailRepetido > 0)
					{
						if (postulante.ApellidoMaterno != null)
						{
							usuariogenerado = string.Concat(postulante.Nombre.Substring(0, i).Trim(), postulante.ApellidoPaterno.Trim(), postulante.ApellidoMaterno.Substring(0, i)).ToLower();
							correogenerado = string.Concat(usuariogenerado, "@bsginstitute.com");
							ListEmailRepetidoValido = _repPersonal.ObtenerListaEmailRepetidosActivos(correogenerado);
							IdPersonalEmailRepetido = _repPersonal.ObtenerEmailEstadoFalse(correogenerado);
							i++;
							//Validar si es nulo o no
							if (IdPersonalEmailRepetido > 0)
							{
								validar = _repPersonal.ObtenerNumeroDocumento(IdPersonalEmailRepetido);
								if (!string.IsNullOrEmpty(validar) && validar == postulante.NroDocumento)
								{
									_repPersonal.ActivarPersonal(IdPersonalEmailRepetido);
									flagModificar = true;
									salida = IdPersonalEmailRepetido;
									IdPersonalEmailRepetido = 0;
								}

							}
							if (ListEmailRepetidoValido.Count > 0 && IdPersonalEmailRepetido > 0)
							{
								validar = _repPersonal.ObtenerNumeroDocumento(IdPersonalEmailRepetido);
								if (!string.IsNullOrEmpty(validar) && validar == postulante.NroDocumento)
								{
									string errorMessage = ("El postulante ya es un personal activo: " + postulante.Id + " " + postulante.Email);
									return false;
								}
							}
						}
						else
						{
							usuariogenerado = string.Concat(postulante.Nombre.Substring(0, i).Trim(), postulante.ApellidoPaterno.Trim()).ToLower();
							correogenerado = string.Concat(usuariogenerado, "@bsginstitute.com");
							ListEmailRepetidoValido = _repPersonal.ObtenerListaEmailRepetidosActivos(correogenerado);
							i++;
							//Validar si es nulo
							if (IdPersonalEmailRepetido > 0)
							{
								validar = _repPersonal.ObtenerNumeroDocumento(IdPersonalEmailRepetido);
								if (!string.IsNullOrEmpty(validar) && validar == postulante.NroDocumento)
								{
									_repPersonal.ActivarPersonal(IdPersonalEmailRepetido);
									flagModificar = true;
									salida = IdPersonalEmailRepetido;
									IdPersonalEmailRepetido = 0;
								}

							}
							if (ListEmailRepetidoValido.Count > 0 && IdPersonalEmailRepetido > 0)
							{
								validar = _repPersonal.ObtenerNumeroDocumento(IdPersonalEmailRepetido);
								if (!string.IsNullOrEmpty(validar) && validar == postulante.NroDocumento)
								{
									return false;
								}
							}
						}
					}
				}

				if (IdPersonalEmailRepetido == 0 && ListEmailRepetidoValido.Count == 0 && flagModificar == false) //Si NO existe un correo igual inactivo
				{
					flagCrear = true;
				}

				if (flagCrear == true)
				{
					PersonalBO personal = new PersonalBO
					{
						Nombres = postulante.Nombre,
						ApellidoPaterno = postulante.ApellidoPaterno,
						ApellidoMaterno = postulante.ApellidoMaterno,
						Rol = listaRolArea.Nombre,
						Email = correogenerado,
						FechaNacimiento = postulante.FechaNacimiento,
						IdPaisNacimiento = postulante.IdPais,
						IdCiudad = postulante.IdCiudad,
						IdSexo = postulante.IdSexo,
						Apellidos = postulante.ApellidoPaterno + " " + postulante.ApellidoMaterno,
						IdTipoDocumento = postulante.IdTipoDocumento,
						NumeroDocumento = postulante.NroDocumento,
						MovilReferencia = postulante.Celular,
						EmailReferencia = postulante.Email,
						IdPostulante = postulante.Id,
						Estado = true,
						UsuarioCreacion = "portalwebr",
						UsuarioModificacion = "portalwebr",
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now,
					};
					_repPersonal.Insert(personal);
					foreach (var item in postulanteFormacion)
					{
						PersonalFormacionBO personalFormacion = new PersonalFormacionBO
						{
							IdPersonal = personal.Id,
							IdCentroEstudio = item.IdCentroEstudio,
							IdTipoEstudio = item.IdTipoEstudio,
							IdAreaFormacion = item.IdAreaFormacion,
							IdEstadoEstudio = item.IdEstadoEstudio,
							FechaInicio = item.FechaInicio,
							FechaFin = item.FechaFin,
							AlaActualidad = item.AlaActualidad,
							Estado = true,
							UsuarioCreacion = "portalwebr",
							UsuarioModificacion = "portalwebr",
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now,
						};
						_repPersonalFormacion.Insert(personalFormacion);
					}
					foreach (var item in postulanteIdioma)
					{
						PersonalIdiomaBO personalIdioma = new PersonalIdiomaBO
						{
							IdPersonal = personal.Id,
							IdIdioma = item.IdIdioma,
							IdNivelIdioma = item.IdNivelIdioma,
							Estado = true,
							UsuarioCreacion = "portalwebr",
							UsuarioModificacion = "portalwebr",
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now,
						};
						_repPersonalIdioma.Insert(personalIdioma);
					}
					foreach (var item in postulanteExperiencia)
					{
						PersonalExperienciaBO personalExperiencia = new PersonalExperienciaBO
						{
							IdPersonal = personal.Id,
							IdEmpresa = item.IdEmpresa,
							IdAreaTrabajo = item.IdAreaTrabajo,
							IdCargo = item.IdCargo,
							FechaIngreso = item.FechaInicio,
							FechaRetiro = item.FechaFin,
							NombreJefeInmediato = item.NombreJefe,
							TelefonoJefeInmediato = item.NumeroJefe,
							IdIndustria = item.IdIndustria,
							Estado = true,
							UsuarioCreacion = "portalwebr",
							UsuarioModificacion = "portalwebr",
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now,
						};
						_repPersonalExperiencia.Insert(personalExperiencia);
					}


					IdPersonaClasificacion = persona.InsertarPersona(personal.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Personal, "portalwebr");
					if (IdPersonaClasificacion == null)
					{
						throw new Exception("Error al insertar el Tipo Persona Clasificacion");
					}
					if (personal != null)
					{
						PersonalLogBO personalLogBO = new PersonalLogBO();
						personalLogBO.IdPersonal = personal.Id;
						personalLogBO.Rol = listaRolArea.Nombre;
						personalLogBO.TipoPersonal = personal.TipoPersonal;
						personalLogBO.IdJefe = personal.IdJefe;
						personalLogBO.EstadoRol = true;
						personalLogBO.EstadoTipoPersonal = true;
						personalLogBO.EstadoIdJefe = true;
						personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
						personalLogBO.FechaFin = null;
						personalLogBO.Estado = true;
						personalLogBO.UsuarioModificacion = "portalwebr";
						personalLogBO.UsuarioCreacion = "portalwebr";
						personalLogBO.FechaCreacion = DateTime.Now;
						personalLogBO.FechaModificacion = DateTime.Now;
						_repPersonalLog.Insert(personalLogBO);
					}

					var clavegenerada = "";
					if (postulante.ApellidoMaterno != null)
					{
						clavegenerada = string.Concat(postulante.Nombre.Substring(0, 1).Trim(),
													  postulante.ApellidoPaterno.Substring(0, 1).Trim(),
													  personal.Id,
													  postulante.ApellidoMaterno.Substring(0, 2).Trim()
													  ).ToUpper();
					}
					else
					{
						clavegenerada = string.Concat(postulante.Nombre.Substring(0, 1).Trim(),
													  postulante.ApellidoPaterno.Trim(),
													  personal.Id,
													  postulante.ApellidoPaterno.Substring(0, 4).Trim()
													  ).ToUpper();
					}

					var validacion = _repUsuario.GetBy(o => true, x => new { x.Id, x.NombreUsuario }).Where(x => x.NombreUsuario == usuariogenerado).ToList();
					if (validacion.Count() == 1)
					{
						throw new System.Exception("El nombre de usuario ya existe");
					}

					UsuarioBO UserBO = new UsuarioBO()
					{
						IdPersonal = personal.Id,
						NombreUsuario = usuariogenerado,
						Clave = _repRegistrosIntegra.Encriptar(clavegenerada),
						IdUsuarioRol = 99,
						CodigoAreaTrabajo = listaRolArea.Codigo,
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now,
						Estado = true,
						UsuarioCreacion = "portalwebr",
						UsuarioModificacion = "portalwebr"
					};
					_repUsuario.Insert(UserBO);

					IntegraAspNetUsersBO IntegraBO = new IntegraAspNetUsersBO()
					{
						Id = Guid.NewGuid().ToString(),
						PasswordHash = Crypto.HashPassword(clavegenerada),
						UsClave = clavegenerada,
						PerId = personal.Id,
						AreaTrabajo = listaRolArea.Codigo,
						RolId = 99,
						Email = correogenerado,
						EmailConfirmed = true,
						Estado = true,
						UserName = usuariogenerado,
						UsuarioCreacion = "portalwebr",
						UsuarioModificacion = "portalwebr",
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now
					};
					_repRegistrosIntegra.Insert(IntegraBO);

					scope.Complete();
					return true;
				}
				if (flagModificar == true) // ----------------------------------- MODIFICAR -------------------------------------
				{
					IdPersonalEmailRepetido = salida;

					PersonalBO personal = new PersonalBO();
					personal = _repPersonal.FirstById(IdPersonalEmailRepetido);

					personal.Nombres = postulante.Nombre;
					personal.ApellidoPaterno = postulante.ApellidoPaterno;
					personal.ApellidoMaterno = postulante.ApellidoMaterno;
					personal.Rol = listaRolArea.Nombre;
					personal.FechaNacimiento = postulante.FechaNacimiento;
					personal.IdPaisNacimiento = postulante.IdPais;
					personal.IdCiudad = postulante.IdCiudad;
					personal.IdSexo = postulante.IdSexo;
					personal.Apellidos = postulante.ApellidoPaterno + " " + postulante.ApellidoMaterno;
					personal.IdTipoDocumento = postulante.IdTipoDocumento;
					personal.NumeroDocumento = postulante.NroDocumento;
					personal.MovilReferencia = postulante.Celular;
					personal.EmailReferencia = postulante.Email;
					personal.IdPostulante = postulante.Id;
					personal.Estado = true;
					personal.UsuarioCreacion = "portalwebr";
					personal.UsuarioModificacion = "portalwebr";
					personal.FechaCreacion = DateTime.Now;
					personal.FechaModificacion = DateTime.Now;

					_repPersonal.Update(personal);

					foreach (var item in postulanteFormacion)
					{

						PersonalFormacionBO personalFormacion = new PersonalFormacionBO();
						personalFormacion = _repPersonalFormacion.FirstBy(x => x.Estado == true && x.IdPersonal.Equals(IdPersonalEmailRepetido));

						personalFormacion.IdCentroEstudio = item.IdCentroEstudio;
						personalFormacion.IdTipoEstudio = item.IdTipoEstudio;
						personalFormacion.IdAreaFormacion = item.IdAreaFormacion;
						personalFormacion.IdEstadoEstudio = item.IdEstadoEstudio;
						personalFormacion.FechaInicio = item.FechaInicio;
						personalFormacion.FechaFin = item.FechaFin;
						personalFormacion.AlaActualidad = item.AlaActualidad;
						personalFormacion.Estado = true;
						personalFormacion.UsuarioCreacion = "portalwebr";
						personalFormacion.UsuarioModificacion = "portalwebr";
						personalFormacion.FechaCreacion = DateTime.Now;
						personalFormacion.FechaModificacion = DateTime.Now;

						_repPersonalFormacion.Update(personalFormacion);
					}
					foreach (var item in postulanteIdioma)
					{
						PersonalIdiomaBO personalIdioma = new PersonalIdiomaBO();
						personalIdioma = _repPersonalIdioma.FirstBy(x => x.Estado == true && x.IdPersonal.Equals(IdPersonalEmailRepetido));

						personalIdioma.IdIdioma = item.IdIdioma;
						personalIdioma.IdNivelIdioma = item.IdNivelIdioma;
						personalIdioma.Estado = true;
						personalIdioma.UsuarioCreacion = "portalwebr";
						personalIdioma.UsuarioModificacion = "portalwebr";
						personalIdioma.FechaCreacion = DateTime.Now;
						personalIdioma.FechaModificacion = DateTime.Now;

						_repPersonalIdioma.Update(personalIdioma);
					}

					foreach (var item in postulanteExperiencia)
					{
						PersonalExperienciaBO personalExperiencia = new PersonalExperienciaBO();

						personalExperiencia = _repPersonalExperiencia.FirstBy(x => x.Estado == true && x.IdPersonal.Equals(IdPersonalEmailRepetido));

						personalExperiencia.IdEmpresa = item.IdEmpresa;
						personalExperiencia.IdAreaTrabajo = item.IdAreaTrabajo;
						personalExperiencia.IdCargo = item.IdCargo;
						personalExperiencia.FechaIngreso = item.FechaInicio;
						personalExperiencia.FechaRetiro = item.FechaFin;
						personalExperiencia.NombreJefeInmediato = item.NombreJefe;
						personalExperiencia.TelefonoJefeInmediato = item.NumeroJefe;
						personalExperiencia.IdIndustria = item.IdIndustria;
						personalExperiencia.Estado = true;
						personalExperiencia.UsuarioCreacion = "portalwebr";
						personalExperiencia.UsuarioModificacion = "portalwebr";
						personalExperiencia.FechaCreacion = DateTime.Now;
						personalExperiencia.FechaModificacion = DateTime.Now;

						_repPersonalExperiencia.Update(personalExperiencia);
					}


					IdPersonaClasificacion = persona.InsertarPersona(personal.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Personal, "portalwebr");
					if (IdPersonaClasificacion == null)
					{
						throw new Exception("Error al insertar el Tipo Persona Clasificacion");
					}

					if (personal != null)
					{
						PersonalLogBO personalLogBO = new PersonalLogBO();
						personalLogBO = _repPersonalLog.FirstBy(x => x.Estado == true && x.IdPersonal.Equals(IdPersonalEmailRepetido));

						personalLogBO.Rol = listaRolArea.Nombre;
						personalLogBO.TipoPersonal = personal.TipoPersonal;
						personalLogBO.IdJefe = personal.IdJefe;
						personalLogBO.EstadoRol = true;
						personalLogBO.EstadoTipoPersonal = true;
						personalLogBO.EstadoIdJefe = true;
						personalLogBO.FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
						personalLogBO.FechaFin = null;
						personalLogBO.Estado = true;
						personalLogBO.UsuarioModificacion = "portalwebr";
						personalLogBO.UsuarioCreacion = "portalwebr";
						personalLogBO.FechaCreacion = DateTime.Now;
						personalLogBO.FechaModificacion = DateTime.Now;
						_repPersonalLog.Update(personalLogBO);
					}

					var clavegenerada = "";
					if (postulante.ApellidoMaterno != null)
					{
						clavegenerada = string.Concat(postulante.Nombre.Substring(0, 2).Trim(),
													  postulante.Id,
													  postulante.ApellidoPaterno.Substring(0, 2).Trim(),
													  postulante.ApellidoMaterno.Substring(0, 2).Trim()
													  ).ToUpper();
					}
					else
					{
						clavegenerada = string.Concat(postulante.Nombre.Substring(0, 2).Trim(),
													  postulante.Id,
													  postulante.ApellidoPaterno.Substring(0, 4).Trim()
													  ).ToUpper();
					}

					//var UsuarioRol = _repPersonal.
					UsuarioBO UserBO = new UsuarioBO();
					UserBO = _repUsuario.FirstBy(x => x.Estado == true && x.IdPersonal.Equals(IdPersonalEmailRepetido));

					//UserBO.IdPersonal = personal.Id;
					UserBO.NombreUsuario = usuariogenerado;
					UserBO.Clave = _repRegistrosIntegra.Encriptar(clavegenerada);
					UserBO.IdUsuarioRol = 99;
					UserBO.CodigoAreaTrabajo = listaRolArea.Codigo;
					UserBO.FechaCreacion = DateTime.Now;
					UserBO.FechaModificacion = DateTime.Now;
					UserBO.Estado = true;
					UserBO.UsuarioCreacion = "portalwebr";
					UserBO.UsuarioModificacion = "portalwebr";

					_repUsuario.Update(UserBO);

					IntegraAspNetUsersBO IntegraBO = new IntegraAspNetUsersBO();
					IntegraBO = _repRegistrosIntegra.FirstBy(x => x.Estado == true && x.PerId.Equals(IdPersonalEmailRepetido));

					//IntegraBO.PasswordHash = Crypto.HashPassword(usuariogenerado);
					IntegraBO.UsClave = usuariogenerado;
					//IntegraBO.PerId = personal.Id;
					IntegraBO.AreaTrabajo = listaRolArea.Nombre;
					IntegraBO.RolId = 99;
					IntegraBO.EmailConfirmed = true;
					IntegraBO.Estado = true;
					IntegraBO.UserName = usuariogenerado;
					IntegraBO.UsuarioCreacion = "portalwebr";
					IntegraBO.UsuarioModificacion = "portalwebr";
					IntegraBO.FechaCreacion = DateTime.Now;
					IntegraBO.FechaModificacion = DateTime.Now;

					_repRegistrosIntegra.Update(IntegraBO);

					scope.Complete();
					return true;

				}
				scope.Complete();
				return true;
			}
		}
    }
}
