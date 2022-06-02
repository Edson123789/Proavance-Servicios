using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/CampaniaMailingDetalle
    /// Autor: Johan Cayo - Ansoli Espinoza - Fischer Valdez - Wilber Choque - Gian Miranda
    /// Fecha: 09/04/2021
    /// <summary>
    /// Repositorio para la interaccion con la DB mkt.T_CampaniaMailingDetalle
    /// </summary>
    public class CampaniaMailingDetalleRepositorio : BaseRepository<TCampaniaMailingDetalle, CampaniaMailingDetalleBO>
    {
        #region Metodos Base
        public CampaniaMailingDetalleRepositorio() : base()
        {
        }
        public CampaniaMailingDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaMailingDetalleBO> GetBy(Expression<Func<TCampaniaMailingDetalle, bool>> filter)
        {
            IEnumerable<TCampaniaMailingDetalle> listado = base.GetBy(filter);
            List<CampaniaMailingDetalleBO> listadoBO = new List<CampaniaMailingDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaMailingDetalleBO objetoBO = Mapper.Map<TCampaniaMailingDetalle, CampaniaMailingDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaMailingDetalleBO FirstById(int id)
        {
            try
            {
                TCampaniaMailingDetalle entidad = base.FirstById(id);
                CampaniaMailingDetalleBO objetoBO = new CampaniaMailingDetalleBO();
                Mapper.Map<TCampaniaMailingDetalle, CampaniaMailingDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaMailingDetalleBO FirstBy(Expression<Func<TCampaniaMailingDetalle, bool>> filter)
        {
            try
            {
                TCampaniaMailingDetalle entidad = base.FirstBy(filter);
                CampaniaMailingDetalleBO objetoBO = Mapper.Map<TCampaniaMailingDetalle, CampaniaMailingDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaMailingDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaMailingDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampaniaMailingDetalleBO> listadoBO)
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

        public bool Update(CampaniaMailingDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaMailingDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampaniaMailingDetalleBO> listadoBO)
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
        private void AsignacionId(TCampaniaMailingDetalle entidad, CampaniaMailingDetalleBO objetoBO)
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

        private TCampaniaMailingDetalle MapeoEntidad(CampaniaMailingDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaMailingDetalle entidad = new TCampaniaMailingDetalle();
                entidad = Mapper.Map<CampaniaMailingDetalleBO, TCampaniaMailingDetalle>(objetoBO,
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
        /// Obtiene el id de la campania mailing por el codigo enviado
        /// </summary>
        /// <param name="codMailing">Codigo Mailing</param>
        /// <returns>Objeto de clase IdCampaniaMailingDetalleDTO</returns>
        public IdCampaniaMailingDetalleDTO ObtenerIdCampaniaMailing(string codMailing)
        {
            try
            {
                IdCampaniaMailingDetalleDTO campaniaMailingDetalle = new IdCampaniaMailingDetalleDTO();
                string _query = "SELECT IdCampaniaMailing FROM mkt.V_TCampaniaMailingDetall_IdCampaniaMailing WHERE Estado = 1 AND CodMailing = @codMailing";
                var queryCampania = _dapper.FirstOrDefault(_query, new { codMailing });
                campaniaMailingDetalle = JsonConvert.DeserializeObject<IdCampaniaMailingDetalleDTO>(queryCampania);
                return campaniaMailingDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los registros de CampaniaMailingDetalle con sus respectivos programas, filtrado por el IdCampaniaMailing
        /// </summary>
        /// <param name="idCampaniaMailing">Id de la campania mailing (PK de la tabla mkt.T_CampaniaMailing)</param>
        /// <returns>Lista de objetos de clase PrioridadesDTO</returns>
        public List<PrioridadesDTO> ObtenerListaCampaniaMailingDetalleConProgramas(int idCampaniaMailing)
        {
            try
            {
                string query = @"
                  SELECT Id, 
                       Prioridad, 
                       Tipo, 
                       IdRemitenteMailing, 
                       IdPersonal, 
                       Subject, 
                       FechaEnvio, 
                       IdHoraEnvio, 
                       Proveedor, 
                       EstadoEnvio, 
                       IdFiltroSegmento, 
                       IdPlantilla, 
                       IdConjuntoAnuncio, 
                       Campania, 
                       CodMailing, 
                       CantidadContactos, 
                       RowVersion, 
                       IdCampaniaMailingDetallePrograma, 
                       IdPGeneral, 
                       Nombre, 
                       TipoPrograma, 
                       IdArea, 
                       IdSubArea, 
                       CantidadSubidosMailChimp,
                       IdCentroCosto
                FROM mkt.V_ObtenerCampaniaMailingDetalle
                WHERE EstadoCampaniaMailingDetalle = 1
                      AND IdCampaniaMailing = @idCampaniaMailing
                      AND (EstadoCampaniaMailingDetallePrograma IS NULL
                           OR EstadoCampaniaMailingDetallePrograma = 1)
                      AND (EstadoArea IS NULL
                           OR EstadoArea = 1)
                      AND (EstadoSubArea IS NULL
                           OR EstadoSubArea = 1)
                ORDER BY Id;
                ";
                var respuestaQuery = _dapper.QueryDapper(query, new { idCampaniaMailing });
                IEnumerable<CampaniaMailingDetalleConProgramasDTO> listaCampaniaMailingDetalle = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleConProgramasDTO>>(respuestaQuery);

                var listaPrioridades = (from p in listaCampaniaMailingDetalle
                                        group p by new
                                        {
                                            p.Id,
                                            p.Prioridad,
                                            p.Tipo,
                                            p.IdRemitenteMailing,
                                            p.IdPersonal,
                                            p.Subject,
                                            p.Campania,
                                            p.CodMailing,
                                            p.IdConjuntoAnuncio,
                                            p.FechaEnvio,
                                            p.IdHoraEnvio,
                                            p.Proveedor,
                                            p.IdFiltroSegmento,
                                            p.IdPlantilla,
                                            p.EstadoEnvio,
                                            p.CantidadContactos,
                                            p.CantidadSubidosMailChimp,
                                            p.IdCentroCosto
                                        } into g
                                        select new PrioridadesDTO
                                        {
                                            Id = g.Key.Id,
                                            Prioridad = g.Key.Prioridad,
                                            Tipo = g.Key.Tipo,
                                            IdRemitenteMailing = g.Key.IdRemitenteMailing,
                                            IdPersonal = g.Key.IdPersonal,
                                            Subject = g.Key.Subject,
                                            Campania = g.Key.Campania,
                                            CodMailing = g.Key.CodMailing,
                                            IdConjuntoAnuncio = g.Key.IdConjuntoAnuncio,
                                            FechaEnvio = g.Key.FechaEnvio,
                                            IdHoraEnvio = g.Key.IdHoraEnvio,
                                            Proveedor = g.Key.Proveedor,
                                            EstadoEnvio = g.Key.EstadoEnvio,
                                            IdFiltroSegmento = g.Key.IdFiltroSegmento,
                                            IdPlantilla = g.Key.IdPlantilla,
                                            CantidadContactos = g.Key.CantidadContactos,
                                            CantidadSubidosMailChimp = g.Key.CantidadSubidosMailChimp,
                                            IdCentroCosto = g.Key.IdCentroCosto,
                                            ProgramasPrincipales = g.Where(o => o.TipoPrograma == "Principales").Select(o => new CampaniaMailingDetalleProgramaDTO
                                            {
                                                Id = o.IdCampaniaMailingDetallePrograma,
                                                IdPgeneral = o.IdPGeneral,
                                                Nombre = o.Nombre,
                                                Tipo = o.TipoPrograma
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                            ProgramasSecundarios = g.Where(o => o.TipoPrograma == "Secundarios").Select(o => new CampaniaMailingDetalleProgramaDTO
                                            {
                                                Id = o.IdCampaniaMailingDetallePrograma,
                                                IdPgeneral = o.IdPGeneral,
                                                Nombre = o.Nombre,
                                                Tipo = o.TipoPrograma
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                            ProgramasFiltro = g.Where(o => o.TipoPrograma == "Filtro").Select(o => new CampaniaMailingDetalleProgramaDTO
                                            {
                                                Id = o.IdCampaniaMailingDetallePrograma,
                                                IdPgeneral = o.IdPGeneral,
                                                Nombre = o.Nombre,
                                                Tipo = o.TipoPrograma
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                            Areas = g.Where(x => x.IdArea != null).Select(o => o.IdArea.Value).GroupBy(i => i).Select(i => i.First()).ToList(),
                                            SubAreas = g.Where(x => x.IdSubArea != null).Select(o => o.IdSubArea.Value).GroupBy(i => i).Select(i => i.First()).ToList()

                                        }).ToList();

                return listaPrioridades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Listas Mailing Por ConjuntoLista
        /// </summary>
        /// <param name="idConjuntoLista">Id del conjunto lista (PK de la tabla mkt.T_ConjuntoLista)</param>
        /// <returns>Lista de objetos de clase PrioridadesDTO</returns>
        public List<PrioridadesDTO> ObtenerListaPrioridades(int idConjuntoLista)
        {
            try
            {
                string query = "select Id,IdConjuntoListaDetalle, Prioridad, Tipo, IdRemitenteMailing, IdPersonal, Subject, FechaEnvio, IdHoraEnvio, Proveedor, " +
                    "EstadoEnvio, IdFiltroSegmento, IdPlantilla, IdConjuntoAnuncio, Campania, " +"CodMailing, CantidadContactos, RowVersion," +
                    "IdCampaniaMailingDetallePrograma, IdPGeneral, Nombre, TipoPrograma, IdArea, IdSubArea " +
                    "from mkt.V_ObtenerCampaniaMailingDetalleConjuntoLista " +
                    "where EstadoCampaniaMailingDetalle = 1 AND IdConjuntoLista = @IdConjuntoLista " +
                    "AND (EstadoCampaniaMailingDetallePrograma IS NULL OR EstadoCampaniaMailingDetallePrograma = 1 ) " +                    
                    "ORDER BY Id";
                var respuestaQuery = _dapper.QueryDapper(query, new { IdConjuntoLista = idConjuntoLista });
                IEnumerable<CampaniaMailingDetalleConjuntoListaDTO> listaCampaniaMailingDetalle = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleConjuntoListaDTO>>(respuestaQuery);

                var listaPrioridades = (from p in listaCampaniaMailingDetalle
                                        group p by new
                                        {
                                            p.Id,
                                            p.Prioridad,
                                            p.Tipo,
                                            p.IdRemitenteMailing,
                                            p.IdPersonal,
                                            p.Subject,
                                            p.Campania,
                                            p.CodMailing,
                                            p.IdConjuntoAnuncio,
                                            p.FechaEnvio,
                                            p.IdHoraEnvio,
                                            p.Proveedor,
                                            p.IdFiltroSegmento,
                                            p.IdConjuntoListaDetalle,
                                            p.IdPlantilla,
                                            p.EstadoEnvio,
                                            p.CantidadContactos
                                        } into g
                                        select new PrioridadesDTO
                                        {
                                            Id = g.Key.Id,
                                            Prioridad = g.Key.Prioridad,
                                            Tipo = g.Key.Tipo,
                                            IdRemitenteMailing = g.Key.IdRemitenteMailing,
                                            IdPersonal = g.Key.IdPersonal,
                                            Subject = g.Key.Subject,
                                            Campania = g.Key.Campania,
                                            CodMailing = g.Key.CodMailing,
                                            IdConjuntoAnuncio = g.Key.IdConjuntoAnuncio,
                                            FechaEnvio = g.Key.FechaEnvio,
                                            IdHoraEnvio = g.Key.IdHoraEnvio,
                                            Proveedor = g.Key.Proveedor,
                                            EstadoEnvio = g.Key.EstadoEnvio,
                                            IdFiltroSegmento = g.Key.IdFiltroSegmento,
                                            IdConjuntoListaDetalle = g.Key.IdConjuntoListaDetalle,
                                            IdPlantilla = g.Key.IdPlantilla,
                                            CantidadContactos = g.Key.CantidadContactos,

                                            ProgramasPrincipales = g.Where(o => o.TipoPrograma == "Principales").Select(o => new CampaniaMailingDetalleProgramaDTO
                                            {
                                                Id = o.IdCampaniaMailingDetallePrograma,
                                                IdPgeneral = o.IdPGeneral,
                                                Nombre = o.Nombre,
                                                Tipo = o.TipoPrograma
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                            ProgramasSecundarios = g.Where(o => o.TipoPrograma == "Secundarios").Select(o => new CampaniaMailingDetalleProgramaDTO
                                            {
                                                Id = o.IdCampaniaMailingDetallePrograma,
                                                IdPgeneral = o.IdPGeneral,
                                                Nombre = o.Nombre,
                                                Tipo = o.TipoPrograma
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                            ProgramasFiltro = g.Where(o => o.TipoPrograma == "Filtro").Select(o => new CampaniaMailingDetalleProgramaDTO
                                            {
                                                Id = o.IdCampaniaMailingDetallePrograma,
                                                IdPgeneral = o.IdPGeneral,
                                                Nombre = o.Nombre,
                                                Tipo = o.TipoPrograma
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                            Areas = g.Where(x => x.IdArea != null).Select(o => o.IdArea.Value).GroupBy(i => i).Select(i => i.First()).ToList(),
                                            SubAreas = g.Where(x => x.IdSubArea != null).Select(o => o.IdSubArea.Value).GroupBy(i => i).Select(i => i.First()).ToList()

                                        }).ToList();

                return listaPrioridades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las campaña mailing detalle sin enviar
        /// </summary>
        /// <param name="idCampaniaMailing"></param>
        /// <returns></returns>
        public List<CampaniaMailingDetalleBO> ObtenerListaCampaniaMailingDetalle(int idCampaniaMailing)
        {
            try
            {
                string query = $@"
                            SELECT Id, 
                                   IdCampaniaMailing, 
                                   Prioridad, 
                                   Tipo, 
                                   IdRemitenteMailing, 
                                   IdPersonal, 
                                   Subject, 
                                   FechaEnvio, 
                                   IdHoraEnvio, 
                                   Proveedor, 
                                   EstadoEnvio, 
                                   IdFiltroSegmento, 
                                   IdPlantilla, 
                                   IdConjuntoAnuncio, 
                                   Campania, 
                                   CodMailing, 
                                   CantidadContactos, 
                                   IdConjuntoListaDetalle, 
                                   IdCentroCosto, 
                                   EsSubidaManual, 
                                   Estado, 
                                   UsuarioCreacion, 
                                   UsuarioModificacion, 
                                   FechaCreacion, 
                                   FechaModificacion, 
                                   RowVersion, 
                                   IdMigracion
                            FROM mkt.V_ObtenerCampaniaMailingDetalleYCampaniaMailchimp
                            WHERE IdCampaniaMailing = @IdCampaniaMailing 
                                  AND IdCampaniaMailchimp IS NOT NULL
                                  AND IdListaMailchimp IS NOT NULL;
                            ";
                var responseQuery = _dapper.QueryDapper(query, new { IdCampaniaMailing = idCampaniaMailing });
                List<CampaniaMailingDetalleBO> listaCampaniaMailingDetalle = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleBO>>(responseQuery);

                return listaCampaniaMailingDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CampaniaMailingDetalleIdCampaniaMailingDTO> ObtenerCampaniaMailingDetalleEstadoCero(int idCampaniaMailing)
        {
            try
            {
                string query = "select Id, IdCampaniaMailing, Estado FROM mkt.V_TCampaniaMailingDetalle_IdCampaniaMailing WHERE IdCampaniaMailing = @IdCampaniaMailing AND Estado = 0";
                var responseQuery = _dapper.QueryDapper(query, new { IdCampaniaMailing = idCampaniaMailing });
                List<CampaniaMailingDetalleIdCampaniaMailingDTO> listaCampaniaMailingDetalle = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleIdCampaniaMailingDTO>>(responseQuery);

                return listaCampaniaMailingDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de campanias detalles con sus fechas de envio
        /// </summary>
        /// <returns></returns>
        public List<CampaniaDetalleWindowsServiceDTO> ObtenerCampaniaMailingDetalleParaSubirListas()
        {
            try
            {
                var fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                List<CampaniaDetalleWindowsServiceDTO> lista = new List<CampaniaDetalleWindowsServiceDTO>();

                var _query = @"
                   SELECT TOP 1 IdCampaniaMailingDetalle, 
                                 IdCampaniaMailing, 
                                 FechaEnvio, 
                                 EstadoEnvio, 
                                 FechaCompleta
                    FROM [mkt].[V_TCampaniaMailingDetalle_Serivicio]
                    WHERE FechaEnvio = @FechaEnvio
                          AND EstadoCampaniaMailingDetalle = 1
                          AND EstadoPrioridadMailChimpLista = 1
                          AND EsSubidoCorrectamente IS NULL
                    ORDER BY FechaCompleta ASC;
                ";
                var query = _dapper.QueryDapper(_query, new { FechaEnvio = fechaActual });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {   
                    lista = JsonConvert.DeserializeObject<List<CampaniaDetalleWindowsServiceDTO>>(query);
                    foreach (var item in lista)
                    {
                        item.FechaEnvio = Convert.ToDateTime(item.FechaCompleta);
                    }
                }
                return lista;
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }        

        /// <summary>
        /// Obtiene la lista de campanias detalles Para Subir listas Automaticas
        /// </summary>
        /// <returns></returns>
        public List<CampaniaDetalleAutomaticoServiceDTO> ObtenerCampaniaMailingDetalleParaSubirListasAutomaticas()
        {
            try
            {
                List<CampaniaDetalleAutomaticoServiceDTO> lista = new List<CampaniaDetalleAutomaticoServiceDTO>();
                DateTime HoraActual = DateTime.Now;
                string FechaInicioActividad = HoraActual.ToString("dd/MM/yyyy");
                

                var _query = "SELECT IdCampaniaMailingDetalle, IdCampaniaMailing,IdConjuntoLista,IdConjuntoListaDetalle,Activo AS ActivoEjecutarFiltro " +
                             "FROM mkt.V_ActividadCabcera_SubirListaAutomatica WHERE Estado = 1 and "+
                             " @FechaInicioActividad>=FechaInicioActividad and @FechaInicioActividad<= FechaFinActividad and SeEjecuta=1";
                var query = _dapper.QueryDapper(_query, new { FechaInicioActividad = FechaInicioActividad });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<CampaniaDetalleAutomaticoServiceDTO>>(query);                    
                }
                return lista;
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de campanias mailing detalle para enviar
        /// </summary>
        /// <returns></returns>
        public List<CampaniaDetalleEnvioDTO> ObtenerCampaniaMailingDetalleParaEnviar()
        {
            try
            {
                List<CampaniaDetalleEnvioDTO> lista = new List<CampaniaDetalleEnvioDTO>();
                lista = GetBy(x => x.EstadoEnvio == 0 && x.Estado == true, y => new CampaniaDetalleEnvioDTO
                {
                    IdCampaniaMailingDetalle = y.Id
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las plantillas y la información del Asesor
        /// </summary>
        /// <param name="idPrioridad"></param>
        /// <returns></returns>
        public CampaniaMailingDetalleProgramaPlantillaDTO ObtenerPlantillasInformacionAsesor(int idPrioridad)
        {
            try
            {
                CampaniaMailingDetalleProgramaPlantillaDTO plantillas = new CampaniaMailingDetalleProgramaPlantillaDTO();
                string _queryObtenerPlantillas = "select IdCampaniaMailingDetalle, IdCampaniaMailing, Subject, CorreoElectronico, IdPersonal, NombreCompletoPersonal, CentralPersonal, AnexoPersonal, Contenido, Asunto, NombreCampania " +
                "from mkt.V_ObtenerPlantillasInformacionAsesor where IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle AND ClaveContenido = 'Texto'  AND ClaveAsunto = 'Asunto' ";
                string registrosBD = _dapper.FirstOrDefault(_queryObtenerPlantillas, new { @IdCampaniaMailingDetalle = idPrioridad });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    plantillas = JsonConvert.DeserializeObject<CampaniaMailingDetalleProgramaPlantillaDTO>(registrosBD);
                }
                return plantillas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el programa con su respectiva etiqueta
        /// </summary>
        /// <param name="idPrioridad"></param>
        /// <returns></returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerProgramaYEtiqueta(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> nombresProgramas = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string _queryObtenerProgramas = "select Contenido, Etiqueta from mkt.V_ObtenerProgramaYEtiqueta " +
                    "where IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle and EstadoPGeneralParametroSEO = 1 and NombreParametroSEO = @Descripcion " +
                    "and EstadoDetallePrograma = 1 and TipoPrograma != @TipoPrograma ";
                string registrosBD = _dapper.QueryDapper(_queryObtenerProgramas, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @Descripcion = "description",
                    @TipoPrograma = "Filtro"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    nombresProgramas = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return nombresProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para los botones de Mas Informacion
        /// </summary>
        /// <param name="idPrioridad"></param>
        /// <returns></returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerInformacionBotonesyEtiqueta(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> nombresProgramas = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string _queryObtenerProgramas = "select Contenido, Etiqueta from mkt.V_ObtenerInformacionBotonyEtiqueta " +
                    "where IdCampaniaMaIlingDetalle = @IdCampaniaMailingDetalle and EstadoPGeneralParametroSEO = 1 and ParametroSEONombre = @Descripcion " +
                    "and EstadoDetallePrograma = 1 and TipoPrograma != @TipoPrograma ";
                string registrosBD = _dapper.QueryDapper(_queryObtenerProgramas, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @Descripcion = "description",
                    @TipoPrograma = "Filtro"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    nombresProgramas = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return nombresProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Datos Empresa
        /// </summary>
        /// <param name="idPrioridad">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaDatosEmpresa(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> datosEmpresa = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string _queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaDatosEmpresa WHERE IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle " +
                    "TituloDocumento= @TipoDocumento AND TipoPrograma != @TipoPrograma";
                string registrosBD = _dapper.QueryDapper(_queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @TipoPrograma = "Filtro",
                    @TipoDocumento = "Datos Empresa"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    datosEmpresa = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return datosEmpresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Encabezado
        /// </summary>
        /// <param name="idPrioridad">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaEncabezado(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> encabezados = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaEncabezado WHERE IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle " +
                    "TituloDocumento= @TipoDocumento AND TipoPrograma != @TipoPrograma";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @TipoPrograma = "Filtro",
                    @TipoDocumento = "Encabezado"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    encabezados = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return encabezados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Pie de Pagina
        /// </summary>
        /// <param name="idPrioridad">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaDatosPiePagina(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> piedePagina = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaPieDePaginaMailing WHERE IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle " +
                    "TituloDocumento= @TipoDocumento AND TipoPrograma != @TipoPrograma";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @TipoPrograma = "Filtro",
                    @TipoDocumento = "Pie de p&#225;gina"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    piedePagina = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return piedePagina;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Redes Sociales
        /// </summary>
        /// <param name="idPrioridad">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaRedesSociales(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> redesSociales = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaRedesSociales WHERE IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle" +
                    "TituloDocumento= @TipoDocumento AND TipoPrograma != @TipoPrograma";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @TipoPrograma = "Filtro",
                    @TipoDocumento = "Redes Sociales"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    redesSociales = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return redesSociales;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Imagen Programa
        /// </summary>
        /// <param name="idPrioridad">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaImagenPrograma(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> imagenesPrograma = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaImagenPrograma WHERE IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle " +
                    "TituloDocumento= @TipoDocumento AND TipoPrograma != @TipoPrograma";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @TipoPrograma = "Filtro",
                    @TipoDocumento = "Imagen de programa"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    imagenesPrograma = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return imagenesPrograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Texto Complemento 1
        /// </summary>
        /// <param name="idPrioridad">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaTextoComplemento1(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> textosComplemento1 = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaTextoComplemento1 WHERE IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle " +
                    "TituloDocumento= @TipoDocumento AND TipoPrograma != @TipoPrograma";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @TipoPrograma = "Filtro",
                    @TipoDocumento = "Texto Complemento 1"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    textosComplemento1 = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return textosComplemento1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Texto Complemento 2
        /// </summary>
        /// <param name="idPrioridad">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaTextoComplemento2(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> textosComplemento2 = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaTextoComplemento2 WHERE IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle " +
                    "TituloDocumento= @TipoDocumento AND TipoPrograma != @TipoPrograma";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @TipoPrograma = "Filtro",
                    @TipoDocumento = "Texto Complemento 2"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    textosComplemento2 = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return textosComplemento2;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la etiqueta y contenido para mailing Texto Complemento 3
        /// </summary>
        /// <param name="idPrioridad">Id de la campania mailing detalle</param>
        /// <returns>Lista de objetos de clase CampaniaMailingDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerContenidoYEtiquetaTextoComplemento3(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> textosComplemento3 = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                string queryObtenerContenidoPrograma = "SELECT Contenido, Etiqueta FROM mkt.V_ObtenerContenidoYEtiquetaTextoComplemento3 WHERE IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle " +
                    "TituloDocumento= @TipoDocumento AND TipoPrograma != @TipoPrograma";
                string registrosBD = _dapper.QueryDapper(queryObtenerContenidoPrograma, new
                {
                    @IdCampaniaMailingDetalle = idPrioridad,
                    @TipoPrograma = "Filtro",
                    @TipoDocumento = "Texto Complemento 3"
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    textosComplemento3 = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(registrosBD);
                }
                return textosComplemento3;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de campanias detalles con sus fechas de envio
        /// </summary>
        /// <returns></returns>
        public List<CampaniaDetalleWindowsServiceDTO> ObtenerCampaniaMailingDetalleParaSubirPorActividad()
        {
            try
            {
                var fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                List<CampaniaDetalleWindowsServiceDTO> lista = new List<CampaniaDetalleWindowsServiceDTO>();

                var _query = "SELECT IdCampaniaMailingDetalle, IdCampaniaMailing, FechaEnvio, EstadoEnvio,FechaCompleta " +
                    "FROM mkt.V_TCampaniaMailingDetalle_Serivicio WHERE Estado = 1 and FechaEnvio = @FechaEnvio";
                var query = _dapper.QueryDapper(_query, new { FechaEnvio = fechaActual });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<CampaniaDetalleWindowsServiceDTO>>(query);
                    foreach (var item in lista)
                    {
                        item.FechaEnvio = Convert.ToDateTime(item.FechaCompleta);
                    }
                }
                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Obtiene la lista de campanias detalles por ConjuntoLista
        /// </summary>
        /// <returns></returns>
        public List<CampaniaDetalleEnvioDTO> ObtenerListaCampaniaMailingDetallePorConjuntoLista(int IdConjuntoLista)
        {
            try
            {
                List<CampaniaDetalleEnvioDTO> lista = new List<CampaniaDetalleEnvioDTO>();

                var _query = "SELECT IdCampaniaMailingDetalle " +
                    "FROM mkt.V_ObtenerMailingDetallePorConjuntoLista WHERE Estado = 1 and IdConjuntoLista = @IdConjuntoLista";
                var query = _dapper.QueryDapper(_query, new { IdConjuntoLista });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<CampaniaDetalleEnvioDTO>>(query);                    
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los expositores por programa general
        /// </summary>
        /// <param name="idProgramaGeneral"></param>
        /// <returns></returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerExpositoresPorProgramaGeneral(int idCampaniaMailingDetalle)
        {
            try
            {
                var _campaniaMailingDetalle = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                var _query = @"
                SELECT Etiqueta,
                       Contenido
                FROM mkt.V_ObtenerInformacionDocentePorCampaniaMailingDetalle
                WHERE IdCampaniaMailingDetalle = @idCampaniaMailingDetalle
                      AND TipoPrograma <> @tipoPrograma
                      AND EstadoCampaniaMailing = 1
                      AND EstadoCampaniaMailingDetalle = 1
                      AND EstadoCampaniaMailingDetallePrograma = 1
                      AND EstadoPGeneral = 1;
            ";
                var expositoresDB = _dapper.QueryDapper(_query, new { idCampaniaMailingDetalle, tipoPrograma = "Filtro" });
                if (!string.IsNullOrEmpty(expositoresDB))
                {
                    _campaniaMailingDetalle = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(expositoresDB);
                }
                return _campaniaMailingDetalle.GroupBy(x => x.Etiqueta).Select(x => new CampaniaMailingDetalleContenidoEtiquetaDTO
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
        /// Obtiene los botones de whatsapp para Perú, colombia y bolivia
        /// </summary>
        /// <param name="idCampaniaMailingDetalle"></param>
        /// <returns></returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerBotonesWhatsapp(int idCampaniaMailingDetalle)
        {
            try
            {
                var _campaniaMailingDetalle = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                var rows = _dapper.QuerySPDapper("mkt.SP_ObtenerBotonesWhatsapp", new { idCampaniaMailingDetalle });
                if (!string.IsNullOrEmpty(rows))
                {
                    _campaniaMailingDetalle = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(rows);
                }
                return _campaniaMailingDetalle;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los botones de whatsapp para Perú, colombia y bolivia
        /// </summary>
        /// <param name="idCampaniaMailingDetalle"></param>
        /// <returns></returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerBotonesMessenger(int idCampaniaMailingDetalle)
        {
            try
            {
                var _campaniaMailingDetalle = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                var rows = _dapper.QuerySPDapper("mkt.SP_ObtenerBotonesMessenger", new { idCampaniaMailingDetalle });
                if (!string.IsNullOrEmpty(rows))
                {
                    _campaniaMailingDetalle = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleContenidoEtiquetaDTO>>(rows);
                }
                return _campaniaMailingDetalle;
            }
            catch (Exception e)
            {
                throw e; 
            }
        }

        /// <summary>
        /// Actualiza Campania Mailing detalle, datos (CantidadContactos, UsuarioModificacion, FechaModificacion)
        /// </summary>
        /// <param name="campaniaMailingDetalleActualizacion"></param>
        /// <returns></returns>
        public void ActualizarDatosFiltroMailchimp(CampaniaMailingDetalleActualizacionDTO campaniaMailingDetalleActualizacion)
        {
               try
            {
                string spQuery = "[mkt].[SP_ActualizarCampaniaMailingDetalleFiltro]";
                var query = _dapper.QuerySPDapper(spQuery, new { campaniaMailingDetalleActualizacion.CantidadContactos, campaniaMailingDetalleActualizacion.UsuarioModificacion, campaniaMailingDetalleActualizacion.Id });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Obtiene los miembros que pertenecen a la lista
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ContactoCampaniaMailingDTO> ObtenerContactos(int id)
        {
            try
            {
                List<ContactoCampaniaMailingDTO> listaContactos = new List<ContactoCampaniaMailingDTO>();
                var _query = @"
                            SELECT  IdPrioridadMailChimpListaCorreo,
                                    Nombre1,
                                    ApellidoPaterno,
                                    Email1
                            FROM mkt.V_ObtenerCorreosPorCampania
                            WHERE EstadoCampaniaMailing = 1
                                AND EstadoCampaniaMailingDetalle = 1
                                AND EstadoPrioridadMailChimpLista = 1
                                AND EstadoPrioridadMailChimpListaCorreo = 1
                                AND IdCampaniaMailingDetalle = @id;
                            ";
                var resultadoQuery = _dapper.QueryDapper(_query, new { id });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    listaContactos = JsonConvert.DeserializeObject<List<ContactoCampaniaMailingDTO>>(resultadoQuery);
                }
                return listaContactos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        /// <summary>
        /// Obtiene las listas que pertenecen a una campania
        /// </summary>
        /// <param name="idCampaniaMailing"></param>
        /// <returns></returns>
        public List<CampaniaMailingDetalleBO> ObtenerPorCampaniaMailing(int idCampaniaMailing) {
            try
            {
                return this.GetBy(x => x.IdCampaniaMailing == idCampaniaMailing && x.CantidadContactos != 0).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
