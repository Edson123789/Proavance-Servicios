using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EstadoContactoMailingRepositorio : BaseRepository<TEstadoContactoMailing, EstadoContactoMailingBO>
    {
        #region Metodos Base
        public EstadoContactoMailingRepositorio() : base()
        {
        }
        public EstadoContactoMailingRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstadoContactoMailingBO> GetBy(Expression<Func<TEstadoContactoMailing, bool>> filter)
        {
            IEnumerable<TEstadoContactoMailing> listado = base.GetBy(filter);
            List<EstadoContactoMailingBO> listadoBO = new List<EstadoContactoMailingBO>();
            foreach (var itemEntidad in listado)
            {
                EstadoContactoMailingBO objetoBO = Mapper.Map<TEstadoContactoMailing, EstadoContactoMailingBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstadoContactoMailingBO FirstById(int id)
        {
            try
            {
                TEstadoContactoMailing entidad = base.FirstById(id);
                EstadoContactoMailingBO objetoBO = new EstadoContactoMailingBO();
                Mapper.Map<TEstadoContactoMailing, EstadoContactoMailingBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoContactoMailingBO FirstBy(Expression<Func<TEstadoContactoMailing, bool>> filter)
        {
            try
            {
                TEstadoContactoMailing entidad = base.FirstBy(filter);
                EstadoContactoMailingBO objetoBO = Mapper.Map<TEstadoContactoMailing, EstadoContactoMailingBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoContactoMailingBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoContactoMailing entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoContactoMailingBO> listadoBO)
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

        public bool Update(EstadoContactoMailingBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoContactoMailing entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoContactoMailingBO> listadoBO)
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
        private void AsignacionId(TEstadoContactoMailing entidad, EstadoContactoMailingBO objetoBO)
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

        private TEstadoContactoMailing MapeoEntidad(EstadoContactoMailingBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoContactoMailing entidad = new TEstadoContactoMailing();
                entidad = Mapper.Map<EstadoContactoMailingBO, TEstadoContactoMailing>(objetoBO,
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
