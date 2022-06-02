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
    public class CampaniaGeneralDetalleAreaRepositorio : BaseRepository<TCampaniaGeneralDetalleArea, CampaniaGeneralDetalleAreaBO>
    {
        #region Metodos Base
        public CampaniaGeneralDetalleAreaRepositorio() : base()
        {
        }
        public CampaniaGeneralDetalleAreaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaGeneralDetalleAreaBO> GetBy(Expression<Func<TCampaniaGeneralDetalleArea, bool>> filter)
        {
            IEnumerable<TCampaniaGeneralDetalleArea> listado = base.GetBy(filter);
            List<CampaniaGeneralDetalleAreaBO> listadoBO = new List<CampaniaGeneralDetalleAreaBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralDetalleAreaBO objetoBO = Mapper.Map<TCampaniaGeneralDetalleArea, CampaniaGeneralDetalleAreaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaGeneralDetalleAreaBO FirstById(int id)
        {
            try
            {
                TCampaniaGeneralDetalleArea entidad = base.FirstById(id);
                CampaniaGeneralDetalleAreaBO objetoBO = new CampaniaGeneralDetalleAreaBO();
                Mapper.Map<TCampaniaGeneralDetalleArea, CampaniaGeneralDetalleAreaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaGeneralDetalleAreaBO FirstBy(Expression<Func<TCampaniaGeneralDetalleArea, bool>> filter)
        {
            try
            {
                TCampaniaGeneralDetalleArea entidad = base.FirstBy(filter);
                CampaniaGeneralDetalleAreaBO objetoBO = Mapper.Map<TCampaniaGeneralDetalleArea, CampaniaGeneralDetalleAreaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaGeneralDetalleAreaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaGeneralDetalleArea entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampaniaGeneralDetalleAreaBO> listadoBO)
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

        public bool Update(CampaniaGeneralDetalleAreaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaGeneralDetalleArea entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampaniaGeneralDetalleAreaBO> listadoBO)
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
        private void AsignacionId(TCampaniaGeneralDetalleArea entidad, CampaniaGeneralDetalleAreaBO objetoBO)
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

        private TCampaniaGeneralDetalleArea MapeoEntidad(CampaniaGeneralDetalleAreaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaGeneralDetalleArea entidad = new TCampaniaGeneralDetalleArea();
                entidad = Mapper.Map<CampaniaGeneralDetalleAreaBO, TCampaniaGeneralDetalleArea>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CampaniaGeneralDetalleAreaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCampaniaGeneralDetalleArea, bool>>> filters, Expression<Func<TCampaniaGeneralDetalleArea, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TCampaniaGeneralDetalleArea> listado = base.GetFiltered(filters, orderBy, ascending);
            List<CampaniaGeneralDetalleAreaBO> listadoBO = new List<CampaniaGeneralDetalleAreaBO>();

            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralDetalleAreaBO objetoBO = Mapper.Map<TCampaniaGeneralDetalleArea, CampaniaGeneralDetalleAreaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
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

                string queryDapper = "SELECT Valor, IdCategoriaObjetoFiltro FROM mkt.V_TCampaniaGeneralDetalleArea_CampaniaGeneral WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle";

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
        /// Eliminado logico por id de la campania general detalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <param name="usuario">Usuario responsable del cambio</param>
        /// <param name="nuevos">Lista de ids nuevos</param>
        public void EliminacionLogicoPorCampaniaGeneral(int idCampaniaGeneralDetalle, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdCampaniaGeneralDetalle == idCampaniaGeneralDetalle && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdAreaCapacitacion));
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
