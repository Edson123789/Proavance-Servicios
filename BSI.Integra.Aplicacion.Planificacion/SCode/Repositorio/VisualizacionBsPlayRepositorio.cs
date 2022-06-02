using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class VisualizacionBsPlayRepositorio : BaseRepository<TVisualizacionBsPlay, VisualizacionBsPlayBO>
    {
        #region Metodos Base
        public VisualizacionBsPlayRepositorio() : base()
        {
        }
        public VisualizacionBsPlayRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<VisualizacionBsPlayBO> GetBy(Expression<Func<TVisualizacionBsPlay, bool>> filter)
        {
            IEnumerable<TVisualizacionBsPlay> listado = base.GetBy(filter);
            List<VisualizacionBsPlayBO> listadoBO = new List<VisualizacionBsPlayBO>();
            foreach (var itemEntidad in listado)
            {
                VisualizacionBsPlayBO objetoBO = Mapper.Map<TVisualizacionBsPlay, VisualizacionBsPlayBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public VisualizacionBsPlayBO FirstById(int id)
        {
            try
            {
                TVisualizacionBsPlay entidad = base.FirstById(id);
                VisualizacionBsPlayBO objetoBO = new VisualizacionBsPlayBO();
                Mapper.Map<TVisualizacionBsPlay, VisualizacionBsPlayBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public VisualizacionBsPlayBO FirstBy(Expression<Func<TVisualizacionBsPlay, bool>> filter)
        {
            try
            {
                TVisualizacionBsPlay entidad = base.FirstBy(filter);
                VisualizacionBsPlayBO objetoBO = Mapper.Map<TVisualizacionBsPlay, VisualizacionBsPlayBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(VisualizacionBsPlayBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TVisualizacionBsPlay entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<VisualizacionBsPlayBO> listadoBO)
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

        public bool Update(VisualizacionBsPlayBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TVisualizacionBsPlay entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<VisualizacionBsPlayBO> listadoBO)
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
        private void AsignacionId(TVisualizacionBsPlay entidad, VisualizacionBsPlayBO objetoBO)
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

        private TVisualizacionBsPlay MapeoEntidad(VisualizacionBsPlayBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TVisualizacionBsPlay entidad = new TVisualizacionBsPlay();
                entidad = Mapper.Map<VisualizacionBsPlayBO, TVisualizacionBsPlay>(objetoBO,
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
        /// Obtiene la opciones para mostrar o no las App bs Play con los campos necesarios para llenar un Combobox
        /// </summary>
        /// <returns></returns>
        public List<VisualizacionBsPlayFiltroDTO> ObtenerBsPlayFiltro()
        {
            try
            {
                List<VisualizacionBsPlayFiltroDTO> bsPlay = new List<VisualizacionBsPlayFiltroDTO>();
                string _queryBsPlay = string.Empty;
                _queryBsPlay = "Select Id,Nombre From pla.V_TVisualizacionBsPlay_Filtro Where Estado=1";
                var queryBsPlay = _dapper.QueryDapper(_queryBsPlay, null);
                if (!string.IsNullOrEmpty(queryBsPlay) && !queryBsPlay.Contains("[]"))
                {
                    bsPlay = JsonConvert.DeserializeObject<List<VisualizacionBsPlayFiltroDTO>>(queryBsPlay);
                }
                return bsPlay;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        

    }
}
