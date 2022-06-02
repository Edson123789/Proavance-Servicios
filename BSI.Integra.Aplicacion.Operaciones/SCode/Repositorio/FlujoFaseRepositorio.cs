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
    public class FlujoFaseRepositorio : BaseRepository<TFlujoFase, FlujoFaseBO>
    {
        #region Metodos Base
        public FlujoFaseRepositorio() : base()
        {
        }
        public FlujoFaseRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FlujoFaseBO> GetBy(Expression<Func<TFlujoFase, bool>> filter)
        {
            IEnumerable<TFlujoFase> listado = base.GetBy(filter);
            List<FlujoFaseBO> listadoBO = new List<FlujoFaseBO>();
            foreach (var itemEntidad in listado)
            {
                FlujoFaseBO objetoBO = Mapper.Map<TFlujoFase, FlujoFaseBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FlujoFaseBO FirstById(int id)
        {
            try
            {
                TFlujoFase entidad = base.FirstById(id);
                FlujoFaseBO objetoBO = new FlujoFaseBO();
                Mapper.Map<TFlujoFase, FlujoFaseBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FlujoFaseBO FirstBy(Expression<Func<TFlujoFase, bool>> filter)
        {
            try
            {
                TFlujoFase entidad = base.FirstBy(filter);
                FlujoFaseBO objetoBO = Mapper.Map<TFlujoFase, FlujoFaseBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FlujoFaseBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFlujoFase entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FlujoFaseBO> listadoBO)
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

        public bool Update(FlujoFaseBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFlujoFase entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FlujoFaseBO> listadoBO)
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
        private void AsignacionId(TFlujoFase entidad, FlujoFaseBO objetoBO)
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

        private TFlujoFase MapeoEntidad(FlujoFaseBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFlujoFase entidad = new TFlujoFase();
                entidad = Mapper.Map<FlujoFaseBO, TFlujoFase>(objetoBO,
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
