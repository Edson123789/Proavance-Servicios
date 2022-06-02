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
    public class EstadoOportunidadRepositorio : BaseRepository<TEstadoOportunidad, EstadoOportunidadBO>
    {
        #region Metodos Base
        public EstadoOportunidadRepositorio() : base()
        {
        }
        public EstadoOportunidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstadoOportunidadBO> GetBy(Expression<Func<TEstadoOportunidad, bool>> filter)
        {
            IEnumerable<TEstadoOportunidad> listado = base.GetBy(filter);
            List<EstadoOportunidadBO> listadoBO = new List<EstadoOportunidadBO>();
            foreach (var itemEntidad in listado)
            {
                EstadoOportunidadBO objetoBO = Mapper.Map<TEstadoOportunidad, EstadoOportunidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstadoOportunidadBO FirstById(int id)
        {
            try
            {
                TEstadoOportunidad entidad = base.FirstById(id);
                EstadoOportunidadBO objetoBO = new EstadoOportunidadBO();
                Mapper.Map<TEstadoOportunidad, EstadoOportunidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoOportunidadBO FirstBy(Expression<Func<TEstadoOportunidad, bool>> filter)
        {
            try
            {
                TEstadoOportunidad entidad = base.FirstBy(filter);
                EstadoOportunidadBO objetoBO = Mapper.Map<TEstadoOportunidad, EstadoOportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoOportunidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoOportunidadBO> listadoBO)
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

        public bool Update(EstadoOportunidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoOportunidadBO> listadoBO)
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
        private void AsignacionId(TEstadoOportunidad entidad, EstadoOportunidadBO objetoBO)
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

        private TEstadoOportunidad MapeoEntidad(EstadoOportunidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoOportunidad entidad = new TEstadoOportunidad();
                entidad = Mapper.Map<EstadoOportunidadBO, TEstadoOportunidad>(objetoBO,
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


        public List<EstadoOportunidadFiltroDTO> ObtenerTodoFiltro() {
            try
            {
                List<EstadoOportunidadFiltroDTO> estadosOportunidad = new List<EstadoOportunidadFiltroDTO>();
                var _query = "SELECT Id, Nombre FROM pla.V_TEstadoOportunidad_ParaFiltro WHERE estado = 1";
                var estadosOportunidadDB = _dapper.QueryDapper(_query,null);
                estadosOportunidad = JsonConvert.DeserializeObject<List<EstadoOportunidadFiltroDTO>>(estadosOportunidadDB);
                return estadosOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

    }
}
