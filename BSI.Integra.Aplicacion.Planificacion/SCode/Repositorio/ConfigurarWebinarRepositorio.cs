using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ConfigurarWebinarRepositorio : BaseRepository<TConfigurarWebinar, ConfigurarWebinarBO>
    {
        #region Metodos Base
        public ConfigurarWebinarRepositorio() : base()
        {
        }
        public ConfigurarWebinarRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfigurarWebinarBO> GetBy(Expression<Func<TConfigurarWebinar, bool>> filter)
        {
            IEnumerable<TConfigurarWebinar> listado = base.GetBy(filter);
            List<ConfigurarWebinarBO> listadoBO = new List<ConfigurarWebinarBO>();
            foreach (var itemEntidad in listado)
            {
                ConfigurarWebinarBO objetoBO = Mapper.Map<TConfigurarWebinar, ConfigurarWebinarBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfigurarWebinarBO FirstById(int id)
        {
            try
            {
                TConfigurarWebinar entidad = base.FirstById(id);
                ConfigurarWebinarBO objetoBO = new ConfigurarWebinarBO();
                Mapper.Map<TConfigurarWebinar, ConfigurarWebinarBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfigurarWebinarBO FirstBy(Expression<Func<TConfigurarWebinar, bool>> filter)
        {
            try
            {
                TConfigurarWebinar entidad = base.FirstBy(filter);
                ConfigurarWebinarBO objetoBO = Mapper.Map<TConfigurarWebinar, ConfigurarWebinarBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfigurarWebinarBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfigurarWebinar entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfigurarWebinarBO> listadoBO)
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

        public bool Update(ConfigurarWebinarBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfigurarWebinar entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfigurarWebinarBO> listadoBO)
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
        private void AsignacionId(TConfigurarWebinar entidad, ConfigurarWebinarBO objetoBO)
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

        private TConfigurarWebinar MapeoEntidad(ConfigurarWebinarBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfigurarWebinar entidad = new TConfigurarWebinar();
                entidad = Mapper.Map<ConfigurarWebinarBO, TConfigurarWebinar>(objetoBO,
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

        public List<ConfigurarWebinarDTO> ObtenerWebinarInscrito(int idPEspecificoPadre)
        {

            try
            {
                List<ConfigurarWebinarDTO> Webinar = new List<ConfigurarWebinarDTO>();
                var campos = "Id, IdPEspecifico,Modalidad,Codigo,IdOperadorComparacionAvance,ValorAvance,ValorAVanceOpc,IdOperadorComparacionPromedio,ValorPromedio,ValorPromedioOpc,UsuarioModificacion,IdPEspecificoPadre";

                var _query = "SELECT " + campos + " FROM  pla.V_TObtenerWebinar WHERE Estado=1 AND IdPEspecificoPadre=@IdPEspecificoPadre";
                var listaWebinarDB = _dapper.QueryDapper(_query, new { IdPEspecificoPadre = idPEspecificoPadre });
                Webinar = JsonConvert.DeserializeObject<List<ConfigurarWebinarDTO>>(listaWebinarDB);                
                return Webinar;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

    }


}
