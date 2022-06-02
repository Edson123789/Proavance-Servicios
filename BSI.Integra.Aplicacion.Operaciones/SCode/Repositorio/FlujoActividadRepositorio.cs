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
    public class FlujoActividadRepositorio : BaseRepository<TFlujoActividad, FlujoActividadBO>
    {
        #region Metodos Base
        public FlujoActividadRepositorio() : base()
        {
        }
        public FlujoActividadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FlujoActividadBO> GetBy(Expression<Func<TFlujoActividad, bool>> filter)
        {
            IEnumerable<TFlujoActividad> listado = base.GetBy(filter);
            List<FlujoActividadBO> listadoBO = new List<FlujoActividadBO>();
            foreach (var itemEntidad in listado)
            {
                FlujoActividadBO objetoBO = Mapper.Map<TFlujoActividad, FlujoActividadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FlujoActividadBO FirstById(int id)
        {
            try
            {
                TFlujoActividad entidad = base.FirstById(id);
                FlujoActividadBO objetoBO = new FlujoActividadBO();
                Mapper.Map<TFlujoActividad, FlujoActividadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FlujoActividadBO FirstBy(Expression<Func<TFlujoActividad, bool>> filter)
        {
            try
            {
                TFlujoActividad entidad = base.FirstBy(filter);
                FlujoActividadBO objetoBO = Mapper.Map<TFlujoActividad, FlujoActividadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FlujoActividadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFlujoActividad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FlujoActividadBO> listadoBO)
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

        public bool Update(FlujoActividadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFlujoActividad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FlujoActividadBO> listadoBO)
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
        private void AsignacionId(TFlujoActividad entidad, FlujoActividadBO objetoBO)
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

        private TFlujoActividad MapeoEntidad(FlujoActividadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFlujoActividad entidad = new TFlujoActividad();
                entidad = Mapper.Map<FlujoActividadBO, TFlujoActividad>(objetoBO,
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
