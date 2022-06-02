using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    class ReclamoEstadoRepositorio : BaseRepository<TReclamoEstado, ReclamoEstadoBO>
    {
        #region Metodos Base
        public ReclamoEstadoRepositorio() : base()
        {
        }
        public ReclamoEstadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ReclamoEstadoBO> GetBy(Expression<Func<TReclamoEstado, bool>> filter)
        {
            IEnumerable<TReclamoEstado> listado = base.GetBy(filter);
            List<ReclamoEstadoBO> listadoBO = new List<ReclamoEstadoBO>();
            foreach (var itemEntidad in listado)
            {
                ReclamoEstadoBO objetoBO = Mapper.Map<TReclamoEstado, ReclamoEstadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ReclamoEstadoBO FirstById(int id)
        {
            try
            {
                TReclamoEstado entidad = base.FirstById(id);
                ReclamoEstadoBO objetoBO = new ReclamoEstadoBO();
                Mapper.Map<TReclamoEstado, ReclamoEstadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ReclamoEstadoBO FirstBy(Expression<Func<TReclamoEstado, bool>> filter)
        {
            try
            {
                TReclamoEstado entidad = base.FirstBy(filter);
                ReclamoEstadoBO objetoBO = Mapper.Map<TReclamoEstado, ReclamoEstadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ReclamoEstadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TReclamoEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ReclamoEstadoBO> listadoBO)
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

        public bool Update(ReclamoEstadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TReclamoEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ReclamoEstadoBO> listadoBO)
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
        private void AsignacionId(TReclamoEstado entidad, ReclamoEstadoBO objetoBO)
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

        private TReclamoEstado MapeoEntidad(ReclamoEstadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TReclamoEstado entidad = new TReclamoEstado();
                entidad = Mapper.Map<ReclamoEstadoBO, TReclamoEstado>(objetoBO,
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
