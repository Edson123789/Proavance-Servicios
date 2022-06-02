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
    public class AlumnoLogRepositorio : BaseRepository<TAlumnoLog, AlumnoLogBO>
    {
        #region Metodos Base
        public AlumnoLogRepositorio() : base()
        {
        }
        public AlumnoLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AlumnoLogBO> GetBy(Expression<Func<TAlumnoLog, bool>> filter)
        {
            IEnumerable<TAlumnoLog> listado = base.GetBy(filter);
            List<AlumnoLogBO> listadoBO = new List<AlumnoLogBO>();
            foreach (var itemEntidad in listado)
            {
                AlumnoLogBO objetoBO = Mapper.Map<TAlumnoLog, AlumnoLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AlumnoLogBO FirstById(int id)
        {
            try
            {
                TAlumnoLog entidad = base.FirstById(id);
                AlumnoLogBO objetoBO = new AlumnoLogBO();
                Mapper.Map<TAlumnoLog, AlumnoLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AlumnoLogBO FirstBy(Expression<Func<TAlumnoLog, bool>> filter)
        {
            try
            {
                TAlumnoLog entidad = base.FirstBy(filter);
                AlumnoLogBO objetoBO = Mapper.Map<TAlumnoLog, AlumnoLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AlumnoLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAlumnoLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AlumnoLogBO> listadoBO)
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

        public bool Update(AlumnoLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAlumnoLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AlumnoLogBO> listadoBO)
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
        private void AsignacionId(TAlumnoLog entidad, AlumnoLogBO objetoBO)
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

        private TAlumnoLog MapeoEntidad(AlumnoLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAlumnoLog entidad = new TAlumnoLog();
                entidad = Mapper.Map<AlumnoLogBO, TAlumnoLog>(objetoBO,
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
