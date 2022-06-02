using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ConfirmacionWebinarRepositorio : BaseRepository<TConfirmacionWebinar, ConfirmacionWebinarBO>
    {
        #region Metodos Base
        public ConfirmacionWebinarRepositorio() : base()
        {
        }
        public ConfirmacionWebinarRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfirmacionWebinarBO> GetBy(Expression<Func<TConfirmacionWebinar, bool>> filter)
        {
            IEnumerable<TConfirmacionWebinar> listado = base.GetBy(filter);
            List<ConfirmacionWebinarBO> listadoBO = new List<ConfirmacionWebinarBO>();
            foreach (var itemEntidad in listado)
            {
                ConfirmacionWebinarBO objetoBO = Mapper.Map<TConfirmacionWebinar, ConfirmacionWebinarBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfirmacionWebinarBO FirstById(int id)
        {
            try
            {
                TConfirmacionWebinar entidad = base.FirstById(id);
                ConfirmacionWebinarBO objetoBO = new ConfirmacionWebinarBO();
                Mapper.Map<TConfirmacionWebinar, ConfirmacionWebinarBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfirmacionWebinarBO FirstBy(Expression<Func<TConfirmacionWebinar, bool>> filter)
        {
            try
            {
                TConfirmacionWebinar entidad = base.FirstBy(filter);
                ConfirmacionWebinarBO objetoBO = Mapper.Map<TConfirmacionWebinar, ConfirmacionWebinarBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfirmacionWebinarBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfirmacionWebinar entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfirmacionWebinarBO> listadoBO)
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

        public bool Update(ConfirmacionWebinarBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfirmacionWebinar entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfirmacionWebinarBO> listadoBO)
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
        private void AsignacionId(TConfirmacionWebinar entidad, ConfirmacionWebinarBO objetoBO)
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

        private TConfirmacionWebinar MapeoEntidad(ConfirmacionWebinarBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfirmacionWebinar entidad = new TConfirmacionWebinar();
                entidad = Mapper.Map<ConfirmacionWebinarBO, TConfirmacionWebinar>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfirmacionWebinarBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfirmacionWebinar, bool>>> filters, Expression<Func<TConfirmacionWebinar, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfirmacionWebinar> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfirmacionWebinarBO> listadoBO = new List<ConfirmacionWebinarBO>();

            foreach (var itemEntidad in listado)
            {
                ConfirmacionWebinarBO objetoBO = Mapper.Map<TConfirmacionWebinar, ConfirmacionWebinarBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
