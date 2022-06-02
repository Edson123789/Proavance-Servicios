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
    public class MandrilClickRepositorio : BaseRepository<TMandrilClick, MandrilClickBO>
    {
        #region Metodos Base
        public MandrilClickRepositorio() : base()
        {
        }
        public MandrilClickRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MandrilClickBO> GetBy(Expression<Func<TMandrilClick, bool>> filter)
        {
            IEnumerable<TMandrilClick> listado = base.GetBy(filter);
            List<MandrilClickBO> listadoBO = new List<MandrilClickBO>();
            foreach (var itemEntidad in listado)
            {
                MandrilClickBO objetoBO = Mapper.Map<TMandrilClick, MandrilClickBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MandrilClickBO FirstById(int id)
        {
            try
            {
                TMandrilClick entidad = base.FirstById(id);
                MandrilClickBO objetoBO = new MandrilClickBO();
                Mapper.Map<TMandrilClick, MandrilClickBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MandrilClickBO FirstBy(Expression<Func<TMandrilClick, bool>> filter)
        {
            try
            {
                TMandrilClick entidad = base.FirstBy(filter);
                MandrilClickBO objetoBO = Mapper.Map<TMandrilClick, MandrilClickBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MandrilClickBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMandrilClick entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MandrilClickBO> listadoBO)
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

        public bool Update(MandrilClickBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMandrilClick entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MandrilClickBO> listadoBO)
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
        private void AsignacionId(TMandrilClick entidad, MandrilClickBO objetoBO)
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

        private TMandrilClick MapeoEntidad(MandrilClickBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMandrilClick entidad = new TMandrilClick();
                entidad = Mapper.Map<MandrilClickBO, TMandrilClick>(objetoBO,
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
