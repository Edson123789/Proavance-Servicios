using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class ReferidoConfiguracionRepositorio : BaseRepository<TReferidoConfiguracion, ReferidoConfiguracionBO>
    {
        #region Metodos Base
        public ReferidoConfiguracionRepositorio() : base()
        {
        }
        public ReferidoConfiguracionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ReferidoConfiguracionBO> GetBy(Expression<Func<TReferidoConfiguracion, bool>> filter)
        {
            IEnumerable<TReferidoConfiguracion> listado = base.GetBy(filter);
            List<ReferidoConfiguracionBO> listadoBO = new List<ReferidoConfiguracionBO>();
            foreach (var itemEntidad in listado)
            {
                ReferidoConfiguracionBO objetoBO = Mapper.Map<TReferidoConfiguracion, ReferidoConfiguracionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ReferidoConfiguracionBO FirstById(int id)
        {
            try
            {
                TReferidoConfiguracion entidad = base.FirstById(id);
                ReferidoConfiguracionBO objetoBO = new ReferidoConfiguracionBO();
                Mapper.Map<TReferidoConfiguracion, ReferidoConfiguracionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ReferidoConfiguracionBO FirstBy(Expression<Func<TReferidoConfiguracion, bool>> filter)
        {
            try
            {
                TReferidoConfiguracion entidad = base.FirstBy(filter);
                ReferidoConfiguracionBO objetoBO = Mapper.Map<TReferidoConfiguracion, ReferidoConfiguracionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ReferidoConfiguracionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TReferidoConfiguracion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ReferidoConfiguracionBO> listadoBO)
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

        public bool Update(ReferidoConfiguracionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TReferidoConfiguracion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ReferidoConfiguracionBO> listadoBO)
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
        private void AsignacionId(TReferidoConfiguracion entidad, ReferidoConfiguracionBO objetoBO)
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

        private TReferidoConfiguracion MapeoEntidad(ReferidoConfiguracionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TReferidoConfiguracion entidad = new TReferidoConfiguracion();
                entidad = Mapper.Map<ReferidoConfiguracionBO, TReferidoConfiguracion>(objetoBO,
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
        /// Obtiene el primer registro de la tabla.
        /// </summary>
        /// <returns></returns>
        public ReferidoConfiguracionDTO GetConfiguracionReferidos()
        {
            try
            {
                string query = "SELECT IdTipoDato, DescripcionTipoDato, IdOrigen, NombreOrigen, IdFaseOportunidad, CodigoFaseOportunidad FROM com.V_ObtenerDescripcionReferidoConfiguracion WHERE Estado = 1";
                string queryRespuesta = _dapper.FirstOrDefault(query, null);
                if(queryRespuesta != "null")
                {
                    return JsonConvert.DeserializeObject<ReferidoConfiguracionDTO>(queryRespuesta);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
