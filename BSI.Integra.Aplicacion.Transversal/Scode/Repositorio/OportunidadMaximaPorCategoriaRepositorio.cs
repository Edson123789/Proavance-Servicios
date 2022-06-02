using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class OportunidadMaximaPorCategoriaRepositorio : BaseRepository<TOportunidadMaximaPorCategoria, OportunidadMaximaPorCategoriaBO>
    {
        #region Metodos Base
        public OportunidadMaximaPorCategoriaRepositorio() : base()
        {
        }
        public OportunidadMaximaPorCategoriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        #endregion
        /// <summary>
        /// Conteo de Oportunidades Cerradas por el Asesor por Grupos (ejemplo: Grupo 1, Grupo 2, etc)
        ///En Caso de Generar un IS el Conteo se reinicia en 0. 
        /// </summary>
        /// <param name="estadoPantalla2">es un flag con valores 0 = si es un IS, 1 = es una
        /// oportunidad cerrada y 2 = visualizacion solo obtiene datos para mostrar</param>        
        public void ActualizarDatosEstaticosPantalla2(int idAsesor, int idCategoriaOrigen, int estadoISOM)
        {
            try
            {
                string _querypantalla1 = "com.SP_ObtenerDatosEstaticosPantalla2";
                var querypantalla1 = _dapper.QuerySPDapper(_querypantalla1, new { idAsesor = idAsesor, idCategoriaOrigen = idCategoriaOrigen, estado = estadoISOM });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Conteo de Oportunidades Cerradas por el Asesor por Grupos (ejemplo: Grupo 1, Grupo 2, etc)
        ///En Caso de Generar un IS el Conteo se reinicia en 0. 
        /// </summary>
        /// <param name="estadoPantalla2">es un flag con valores 0 = si es un IS, 1 = es una
        /// oportunidad cerrada y 2 = visualizacion solo obtiene datos para mostrar</param>        
        public SeguimientoAsesorDTO CargarSeguimientoAsesor(OportunidadMaximaPorCategoriaBO oportunidadCategoria)
        {
            try
            {
                var queryInformacionAsesor = _dapper.QuerySPFirstOrDefault("com.SP_ObtenerDatosEstaticosPantalla2", new { IdAsesor = oportunidadCategoria.IdPersonal, IdCategoriaOrigen =  oportunidadCategoria.IdTipoCategoriaOrigen, Estado = oportunidadCategoria.estadoPantalla2 });
                return JsonConvert.DeserializeObject<SeguimientoAsesorDTO>(queryInformacionAsesor);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
          
        }
    }
}
