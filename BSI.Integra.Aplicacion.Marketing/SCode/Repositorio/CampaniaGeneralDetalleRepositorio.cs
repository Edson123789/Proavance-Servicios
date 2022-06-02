using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: CampaniaGeneralDetalle
	/// Autor: Carlos Crispin - Gian Miranda
	/// Fecha: 12/05/2021
	/// <summary>
	/// Gestión de la tabla mkt.T_CampaniaGeneralDetalle
	/// </summary>
    public class CampaniaGeneralDetalleRepositorio : BaseRepository<TCampaniaGeneralDetalle, CampaniaGeneralDetalleBO>
    {
        #region Metodos Base
        public CampaniaGeneralDetalleRepositorio() : base()
        {
        }
        public CampaniaGeneralDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaGeneralDetalleBO> GetBy(Expression<Func<TCampaniaGeneralDetalle, bool>> filter)
        {
            IEnumerable<TCampaniaGeneralDetalle> listado = base.GetBy(filter);
            List<CampaniaGeneralDetalleBO> listadoBO = new List<CampaniaGeneralDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralDetalleBO objetoBO = Mapper.Map<TCampaniaGeneralDetalle, CampaniaGeneralDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaGeneralDetalleBO FirstById(int id)
        {
            try
            {
                TCampaniaGeneralDetalle entidad = base.FirstById(id);
                CampaniaGeneralDetalleBO objetoBO = new CampaniaGeneralDetalleBO();
                Mapper.Map<TCampaniaGeneralDetalle, CampaniaGeneralDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaGeneralDetalleBO FirstBy(Expression<Func<TCampaniaGeneralDetalle, bool>> filter)
        {
            try
            {
                TCampaniaGeneralDetalle entidad = base.FirstBy(filter);
                CampaniaGeneralDetalleBO objetoBO = Mapper.Map<TCampaniaGeneralDetalle, CampaniaGeneralDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaGeneralDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaGeneralDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampaniaGeneralDetalleBO> listadoBO)
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

        public bool Update(CampaniaGeneralDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaGeneralDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampaniaGeneralDetalleBO> listadoBO)
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
        private void AsignacionId(TCampaniaGeneralDetalle entidad, CampaniaGeneralDetalleBO objetoBO)
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

        private TCampaniaGeneralDetalle MapeoEntidad(CampaniaGeneralDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaGeneralDetalle entidad = new TCampaniaGeneralDetalle();
                entidad = Mapper.Map<CampaniaGeneralDetalleBO, TCampaniaGeneralDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CampaniaGeneralDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCampaniaGeneralDetalle, bool>>> filters, Expression<Func<TCampaniaGeneralDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TCampaniaGeneralDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<CampaniaGeneralDetalleBO> listadoBO = new List<CampaniaGeneralDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralDetalleBO objetoBO = Mapper.Map<TCampaniaGeneralDetalle, CampaniaGeneralDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene las plantillas y la información del Asesor
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Objeto de tipo CampaniaGeneralDetalleProgramaPlantillaDTO</returns>
        public CampaniaGeneralDetalleProgramaPlantillaDTO ObtenerPlantillasInformacionAsesor(int idCampaniaGeneralDetalle)
        {
            try
            {
                CampaniaGeneralDetalleProgramaPlantillaDTO plantillas = new CampaniaGeneralDetalleProgramaPlantillaDTO();
                string queryObtenerPlantillas = "SELECT IdCampaniaGeneralDetalle, IdCampaniaGeneral, Subject, CorreoElectronico, IdPersonal, NombreCompletoPersonal, CentralPersonal, AnexoPersonal, Contenido, Asunto, NombreCampania " +
                "FROM mkt.V_ObtenerPlantillaMailingInformacionAsesor WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle AND ClaveContenido = 'Texto'  AND ClaveAsunto = 'Asunto'";
                string registrosBD = _dapper.FirstOrDefault(queryObtenerPlantillas, new { @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    plantillas = JsonConvert.DeserializeObject<CampaniaGeneralDetalleProgramaPlantillaDTO>(registrosBD);
                }
                return plantillas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las plantillas y la información del Asesor
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <param name="contexto">Contexto padre para evitar problemas de multiples transacciones</param>
        /// <returns>Objeto de clase CampaniaGeneralDetalleBO</returns>
        public CampaniaGeneralDetalleBO BuscarCampaniaGeneralDetallePorId(int idCampaniaGeneralDetalle, integraDBContext contexto)
        {
            var campaniaGeneralDetalleResultado = new CampaniaGeneralDetalleBO();
            var spBusqueda = "[mkt].[SP_BuscarCampaniaGeneralDetallePorId]";
            var resultadoSp = _dapper.QuerySPFirstOrDefault(spBusqueda, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
            if (!string.IsNullOrEmpty(resultadoSp) && !resultadoSp.Contains("[]") && resultadoSp != "null")
            {
                campaniaGeneralDetalleResultado = JsonConvert.DeserializeObject<CampaniaGeneralDetalleBO>(resultadoSp);
                campaniaGeneralDetalleResultado.setIntegraDBContext(contexto);
            }

            return campaniaGeneralDetalleResultado;
        }

        /// <summary>
        /// Actualiza el estado EnEjecucion de la campania general detalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <param name="flagEjecucion">Booleano para determinar si pasar a estado de ejecucion o reposo</param>
        /// <param name="usuario">Usuario que realiza la actualizacion</param>
        public void ActualizarEstadoEjecucionCampaniaGeneralDetalle(int idCampaniaGeneralDetalle, bool flagEjecucion, string usuario)
        {
            try
            {
                string spQuery = "[mkt].[SP_ActualizarEstadoEjecucionCampaniaGeneralDetalle]";
                var query = _dapper.QuerySPDapper(spQuery, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, EnEjecucion = flagEjecucion, UsuarioModificacion = usuario });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actualiza Campania Mailing detalle, datos (CantidadContactos, UsuarioModificacion, FechaModificacion)
        /// </summary>
        /// <param name="campaniaGeneralDetalleActualizacion">Objeto de tipo CampaniaGeneralDetalleActualizacionDTO</param>
        /// <returns>Booleano con true si en caso fue exitoso</returns>
        public bool ActualizarDatosFiltroMailchimp(CampaniaGeneralDetalleActualizacionDTO campaniaGeneralDetalleActualizacion)
        {
            try
            {
                string spQuery = "[mkt].[SP_ActualizarCampaniaGeneralDetalleFiltro]";
                var query = _dapper.QuerySPDapper(spQuery, new { campaniaGeneralDetalleActualizacion.CantidadContactosMailing, campaniaGeneralDetalleActualizacion.CantidadContactosWhatsapp, campaniaGeneralDetalleActualizacion.UsuarioModificacion, campaniaGeneralDetalleActualizacion.Id });

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la cantidad de contactos de Mailing y WhatsApp dependiendo de la CampaniaGeneralDetalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Objeto de clase CampaniaGeneralDetalleResultadoDTO</returns>
        public CampaniaGeneralDetalleResultadoDTO ObtenerResultadoPorIdCampaniaGeneralDetalle(int idCampaniaGeneralDetalle)
        {
            try
            {
                var resultadoCantidad = new CampaniaGeneralDetalleResultadoDTO();

                string spQuery = "SELECT Id, CantidadContactosMailing, CantidadContactosWhatsapp, EnEjecucion FROM mkt.V_TCampaniaGeneralDetalleCantidadContacto WHERE Id = @IdCampaniaGeneralDetalle";
                var query = _dapper.FirstOrDefault(spQuery, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    resultadoCantidad = JsonConvert.DeserializeObject<CampaniaGeneralDetalleResultadoDTO>(query);
                }

                return resultadoCantidad;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para los botones de Mas Informacion
        /// </summary>
        /// <param name="idPrioridad">Id de la campania gneral detalle que se esta ejecutando (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de clase CampaniaGeneralDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerInformacionBotonesyEtiqueta(int idPrioridad)
        {
            try
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> nombresProgramas = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                string queryObtenerProgramas = "select Contenido, Etiqueta from mkt.V_ObtenerInformacionBotonyEtiquetaMailingGeneral " +
                    "WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle and ParametroSEONombre = @Descripcion";
                string registrosBD = _dapper.QueryDapper(queryObtenerProgramas, new
                {
                    @IdCampaniaGeneralDetalle = idPrioridad,
                    @Descripcion = "description"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    nombresProgramas = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return nombresProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los botones de whatsapp para Perú, Colombia y Bolivia
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objeto de tipo CampaniaMailingdetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerBotonesWhatsapp(int idCampaniaGeneralDetalle)
        {
            try
            {
                var campaniaGeneralDetalle = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                var resultado = _dapper.QuerySPDapper("mkt.SP_ObtenerBotonesWhatsappCampaniaGeneral", new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
                if (!string.IsNullOrEmpty(resultado))
                {
                    campaniaGeneralDetalle = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(resultado);
                }
                return campaniaGeneralDetalle;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Datos Empresa
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaDatosEmpresa(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> estructuraProgramas = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaDatosEmpresaCampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle AND " +
                    "TituloDocumento= @TipoDocumento";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle,
                    @TipoDocumento = "Datos Empresa"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    estructuraProgramas = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return estructuraProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Encabezado
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaEncabezado(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> estructuraProgramas = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaEncabezadoCampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle AND " +
                    "TituloDocumento= @TipoDocumento";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle,
                    @TipoDocumento = "Encabezado"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    estructuraProgramas = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return estructuraProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Pie de Pagina
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaDatosPiePagina(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> estructuraProgramas = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaPieDePaginaCampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle AND " +
                    "TituloDocumento= @TipoDocumento";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle,
                    @TipoDocumento = "Pie de p&#225;gina"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    estructuraProgramas = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return estructuraProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Pie de Pagina
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaRedesSociales(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> estructuraProgramas = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaRedesSocialesCampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle AND " +
                    "TituloDocumento= @TipoDocumento";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle,
                    @TipoDocumento = "Redes Sociales"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    estructuraProgramas = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return estructuraProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Pie de Pagina
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaImagenPrograma(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> estructuraProgramas = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                string _queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaImagenProgramaCampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle AND " +
                    "TituloDocumento= @TipoDocumento";
                string registrosBD = _dapper.QueryDapper(_queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle,
                    @TipoDocumento = "Imagen de programa"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    estructuraProgramas = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return estructuraProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Pie de Pagina
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaTextoComplemento1(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> estructuraProgramas = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                string _queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaTextoComplemento1CampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle AND " +
                    "TituloDocumento= @TipoDocumento";
                string registrosBD = _dapper.QueryDapper(_queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle,
                    @TipoDocumento = "Texto Complemento 1"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    estructuraProgramas = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return estructuraProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Pie de Pagina
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaTextoComplemento2(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> estructuraProgramas = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                string _queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaTextoComplemento2CampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle AND " +
                    "TituloDocumento= @TipoDocumento";
                string registrosBD = _dapper.QueryDapper(_queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle,
                    @TipoDocumento = "Texto Complemento 2"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    estructuraProgramas = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return estructuraProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Pie de Pagina
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaTextoComplemento3(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> textosComplemento3 = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaTextoComplemento3CampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle AND " +
                    "TituloDocumento= @TipoDocumento";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle,
                    @TipoDocumento = "Texto Complemento 3"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    textosComplemento3 = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return textosComplemento3;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los botones de whatsapp para Perú, Colombia y Bolivia
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objeto de tipo CampaniaMailingdetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerBotonesMessenger(int idCampaniaGeneralDetalle)
        {
            try
            {
                var campaniaGeneralDetalle = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                var resultado = _dapper.QuerySPDapper("mkt.SP_ObtenerBotonesMessengerCampaniaGeneral", new { idCampaniaGeneralDetalle });
                if (!string.IsNullOrEmpty(resultado))
                {
                    campaniaGeneralDetalle = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(resultado);
                }
                return campaniaGeneralDetalle;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los expositores por programa general
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de tipo CampaniaGeneralDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerExpositoresPorProgramaGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                var campaniaGeneralDetalle = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                var query = @"
                            SELECT DISTINCT Etiqueta,
		                            Contenido
                            FROM [mkt].[V_ObtenerInformacionDocentePorCampaniaGeneralDetalle]
                            WHERE IdCampaniaGeneral = @idCampaniaGeneralDetalle";

                var expositoresDB = _dapper.QueryDapper(query, new { idCampaniaGeneralDetalle });
                if (!string.IsNullOrEmpty(expositoresDB))
                {
                    campaniaGeneralDetalle = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(expositoresDB);
                }
                return campaniaGeneralDetalle.GroupBy(x => x.Etiqueta).Select(x => new CampaniaGeneralDetalleContenidoEtiquetaDTO
                {
                    Etiqueta = x.Key,
                    Contenido = string.Join(" ", x.Select(y => y.Contenido))
                }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene datos basicos del programa general por el Id Campania General Detalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Objeto de tipo ProgramaGeneralFiltroDTO</returns>
        public ProgramaGeneralFiltroDTO ObtenerProgramaGeneralPorIdCampaniaGeneralDetalle(int idCampaniaGeneralDetalle)
        {
            try
            {
                var informacionPGeneral = new ProgramaGeneralFiltroDTO();
                var spInformacionPGeneral = "mkt.SP_ObtenerProgramaGeneralPorIdCampaniaGeneralDetalle";

                var informacionPGeneralDb = _dapper.QuerySPFirstOrDefault(spInformacionPGeneral, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
                
                if (!string.IsNullOrEmpty(informacionPGeneralDb) && informacionPGeneralDb != "[]" && informacionPGeneralDb != "null")
                {
                    informacionPGeneral = JsonConvert.DeserializeObject<ProgramaGeneralFiltroDTO>(informacionPGeneralDb);
                }

                return informacionPGeneral;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene el Id de T_PGeneral y la Etiqueta
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos CampaniaMailingPGeneralEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerProgramaYEtiqueta(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> listaPGeneralEtiqueta = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();
                string queryObtenerPGeneralEtiqueta = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerProgramaYEtiquetaCampaniaGeneral " +
                    "WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle";
                string registrosBD = _dapper.QueryDapper(queryObtenerPGeneralEtiqueta, new
                {
                    @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle
                });

                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    listaPGeneralEtiqueta = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return listaPGeneralEtiqueta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actualiza la cantidad de contactos resultantes de la ejecucion de preprocesamiento de whatsapp y actualiza el registro del detalle de la campania general
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Entero con la cantidad de contactos resultantes</returns>
        public int ObtenerCantidadContactosWhatsAppCampaniaGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                ValorIntDTO resultado = new ValorIntDTO();
                var spCantidadContactosWhatsApp = "[mkt].[SP_ObtenerCantidadContactosWhatsAppCampaniaGeneral]";
                var resultadoSp = _dapper.QuerySPFirstOrDefault(spCantidadContactosWhatsApp, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
                if (!string.IsNullOrEmpty(resultadoSp) && !resultadoSp.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoSp);
                }
                return resultado.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina el preprocesamiento de WhatsApp de la campania general detalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <param name="usuarioResponsable">Usuario responsable del elminado</param>
        /// <returns>Booleano con true si en caso fue exitoso</returns>
        public bool EliminarValidacionWhatsAppIdCampaniaGeneralDetalle(int idCampaniaGeneralDetalle, string usuarioResponsable)
        {
            try
            {
                var spEliminarValidacion = "[mkt].[SP_EliminarValidacionWhatsAppIdCampaniaGeneralDetalle]";
                var resultadoSp = _dapper.QuerySPFirstOrDefault(spEliminarValidacion, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, Usuario = usuarioResponsable });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<AlumnoInformacionBasicaDTO> ObtenerAlumnosPorCampaniaGeneralDetalle(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<AlumnoInformacionBasicaDTO> alumnoResultado = new List<AlumnoInformacionBasicaDTO>();

                var spObtenerAlumnos = "[mkt].[SP_ObtenerAlumnosPorCampaniaGeneralDetalle]";
                var resultadoSp = _dapper.QuerySPDapper(spObtenerAlumnos, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });

                if (!string.IsNullOrEmpty(resultadoSp) && !resultadoSp.Contains("[]"))
                {
                    alumnoResultado = JsonConvert.DeserializeObject<List<AlumnoInformacionBasicaDTO>>(resultadoSp);
                }

                return alumnoResultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las listas que pertenecen a una campania general
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Lista de objetos de clase CampaniaGeneralDetalleBO</returns>
        public List<CampaniaGeneralDetalleBO> ObtenerPorCampaniaGeneral(int idCampaniaGeneral)
        {
            try
            {
                return this.GetBy(x => x.IdCampaniaGeneral == idCampaniaGeneral && x.CantidadContactosMailing != 0).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene las listas que pertenecen a una campania general y no estan en ejecucion
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Lista de objetos de clase CampaniaGeneralDetalleBO</returns>
        public List<CampaniaGeneralDetalleBO> ObtenerDetallePorCampaniaGeneralNoEjecucion(int idCampaniaGeneral)
        {
            try
            {
                List<CampaniaGeneralDetalleBO> listaCampaniaGeneralDetalle = new List<CampaniaGeneralDetalleBO>();
                string queryCampaniaGeneralDetalle = "SELECT VTCGD.* FROM mkt.V_TCampaniaGeneralDetalle AS VTCGD WHERE VTCGD.IdCampaniaGeneral = @IdCampaniaGeneral";

                string registrosBD = _dapper.QueryDapper(queryCampaniaGeneralDetalle, new
                {
                    @IdCampaniaGeneral = idCampaniaGeneral
                });

                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    listaCampaniaGeneralDetalle = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleBO>>(registrosBD);
                }

                return listaCampaniaGeneralDetalle;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
