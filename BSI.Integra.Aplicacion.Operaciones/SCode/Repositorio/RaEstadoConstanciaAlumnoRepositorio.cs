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
    public class RaEstadoConstanciaAlumnoRepositorio : BaseRepository<TRaEstadoConstanciaAlumno, RaEstadoConstanciaAlumnoBO>
    {
        #region Metodos Base
        public RaEstadoConstanciaAlumnoRepositorio() : base()
        {
        }
        public RaEstadoConstanciaAlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaEstadoConstanciaAlumnoBO> GetBy(Expression<Func<TRaEstadoConstanciaAlumno, bool>> filter)
        {
            IEnumerable<TRaEstadoConstanciaAlumno> listado = base.GetBy(filter);
            List<RaEstadoConstanciaAlumnoBO> listadoBO = new List<RaEstadoConstanciaAlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                RaEstadoConstanciaAlumnoBO objetoBO = Mapper.Map<TRaEstadoConstanciaAlumno, RaEstadoConstanciaAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaEstadoConstanciaAlumnoBO FirstById(int id)
        {
            try
            {
                TRaEstadoConstanciaAlumno entidad = base.FirstById(id);
                RaEstadoConstanciaAlumnoBO objetoBO = new RaEstadoConstanciaAlumnoBO();
                Mapper.Map<TRaEstadoConstanciaAlumno, RaEstadoConstanciaAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaEstadoConstanciaAlumnoBO FirstBy(Expression<Func<TRaEstadoConstanciaAlumno, bool>> filter)
        {
            try
            {
                TRaEstadoConstanciaAlumno entidad = base.FirstBy(filter);
                RaEstadoConstanciaAlumnoBO objetoBO = Mapper.Map<TRaEstadoConstanciaAlumno, RaEstadoConstanciaAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaEstadoConstanciaAlumnoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaEstadoConstanciaAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaEstadoConstanciaAlumnoBO> listadoBO)
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

        public bool Update(RaEstadoConstanciaAlumnoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaEstadoConstanciaAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaEstadoConstanciaAlumnoBO> listadoBO)
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
        private void AsignacionId(TRaEstadoConstanciaAlumno entidad, RaEstadoConstanciaAlumnoBO objetoBO)
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

        private TRaEstadoConstanciaAlumno MapeoEntidad(RaEstadoConstanciaAlumnoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaEstadoConstanciaAlumno entidad = new TRaEstadoConstanciaAlumno();
                entidad = Mapper.Map<RaEstadoConstanciaAlumnoBO, TRaEstadoConstanciaAlumno>(objetoBO,
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
