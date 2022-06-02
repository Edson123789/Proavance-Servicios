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
    public class CategoriaTipoMoodleRepositorio : BaseRepository<TCategoriaTipoMoodle, CategoriaTipoMoodleBO>
    {
        #region Metodos Base
        public CategoriaTipoMoodleRepositorio() : base()
        {
        }
        public CategoriaTipoMoodleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CategoriaTipoMoodleBO> GetBy(Expression<Func<TCategoriaTipoMoodle, bool>> filter)
        {
            IEnumerable<TCategoriaTipoMoodle> listado = base.GetBy(filter);
            List<CategoriaTipoMoodleBO> listadoBO = new List<CategoriaTipoMoodleBO>();
            foreach (var itemEntidad in listado)
            {
                CategoriaTipoMoodleBO objetoBO = Mapper.Map<TCategoriaTipoMoodle, CategoriaTipoMoodleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CategoriaTipoMoodleBO FirstById(int id)
        {
            try
            {
                TCategoriaTipoMoodle entidad = base.FirstById(id);
                CategoriaTipoMoodleBO objetoBO = new CategoriaTipoMoodleBO();
                Mapper.Map<TCategoriaTipoMoodle, CategoriaTipoMoodleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CategoriaTipoMoodleBO FirstBy(Expression<Func<TCategoriaTipoMoodle, bool>> filter)
        {
            try
            {
                TCategoriaTipoMoodle entidad = base.FirstBy(filter);
                CategoriaTipoMoodleBO objetoBO = Mapper.Map<TCategoriaTipoMoodle, CategoriaTipoMoodleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CategoriaTipoMoodleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCategoriaTipoMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CategoriaTipoMoodleBO> listadoBO)
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

        public bool Update(CategoriaTipoMoodleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCategoriaTipoMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CategoriaTipoMoodleBO> listadoBO)
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
        private void AsignacionId(TCategoriaTipoMoodle entidad, CategoriaTipoMoodleBO objetoBO)
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

        private TCategoriaTipoMoodle MapeoEntidad(CategoriaTipoMoodleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCategoriaTipoMoodle entidad = new TCategoriaTipoMoodle();
                entidad = Mapper.Map<CategoriaTipoMoodleBO, TCategoriaTipoMoodle>(objetoBO,
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
