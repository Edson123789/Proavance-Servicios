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
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ExpositorLogRepositorio : BaseRepository<TExpositorLog, ExpositorLogBO>
    {
        #region Metodos Base
        public ExpositorLogRepositorio() : base()
        {
        }
        public ExpositorLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ExpositorLogBO> GetBy(Expression<Func<TExpositorLog, bool>> filter)
        {
            IEnumerable<TExpositorLog> listado = base.GetBy(filter);
            List<ExpositorLogBO> listadoBO = new List<ExpositorLogBO>();
            foreach (var itemEntidad in listado)
            {
                ExpositorLogBO objetoBO = Mapper.Map<TExpositorLog, ExpositorLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ExpositorLogBO FirstById(int id)
        {
            try
            {
                TExpositorLog entidad = base.FirstById(id);
                ExpositorLogBO objetoBO = new ExpositorLogBO();
                Mapper.Map<TExpositorLog, ExpositorLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ExpositorLogBO FirstBy(Expression<Func<TExpositorLog, bool>> filter)
        {
            try
            {
                TExpositorLog entidad = base.FirstBy(filter);
                ExpositorLogBO objetoBO = Mapper.Map<TExpositorLog, ExpositorLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ExpositorLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TExpositorLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ExpositorLogBO> listadoBO)
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

        public bool Update(ExpositorLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TExpositorLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ExpositorLogBO> listadoBO)
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
        private void AsignacionId(TExpositorLog entidad, ExpositorLogBO objetoBO)
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

        private TExpositorLog MapeoEntidad(ExpositorLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TExpositorLog entidad = new TExpositorLog();
                entidad = Mapper.Map<ExpositorLogBO, TExpositorLog>(objetoBO,
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
