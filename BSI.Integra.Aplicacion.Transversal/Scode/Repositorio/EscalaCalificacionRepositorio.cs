using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EscalaCalificacionRepositorio : BaseRepository<TEscalaCalificacion, EscalaCalificacionBO>
    {
        #region Metodos Base
        public EscalaCalificacionRepositorio() : base()
        {
        }
        public EscalaCalificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EscalaCalificacionBO> GetBy(Expression<Func<TEscalaCalificacion, bool>> filter)
        {
            IEnumerable<TEscalaCalificacion> listado = base.GetBy(filter);
            List<EscalaCalificacionBO> listadoBO = new List<EscalaCalificacionBO>();
            foreach (var itemEntidad in listado)
            {
                EscalaCalificacionBO objetoBO = Mapper.Map<TEscalaCalificacion, EscalaCalificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EscalaCalificacionBO FirstById(int id)
        {
            try
            {
                TEscalaCalificacion entidad = base.FirstById(id);
                EscalaCalificacionBO objetoBO = new EscalaCalificacionBO();
                Mapper.Map<TEscalaCalificacion, EscalaCalificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EscalaCalificacionBO FirstBy(Expression<Func<TEscalaCalificacion, bool>> filter)
        {
            try
            {
                TEscalaCalificacion entidad = base.FirstBy(filter);
                EscalaCalificacionBO objetoBO = Mapper.Map<TEscalaCalificacion, EscalaCalificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EscalaCalificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEscalaCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EscalaCalificacionBO> listadoBO)
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

        public bool Update(EscalaCalificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEscalaCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EscalaCalificacionBO> listadoBO)
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
        private void AsignacionId(TEscalaCalificacion entidad, EscalaCalificacionBO objetoBO)
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

        private TEscalaCalificacion MapeoEntidad(EscalaCalificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEscalaCalificacion entidad = new TEscalaCalificacion();
                entidad = Mapper.Map<EscalaCalificacionBO, TEscalaCalificacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListadoDetalle != null && objetoBO.ListadoDetalle.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListadoDetalle)
                    {
                        TEscalaCalificacionDetalle entidadHijo = new TEscalaCalificacionDetalle();
                        entidadHijo = Mapper.Map<EscalaCalificacionDetalleBO, TEscalaCalificacionDetalle>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TEscalaCalificacionDetalle.Add(entidadHijo);

                        //mapea al hijo interno
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        public IEnumerable<EscalaCalificacionBO> ObtenerTodo()
        {
            IEnumerable<TEscalaCalificacion> listado = base.GetAll();
            List<EscalaCalificacionBO> listadoBO = new List<EscalaCalificacionBO>();
            foreach (var itemEntidad in listado)
            {
                EscalaCalificacionBO objetoBO = Mapper.Map<TEscalaCalificacion, EscalaCalificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public IEnumerable<ComboGenericoDTO> ObtenerCombo()
        {
            return GetAll().Select(s => new ComboGenericoDTO() { Id = s.Id, Nombre = s.Nombre });
        }
    }
}
