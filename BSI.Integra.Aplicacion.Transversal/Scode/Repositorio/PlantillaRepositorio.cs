using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/Plantilla
    /// Autor: Wilber Choque - Esthephany Tanco - Fischer Valdez - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_Plantilla
    /// </summary>
    public class PlantillaRepositorio : BaseRepository<TPlantilla, PlantillaBO>
    {
        #region Metodos Base
        public PlantillaRepositorio() : base()
        {
        }
        public PlantillaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlantillaBO> GetBy(Expression<Func<TPlantilla, bool>> filter)
        {
            IEnumerable<TPlantilla> listado = base.GetBy(filter);
            List<PlantillaBO> listadoBO = new List<PlantillaBO>();
            foreach (var itemEntidad in listado)
            {
                PlantillaBO objetoBO = Mapper.Map<TPlantilla, PlantillaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlantillaBO FirstById(int id)
        {
            try
            {
                TPlantilla entidad = base.FirstById(id);
                PlantillaBO objetoBO = new PlantillaBO();
                Mapper.Map<TPlantilla, PlantillaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlantillaBO FirstBy(Expression<Func<TPlantilla, bool>> filter)
        {
            try
            {
                TPlantilla entidad = base.FirstBy(filter);
                PlantillaBO objetoBO = Mapper.Map<TPlantilla, PlantillaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlantillaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlantillaBO> listadoBO)
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

        public bool Update(PlantillaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlantillaBO> listadoBO)
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
        private void AsignacionId(TPlantilla entidad, PlantillaBO objetoBO)
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

        private TPlantilla MapeoEntidad(PlantillaBO objetoBO)
        {
            try
            {
                //Crea la entidad padre
                TPlantilla entidad = new TPlantilla();
                entidad = Mapper.Map<PlantillaBO, TPlantilla>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //Mapea los hijos
                if (objetoBO.PlantillaClaveValor != null && objetoBO.PlantillaClaveValor.Count > 0)
                {
                    foreach (var hijo in objetoBO.PlantillaClaveValor)
                    {
                        TPlantillaClaveValor entidadHijo = new TPlantillaClaveValor();
                        entidadHijo = Mapper.Map<PlantillaClaveValorBO, TPlantillaClaveValor>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPlantillaClaveValor.Add(entidadHijo);
                    }
                }

                if (objetoBO.FaseByPlantilla != null && objetoBO.FaseByPlantilla.Count > 0)
                {
                    foreach (var hijo in objetoBO.FaseByPlantilla)
                    {
                        TFaseByPlantilla entidadHijo = new TFaseByPlantilla();
                        entidadHijo = Mapper.Map<FaseByPlantillaBO, TFaseByPlantilla>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TFaseByPlantilla.Add(entidadHijo);
                    }
                }

                if (objetoBO.ListaPlantillaAsociacionModuloSistema != null && objetoBO.ListaPlantillaAsociacionModuloSistema.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaPlantillaAsociacionModuloSistema)
                    {
                        TPlantillaAsociacionModuloSistema entidadHijo = new TPlantillaAsociacionModuloSistema();
                        entidadHijo = Mapper.Map<PlantillaAsociacionModuloSistemaBO, TPlantillaAsociacionModuloSistema>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPlantillaAsociacionModuloSistema.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        /// <summary>
        /// Obtiene una lista con los registros de la tabla mkt.T_Plantilla
        /// </summary>
        /// <returns>Lista de objetos de clase PlantillaDTO</returns>
        public List<PlantillaDTO> ObtenerListaPlantilla()
        {
            try
            {
                string query = "select Id, Nombre from mkt.V_TPlantilla_Nombre where Estado = 1";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<PlantillaDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<PlantillaDTO>>(responseQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de plantilla de marketing para el envio de correo
        /// </summary>
        /// <returns>Lista de objetos de clase PlantillaDTO</returns>
        public List<PlantillaDTO> ObtenerListaPlantillaMarketing()
        {
            try
            {
                List<PlantillaDTO> resultadoFinal = new List<PlantillaDTO>();

                string query = "SELECT Id, Nombre FROM mkt.V_TPlantilla_CorreoMarketing";
                var respuesta = _dapper.QueryDapper(query, null);
                
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<PlantillaDTO>>(respuesta);
                }

                return resultadoFinal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista con los registros de la tabla mkt.T_Plantilla
        /// </summary>
        /// <returns></returns>
        public List<PlantillaDTO> ObtenerListaPlantillaCertificado()
        {
            try
            {
                string query = "select Id,IdPlantillaBase,Nombre from mkt.V_TPlantilla_Certificado where Estado = 1";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<PlantillaDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<PlantillaDTO>>(responseQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista con todos los registro de Plantilla 
        /// </summary>
        /// <returns></returns>
        public List<PlantillaDatoDTO> ObtenerListarPlantilla()
        {
            try
            {
                var listaPlantilla = new List<PlantillaDatoDTO>();

                var _query = @"
                    SELECT Id, 
                           Nombre, 
                           Descripcion, 
                           IdPlantillaBase, 
                           NombrePlantillaBase, 
                           EstadoAgenda, 
                           Documento, 
                           IdPersonalAreaTrabajo, 
                           NombrePersonalAreaTrabajo,
                           Estado
                    FROM mkt.V_ObtenerPlantilla
                    WHERE EstadoPlantilla = 1
                          AND EstadoPlantillaBase = 1
                          AND EstadoPersonalAreaTrabajo = 1
                    ORDER BY FechaCreacionPlantilla DESC
                    ";
                var query = _dapper.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    listaPlantilla = JsonConvert.DeserializeObject<List<PlantillaDatoDTO>>(query);
                }
                return listaPlantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene una el registro de Plantilla 
        /// </summary>
        /// <returns></returns>
        public DatosPlantillaDTO ObtenerPlantillaPorId(int IdPlantilla)
        {
            try
            {
                DatosPlantillaDTO Plantilla = new DatosPlantillaDTO();
                var Query = string.Empty;
                Query = "SELECT Descripcion FROM mkt.T_Plantilla WHERE Estado = 1 AND Id=@IdPlantilla";
                var PlantillaDB = _dapper.FirstOrDefault(Query, new { IdPlantilla });
                if (!string.IsNullOrEmpty(PlantillaDB) && !PlantillaDB.Contains("[]") && PlantillaDB != "null")
                {
                    Plantilla = JsonConvert.DeserializeObject<DatosPlantillaDTO>(PlantillaDB);
                }
                return Plantilla;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene todas las Plantillas "TIPO: SPEECH" (para llenado de ComboBox).
        /// </summary>
        /// <returns></returns>
        public List<PlantillaDTO> ObtenerAllPlantillaSpeech()
        {
            try
            {
                List<PlantillaDTO> Plantilla = new List<PlantillaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM mkt.T_Plantilla WHERE Estado = 1 AND IdPlantillaBase=1";
                var PlantillaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(PlantillaDB) && !PlantillaDB.Contains("[]"))
                {
                    Plantilla = JsonConvert.DeserializeObject<List<PlantillaDTO>>(PlantillaDB);
                }
                return Plantilla;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene todas las Plantillas "TIPO: SPEECH_DESPEDIDA" (para llenado de ComboBox).
        /// </summary>
        /// <returns></returns>
        public List<PlantillaDTO> ObtenerAllPlantillaSpeechDespedida()
        {
            try
            {
                List<PlantillaDTO> Plantilla = new List<PlantillaDTO>();
                var _query = string.Empty;
                _query = "select P.Id, P.Nombre from mkt.T_Plantilla P  " +
                    "left join pla.T_PlantillaBase PB on PB.Id=P.IdPlantillaBase  " +
                    "where PB.Nombre like 'speech despedida'";
                var PlantillaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(PlantillaDB) && !PlantillaDB.Contains("[]"))
                {
                    Plantilla = JsonConvert.DeserializeObject<List<PlantillaDTO>>(PlantillaDB);
                }
                return Plantilla;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        ///  Obtiene todas las Plantillas "TIPO: EMAIL" que a su vez contengan tipos de nombres especificos (para llenado de ComboBox de BncObsoletos).
        /// </summary>
        /// <param></param>
        /// <returns>Id, Nombre</returns> 
        public List<PlantillaDTO> ObtenerAllPlantillaEmailAlterado()
        {
            try
            {
                List<PlantillaDTO> Plantilla = new List<PlantillaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM mkt.T_Plantilla WHERE Estado = 1 AND IdPlantillaBase=2 AND Nombre in(";

                string[] nombres = {
                    "Correo Informacion del curso",
                    "Correo Confirmación de Participación",
                    "Correo de Propuesta de Pago",
                    "Correo Promociones",
                    "Correo Promocion Muy Alta",
                    "Ficha de Matricula",
                    "Confirmación de participación",
                    "Accesos Temporales",
                    "Condiciones y Características - Aonline Colombia",
                    "Condiciones y Caracteristicas - Aonline Perú",
                    "Pago Tarjeta Mastercard Amex Diners - Extranjero",
                    "Pago Tarjeta Mastercard Amex Diners - Perú",
                    "Pago Tarjeta Visa - Extranjero",
                    "Pago Tarjeta Visa - Perú",
                    "Pago Tarjeta Visa Mastercard Amex- Colombia",
                    "Propuesta Personalizada de Pagos + Link 1° Clase",
                    "Propuesta Personalizada de Pagos",
                    "Solicitud de Telefono de Contacto",
                    "Codigo de Alumno para Pago Colombia",
                    "Codigo de Alumno para Pago Perú"
                };

                for (int i = 0; i < nombres.Length; ++i)
                    if (i == 0) _query += "'" + nombres[i] + "'";
                    else _query += ",'" + nombres[i] + "'";

                _query += ")";


                var PlantillaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(PlantillaDB) && !PlantillaDB.Contains("[]"))
                {
                    Plantilla = JsonConvert.DeserializeObject<List<PlantillaDTO>>(PlantillaDB);
                }
                return Plantilla;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Repositorio: PlantillaRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene la plantilla inicial para envio de correo masivo por operaciones
        /// </summary>
        /// <param name="id">Id de la plantilla a buscar</param>
        /// <returns>Objeto del tipo PlantillaBaseCorreoOperacionesDTO</returns>
        public PlantillaBaseCorreoOperacionesDTO ObtenerPlantillaCorreo(int id)
        {
            try
            {

                var listaPlantillaClaveValor = new List<PlantillaClaveValorDTO>();

                var query = $@"
                                SELECT Id, 
                                       Clave, 
                                       Valor, 
                                       IdPlantilla
                                FROM mkt.T_PlantillaClaveValor
                                WHERE IdPlantilla = @id
                                      AND Estado = 1
                            ";

                var plantillaDB = _dapper.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(plantillaDB) && !plantillaDB.Contains("[]"))
                {
                    listaPlantillaClaveValor = JsonConvert.DeserializeObject<List<PlantillaClaveValorDTO>>(plantillaDB);
                }

                var asunto = listaPlantillaClaveValor.Where(x => x.Clave == "Asunto").FirstOrDefault();
                var asuntoValor = "";
                if (asunto != null) asuntoValor = asunto.Valor;

                var cuerpo = listaPlantillaClaveValor.Where(x => x.Clave == "Texto").FirstOrDefault();
                var cuerpoValor = "";
                if (cuerpo != null) cuerpoValor = cuerpo.Valor;

                return new PlantillaBaseCorreoOperacionesDTO()
                {
                    Asunto = asuntoValor,
                    Cuerpo = cuerpoValor
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene plantilla para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                //return this.GetBy(x => x.Estado, x => new FiltroDTO {Id = x.Id, Nombre = x.Nombre }).ToList();
                string query = @"
                    SELECT Plantilla.Id AS Id, 
                           concat(Plantilla.Nombre, ' - ', PlantillaBase.Nombre) AS Nombre
                    FROM mkt.T_Plantilla AS Plantilla
                         INNER JOIN pla.T_PlantillaBase AS PlantillaBase ON Plantilla.IdPlantillaBase = PlantillaBase.Id
                    WHERE Plantilla.Estado = 1
                        ";
                string queryRespuesta = _dapper.QueryDapper(query, new { });
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtiene plantilla para filtro GP
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltroGP()
        {
            try
            {
                //return this.GetBy(x => x.Estado, x => new FiltroDTO {Id = x.Id, Nombre = x.Nombre }).ToList();
                string query = @"
                    SELECT Plantilla.Id AS Id, 
                           concat(Plantilla.Nombre, ' - ', PlantillaBase.Nombre) AS Nombre
                    FROM mkt.T_Plantilla AS Plantilla
                         INNER JOIN pla.T_PlantillaBase AS PlantillaBase ON Plantilla.IdPlantillaBase = PlantillaBase.Id
                    WHERE Plantilla.Estado = 1 AND IdPersonalAreaTrabajo = 5 AND IdPlantillaBase = 2
                        ";
                string queryRespuesta = _dapper.QueryDapper(query, new { });
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtiene plantilla para filtro GP
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltroGPWhatsApp()
        {
            try
            {
                //return this.GetBy(x => x.Estado, x => new FiltroDTO {Id = x.Id, Nombre = x.Nombre }).ToList();
                string query = @"
                    SELECT Plantilla.Id AS Id, 
                           concat(Plantilla.Nombre, ' - ', PlantillaBase.Nombre) AS Nombre
                    FROM mkt.T_Plantilla AS Plantilla
                         INNER JOIN pla.T_PlantillaBase AS PlantillaBase ON Plantilla.IdPlantillaBase = PlantillaBase.Id
                    WHERE Plantilla.Estado = 1 AND IdPersonalAreaTrabajo = 5 AND IdPlantillaBase = 8
                        ";
                string queryRespuesta = _dapper.QueryDapper(query, new { });
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene las plantillas a calcular etiqueta
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerPlantillaCalcularEtiqueta()
        {
            try
            {
                //return this.GetBy(x => x.Estado, x => new FiltroDTO {Id = x.Id, Nombre = x.Nombre }).ToList();
                //string query = @"
                //SELECT Plantilla.Id AS Id, 
                //       concat(Plantilla.Nombre, ' - ', PlantillaBase.Nombre) AS Nombre
                //FROM mkt.T_Plantilla AS Plantilla
                //     INNER JOIN pla.T_PlantillaBase AS PlantillaBase ON Plantilla.IdPlantillaBase = PlantillaBase.Id
                //WHERE Plantilla.Estado = 1
                //      AND Plantilla.Id IN(1, 1013, 1119, 1133);
                //        ";

                string query = @"
                SELECT Plantilla.Id AS Id, 
                       concat(Plantilla.Nombre, ' - ', PlantillaBase.Nombre) AS Nombre
                FROM mkt.T_Plantilla AS Plantilla
                     INNER JOIN pla.T_PlantillaBase AS PlantillaBase ON Plantilla.IdPlantillaBase = PlantillaBase.Id
                WHERE Plantilla.Estado = 1
                AND Plantilla.Id NOT IN(1000, 1032)
                AND Plantilla.Id IN(1001, 1017, 1022, 1023, 1025, 1028, 1037, 1044, 1048, 1059, 1060, 1063, 1068, 1078, 1080, 1083, 1090, 1091, 1095, 1100, 1104, 1105, 1107, 1114, 1136, 1149);

                        ";

                string queryRespuesta = _dapper.QueryDapper(query, new { });
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(queryRespuesta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la plantilla clave valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PlantillaClaveValorDTO> ObtenerPlantillaClaveValor(int id)
        {
            try
            {

                var listaPlantillaClaveValor = new List<PlantillaClaveValorDTO>();

                var _query = $@"
                                SELECT Id, 
                                       Clave, 
                                       Valor, 
                                       IdPlantilla
                                FROM mkt.T_PlantillaClaveValor
                                WHERE IdPlantilla = @id
                                      AND Estado = 1
                            ";

                var PlantillaDB = _dapper.QueryDapper(_query, new { id });
                if (!string.IsNullOrEmpty(PlantillaDB) && !PlantillaDB.Contains("[]"))
                {
                    listaPlantillaClaveValor = JsonConvert.DeserializeObject<List<PlantillaClaveValorDTO>>(PlantillaDB);
                }
                return listaPlantillaClaveValor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int ObtenerPlantillaCertificadoModular()
        {
            try
            {
                ValorIntDTO valor = new ValorIntDTO();
                string query = "select Id as Valor from mkt.V_TPlantilla_CertificadoModular";
                string queryRespuesta = _dapper.FirstOrDefault(query, new { });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("null"))
                {
                    valor = JsonConvert.DeserializeObject<ValorIntDTO>(queryRespuesta);
                }
                return valor.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PlantillaDTO> ObtenerListaPlantillasCertificadoConstancia()
        {
            try
            {
                string query = "select Id,IdPlantillaBase,Nombre from [mkt].[V_TPlantilla_CertificadoConstancia] where Estado = 1";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<PlantillaDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<PlantillaDTO>>(responseQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Plantillas de correo para Operaciones
        /// </summary>        
        public List<PlantillaDTO> ObtenerListaPlantillasCorreos()
        {
            try
            {
                string query = "select Id, Nombre, 3 as idTipoEnvio from mkt.V_TPlantilla_Nombre_CorreoOperaciones where Estado = 1";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<PlantillaDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<PlantillaDTO>>(responseQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Plantillas de WhatsApp para Operaciones
        /// </summary>        
        public List<PlantillaDTO> ObtenerListaPlantillasWhatsApp()
        {
            try
            {
                string query = "select Id, Nombre, 1 as idTipoEnvio from mkt.V_TPlantilla_Nombre_WhatsAppOperaciones where Estado = 1";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<PlantillaDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<PlantillaDTO>>(responseQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Plantillas de WhatsApp para Operaciones
        /// </summary>        
        public List<PlantillaDTO> ObtenerListaPlantillasSms()
        {
            try
            {
                string query = "select Id, Nombre, 2 as idTipoEnvio from mkt.V_TPlantilla_Nombre_SMSOperaciones where Estado = 1";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<PlantillaDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<PlantillaDTO>>(responseQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Plantillas de WhatsApp para Operaciones
        /// </summary>        
        public List<PlantillaDTO> ObtenerListaPlantillasConfiguracionEnvio()
        {
            try
            {
                string query = "select Id, Nombre, idTipoEnvio from mkt.V_TPlantilla_Nombre_ConfiguracionEnvio where Estado = 1 order by IdPlantillaBase ";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<PlantillaDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<PlantillaDTO>>(responseQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        // <summary>
        /// Obtiene Plantillas de WhatsApp para Operaciones
        /// </summary>        
        public List<PlantillaDTO> ObtenerListaPlantillasWhatsAppGeneral()
        {
            try
            {
                string query = "select Id, Nombre from mkt.V_TPlantilla_Nombre_WhatsApp where Estado = 1";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<PlantillaDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<PlantillaDTO>>(responseQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
