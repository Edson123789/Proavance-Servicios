using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FiltroSegmentoValorTipoRepositorio : BaseRepository<TFiltroSegmentoValorTipo, FiltroSegmentoValorTipoBO>
    {
        #region Metodos Base
        public FiltroSegmentoValorTipoRepositorio() : base()
        {
        }
        public FiltroSegmentoValorTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FiltroSegmentoValorTipoBO> GetBy(Expression<Func<TFiltroSegmentoValorTipo, bool>> filter)
        {
            IEnumerable<TFiltroSegmentoValorTipo> listado = base.GetBy(filter);
            List<FiltroSegmentoValorTipoBO> listadoBO = new List<FiltroSegmentoValorTipoBO>();
            foreach (var itemEntidad in listado)
            {
                FiltroSegmentoValorTipoBO objetoBO = Mapper.Map<TFiltroSegmentoValorTipo, FiltroSegmentoValorTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FiltroSegmentoValorTipoBO FirstById(int id)
        {
            try
            {
                TFiltroSegmentoValorTipo entidad = base.FirstById(id);
                FiltroSegmentoValorTipoBO objetoBO = new FiltroSegmentoValorTipoBO();
                Mapper.Map<TFiltroSegmentoValorTipo, FiltroSegmentoValorTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FiltroSegmentoValorTipoBO FirstBy(Expression<Func<TFiltroSegmentoValorTipo, bool>> filter)
        {
            try
            {
                TFiltroSegmentoValorTipo entidad = base.FirstBy(filter);
                FiltroSegmentoValorTipoBO objetoBO = Mapper.Map<TFiltroSegmentoValorTipo, FiltroSegmentoValorTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FiltroSegmentoValorTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFiltroSegmentoValorTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FiltroSegmentoValorTipoBO> listadoBO)
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

        public bool Update(FiltroSegmentoValorTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFiltroSegmentoValorTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FiltroSegmentoValorTipoBO> listadoBO)
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
        private void AsignacionId(TFiltroSegmentoValorTipo entidad, FiltroSegmentoValorTipoBO objetoBO)
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

        private TFiltroSegmentoValorTipo MapeoEntidad(FiltroSegmentoValorTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFiltroSegmentoValorTipo entidad = new TFiltroSegmentoValorTipo();
                entidad = Mapper.Map<FiltroSegmentoValorTipoBO, TFiltroSegmentoValorTipo>(objetoBO,
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
        /// Obtiene la lista de BOs de FiltroSegmentoValorTipo
        /// </summary>
        /// <param name="idFiltroSegmento">Id del filtro segmento (PK de la tabla mkt.T_FiltroSegmento)</param>
        /// <returns>Objeto de tipo ValorTipoDTO</returns>
        public List<FiltroSegmentoValorTipoBO> ObtenerListaFiltroSegmentoValorTipo(int idFiltroSegmento)
        {
            try
            {
                List<FiltroSegmentoValorTipoBO> resultadoFinal = new List<FiltroSegmentoValorTipoBO>();

                string queryDapper = "SELECT * FROM mkt.V_TFiltroSegmentoValorTipo_Completo WHERE IdFiltroSegmento = @IdFiltroSegmento";

                var listaRegistros = _dapper.QueryDapper(queryDapper, new { IdFiltroSegmento = idFiltroSegmento });
                if (!string.IsNullOrEmpty(listaRegistros) && !listaRegistros.Contains("[]"))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<FiltroSegmentoValorTipoBO>>(listaRegistros);
                }

                return resultadoFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Se recuperan los filtros para CampaniaMailchimp
        /// </summary>
        /// <param name="idFiltroSegmento">Id del filtro segmento (PK de la tabla mkt.T_FiltroSegmento)</param>
        /// <returns>Objeto de tipo ValorTipoDTO</returns>
        public ValorTipoDTO ObtenerFiltrosCampaniaMailchimp(int idFiltroSegmento)
        {
            List<FiltroSegmentoValorTipoBO> filtroSegmentoValorTipoBOs = ObtenerListaFiltroSegmentoValorTipo(idFiltroSegmento);
            //List<FiltroSegmentoValorTipoBO> filtroSegmentoValorTipoBOs = GetBy(x => x.IdFiltroSegmento == idFiltroSegmento).ToList();
            ValorTipoDTO valorTipo = new ValorTipoDTO();

            List<int> listaArea = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaArea.Count > 0) valorTipo.Areas = String.Join(",", listaArea);

            List<int> listaSubArea = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaSubArea.Count > 0) valorTipo.SubAreas = String.Join(",", listaSubArea);

            List<int> listaPGeneral = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaPGeneral.Count > 0) valorTipo.ProgramaGeneral = String.Join(",", listaPGeneral);

            List<int> listaPEspecifico = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaPEspecifico.Count > 0) valorTipo.ProgramaEspecifico = String.Join(",", listaPEspecifico);

            List<int> listaProbabilidadRegistro = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaProbabilidadRegistro.Count > 0) valorTipo.ProbabilidadRegistro = String.Join(",", listaProbabilidadRegistro);

            List<int> listaPaises = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaPaises.Count > 0) valorTipo.Paises = String.Join(",", listaPaises);

            List<int> listaCiudades = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaCiudades.Count > 0) valorTipo.Ciudades = String.Join(",", listaCiudades);

            List<int> listaCategoriaDato = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaCategoriaDato.Count > 0) valorTipo.CategoriaDato = String.Join(",", listaCategoriaDato);

            List<int> listaCargos = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaCargos.Count > 0) valorTipo.Cargos = String.Join(",", listaCargos);

            List<int> listaIndustrias = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaIndustrias.Count > 0) valorTipo.Industrias = String.Join(",", listaIndustrias);

            List<int> listaAreaFormacion = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaAreaFormacion.Count > 0) valorTipo.AreaFormacion = String.Join(",", listaAreaFormacion);

            List<int> listaAreaTrabajo = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaAreaTrabajo.Count > 0) valorTipo.AreaTrabajo = String.Join(",", listaAreaTrabajo);

            List<int> listaFaseOportunidadInicial = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaFaseOportunidadInicial.Count > 0) valorTipo.FaseOportunidadInicial = String.Join(",", listaFaseOportunidadInicial);

            List<int> listaFaseOportunidadMaximaInicial = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaFaseOportunidadMaximaInicial.Count > 0) valorTipo.FaseOportunidadMaximaInicial = String.Join(",", listaFaseOportunidadMaximaInicial);

            List<int> listaFaseHistorica = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaFaseHistorica.Count > 0) valorTipo.FaseHistorica = String.Join(",", listaFaseHistorica);

            List<int> listaFaseHistoricaMaxima = filtroSegmentoValorTipoBOs.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).Select(y => y.Valor).ToList();
            if (listaFaseHistoricaMaxima.Count > 0) valorTipo.FaseHistoricaMaxima = String.Join(",", listaFaseHistoricaMaxima);

            return valorTipo;
        }

        /// <summary>
        /// Obtiene la lista de valores filtros por filtro segmento
        /// </summary>
        /// <param name="idFiltroSegmento"></param>
        /// <returns></returns>
        public List<FiltroSegmentoValorTipoDTO> ObtenerFiltroValorPorIdFiltroSegmento(int idFiltroSegmento)
        {
            try
            {
                List<FiltroSegmentoValorTipoDTO> items = new List<FiltroSegmentoValorTipoDTO>();
                string _query = @"
                                SELECT Id, IdCategoriaObjetoFiltro, Valor
                                FROM [mkt].[V_TFiltroSegmentoValorTipo_ConfiguracionFiltroSegmento]
                                WHERE Estado = 1 AND IdFiltroSegmento = @idFiltroSegmento";

                string filtroSegmentoValorTipoDB = _dapper.QueryDapper(_query, new { idFiltroSegmento });

                items = JsonConvert.DeserializeObject<List<FiltroSegmentoValorTipoDTO>>(filtroSegmentoValorTipoDB);

                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Realiza una Eliminacion Logica de todos los hijos por filtro Segmento
        /// </summary>
        /// <param name="filtro"></param>
        public void EliminacionLogica(FiltroSegmentoDTO filtro)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdFiltroSegmento == filtro.Id && x.Estado == true).ToList();

                listaBorrar.RemoveAll(x => filtro.ListaArea.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea));
                listaBorrar.RemoveAll(x => filtro.ListaSubArea.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea));
                listaBorrar.RemoveAll(x => filtro.ListaProgramaGeneral.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral));
                listaBorrar.RemoveAll(x => filtro.ListaProgramaEspecifico.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaEspecifico));

                listaBorrar.RemoveAll(x => filtro.ListaOportunidadInicialFaseMaxima.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseMaxima));
                listaBorrar.RemoveAll(x => filtro.ListaOportunidadInicialFaseActual.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseActual));
                listaBorrar.RemoveAll(x => filtro.ListaOportunidadActualFaseMaxima.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseMaxima));
                listaBorrar.RemoveAll(x => filtro.ListaOportunidadActualFaseActual.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseActual));

                listaBorrar.RemoveAll(x => filtro.ListaPais.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPais));
                listaBorrar.RemoveAll(x => filtro.ListaCiudad.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCiudad));

                listaBorrar.RemoveAll(x => filtro.ListaTipoCategoriaOrigen.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoCategoriaOrigen));
                listaBorrar.RemoveAll(x => filtro.ListaCategoriaOrigen.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCategoriaOrigen));

                listaBorrar.RemoveAll(x => filtro.ListaCargo.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCargo));
                listaBorrar.RemoveAll(x => filtro.ListaIndustria.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroIndustria));
                listaBorrar.RemoveAll(x => filtro.ListaAreaFormacion.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroAreaFormacion));
                listaBorrar.RemoveAll(x => filtro.ListaAreaTrabajo.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroAreaTrabajo));

                listaBorrar.RemoveAll(x => filtro.ListaTipoFormulario.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoFormulario));
                listaBorrar.RemoveAll(x => filtro.ListaTipoInteraccionFormulario.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoInteraccionFormulario));

                listaBorrar.RemoveAll(x => filtro.ListaProbabilidadOportunidad.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProbabilidadOportunidad));
                listaBorrar.RemoveAll(x => filtro.ListaActividadLlamada.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroActividadesLlamada));

                listaBorrar.RemoveAll(x => filtro.ListaVCArea.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCArea));
                listaBorrar.RemoveAll(x => filtro.ListaVCSubArea.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCSubArea));

                listaBorrar.RemoveAll(x => filtro.ListaProbabilidadVentaCruzada.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProbabilidadVentaCruzada));

                listaBorrar.RemoveAll(x => filtro.ListaProgramaGeneralPrincipalExcluirPorMismoCorreoEnviado.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPGeneralPrincipalExcluir));

                listaBorrar.RemoveAll(x => filtro.ListaExcluirPorFiltroSegmento.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroFiltroSegmento));
                listaBorrar.RemoveAll(x => filtro.ListaExcluirPorCampaniaMailing.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing));
                listaBorrar.RemoveAll(x => filtro.ListaExcluirPorConjuntoLista.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroConjuntoLista));

                listaBorrar.RemoveAll(x => filtro.ListaActividadCabecera.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroActividadCabecera));
                listaBorrar.RemoveAll(x => filtro.ListaOcurrencia.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOcurrencia));
                listaBorrar.RemoveAll(x => filtro.ListaDocumentoAlumno.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroDocumentoAlumno));
                listaBorrar.RemoveAll(x => filtro.ListaEstadoMatricula.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoMatricula));
                listaBorrar.RemoveAll(x => filtro.ListaSubEstadoMatricula.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubEstadoMatricula));
                listaBorrar.RemoveAll(x => filtro.ListaModalidadCurso.Any(y => y.Valor == x.Valor && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroModalidadCurso));

                this.Delete(listaBorrar.Select(x => x.Id), filtro.NombreUsuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
