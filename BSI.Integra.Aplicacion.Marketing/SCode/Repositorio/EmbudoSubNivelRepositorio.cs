using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class EmbudoSubNivelRepositorio : BaseRepository<TEmbudoSubNivel, EmbudoSubNivelBO>
    {
        #region Metodos Base
        public EmbudoSubNivelRepositorio() : base()
        {
        }
        public EmbudoSubNivelRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EmbudoSubNivelBO> GetBy(Expression<Func<TEmbudoSubNivel, bool>> filter)
        {
            IEnumerable<TEmbudoSubNivel> listado = base.GetBy(filter);
            List<EmbudoSubNivelBO> listadoBO = new List<EmbudoSubNivelBO>();
            foreach (var itemEntidad in listado)
            {
                EmbudoSubNivelBO objetoBO = Mapper.Map<TEmbudoSubNivel, EmbudoSubNivelBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EmbudoSubNivelBO FirstById(int id)
        {
            try
            {
                TEmbudoSubNivel entidad = base.FirstById(id);
                EmbudoSubNivelBO objetoBO = new EmbudoSubNivelBO();
                Mapper.Map<TEmbudoSubNivel, EmbudoSubNivelBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EmbudoSubNivelBO FirstBy(Expression<Func<TEmbudoSubNivel, bool>> filter)
        {
            try
            {
                TEmbudoSubNivel entidad = base.FirstBy(filter);
                EmbudoSubNivelBO objetoBO = Mapper.Map<TEmbudoSubNivel, EmbudoSubNivelBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EmbudoSubNivelBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEmbudoSubNivel entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EmbudoSubNivelBO> listadoBO)
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

        public bool Update(EmbudoSubNivelBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEmbudoSubNivel entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EmbudoSubNivelBO> listadoBO)
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
        private void AsignacionId(TEmbudoSubNivel entidad, EmbudoSubNivelBO objetoBO)
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

        private TEmbudoSubNivel MapeoEntidad(EmbudoSubNivelBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEmbudoSubNivel entidad = new TEmbudoSubNivel();
                entidad = Mapper.Map<EmbudoSubNivelBO, TEmbudoSubNivel>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<EmbudoSubNivelBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TEmbudoSubNivel, bool>>> filters, Expression<Func<TEmbudoSubNivel, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TEmbudoSubNivel> listado = base.GetFiltered(filters, orderBy, ascending);
            List<EmbudoSubNivelBO> listadoBO = new List<EmbudoSubNivelBO>();

            foreach (var itemEntidad in listado)
            {
                EmbudoSubNivelBO objetoBO = Mapper.Map<TEmbudoSubNivel, EmbudoSubNivelBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
