using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class EmbudoRepositorio : BaseRepository<TEmbudo, EmbudoBO>
    {
        #region Metodos Base
        public EmbudoRepositorio() : base()
        {
        }
        public EmbudoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EmbudoBO> GetBy(Expression<Func<TEmbudo, bool>> filter)
        {
            IEnumerable<TEmbudo> listado = base.GetBy(filter);
            List<EmbudoBO> listadoBO = new List<EmbudoBO>();
            foreach (var itemEntidad in listado)
            {
                EmbudoBO objetoBO = Mapper.Map<TEmbudo, EmbudoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EmbudoBO FirstById(int id)
        {
            try
            {
                TEmbudo entidad = base.FirstById(id);
                EmbudoBO objetoBO = new EmbudoBO();
                Mapper.Map<TEmbudo, EmbudoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EmbudoBO FirstBy(Expression<Func<TEmbudo, bool>> filter)
        {
            try
            {
                TEmbudo entidad = base.FirstBy(filter);
                EmbudoBO objetoBO = Mapper.Map<TEmbudo, EmbudoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EmbudoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEmbudo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EmbudoBO> listadoBO)
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

        public bool Update(EmbudoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEmbudo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EmbudoBO> listadoBO)
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
        private void AsignacionId(TEmbudo entidad, EmbudoBO objetoBO)
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

        private TEmbudo MapeoEntidad(EmbudoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEmbudo entidad = new TEmbudo();
                entidad = Mapper.Map<EmbudoBO, TEmbudo>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<EmbudoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TEmbudo, bool>>> filters, Expression<Func<TEmbudo, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TEmbudo> listado = base.GetFiltered(filters, orderBy, ascending);
            List<EmbudoBO> listadoBO = new List<EmbudoBO>();

            foreach (var itemEntidad in listado)
            {
                EmbudoBO objetoBO = Mapper.Map<TEmbudo, EmbudoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los resultados de nivel y sub nivel del embudo
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<EmbudoResultadoDTO> ObtenerFiltrado(FiltroReporteEmbudoDTO filtro) {
            try
            {
                List<EmbudoResultadoDTO> resultadoEmbudo = new List<EmbudoResultadoDTO>() { };
                var filtros = new {
                    FechaInicial = filtro.FechaInicio.Date,
                    FechaFinal = filtro.FechaFin.Date,
                    ListaArea = string.Join(",", filtro.ListaArea.Select(x => x.Valor)),
                    ListaSubArea = string.Join(",", filtro.ListaSubArea.Select(x => x.Valor)),
                    ListaProgramaGeneral = string.Join(",", filtro.ListaProgramaGeneral.Select(x => x.Valor)),
                    ListaPais = string.Join(",", filtro.ListaPais.Select(x => x.Valor)),
                    ListaTipoCategoriaOrigen = string.Join(",", filtro.ListaTipoCategoriaOrigen.Select(x => x.Valor))
                };
                var resultadosDB = this._dapper.QuerySPDapper("mkt.SP_ObtenerEmbudo", filtros);
                if (!string.IsNullOrEmpty(resultadosDB) && !resultadosDB.Contains("[]"))
                {
                    resultadoEmbudo = JsonConvert.DeserializeObject<List<EmbudoResultadoDTO>>(resultadosDB);
                }
                return resultadoEmbudo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los resultados de sub nivel
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<EmbudoDetalleResultadoDTO> ObtenerDetalleFiltrado(FiltroReporteEmbudoDTO filtro)
        {
            try
            {
                List<EmbudoDetalleResultadoDTO> resultadoEmbudo = new List<EmbudoDetalleResultadoDTO>() { };
                var filtros = new
                {
                    FechaInicial = filtro.FechaInicio.Date,
                    FechaFinal = filtro.FechaFin.Date,
                    ListaArea = string.Join(",", filtro.ListaArea.Select(x => x.Valor)),
                    ListaSubArea = string.Join(",", filtro.ListaSubArea.Select(x => x.Valor)),
                    ListaProgramaGeneral = string.Join(",", filtro.ListaProgramaGeneral.Select(x => x.Valor)),
                    ListaPais = string.Join(",", filtro.ListaPais.Select(x => x.Valor)),
                    ListaTipoCategoriaOrigen = string.Join(",", filtro.ListaTipoCategoriaOrigen.Select(x => x.Valor))
                };
                var resultadosDB = this._dapper.QuerySPDapper("mkt.SP_ObtenerEmbudo_SubNivel", filtros);
                if (!string.IsNullOrEmpty(resultadosDB) && !resultadosDB.Contains("[]"))
                {
                    resultadoEmbudo = JsonConvert.DeserializeObject<List<EmbudoDetalleResultadoDTO>>(resultadosDB);
                }
                return resultadoEmbudo;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la ultima actualizacion del embudo
        /// </summary>
        /// <returns></returns>
        public DateTime? ObtenerUltimaActualizacion() {
            try
            {
                //return this.GetBy(x => x.Estado, x => new { x.FechaRegistro }).Select(X => X.FechaRegistro).Distinct().OrderByDescending(x => x).FirstOrDefault();
                var fecha = new Dictionary<string, DateTime?>();
                var _query = "SELECT DISTINCT FechaRegistro FROM mkt.T_Embudo WHERE Estado = 1 ORDER BY FechaRegistro DESC";
                var resultadosDB = this._dapper.FirstOrDefault(_query, null);

                if (!string.IsNullOrEmpty(resultadosDB) && !resultadosDB.Contains("[]"))
                {
                    fecha = JsonConvert.DeserializeObject<Dictionary<string, DateTime?>>(resultadosDB);
                }

                return fecha.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
