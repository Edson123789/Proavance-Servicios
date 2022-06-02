using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaHistorialCambioAlumnoRepositorio : BaseRepository<TRaHistorialCambioAlumno, RaHistorialCambioAlumnoBO>
    {
        #region Metodos Base
        public RaHistorialCambioAlumnoRepositorio() : base()
        {
        }
        public RaHistorialCambioAlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaHistorialCambioAlumnoBO> GetBy(Expression<Func<TRaHistorialCambioAlumno, bool>> filter)
        {
            IEnumerable<TRaHistorialCambioAlumno> listado = base.GetBy(filter);
            List<RaHistorialCambioAlumnoBO> listadoBO = new List<RaHistorialCambioAlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                RaHistorialCambioAlumnoBO objetoBO = Mapper.Map<TRaHistorialCambioAlumno, RaHistorialCambioAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaHistorialCambioAlumnoBO FirstById(int id)
        {
            try
            {
                TRaHistorialCambioAlumno entidad = base.FirstById(id);
                RaHistorialCambioAlumnoBO objetoBO = new RaHistorialCambioAlumnoBO();
                Mapper.Map<TRaHistorialCambioAlumno, RaHistorialCambioAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaHistorialCambioAlumnoBO FirstBy(Expression<Func<TRaHistorialCambioAlumno, bool>> filter)
        {
            try
            {
                TRaHistorialCambioAlumno entidad = base.FirstBy(filter);
                RaHistorialCambioAlumnoBO objetoBO = Mapper.Map<TRaHistorialCambioAlumno, RaHistorialCambioAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaHistorialCambioAlumnoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaHistorialCambioAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaHistorialCambioAlumnoBO> listadoBO)
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

        public bool Update(RaHistorialCambioAlumnoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaHistorialCambioAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaHistorialCambioAlumnoBO> listadoBO)
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
        private void AsignacionId(TRaHistorialCambioAlumno entidad, RaHistorialCambioAlumnoBO objetoBO)
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

        private TRaHistorialCambioAlumno MapeoEntidad(RaHistorialCambioAlumnoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaHistorialCambioAlumno entidad = new TRaHistorialCambioAlumno();
                entidad = Mapper.Map<RaHistorialCambioAlumnoBO, TRaHistorialCambioAlumno>(objetoBO,
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

        /// <summary>
        /// Obtiene un listado de centro de costo con asistencia mensual filtrado por texto a buscar
        /// </summary>
        /// <param name="textoBuscar"></param>
        /// <returns></returns>
        public List<HistorialCambioAlumnoDetalleDTO> ListadoCentroCostoConAsistenciaMensual(string textoBuscar)
        {
            try
            {
                List<HistorialCambioAlumnoDetalleDTO> historialCambioAlumnoDetalle = new List<HistorialCambioAlumnoDetalleDTO>();
                var query = "SELECT IdHistorialCambioAlumno, CodigoAlumno, NombreAlumno, IdRaHistorialCambioAlumnoTipo, CentroCostoOrigen, CentroCostoDestino, Cancelado, Aprobado, UsuarioSolicitud, ComentarioSolicitud, UsuarioAprobacion, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion FROM ope.V_ObtenerHistorialCambioAlumno;";
                var historialCambioAlumnoDetalleDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(historialCambioAlumnoDetalleDB) && !historialCambioAlumnoDetalleDB.Contains("[]"))
                {
                    historialCambioAlumnoDetalle = JsonConvert.DeserializeObject<List<HistorialCambioAlumnoDetalleDTO>>(historialCambioAlumnoDetalleDB);
                    if (!string.IsNullOrEmpty(textoBuscar)) {
                        historialCambioAlumnoDetalle = historialCambioAlumnoDetalle.Where(x => x.CodigoAlumno.Contains(textoBuscar) || x.NombreAlumno.Contains(textoBuscar)).ToList();
                    }
                }
                return historialCambioAlumnoDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Valida si que no tenga más de una solicitud en proceso
        /// </summary>
        /// <param name="codigoAlumno"></param>
        /// <returns></returns>
        public bool ValidarRegistroSolictud(string codigoAlumno) {
            try
            {
                List<RaHistorialCambioAlumnoBO> listado = this.ListadoPorCodigoAlumno(codigoAlumno);
                if (listado != null && listado.Any(w => w.Cancelado == false && w.Aprobado == null)) {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un listado de alumnos filtrado por codigo de alumno
        /// </summary>
        /// <param name="codigoAlumno"></param>
        /// <returns></returns>
        public List<RaHistorialCambioAlumnoBO> ListadoPorCodigoAlumno(string codigoAlumno)
        {
            try
            {
                return this.GetBy(x => x.CodigoAlumno == codigoAlumno).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los detalles de un historial cambio detalle por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HistorialCambioDetalleDTO ObtenerHistorialCambioAlumno(int id) {
            try
            {
                HistorialCambioDetalleDTO historialCambioDetalle = new HistorialCambioDetalleDTO();
                var solicitud = this.FirstById(id);
                if (solicitud != null)
                {
                    var historialCambioDetalleTemp = new HistorialCambioDetalleDTO()
                    {
                        Aprobado = solicitud.Aprobado,
                        Cancelado = solicitud.Cancelado,
                        CentroCostoDestino = solicitud.CentroCostoDestino,
                        CentroCostoOrigen = solicitud.CentroCostoOrigen,
                        CodigoAlumno = solicitud.CodigoAlumno,
                        ComentarioSolicitud = solicitud.ComentarioSolicitud,
                        IdHistoricoCambioAlumno = solicitud.Id,
                        TipoHistoricoCambioAlumno = solicitud.IdRaHistorialCambioAlumnoTipo.ToString(),//TODO
                        UsuarioAprobacion = solicitud.UsuarioAprobacion,
                        UsuarioSolicitud = solicitud.UsuarioSolicitud
                    };
                    historialCambioDetalle = historialCambioDetalleTemp;
                }
                return historialCambioDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
