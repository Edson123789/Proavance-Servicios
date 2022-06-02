using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class SolicitudOperacionesAccesoTemporalDetalleRepositorio : BaseRepository<TSolicitudOperacionesAccesoTemporalDetalle, SolicitudOperacionesAccesoTemporalDetalleBO>
    {
        #region Metodos Base
        public SolicitudOperacionesAccesoTemporalDetalleRepositorio() : base()
        {
        }
        public SolicitudOperacionesAccesoTemporalDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SolicitudOperacionesAccesoTemporalDetalleBO> GetBy(Expression<Func<TSolicitudOperacionesAccesoTemporalDetalle, bool>> filter)
        {
            IEnumerable<TSolicitudOperacionesAccesoTemporalDetalle> listado = base.GetBy(filter);
            List<SolicitudOperacionesAccesoTemporalDetalleBO> listadoBO = new List<SolicitudOperacionesAccesoTemporalDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                SolicitudOperacionesAccesoTemporalDetalleBO objetoBO = Mapper.Map<TSolicitudOperacionesAccesoTemporalDetalle, SolicitudOperacionesAccesoTemporalDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SolicitudOperacionesAccesoTemporalDetalleBO FirstById(int id)
        {
            try
            {
                TSolicitudOperacionesAccesoTemporalDetalle entidad = base.FirstById(id);
                SolicitudOperacionesAccesoTemporalDetalleBO objetoBO = new SolicitudOperacionesAccesoTemporalDetalleBO();
                Mapper.Map<TSolicitudOperacionesAccesoTemporalDetalle, SolicitudOperacionesAccesoTemporalDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SolicitudOperacionesAccesoTemporalDetalleBO FirstBy(Expression<Func<TSolicitudOperacionesAccesoTemporalDetalle, bool>> filter)
        {
            try
            {
                TSolicitudOperacionesAccesoTemporalDetalle entidad = base.FirstBy(filter);
                SolicitudOperacionesAccesoTemporalDetalleBO objetoBO = Mapper.Map<TSolicitudOperacionesAccesoTemporalDetalle, SolicitudOperacionesAccesoTemporalDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SolicitudOperacionesAccesoTemporalDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSolicitudOperacionesAccesoTemporalDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SolicitudOperacionesAccesoTemporalDetalleBO> listadoBO)
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

        public bool Update(SolicitudOperacionesAccesoTemporalDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSolicitudOperacionesAccesoTemporalDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SolicitudOperacionesAccesoTemporalDetalleBO> listadoBO)
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
        private void AsignacionId(TSolicitudOperacionesAccesoTemporalDetalle entidad, SolicitudOperacionesAccesoTemporalDetalleBO objetoBO)
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

        private TSolicitudOperacionesAccesoTemporalDetalle MapeoEntidad(SolicitudOperacionesAccesoTemporalDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSolicitudOperacionesAccesoTemporalDetalle entidad = new TSolicitudOperacionesAccesoTemporalDetalle();
                entidad = Mapper.Map<SolicitudOperacionesAccesoTemporalDetalleBO, TSolicitudOperacionesAccesoTemporalDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SolicitudOperacionesAccesoTemporalDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TSolicitudOperacionesAccesoTemporalDetalle, bool>>> filters, Expression<Func<TSolicitudOperacionesAccesoTemporalDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TSolicitudOperacionesAccesoTemporalDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<SolicitudOperacionesAccesoTemporalDetalleBO> listadoBO = new List<SolicitudOperacionesAccesoTemporalDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                SolicitudOperacionesAccesoTemporalDetalleBO objetoBO = Mapper.Map<TSolicitudOperacionesAccesoTemporalDetalle, SolicitudOperacionesAccesoTemporalDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
