using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoSolicitudOperacionesRepositorio : BaseRepository<TTipoSolicitudOperaciones, TipoSolicitudOperacionesBO>
    {
        #region Metodos Base
        public TipoSolicitudOperacionesRepositorio() : base()
        {
        }
        public TipoSolicitudOperacionesRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoSolicitudOperacionesBO> GetBy(Expression<Func<TTipoSolicitudOperaciones, bool>> filter)
        {
            IEnumerable<TTipoSolicitudOperaciones> listado = base.GetBy(filter);
            List<TipoSolicitudOperacionesBO> listadoBO = new List<TipoSolicitudOperacionesBO>();
            foreach (var itemEntidad in listado)
            {
                TipoSolicitudOperacionesBO objetoBO = Mapper.Map<TTipoSolicitudOperaciones, TipoSolicitudOperacionesBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoSolicitudOperacionesBO FirstById(int id)
        {
            try
            {
                TTipoSolicitudOperaciones entidad = base.FirstById(id);
                TipoSolicitudOperacionesBO objetoBO = new TipoSolicitudOperacionesBO();
                Mapper.Map<TTipoSolicitudOperaciones, TipoSolicitudOperacionesBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoSolicitudOperacionesBO FirstBy(Expression<Func<TTipoSolicitudOperaciones, bool>> filter)
        {
            try
            {
                TTipoSolicitudOperaciones entidad = base.FirstBy(filter);
                TipoSolicitudOperacionesBO objetoBO = Mapper.Map<TTipoSolicitudOperaciones, TipoSolicitudOperacionesBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoSolicitudOperacionesBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoSolicitudOperaciones entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoSolicitudOperacionesBO> listadoBO)
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

        public bool Update(TipoSolicitudOperacionesBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoSolicitudOperaciones entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoSolicitudOperacionesBO> listadoBO)
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
        private void AsignacionId(TTipoSolicitudOperaciones entidad, TipoSolicitudOperacionesBO objetoBO)
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

        private TTipoSolicitudOperaciones MapeoEntidad(TipoSolicitudOperacionesBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoSolicitudOperaciones entidad = new TTipoSolicitudOperaciones();
                entidad = Mapper.Map<TipoSolicitudOperacionesBO, TTipoSolicitudOperaciones>(objetoBO,
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
