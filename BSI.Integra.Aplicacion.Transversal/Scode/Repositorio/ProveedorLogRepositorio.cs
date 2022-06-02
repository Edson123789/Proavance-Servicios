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
    public class ProveedorLogRepositorio : BaseRepository<TProveedorLog, ProveedorLogBO>
    {
        #region Metodos Base
        public ProveedorLogRepositorio() : base()
        {
        }
        public ProveedorLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProveedorLogBO> GetBy(Expression<Func<TProveedorLog, bool>> filter)
        {
            IEnumerable<TProveedorLog> listado = base.GetBy(filter);
            List<ProveedorLogBO> listadoBO = new List<ProveedorLogBO>();
            foreach (var itemEntidad in listado)
            {
                ProveedorLogBO objetoBO = Mapper.Map<TProveedorLog, ProveedorLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProveedorLogBO FirstById(int id)
        {
            try
            {
                TProveedorLog entidad = base.FirstById(id);
                ProveedorLogBO objetoBO = new ProveedorLogBO();
                Mapper.Map<TProveedorLog, ProveedorLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProveedorLogBO FirstBy(Expression<Func<TProveedorLog, bool>> filter)
        {
            try
            {
                TProveedorLog entidad = base.FirstBy(filter);
                ProveedorLogBO objetoBO = Mapper.Map<TProveedorLog, ProveedorLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProveedorLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProveedorLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProveedorLogBO> listadoBO)
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

        public bool Update(ProveedorLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProveedorLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProveedorLogBO> listadoBO)
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
        private void AsignacionId(TProveedorLog entidad, ProveedorLogBO objetoBO)
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

        private TProveedorLog MapeoEntidad(ProveedorLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProveedorLog entidad = new TProveedorLog();
                entidad = Mapper.Map<ProveedorLogBO, TProveedorLog>(objetoBO,
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
