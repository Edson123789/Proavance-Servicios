using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class EmbudoNivelRepositorio : BaseRepository<TEmbudoNivel, EmbudoNivelBO>
    {
        #region Metodos Base
        public EmbudoNivelRepositorio() : base()
        {
        }
        public EmbudoNivelRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EmbudoNivelBO> GetBy(Expression<Func<TEmbudoNivel, bool>> filter)
        {
            IEnumerable<TEmbudoNivel> listado = base.GetBy(filter);
            List<EmbudoNivelBO> listadoBO = new List<EmbudoNivelBO>();
            foreach (var itemEntidad in listado)
            {
                EmbudoNivelBO objetoBO = Mapper.Map<TEmbudoNivel, EmbudoNivelBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EmbudoNivelBO FirstById(int id)
        {
            try
            {
                TEmbudoNivel entidad = base.FirstById(id);
                EmbudoNivelBO objetoBO = new EmbudoNivelBO();
                Mapper.Map<TEmbudoNivel, EmbudoNivelBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EmbudoNivelBO FirstBy(Expression<Func<TEmbudoNivel, bool>> filter)
        {
            try
            {
                TEmbudoNivel entidad = base.FirstBy(filter);
                EmbudoNivelBO objetoBO = Mapper.Map<TEmbudoNivel, EmbudoNivelBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EmbudoNivelBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEmbudoNivel entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EmbudoNivelBO> listadoBO)
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

        public bool Update(EmbudoNivelBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEmbudoNivel entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EmbudoNivelBO> listadoBO)
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
        private void AsignacionId(TEmbudoNivel entidad, EmbudoNivelBO objetoBO)
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

        private TEmbudoNivel MapeoEntidad(EmbudoNivelBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEmbudoNivel entidad = new TEmbudoNivel();
                entidad = Mapper.Map<EmbudoNivelBO, TEmbudoNivel>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None).ForMember(dest => dest.IdMigracion, m => m.Ignore()));

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
        /// Obtiene el Id y nombre de todos los registros
        /// </summary>
        /// <returns></returns>
        public List<FiltroIdNombreDTO> ObtenerEmbudoNivel()
        {
            try
            {
                return new List<FiltroIdNombreDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
