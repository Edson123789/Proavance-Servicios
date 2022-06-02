using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.DTO;
using System.Linq;
using BSI.Integra.Aplicacion.DTO;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: PlantillaClaveValorRepositorio
    /// Autor: _ _ _ _ _ _ _ _ _ .
    /// Fecha: 30/04/2021
    /// <summary>
    /// Repositorio para consultas de T_PlantillaClaveValor
    /// </summary>
    public class PlantillaClaveValorRepositorio : BaseRepository<TPlantillaClaveValor, PlantillaClaveValorBO>
    {
        #region Metodos Base
        public PlantillaClaveValorRepositorio() : base()
        {
        }
        public PlantillaClaveValorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PlantillaClaveValorBO> GetBy(Expression<Func<TPlantillaClaveValor, bool>> filter)
        {
            IEnumerable<TPlantillaClaveValor> listado = base.GetBy(filter);
            List<PlantillaClaveValorBO> listadoBO = new List<PlantillaClaveValorBO>();
            foreach (var itemEntidad in listado)
            {
                PlantillaClaveValorBO objetoBO = Mapper.Map<TPlantillaClaveValor, PlantillaClaveValorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PlantillaClaveValorBO FirstById(int id)
        {
            try
            {
                TPlantillaClaveValor entidad = base.FirstById(id);
                PlantillaClaveValorBO objetoBO = new PlantillaClaveValorBO();
                Mapper.Map<TPlantillaClaveValor, PlantillaClaveValorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PlantillaClaveValorBO FirstBy(Expression<Func<TPlantillaClaveValor, bool>> filter)
        {
            try
            {
                TPlantillaClaveValor entidad = base.FirstBy(filter);
                PlantillaClaveValorBO objetoBO = Mapper.Map<TPlantillaClaveValor, PlantillaClaveValorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PlantillaClaveValorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPlantillaClaveValor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PlantillaClaveValorBO> listadoBO)
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

        public bool Update(PlantillaClaveValorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPlantillaClaveValor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PlantillaClaveValorBO> listadoBO)
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
        private void AsignacionId(TPlantillaClaveValor entidad, PlantillaClaveValorBO objetoBO)
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

        private TPlantillaClaveValor MapeoEntidad(PlantillaClaveValorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPlantillaClaveValor entidad = new TPlantillaClaveValor();
                entidad = Mapper.Map<PlantillaClaveValorBO, TPlantillaClaveValor>(objetoBO,
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
        /// Obtiene Plantillas Para Agenda
        /// </summary>
        /// <param name="idFaseOportunidad"></param>
        /// <returns></returns>
        public List<ContenidoPlantillaDTO> ObtenerPlantillas(int idFaseOportunidad)
        {
            try
            {
                string _queryContenidoPlantilla = "Select IdPlantilla AS Id,IdPlantillaClaveValor,Clave,Valor,IdAreaEtiqueta from com.V_PlantillasAgendaPantalla2Detalle Where Estado=1 and IdFaseOrigen=@IdFaseOportunidad or IdPlantillaBase in (1,6)";
                var queryContenidoPlantilla = _dapper.QueryDapper(_queryContenidoPlantilla, new { IdFaseOportunidad=idFaseOportunidad });
                return JsonConvert.DeserializeObject<List<ContenidoPlantillaDTO>>(queryContenidoPlantilla);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ContenidoPlantillaDTO> ObtenerPlantillasWP()
        {
            try
            {
                string _queryContenidoPlantilla = "Select IdPlantilla AS Id,Nombre,Valor,IdPlantillaClaveValor from com.V_PlantillasAgendaPantalla2Detalle Where Estado=1 and clave='Texto' and Nombre not  like'%Lista%' and IdPlantillaBase in (7,8)  group by  IdPlantilla,Nombre,Valor,IdPlantillaClaveValor";
                var queryContenidoPlantilla = _dapper.QueryDapper(_queryContenidoPlantilla, null);
                return JsonConvert.DeserializeObject<List<ContenidoPlantillaDTO>>(queryContenidoPlantilla);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ContenidoPlantillaDTO> ObtenerPlantillasCorreo()
        {
            try
            {
                string _queryContenidoPlantilla = "Select IdPlantilla AS Id,Nombre,Valor,IdPlantillaClaveValor from com.V_PlantillasAgendaPantalla2Detalle Where Estado=1 and clave='Texto' and Nombre not  like'%Lista%' and IdPlantillaBase in (2,3,6)  group by  IdPlantilla,Nombre,Valor,IdPlantillaClaveValor";
                var queryContenidoPlantilla = _dapper.QueryDapper(_queryContenidoPlantilla, null);
                return JsonConvert.DeserializeObject<List<ContenidoPlantillaDTO>>(queryContenidoPlantilla);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ContenidoPlantillaDTO> ObtenerTodoPlantillasMailing()
        {
            try
            {
                string _queryContenidoPlantilla = "Select IdPlantilla AS Id,Nombre,Valor,IdPlantillaClaveValor from com.V_PlantillasAgendaPantalla2Detalle Where Estado=1 and clave='Texto' and Nombre not  like'%Lista%' and IdFaseOrigen IS NOT NULL  OR (IdPlantillaBase IN(6))  group by  IdPlantilla,Nombre,Valor,IdPlantillaClaveValor";
                var queryContenidoPlantilla = _dapper.QueryDapper(_queryContenidoPlantilla, null);
                return JsonConvert.DeserializeObject<List<ContenidoPlantillaDTO>>(queryContenidoPlantilla);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Fischer Valdez
        /// Fecha: 18/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el monto de los cursos relacionados
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="idEtiqueta">Id de la lista curso area(PK de la tabla mkt.T_ListaCursoAreaEtiqueta)</param>
        /// <returns>String</returns>
        public List<CursosRelacionadosDTO> ObtenerMontosCursosRelacionados(int idOportunidad,int idEtiqueta)
        {
            try
            {
                string queryListaProgramasMonto = "pla.SP_ObtenerListaCursosRelacionadosMontos";
                var resultadoListaProgramasMonto = _dapper.QuerySPDapper(queryListaProgramasMonto, new { Idoportunidad = idOportunidad, IdEtiqueta = idEtiqueta });//solo Proyectos
                return JsonConvert.DeserializeObject<List<CursosRelacionadosDTO>>(resultadoListaProgramasMonto);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ProblemaCausaDTO> ObtenerPlantillaCausaProblema(int idOportunidad)
        {
            try
            {
                var queryListaProblemasCausa = _dapper.QuerySPDapper("com.GetListaProblemaCausaByOportunidad", new { Idoportunidad = idOportunidad });
                return  JsonConvert.DeserializeObject<List<ProblemaCausaDTO>>(queryListaProblemasCausa);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PlantillaClaveValorRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene Plantilla de Cursos Relacionados
        /// </summary>
        /// <param name="idCentroCosto">Id de Centro de Costo </param>
        /// <returns> List<PGeneralCursosRelacionadosDTO> </returns>
        public List<PGeneralCursosRelacionadosDTO> ObtenerCursosRelacionadosPlantilla(int idCentroCosto)
        {
            try
            {
                var queryGetUrlCursosRelacionados = _dapper.QuerySPDapper("com.SP_GetUrlCursosRelacionadosForMails", new { IdCentroCosto = idCentroCosto });
                return JsonConvert.DeserializeObject<List<PGeneralCursosRelacionadosDTO>>(queryGetUrlCursosRelacionados);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PlantillaClaveValorRepositorio
        /// Autor:
        /// Fecha: 18/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas disponibles por fase
        /// </summary>
        /// <param name="idFaseOportunidad"> Id de Fase de Oportunidad</param>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerPlantillaGenerarMensaje(int idFaseOportunidad)
        {
            try
            {
                string queryPlantillaFase = "Select Id, Nombre from mkt.V_PlantillaPorFaseOportunidad  Where IdFaseOrigen = @IdFaseOportunidad";
                var resultado = _dapper.QueryDapper(queryPlantillaFase, new { IdFaseOportunidad = idFaseOportunidad });
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene las plantillas mailing disponibles para operaciones
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerPlantillaGenerarMensajeOperaciones()
        {
            try
            {
                string query = $@"
                                    SELECT Id, 
                                            Nombre
                                    FROM mkt.V_ObtenerPlantillaDisponibleMailingOperaciones
                                    WHERE Estado = 1;
                                    ";
                var resultadoQuery = _dapper.QueryDapper(query, new { });
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(resultadoQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PlantillaWhatsAppDTO> ObtenterPlantillaWhatsApp()
        {
            try
            {
                string _queryplantillabyfase = "SELECT PL.Id ,PL.Nombre, PL.Descripcion, PB.Id AS TipoPlantilla, PCV.Valor AS Contenido " +
                                               "FROM mkt.T_Plantilla AS PL " +
                                               "INNER JOIN pla.T_PlantillaBase AS PB ON PL.IdPlantillaBase=PB.Id " +
                                               "INNER JOIN mkt.T_PlantillaClaveValor AS PCV ON PL.Id=PCV.IdPlantilla " +
                                               "WHERE PB.Nombre like '%WhatsApp%' AND PL.Estado=1";
                var queryplantillabyfase = _dapper.QueryDapper(_queryplantillabyfase, null);
                return JsonConvert.DeserializeObject<List<PlantillaWhatsAppDTO>>(queryplantillabyfase);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 20/01/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de plantillas SMS con sus claves y valores
        /// </summary>
        /// <returns>Lista de objetos de clase SmsPlantillaClaveValorDTO</returns>
        public List<SmsPlantillaClaveValorDTO> ObtenerPlantillaSmsMarketing()
        {
            try
            {
                var listaResultadoReporte = new List<SmsPlantillaClaveValorDTO>();

                var queryVista = "SELECT Id, Nombre, Descripcion, IdPlantillaBase, Contenido FROM com.V_ObtenerPlantillaClaveValorSms";

                var resultadoReporte = _dapper.QueryDapper(queryVista, null);

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]") && resultadoReporte != "null")
                {
                    listaResultadoReporte = JsonConvert.DeserializeObject<List<SmsPlantillaClaveValorDTO>>(resultadoReporte);
                }

                return listaResultadoReporte;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<PlantillaWhatsAppDTO> ObtenterPlantillaCorreo()
        {
            try
            {
                string _queryplantillabyfase = "SELECT PL.Id ,PL.Nombre, PL.Descripcion, PB.Id AS TipoPlantilla, PCV.Valor AS Contenido " +
                                               "FROM mkt.T_Plantilla AS PL " +
                                               "INNER JOIN pla.T_PlantillaBase AS PB ON PL.IdPlantillaBase=PB.Id " +
                                               "INNER JOIN mkt.T_PlantillaClaveValor AS PCV ON PL.Id=PCV.IdPlantilla " +
                                               "WHERE PB.Nombre not like '%WhatsApp%' AND PL.Estado=1";
                var queryplantillabyfase = _dapper.QueryDapper(_queryplantillabyfase, null);
                return JsonConvert.DeserializeObject<List<PlantillaWhatsAppDTO>>(queryplantillabyfase);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las plantillas para WhatsApp por módulo
        /// </summary>
        /// <param name="idModuloSistema">Id del modulo del sistema (PK de la tabla conf.T_ModuloSistema)</param>
        /// <returns>Lista de objetos de clase PlantillaWhatsAppDTO</returns>
        public List<PlantillaWhatsAppDTO> ObtenterPlantillaWhatsApp_PorIdModuloSistema(int idModuloSistema)
        {
            try
            {
                string queryplantillabyfase = "SELECT Id , Nombre, Descripcion, TipoPlantilla, Contenido " +
                                               "FROM mkt.V_ObtenerPlantilla_ModuloSistema " +
                                               "WHERE IdModuloSistema = @idModuloSistema";
                var queryplantillabyfaseDb = _dapper.QueryDapper(queryplantillabyfase, new { idModuloSistema });
                return JsonConvert.DeserializeObject<List<PlantillaWhatsAppDTO>>(queryplantillabyfaseDb);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<PlantillaWhatsAppDTO> ObtenterPlantillaWhatsAppOperaciones()
        {
            try
            {
                string _queryplantillabyfase = "SELECT Id ,Nombre, Descripcion,TipoPlantilla,Contenido " +
                                               " From ope.V_WhatsAppPlantillaOperaciones";
                var queryplantillabyfase = _dapper.QueryDapper(_queryplantillabyfase, null);
                return JsonConvert.DeserializeObject<List<PlantillaWhatsAppDTO>>(queryplantillabyfase);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PlantillaWhatsAppDTO> ObtenerPlantillaWhatsAppDocentes()
        {
            try
            {
                string _queryplantillabyfase = "SELECT Id ,Nombre, Descripcion,TipoPlantilla,Contenido " +
                                               "FROM ope.V_WhatsAppPlantillaDocentes";
                var queryplantillabyfase = _dapper.QueryDapper(_queryplantillabyfase, null);
                return JsonConvert.DeserializeObject<List<PlantillaWhatsAppDTO>>(queryplantillabyfase);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las clave valor por Plantilla
        /// </summary>
        /// <returns></returns>
        public List<PlantillaClaveValorDTO> ObtenerClaveValorPorPlantilla(int id)
        {
            try
            {
                var listaClave = GetBy(x => x.IdPlantilla == id, y => new PlantillaClaveValorDTO
                {
                    Id = y.Id,
                    Clave = y.Clave,
                    Valor = y.Valor,
                    IdPlantilla = y.IdPlantilla
                    
                }).ToList();

                return listaClave;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Plantilla Clave Valor asociados a una Plantilla
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPlantilla(int idPlantilla, string usuario, List<PlantillaClavesValoresDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPlantilla == idPlantilla && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Clave == x.Clave));
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

        public List<PlantillaValorDTO> ObtenerPlantillaPorPlantillaBase(string nombrePlantillaBase)
        {
            try
            {
                List<PlantillaValorDTO> complementosDTO = new List<PlantillaValorDTO>();
                var _query = "SELECT Id, Nombre, Valor FROM mkt.V_ObtenerPlantillaValor WHERE EstadoPlantilla = 1 and EstadoPlantillaClaveValor = 1 and NombrePlantillaBase = @nombrePlantillaBase";
                var queryRespuesta = _dapper.QueryDapper(_query, new { nombrePlantillaBase = nombrePlantillaBase });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    complementosDTO = JsonConvert.DeserializeObject<List<PlantillaValorDTO>>(queryRespuesta);
                }
                return complementosDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
