using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class CampaniaGeneralDetalleProgramaRepositorio : BaseRepository<TCampaniaGeneralDetallePrograma, CampaniaGeneralDetalleProgramaBO>
    {
        #region Metodos Base
        public CampaniaGeneralDetalleProgramaRepositorio() : base()
        {
        }
        public CampaniaGeneralDetalleProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaGeneralDetalleProgramaBO> GetBy(Expression<Func<TCampaniaGeneralDetallePrograma, bool>> filter)
        {
            IEnumerable<TCampaniaGeneralDetallePrograma> listado = base.GetBy(filter);
            List<CampaniaGeneralDetalleProgramaBO> listadoBO = new List<CampaniaGeneralDetalleProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralDetalleProgramaBO objetoBO = Mapper.Map<TCampaniaGeneralDetallePrograma, CampaniaGeneralDetalleProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaGeneralDetalleProgramaBO FirstById(int id)
        {
            try
            {
                TCampaniaGeneralDetallePrograma entidad = base.FirstById(id);
                CampaniaGeneralDetalleProgramaBO objetoBO = new CampaniaGeneralDetalleProgramaBO();
                Mapper.Map<TCampaniaGeneralDetallePrograma, CampaniaGeneralDetalleProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaGeneralDetalleProgramaBO FirstBy(Expression<Func<TCampaniaGeneralDetallePrograma, bool>> filter)
        {
            try
            {
                TCampaniaGeneralDetallePrograma entidad = base.FirstBy(filter);
                CampaniaGeneralDetalleProgramaBO objetoBO = Mapper.Map<TCampaniaGeneralDetallePrograma, CampaniaGeneralDetalleProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaGeneralDetalleProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaGeneralDetallePrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampaniaGeneralDetalleProgramaBO> listadoBO)
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

        public bool Update(CampaniaGeneralDetalleProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaGeneralDetallePrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampaniaGeneralDetalleProgramaBO> listadoBO)
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
        private void AsignacionId(TCampaniaGeneralDetallePrograma entidad, CampaniaGeneralDetalleProgramaBO objetoBO)
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

        private TCampaniaGeneralDetallePrograma MapeoEntidad(CampaniaGeneralDetalleProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaGeneralDetallePrograma entidad = new TCampaniaGeneralDetallePrograma();
                entidad = Mapper.Map<CampaniaGeneralDetalleProgramaBO, TCampaniaGeneralDetallePrograma>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CampaniaGeneralDetalleProgramaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCampaniaGeneralDetallePrograma, bool>>> filters, Expression<Func<TCampaniaGeneralDetallePrograma, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TCampaniaGeneralDetallePrograma> listado = base.GetFiltered(filters, orderBy, ascending);
            List<CampaniaGeneralDetalleProgramaBO> listadoBO = new List<CampaniaGeneralDetalleProgramaBO>();

            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralDetalleProgramaBO objetoBO = Mapper.Map<TCampaniaGeneralDetallePrograma, CampaniaGeneralDetalleProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Se obtiene la lista de los filtros segmentos de valor Tipo por la campania general detalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de clase FiltroSegmentoValorTipoDTO</returns>
        public List<FiltroSegmentoValorTipoDTO> ObtenerPorIdCampaniaGeneralDetalle(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<FiltroSegmentoValorTipoDTO> resultadoFinal = new List<FiltroSegmentoValorTipoDTO>();

                string queryDapper = "SELECT Valor, IdCategoriaObjetoFiltro FROM mkt.V_TCampaniaGeneralDetallePGeneral_CampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle";

                var listaRegistros = _dapper.QueryDapper(queryDapper, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
                if (!string.IsNullOrEmpty(listaRegistros) && !listaRegistros.Contains("[]"))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<FiltroSegmentoValorTipoDTO>>(listaRegistros);
                }

                return resultadoFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obteniene el Id de T_PGeneral y la Etiqueta
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de tipo CampaniaMailingPGeneralEtiquetaDTO</returns>
        public List<CampaniaMailingPGeneralEtiquetaDTO> ObtenerProgramaYEtiqueta(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaMailingPGeneralEtiquetaDTO> listaPGeneralEtiqueta = new List<CampaniaMailingPGeneralEtiquetaDTO>();
                string _queryObtenerPGeneralEtiqueta = "select IdPGeneral, Etiqueta from mkt.V_ObtenerPGeneralEtiquetaCampaniaGeneral " +
                    "where IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle";
                string registrosBD = _dapper.QueryDapper(_queryObtenerPGeneralEtiqueta, new
                {
                    IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle
                });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    listaPGeneralEtiqueta = JsonConvert.DeserializeObject<List<CampaniaMailingPGeneralEtiquetaDTO>>(registrosBD);
                }
                return listaPGeneralEtiqueta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
