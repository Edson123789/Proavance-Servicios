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
    public class PortalEmpleoPaisRepositorio : BaseRepository<TPortalEmpleoPais, PortalEmpleoPaisBO>
    {
        #region Metodos Base
        public PortalEmpleoPaisRepositorio() : base()
        {
        }
        public PortalEmpleoPaisRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PortalEmpleoPaisBO> GetBy(Expression<Func<TPortalEmpleoPais, bool>> filter)
        {
            IEnumerable<TPortalEmpleoPais> listado = base.GetBy(filter);
            List<PortalEmpleoPaisBO> listadoBO = new List<PortalEmpleoPaisBO>();
            foreach (var itemEntidad in listado)
            {
                PortalEmpleoPaisBO objetoBO = Mapper.Map<TPortalEmpleoPais, PortalEmpleoPaisBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PortalEmpleoPaisBO FirstById(int id)
        {
            try
            {
                TPortalEmpleoPais entidad = base.FirstById(id);
                PortalEmpleoPaisBO objetoBO = new PortalEmpleoPaisBO();
                Mapper.Map<TPortalEmpleoPais, PortalEmpleoPaisBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PortalEmpleoPaisBO FirstBy(Expression<Func<TPortalEmpleoPais, bool>> filter)
        {
            try
            {
                TPortalEmpleoPais entidad = base.FirstBy(filter);
                PortalEmpleoPaisBO objetoBO = Mapper.Map<TPortalEmpleoPais, PortalEmpleoPaisBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PortalEmpleoPaisBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPortalEmpleoPais entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PortalEmpleoPaisBO> listadoBO)
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

        public bool Update(PortalEmpleoPaisBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPortalEmpleoPais entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PortalEmpleoPaisBO> listadoBO)
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
        private void AsignacionId(TPortalEmpleoPais entidad, PortalEmpleoPaisBO objetoBO)
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

        private TPortalEmpleoPais MapeoEntidad(PortalEmpleoPaisBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPortalEmpleoPais entidad = new TPortalEmpleoPais();
                entidad = Mapper.Map<PortalEmpleoPaisBO, TPortalEmpleoPais>(objetoBO,
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
