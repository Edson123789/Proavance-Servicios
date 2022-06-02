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
    public class CampaniaGeneralDetalleSubAreaRepositorio : BaseRepository<TCampaniaGeneralDetalleSubArea, CampaniaGeneralDetalleSubAreaBO>
    {
        #region Metodos Base
        public CampaniaGeneralDetalleSubAreaRepositorio() : base()
        {
        }
        public CampaniaGeneralDetalleSubAreaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaGeneralDetalleSubAreaBO> GetBy(Expression<Func<TCampaniaGeneralDetalleSubArea, bool>> filter)
        {
            IEnumerable<TCampaniaGeneralDetalleSubArea> listado = base.GetBy(filter);
            List<CampaniaGeneralDetalleSubAreaBO> listadoBO = new List<CampaniaGeneralDetalleSubAreaBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralDetalleSubAreaBO objetoBO = Mapper.Map<TCampaniaGeneralDetalleSubArea, CampaniaGeneralDetalleSubAreaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaGeneralDetalleSubAreaBO FirstById(int id)
        {
            try
            {
                TCampaniaGeneralDetalleSubArea entidad = base.FirstById(id);
                CampaniaGeneralDetalleSubAreaBO objetoBO = new CampaniaGeneralDetalleSubAreaBO();
                Mapper.Map<TCampaniaGeneralDetalleSubArea, CampaniaGeneralDetalleSubAreaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaGeneralDetalleSubAreaBO FirstBy(Expression<Func<TCampaniaGeneralDetalleSubArea, bool>> filter)
        {
            try
            {
                TCampaniaGeneralDetalleSubArea entidad = base.FirstBy(filter);
                CampaniaGeneralDetalleSubAreaBO objetoBO = Mapper.Map<TCampaniaGeneralDetalleSubArea, CampaniaGeneralDetalleSubAreaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaGeneralDetalleSubAreaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaGeneralDetalleSubArea entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampaniaGeneralDetalleSubAreaBO> listadoBO)
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

        public bool Update(CampaniaGeneralDetalleSubAreaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaGeneralDetalleSubArea entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampaniaGeneralDetalleSubAreaBO> listadoBO)
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
        private void AsignacionId(TCampaniaGeneralDetalleSubArea entidad, CampaniaGeneralDetalleSubAreaBO objetoBO)
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

        private TCampaniaGeneralDetalleSubArea MapeoEntidad(CampaniaGeneralDetalleSubAreaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaGeneralDetalleSubArea entidad = new TCampaniaGeneralDetalleSubArea();
                entidad = Mapper.Map<CampaniaGeneralDetalleSubAreaBO, TCampaniaGeneralDetalleSubArea>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CampaniaGeneralDetalleSubAreaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCampaniaGeneralDetalleSubArea, bool>>> filters, Expression<Func<TCampaniaGeneralDetalleSubArea, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TCampaniaGeneralDetalleSubArea> listado = base.GetFiltered(filters, orderBy, ascending);
            List<CampaniaGeneralDetalleSubAreaBO> listadoBO = new List<CampaniaGeneralDetalleSubAreaBO>();

            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralDetalleSubAreaBO objetoBO = Mapper.Map<TCampaniaGeneralDetalleSubArea, CampaniaGeneralDetalleSubAreaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
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

                string queryDapper = "SELECT Valor, IdCategoriaObjetoFiltro FROM mkt.V_TCampaniaGeneralDetalleSubArea_CampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle";

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
        /// Elimina (Actualiza estado a false ) todos las SubAreas Clave Valor asociados a una Campania General Detalle
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorCampaniaGeneral(int idCampaniaGeneralDetalle, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdCampaniaGeneralDetalle == idCampaniaGeneralDetalle && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdSubAreaCapacitacion));
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
    }
}
