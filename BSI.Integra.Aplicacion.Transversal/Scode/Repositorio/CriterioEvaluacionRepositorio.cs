using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class CriterioEvaluacionRepositorio : BaseRepository<TCriterioEvaluacion, CriterioEvaluacionBO>
    {
        #region Metodos Base
        public CriterioEvaluacionRepositorio() : base()
        {
        }
        public CriterioEvaluacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CriterioEvaluacionBO> GetBy(Expression<Func<TCriterioEvaluacion, bool>> filter)
        {
            IEnumerable<TCriterioEvaluacion> listado = base.GetBy(filter);
            List<CriterioEvaluacionBO> listadoBO = new List<CriterioEvaluacionBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionBO objetoBO = Mapper.Map<TCriterioEvaluacion, CriterioEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CriterioEvaluacionBO FirstById(int id)
        {
            try
            {
                TCriterioEvaluacion entidad = base.FirstById(id);
                CriterioEvaluacionBO objetoBO = new CriterioEvaluacionBO();
                Mapper.Map<TCriterioEvaluacion, CriterioEvaluacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CriterioEvaluacionBO FirstBy(Expression<Func<TCriterioEvaluacion, bool>> filter)
        {
            try
            {
                TCriterioEvaluacion entidad = base.FirstBy(filter);
                CriterioEvaluacionBO objetoBO = Mapper.Map<TCriterioEvaluacion, CriterioEvaluacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CriterioEvaluacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCriterioEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CriterioEvaluacionBO> listadoBO)
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

        public bool Update(CriterioEvaluacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCriterioEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CriterioEvaluacionBO> listadoBO)
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
        private void AsignacionId(TCriterioEvaluacion entidad, CriterioEvaluacionBO objetoBO)
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

        private TCriterioEvaluacion MapeoEntidad(CriterioEvaluacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCriterioEvaluacion entidad = new TCriterioEvaluacion();
                entidad = Mapper.Map<CriterioEvaluacionBO, TCriterioEvaluacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListadoParametro != null && objetoBO.ListadoParametro.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListadoParametro)
                    {
                        TParametroEvaluacion entidadHijo = new TParametroEvaluacion();
                        entidadHijo = Mapper.Map<ParametroEvaluacionBO, TParametroEvaluacion>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TParametroEvaluacion.Add(entidadHijo);

                        //mapea al hijo interno
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

        public List<CriterioEvaluacionDTO> ListarCriterios(FiltroPaginadorDTO filtro)
        {
            try
            {
                List<CriterioEvaluacionDTO> items = new List<CriterioEvaluacionDTO>();
                List<Expression<Func<TCriterioEvaluacion, bool>>> filters = new List<Expression<Func<TCriterioEvaluacion, bool>>>();
                var total = 0;
                List<CriterioEvaluacionBO> lista = new List<CriterioEvaluacionBO>();
                if (filtro != null && filtro.Take != 0)
                {
                    if ((filtro.FiltroKendo != null && filtro.FiltroKendo.Filters.Count > 0))
                    {
                        // Creamos la Lista de filtros
                        foreach (var filterGrid in filtro.FiltroKendo.Filters)
                        {
                            switch (filterGrid.Field)
                            {
                                case "Nombre":
                                    filters.Add(o => o.Nombre.Contains(filterGrid.Value));
                                    break;
                               
                                case "IdCriterioEvaluacionCategoria":
                                    filters.Add(o => o.IdCriterioEvaluacionCategoria.ToString().Contains(filterGrid.Value));
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
                items = lista.Select(x => new CriterioEvaluacionDTO
                {
                    Id = x.Id,                                      
                    Nombre = x.Nombre,
                    IdCriterioEvaluacionCategoria = x.IdCriterioEvaluacionCategoria
                }).ToList();

                return items;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CriterioEvaluacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCriterioEvaluacion, bool>>> filters, Expression<Func<TCriterioEvaluacion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TCriterioEvaluacion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<CriterioEvaluacionBO> listadoBO = new List<CriterioEvaluacionBO>();

            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionBO objetoBO = Mapper.Map<TCriterioEvaluacion, CriterioEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }

        public List<FiltroDTO> ObtenerFiltro()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CriterioEvaluacionFiltroBO> ObtenerCriterio(int tipoprograma, int modalidadprograma)
        {
            try
            {
                List<CriterioEvaluacionFiltroBO> criteriosFiltro = new List<CriterioEvaluacionFiltroBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocriterio = "Select Id,Nombre FROM [pla].[V_CriterioEvaluacion] where Estado = 1 and IdTipoPrograma =@tipoprograma and IdModalidadCurso=@modalidadprograma group by Id,Nombre";
                var SubfiltroCriterio = _dapper.QueryDapper(_queryfiltrocriterio, new {tipoprograma,modalidadprograma});
                if (!string.IsNullOrEmpty(SubfiltroCriterio) && !SubfiltroCriterio.Contains("[]"))
                {
                    criteriosFiltro = JsonConvert.DeserializeObject<List<CriterioEvaluacionFiltroBO>>(SubfiltroCriterio);
                }
                return criteriosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public List<CriterioEvaluacionDTO> ListarCriteriosEvaluacion()
        {
            try
            {
                List<CriterioEvaluacionDTO> criteriosFiltro = new List<CriterioEvaluacionDTO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocriterio = "Select Id,Nombre,IdCriterioEvaluacionCategoria FROM pla.T_CriterioEvaluacion where Estado = 1";
                var SubfiltroCriterio = _dapper.QueryDapper(_queryfiltrocriterio, null);
                if (!string.IsNullOrEmpty(SubfiltroCriterio) && !SubfiltroCriterio.Contains("[]"))
                {
                    criteriosFiltro = JsonConvert.DeserializeObject<List<CriterioEvaluacionDTO>>(SubfiltroCriterio);
                    //foreach

                    foreach (var item in criteriosFiltro)
                    {
                        item.IdTipoPrograma = new List<CriterioEvaluacionTipoProgramaDTO>();
                        var _queryfiltrotipo = "Select Id,IdCriterioEvaluacion,IdTipoPrograma FROM pla.T_CriterioEvaluacionTipoPrograma where Estado = 1 and IdCriterioEvaluacion =@IdCriterioEvaluacion";
                        var SubfiltroCriteriotipo = _dapper.QueryDapper(_queryfiltrotipo, new { IdCriterioEvaluacion = item.Id });
                        if (!string.IsNullOrEmpty(SubfiltroCriteriotipo) && !SubfiltroCriteriotipo.Contains("[]"))
                        {
                            item.IdTipoPrograma = JsonConvert.DeserializeObject<List<CriterioEvaluacionTipoProgramaDTO>>(SubfiltroCriteriotipo);
                        }

                        item.IdModalidadCurso = new List<CriterioEvaluacionModalidadCursoDTO>();
                        var _queryfiltromodalidad = "Select Id,IdCriterioEvaluacion,IdModalidadCurso FROM pla.T_CriterioEvaluacionModalidadCurso where Estado = 1 and IdCriterioEvaluacion =@IdCriterioEvaluacion";
                        var SubfiltroModalidad = _dapper.QueryDapper(_queryfiltromodalidad, new { IdCriterioEvaluacion = item.Id });
                        if (!string.IsNullOrEmpty(SubfiltroModalidad) && !SubfiltroModalidad.Contains("[]"))
                        {
                            item.IdModalidadCurso = JsonConvert.DeserializeObject<List<CriterioEvaluacionModalidadCursoDTO>>(SubfiltroModalidad);
                        }

                        item.IdTipoPersona = new List<CriterioEvaluacionTipoPersonaDTO>();
                        var _queryfiltroTipoPersona = "Select Id,IdCriterioEvaluacion,IdTipoPersona FROM pla.T_CriterioEvaluacionTipoPersona where Estado = 1 and IdCriterioEvaluacion =@IdCriterioEvaluacion";
                        var SubfiltroTipoPersona = _dapper.QueryDapper(_queryfiltroTipoPersona, new { IdCriterioEvaluacion = item.Id });
                        if (!string.IsNullOrEmpty(SubfiltroTipoPersona) && !SubfiltroTipoPersona.Contains("[]"))
                        {
                            item.IdTipoPersona = JsonConvert.DeserializeObject<List<CriterioEvaluacionTipoPersonaDTO>>(SubfiltroTipoPersona);
                        }
                    }
                    
                }
                return criteriosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

         




    }
}
