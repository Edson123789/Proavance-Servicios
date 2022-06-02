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
    public class FeedbackConfigurarGrupoPreguntaRepositorio : BaseRepository<TFeedbackConfigurarGrupoPregunta, FeedbackConfigurarGrupoPreguntaBO>
    {
        #region Metodos Base
        public FeedbackConfigurarGrupoPreguntaRepositorio() : base()
        {
        }
        public FeedbackConfigurarGrupoPreguntaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FeedbackConfigurarGrupoPreguntaBO> GetBy(Expression<Func<TFeedbackConfigurarGrupoPregunta, bool>> filter)
        {
            IEnumerable<TFeedbackConfigurarGrupoPregunta> listado = base.GetBy(filter);
            List<FeedbackConfigurarGrupoPreguntaBO> listadoBO = new List<FeedbackConfigurarGrupoPreguntaBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackConfigurarGrupoPreguntaBO objetoBO = Mapper.Map<TFeedbackConfigurarGrupoPregunta, FeedbackConfigurarGrupoPreguntaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public IEnumerable<FeedbackConfigurarGrupoPreguntaBO> GetAll()
        {
            IEnumerable<TFeedbackConfigurarGrupoPregunta> listado = base.GetAll();
            List<FeedbackConfigurarGrupoPreguntaBO> listadoBO = new List<FeedbackConfigurarGrupoPreguntaBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackConfigurarGrupoPreguntaBO objetoBO = Mapper.Map<TFeedbackConfigurarGrupoPregunta, FeedbackConfigurarGrupoPreguntaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }

        public FeedbackConfigurarGrupoPreguntaBO FirstById(int id)
        {
            try
            {
                TFeedbackConfigurarGrupoPregunta entidad = base.FirstById(id);
                FeedbackConfigurarGrupoPreguntaBO objetoBO = new FeedbackConfigurarGrupoPreguntaBO();
                Mapper.Map<TFeedbackConfigurarGrupoPregunta, FeedbackConfigurarGrupoPreguntaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FeedbackConfigurarGrupoPreguntaBO FirstBy(Expression<Func<TFeedbackConfigurarGrupoPregunta, bool>> filter)
        {
            try
            {
                TFeedbackConfigurarGrupoPregunta entidad = base.FirstBy(filter);
                FeedbackConfigurarGrupoPreguntaBO objetoBO = Mapper.Map<TFeedbackConfigurarGrupoPregunta, FeedbackConfigurarGrupoPreguntaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FeedbackConfigurarGrupoPreguntaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFeedbackConfigurarGrupoPregunta entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FeedbackConfigurarGrupoPreguntaBO> listadoBO)
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

        public bool Update(FeedbackConfigurarGrupoPreguntaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFeedbackConfigurarGrupoPregunta entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FeedbackConfigurarGrupoPreguntaBO> listadoBO)
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
        private void AsignacionId(TFeedbackConfigurarGrupoPregunta entidad, FeedbackConfigurarGrupoPreguntaBO objetoBO)
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

        private TFeedbackConfigurarGrupoPregunta MapeoEntidad(FeedbackConfigurarGrupoPreguntaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFeedbackConfigurarGrupoPregunta entidad = new TFeedbackConfigurarGrupoPregunta();
                entidad = Mapper.Map<FeedbackConfigurarGrupoPreguntaBO, TFeedbackConfigurarGrupoPregunta>(objetoBO,
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

        public List<listaFeedbackConfigurarGrupoPregunta> ObtenerListaFeedbackConfigurarGrupoPregunta()
        {
            try
            {
                var _query = "SELECT Id,IdFeedbackConfigurar,Nombre FROM pla.V_listaFeedbackConfigurarGrupoPregunta";
                var areaTrabajoDB = _dapper.QueryDapper(_query, null);
                return JsonConvert.DeserializeObject<List<listaFeedbackConfigurarGrupoPregunta>>(areaTrabajoDB);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
