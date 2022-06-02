using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class WebinarCategoriaConfirmacionAsistenciaRepositorio : BaseRepository<TWebinarCategoriaConfirmacionAsistencia, WebinarCategoriaConfirmacionAsistenciaBO>
    {
        #region Metodos Base
        public WebinarCategoriaConfirmacionAsistenciaRepositorio() : base()
        {
        }
        public WebinarCategoriaConfirmacionAsistenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WebinarCategoriaConfirmacionAsistenciaBO> GetBy(Expression<Func<TWebinarCategoriaConfirmacionAsistencia, bool>> filter)
        {
            IEnumerable<TWebinarCategoriaConfirmacionAsistencia> listado = base.GetBy(filter);
            List<WebinarCategoriaConfirmacionAsistenciaBO> listadoBO = new List<WebinarCategoriaConfirmacionAsistenciaBO>();
            foreach (var itemEntidad in listado)
            {
                WebinarCategoriaConfirmacionAsistenciaBO objetoBO = Mapper.Map<TWebinarCategoriaConfirmacionAsistencia, WebinarCategoriaConfirmacionAsistenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WebinarCategoriaConfirmacionAsistenciaBO FirstById(int id)
        {
            try
            {
                TWebinarCategoriaConfirmacionAsistencia entidad = base.FirstById(id);
                WebinarCategoriaConfirmacionAsistenciaBO objetoBO = new WebinarCategoriaConfirmacionAsistenciaBO();
                Mapper.Map<TWebinarCategoriaConfirmacionAsistencia, WebinarCategoriaConfirmacionAsistenciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WebinarCategoriaConfirmacionAsistenciaBO FirstBy(Expression<Func<TWebinarCategoriaConfirmacionAsistencia, bool>> filter)
        {
            try
            {
                TWebinarCategoriaConfirmacionAsistencia entidad = base.FirstBy(filter);
                WebinarCategoriaConfirmacionAsistenciaBO objetoBO = Mapper.Map<TWebinarCategoriaConfirmacionAsistencia, WebinarCategoriaConfirmacionAsistenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WebinarCategoriaConfirmacionAsistenciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWebinarCategoriaConfirmacionAsistencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WebinarCategoriaConfirmacionAsistenciaBO> listadoBO)
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

        public bool Update(WebinarCategoriaConfirmacionAsistenciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWebinarCategoriaConfirmacionAsistencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WebinarCategoriaConfirmacionAsistenciaBO> listadoBO)
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
        private void AsignacionId(TWebinarCategoriaConfirmacionAsistencia entidad, WebinarCategoriaConfirmacionAsistenciaBO objetoBO)
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

        private TWebinarCategoriaConfirmacionAsistencia MapeoEntidad(WebinarCategoriaConfirmacionAsistenciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWebinarCategoriaConfirmacionAsistencia entidad = new TWebinarCategoriaConfirmacionAsistencia();
                entidad = Mapper.Map<WebinarCategoriaConfirmacionAsistenciaBO, TWebinarCategoriaConfirmacionAsistencia>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WebinarCategoriaConfirmacionAsistenciaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TWebinarCategoriaConfirmacionAsistencia, bool>>> filters, Expression<Func<TWebinarCategoriaConfirmacionAsistencia, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TWebinarCategoriaConfirmacionAsistencia> listado = base.GetFiltered(filters, orderBy, ascending);
            List<WebinarCategoriaConfirmacionAsistenciaBO> listadoBO = new List<WebinarCategoriaConfirmacionAsistenciaBO>();

            foreach (var itemEntidad in listado)
            {
                WebinarCategoriaConfirmacionAsistenciaBO objetoBO = Mapper.Map<TWebinarCategoriaConfirmacionAsistencia, WebinarCategoriaConfirmacionAsistenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
             
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
