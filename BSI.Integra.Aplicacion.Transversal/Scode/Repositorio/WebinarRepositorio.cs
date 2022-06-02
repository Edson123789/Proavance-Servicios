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
    public class WebinarRepositorio : BaseRepository<TWebinar, WebinarBO>
    {
        #region Metodos Base
        public WebinarRepositorio() : base()
        {
        }
        public WebinarRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WebinarBO> GetBy(Expression<Func<TWebinar, bool>> filter)
        {
            IEnumerable<TWebinar> listado = base.GetBy(filter);
            List<WebinarBO> listadoBO = new List<WebinarBO>();
            foreach (var itemEntidad in listado)
            {
                WebinarBO objetoBO = Mapper.Map<TWebinar, WebinarBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WebinarBO FirstById(int id)
        {
            try
            {
                TWebinar entidad = base.FirstById(id);
                WebinarBO objetoBO = new WebinarBO();
                Mapper.Map<TWebinar, WebinarBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WebinarBO FirstBy(Expression<Func<TWebinar, bool>> filter)
        {
            try
            {
                TWebinar entidad = base.FirstBy(filter);
                WebinarBO objetoBO = Mapper.Map<TWebinar, WebinarBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WebinarBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWebinar entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WebinarBO> listadoBO)
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

        public bool Update(WebinarBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWebinar entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WebinarBO> listadoBO)
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
        private void AsignacionId(TWebinar entidad, WebinarBO objetoBO)
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

        private TWebinar MapeoEntidad(WebinarBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWebinar entidad = new TWebinar();
                entidad = Mapper.Map<WebinarBO, TWebinar>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaWebinarCentroCosto != null && objetoBO.ListaWebinarCentroCosto.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaWebinarCentroCosto)
                    {
                        TWebinarCentroCosto entidadHijo = new TWebinarCentroCosto();
                        entidadHijo = Mapper.Map<WebinarCentroCostoBO, TWebinarCentroCosto>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TWebinarCentroCosto.Add(entidadHijo);
                    }
                }
                if (objetoBO.ListaWebinarDetalle != null && objetoBO.ListaWebinarDetalle.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaWebinarDetalle)
                    {
                        TWebinarDetalle entidadHijo = new TWebinarDetalle();
                        entidadHijo = Mapper.Map<WebinarDetalleBO, TWebinarDetalle>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TWebinarDetalle.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WebinarBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TWebinar, bool>>> filters, Expression<Func<TWebinar, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TWebinar> listado = base.GetFiltered(filters, orderBy, ascending);
            List<WebinarBO> listadoBO = new List<WebinarBO>();

            foreach (var itemEntidad in listado)
            {
                WebinarBO objetoBO = Mapper.Map<TWebinar, WebinarBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene para filtro
        /// </summary>
        /// <returns></returns>
        public List<WebinarBO> Obtener()
        {
            try
            {
                //return this.GetBy(x => x.Estado, x => new WebinarDTO
                //{
                //    Id = x.Id,
                //    Nombre = x.Nombre,
                //    NombreCursoCompleto = x.NombreCursoCompleto,
                //    IdExpositor = x.IdExpositor,
                //    IdWebinarCategoriaConfirmacionAsistencia = x.IdWebinarCategoriaConfirmacionAsistencia,
                //    IdPersonal = x.IdPersonal,
                //    IdFrecuencia = x.IdFrecuencia,
                //    Usuario = x.Usuario,
                //    Clave = x.Clave,
                //    LinkAulaVirtual = x.LinkAulaVirtual,
                //    Activo = x.Activo
                //}).ToList();
                return this.GetBy(x => x.Estado).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
