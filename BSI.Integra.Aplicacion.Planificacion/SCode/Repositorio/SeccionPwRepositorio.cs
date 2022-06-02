using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class SeccionPwRepositorio : BaseRepository<TSeccionPw, SeccionPwBO>
    {
        #region Metodos Base
        public SeccionPwRepositorio() : base()
        {
        }
        public SeccionPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SeccionPwBO> GetBy(Expression<Func<TSeccionPw, bool>> filter)
        {
            IEnumerable<TSeccionPw> listado = base.GetBy(filter);
            List<SeccionPwBO> listadoBO = new List<SeccionPwBO>();
            foreach (var itemEntidad in listado)
            {
                SeccionPwBO objetoBO = Mapper.Map<TSeccionPw, SeccionPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SeccionPwBO FirstById(int id)
        {
            try
            {
                TSeccionPw entidad = base.FirstById(id);
                SeccionPwBO objetoBO = new SeccionPwBO();
                Mapper.Map<TSeccionPw, SeccionPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SeccionPwBO FirstBy(Expression<Func<TSeccionPw, bool>> filter)
        {
            try
            {
                TSeccionPw entidad = base.FirstBy(filter);
                SeccionPwBO objetoBO = Mapper.Map<TSeccionPw, SeccionPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SeccionPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSeccionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SeccionPwBO> listadoBO)
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

        public bool Update(SeccionPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSeccionPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SeccionPwBO> listadoBO)
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
        private void AsignacionId(TSeccionPw entidad, SeccionPwBO objetoBO)
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

        private TSeccionPw MapeoEntidad(SeccionPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSeccionPw entidad = new TSeccionPw();
                entidad = Mapper.Map<SeccionPwBO, TSeccionPw>(objetoBO,
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
        /// Obtiene todos los registros de Secciones por el IdPlantillaPW.
        /// </summary>
        /// <returns></returns>
        public List<SeccionPwFiltroPlantillaPwDTO> ObtenerSeccionesPorIdPlantillaPW(int idPlantillaPw)
        {
            try
            {
                List<SeccionPwFiltroPlantillaPwDTO> documentos = new List<SeccionPwFiltroPlantillaPwDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre,Descripcion,Contenido,IdPlantillaPW,IdPlantilla,VisibleWeb,ZonaWeb,OrdenEeb,Titulo,Posicion,Tipo,IdSeccionTipoContenido,NombreSeccionTipoContenido," +
                    "IdSeccionTipoDetallePw,NombreSubSeccion,IdSubSeccionTipoContenido FROM pla.V_ObtenerSeccionesPlantillaPorIdPlantilla WHERE " +
                    "Estado = 1 and IdPlantillaPW = @IdPlantillaPw";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPlantillaPw = idPlantillaPw });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    documentos = JsonConvert.DeserializeObject<List<SeccionPwFiltroPlantillaPwDTO>>(respuestaDapper);
                }

                return documentos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros de Secciones por el IdPlantillaPW.
        /// </summary>
        /// <returns></returns>
        public List<SeccionPwPlantillaPwDTO> ObtenerSeccionesPorIdPlantillaPW_Plantilla(int idPlantillaPw)
        {
            try
            {
                List<SeccionPwPlantillaPwDTO> documentos = new List<SeccionPwPlantillaPwDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre,Descripcion,Contenido,IdPlantillaPW,IdPlantilla,VisibleWeb,ZonaWeb,OrdenEeb,Titulo,Posicion,Tipo,IdSeccionTipoContenido,NombreSeccionTipoContenido, " +
                    "IdSeccionTipoDetallePw,NombreSubSeccion,IdSubSeccionTipoContenido FROM pla.V_ObtenerSeccionesPlantillaPorIdPlantilla_PlantillaPw_Plantilla WHERE " +
                    "Estado = 1 and IdPlantillaPW = @IdPlantillaPw";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPlantillaPw = idPlantillaPw });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    documentos = JsonConvert.DeserializeObject<List<SeccionPwPlantillaPwDTO>>(respuestaDapper);
                }

                return documentos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Seccion asociados a una PlantillaPw
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPlantillaPw(int idPlantilla, string usuario, List<SeccionPwDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPlantillaPw == idPlantilla && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene todos los registros de Secciones.
        /// </summary>
        /// <returns></returns>
        public SeccionPwBO ObtenerSeccionesPorId_IdPlantillaPw(int id, int idPlantillaPw)
        {
            try
            {
                SeccionPwDTO documentos = new SeccionPwDTO();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre,Descripcion,Contenido,IdPlantillaPw,VisibleWeb,ZonaWeb,OrdenEeb,IdSeccionTipoContenido FROM pla.T_Seccion_PW WHERE " +
                    "Estado = 1 and Id = @Id and IdPlantillaPw = @IdPlantillaPw";
                var respuestaDapper = _dapper.QueryDapper(_query, new { Id = id ,IdPlantillaPw = idPlantillaPw });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    documentos = JsonConvert.DeserializeObject<SeccionPwDTO>(respuestaDapper);
                }
                SeccionPwBO secciones = new SeccionPwBO();
                secciones.Id = documentos.Id;
                secciones.Nombre = documentos.Nombre;
                secciones.Descripcion = documentos.Descripcion;
                secciones.Contenido = documentos.Contenido;
                secciones.IdPlantillaPw = documentos.IdPlantillaPw;
                secciones.VisibleWeb = documentos.VisibleWeb;
                secciones.ZonaWeb = documentos.ZonaWeb;
                secciones.OrdenEeb = documentos.OrdenEeb;
                secciones.IdSeccionTipoContenido = documentos.IdSeccionTipoContenido;


                return secciones;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las secciones de un proyecto de aplicacion por el IdAlumno.
        /// </summary>
        /// <returns></returns>
        public List<SeccionPwProyectoAplicacionDTO> ObtenerSeccionesProyectoAplicacionPorIdAlumno(int idAlumno)
        {
            try
            {
                List<SeccionPwProyectoAplicacionDTO> documentos = new List<SeccionPwProyectoAplicacionDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Titulo,VisibleWeb,ZonaWeb,OrdenEeb,Contenido,IdPlantillaPW,Posicion,Tipo,IdDocumentoPW,IdSeccionPW," +
                    "IdMatriculaCabecera,IdPEspecifico FROM pla.V_ObtenerSeccionesProyectoAplicacionPorIdAlumno WHERE " +
                    "EstadoDocumentoSeccion = 1 and IdAlumno = @IdAlumno and IdPlantillaMaestroPw = 9";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdAlumno = idAlumno });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    documentos = JsonConvert.DeserializeObject<List<SeccionPwProyectoAplicacionDTO>>(respuestaDapper);
                }

                return documentos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

    }

}
