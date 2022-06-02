using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class InteraccionRepositorio : BaseRepository<TInteraccion, InteraccionBO>
    {
        #region Metodos Base
        public InteraccionRepositorio() : base()
        {
        }
        public InteraccionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<InteraccionBO> GetBy(Expression<Func<TInteraccion, bool>> filter)
        {
            IEnumerable<TInteraccion> listado = base.GetBy(filter);
            List<InteraccionBO> listadoBO = new List<InteraccionBO>();
            foreach (var itemEntidad in listado)
            {
                InteraccionBO objetoBO = Mapper.Map<TInteraccion, InteraccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public InteraccionBO FirstById(int id)
        {
            try
            {
                TInteraccion entidad = base.FirstById(id);
                InteraccionBO objetoBO = new InteraccionBO();
                Mapper.Map<TInteraccion, InteraccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public InteraccionBO FirstBy(Expression<Func<TInteraccion, bool>> filter)
        {
            try
            {
                TInteraccion entidad = base.FirstBy(filter);
                InteraccionBO objetoBO = Mapper.Map<TInteraccion, InteraccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(InteraccionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TInteraccion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<InteraccionBO> listadoBO)
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

        public bool Update(InteraccionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TInteraccion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<InteraccionBO> listadoBO)
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
        private void AsignacionId(TInteraccion entidad, InteraccionBO objetoBO)
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

        private TInteraccion MapeoEntidad(InteraccionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TInteraccion entidad = new TInteraccion();
                entidad = Mapper.Map<InteraccionBO, TInteraccion>(objetoBO,
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
