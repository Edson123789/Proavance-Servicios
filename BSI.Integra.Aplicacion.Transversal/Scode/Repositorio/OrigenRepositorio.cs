using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: OrigenRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Origen
    /// </summary>
    public class OrigenRepositorio : BaseRepository<TOrigen, OrigenBO>
    {
        #region Metodos Base
        public OrigenRepositorio() : base()
        {
        }
        public OrigenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OrigenBO> GetBy(Expression<Func<TOrigen, bool>> filter)
        {
            IEnumerable<TOrigen> listado = base.GetBy(filter);
            List<OrigenBO> listadoBO = new List<OrigenBO>();
            foreach (var itemEntidad in listado)
            {
                OrigenBO objetoBO = Mapper.Map<TOrigen, OrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OrigenBO FirstById(int id)
        {
            try
            {
                TOrigen entidad = base.FirstById(id);
                OrigenBO objetoBO = new OrigenBO();
                Mapper.Map<TOrigen, OrigenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OrigenBO FirstBy(Expression<Func<TOrigen, bool>> filter)
        {
            try
            {
                TOrigen entidad = base.FirstBy(filter);
                OrigenBO objetoBO = Mapper.Map<TOrigen, OrigenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OrigenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOrigen entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<OrigenBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(OrigenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOrigen entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<OrigenBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TOrigen entidad, OrigenBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TOrigen MapeoEntidad(OrigenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOrigen entidad = new TOrigen();
                entidad = Mapper.Map<OrigenBO, TOrigen>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public IEnumerable<OrigenBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TOrigen, bool>>> filters, Expression<Func<TOrigen, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TOrigen> listado = base.GetFiltered(filters, orderBy, ascending);
			List<OrigenBO> listadoBO = new List<OrigenBO>();

			foreach (var itemEntidad in listado)
			{
				OrigenBO objetoBO = Mapper.Map<TOrigen, OrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Retorna una lista de los cargos para ser usados en filtros 
		/// </summary>
		/// <returns>Id, Nombre</returns>
		public List<OrigenFiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<OrigenFiltroDTO> origenes = new List<OrigenFiltroDTO>();
                var _query = "SELECT Id,Nombre FROM mkt.V_TOrigen_ObtenerIdNombre WHERE Estado = 1";
                var origenesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(origenesDB) && !origenesDB.Contains("[]"))
                {
                    origenes = JsonConvert.DeserializeObject<List<OrigenFiltroDTO>>(origenesDB);
                }
                return origenes;
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
		/// Retorna una lista de los Origenes para ser usados en los filtros de comercial "RegistrarOportunidad"
		/// </summary>
		/// <returns>Id, Nombre</returns>
		public List<CategoriaOrigenFiltroDTO> ObtenerTodoFiltroParaAsesores()
        {
            try
            {
                List<CategoriaOrigenFiltroDTO> origenes = new List<CategoriaOrigenFiltroDTO>();
                var _query = "SELECT Id,Nombre FROM mkt.V_TOrigen_ObtenerIdNombre WHERE Nombre in('Referido','Visita Oficina','Llamada Telefonica','Correo Electronico','In House')";
                var origenesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(origenesDB) && !origenesDB.Contains("[]"))
                {
                    origenes = JsonConvert.DeserializeObject<List<CategoriaOrigenFiltroDTO>>(origenesDB);
                }
                return origenes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
		/// Retorna una lista de los Origenes para ser usados en los filtros  "RegistrarOportunidad"
		/// </summary>
		/// <returns>Id, Nombre</returns>
		public List<CategoriaOrigenFiltroDTO> ObtenerOrigeneParaRegistrarOportunidad(string Area)
        {
            if (Area.ToUpper() == "MKT" || Area.ToUpper() == "MK")
            {
                List<CategoriaOrigenFiltroDTO> origenes = new List<CategoriaOrigenFiltroDTO>();
                var _query = "SELECT Id,Nombre FROM mkt.V_TOrigen_ObtenerIdNombre WHERE Estado=1";
                var origenesDB = _dapper.QueryDapper(_query, null);
                origenes = JsonConvert.DeserializeObject<List<CategoriaOrigenFiltroDTO>>(origenesDB);
                return origenes;
            }
            else
            {
                List<CategoriaOrigenFiltroDTO> origenes = new List<CategoriaOrigenFiltroDTO>();
                var _query = "SELECT Id,Nombre FROM mkt.V_TOrigen_ObtenerIdNombre WHERE Nombre in('Referido','Visita Oficina','Llamada Telefonica','Correo Electronico','In House','WhatsApp chat')";
                var origenesDB = _dapper.QueryDapper(_query, null);
                origenes = JsonConvert.DeserializeObject<List<CategoriaOrigenFiltroDTO>>(origenesDB);
                return origenes;
            }
            
        }

        /// <summary>
        /// Obtiene el id, nombre de un origen filtrado por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<OrigenFiltroDTO> ObtenerOrigenChat(string nombre)
        {
            try
            {
                List<OrigenFiltroDTO> origenes = new List<OrigenFiltroDTO>();
                var _query = "SELECT Id,Nombre FROM mkt.V_TOrigen_ObtenerIdNombre Where Estado = 1 AND Nombre = @nombre";
                var OrigenDB = _dapper.QueryDapper(_query, new { nombre });
                if (!string.IsNullOrEmpty(OrigenDB) && !OrigenDB.Contains("[]"))
                {
                    origenes = JsonConvert.DeserializeObject<List<OrigenFiltroDTO>>(OrigenDB);
                }
                return origenes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);;
            }
        }

        /// <summary>
        /// Obte
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrigenFiltroDTO ObtenerOrigenPorId(int id)
        {
            try
            {
                var _query = "SELECT Id,Nombre FROM mkt.V_TOrigen_ObtenerIdNombre Where Estado = 1 AND Id = @Id";
                var OrigenDB = _dapper.QueryDapper(_query, new { Id = id });
                return JsonConvert.DeserializeObject<OrigenFiltroDTO>(OrigenDB);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        /// <summary>
        /// Obtener Origenes Por CategoriaOrigen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<OrigenFiltroDTO> ObtenerOrigenPorCategoriaOrigen(int idCategoriaOrigenInbox, int idCategoriaOrigenCorreo, int idCategoriaOrigenComentarios)
        {
            try
            {
                return GetBy(x => x.IdCategoriaOrigen == idCategoriaOrigenInbox || x.IdCategoriaOrigen == idCategoriaOrigenCorreo || x.IdCategoriaOrigen == idCategoriaOrigenComentarios, y =>  new OrigenFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre
                }).ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene la lista de los origenes y los nombres de sus categoria origen
        /// </summary>
        /// <returns>Lista de objetos de clase OrigenIdCategoriaOrigenDTO</returns>
        public List<OrigenesCategoriaOrigenDTO> ObtenerOrigenesCategoriasOrigen()
        {
            try
            {
                var query = "SELECT Id,Nombre,NombreCategoria FROM mkt.V_ObtenerOrigenesCategoriaOrigen";
                var OrigenDB = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<OrigenesCategoriaOrigenDTO>>(OrigenDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el id de categoria origen por idorigen
        /// </summary>
        /// <param name="idOrigen">Id del origen (PK de la tabla mkt.T_Origen)</param>
        /// <returns>Objeto de clase OrigenIdCategoriaOrigenDTO</returns>
        public OrigenIdCategoriaOrigenDTO ObtenerIdCategoriaOrigenPorOrigen(int idOrigen) {
            try
            {
                OrigenIdCategoriaOrigenDTO idCategoriaOrigen = new OrigenIdCategoriaOrigenDTO()
                {
                    Id = 0,
                    IdCategoriaOrigen = 0
                };
                var query = "SELECT Id, IdCategoriaOrigen FROM mkt.V_TOrigen_ObtenerCategoriaOrigen WHERE Id = @idOrigen AND Estado = 1";
                var idCategoriaorigenDB = _dapper.FirstOrDefault(query, new { idOrigen });
                if (!idCategoriaorigenDB.Contains("[]") || !idCategoriaorigenDB.Contains("null") || !idCategoriaorigenDB.Contains(""))
                {
                    idCategoriaOrigen =  JsonConvert.DeserializeObject<OrigenIdCategoriaOrigenDTO>(idCategoriaorigenDB);
                }
                return idCategoriaOrigen;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        ///  Obtiene la lista de Origenes (activos) registrados en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <returns>Lista de objetos de clase OrigenDTO</returns>
        public List<OrigenDTO> ListarOrigenes(FiltroPaginadorDTO filtro)
		{
			try
			{
				List<OrigenDTO> items = new List<OrigenDTO>();
				List<Expression<Func<TOrigen, bool>>> filters = new List<Expression<Func<TOrigen, bool>>>();
				var total = 0;
				List<OrigenBO> lista = new List<OrigenBO>();
				if (filtro != null && filtro.Take != 0)
				{
					if ((filtro.FiltroKendo != null && filtro.FiltroKendo.Filters.Count > 0))
					{
						// Creamos la Lista de filtros
						foreach (var filterGrid in filtro.FiltroKendo.Filters)
						{
							switch (filterGrid.Field)
							{
								case "Nombre":
									filters.Add(o => o.Nombre.Contains(filterGrid.Value));
									break;
								case "IdTipoDato":
									filters.Add(o => o.IdTipodato.ToString().Contains(filterGrid.Value));
									break;
								case "Descripcion":
									filters.Add(o => o.Descripcion.Contains(filterGrid.Value));
									break;
								case "Prioridad":
									filters.Add(o => o.Prioridad.ToString().Contains(filterGrid.Value));
									break;
								default:
									filters.Add(o => true);
									break;
							}
						}
					}

					lista = GetFiltered(filters, p => p.Id, false).ToList();
					total = lista.Count();
					lista = lista.Skip(filtro.Skip).Take(filtro.Take).ToList();
					
				}
				else
				{
					lista = GetBy(o => true).OrderByDescending(x => x.Id).ToList();
					total = lista.Count();
				}
				items = lista.Select(x => new OrigenDTO
				{
					Id = x.Id,
					IdCategoriaOrigen = x.IdCategoriaOrigen,
					IdTipoDato = x.IdTipodato,
					Prioridad = x.Prioridad,
					Nombre = x.Nombre,
					Descripcion = x.Descripcion,
					Total = total
				}).ToList();

				return items;
			}
			
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


        public List<TarifarioDTO> ObtenerTarifarios()
        {
            try
            {
                var data = new List<TarifarioDTO>();
                var _query = "SELECT Id,Nombre,FechaInicio,FechaFin,VisiblePortalWeb,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,Estado  FROM mkt.t_tarifario WHERE Estado = 1";
                var respuesta = _dapper.QueryDapper(_query,null);
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data =  JsonConvert.DeserializeObject<List<TarifarioDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<TarifarioDetalleConfiguracionDTO> ObtenerTarifariosDetalles(int idTarifario)
        {
            try
            {
                var data = new List<TarifarioDetalleConfiguracionDTO>();
                var _query = "SELECT Id,IdTarifario,Concepto,Descripcion,Monto,IdPais,NombrePais,IdMoneda,NombrePlural,Simbolo,TipoCantidad,Estados,SubEstados FROM [mkt].[V_ObtenerTarifarioDetalle] WHERE Estado = 1  and IdTarifario=@idTarifario";
                var respuesta = _dapper.QueryDapper(_query, new { idTarifario });
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<TarifarioDetalleConfiguracionDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TarifarioDetalleConfiguracionDTO> ObtenerTarifariosDetallesPais(int idTarifario)
        {
            try
            {
                var data = new List<TarifarioDetalleConfiguracionDTO>();
                var _query = "SELECT Id,IdTarifario,Concepto,Descripcion,Monto,IdPais,NombrePais,IdMoneda,NombrePlural,Simbolo,TipoCantidad,Estados,SubEstados FROM [mkt].[V_ObtenerTarifarioDetalle] WHERE Estado = 1  and IdTarifario=@idTarifario";
                var respuesta = _dapper.QueryDapper(_query, new { idTarifario });
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<TarifarioDetalleConfiguracionDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: OrigenRepositorio
        ///Autor: Lisbeth Ortogorin
        ///Fecha: 11/09/2021
        /// <summary>
        /// Obtener Tarifario Detalle para modulo Cronograma Pagos
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
        /// <returns> Lista Tarifario Detalle Agenda: List<TarifarioDetalleAgendaDTO></returns>
        public List<TarifarioDetalleMontoDTO> ObtenerTarifariosDetallesMonto(string nombre)
        {
            try
            {
                var data = new List<TarifarioDetalleMontoDTO>();
                var _query = "SELECT Id,IdTarifario,CONCAT(Concepto, ' - ', Monto, ' ', NombrePlural) as Detalle FROM [mkt].[V_ObtenerTarifarioDetalle] WHERE Concepto LIKE CONCAT('%',@nombre,'%') AND Estado = 1";
                var respuesta = _dapper.QueryDapper(_query, new { nombre });
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<TarifarioDetalleMontoDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: OrigenRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener Tarifario Detalle de Agenda
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
        /// <returns> Lista Tarifario Detalle Agenda: List<TarifarioDetalleAgendaDTO></returns> 
        public List<TarifarioDetalleAgendaDTO> ObtenerTarifariosDetallesAgenda(int idMatriculaCabecera)
        {
            try
            {
                var data = new List<TarifarioDetalleAgendaDTO>();
                var query = 
                    " select TARD.Id,TARD.IdTarifario,TARD.Concepto,TARD.Descripcion,"+
                    " case "+
	                "     when TARD.TipoCantidad = '1' then concat('S/ ',TARD.MontoPeru)"+
	                "     else concat(TARD.MontoPeru,' %')"+
                    " end as MontoPeru,"+
                    " case"+
	                "     when TARD.TipoCantidad = '1' then concat('COP ',TARD.MontoColombia)"+
	                "     else concat(TARD.MontoColombia,' %')"+
                    " end as MontoColombia,"+
                    " case"+
	                "     when TARD.TipoCantidad = '1' then concat('Bs ',TARD.MontoBolivia)"+
	                "     else concat(TARD.MontoBolivia,' %')"+
                    " end as MontoBolivia,"+
                    " case" +
                    "     when TARD.TipoCantidad = '1' then concat('MXN  ',TARD.MontoMexico)" +
                    "     else concat(TARD.MontoMexico,' %')" +
                    " end as MontoMexico," +
                    " case" +
	                "     when TARD.TipoCantidad = '1' then concat('$ ',TARD.MontoExtranjero)"+
	                "     else concat(TARD.MontoExtranjero,' %')"+
                    " end as MontoExtranjero"+
                    " from ope.T_OportunidadClasificacionOperaciones OPE inner join"+
                    " mkt.t_tarifario TAR ON OPE.idtarifario= TAR.id inner join"+
                    " mkt.t_tarifariodetalle TARD on TAR.id=TARD.idtarifario"+
                    " where TAR.Estado=1 and TARD.Estado=1 and IdMatriculaCabecera=@idMatriculaCabecera order by Id asc";

                var respuesta = _dapper.QueryDapper(query, new { idMatriculaCabecera });
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<TarifarioDetalleAgendaDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TarifarioDTO> InsertarTarifario(TarifarioNuevoDTO objeto)
        {
            try
            {
                string _queryInsertar = "mkt.SP_InsertarTarifario";
                var queryInsert = _dapper.QuerySPDapper(_queryInsertar, new
                {
                    NombreTarifario = objeto.Nombre,
                    FechaInicioTarifario = objeto.FechaInicio,
                    FechaFinTarifario = objeto.FechaFin,
                    VisiblePortalWebTarifario = objeto.VisiblePortalWeb,
                    UsuarioTarifario = objeto.Usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDTO>>(queryInsert);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<TarifarioDTO> ActualizarTarifario(TarifarioNuevoDTO objeto)
        {
            try
            {
                string _queryInsertar = "mkt.SP_ActualizarTarifario";
                var queryInsert = _dapper.QuerySPDapper(_queryInsertar, new
                {
                    IdTarifario= objeto.Id,
                    NombreTarifario = objeto.Nombre,
                    FechaInicioTarifario = objeto.FechaInicio,
                    FechaFinTarifario = objeto.FechaFin,
                    VisiblePortalWebTarifario = objeto.VisiblePortalWeb,
                    UsuarioTarifario = objeto.Usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDTO>>(queryInsert);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<TarifarioDetalleDTO> InsertarTarifarioDetalles(TarifarioDetalleDTO objeto)
        {
            try
            {
                string _queryInsertar = "mkt.SP_InsertarTarifarioDetalles";
                var queryInsert = _dapper.QuerySPDapper(_queryInsertar, new
                {
                    IdTarifarioDetalle= objeto.IdTarifario,
                    ConceptoDetalle = objeto.Concepto,
                    DescripcionDetalle = objeto.Descripcion,
                    //MontoPeruDetalle = objeto.MontoPeru,
                    //MontoColombiaDetalle = objeto.MontoColombia,
                    //MontoBoliviaDetalle = objeto.MontoBolivia,
                    //MontoExtranjeroDetalle = objeto.MontoExtranjero,
                    TipoCantidadDetalle = objeto.TipoCantidad,
                    EstadosDetalle = objeto.Estados,
                    SubEstadosDetalle = objeto.SubEstados,
                    UsuarioDetalle = objeto.Usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDetalleDTO>>(queryInsert);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<TarifarioDetalleDTO> ActualizarTarifarioDetalles(TarifarioDetalleDTO objeto)
        {
            try
            {
                string _queryInsertar = "mkt.SP_ActualizarTarifarioDetalles";
                var queryInsert = _dapper.QuerySPDapper(_queryInsertar, new
                {
                    IdDetalle = objeto.Id,
                    IdTarifarioDetalle =objeto.IdTarifario,
                    ConceptoDetalle = objeto.Concepto,
                    DescripcionDetalle = objeto.Descripcion,
                    //MontoPeruDetalle = objeto.MontoPeru,
                    //MontoColombiaDetalle = objeto.MontoColombia,
                    //MontoBoliviaDetalle = objeto.MontoBolivia,
                    //MontoExtranjeroDetalle = objeto.MontoExtranjero,
                    TipoCantidadDetalle = objeto.TipoCantidad,
                    EstadosDetalle = objeto.Estados,
                    SubEstadosDetalle = objeto.SubEstados,
                    UsuarioDetalle = objeto.Usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDetalleDTO>>(queryInsert);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<TarifarioDTO> EliminarTarifario(int IdTarifario,string Usuario)
        {
            try
            {
                string _queryInsertar = "mkt.SP_EliminarTarifario";
                var queryInsert = _dapper.QuerySPDapper(_queryInsertar, new
                {
                    IdTarifario = IdTarifario,
                    UsuarioTarifario = Usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDTO>>(queryInsert);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TarifarioDetalleDTO> EliminarTarifarioDetalle(string Concepto, string Usuario)
        {
            try
            {
                string _queryEliminar = "mkt.SP_EliminarTarifarioDetalle";
                var queryEliminar = _dapper.QuerySPDapper(_queryEliminar, new
                {
                    Concepto = Concepto,
                    UsuarioTarifario = Usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDetalleDTO>>(queryEliminar);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TarifarioDetalleDTO> EliminarTarifarioDetallePais(int Id, string Usuario)
        {
            try
            {
                string _queryEliminar = "mkt.SP_EliminarTarifarioDetallePais";
                var queryEliminar = _dapper.QuerySPDapper(_queryEliminar, new
                {
                    Id = Id,
                    UsuarioTarifario = Usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDetalleDTO>>(queryEliminar);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<LogAccesosDTO> InsertarLogAccesos(string Personal,string Modulo,string Ip)
        {
            try
            {
                string _queryInsertar = "conf.SP_InsertarLogAccesos";
                var queryInsert = _dapper.QuerySPDapper(_queryInsertar, new
                {
                    PersonalLog = Personal,
                    ModuloLog = Modulo,
                    IpCliente= Ip
                });
                return JsonConvert.DeserializeObject<List<LogAccesosDTO>>(queryInsert);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Devuelve la lista de Orgien Personalizado 
        //Correo, Formulario, Llamada, Whatsapp,Facebook,Instagram,Twitter,LinkedIn
        public List<ListaOrigenReclamoDTO> ObtenerCombosOrigen()
        {
            try
            {
                var data = new List<ListaOrigenReclamoDTO>();
                var _query = "SELECT Id,Nombre FROM mkt.V_OrigenFiltroReclamo";
                var respuesta = _dapper.QueryDapper(_query,new { });
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<ListaOrigenReclamoDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<TasasAcademicasDetalleDTO> AgregarTasasAcademicasProcedimiento(string CodigoMatricula, int IdConcepto, float Monto, string Moneda, string Usuario, DateTime Fecha)
        {
            try
            {
                string _queryInsertar = "fin.SP_AgregarTasasAcademicas";
                var queryInsert = _dapper.QuerySPDapper(_queryInsertar, new
                {
                    CodigoMatricula = CodigoMatricula,
                    IdConcepto = IdConcepto,
                    Monto = Monto,
                    Moneda = Moneda,
                    Usuario = Usuario,
                    Fecha = Fecha,
                });
                return JsonConvert.DeserializeObject<List<TasasAcademicasDetalleDTO>>(queryInsert);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
