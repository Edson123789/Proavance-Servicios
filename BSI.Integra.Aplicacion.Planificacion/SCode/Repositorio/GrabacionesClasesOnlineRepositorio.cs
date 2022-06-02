using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class GrabacionesClasesOnlineRepositorio : BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
    {
        #region Metodos Base
        public GrabacionesClasesOnlineRepositorio() : base()
        {
        }
        public GrabacionesClasesOnlineRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        #endregion

        /// Autor: Cesar Santillana
        /// Fecha: 02/07/2021
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data para la grilla principal.
        /// </summary>
        /// <returns>Retorma una lista List<GrabacionesClasesOnlineDTO> </returns>
        public List<GrabacionesClasesOnlineDTO> GenerarVistaProgramasOnline(GrabacionesClasesOnlineFiltroDTO filtroReporte)
        {
            try
            {
                string IdArea = null, IdSubArea = null, IdPGeneral = null, IdPEspecifico = null, IdPartner = null;
                if (filtroReporte.Area != null && filtroReporte.Area.Count() > 0) IdArea = String.Join(",", filtroReporte.Area);
                if (filtroReporte.SubArea != null && filtroReporte.SubArea.Count() > 0) IdSubArea = String.Join(",", filtroReporte.SubArea);
                if (filtroReporte.PGeneral != null && filtroReporte.PGeneral.Count() > 0) IdPGeneral = String.Join(",", filtroReporte.PGeneral);
                if (filtroReporte.PEspecifico != null && filtroReporte.PEspecifico.Count() > 0) IdPEspecifico = String.Join(",", filtroReporte.PEspecifico);
                if (filtroReporte.Partner != null && filtroReporte.Partner.Count() > 0) IdPartner = String.Join(",", filtroReporte.Partner);

                List<GrabacionesClasesOnlineDTO> reporteProgramasOnline = new List<GrabacionesClasesOnlineDTO>();
                var query = _dapper.QuerySPDapper("[pla].[SP_ConfigurarVideoProgramaSincronico]", new { IdPGeneral, IdPEspecifico, IdArea, IdSubArea, IdPartner });
                
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteProgramasOnline = JsonConvert.DeserializeObject<List<GrabacionesClasesOnlineDTO>>(query);
                }
                return reporteProgramasOnline;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Cesar Santillana
        /// Fecha: 08/07/2021
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data para la grilla secundaria de sesiones
        /// </summary>
        /// <returns>Retorma una lista List<SesionesClasesOnlineDTO> </returns>
        public List<SesionesClasesOnlineDTO> GenerarVistaSesiones(SesionesFiltroDTO filtroReporte)
        {
            try
            {
                string IdPEspecifico = null;
                if (filtroReporte.IdPEspecifico != null) IdPEspecifico = String.Join(",", filtroReporte.IdPEspecifico);
               
                List<SesionesClasesOnlineDTO> reporteSesiones = new List<SesionesClasesOnlineDTO>();
                var query = _dapper.QuerySPDapper("[pla].[SP_ConfigurarVideoProgramaSesion]" , new { IdPEspecifico });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteSesiones = JsonConvert.DeserializeObject<List<SesionesClasesOnlineDTO>>(query);
                }
                return reporteSesiones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Cesar Santillana
        /// Fecha: 08/07/2021
        /// Version: 1.0
        /// <summary>
        /// Función para insertar o modificar nueva data por sesion
        /// </summary>
        /// <returns>Retorma un bool </returns>
        public bool ActualizarSesiones(SesionesClasesOnlineModificarFiltroDTO filtroReporte)
        {
            try
            {
                foreach(var item in filtroReporte.Data)
                {
                    string IdPEspecifico = null, IdPEspecificoSesion = null, NombreSesion = null, IdTipoProveedorVideo = null, Video = null ;
                    string Habilitado = "false";
                    DateTime? fechainicio = null;
                    DateTime? fechafin = null;

                    if (item.IdPEspecifico != null) IdPEspecifico =  item.IdPEspecifico;
                    if (item.IdPEspecificoSesion != null) IdPEspecificoSesion = item.IdPEspecificoSesion;
                    if (item.NombreSesion != null) NombreSesion = item.NombreSesion;
                    if (item.IdTipoProveedorVideo != null) IdTipoProveedorVideo = item.IdTipoProveedorVideo;
                    if (item.Video != null) Video = item.Video;
                    if (item.Habilitado != null) Habilitado = item.Habilitado;
                    if (item.FechaInicio != null) fechainicio = item.FechaInicio.Value.AddHours(-5);
                    if (item.FechaFin != null) fechafin = item.FechaFin.Value.AddHours(-5);

                    var Estado = 1;
                    var FechaCreacion = DateTime.Now;
                    var FechaModificacion = DateTime.Now;
                    var UsuarioCreacion = "SYSTEM-PRUEBA";
                    var UsuarioModificacion = "SYSTEM-PRUEBA";

                    var _consultaDataTabla = _dapper.QueryDapper("Select * from pla.T_ConfigurarVideoSesionProgramaSincronico where IdPEspecificoSesion = @IdPEspecificoSesion ", new { IdPEspecificoSesion });
                    if (String.Equals(_consultaDataTabla, "[]"))
                    {
                        var _query = "INSERT INTO pla.T_ConfigurarVideoSesionProgramaSincronico (IdPEspecifico,IdPEspecificoSesion,NombreSesion,IdTipoProveedorVideo,Video, fechainicio, fechafin, Habilitado, Estado ,UsuarioCreacion ,UsuarioModificacion ,FechaCreacion ,FechaModificacion )" +
                        " VALUES(@IdPEspecifico,@IdPEspecificoSesion, @NombreSesion, @IdTipoProveedorVideo, @Video, @fechainicio, @fechafin, @Habilitado , @Estado ,@UsuarioCreacion ,@UsuarioModificacion ,@FechaCreacion ,@FechaModificacion)";
                        var query = _dapper.QueryDapper(_query, new { 
                            IdPEspecifico, 
                            IdPEspecificoSesion,
                            NombreSesion, 
                            IdTipoProveedorVideo,
                            Video, 
                            fechainicio, 
                            fechafin, Habilitado, Estado,
                            UsuarioCreacion ,
                            UsuarioModificacion ,
                            FechaCreacion ,
                            FechaModificacion 
                        });
                    }
                    else
                    {

                        var _query = "UPDATE pla.T_ConfigurarVideoSesionProgramaSincronico SET " +
                        " NombreSesion = @NombreSesion,IdTipoProveedorVideo= @IdTipoProveedorVideo,Video = @Video, fechainicio= @fechainicio, fechafin = @fechafin, Habilitado = @Habilitado  WHERE IdPEspecificoSesion = @IdPEspecificoSesion";
                        var query = _dapper.QueryDapper(_query, new { IdPEspecifico, IdPEspecificoSesion, NombreSesion, IdTipoProveedorVideo, Video, fechainicio, fechafin, Habilitado });

                    }
                    
                }
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Cesar Santillana
        /// Fecha: 30/07/2021
        /// Version: 1.0
        /// <summary>
        /// Función para insertar o modificar nueva data de Disponibilidad del programa por defecto
        /// </summary>
        /// <returns>Retorma un bool </returns>
        public bool ModificarDisponibilidadProgramaDefecto(DataDisponibilidadProgramaDefectoDTO filtroReporte)
        {
            try
            {
                    var Id = filtroReporte.Id;
                    var NumeroDia = filtroReporte.NumeroDia;
                    var Estado = 1;
                    var FechaCreacion = DateTime.Now;
                    var FechaModificacion = DateTime.Now;
                    var UsuarioCreacion = "SYSTEM-PRUEBA";
                    var UsuarioModificacion = "SYSTEM-PRUEBA";

                    var _consultaDataTabla = _dapper.QueryDapper("Select * from pla.T_DisponibilidadProgramaSincronicoDefecto", new { });
                    if (String.Equals(_consultaDataTabla, "[]"))
                    {
                        var _query = "INSERT INTO pla.T_DisponibilidadProgramaSincronicoDefecto (NumeroDia, Estado ,UsuarioCreacion ,UsuarioModificacion ,FechaCreacion ,FechaModificacion )" +
                        " VALUES(@NumeroDia, @Estado ,@UsuarioCreacion ,@UsuarioModificacion ,@FechaCreacion ,@FechaModificacion)";
                        var query = _dapper.QueryDapper(_query, new
                        {
                            NumeroDia,
                            Estado,
                            UsuarioCreacion,
                            UsuarioModificacion,
                            FechaCreacion,
                            FechaModificacion
                        });
                    }
                    else
                    {
                        var _query = "UPDATE pla.T_DisponibilidadProgramaSincronicoDefecto SET " +
                        " NumeroDia = @NumeroDia, UsuarioModificacion = @UsuarioModificacion, FechaModificacion = @FechaModificacion WHERE Id = @Id";
                        var query = _dapper.QueryDapper(_query, new { NumeroDia,UsuarioModificacion,FechaModificacion, Id });
                    }
              
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Cesar Santillana
        /// Fecha: 30/07/2021
        /// Version: 1.0
        /// <summary>
        /// Función para obtener los datos de la disponibilidad del programa
        /// </summary>
        /// <returns>Retorma un bool </returns>
        public List<DataDisponibilidadProgramaDefectoDTO> ObtenerDisponibilidadPrograma()
        {
            List<DataDisponibilidadProgramaDefectoDTO> DisponibilidadPrograma = new List<DataDisponibilidadProgramaDefectoDTO>();
            var query = _dapper.QueryDapper("select * from pla.T_DisponibilidadProgramaSincronicoDefecto", new { });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                DisponibilidadPrograma = JsonConvert.DeserializeObject<List<DataDisponibilidadProgramaDefectoDTO>>(query);
            }
            return DisponibilidadPrograma;
        }
    }
}
