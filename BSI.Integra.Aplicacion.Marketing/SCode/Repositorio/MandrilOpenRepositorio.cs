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
    public class MandrilOpenRepositorio : BaseRepository<TMandrilOpen, MandrilOpenBO>
    {
        #region Metodos Base
        public MandrilOpenRepositorio() : base()
        {
        }
        public MandrilOpenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MandrilOpenBO> GetBy(Expression<Func<TMandrilOpen, bool>> filter)
        {
            IEnumerable<TMandrilOpen> listado = base.GetBy(filter);
            List<MandrilOpenBO> listadoBO = new List<MandrilOpenBO>();
            foreach (var itemEntidad in listado)
            {
                MandrilOpenBO objetoBO = Mapper.Map<TMandrilOpen, MandrilOpenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MandrilOpenBO FirstById(int id)
        {
            try
            {
                TMandrilOpen entidad = base.FirstById(id);
                MandrilOpenBO objetoBO = new MandrilOpenBO();
                Mapper.Map<TMandrilOpen, MandrilOpenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MandrilOpenBO FirstBy(Expression<Func<TMandrilOpen, bool>> filter)
        {
            try
            {
                TMandrilOpen entidad = base.FirstBy(filter);
                MandrilOpenBO objetoBO = Mapper.Map<TMandrilOpen, MandrilOpenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MandrilOpenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMandrilOpen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MandrilOpenBO> listadoBO)
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

        public bool Update(MandrilOpenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMandrilOpen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MandrilOpenBO> listadoBO)
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
        private void AsignacionId(TMandrilOpen entidad, MandrilOpenBO objetoBO)
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

        private TMandrilOpen MapeoEntidad(MandrilOpenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMandrilOpen entidad = new TMandrilOpen();
                entidad = Mapper.Map<MandrilOpenBO, TMandrilOpen>(objetoBO,
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
