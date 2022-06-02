
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class TipoModalidadRepositorio : BaseRepository<TTipoModalidad, TipoModalidadBO>
    {
        #region Metodos Base
        public TipoModalidadRepositorio() : base()
        {
        }
        public TipoModalidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoModalidadBO> GetBy(Expression<Func<TTipoModalidad, bool>> filter)
        {
            IEnumerable<TTipoModalidad> listado = base.GetBy(filter);
            List<TipoModalidadBO> listadoBO = new List<TipoModalidadBO>();
            foreach (var itemEntidad in listado)
            {
                TipoModalidadBO objetoBO = Mapper.Map<TTipoModalidad, TipoModalidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoModalidadBO FirstById(int id)
        {
            try
            {
                TTipoModalidad entidad = base.FirstById(id);
                TipoModalidadBO objetoBO = new TipoModalidadBO();
                Mapper.Map<TTipoModalidad, TipoModalidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoModalidadBO FirstBy(Expression<Func<TTipoModalidad, bool>> filter)
        {
            try
            {
                TTipoModalidad entidad = base.FirstBy(filter);
                TipoModalidadBO objetoBO = Mapper.Map<TTipoModalidad, TipoModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoModalidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoModalidadBO> listadoBO)
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

        public bool Update(TipoModalidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoModalidadBO> listadoBO)
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
        private void AsignacionId(TTipoModalidad entidad, TipoModalidadBO objetoBO)
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

        private TTipoModalidad MapeoEntidad(TipoModalidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoModalidad entidad = new TTipoModalidad();
                entidad = Mapper.Map<TipoModalidadBO, TTipoModalidad>(objetoBO,
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
        /// Obtiene Los tipos de Modalidad [Id, Nombre] para ser usados en Combobox
        /// </summary>
        /// <returns></returns>
        public List<TipoModalidadFiltroDTO> ObtenerTodoTipoModalidadesFiltro()
        {
            try
            {
                List<TipoModalidadFiltroDTO> TipoModalidades = new List<TipoModalidadFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.T_TipoModalidad WHERE Estado = 1";
                var TipoModalidadesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(TipoModalidadesDB) && !TipoModalidadesDB.Contains("[]"))
                {
                    TipoModalidades = JsonConvert.DeserializeObject<List<TipoModalidadFiltroDTO>>(TipoModalidadesDB);
                }
                return TipoModalidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
