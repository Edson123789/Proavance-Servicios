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
    public class AsignacionAutomaticaErrorRepositorio : BaseRepository<TAsignacionAutomaticaError, AsignacionAutomaticaErrorBO>
    {
        #region Metodos Base
        public AsignacionAutomaticaErrorRepositorio() : base()
        {
        }
        public AsignacionAutomaticaErrorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsignacionAutomaticaErrorBO> GetBy(Expression<Func<TAsignacionAutomaticaError, bool>> filter)
        {
            IEnumerable<TAsignacionAutomaticaError> listado = base.GetBy(filter);
            List<AsignacionAutomaticaErrorBO> listadoBO = new List<AsignacionAutomaticaErrorBO>();
            foreach (var itemEntidad in listado)
            {
                AsignacionAutomaticaErrorBO objetoBO = Mapper.Map<TAsignacionAutomaticaError, AsignacionAutomaticaErrorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsignacionAutomaticaErrorBO FirstById(int id)
        {
            try
            {
                TAsignacionAutomaticaError entidad = base.FirstById(id);
                AsignacionAutomaticaErrorBO objetoBO = new AsignacionAutomaticaErrorBO();
                Mapper.Map<TAsignacionAutomaticaError, AsignacionAutomaticaErrorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsignacionAutomaticaErrorBO FirstBy(Expression<Func<TAsignacionAutomaticaError, bool>> filter)
        {
            try
            {
                TAsignacionAutomaticaError entidad = base.FirstBy(filter);
                AsignacionAutomaticaErrorBO objetoBO = Mapper.Map<TAsignacionAutomaticaError, AsignacionAutomaticaErrorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsignacionAutomaticaErrorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsignacionAutomaticaError entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsignacionAutomaticaErrorBO> listadoBO)
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

        public bool Update(AsignacionAutomaticaErrorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsignacionAutomaticaError entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsignacionAutomaticaErrorBO> listadoBO)
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
        private void AsignacionId(TAsignacionAutomaticaError entidad, AsignacionAutomaticaErrorBO objetoBO)
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

        private TAsignacionAutomaticaError MapeoEntidad(AsignacionAutomaticaErrorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsignacionAutomaticaError entidad = new TAsignacionAutomaticaError();
                entidad = Mapper.Map<AsignacionAutomaticaErrorBO, TAsignacionAutomaticaError>(objetoBO,
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

        public List<AsignacionAutomaticaErrorDTO> ObtenerError(int idAsignacionAutomatica)
        {
            try
            {
                string _queryError = "Select Id,Campo,Descripcion,IdContacto,IdAsignacionAutomatica,IdAsignacionAutomaticaTipoError From com.V_TAsignacionAutomaticaError_ObtenerError where Estado=1 and IdAsignacionAutomaticaTipoError=1 and IdAsignacionAutomatica = @IdAsignacionAutomatica  ";
                var queryError = _dapper.QueryDapper(_queryError,new { IdAsignacionAutomatica = idAsignacionAutomatica});
                return JsonConvert.DeserializeObject<List<AsignacionAutomaticaErrorDTO>>(queryError);


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
