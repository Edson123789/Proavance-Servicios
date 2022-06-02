using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FeedbackTipoRepositorio : BaseRepository<TFeedbackTipo, FeedbackTipoBO>
    {
        #region Metodos Base
        public FeedbackTipoRepositorio() : base()
        {
        }
        public FeedbackTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FeedbackTipoBO> GetBy(Expression<Func<TFeedbackTipo, bool>> filter)
        {
            IEnumerable<TFeedbackTipo> listado = base.GetBy(filter);
            List<FeedbackTipoBO> listadoBO = new List<FeedbackTipoBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackTipoBO objetoBO = Mapper.Map<TFeedbackTipo, FeedbackTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public IEnumerable<FeedbackTipoBO> GetAll()
        {
            IEnumerable<TFeedbackTipo> listado = base.GetAll();
            List<FeedbackTipoBO> listadoBO = new List<FeedbackTipoBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackTipoBO objetoBO = Mapper.Map<TFeedbackTipo, FeedbackTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }

        public FeedbackTipoBO FirstById(int id)
        {
            try
            {
                TFeedbackTipo entidad = base.FirstById(id);
                FeedbackTipoBO objetoBO = new FeedbackTipoBO();
                Mapper.Map<TFeedbackTipo, FeedbackTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FeedbackTipoBO FirstBy(Expression<Func<TFeedbackTipo, bool>> filter)
        {
            try
            {
                TFeedbackTipo entidad = base.FirstBy(filter);
                FeedbackTipoBO objetoBO = Mapper.Map<TFeedbackTipo, FeedbackTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FeedbackTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFeedbackTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FeedbackTipoBO> listadoBO)
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

        public bool Update(FeedbackTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFeedbackTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FeedbackTipoBO> listadoBO)
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
        private void AsignacionId(TFeedbackTipo entidad, FeedbackTipoBO objetoBO)
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

        private TFeedbackTipo MapeoEntidad(FeedbackTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFeedbackTipo entidad = new TFeedbackTipo();
                entidad = Mapper.Map<FeedbackTipoBO, TFeedbackTipo>(objetoBO,
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

        public List<FeedbackTipoFiltroDTO> ObtenerTodoFeedbackTipoFiltro()
        {
            try
            {
                var _query = "SELECT Id,Nombre FROM pla.V_TFeedbackTipo_Filtro Where Estado = 1";
                var areaTrabajoDB = _dapper.QueryDapper(_query, null);
                return JsonConvert.DeserializeObject<List<FeedbackTipoFiltroDTO>>(areaTrabajoDB);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
