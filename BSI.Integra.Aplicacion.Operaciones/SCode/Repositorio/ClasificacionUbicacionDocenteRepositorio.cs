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
    public class ClasificacionUbicacionDocenteRepositorio : BaseRepository<TClasificacionUbicacionDocente, ClasificacionUbicacionDocenteBO>
    {
        #region Metodos Base
        public ClasificacionUbicacionDocenteRepositorio() : base()
        {
        }
        public ClasificacionUbicacionDocenteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ClasificacionUbicacionDocenteBO> GetBy(Expression<Func<TClasificacionUbicacionDocente, bool>> filter)
        {
            IEnumerable<TClasificacionUbicacionDocente> listado = base.GetBy(filter);
            List<ClasificacionUbicacionDocenteBO> listadoBO = new List<ClasificacionUbicacionDocenteBO>();
            foreach (var itemEntidad in listado)
            {
                ClasificacionUbicacionDocenteBO objetoBO = Mapper.Map<TClasificacionUbicacionDocente, ClasificacionUbicacionDocenteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ClasificacionUbicacionDocenteBO FirstById(int id)
        {
            try
            {
                TClasificacionUbicacionDocente entidad = base.FirstById(id);
                ClasificacionUbicacionDocenteBO objetoBO = new ClasificacionUbicacionDocenteBO();
                Mapper.Map<TClasificacionUbicacionDocente, ClasificacionUbicacionDocenteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ClasificacionUbicacionDocenteBO FirstBy(Expression<Func<TClasificacionUbicacionDocente, bool>> filter)
        {
            try
            {
                TClasificacionUbicacionDocente entidad = base.FirstBy(filter);
                ClasificacionUbicacionDocenteBO objetoBO = Mapper.Map<TClasificacionUbicacionDocente, ClasificacionUbicacionDocenteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ClasificacionUbicacionDocenteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TClasificacionUbicacionDocente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ClasificacionUbicacionDocenteBO> listadoBO)
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

        public bool Update(ClasificacionUbicacionDocenteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TClasificacionUbicacionDocente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ClasificacionUbicacionDocenteBO> listadoBO)
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
        private void AsignacionId(TClasificacionUbicacionDocente entidad, ClasificacionUbicacionDocenteBO objetoBO)
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

        private TClasificacionUbicacionDocente MapeoEntidad(ClasificacionUbicacionDocenteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TClasificacionUbicacionDocente entidad = new TClasificacionUbicacionDocente();
                entidad = Mapper.Map<ClasificacionUbicacionDocenteBO, TClasificacionUbicacionDocente>(objetoBO,
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
