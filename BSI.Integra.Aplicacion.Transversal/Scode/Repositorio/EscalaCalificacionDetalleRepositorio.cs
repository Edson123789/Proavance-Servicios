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
    public class EscalaCalificacionDetalleRepositorio : BaseRepository<TEscalaCalificacionDetalle, EscalaCalificacionDetalleBO>
    {
        #region Metodos Base
        public EscalaCalificacionDetalleRepositorio() : base()
        {
        }
        public EscalaCalificacionDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EscalaCalificacionDetalleBO> GetBy(Expression<Func<TEscalaCalificacionDetalle, bool>> filter)
        {
            IEnumerable<TEscalaCalificacionDetalle> listado = base.GetBy(filter);
            List<EscalaCalificacionDetalleBO> listadoBO = new List<EscalaCalificacionDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                EscalaCalificacionDetalleBO objetoBO = Mapper.Map<TEscalaCalificacionDetalle, EscalaCalificacionDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EscalaCalificacionDetalleBO FirstById(int id)
        {
            try
            {
                TEscalaCalificacionDetalle entidad = base.FirstById(id);
                EscalaCalificacionDetalleBO objetoBO = new EscalaCalificacionDetalleBO();
                Mapper.Map<TEscalaCalificacionDetalle, EscalaCalificacionDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EscalaCalificacionDetalleBO FirstBy(Expression<Func<TEscalaCalificacionDetalle, bool>> filter)
        {
            try
            {
                TEscalaCalificacionDetalle entidad = base.FirstBy(filter);
                EscalaCalificacionDetalleBO objetoBO = Mapper.Map<TEscalaCalificacionDetalle, EscalaCalificacionDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EscalaCalificacionDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEscalaCalificacionDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EscalaCalificacionDetalleBO> listadoBO)
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

        public bool Update(EscalaCalificacionDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEscalaCalificacionDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EscalaCalificacionDetalleBO> listadoBO)
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
        private void AsignacionId(TEscalaCalificacionDetalle entidad, EscalaCalificacionDetalleBO objetoBO)
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

        private TEscalaCalificacionDetalle MapeoEntidad(EscalaCalificacionDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEscalaCalificacionDetalle entidad = new TEscalaCalificacionDetalle();
                entidad = Mapper.Map<EscalaCalificacionDetalleBO, TEscalaCalificacionDetalle>(objetoBO,
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
