using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Persistencia.SCode.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: FaseOportunidadRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Probabilidad de Fases de Oportunidad
    /// </summary>
    public class FaseOportunidadRepositorio : BaseRepository<TFaseOportunidad, FaseOportunidadBO>
    {
        #region Metodos Base
        public FaseOportunidadRepositorio() : base()
        {
        }
        public FaseOportunidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FaseOportunidadBO> GetBy(Expression<Func<TFaseOportunidad, bool>> filter)
        {
            IEnumerable<TFaseOportunidad> listado = base.GetBy(filter).ToList();
            List<FaseOportunidadBO> listadoBO = new List<FaseOportunidadBO>();
            foreach (var itemEntidad in listado)
            {
                FaseOportunidadBO objetoBO = Mapper.Map<TFaseOportunidad, FaseOportunidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FaseOportunidadBO FirstById(int id)
        {
            try
            {
                TFaseOportunidad entidad = base.FirstById(id);
                FaseOportunidadBO objetoBO = Mapper.Map<TFaseOportunidad, FaseOportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FaseOportunidadBO FirstBy(Expression<Func<TFaseOportunidad, bool>> filter)
        {
            try
            {
                TFaseOportunidad entidad = base.FirstBy(filter);
                FaseOportunidadBO objetoBO = Mapper.Map<TFaseOportunidad, FaseOportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FaseOportunidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFaseOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FaseOportunidadBO> listadoBO)
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

        public bool Update(FaseOportunidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFaseOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FaseOportunidadBO> listadoBO)
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
        private void AsignacionId(TFaseOportunidad entidad, FaseOportunidadBO objetoBO)
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

        private TFaseOportunidad MapeoEntidad(FaseOportunidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFaseOportunidad entidad = new TFaseOportunidad();
                entidad = Mapper.Map<FaseOportunidadBO, TFaseOportunidad>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// Repositorio: FaseOportunidadRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Retorna el Id de la Fase Maxima segun dos fases entregadas
        /// </summary>
        /// <param name="faseUno"> Fase Uno </param>
        /// <param name="faseDos"> Fase Dos </param>
        /// <returns> Retorna el Id de la Fase Maxima segun dos fases entregadas : int </returns>
        public int GetFaseMaxima(int faseUno, int faseDos)
        {
            try
            {
				var maximoUno = this.FirstById(faseUno);
				var maximoDos = this.FirstById(faseDos);

                var resultadoMaximoUno = maximoUno != null ? maximoUno.Id : 0;
                var resultadoMaximoDos = maximoDos != null ? maximoDos.Id : 0;

                return resultadoMaximoUno < resultadoMaximoDos ? faseDos : faseUno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Retorna si la Fase como parametro es una Fase IS
        /// </summary>
        /// <param name="idFase"></param>
        /// <returns></returns>
        public bool ValidarFaseIS(int idFase)
        {
            try
            {

                string _queryFase = "Select Codigo From pla.T_FaseOportunidad Where Estado=1 and Id=@IdFase";
                var queryFase = _dapper.FirstOrDefault(_queryFase, new { IdFase = idFase });
                var Codigo = JsonConvert.DeserializeObject<Dictionary<string, string>>(queryFase);
                return Codigo.Any(x => x.Value == "IS");
                
            }
            catch (Exception e)
            {
                 throw new Exception(e.Message);
            }
        }
        public bool ValidarFaseCierreOportunidad(int idFase)
        {
            try
            {
                string _queryFase = "select Id, Nombre from pla.T_FaseOportunidad where Codigo in ('D','RN4','NI','IS','RN3','RN2') and Estado = 1";
                string queryFase = _dapper.QueryDapper(_queryFase,null);
                var idFasesCierre = JsonConvert.DeserializeObject<List<FasesCierreDTO>>(queryFase);
                return idFasesCierre.Any(x => x.Id == idFase) ;
           
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el id de FaseOportunidad filtrado por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        private int ObtenerIdPorNombre(string nombre)
        {
            try
            {
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la fase oportunidad de ip
        /// </summary>
        /// <returns></returns>
        public FaseOportunidadFiltroDTO ObtenerIdFaseOportunidadIP()
        {
            try
            {
                FaseOportunidadFiltroDTO ocurrencia = new FaseOportunidadFiltroDTO();
                ocurrencia.Id = this.ObtenerIdPorNombre("IP");
                return ocurrencia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public FaseOportunidadFiltroDTO ObtenerIdFaseOportunidadBNC()
        {
            try
            {
                FaseOportunidadFiltroDTO ocurrencia = new FaseOportunidadFiltroDTO();
                ocurrencia.Id = this.ObtenerIdPorNombre("BNC");
                return ocurrencia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        ///  Obtiene Lista de Fases de oportunidad para filtros de formularios
        /// </summary>
        /// <param></param>
        /// <returns>Id, Codigo</returns>
        public List<FaseOportunidadFiltroDTO> ObtenerTodoFiltro() {
            try
            {
                return GetBy(x=> x.Estado == true, x => new FaseOportunidadFiltroDTO { Id = x.Id, Codigo = x.Codigo}).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Lista de Fases de oportunidad para filtros de fromularios
        /// </summary>
        /// <returns>Lista de objetos de clase FaseOportunidadFiltroDTO</returns>
        public List<FaseOportunidadFiltroDTO> ObtenerFaseOportunidadTodoFiltro()
        {
            try
            {
                List<FaseOportunidadFiltroDTO> fasesOportunidad = new List<FaseOportunidadFiltroDTO>();
                var query = "SELECT Id, Nombre, Codigo FROM pla.V_TFaseOportunidad_ParaFiltro WHERE estado = 1";
                var fasesOportunidadDB = _dapper.QueryDapper(query, new { });
                fasesOportunidad = JsonConvert.DeserializeObject<List<FaseOportunidadFiltroDTO>>(fasesOportunidadDB);
                return fasesOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <summary>
		/// Obtiene la fase oportunidad mediante Id interaccionChat
		/// </summary>
		/// <param name="idInteraccionChat"></param>
		/// <returns></returns>
		public FaseOportunidadInteraccionDTO ObtenerFaseOportunidadPorInteraccionId(int idInteraccionChat)
		{
			try
			{
				string query = "SELECT Id, IdFaseOportunidadPortal FROM com.V_ObtenerFaseOportunidadPorInteraccionChatId Where Id = @idInteraccionChat";
				var _faseOportunidad = _dapper.FirstOrDefault(query, new { idInteraccionChat });
				return JsonConvert.DeserializeObject<FaseOportunidadInteraccionDTO>(_faseOportunidad);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Obtiene datos de la Oportunidad mediante IdFaseOportunidad
		/// </summary>
		/// <param name="idFaseOportunidadPortal"></param>
		/// <returns></returns>
		public OportunidadDatosChatDTO ObtenerOportunidadDatosChatPorIdFaseOportunidadPortal(string idFaseOportunidadPortal)
		{
			try
			{
				string query = "Select 	IdOportunidad,IdContacto,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Direccion,Telefono,Celular,Email1,Email2,IdCodigoPais,IdCiudad,IdCargo,IdAFormacion,IdATrabajo,IdIndustria,IdCentroCosto,IdTipoDato,IdFaseOportunidad,IdOrigen,IdEmpresa FROM com.V_ObtenerDatosPorIdFaseOportunidad WHERE Estado = 1 and IdFaseOportunidadPortal = @idFaseOportunidadPortal";
				var faseOportunidadChat = _dapper.FirstOrDefault(query, new { idFaseOportunidadPortal});
				return JsonConvert.DeserializeObject<OportunidadDatosChatDTO>(faseOportunidadChat);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Obtiene datos de la oportunidad mediante IdFaseOportunidad
		/// </summary>
		/// <param name="idFaseOportunidadPortal"></param>
		/// <returns></returns>
		public OportunidadDatosChatDTO ObtenerOportunidadDatosChatPorIdFaseOportunidadPortalAA(string idFaseOportunidadPortal)
		{
			try
			{
				string query = "Select 	IdOportunidad,Nombre1,Nombre2,ApellidoPaterno, NombreCentroCosto, "+
					"ApellidoMaterno,Direccion, Telefono,Celular,Email,Email2,IdCodigoPais,IdCiudad,IdCargo,IdAreaFormacion,IdAreaTrabajo,IdIndustria,"+
					"IdCentroCosto,IdTipoDato,IdFaseOportunidad,IdOrigen,IdEmpresa,Error FROM com.V_ObtenerDatosPorIdFaseOportunidad_AA WHERE Estado = 1 and IdFaseOportunidadPortal = @idFaseOportunidadPortal";

				var faseOportunidadChatAA = _dapper.FirstOrDefault(query,new { idFaseOportunidadPortal });
				return JsonConvert.DeserializeObject<OportunidadDatosChatDTO>(faseOportunidadChatAA);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		
		/// <summary>
		/// Obtiene los datos del programa mediante IdInteraccion chat
		/// </summary>
		/// <param name="idInteraccionChat"></param>
		/// <returns></returns>
		public DatosProgramaDTO ObtenerDatosPrograma(int idInteraccionChat)
		{
			try
			{
				string query = "SELECT LinkPrograma, NombrePrograma, Ubicacion, LinkPrevia, NombreProgramaPrevio, IdInteraccionChatIntegra FROM com.V_ObtenerDatosProgramaGeneralPorIdInteraccionChat Where Estado = 1 AND IdInteraccionChatIntegra = @idInteraccionChat";
				var datosPrograma = _dapper.FirstOrDefault(query, new { idInteraccionChat });
				return JsonConvert.DeserializeObject<DatosProgramaDTO>(datosPrograma);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        /// <summary>
		/// Obtiene todas las fases de oportunidad
		/// </summary>
		/// <returns></returns>
		public List<FaseOportunidadDTO> ObtenerFasesOportunidad()
        {
            try
            {
                var listaFases = GetBy(x => true, y => new FaseOportunidadDTO
                {
                    Id = y.Id,
                    Codigo = y.Codigo
                                       
                }).ToList();

                return listaFases;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
		/// Obtiene todas las fases de oportunidad solo el codigo para combobox
		/// </summary>
		/// <returns></returns>
		public List<FaseOportunidadFiltroCodigoDTO> ObtenerFasesOportunidadFiltroCodigo()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new FaseOportunidadFiltroCodigoDTO { Id = x.Id, Codigo = x.Codigo }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<FaseOportunidadDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new FaseOportunidadDTO
                {
                    Id = y.Id,
                    Codigo = y.Codigo,
                    Nombre = y.Nombre,
                    NroMinutos = y.NroMinutos,
                    IdActividad = y.IdActividad,
                    MaxNumDias = y.MaxNumDias,
                    MinNumDias = y.MinNumDias,
                    TasaConversionEsperada = y.TasaConversionEsperada,
                    Meta = y.Meta,
                    Final = y.Final,
                    ReporteMeta = y.ReporteMeta,
                    EnSeguimiento = y.EnSeguimiento,
                    EsCierre = y.EsCierre,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene las fases oportunidad con los campos necesarios para ser llenada en una grilla (para su propio CRUD)
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		public List<FaseOportunidadDTO> ObtenerTodasFaseOportunidad()
        {
            try
            {
                List<FaseOportunidadDTO> FOportunidades = new List<FaseOportunidadDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Codigo,Nombre, NroMinutos, IdActividad, MaxNumDias, MinNumDias, TasaConversionEsperada, Meta, Final, ReporteMeta, EnSeguimiento FROM pla.T_FaseOportunidad WHERE Estado = 1";
                var FOportunidadDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(FOportunidadDB) && !FOportunidadDB.Contains("[]"))
                {
                    FOportunidades = JsonConvert.DeserializeObject<List<FaseOportunidadDTO>>(FOportunidadDB);
                }
                return FOportunidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
