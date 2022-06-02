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
    public class LlamadaWebphoneRepositorio : BaseRepository<TLlamadaWebphone, LlamadaWebphoneBO>
    {
        #region Metodos Base
        public LlamadaWebphoneRepositorio() : base()
        {
        }
        public LlamadaWebphoneRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<LlamadaWebphoneBO> GetBy(Expression<Func<TLlamadaWebphone, bool>> filter)
        {
            IEnumerable<TLlamadaWebphone> listado = base.GetBy(filter);
            List<LlamadaWebphoneBO> listadoBO = new List<LlamadaWebphoneBO>();
            foreach (var itemEntidad in listado)
            {
                LlamadaWebphoneBO objetoBO = Mapper.Map<TLlamadaWebphone, LlamadaWebphoneBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public LlamadaWebphoneBO FirstById(int id)
        {
            try
            {
                TLlamadaWebphone entidad = base.FirstById(id);
                LlamadaWebphoneBO objetoBO = new LlamadaWebphoneBO();
                Mapper.Map<TLlamadaWebphone, LlamadaWebphoneBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LlamadaWebphoneBO FirstBy(Expression<Func<TLlamadaWebphone, bool>> filter)
        {
            try
            {
                TLlamadaWebphone entidad = base.FirstBy(filter);
                LlamadaWebphoneBO objetoBO = Mapper.Map<TLlamadaWebphone, LlamadaWebphoneBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(LlamadaWebphoneBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TLlamadaWebphone entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<LlamadaWebphoneBO> listadoBO)
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

        public bool Update(LlamadaWebphoneBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TLlamadaWebphone entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<LlamadaWebphoneBO> listadoBO)
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
        private void AsignacionId(TLlamadaWebphone entidad, LlamadaWebphoneBO objetoBO)
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

        private TLlamadaWebphone MapeoEntidad(LlamadaWebphoneBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TLlamadaWebphone entidad = new TLlamadaWebphone();
                entidad = Mapper.Map<LlamadaWebphoneBO, TLlamadaWebphone>(objetoBO,
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
        /// Obtiene los datos de actividades para regularizar datos
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<RegularizacionLLamadaWebphoneDTO> ObtenerActividadesSinLlamadas()
        {
            try
            {
                List<RegularizacionLLamadaWebphoneDTO> items = new List<RegularizacionLLamadaWebphoneDTO>();

                var query = _dapper.QuerySPDapper("com.SP_RegularizacionLlamada", new
                { });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<RegularizacionLLamadaWebphoneDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene las llamadas para una activdad segun sus fechas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<LlamadaRegularizacionDTO> ObtenerLlamadasPorFecha(DateTime fechaInicio, DateTime FechaFin, string Anexo)
        {
            try
            {
                List<LlamadaRegularizacionDTO> items = new List<LlamadaRegularizacionDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ObtenerLlamadaPorFecha", new
                {
                    FechaInicio = fechaInicio,
                    FechaFin = FechaFin,
                    Anexo = Anexo
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<LlamadaRegularizacionDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

    }
}
