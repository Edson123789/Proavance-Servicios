using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Base.DTO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ErrorRepositorio : BaseRepository<TError, ErrorBO>
    {
        #region Metodos Base
        public ErrorRepositorio() : base()
        {
        }
        public ErrorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ErrorBO> GetBy(Expression<Func<TError, bool>> filter)
        {
            IEnumerable<TError> listado = base.GetBy(filter);
            List<ErrorBO> listadoBO = new List<ErrorBO>();
            foreach (var itemEntidad in listado)
            {
                ErrorBO objetoBO = Mapper.Map<TError, ErrorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ErrorBO FirstById(int id)
        {
            try
            {
                TError entidad = base.FirstById(id);
                ErrorBO objetoBO = new ErrorBO();
                Mapper.Map<TError, ErrorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ErrorBO FirstBy(Expression<Func<TError, bool>> filter)
        {
            try
            {
                TError entidad = base.FirstBy(filter);
                ErrorBO objetoBO = Mapper.Map<TError, ErrorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ErrorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TError entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ErrorBO> listadoBO)
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

        public bool Update(ErrorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TError entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ErrorBO> listadoBO)
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
        private void AsignacionId(TError entidad, ErrorBO objetoBO)
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

        private TError MapeoEntidad(ErrorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TError entidad = new TError();
                entidad = Mapper.Map<ErrorBO, TError>(objetoBO,
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
        public List<ErrorDTO> ObtenerTodosErroresSistema()
        {
            List<ErrorDTO> errores = new List<ErrorDTO>();
            var _query = string.Empty;
            _query = "SELECT Codigo,IdErrorTipo,Descripcion,Estado, DescripcionPersonalizada FROM conf.V_TError_StartupDatos Where Estado = 1";
            var _errores = _dapper.QueryDapper(_query,null);
            errores = JsonConvert.DeserializeObject<List<ErrorDTO>>(_errores);
            return errores;
        }
    }
}
