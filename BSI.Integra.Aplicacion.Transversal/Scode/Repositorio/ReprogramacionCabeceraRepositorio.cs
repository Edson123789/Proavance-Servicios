using System;
using System.Collections.Generic;
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
    public class ReprogramacionCabeceraRepositorio : BaseRepository<TReprogramacionCabecera, ReprogramacionCabeceraBO>
    {
        #region Metodos Base
        public ReprogramacionCabeceraRepositorio() : base()
        {
        }
        public ReprogramacionCabeceraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ReprogramacionCabeceraBO> GetBy(Expression<Func<TReprogramacionCabecera, bool>> filter)
        {
            IEnumerable<TReprogramacionCabecera> listado = base.GetBy(filter);
            List<ReprogramacionCabeceraBO> listadoBO = new List<ReprogramacionCabeceraBO>();
            foreach (var itemEntidad in listado)
            {
                ReprogramacionCabeceraBO objetoBO = Mapper.Map<TReprogramacionCabecera, ReprogramacionCabeceraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ReprogramacionCabeceraBO FirstById(int id)
        {
            try
            {
                TReprogramacionCabecera entidad = base.FirstById(id);
                ReprogramacionCabeceraBO objetoBO = new ReprogramacionCabeceraBO();
                Mapper.Map<TReprogramacionCabecera, ReprogramacionCabeceraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ReprogramacionCabeceraBO FirstBy(Expression<Func<TReprogramacionCabecera, bool>> filter)
        {
            try
            {
                TReprogramacionCabecera entidad = base.FirstBy(filter);
                ReprogramacionCabeceraBO objetoBO = Mapper.Map<TReprogramacionCabecera, ReprogramacionCabeceraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ReprogramacionCabeceraBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TReprogramacionCabecera entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ReprogramacionCabeceraBO> listadoBO)
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

        public bool Update(ReprogramacionCabeceraBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TReprogramacionCabecera entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ReprogramacionCabeceraBO> listadoBO)
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
        private void AsignacionId(TReprogramacionCabecera entidad, ReprogramacionCabeceraBO objetoBO)
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

        private TReprogramacionCabecera MapeoEntidad(ReprogramacionCabeceraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TReprogramacionCabecera entidad = new TReprogramacionCabecera();
                entidad = Mapper.Map<ReprogramacionCabeceraBO, TReprogramacionCabecera>(objetoBO,
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
       /// <summary>
       /// Obtiene el intervalo minimo y la maxima cantidad de programaaciones que se puede hacer por dia segun la categoria
       /// </summary>
       /// <param name="idActividadCabecera"></param>
       /// <param name="idCategoria"></param>
       /// <returns></returns>
        public ReprogramacionCabeceraRADTO ObtenerCantidadIntervaloYReprogramacionPorDiaPermitida(int idActividadCabecera, int idCategoria)
        {
            try
            {
                string _queryreprogramacionv2 = "Select IntervaloSigProgramacionMin, MaxReproPorDia from com.V_TReprogramacionCabecera_FechaProgramacionAutomatica Where IdActividadCabecera=@IdActividadCabecera and IdCategoriaOrigen=@IdCategoria";
                var queryreprogramacionv2 = _dapper.FirstOrDefault(_queryreprogramacionv2, new { IdActividadCabecera = idActividadCabecera, IdCategoria = idCategoria });
                ReprogramacionCabeceraRADTO reprogramacionPermitida = JsonConvert.DeserializeObject<ReprogramacionCabeceraRADTO>(queryreprogramacionv2);
                return reprogramacionPermitida;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene la catidad de Reprogramaciones realidas durante el dia del asesor por categoria
        /// </summary>
        /// <param name="idActividadCabecera"></param>
        /// <param name="idCategoria"></param>
        /// <returns></returns>
        public ReprogramacionCabeceraPersonalRADTO ObtenerCantidadReprogramacionDelDiaPorAsesor(int idActividadCabecera, int idCategoria, int idPersonal)
        {
            try
            {
                string _queryReprogramacionesV2 = "Select ReproDia from com.V_TReprogramacionCabeceraPersonal_FechaProgramacionAutomatica where IdActividadCabecera=@IdActividadCabecera and IdCategoriaOrigen=@IdCategoria and IdPersonal=@IdPersonal";
                var queryReprogramacionesV2 = _dapper.FirstOrDefault(_queryReprogramacionesV2, new { IdActividadCabecera = idActividadCabecera, IdCategoria = idCategoria, IdPersonal = idPersonal });
                ReprogramacionCabeceraPersonalRADTO reprogramacionAsesor = JsonConvert.DeserializeObject<ReprogramacionCabeceraPersonalRADTO>(queryReprogramacionesV2);
                return reprogramacionAsesor;


            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene Todos Los Datos de la Reprogramacion Por Actividad  y Categoria
        /// </summary>
        /// <param name="idActividadCabecera"></param>
        /// <param name="idCategoriaOrigen"></param>
        /// <returns></returns>
        public ReprogramacionCabeceraBO ObtenerReprogramacionCabecera(int idActividadCabecera, int idCategoriaOrigen)
        {
            try
            {
                string _queryOcurrencia = "Select Id,IdActividadCabecera,IdCategoriaOrigen,MaxReproPorDia,IntervaloSigProgramacionMin From com.V_TReprogramacionCabecera_ObtenerTodo Where Estado=1 and IdActividadCabecera=@IdActividadCabecera and IdCategoriaOrigen=@IdCategoriaOrigen";
                var queryOcurrencia = _dapper.FirstOrDefault(_queryOcurrencia, new { IdActividadCabecera = idActividadCabecera, IdCategoriaOrigen = idCategoriaOrigen });
                return JsonConvert.DeserializeObject<ReprogramacionCabeceraBO>(queryOcurrencia);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: --, Jashin Salazar
        /// Fecha: 19/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Todos Los Datos de la Reprogramacion Por Actividad
        /// </summary>
        /// <param name="idActividadCabecera"></param>
        /// <returns>List<ReprogramacionCabeceraDTO></returns>
        public List<ReprogramacionCabeceraDTO> ObtenerReprogramacionCabPorActividadCab(int IdActividadCabecera)
        {
            try
            {
                List<ReprogramacionCabeceraDTO> ReprogramacionCab = new List<ReprogramacionCabeceraDTO>();
                var query = string.Empty;
                query = "SELECT Id, IdActividadCabecera, IdCategoriaOrigen, MaxReproPorDia, IntervaloSigProgramacionMin, Text_IdCategoriaOrigen FROM com.V_TReprogramacionCabecera_ObtenerTodo WHERE Estado=1 AND IdActividadCabecera=" + IdActividadCabecera;
                var ReprogramacionCabDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(ReprogramacionCabDB) && !ReprogramacionCabDB.Contains("[]"))
                {
                    ReprogramacionCab = JsonConvert.DeserializeObject<List<ReprogramacionCabeceraDTO>>(ReprogramacionCabDB);
                }
                return ReprogramacionCab;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
