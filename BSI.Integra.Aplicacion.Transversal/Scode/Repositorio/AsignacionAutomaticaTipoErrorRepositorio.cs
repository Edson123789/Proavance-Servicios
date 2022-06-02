using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class AsignacionAutomaticaTipoErrorRepositorio : BaseRepository<TAsignacionAutomaticaTipoError, AsignacionAutomaticaTipoErrorBO>
    {
        #region Metodos Base
        public AsignacionAutomaticaTipoErrorRepositorio() : base()
        {
        }
        public AsignacionAutomaticaTipoErrorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsignacionAutomaticaTipoErrorBO> GetBy(Expression<Func<TAsignacionAutomaticaTipoError, bool>> filter)
        {
            IEnumerable<TAsignacionAutomaticaTipoError> listado = base.GetBy(filter);
            List<AsignacionAutomaticaTipoErrorBO> listadoBO = new List<AsignacionAutomaticaTipoErrorBO>();
            foreach (var itemEntidad in listado)
            {
                AsignacionAutomaticaTipoErrorBO objetoBO = Mapper.Map<TAsignacionAutomaticaTipoError, AsignacionAutomaticaTipoErrorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsignacionAutomaticaTipoErrorBO FirstById(int id)
        {
            try
            {
                TAsignacionAutomaticaTipoError entidad = base.FirstById(id);
                AsignacionAutomaticaTipoErrorBO objetoBO = new AsignacionAutomaticaTipoErrorBO();
                Mapper.Map<TAsignacionAutomaticaTipoError, AsignacionAutomaticaTipoErrorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsignacionAutomaticaTipoErrorBO FirstBy(Expression<Func<TAsignacionAutomaticaTipoError, bool>> filter)
        {
            try
            {
                TAsignacionAutomaticaTipoError entidad = base.FirstBy(filter);
                AsignacionAutomaticaTipoErrorBO objetoBO = Mapper.Map<TAsignacionAutomaticaTipoError, AsignacionAutomaticaTipoErrorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsignacionAutomaticaTipoErrorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsignacionAutomaticaTipoError entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsignacionAutomaticaTipoErrorBO> listadoBO)
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

        public bool Update(AsignacionAutomaticaTipoErrorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsignacionAutomaticaTipoError entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsignacionAutomaticaTipoErrorBO> listadoBO)
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
        private void AsignacionId(TAsignacionAutomaticaTipoError entidad, AsignacionAutomaticaTipoErrorBO objetoBO)
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

        private TAsignacionAutomaticaTipoError MapeoEntidad(AsignacionAutomaticaTipoErrorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsignacionAutomaticaTipoError entidad = new TAsignacionAutomaticaTipoError();
                entidad = Mapper.Map<AsignacionAutomaticaTipoErrorBO, TAsignacionAutomaticaTipoError>(objetoBO,
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
    }
}
