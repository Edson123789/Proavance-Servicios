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
    public class CursoMoodleRepositorio : BaseRepository<TCursoMoodle, CursoMoodleBO>
    {
        #region Metodos Base
        public CursoMoodleRepositorio() : base()
        {
        }
        public CursoMoodleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CursoMoodleBO> GetBy(Expression<Func<TCursoMoodle, bool>> filter)
        {
            IEnumerable<TCursoMoodle> listado = base.GetBy(filter);
            List<CursoMoodleBO> listadoBO = new List<CursoMoodleBO>();
            foreach (var itemEntidad in listado)
            {
                CursoMoodleBO objetoBO = Mapper.Map<TCursoMoodle, CursoMoodleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CursoMoodleBO FirstById(int id)
        {
            try
            {
                TCursoMoodle entidad = base.FirstById(id);
                CursoMoodleBO objetoBO = new CursoMoodleBO();
                Mapper.Map<TCursoMoodle, CursoMoodleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CursoMoodleBO FirstBy(Expression<Func<TCursoMoodle, bool>> filter)
        {
            try
            {
                TCursoMoodle entidad = base.FirstBy(filter);
                CursoMoodleBO objetoBO = Mapper.Map<TCursoMoodle, CursoMoodleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CursoMoodleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCursoMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CursoMoodleBO> listadoBO)
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

        public bool Update(CursoMoodleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCursoMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CursoMoodleBO> listadoBO)
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
        private void AsignacionId(TCursoMoodle entidad, CursoMoodleBO objetoBO)
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

        private TCursoMoodle MapeoEntidad(CursoMoodleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCursoMoodle entidad = new TCursoMoodle();
                entidad = Mapper.Map<CursoMoodleBO, TCursoMoodle>(objetoBO,
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
