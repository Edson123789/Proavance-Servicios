using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaTipoMovilidadRepositorio : BaseRepository<TRaTipoMovilidad, RaTipoMovilidadBO>
    {
        #region Metodos Base
        public RaTipoMovilidadRepositorio() : base()
        {
        }
        public RaTipoMovilidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaTipoMovilidadBO> GetBy(Expression<Func<TRaTipoMovilidad, bool>> filter)
        {
            IEnumerable<TRaTipoMovilidad> listado = base.GetBy(filter);
            List<RaTipoMovilidadBO> listadoBO = new List<RaTipoMovilidadBO>();
            foreach (var itemEntidad in listado)
            {
                RaTipoMovilidadBO objetoBO = Mapper.Map<TRaTipoMovilidad, RaTipoMovilidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaTipoMovilidadBO FirstById(int id)
        {
            try
            {
                TRaTipoMovilidad entidad = base.FirstById(id);
                RaTipoMovilidadBO objetoBO = new RaTipoMovilidadBO();
                Mapper.Map<TRaTipoMovilidad, RaTipoMovilidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaTipoMovilidadBO FirstBy(Expression<Func<TRaTipoMovilidad, bool>> filter)
        {
            try
            {
                TRaTipoMovilidad entidad = base.FirstBy(filter);
                RaTipoMovilidadBO objetoBO = Mapper.Map<TRaTipoMovilidad, RaTipoMovilidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaTipoMovilidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaTipoMovilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaTipoMovilidadBO> listadoBO)
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

        public bool Update(RaTipoMovilidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaTipoMovilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaTipoMovilidadBO> listadoBO)
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
        private void AsignacionId(TRaTipoMovilidad entidad, RaTipoMovilidadBO objetoBO)
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

        private TRaTipoMovilidad MapeoEntidad(RaTipoMovilidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaTipoMovilidad entidad = new TRaTipoMovilidad();
                entidad = Mapper.Map<RaTipoMovilidadBO, TRaTipoMovilidad>(objetoBO,
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
