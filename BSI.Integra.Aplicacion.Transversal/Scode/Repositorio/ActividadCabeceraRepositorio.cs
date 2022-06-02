
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ActividadCabeceraRepositorio : BaseRepository<TActividadCabecera, ActividadCabeceraBO>
    {
        #region Metodos Base
        public ActividadCabeceraRepositorio() : base()
        {
        }
        public ActividadCabeceraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ActividadCabeceraBO> GetBy(Expression<Func<TActividadCabecera, bool>> filter)
        {
            IEnumerable<TActividadCabecera> listado = base.GetBy(filter);
            List<ActividadCabeceraBO> listadoBO = new List<ActividadCabeceraBO>();
            foreach (var itemEntidad in listado)
            {
                ActividadCabeceraBO objetoBO = Mapper.Map<TActividadCabecera, ActividadCabeceraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ActividadCabeceraBO FirstById(int id)
        {
            try
            {
                TActividadCabecera entidad = base.FirstById(id);
                ActividadCabeceraBO objetoBO = new ActividadCabeceraBO();
                Mapper.Map<TActividadCabecera, ActividadCabeceraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ActividadCabeceraBO FirstBy(Expression<Func<TActividadCabecera, bool>> filter)
        {
            try
            {
                TActividadCabecera entidad = base.FirstBy(filter);
                ActividadCabeceraBO objetoBO = Mapper.Map<TActividadCabecera, ActividadCabeceraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ActividadCabeceraBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TActividadCabecera entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ActividadCabeceraBO> listadoBO)
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

        public bool Update(ActividadCabeceraBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TActividadCabecera entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ActividadCabeceraBO> listadoBO)
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
        private void AsignacionId(TActividadCabecera entidad, ActividadCabeceraBO objetoBO)
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

        private TActividadCabecera MapeoEntidad(ActividadCabeceraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TActividadCabecera entidad = new TActividadCabecera();
                entidad = Mapper.Map<ActividadCabeceraBO, TActividadCabecera>(objetoBO,
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
        /// Autor: Jashin Salazar
        /// Fecha: 19/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de las ActividadCabecera para ser usada en un combobox
        /// </summary>
        /// <returns>id, nombre</returns>
        public List<ActividadCabeceraFiltroDTO> ObtenerTodoFiltro() {
            try
            {
				//List<ActividadCabeceraFiltroDTO> ActividadCabeceraFiltro = new List<ActividadCabeceraFiltroDTO>();
				//var ActividadCabeceraDB = GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre });
				//foreach (var item in ActividadCabeceraDB)
				//{
				//    var ActividadCabeceraTemp = new ActividadCabeceraFiltroDTO() {
				//        Id = item.Id,
				//        Nombre = item.Nombre
				//    };
				//    ActividadCabeceraFiltro.Add(ActividadCabeceraTemp);
				//}
				//return ActividadCabeceraFiltro;
				var query = "SELECT Id, Nombre, IdPersonalAreaTrabajo, PersonalAreaTrabajo FROM [com].[V_ObtenerActividadesCabecera_Agenda] WHERE Estado = 1";
				var dapper = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<ActividadCabeceraFiltroDTO>>(dapper);

			}
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// Autor: Jashin Salazar
        /// Fecha: 28/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de las ActividadCabecera para ser usada en un combobox
        /// </summary>
        /// <returns>id, nombre</returns>
        public List<ActividadCabeceraFiltroDTO> ObtenerTodoFiltroAlterno()
        {
            try
            {
                var query = "SELECT Id, Nombre, IdPersonalAreaTrabajo, PersonalAreaTrabajo FROM [com].[V_ObtenerActividadesCabecera_Agenda] WHERE Estado = 1 AND EsEnvioMasivo=0 ";
                var dapper = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<ActividadCabeceraFiltroDTO>>(dapper);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Autor: Jashin Salazar
        /// Fecha: 19/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene sus datos (para visualizacion en grilla de su propio CRUD).
        /// </summary>
        /// <returns>Id, Nombre</returns>
        public List<ActividadCabeceraDTO> ObtenerAllActividadCabecera()
        {
            try
            {
                List<ActividadCabeceraDTO> ACabecera = new List<ActividadCabeceraDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion, FechaCreacion2, DuracionEstimada, ReproManual, ReproAutomatica, IdPlantilla, IdActividadBase, FechaModificacion2, ValidaLlamada, IdPlantillaSpeech, NumeroMaximoLlamadas" +
                    " ,IdConjuntoLista,IdFrecuencia,FechaInicioActividad,DiaFrecuenciaMensual,EsRepetitivo,HoraInicio,HoraFin,CantidadIntevaloTiempo,IdTiempoIntervalo,Activo,FechaFinActividad, IdPersonalAreaTrabajo, PersonalAreaTrabajo, IdFacebookCuentaPublicitaria FROM [com].[V_ObtenerActividadesCabecera_Agenda] WHERE Estado = 1 AND EsEnvioMasivo=0 Order by FechaCreacion desc";
                var ACabeceraDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ACabeceraDB) && !ACabeceraDB.Contains("[]"))
                {
                    ACabecera = JsonConvert.DeserializeObject<List<ActividadCabeceraDTO>>(ACabeceraDB);
                }
                return ACabecera;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 18/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las activdades automaticas.
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        public List<ActividadCabeceraDTO> ObtenerTodoActividadAutomatica()
        {
            try
            {
                List<ActividadCabeceraDTO> ACabecera = new List<ActividadCabeceraDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion, FechaCreacion2, DuracionEstimada, ReproManual, ReproAutomatica, IdPlantilla, IdActividadBase, FechaModificacion2, ValidaLlamada, IdPlantillaSpeech, NumeroMaximoLlamadas" +
                    " ,IdConjuntoLista,IdFrecuencia,FechaInicioActividad,DiaFrecuenciaMensual,EsRepetitivo,HoraInicio,HoraFin,CantidadIntevaloTiempo,IdTiempoIntervalo,Activo,FechaFinActividad, IdPersonalAreaTrabajo, PersonalAreaTrabajo, IdFacebookCuentaPublicitaria FROM [com].[V_ObtenerActividadesCabecera_Agenda] WHERE Estado = 1 AND EsEnvioMasivo=1 Order by FechaCreacion desc";
                var ACabeceraDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ACabeceraDB) && !ACabeceraDB.Contains("[]"))
                {
                    ACabecera = JsonConvert.DeserializeObject<List<ActividadCabeceraDTO>>(ACabeceraDB);
                }
                return ACabecera;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene Las Actividades a Ejecutar.
        /// </summary>
        /// <returns></returns>
        public List<ActividadParaEjecutarDTO> ObtenerActividadParaEjecutar()
        {
            try
            {
                DateTime HoraActual = DateTime.Now;
                string FechaInicioActividad = HoraActual.ToString("dd/MM/yyyy");
                string minutoActual;
                if (HoraActual.Minute.ToString().Length == 1)
                {
                    minutoActual = "0" + HoraActual.Minute;
                }
                else 
                {
                    minutoActual = HoraActual.Minute.ToString();
                }
                var HoraInicio = HoraActual.Hour + ":" + minutoActual + ":00";

                List<ActividadParaEjecutarDTO> ACabecera = new List<ActividadParaEjecutarDTO>();
                var _query = string.Empty;
                //_query = "SELECT Id,IdConjuntoLista,HoraFin,HoraInicio,DiafrecuenciaMensual,IdFrecuencia,CantidadIntevaloTiempo,IdTiempoIntervalo,Activo AS ActivoEjecutarFiltro,ActividadBase From com.V_ActividadCabcera_ParaEjecutar WHERE Estado = 1 and @FechaInicioActividad>=FechaInicioActividad and @FechaInicioActividad<= FechaFinActividad and HoraInicio=@HoraInicio";
                _query = "com.SP_ActividadCabecera_ParaEjecutar";
                var ACabeceraDB = _dapper.QuerySPDapper(_query, new { FechaInicioActividad, HoraInicio });
                if (!string.IsNullOrEmpty(ACabeceraDB) && !ACabeceraDB.Contains("[]"))
                {
                    ACabecera = JsonConvert.DeserializeObject<List<ActividadParaEjecutarDTO>>(ACabeceraDB);
                }
                return ACabecera;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }   

        /// <summary>
        /// Obtiene las actividades de tipo mailing configuradas para el dia
        /// </summary>
        /// <returns></returns>
        public List<ActividadParaEjecutarDTO> ObtenerActividadesMailingConfiguradasHoy()
        {
            try
            {
                DateTime HoraActual = DateTime.Now;
                string FechaInicioActividad = HoraActual.ToString("dd/MM/yyyy");

                var listaActividadCabecera = new List<ActividadParaEjecutarDTO>();
                var _query = "com.SP_ActividadCabeceraMailingMasivoOperaciones_ParaEjecutar";
                var listaActividadCabeceraDB = _dapper.QuerySPDapper(_query, new { FechaInicioActividad });
                if (!string.IsNullOrEmpty(listaActividadCabeceraDB) && !listaActividadCabeceraDB.Contains("[]"))
                {
                    listaActividadCabecera = JsonConvert.DeserializeObject<List<ActividadParaEjecutarDTO>>(listaActividadCabeceraDB);
                }
                return listaActividadCabecera;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene las actividades cabecera para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
		/// Actualiza en los resultados de Conjunto Lista la oportunidad sujeta a una determinada configuracion
		/// </summary>
        /// <param name="idConjuntoListaDetalle"></param>
		/// <returns></returns>
        public bool ActualizarOportunidadConjuntoListaResultado(int idConjuntoListaDetalle)
		{
			try
			{
				var resultado = new Dictionary<string, bool>();

				string query = _dapper.QuerySPFirstOrDefault("[mkt].[SP_ActualizarOportunidadActividadCabecera]", new { IdConjuntoListaDetalle = idConjuntoListaDetalle });
				if (!string.IsNullOrEmpty(query))
				{
					resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
				}
				return resultado.Select(x => x.Value).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
    }
}
