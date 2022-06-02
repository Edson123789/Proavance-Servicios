using AutoMapper;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class ExamenConfigurarResultadoRepositorio : BaseRepository<TExamenConfigurarResultado, ExamenConfigurarResultadoBO>
    {
        #region Metodos Base
        public ExamenConfigurarResultadoRepositorio() : base()
        {
        }
        public ExamenConfigurarResultadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ExamenConfigurarResultadoBO> GetBy(Expression<Func<TExamenConfigurarResultado, bool>> filter)
        {
            IEnumerable<TExamenConfigurarResultado> listado = base.GetBy(filter);
            List<ExamenConfigurarResultadoBO> listadoBO = new List<ExamenConfigurarResultadoBO>();
            foreach (var itemEntidad in listado)
            {
                ExamenConfigurarResultadoBO objetoBO = Mapper.Map<TExamenConfigurarResultado, ExamenConfigurarResultadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ExamenConfigurarResultadoBO FirstById(int id)
        {
            try
            {
                TExamenConfigurarResultado entidad = base.FirstById(id);
                ExamenConfigurarResultadoBO objetoBO = new ExamenConfigurarResultadoBO();
                Mapper.Map<TExamenConfigurarResultado, ExamenConfigurarResultadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ExamenConfigurarResultadoBO FirstBy(Expression<Func<TExamenConfigurarResultado, bool>> filter)
        {
            try
            {
                TExamenConfigurarResultado entidad = base.FirstBy(filter);
                ExamenConfigurarResultadoBO objetoBO = Mapper.Map<TExamenConfigurarResultado, ExamenConfigurarResultadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ExamenConfigurarResultadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TExamenConfigurarResultado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ExamenConfigurarResultadoBO> listadoBO)
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

        public bool Update(ExamenConfigurarResultadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TExamenConfigurarResultado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ExamenConfigurarResultadoBO> listadoBO)
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
        private void AsignacionId(TExamenConfigurarResultado entidad, ExamenConfigurarResultadoBO objetoBO)
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

        private TExamenConfigurarResultado MapeoEntidad(ExamenConfigurarResultadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TExamenConfigurarResultado entidad = new TExamenConfigurarResultado();
                entidad = Mapper.Map<ExamenConfigurarResultadoBO, TExamenConfigurarResultado>(objetoBO,
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
