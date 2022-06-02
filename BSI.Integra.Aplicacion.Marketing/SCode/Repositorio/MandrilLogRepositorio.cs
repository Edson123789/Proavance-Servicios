using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class MandrilLogRepositorio : BaseRepository<TMandrilLog, MandrilLogBO>
    {
        #region Metodos Base
        public MandrilLogRepositorio() : base()
        {
        }
        public MandrilLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MandrilLogBO> GetBy(Expression<Func<TMandrilLog, bool>> filter)
        {
            IEnumerable<TMandrilLog> listado = base.GetBy(filter);
            List<MandrilLogBO> listadoBO = new List<MandrilLogBO>();
            foreach (var itemEntidad in listado)
            {
                MandrilLogBO objetoBO = Mapper.Map<TMandrilLog, MandrilLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MandrilLogBO FirstById(int id)
        {
            try
            {
                TMandrilLog entidad = base.FirstById(id);
                MandrilLogBO objetoBO = new MandrilLogBO();
                Mapper.Map<TMandrilLog, MandrilLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MandrilLogBO FirstBy(Expression<Func<TMandrilLog, bool>> filter)
        {
            try
            {
                TMandrilLog entidad = base.FirstBy(filter);
                MandrilLogBO objetoBO = Mapper.Map<TMandrilLog, MandrilLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MandrilLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMandrilLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MandrilLogBO> listadoBO)
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

        public bool Update(MandrilLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMandrilLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MandrilLogBO> listadoBO)
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
        private void AsignacionId(TMandrilLog entidad, MandrilLogBO objetoBO)
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

        private TMandrilLog MapeoEntidad(MandrilLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMandrilLog entidad = new TMandrilLog();
                entidad = Mapper.Map<MandrilLogBO, TMandrilLog>(objetoBO,
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
