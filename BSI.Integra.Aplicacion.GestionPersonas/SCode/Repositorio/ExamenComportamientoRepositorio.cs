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
    public class ExamenComportamientoRepositorio : BaseRepository<TExamenComportamiento, ExamenComportamientoBO>
    {
        #region Metodos Base
        public ExamenComportamientoRepositorio() : base()
        {
        }
        public ExamenComportamientoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ExamenComportamientoBO> GetBy(Expression<Func<TExamenComportamiento, bool>> filter)
        {
            IEnumerable<TExamenComportamiento> listado = base.GetBy(filter);
            List<ExamenComportamientoBO> listadoBO = new List<ExamenComportamientoBO>();
            foreach (var itemEntidad in listado)
            {
                ExamenComportamientoBO objetoBO = Mapper.Map<TExamenComportamiento, ExamenComportamientoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ExamenComportamientoBO FirstById(int id)
        {
            try
            {
                TExamenComportamiento entidad = base.FirstById(id);
                ExamenComportamientoBO objetoBO = new ExamenComportamientoBO();
                Mapper.Map<TExamenComportamiento, ExamenComportamientoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ExamenComportamientoBO FirstBy(Expression<Func<TExamenComportamiento, bool>> filter)
        {
            try
            {
                TExamenComportamiento entidad = base.FirstBy(filter);
                ExamenComportamientoBO objetoBO = Mapper.Map<TExamenComportamiento, ExamenComportamientoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ExamenComportamientoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TExamenComportamiento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ExamenComportamientoBO> listadoBO)
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

        public bool Update(ExamenComportamientoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TExamenComportamiento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ExamenComportamientoBO> listadoBO)
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
        private void AsignacionId(TExamenComportamiento entidad, ExamenComportamientoBO objetoBO)
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

        private TExamenComportamiento MapeoEntidad(ExamenComportamientoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TExamenComportamiento entidad = new TExamenComportamiento();
                entidad = Mapper.Map<ExamenComportamientoBO, TExamenComportamiento>(objetoBO,
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
