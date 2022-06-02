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
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class WebinarAsistenciaRepositorio : BaseRepository<TWebinarAsistencia, WebinarAsistenciaBO>
    {
        #region Metodos Base
        public WebinarAsistenciaRepositorio() : base()
        {
        }
        public WebinarAsistenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WebinarAsistenciaBO> GetBy(Expression<Func<TWebinarAsistencia, bool>> filter)
        {
            IEnumerable<TWebinarAsistencia> listado = base.GetBy(filter);
            List<WebinarAsistenciaBO> listadoBO = new List<WebinarAsistenciaBO>();
            foreach (var itemEntidad in listado)
            {
                WebinarAsistenciaBO objetoBO = Mapper.Map<TWebinarAsistencia, WebinarAsistenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WebinarAsistenciaBO FirstById(int id)
        {
            try
            {
                TWebinarAsistencia entidad = base.FirstById(id);
                WebinarAsistenciaBO objetoBO = new WebinarAsistenciaBO();
                Mapper.Map<TWebinarAsistencia, WebinarAsistenciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WebinarAsistenciaBO FirstBy(Expression<Func<TWebinarAsistencia, bool>> filter)
        {
            try
            {
                TWebinarAsistencia entidad = base.FirstBy(filter);
                WebinarAsistenciaBO objetoBO = Mapper.Map<TWebinarAsistencia, WebinarAsistenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WebinarAsistenciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWebinarAsistencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WebinarAsistenciaBO> listadoBO)
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

        public bool Update(WebinarAsistenciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWebinarAsistencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WebinarAsistenciaBO> listadoBO)
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
        private void AsignacionId(TWebinarAsistencia entidad, WebinarAsistenciaBO objetoBO)
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

        private TWebinarAsistencia MapeoEntidad(WebinarAsistenciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWebinarAsistencia entidad = new TWebinarAsistencia();
                entidad = Mapper.Map<WebinarAsistenciaBO, TWebinarAsistencia>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WebinarAsistenciaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TWebinarAsistencia, bool>>> filters, Expression<Func<TWebinarAsistencia, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TWebinarAsistencia> listado = base.GetFiltered(filters, orderBy, ascending);
            List<WebinarAsistenciaBO> listadoBO = new List<WebinarAsistenciaBO>();

            foreach (var itemEntidad in listado)
            {
                WebinarAsistenciaBO objetoBO = Mapper.Map<TWebinarAsistencia, WebinarAsistenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los alumnos que deberian asistir a una sesion webinar
        /// </summary>
        /// <returns></returns>
        public List<WebinarAsistenciaDTO> Obtener(int idWebinarDetalle) {
            try
            {
                var listaWebinarAsistencia = new List<WebinarAsistenciaDTO>();
                var _query = $@"
                                SELECT Id, 
                                       IdWebinarDetalle, 
                                       IdMatriculaCabecera, 
                                       CodigoMatricula, 
                                       IdCentroCosto, 
                                       NombreCentroCosto, 
                                       ConfirmoAsistencia, 
                                       Asistio, 
                                       EstadoConfirmacion
                                FROM ope.V_ObtenerWebinarAsistencia
                                WHERE IdWebinarDetalle = @idWebinarDetalle;
                    ";
                var resultadoDB = _dapper.QueryDapper(_query, new { idWebinarDetalle });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    listaWebinarAsistencia = JsonConvert.DeserializeObject<List<WebinarAsistenciaDTO>>(resultadoDB);
                }
                return listaWebinarAsistencia;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
