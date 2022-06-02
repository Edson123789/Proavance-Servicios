using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	/// BO: ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO
	/// Autor: Jose Villena .
	/// Fecha: 28/05/2021
	/// <summary>
	/// Columnas y funciones de la tabla T_ConfiguracionAsignacionCoordinadorOportunidadOperaciones
	/// </summary>
	public class ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO : BaseBO
	{
		/// Propiedades		                    Significado
		/// -------------	                    -----------------------
		/// IdPersonal                          Id del Personal
		/// IdCentroCosto                       Id de Centro Costo Padre
		/// IdCentroCostoHijo                   Id de centro de costo Hijo
		/// IdMigracion                         Id de Migracion

		public int IdPersonal { get; set; }
		public int IdCentroCosto { get; set; }
		public int? IdCentroCostoHijo { get; set; }
		public int? IdEstadoMatricula { get; set; }
		public int? IdSubEstadoMatricula { get; set; }
		public int? IdMigracion { get; set; }


		
		/// Autor: Jose Villena
		/// Fecha: 28/05/2021
		/// Version: 1.0
		/// <summary>
		/// Inserta o actualiza un registro de configuracion de coordinadoras
		/// </summary>
		/// <param name="ConfiguracionCoordinador">List<ConfiguracionCoordinadorDTO></param>
		/// <returns>Bool: True/False</returns>
		public bool InsertarActualizarConfiguracionCoordinador(List<ConfiguracionCoordinadorDTO> ConfiguracionCoordinador)
		{
			try
			{
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio repConfiguracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio();
				foreach (var configuracion in ConfiguracionCoordinador)
				{
					using (TransactionScope scope = new TransactionScope())
					{
						foreach (var personal in configuracion.ListaPersonal)
						{
							if (configuracion.ListaEstadoMatricula.Length > 0)
							{
								foreach (var estadomatricula in configuracion.ListaEstadoMatricula)
								{
									if (configuracion.ListaSubEstadoMatricula.Length > 0)
									{
										foreach (var subestadomatricula in configuracion.ListaSubEstadoMatricula)
										{
											foreach (var centroCosto in configuracion.ListaCentroCosto)
											{
												ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
												
												var centroCostoHijos = repConfiguracionCoordinador.ObtenerCentroCostoHijos(centroCosto);
												if (centroCostoHijos.Count > 0) // Es padre
												{
													foreach (var item in centroCostoHijos)
													{
														configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
														configuracionCoordinador.IdPersonal = personal;
														configuracionCoordinador.IdCentroCosto = centroCosto;
														configuracionCoordinador.IdCentroCostoHijo = item.IdCentroCostoHijo;
														configuracionCoordinador.IdEstadoMatricula = estadomatricula;
														configuracionCoordinador.IdSubEstadoMatricula = subestadomatricula;
														configuracionCoordinador.Estado = true;
														configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
														configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
														configuracionCoordinador.FechaCreacion = DateTime.Now;
														configuracionCoordinador.FechaModificacion = DateTime.Now;
														repConfiguracionCoordinador.Insert(configuracionCoordinador);
													}
												}
												else // Es Individual
												{
													configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
													configuracionCoordinador.IdPersonal = personal;
													configuracionCoordinador.IdCentroCosto = centroCosto;
													configuracionCoordinador.IdEstadoMatricula = estadomatricula;
													configuracionCoordinador.IdSubEstadoMatricula = subestadomatricula;
													configuracionCoordinador.Estado = true;
													configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
													configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
													configuracionCoordinador.FechaCreacion = DateTime.Now;
													configuracionCoordinador.FechaModificacion = DateTime.Now;
													repConfiguracionCoordinador.Insert(configuracionCoordinador);
												}
											}
										}
									}
									else
									{
										foreach (var centroCosto in configuracion.ListaCentroCosto)
										{
											ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
											
											var centroCostoHijos = repConfiguracionCoordinador.ObtenerCentroCostoHijos(centroCosto);
											if (centroCostoHijos.Count > 0) // Es padre
											{
												foreach (var item in centroCostoHijos)
												{
													configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
													configuracionCoordinador.IdPersonal = personal;
													configuracionCoordinador.IdCentroCosto = centroCosto;
													configuracionCoordinador.IdCentroCostoHijo = item.IdCentroCostoHijo;
													configuracionCoordinador.IdEstadoMatricula = estadomatricula;
													configuracionCoordinador.IdSubEstadoMatricula = null;
													configuracionCoordinador.Estado = true;
													configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
													configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
													configuracionCoordinador.FechaCreacion = DateTime.Now;
													configuracionCoordinador.FechaModificacion = DateTime.Now;
													repConfiguracionCoordinador.Insert(configuracionCoordinador);
												}
											}
											else // Es Individual
											{
												configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
												configuracionCoordinador.IdPersonal = personal;
												configuracionCoordinador.IdCentroCosto = centroCosto;
												configuracionCoordinador.IdEstadoMatricula = estadomatricula;
												configuracionCoordinador.IdSubEstadoMatricula = null;
												configuracionCoordinador.Estado = true;
												configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
												configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
												configuracionCoordinador.FechaCreacion = DateTime.Now;
												configuracionCoordinador.FechaModificacion = DateTime.Now;
												repConfiguracionCoordinador.Insert(configuracionCoordinador);
											}
										}
									}
									
								}
							}
							else
							{
								foreach (var centroCosto in configuracion.ListaCentroCosto)
								{
									ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
									
									var centroCostoHijos = repConfiguracionCoordinador.ObtenerCentroCostoHijos(centroCosto);
									if (centroCostoHijos.Count > 0) // Es padre
									{
										foreach (var item in centroCostoHijos)
										{
											configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
											configuracionCoordinador.IdPersonal = personal;
											configuracionCoordinador.IdCentroCosto = centroCosto;
											configuracionCoordinador.IdCentroCostoHijo = item.IdCentroCostoHijo;
											configuracionCoordinador.IdEstadoMatricula = null;
											configuracionCoordinador.IdSubEstadoMatricula = null;
											configuracionCoordinador.Estado = true;
											configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
											configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
											configuracionCoordinador.FechaCreacion = DateTime.Now;
											configuracionCoordinador.FechaModificacion = DateTime.Now;
											repConfiguracionCoordinador.Insert(configuracionCoordinador);
										}
									}
									else // Es Individual
									{
										configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
										configuracionCoordinador.IdPersonal = personal;
										configuracionCoordinador.IdCentroCosto = centroCosto;
										configuracionCoordinador.IdEstadoMatricula = null;
										configuracionCoordinador.IdSubEstadoMatricula = null;
										configuracionCoordinador.Estado = true;
										configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
										configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
										configuracionCoordinador.FechaCreacion = DateTime.Now;
										configuracionCoordinador.FechaModificacion = DateTime.Now;
										repConfiguracionCoordinador.Insert(configuracionCoordinador);
									}
								}
							}
							
							
						}
						scope.Complete();
					}
				}
				return true;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


		/// Autor: Jose Villena
		/// Fecha: 28/05/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene el coordinador de un determinado programa especifico mediante la configuracion ingresada en el modulo de configuracion coordinador
		/// </summary>
		/// <param name="idPEspecifico">Id del Programa Especifico</param>
		/// <returnsList<ConfiguracionCoordinadoraCentroCostoDTO></returns>
		public ConfiguracionCoordinadoraCentroCostoCantidadDTO ObtenerCoordinadorAsignacion(int idPEspecifico,int? idEstadoMatricula,int? idSubEstadoMatricula,int idMatriculaCabecera)
		{
			try
			{
				//Si se matriculo correctamente se hace la asignacion de coordinadora
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio repConfiguracion = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio();
				MatriculaCabeceraRepositorio repMatriculaCabecera = new MatriculaCabeceraRepositorio();
				List<ConfiguracionCoordinadoraCentroCostoCantidadDTO> coordinadorCantidad = new List<ConfiguracionCoordinadoraCentroCostoCantidadDTO>();

				//validar los casos y saber los de seguimiento academico
				//1:REGULAR , 6:REINCORPORADO , 11:ABANDONO REINCORPORADO
				if (idEstadoMatricula ==1 || idEstadoMatricula == 6 || idEstadoMatricula == 11 )
				{
					var subestado = repConfiguracion.ObtenerSubEstadoPorIdMatricula(idMatriculaCabecera);
					if(subestado.Count > 0)
					{
						idSubEstadoMatricula = subestado.FirstOrDefault().IdSubEstado;
					}
				}

				var coordinadores = repConfiguracion.ObtenerConfiguracionPorPespecifico(idPEspecifico);


				var coordinadoresEstadoSubEstado = coordinadores.Where(w => w.IdEstadoMatricula == idEstadoMatricula && w.IdSubEstadoMatricula == idSubEstadoMatricula).ToList();
				if(coordinadoresEstadoSubEstado.Count > 0)
				{
					coordinadores = coordinadoresEstadoSubEstado;
				}


				if (coordinadores.Count > 0)
				{
					foreach (var item in coordinadores)
					{
						ConfiguracionCoordinadoraCentroCostoCantidadDTO dto = new ConfiguracionCoordinadoraCentroCostoCantidadDTO();
						dto.IdPersonal = item.IdPersonal;
						dto.UsuarioPersonal = item.UsuarioPersonal;
						dto.IdPespecifico = idPEspecifico;
						dto.Cantidad = repMatriculaCabecera.GetBy(x => x.IdPespecifico == idPEspecifico && x.UsuarioCoordinadorAcademico.Equals(item.UsuarioPersonal)).ToList().Count;
						coordinadorCantidad.Add(dto);
					}
					var coordinador = coordinadorCantidad.OrderBy(x => x.Cantidad).FirstOrDefault();
					if (coordinador.UsuarioPersonal.Equals("esanchez1"))
					{
						coordinador.UsuarioPersonal = "esanchez";
					}
					return coordinador;
				}
				else
				{
					throw new Exception("No existe configuracion de coordinador que cumpla con los criterios");
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
