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
    public class FeedbackConfigurarRepositorio : BaseRepository<TFeedbackConfigurar, FeedbackConfigurarBO>
    {
        #region Metodos Base
        public FeedbackConfigurarRepositorio() : base()
        {
        }
        public FeedbackConfigurarRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FeedbackConfigurarBO> GetBy(Expression<Func<TFeedbackConfigurar, bool>> filter)
        {
            IEnumerable<TFeedbackConfigurar> listado = base.GetBy(filter);
            List<FeedbackConfigurarBO> listadoBO = new List<FeedbackConfigurarBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackConfigurarBO objetoBO = Mapper.Map<TFeedbackConfigurar, FeedbackConfigurarBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public IEnumerable<FeedbackConfigurarBO> GetAll()
        {
            IEnumerable<TFeedbackConfigurar> listado = base.GetAll();
            List<FeedbackConfigurarBO> listadoBO = new List<FeedbackConfigurarBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackConfigurarBO objetoBO = Mapper.Map<TFeedbackConfigurar, FeedbackConfigurarBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }

        public FeedbackConfigurarBO FirstById(int id)
        {
            try
            {
                TFeedbackConfigurar entidad = base.FirstById(id);
                FeedbackConfigurarBO objetoBO = new FeedbackConfigurarBO();
                Mapper.Map<TFeedbackConfigurar, FeedbackConfigurarBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FeedbackConfigurarBO FirstBy(Expression<Func<TFeedbackConfigurar, bool>> filter)
        {
            try
            {
                TFeedbackConfigurar entidad = base.FirstBy(filter);
                FeedbackConfigurarBO objetoBO = Mapper.Map<TFeedbackConfigurar, FeedbackConfigurarBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FeedbackConfigurarBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFeedbackConfigurar entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FeedbackConfigurarBO> listadoBO)
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

        public bool Update(FeedbackConfigurarBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFeedbackConfigurar entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FeedbackConfigurarBO> listadoBO)
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
        private void AsignacionId(TFeedbackConfigurar entidad, FeedbackConfigurarBO objetoBO)
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

        private TFeedbackConfigurar MapeoEntidad(FeedbackConfigurarBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFeedbackConfigurar entidad = new TFeedbackConfigurar();
                entidad = Mapper.Map<FeedbackConfigurarBO, TFeedbackConfigurar>(objetoBO,
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

        public List<FeedbackConfigurarFiltroDTO> ObtenerTodoFeedbackConfigurarFiltro()
        {
            try
            {
                var _query = "SELECT Id,IdFeedbackTipo,NombreFeedbackTipo,Nombre FROM pla.V_TFeedbackConfigurar_Filtro Where Estado = 1";
                var areaTrabajoDB = _dapper.QueryDapper(_query, null);
                return JsonConvert.DeserializeObject<List<FeedbackConfigurarFiltroDTO>>(areaTrabajoDB);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
