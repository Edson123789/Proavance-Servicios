﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: OcurrenciaRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Ocurrencias
    /// </summary>
    public class OcurrenciaAlternoRepositorio : BaseRepository<TOcurrenciaAlterno, OcurrenciaAlternoBO>
    {
        private int idOcurrencia;

        #region Metodos Base
        public OcurrenciaAlternoRepositorio() : base()
        {
        }
        public OcurrenciaAlternoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OcurrenciaAlternoBO> GetBy(Expression<Func<TOcurrenciaAlterno, bool>> filter)
        {
            IEnumerable<TOcurrenciaAlterno> listado = base.GetBy(filter);
            List<OcurrenciaAlternoBO> listadoBO = new List<OcurrenciaAlternoBO>();
            foreach (var itemEntidad in listado)
            {
                OcurrenciaAlternoBO objetoBO = Mapper.Map<TOcurrenciaAlterno, OcurrenciaAlternoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OcurrenciaAlternoBO FirstById(int id)
        {
            try
            {
                TOcurrenciaAlterno entidad = base.FirstById(id);
                OcurrenciaAlternoBO objetoBO = new OcurrenciaAlternoBO();
                Mapper.Map<TOcurrenciaAlterno, OcurrenciaAlternoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OcurrenciaAlternoBO FirstBy(Expression<Func<TOcurrenciaAlterno, bool>> filter)
        {
            try
            {
                TOcurrenciaAlterno entidad = base.FirstBy(filter);
                OcurrenciaAlternoBO objetoBO = Mapper.Map<TOcurrenciaAlterno, OcurrenciaAlternoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OcurrenciaAlternoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOcurrenciaAlterno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OcurrenciaAlternoBO> listadoBO)
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

        public bool Update(OcurrenciaAlternoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOcurrenciaAlterno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OcurrenciaAlternoBO> listadoBO)
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
        private void AsignacionId(TOcurrenciaAlterno entidad, OcurrenciaAlternoBO objetoBO)
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

        private TOcurrenciaAlterno MapeoEntidad(OcurrenciaAlternoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOcurrenciaAlterno entidad = new TOcurrenciaAlterno();
                entidad = Mapper.Map<OcurrenciaAlternoBO, TOcurrenciaAlterno>(objetoBO,
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
        /// Obtiene todas las ocurrencias (para mostrarlas en grillas, comboboxes, etc)
        /// </summary>
        /// <returns></returns>
        public List<OcurrenciaDTO> ObtenerOcurrenciasParaFiltro()
        {
            try
            {
                List<OcurrenciaDTO> ocurrencias = new List<OcurrenciaDTO>();
                string _query = "SELECT Id, Nombre, NombreM, IdFaseOportunidad, IdActividadCabecera, IdPlantilla_Speech, IdEstadoOcurrencia, Oportunidad, RequiereLlamada, Roles, Color, NombreCs, IdPersonalAreaTrabajo, PersonalAreaTrabajo, IdTipoOcurrencia FROM [com].[V_ObtenerOcurrencias_AgendaAlterno] WHERE Estado=1 ORDER BY Id DESC";
                var ocurrenciasDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ocurrenciasDB) && !ocurrenciasDB.Contains("[]"))
                {
                    ocurrencias = JsonConvert.DeserializeObject<List<OcurrenciaDTO>>(ocurrenciasDB);
                }
                return ocurrencias;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        ///  Obtiene todas las ocurrencia
        /// </summary>
        /// <param></param>
        /// <returns>Id, Nombre, IdFaseOportunidad</returns>
        public List<FiltroOcurrenciaDTO> ObtenerOcurrenciaFiltro()
        {
            try
            {
                var listaOcurrencia = GetBy(x => true, y => new FiltroOcurrenciaDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdFaseOportunidad = y.IdFaseOportunidad
                }).ToList();

                return listaOcurrencia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        ///Repositorio: OcurrenciaRepositorio
        ///Autor: Edgar S.
        ///Fecha: 06/02/2021
        /// <summary>
        /// Obtener Información de Ocurrencia por Id
        /// </summary>
        /// <param name="idOcurrencia"> Id de Ocurrencia </param>
        /// <returns> OcurrenciaAlternoBO </returns>
        public OcurrenciaAlternoBO ObtenerOcurrenciaPorActividad(int idOcurrencia)
        {
            try
            {
                string queryOcurrencia = "Select * From com.T_OcurrenciaReporteAlterno Where Id=@IdOcurrencia";
                var resultado = _dapper.FirstOrDefault(queryOcurrencia, new { IdOcurrencia = idOcurrencia });
                return JsonConvert.DeserializeObject<OcurrenciaAlternoBO>(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool ValidacionOcurrenciaFinalizarActividad(int idOcurrencia)
        {
            try
            {
                string _queryOcurrencia = "SELECT Id, Nombre FROM com.V_TOcurrenciaAlternoValidacionFinalizarActividad WHERE Estado=1 and Validacion = 1";
                var queryOcurrencia = _dapper.QueryDapper(_queryOcurrencia, null);
                var idValidaciones = JsonConvert.DeserializeObject<List<OcurrenciaFiltroNombreDTO>>(queryOcurrencia);
                return idValidaciones.Any();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Ob tiene el idOcurrencia filtrado por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        private int ObtenerIdPorNombre(string nombre)
        {
            try
            {
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el idOcurrencia de cerrar a fase OD
        /// </summary>
        /// <returns></returns>
        public OcurrenciaFiltroDTO ObtenerIdOcurrenciaCerrarOD()
        {
            try
            {
                OcurrenciaFiltroDTO ocurrencia = new OcurrenciaFiltroDTO();
                ocurrencia.Id = this.ObtenerIdPorNombre("Cerrado Fase OD");
                return ocurrencia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Obtiene el idOcurrencia de cerrar a fase OM
        /// </summary>
        /// <returns></returns>
        public OcurrenciaFiltroDTO ObtenerIdOcurrenciaCerrarOM()
        {
            try
            {
                OcurrenciaFiltroDTO ocurrencia = new OcurrenciaFiltroDTO();
                ocurrencia.Id = this.ObtenerIdPorNombre("Cerrado Fase OM");
                return ocurrencia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: IntegraAspNetUsersRepositorio
        ///Autor: Edgar S.
        ///Fecha: 06/02/2021
        /// <summary>
        /// Obtener IdOcurrencia por nombre
        /// </summary>
        /// <param name="nombre"> Nombre de Ocurrencia </param>
        /// <returns> Id Ocurrencia : OcurrenciaAlternoBO </returns>
        public int ObtenerOcurrenciaPorNombre(string nombre)
        {
            try
            {
                string query = "Select top 1 Id From com.T_OcurrenciaAlterno Where Nombre= @nombre";
                var queryOcurrenciaNombre = _dapper.FirstOrDefault(query, new { nombre });
                var queryOcurrencia = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryOcurrenciaNombre);


                if (queryOcurrenciaNombre != null && queryOcurrenciaNombre != "")
                {
                    return queryOcurrencia.Select(w => w.Value).FirstOrDefault();
                }
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las Ocurrencias de Actividad por Ocurrencia
        /// </summary>
        /// <param name="IdOcurrencia"></param>
        public List<HojaActividadesDTO> HojaGetActividadesByOcurrencia(int idOcurrencia)
        {
            try
            {
                string _queryHojaActividades = "SELECT Id,TipoActividad, Actividad,FechaProgramada, IdOcurrencia, OcurrenciaPadre FROM com.V_HojaGetActividadesByOcurrencia WHERE IdOcurrencia = @IdOcurrencia AND OcurrenciaPadre = -1 ";
                var queryHojaActividades = _dapper.QueryDapper(_queryHojaActividades, new { IdOcurrencia = idOcurrencia });
                var hojaActividades = JsonConvert.DeserializeObject<List<HojaActividadesDTO>>(queryHojaActividades);
                return hojaActividades;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: OcurrenciaRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Devuelve 1 o 0 si la Ocurrencia debe cambiar su estado 
        /// </summary>
        /// <returns> Confirmación: Bool </returns>
        public bool ValidarEstadoOcurrencia(int idOcurrencia)
        {
            try
            {
                string consultaOcurrencia = "Select Id From com.V_TOcurrenciaAlterno_ValidarCambiarEstado Where Id = @IdOcurrencia and Estado = 1";
                var queryOcurrencia = _dapper.QueryDapper(consultaOcurrencia, new { IdOcurrencia = idOcurrencia });
                var ocurrencias = JsonConvert.DeserializeObject<List<OcurrenciaFiltroEstadoOcurrenciaDTO>>(queryOcurrencia);

                return ocurrencias.Any();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: OcurrenciaRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Valida si pertenece a Workshop o Prelanzamiento
        /// </summary>
        /// <param name="idCategoria"> Id de Categoría </param>
        /// <returns> int </returns>
        public int ValidarGrupoPreLanzamiento(int idCategoria)
        {
            try
            {
                var consultaGrupoPrelanzamiento = _dapper.QuerySPFirstOrDefault("com.SP_ObtenerGrupoAgenda", new { IdCategoria = idCategoria });
                var grupoPrelanzamiento = JsonConvert.DeserializeObject<Dictionary<string, int>>(consultaGrupoPrelanzamiento);
                return grupoPrelanzamiento.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Las ActividadesCabecera Relacionadas a una Ocurrencia
        /// </summary>
        /// <param name="IdOcurrencia"></param>
        /// <returns></returns>
        public List<ActividadCabeceraDTO> ObtenerActividadesPorOcurrencia(int IdOcurrencia)
        {
            try
            {
                string _queryActividad = _dapper.QuerySPDapper("com.SP_ObtenerActividadesCabeceraPorIdOcurrencia", new { IdParam = IdOcurrencia });
                var queryActividad = JsonConvert.DeserializeObject<List<ActividadCabeceraDTO>>(_queryActividad);
                return queryActividad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Obtiene Las Ocurrencias Por Actividad
        /// </summary>
        /// <param name="IdOcurrencia"></param>
        /// <returns></returns>
        public List<OcurrenciaPorActividadPadreDTO> ObtenerTodasOcurrenciasActividad(int IdOcurrencia)
        {
            try
            {
                string _queryActividad = _dapper.QuerySPDapper("com.SP_ObtenerTodasOcurrenciasActividadAlterno", new { IdOcurrenciaParam = IdOcurrencia });
                var queryActividad = JsonConvert.DeserializeObject<List<OcurrenciaPorActividadPadreDTO>>(_queryActividad);
                return queryActividad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Obtiene Las Ocurrencias Por Actividad
        /// </summary>
        /// <param name="IdOcurrencia"></param>
        /// <returns></returns>
        public List<OcurrenciaPorActividadPadreDTO> ObtenerOcurrenciasActividadPorId(int Id)
        {
            try
            {
                string _queryActividad = _dapper.QuerySPDapper("com.SP_ObtenerOcurrenciasPorId", new { Id = Id });
                var queryActividad = JsonConvert.DeserializeObject<List<OcurrenciaPorActividadPadreDTO>>(_queryActividad);
                return queryActividad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Obtiene Las Ocurrencias Por ActividadPadre
        /// </summary>
        /// <param name="IdOcurrencia"></param>
        /// <returns></returns>
        public List<OcurrenciaPorActividadPadreDTO> ObtenerTodasOcurrenciasActividadPadre(int IdPadre, int IdActividad)
        {
            try
            {
                string _queryActividad = _dapper.QuerySPDapper("com.SP_ObtenerTodasOcurrenciasPorActividadPadreAlterno", new { IdOcurrenciaParam = IdPadre, IdActividadParam = IdActividad });
                var queryActividad = JsonConvert.DeserializeObject<List<OcurrenciaPorActividadPadreDTO>>(_queryActividad);
                return queryActividad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// Valida Si la Ocurrencia Pertence a Excepciones para no se modificada
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool ExcepcionesOcurrencia(int Id)
        {
            try
            {
                string _queryOcurrencia = _dapper.FirstOrDefault("Select Excepcion From mkt.V_TOcurrencia_ExcepcionesAlterno Where Estado=1 and Id=@Id", new { Id });
                var queryOcurrencia = JsonConvert.DeserializeObject<OcurrenciaExcepcionDTO>(_queryOcurrencia);
                if (queryOcurrencia.Excepcion == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Obtiene las ocurrencias para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 20/10/2021
        /// Version: 1.0
        /// <summary>
        /// Devuelve los tipos de ocurrencias
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        public List<FiltroDTO> ObtenerTipoOcurrencia()
        {
            try
            {
                var query = "SELECT Id,Nombre FROM com.T_TipoOcurrencia WHERE Estado=1";
                var dapper = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(dapper);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
