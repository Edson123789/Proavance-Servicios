using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Planificacion/SesionConfigurarVideo
    /// Autor: Jorge Rivera - Gian Miranda
    /// Fecha: 27/02/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_SesionConfigurarVideo
    /// </summary>
    public class SesionConfigurarVideoRepositorio : BaseRepository<TSesionConfigurarVideo, SesionConfigurarVideoBO>
    {
        #region Metodos Base
        public SesionConfigurarVideoRepositorio() : base()
        {
        }
        public SesionConfigurarVideoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SesionConfigurarVideoBO> GetBy(Expression<Func<TSesionConfigurarVideo, bool>> filter)
        {
            IEnumerable<TSesionConfigurarVideo> listado = base.GetBy(filter);
            List<SesionConfigurarVideoBO> listadoBO = new List<SesionConfigurarVideoBO>();
            foreach (var itemEntidad in listado)
            {
                SesionConfigurarVideoBO objetoBO = Mapper.Map<TSesionConfigurarVideo, SesionConfigurarVideoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SesionConfigurarVideoBO FirstById(int id)
        {
            try
            {
                TSesionConfigurarVideo entidad = base.FirstById(id);
                SesionConfigurarVideoBO objetoBO = new SesionConfigurarVideoBO();
                Mapper.Map<TSesionConfigurarVideo, SesionConfigurarVideoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SesionConfigurarVideoBO FirstBy(Expression<Func<TSesionConfigurarVideo, bool>> filter)
        {
            try
            {
                TSesionConfigurarVideo entidad = base.FirstBy(filter);
                SesionConfigurarVideoBO objetoBO = Mapper.Map<TSesionConfigurarVideo, SesionConfigurarVideoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SesionConfigurarVideoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSesionConfigurarVideo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SesionConfigurarVideoBO> listadoBO)
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

        public bool Update(SesionConfigurarVideoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSesionConfigurarVideo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SesionConfigurarVideoBO> listadoBO)
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
        private void AsignacionId(TSesionConfigurarVideo entidad, SesionConfigurarVideoBO objetoBO)
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

        private TSesionConfigurarVideo MapeoEntidad(SesionConfigurarVideoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSesionConfigurarVideo entidad = new TSesionConfigurarVideo();
                entidad = Mapper.Map<SesionConfigurarVideoBO, TSesionConfigurarVideo>(objetoBO,
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
        /// Obtiene la lista de registros de sesion de la configuracion de video
        /// </summary>
        /// <param name="idConfigurarVideoPrograma">ID de la configuracion del video del programa (PK de la tabla pla.T_ConfigurarVideoPrograma)</param>
        /// <returns>Retorna una lista de objetos (RegistroSesionConfigurarVideoBO)</returns>
        public List<RegistroSesionConfigurarVideoBO> ObtenerConfigurarSesionVideoPrograma(int idConfigurarVideoPrograma)
        {
            List<RegistroSesionConfigurarVideoBO> rpta = new List<RegistroSesionConfigurarVideoBO>();
            string query = "Select Id,IdConfigurarVideoPrograma,Minuto,IdTipoVista,NroDiapositiva,IdEvaluacion,isnull(ConLogoVideo, 0) AS ConLogoVideo,isnull(ConLogoDiapositiva, 0) AS ConLogoDiapositiva From pla.T_SesionConfigurarVideo Where Estado=1 AND IdConfigurarVideoPrograma=@IdConfigurarVideoPrograma";
            string queryDb = _dapper.QueryDapper(query, new { IdConfigurarVideoPrograma = idConfigurarVideoPrograma });
            if (!string.IsNullOrEmpty(queryDb) && !queryDb.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<RegistroSesionConfigurarVideoBO>>(queryDb);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista tipos de vista para la configuracion de sesiones
        /// </summary>
        /// <returns>Retorna una lista de objetos (TipoVistaDTO)</returns>
        public List<TipoVistaDTO> ObtenerTipoVistaParaFiltro()
        {
            try
            {
                List<TipoVistaDTO> listaTipoVista = new List<TipoVistaDTO>();
                var query = "SELECT Id, Nombre FROM pla.V_TTipoVista_Filtro";
                var queryDb = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(queryDb) && !queryDb.Contains("[]"))
                {
                    listaTipoVista = JsonConvert.DeserializeObject<List<TipoVistaDTO>>(queryDb);
                }
                return listaTipoVista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<InformacionConfigurarVideoDTO> ObtenerInformacionVideoExcel(int idPGeneral)
        {
            try
            {
                List<InformacionConfigurarVideoDTO> listaInformacion = new List<InformacionConfigurarVideoDTO>();
                var query = "SELECT Id, IdConfigurarVideoPrograma,IdPGeneral, Minuto, IdTipoVista, NroDiapositiva, ConLogoVideo, ConLogoDiapositiva FROM [pla].[V_ListadoSesionConfigurarVideoPorPGeneral] where IdPGeneral=@idPGeneral";
                var queryDb = _dapper.QueryDapper(query,new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(queryDb) && !queryDb.Contains("[]"))
                {
                    listaInformacion = JsonConvert.DeserializeObject<List<InformacionConfigurarVideoDTO>>(queryDb);
                }
                return listaInformacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la fecha de inicio de un programa especifico
        /// </summary>
        /// <param name="idProgramaGeneral"></param>        
        /// <returns></returns>
        public ResultadoFinalDTO EliminarSesionesConfiguracionVideo(int idProgramaGeneral,string usuario)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("[pla].[SP_EliminarConfiguracionesVideoProgramaGeneral]", new { IdProgramaGeneral = idProgramaGeneral,Usuario = usuario });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ResultadoFinalDTO ActualizarPadreSesionConfiguracionVideo(int idConfiguracionAntiguo,int idConfiguracionNuevo)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("[pla].[SP_ActualizarSesionConfiguracionVideo]", new { IdConfigurarVideoProgramaAnterior = idConfiguracionAntiguo, IdConfigurarVideoProgramaNuevo= idConfiguracionNuevo });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ResultadoFinalDTO EliminarSesionConfiguracionVideo(int idConfigurarVideoPrograma)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("[pla].[SP_EliminarSesionConfiguracionesVideo]", new { IdConfigurarVideoPrograma = idConfigurarVideoPrograma });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ResultadoFinalDTO EliminarSesionConfiguracionVideoNuevo(int IdProgramaGeneral)
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("[pla].[SP_EliminarSesionConfiguracionesVideoNuevo]", new { IdProgramaGeneral });
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
