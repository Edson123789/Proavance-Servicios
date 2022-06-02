using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaCentroCostoEstadoRepositorio : BaseRepository<TRaCentroCostoEstado, RaCentroCostoEstadoBO>
    {
        #region Metodos Base
        public RaCentroCostoEstadoRepositorio() : base()
        {
        }
        public RaCentroCostoEstadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaCentroCostoEstadoBO> GetBy(Expression<Func<TRaCentroCostoEstado, bool>> filter)
        {
            IEnumerable<TRaCentroCostoEstado> listado = base.GetBy(filter);
            List<RaCentroCostoEstadoBO> listadoBO = new List<RaCentroCostoEstadoBO>();
            foreach (var itemEntidad in listado)
            {
                RaCentroCostoEstadoBO objetoBO = Mapper.Map<TRaCentroCostoEstado, RaCentroCostoEstadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaCentroCostoEstadoBO FirstById(int id)
        {
            try
            {
                TRaCentroCostoEstado entidad = base.FirstById(id);
                RaCentroCostoEstadoBO objetoBO = new RaCentroCostoEstadoBO();
                Mapper.Map<TRaCentroCostoEstado, RaCentroCostoEstadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaCentroCostoEstadoBO FirstBy(Expression<Func<TRaCentroCostoEstado, bool>> filter)
        {
            try
            {
                TRaCentroCostoEstado entidad = base.FirstBy(filter);
                RaCentroCostoEstadoBO objetoBO = Mapper.Map<TRaCentroCostoEstado, RaCentroCostoEstadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaCentroCostoEstadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaCentroCostoEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaCentroCostoEstadoBO> listadoBO)
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

        public bool Update(RaCentroCostoEstadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaCentroCostoEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaCentroCostoEstadoBO> listadoBO)
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
        private void AsignacionId(TRaCentroCostoEstado entidad, RaCentroCostoEstadoBO objetoBO)
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

        private TRaCentroCostoEstado MapeoEntidad(RaCentroCostoEstadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaCentroCostoEstado entidad = new TRaCentroCostoEstado();
                entidad = Mapper.Map<RaCentroCostoEstadoBO, TRaCentroCostoEstado>(objetoBO,
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

