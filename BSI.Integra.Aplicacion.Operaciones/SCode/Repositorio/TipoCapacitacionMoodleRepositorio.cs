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
    public class TipoCapacitacionMoodleRepositorio : BaseRepository<TTipoCapacitacionMoodle, TipoCapacitacionMoodleBO>
    {
        #region Metodos Base
        public TipoCapacitacionMoodleRepositorio() : base()
        {
        }
        public TipoCapacitacionMoodleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoCapacitacionMoodleBO> GetBy(Expression<Func<TTipoCapacitacionMoodle, bool>> filter)
        {
            IEnumerable<TTipoCapacitacionMoodle> listado = base.GetBy(filter);
            List<TipoCapacitacionMoodleBO> listadoBO = new List<TipoCapacitacionMoodleBO>();
            foreach (var itemEntidad in listado)
            {
                TipoCapacitacionMoodleBO objetoBO = Mapper.Map<TTipoCapacitacionMoodle, TipoCapacitacionMoodleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoCapacitacionMoodleBO FirstById(int id)
        {
            try
            {
                TTipoCapacitacionMoodle entidad = base.FirstById(id);
                TipoCapacitacionMoodleBO objetoBO = new TipoCapacitacionMoodleBO();
                Mapper.Map<TTipoCapacitacionMoodle, TipoCapacitacionMoodleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoCapacitacionMoodleBO FirstBy(Expression<Func<TTipoCapacitacionMoodle, bool>> filter)
        {
            try
            {
                TTipoCapacitacionMoodle entidad = base.FirstBy(filter);
                TipoCapacitacionMoodleBO objetoBO = Mapper.Map<TTipoCapacitacionMoodle, TipoCapacitacionMoodleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoCapacitacionMoodleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoCapacitacionMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoCapacitacionMoodleBO> listadoBO)
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

        public bool Update(TipoCapacitacionMoodleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoCapacitacionMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoCapacitacionMoodleBO> listadoBO)
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
        private void AsignacionId(TTipoCapacitacionMoodle entidad, TipoCapacitacionMoodleBO objetoBO)
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

        private TTipoCapacitacionMoodle MapeoEntidad(TipoCapacitacionMoodleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoCapacitacionMoodle entidad = new TTipoCapacitacionMoodle();
                entidad = Mapper.Map<TipoCapacitacionMoodleBO, TTipoCapacitacionMoodle>(objetoBO,
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

