using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/PrioridadMailChimpListaCorreo
    /// Autor: Gian Miranda
    /// Fecha: 27/01/2021
    /// <summary>
    /// Repositorio para la interaccion con la DB mkt.T_PrioridadMailChimpListaCorreo
    /// </summary>
    public class PrioridadMailChimpListaCorreoRepositorio : BaseRepository<TPrioridadMailChimpListaCorreo, PrioridadMailChimpListaCorreoBO>
    {
        #region Metodos Base
        public PrioridadMailChimpListaCorreoRepositorio() : base()
        {
        }
        public PrioridadMailChimpListaCorreoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PrioridadMailChimpListaCorreoBO> GetBy(Expression<Func<TPrioridadMailChimpListaCorreo, bool>> filter)
        {
            IEnumerable<TPrioridadMailChimpListaCorreo> listado = base.GetBy(filter);
            List<PrioridadMailChimpListaCorreoBO> listadoBO = new List<PrioridadMailChimpListaCorreoBO>();
            foreach (var itemEntidad in listado)
            {
                PrioridadMailChimpListaCorreoBO objetoBO = Mapper.Map<TPrioridadMailChimpListaCorreo, PrioridadMailChimpListaCorreoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public IEnumerable<PrioridadMailChimpListaCorreoBO> GetBy(Expression<Func<TPrioridadMailChimpListaCorreo, bool>> filter, int take)
        {
            IEnumerable<TPrioridadMailChimpListaCorreo> listado = base.GetBy(filter).Take(take).ToList();
            List<PrioridadMailChimpListaCorreoBO> listadoBO = new List<PrioridadMailChimpListaCorreoBO>();
            foreach (var itemEntidad in listado)
            {
                PrioridadMailChimpListaCorreoBO objetoBO = Mapper.Map<TPrioridadMailChimpListaCorreo, PrioridadMailChimpListaCorreoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PrioridadMailChimpListaCorreoBO FirstById(int id)
        {
            try
            {
                TPrioridadMailChimpListaCorreo entidad = base.FirstById(id);
                PrioridadMailChimpListaCorreoBO objetoBO = new PrioridadMailChimpListaCorreoBO();
                Mapper.Map<TPrioridadMailChimpListaCorreo, PrioridadMailChimpListaCorreoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PrioridadMailChimpListaCorreoBO FirstBy(Expression<Func<TPrioridadMailChimpListaCorreo, bool>> filter)
        {
            try
            {
                TPrioridadMailChimpListaCorreo entidad = base.FirstBy(filter);
                PrioridadMailChimpListaCorreoBO objetoBO = Mapper.Map<TPrioridadMailChimpListaCorreo, PrioridadMailChimpListaCorreoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PrioridadMailChimpListaCorreoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPrioridadMailChimpListaCorreo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PrioridadMailChimpListaCorreoBO> listadoBO)
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

        public bool Update(PrioridadMailChimpListaCorreoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPrioridadMailChimpListaCorreo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PrioridadMailChimpListaCorreoBO> listadoBO)
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

                //foreach (var objetoBO in listadoBO)
                //{
                //    bool resultado = Update(objetoBO);
                //    if (resultado == false)
                //        return false;
                ////}
                //if (listadoBO == null)
                //{
                //    throw new ArgumentNullException("Entidad nula");
                //}

                ////mapeo de la entidad
                //IEnumerable<TPrioridadMailChimpListaCorreo> entidades = MapeoEntidad(listadoBO.AsEnumerable());

                //bool resultado = base.Update(entidades);
                //if (resultado)
                //    AsignacionId(entidades, listadoBO);

                //return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(IEnumerable<TPrioridadMailChimpListaCorreo> entidad, IEnumerable<PrioridadMailChimpListaCorreoBO> objetoBO)
        {
            try
            {

                //var numbers = new[] { 1, 2, 3, 4 };
                //var words = new[] { "one", "two", "three", "four" };

                var entitiesBOs = entidad.Zip(objetoBO, (n, w) => new { entity = n, bo = w });
                foreach (var nw in entitiesBOs)
                {
                    this.AsignacionId(nw.entity, nw.bo);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void AsignacionId(TPrioridadMailChimpListaCorreo entidad, PrioridadMailChimpListaCorreoBO objetoBO)
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

        private IEnumerable<TPrioridadMailChimpListaCorreo> MapeoEntidad(IEnumerable<PrioridadMailChimpListaCorreoBO> objetoBO)
        {
            try
            {
                //crea la entidad padre
                IEnumerable<TPrioridadMailChimpListaCorreo> entidad = new List<TPrioridadMailChimpListaCorreo>();

                entidad = Mapper.Map<IEnumerable<PrioridadMailChimpListaCorreoBO>, IEnumerable<TPrioridadMailChimpListaCorreo>>
                        (objetoBO, opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private TPrioridadMailChimpListaCorreo MapeoEntidad(PrioridadMailChimpListaCorreoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPrioridadMailChimpListaCorreo entidad = new TPrioridadMailChimpListaCorreo();
                entidad = Mapper.Map<PrioridadMailChimpListaCorreoBO, TPrioridadMailChimpListaCorreo>(objetoBO,
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
        /// Obtiene todo los correos para una lista Integra
        /// </summary>
        /// <param name="idPrioridadMailChimpLista">Id de la Prioridad MailChimp establecida en el conjunto</param>
        /// <param name="take">Propiedad LINQ, tomar los primeros N elementos</param>
        /// <returns>Lista de prioridad Mailchimp, ordenado por prioridad</returns>
        public List<PrioridadMailChimpListaCorreoBO> ObtenerListPrioridadMailChimpListaCorreoSinEnviar(int idPrioridadMailChimpLista, int take)
        {
            try
            {
                return GetBy(x => x.IdPrioridadMailChimpLista == idPrioridadMailChimpLista
                                && x.EsSubidoCorrectamente == null
                                //|| x.EsSubidoCorrectamente is null)//false y null
                                && x.Nombre1 != ""
                                && x.ApellidoPaterno != "", take).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene todo los correos para una lista Integra
        /// </summary>
        /// <param name="idPrioridadMailChimpLista">Id de la Prioridad MailChimp establecida en el conjunto</param>
        /// <returns>Lista de prioridad Mailchimp, ordenado por prioridad</returns>
        public List<PrioridadMailChimpListaCorreoBO> ObtenerListPrioridadMailChimpListaCorreoSinEnviar(int idPrioridadMailChimpLista)
        {
            try
            {
                return GetBy(x => x.IdPrioridadMailChimpLista == idPrioridadMailChimpLista
                                && x.EsSubidoCorrectamente == null
                                //|| x.EsSubidoCorrectamente is null)//false y null
                                && x.Nombre1 != ""
                                && x.Estado == true
                                && x.ApellidoPaterno != "").ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la cantidad de MailChimpListaCorreo que no se ha enviado a Mailchimp
        /// </summary>
        /// <param name="idPrioridadMailChimpLista">Id de la Prioridad MailChimp establecida en el conjunto</param>
        /// <returns>Entero con la cantidad contactos resultantes de la ejecucion</returns>
        public int ObtenerCantidadListPrioridadMailChimpListaCorreoSinEnviar(int idPrioridadMailChimpLista)
        {
            try
            {
                return GetBy(x => x.IdPrioridadMailChimpLista == idPrioridadMailChimpLista
                                && x.EsSubidoCorrectamente == null
                                //|| x.EsSubidoCorrectamente is null)//false y null
                                && x.Nombre1 != ""
                                && x.ApellidoPaterno != "").Count();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Registro de data de prueba para los Correos Mailchimp
        /// </summary>
        /// <param name="idFiltro">Id del filtro que se ejecuta</param>
        /// <param name="idPrioridadesMailchimpLista">PK de</param>
        /// <param name="idCampaniasMailing">PK de mkt.T_CampaniaMailing, identifica la campania a ejecutar</param>
        /// <param name="usuario">Usuario que ejecuta el filtro segmento Mailchimp</param>
        /// <returns>Entero con la cantidad contactos resultantes de la ejecucion</returns>
        public int EjecutarFiltroCampaniaMailchimpPrueba(int idFiltro, int idPrioridadesMailchimpLista, int idCampaniasMailing, string usuario)
        {
            Dictionary<string, int> respuesta = new Dictionary<string, int>();
            if (idFiltro == 201)
            {
                var registrosBD = _dapper.QuerySPFirstOrDefault("mkt.SP_GetFiltroSegmentoOportunidadesCampaniaMailchimpPrueba", new { @IdCampaniasMailing = idCampaniasMailing, @IdPrioridadMailchimpLista = idPrioridadesMailchimpLista, @usuario = usuario });
                respuesta = JsonConvert.DeserializeObject<Dictionary<string, int>>(registrosBD);
            }
            return respuesta["Resultado"];
        }

        /// <summary>
        /// Obtiene los correos para la CampaniaMailchimpLista y los guarda en tablas de prioridades respectivas.
        /// </summary>
        /// <param name="valorTipo">DTO con las configuracion en formato string para ejecutar el filtro</param>
        /// <param name="idPrioridadMailchimpLista">PK de la tabla mkt.T_PrioridadMailChimpLista, usada para identificar las listas de la campania</param>
        /// <param name="idCampaniaMailing">PK de mkt.T_CampaniaMailing, usada para identificar la campania ejecutada</param>
        /// <param name="usuario">Usuario que ejecuta el filtro segmento Mailchimp</param>
        /// <returns>Entero con la cantidad de contactos resultantes de la ejecucion</returns>
        public int EjecutarFiltroCampaniaMailchimp(ValorTipoDTO valorTipo, int idPrioridadMailchimpLista, int idCampaniaMailing, string usuario)
        {
            try
            {
                //Dictionary<string, int> respuesta = new Dictionary<string, int>();
                string sp = "mkt.SP_ObtenerFiltroSegmentoOportunidadesCampaniaMailchimp";

                //var objectContext = (this as IObjectContextAdapter).ObjectContext;
                //objectContext.CommandTimeout = 120;

                var queryRespuesta = _dapper.QuerySPDapper(sp, new
                {
                    IdCampaniaMailing = idCampaniaMailing,
                    IdPrioridadMailchimpLista = idPrioridadMailchimpLista,
                    Usuario = usuario,
                    IdArea = valorTipo.Areas,
                    SubAreas = valorTipo.SubAreas,
                    ProgramaGeneral = valorTipo.ProgramaGeneral,
                    ProgramasEspecificos = valorTipo.ProgramaEspecifico,
                    Scores = valorTipo.ProbabilidadRegistro,
                    Paises = valorTipo.Paises,
                    Ciudades = valorTipo.Ciudades,
                    IdFasesActualFinales = valorTipo.FaseOportunidadInicial,
                    IdFasesActualMaximas = valorTipo.FaseOportunidadMaximaInicial,
                    IdFasesHistoricasFinales = valorTipo.FaseHistorica,
                    IdFasesHistoricasMaximas = valorTipo.FaseHistoricaMaxima,
                    FechaInicioOportunidad = valorTipo.FechaInicioOportunidad,
                    FechaFinOportunidad = valorTipo.FechaFinOportunidad,
                    SinActividadAlumno = valorTipo.SinActividadAlumno,

                    IdCargos = valorTipo.Cargos,
                    IdIndustrias = valorTipo.Industrias,
                    IdFormaciones = valorTipo.AreaFormacion,
                    IdTrabajos = valorTipo.AreaTrabajo,
                    IdCategoriaDato = valorTipo.CategoriaDato,
                    IdFiltroSegmento = valorTipo.IdFiltroSegmento,

                });
                var respuesta = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(queryRespuesta);
                return respuesta[0]["Resultado"];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las interacciones de los correos enviados por Mailchimp
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <returns>Lista objetos de clase CorreoInteraccionesAlumnoDTO</returns>
        public List<CorreoInteraccionesAlumnoDTO> ListaInteraccionCorreo(int idAlumno)
        {
            try
            {
                string query = "SELECT TOP 5 Id, FechaCreacion, Asunto, Estado, CorreoReceptor,Remitente, NombreProgramaGeneral FROM  mkt.V_ObtenerCorreoEnviadoMailchimp WHERE IdAlumno = @IdAlumno order by FechaCreacion desc";
                var queryRespuesta = _dapper.QueryDapper(query, new { IdAlumno = idAlumno });
                List<CorreoInteraccionesAlumnoDTO> listaInteracciones = JsonConvert.DeserializeObject<List<CorreoInteraccionesAlumnoDTO>>(queryRespuesta);

                return listaInteracciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene los datos de las prioridades ya procesadas de Mailing General
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de tipo PrioridadMailingGeneralPreObtencionDTO</returns>
        public List<PreWhatsAppResultadoConjuntoListaDTO> ListaResultadoMailingGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                string query = "SELECT IdPrioridadMailChimpListaCorreo, IdAlumno, Celular, IdCodigoPais, IdPgeneral FROM mkt.V_WhatsAppMailingGeneralResultado WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle";
                var queryRespuesta = _dapper.QueryDapper(query, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
                List<PreWhatsAppResultadoConjuntoListaDTO> listaMailingGeneral = JsonConvert.DeserializeObject<List<PreWhatsAppResultadoConjuntoListaDTO>>(queryRespuesta);

                return listaMailingGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la cantidad apta de una CampaniaGeneralDetalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Entero con la cantidad de datos aptos para WhatsApp</returns>
        public int ObtenerPrimeraListaAptaCampaniaDetalleWhatsApp(int idCampaniaGeneralDetalle)
        {
            string querySp = "mkt.SP_ObtenerPrimeraListaAptaCampaniaDetalleWhatsApp";
            var queryRespuesta = _dapper.QuerySPDapper(querySp, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });

            var cantidadApta = !string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]") && queryRespuesta != "null" ? JsonConvert.DeserializeObject<ValorIntDTO>(queryRespuesta) : new ValorIntDTO { Valor = 1 };

            return cantidadApta.Valor;
        }

        /// <summary>
        /// Obtiene los datos de las prioridades ya procesadas de Mailing General
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de tipo PrioridadMailingGeneralPreObtencionDTO</returns>
        public List<PreWhatsAppResultadoConjuntoListaDTO> ObtenerListaAptaPreprocesamientoWhatsApp(int idCampaniaGeneralDetalle)
        {
            try
            {
                var listaAptaWhatsApp = new List<PreWhatsAppResultadoConjuntoListaDTO>();

                string querySp = "mkt.SP_ObtenerPrimeraListaAptaCampaniaDetalleWhatsApp";
                var queryRespuesta = _dapper.QuerySPDapper(querySp, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]") && queryRespuesta != "null")
                {
                    listaAptaWhatsApp = JsonConvert.DeserializeObject<List<PreWhatsAppResultadoConjuntoListaDTO>>(queryRespuesta);
                }

                return listaAptaWhatsApp;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Regulariza por nombre las ultimas campañas encontradas en Mailchimp
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio de regularizacion de Mailchimp</param>
        /// <param name="fechaFin">Fecha de fin de regularizacion de Mailchimp</param>
        /// <returns>Retorna booleano</returns>
        public bool RegularizarIdMailchimpFaltantePorActualizacion(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                string querySp = "mkt.SP_RegularizarIdMailchimpFaltante";
                var queryRespuesta = _dapper.QuerySPDapper(querySp, new { FechaInicio = fechaInicio, FechaFin = fechaFin });

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los datos completo para obtener la interaccion enlace mailchimp
        /// </summary>
        /// <param name="campaniaMailChimpId">Id original de la campania de Mailchimp</param>
        /// <returns>Lista de objetos de clase InteraccionEnlaceMailchimpBO</returns>
        public List<PrioridadMailChimpListaCorreoBO> PrioridadMailChimpListaCorreoCompletoPorCampaniaMailChimpId(string campaniaMailChimpId)
        {
            try
            {
                List<PrioridadMailChimpListaCorreoBO> listaPrioridadMailChimpListaCorreo = null;

                var query = "SELECT * FROM mkt.V_TPrioridadMailChimpListaCorreo_PorCampaniaMailChimpId WHERE IdCampaniaMailchimp = @campaniaMailChimpId";
                var queryRespuesta = _dapper.QueryDapper(query, new { campaniaMailChimpId });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    listaPrioridadMailChimpListaCorreo = JsonConvert.DeserializeObject<List<PrioridadMailChimpListaCorreoBO>>(queryRespuesta);
                }
                return listaPrioridadMailChimpListaCorreo;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos completo para obtener la interaccion enlace mailchimp
        /// </summary>
        /// <param name="listaMailChimpId">Id original de la lista de Mailchimp</param>
        /// <returns>Lista de objetos de clase PrioridadMailChimpListaCorreoBO</returns>
        public List<PrioridadMailChimpListaCorreoBO> PrioridadMailChimpListaCorreoCompletoPorListaMailChimpId(string listaMailChimpId)
        {
            try
            {
                List<PrioridadMailChimpListaCorreoBO> listaPrioridadMailChimpListaCorreo = null;

                var query = "SELECT * FROM mkt.V_TPrioridadMailChimpListaCorreo_PorCampaniaMailChimpId WHERE IdListaMailchimp = @listaMailChimpId";
                var queryRespuesta = _dapper.QueryDapper(query, new { listaMailChimpId });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    listaPrioridadMailChimpListaCorreo = JsonConvert.DeserializeObject<List<PrioridadMailChimpListaCorreoBO>>(queryRespuesta);
                }
                return listaPrioridadMailChimpListaCorreo;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
