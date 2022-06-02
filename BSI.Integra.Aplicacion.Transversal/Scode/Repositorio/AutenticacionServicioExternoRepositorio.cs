using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class AutenticacionServicioExternoRepositorio : BaseRepository<TAutenticacionServicioExterno, AutenticacionServicioExternoBO>
    {
        #region Metodos Base
        public AutenticacionServicioExternoRepositorio() : base()
        {
        }
        public AutenticacionServicioExternoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AutenticacionServicioExternoBO> GetBy(Expression<Func<TAutenticacionServicioExterno, bool>> filter)
        {
            IEnumerable<TAutenticacionServicioExterno> listado = base.GetBy(filter);
            List<AutenticacionServicioExternoBO> listadoBO = new List<AutenticacionServicioExternoBO>();
            foreach (var itemEntidad in listado)
            {
                AutenticacionServicioExternoBO objetoBO = Mapper.Map<TAutenticacionServicioExterno, AutenticacionServicioExternoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AutenticacionServicioExternoBO FirstById(int id)
        {
            try
            {
                TAutenticacionServicioExterno entidad = base.FirstById(id);
                AutenticacionServicioExternoBO objetoBO = new AutenticacionServicioExternoBO();
                Mapper.Map<TAutenticacionServicioExterno, AutenticacionServicioExternoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AutenticacionServicioExternoBO FirstBy(Expression<Func<TAutenticacionServicioExterno, bool>> filter)
        {
            try
            {
                TAutenticacionServicioExterno entidad = base.FirstBy(filter);
                AutenticacionServicioExternoBO objetoBO = Mapper.Map<TAutenticacionServicioExterno, AutenticacionServicioExternoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AutenticacionServicioExternoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAutenticacionServicioExterno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AutenticacionServicioExternoBO> listadoBO)
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

        public bool Update(AutenticacionServicioExternoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAutenticacionServicioExterno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AutenticacionServicioExternoBO> listadoBO)
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
        private void AsignacionId(TAutenticacionServicioExterno entidad, AutenticacionServicioExternoBO objetoBO)
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

        private TAutenticacionServicioExterno MapeoEntidad(AutenticacionServicioExternoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAutenticacionServicioExterno entidad = new TAutenticacionServicioExterno();
                entidad = Mapper.Map<AutenticacionServicioExternoBO, TAutenticacionServicioExterno>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AutenticacionServicioExternoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TAutenticacionServicioExterno, bool>>> filters, Expression<Func<TAutenticacionServicioExterno, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TAutenticacionServicioExterno> listado = base.GetFiltered(filters, orderBy, ascending);
            List<AutenticacionServicioExternoBO> listadoBO = new List<AutenticacionServicioExternoBO>();

            foreach (var itemEntidad in listado)
            {
                AutenticacionServicioExternoBO objetoBO = Mapper.Map<TAutenticacionServicioExterno, AutenticacionServicioExternoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
