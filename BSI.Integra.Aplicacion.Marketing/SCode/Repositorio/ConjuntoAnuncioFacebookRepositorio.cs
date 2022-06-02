using System;
using System;
using System.Collections.Generic;
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
    public class ConjuntoAnuncioFacebookRepositorio : BaseRepository<TConjuntoAnuncioFacebook, ConjuntoAnuncioFacebookBO>
    {
        #region Metodos Base
        public ConjuntoAnuncioFacebookRepositorio() : base()
        {
        }
        public ConjuntoAnuncioFacebookRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConjuntoAnuncioFacebookBO> GetBy(Expression<Func<TConjuntoAnuncioFacebook, bool>> filter)
        {
            IEnumerable<TConjuntoAnuncioFacebook> listado = base.GetBy(filter);
            List<ConjuntoAnuncioFacebookBO> listadoBO = new List<ConjuntoAnuncioFacebookBO>();
            foreach (var itemEntidad in listado)
            {
                ConjuntoAnuncioFacebookBO objetoBO = Mapper.Map<TConjuntoAnuncioFacebook, ConjuntoAnuncioFacebookBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConjuntoAnuncioFacebookBO FirstById(int id)
        {
            try
            {
                TConjuntoAnuncioFacebook entidad = base.FirstById(id);
                ConjuntoAnuncioFacebookBO objetoBO = new ConjuntoAnuncioFacebookBO();
                Mapper.Map<TConjuntoAnuncioFacebook, ConjuntoAnuncioFacebookBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConjuntoAnuncioFacebookBO FirstBy(Expression<Func<TConjuntoAnuncioFacebook, bool>> filter)
        {
            try
            {
                TConjuntoAnuncioFacebook entidad = base.FirstBy(filter);
                ConjuntoAnuncioFacebookBO objetoBO = Mapper.Map<TConjuntoAnuncioFacebook, ConjuntoAnuncioFacebookBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConjuntoAnuncioFacebookBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConjuntoAnuncioFacebook entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConjuntoAnuncioFacebookBO> listadoBO)
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

        public bool Update(ConjuntoAnuncioFacebookBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConjuntoAnuncioFacebook entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConjuntoAnuncioFacebookBO> listadoBO)
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
        private void AsignacionId(TConjuntoAnuncioFacebook entidad, ConjuntoAnuncioFacebookBO objetoBO)
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

        private TConjuntoAnuncioFacebook MapeoEntidad(ConjuntoAnuncioFacebookBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConjuntoAnuncioFacebook entidad = new TConjuntoAnuncioFacebook();
                entidad = Mapper.Map<ConjuntoAnuncioFacebookBO, TConjuntoAnuncioFacebook>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConjuntoAnuncioFacebookBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConjuntoAnuncioFacebook, bool>>> filters, Expression<Func<TConjuntoAnuncioFacebook, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConjuntoAnuncioFacebook> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConjuntoAnuncioFacebookBO> listadoBO = new List<ConjuntoAnuncioFacebookBO>();

            foreach (var itemEntidad in listado)
            {
                ConjuntoAnuncioFacebookBO objetoBO = Mapper.Map<TConjuntoAnuncioFacebook, ConjuntoAnuncioFacebookBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;

        }
            #endregion

        /// <summary>
        /// Obtiene registros de conjunto anuncios facebook
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
            public List<ConjuntoAnuncioFacebookDTO> ListarConjuntoAnuncioFB(FiltroPaginadorDTO filtro)
        {
            try
            {
                List<ConjuntoAnuncioFacebookDTO> items = new List<ConjuntoAnuncioFacebookDTO>();
                List<Expression<Func<TConjuntoAnuncioFacebook, bool>>> filters = new List<Expression<Func<TConjuntoAnuncioFacebook, bool>>>();
                var total = 0;
                List<ConjuntoAnuncioFacebookBO> lista = new List<ConjuntoAnuncioFacebookBO>();
              
                if (filtro != null && filtro.Take != 0)
                {
                    if ((filtro.FiltroKendo != null && filtro.FiltroKendo.Filters.Count > 0))
                    {
                        foreach (var filterGrid in filtro.FiltroKendo.Filters)
                        {
                            switch (filterGrid.Field)
                            {
                                case "Name":
                                    filters.Add(o => o.Name.Contains(filterGrid.Value));
                                    break;
                                case "IdAnuncioFacebook":
                                    filters.Add(o => o.IdAnuncioFacebook.Contains(filterGrid.Value));
                                    break;
                                case "OptimizationGoal":
                                    filters.Add(o => o.OptimizationGoal.Contains(filterGrid.Value));
                                    break;
                                case "StartTime":
                                    filters.Add(o => o.StartTime.ToString().Contains(filterGrid.Value));
                                    break;
                                case "UpdatedTime":
                                    filters.Add(o => o.UpdatedTime.ToString().Contains(filterGrid.Value));
                                    break;
                                case "Status":
                                    filters.Add(o => o.Status.Contains(filterGrid.Value));
                                    break;
                                case "EffectiveStatus":
                                    filters.Add(o => o.EffectiveStatus.Contains(filterGrid.Value));
                                    break;
                                default:
                                    filters.Add(o => true);
                                    break;
                            }
                        }
                    }
					lista = GetFiltered(filters, p => p.Id, false).ToList();
					total = lista.Count();
                    lista = lista.Skip(filtro.Skip).Take(filtro.Take).ToList();
                }
                else
                {
                   
                    lista = GetBy(o => true).OrderByDescending(x => x.Id).ToList();
                    total = lista.Count();
                }
                items = lista.Select(x => new ConjuntoAnuncioFacebookDTO
                {
                    IdAnuncioFacebook = x.IdAnuncioFacebook,
                    Id = x.Id,
                    AttributionWindowDays = x.BidAmount,
                    BidAmount = x.BidAmount,
                    BillinEevent = x.BillinEevent,
                    BudgetRemaining = x.BudgetRemaining,
                    CampaignId = x.CampaignId,
                    CreatedTime = x.CreatedTime,
                    DailyBudget = x.DailyBudget,
                    DailyImps = x.DailyImps,
                    EffectiveStatus = x.EffectiveStatus,
                    EndTime = x.EndTime,
                    IsAutobid = x.IsAutobid,
                    IsAveragePricePacing = x.IsAveragePricePacing,
                    LifetimeBudget = x.LifetimeBudget,
                    LifetimeImps = x.LifetimeImps,
                    Name = x.Name,
                    OptimizationGoal = x.OptimizationGoal,
                    StartTime = x.StartTime,
                    Status = x.Status,
                    UpdatedTime = x.UpdatedTime,
                    TieneInsights = x.TieneInsights,
                    EsValidado = x.EsValidado,
                    EsIntegra = x.EsIntegra,
                    EsPublicado = x.EsPublicado,
                    IdConjuntoAnuncio = x.IdConjuntoAnuncio,
                    EsRelacionado = x.EsRelacionado,
                    EsOtros = x.EsOtros,
                    Total = total.ToString()
                }).ToList();

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Autocomplete para buscar conjunto de anuncios
        /// </summary>
        /// <param name="NombreBuscar"></param>
        /// <returns></returns>
       public List<ConjuntoAnuncioFiltroDTO> ConjutoAnuncioAutocomplete(string NombreBuscar)
        {
            try
            {
                List<ConjuntoAnuncioFiltroDTO> items = new List<ConjuntoAnuncioFiltroDTO>();
                List<ConjuntoAnuncioBO> lista = new List<ConjuntoAnuncioBO>();
                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio();
                lista = _repConjuntoAnuncio.GetBy(o => o.Estado == true && o.Nombre.Contains(NombreBuscar)).OrderByDescending(x => x.Id).ToList();
                items = lista.Select(x => new ConjuntoAnuncioFiltroDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    IdConjuntoAnuncioFacebook = x.IdConjuntoAnuncioFacebook

                }).ToList();

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Lista conjunto de anuncios facebook no asignados
        /// </summary>
        /// <returns></returns>
        public List<ConjuntoAnuncioFacebookDTO> ListarConjuntoAnuncioFBPendiente()
        {
            try
            {

                List<ConjuntoAnuncioFacebookDTO> items = new List<ConjuntoAnuncioFacebookDTO>();
                List<ConjuntoAnuncioFacebookBO> lista = new List<ConjuntoAnuncioFacebookBO>();
                var total = 0;

                DateTime filtroFecha = DateTime.ParseExact("2015-12-01 00:00:00", "yyyy-MM-dd HH:mm:ss",
                                  System.Globalization.CultureInfo.InvariantCulture);
                lista = GetBy(o => true && o.OptimizationGoal != "LEAD_GENERATION" && o.EsRelacionado != true && o.EsOtros != true && o.CreatedTime >= filtroFecha).OrderByDescending(x => x.Id).ToList();
                total = lista.Count();


                items = lista.Select(x => new ConjuntoAnuncioFacebookDTO
                {

                    IdAnuncioFacebook = x.IdAnuncioFacebook,
                    Id = x.Id,
                    AttributionWindowDays = x.BidAmount,
                    BidAmount = x.BidAmount,
                    BillinEevent = x.BillinEevent,
                    BudgetRemaining = x.BudgetRemaining,
                    CampaignId = x.CampaignId,
                    CreatedTime = x.CreatedTime,
                    DailyBudget = x.DailyBudget,
                    DailyImps = x.DailyImps,
                    EffectiveStatus = x.EffectiveStatus,
                    EndTime = x.EndTime,
                    IsAutobid = x.IsAutobid,
                    IsAveragePricePacing = x.IsAveragePricePacing,
                    LifetimeBudget = x.LifetimeBudget,
                    LifetimeImps = x.LifetimeImps,
                    Name = x.Name,
                    OptimizationGoal = x.OptimizationGoal,
                    StartTime = x.StartTime,
                    Status = x.Status,
                    UpdatedTime = x.UpdatedTime,
                    TieneInsights = x.TieneInsights,
                    EsValidado = x.EsValidado,
                    EsIntegra = x.EsIntegra,
                    EsPublicado = x.EsPublicado,
                    IdConjuntoAnuncio = x.IdConjuntoAnuncio,
                    EsRelacionado = x.EsRelacionado,
                    EsOtros = x.EsOtros,
                    Total = total.ToString()

                }).ToList();

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// <summary>
        /// Obtiene la lista de nombres de las Conjunto Anuncio Facebbok(activos)  registradas en el sistema 
        /// y sus IDs Facebook, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns>IdConjuntoAnuncio_Facebook, Nombre</returns>
        public List<ConjuntoAnuncioFBDTO> ObtenerConjuntoAnuncioFBFiltro()
        {
            try
            {
                List<ConjuntoAnuncioFBDTO> ConjuntoAnuncioFB = new List<ConjuntoAnuncioFBDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM [mkt].[V_TConjuntoAnuncioFacebook_ObtenerIdNombre] WHERE  Estado = 1";
                var ConjuntoAnuncioFBDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ConjuntoAnuncioFBDB) && !ConjuntoAnuncioFBDB.Contains("[]"))
                {
                    ConjuntoAnuncioFB = JsonConvert.DeserializeObject<List<ConjuntoAnuncioFBDTO>>(ConjuntoAnuncioFBDB);
                }
                return ConjuntoAnuncioFB;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
