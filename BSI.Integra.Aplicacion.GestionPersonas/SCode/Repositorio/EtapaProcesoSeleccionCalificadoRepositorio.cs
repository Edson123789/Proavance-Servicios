using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: EtapaProcesoSeleccionCalificadoRepositorio
    /// Autor: Britsel C., Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión Etapas Calificadas por Proceso de Seleccion
    /// </summary>
    public class EtapaProcesoSeleccionCalificadoRepositorio : BaseRepository<TEtapaProcesoSeleccionCalificado, EtapaProcesoSeleccionCalificadoBO>
    {
        #region Metodos Base
        public EtapaProcesoSeleccionCalificadoRepositorio() : base()
        {
        }
        public EtapaProcesoSeleccionCalificadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EtapaProcesoSeleccionCalificadoBO> GetBy(Expression<Func<TEtapaProcesoSeleccionCalificado, bool>> filter)
        {
            IEnumerable<TEtapaProcesoSeleccionCalificado> listado = base.GetBy(filter);
            List<EtapaProcesoSeleccionCalificadoBO> listadoBO = new List<EtapaProcesoSeleccionCalificadoBO>();
            foreach (var itemEntidad in listado)
            {
                EtapaProcesoSeleccionCalificadoBO objetoBO = Mapper.Map<TEtapaProcesoSeleccionCalificado, EtapaProcesoSeleccionCalificadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EtapaProcesoSeleccionCalificadoBO FirstById(int id)
        {
            try
            {
                TEtapaProcesoSeleccionCalificado entidad = base.FirstById(id);
                EtapaProcesoSeleccionCalificadoBO objetoBO = new EtapaProcesoSeleccionCalificadoBO();
                Mapper.Map<TEtapaProcesoSeleccionCalificado, EtapaProcesoSeleccionCalificadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EtapaProcesoSeleccionCalificadoBO FirstBy(Expression<Func<TEtapaProcesoSeleccionCalificado, bool>> filter)
        {
            try
            {
                TEtapaProcesoSeleccionCalificado entidad = base.FirstBy(filter);
                EtapaProcesoSeleccionCalificadoBO objetoBO = Mapper.Map<TEtapaProcesoSeleccionCalificado, EtapaProcesoSeleccionCalificadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EtapaProcesoSeleccionCalificadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEtapaProcesoSeleccionCalificado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EtapaProcesoSeleccionCalificadoBO> listadoBO)
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

        public bool Update(EtapaProcesoSeleccionCalificadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEtapaProcesoSeleccionCalificado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EtapaProcesoSeleccionCalificadoBO> listadoBO)
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
        private void AsignacionId(TEtapaProcesoSeleccionCalificado entidad, EtapaProcesoSeleccionCalificadoBO objetoBO)
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

        private TEtapaProcesoSeleccionCalificado MapeoEntidad(EtapaProcesoSeleccionCalificadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEtapaProcesoSeleccionCalificado entidad = new TEtapaProcesoSeleccionCalificado();
                entidad = Mapper.Map<EtapaProcesoSeleccionCalificadoBO, TEtapaProcesoSeleccionCalificado>(objetoBO,
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


        public List<EtapaProcesoSeleccionCalificadoDTO> ObtenerEtapaCalificadaProcesoSeleccion(int IdPostulante, int IdProcesoSeleccion)
        {
            try
            {
                var query = "SELECT IdEtapaProcesoSeleccionCalificado,IdPostulante,IdProcesoSeleccionEtapa,IdProcesoSeleccion  from gp.V_ObtenerProcesoSeleccionEtapaPostulante WHERE IdPostulante = @IdPostulante and IdProcesoSeleccion=@IdProcesoSeleccion ";
                var res = _dapper.QueryDapper(query, new { IdPostulante = IdPostulante, IdProcesoSeleccion = IdProcesoSeleccion });
                return JsonConvert.DeserializeObject<List<EtapaProcesoSeleccionCalificadoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: EtapaProcesoSeleccionCalificadoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene información de una lista postulantes y lista de procesos de selección
        /// </summary>
        /// <param name="listapostulantes">Lista de postulantes</param>
        /// <param name="listaProcesoSeleccion"> Filtro sentencia WHERE según filtro elegido</param>
		/// <returns> Informción de Etapa y Estado de Proceso por Postlantes y Proceso de Seleccion </returns>
        /// <returns> Lista de Objeto DTO: List<ObtenerPostulantesProcesoSeleccionDTO> </returns>
        public List<ObtenerPostulantesProcesoSeleccionDTO> ObtenerTodosLosPostulantes(string listapostulantes, string listaProcesoSeleccion)
        {
            try
            {
                var query = "SELECT IdPostulante, IdProcesoSeleccion,IdProcesoSeleccionEtapa,ProcesoSeleccionEtapa,NroOrden,IdEtapaProcesoSeleccionCalificado,IdEstadoEtapaProcesoSeleccion, EstadoEtapaProcesoSeleccion, EsEtapaAprobada,EsContactado,EsCalificadoPorPostulante from gp.V_ObtenerEtapasCalificadasPostulanteProcesoSeleccion WHERE IdPostulante in(" + listapostulantes + ") and IdProcesoSeleccion in ("+ listaProcesoSeleccion + ") AND Estado = 1 AND EsContactado IS NOT NULL ORDER BY IdPostulante, NroOrden ASC";
                var res = _dapper.QueryDapper(query, new { listapostulantes = listapostulantes, IdProcesoSeleccion = listaProcesoSeleccion });
                return JsonConvert.DeserializeObject<List<ObtenerPostulantesProcesoSeleccionDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: EtapaProcesoSeleccionCalificadoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene la lista de Estados y Etapas Actuales de un proceso de selección actual, de una lista de postulantes
        /// </summary>
        /// <param name="listapostulantes">Lista de postulantes</param>
        /// <param name="filtro"> Filtro sentencia WHERE según filtro elegido</param>
		/// <returns> Lista Etapas/Estado, Estado Aprobatorio, EstadoProcesoSeleccion </returns>
        /// <returns> Lista de Objeto DTO: List<ObtenerEtapaCalificacionesActivasDTO> </returns>
        public List<ObtenerEtapaCalificacionesActivasDTO> ObtenerEtapasProcesoSeleccionActual(string listapostulantes, string filtro)
        {
            try
            {
                var query = "SELECT Id, IdPostulante,IdProcesoSeleccionEtapa,EsEtapaAprobada,IdEstadoEtapaProcesoSeleccion,EstadoPostulanteProcesoSeleccion from gp.V_ObtenerEtapasCalificadasActivas WHERE IdPostulante in(" + listapostulantes + ") AND Estado = 1 AND EstadoPostulanteProcesoSeleccion = 1 "+ filtro + "";
                var res = _dapper.QueryDapper(query, new { filtro = filtro, IdProcesoSeleccion = listapostulantes });
                return JsonConvert.DeserializeObject<List<ObtenerEtapaCalificacionesActivasDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: EtapaProcesoSeleccionCalificadoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene Cantidad de Examenes por Examen Asignado
        /// </summary>
        /// <param name="idProcesoSeleccion"> Id de Proceso Seleccion </param>
        /// <returns> List<OrdenEtapaTipoDTO> </returns>
        public List<OrdenEtapaTipoDTO> ObtenerOrdenEtapas(int idProcesoSeleccion)
        {
            try
            {
                List<OrdenEtapaTipoDTO> rpta = new List<OrdenEtapaTipoDTO>();
                var query = "SELECT Id,NroOrden,TipoExamen FROM [gp].[V_ObtenerEtapaExamenTipo] WHERE IdProcesoSeleccion = @idProcesoSeleccion AND Estado = 1 ORDER BY NroOrden ASC";
                var respuestaQuery = _dapper.QueryDapper(query, new { idProcesoSeleccion });

                //respuestaQuery = respuestaQuery.Replace("[", "").Replace("]", "");

                if (!respuestaQuery.Contains("[]") && !string.IsNullOrEmpty(respuestaQuery))
                {
                    rpta = JsonConvert.DeserializeObject<List<OrdenEtapaTipoDTO>>(respuestaQuery);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: EtapaProcesoSeleccionCalificadoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene Cantidad de Examenes por Examen Asignado
        /// </summary>
        /// <param name="idExamenAsignado"> Id de Examen Asignado </param>
        /// <returns> CantidadExamenTestDTO </returns>
        public CantidadExamenTestDTO ObtenerExamenesAsociados(int idExamenAsignado)
        {
            try
            {
                CantidadExamenTestDTO rpta = new CantidadExamenTestDTO();
                string query = "gp.SP_ObtenerCantidadExamenesPorExamenAsignado";
                var respuestaQuery = _dapper.QuerySPDapper(query, new { idExamenAsignado });

                respuestaQuery = respuestaQuery.Replace("[", "").Replace("]", "");

                if (!respuestaQuery.Contains("[]") && !string.IsNullOrEmpty(respuestaQuery))
                {
                    rpta = JsonConvert.DeserializeObject<CantidadExamenTestDTO>(respuestaQuery);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: EtapaProcesoSeleccionCalificadoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene Cantidad de Examenes Resueltos por Examen Asignado
        /// </summary>
        /// <param name="idExamenAsignado"> Id de Examen Asignado </param>
        /// <returns> ExamenResueltoPostulanteDTO </returns>
        public CalificacionAutomaticaDTO ObtenerExamenesResueltos(int idExamenAsignado)
        {
            try
            {
                string query = "gp.SP_ObtenerCantidadExamenesRealizadosPostulante";
                string respuestaQuery = _dapper.QuerySPDapper(query, new { idExamenAsignado });

                respuestaQuery = respuestaQuery.Replace("[", "").Replace("]", "");

                return JsonConvert.DeserializeObject<CalificacionAutomaticaDTO>(respuestaQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: EtapaProcesoSeleccionCalificadoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene Lista de Etapas de Examenes por Postulante
        /// </summary>
        /// <param name="idProcesoSeleccion"> Id de Proceso Seleccion </param>
        /// <param name="idPostulante"> Id de Postulante </param>
        /// <returns> List<ListaEtapaExamenesPorPostulante> </returns>
        public List<ListaEtapaExamenesPorPostulante> ObtenerListaEtapaExamenesPorPostulante(int idProcesoSeleccion, int idPostulante)
        {
            try
            {
                List<ListaEtapaExamenesPorPostulante> rpta = new List<ListaEtapaExamenesPorPostulante>();
                var query = "SELECT Id, IdProcesoSeleccionEtapa,IdPostulante,EsEtapaAprobada,IdEstadoEtapaProcesoSeleccion,EsContactado, NroOrden, IdProcesoSeleccion,Nombre,EsCalificadoPorPostulante,Estado FROM gp.V_ObtenerListaEtapaExamenesPorPostulante WHERE IdProcesoSeleccion = @idProcesoSeleccion AND IdPostulante = @idPostulante AND Estado = 1 ORDER BY NroOrden ASC";
                var respuestaQuery = _dapper.QueryDapper(query, new { idProcesoSeleccion, idPostulante });

                //respuestaQuery = respuestaQuery.Replace("[", "").Replace("]", "");

                if (!respuestaQuery.Contains("[]") && !string.IsNullOrEmpty(respuestaQuery))
                {
                    rpta = JsonConvert.DeserializeObject<List<ListaEtapaExamenesPorPostulante>>(respuestaQuery);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
