using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class GastoFinancieroCronogramaDetalleRepositorio : BaseRepository<TGastoFinancieroCronogramaDetalle, GastoFinancieroCronogramaDetalleBO>
    {
        #region Metodos Base
        public GastoFinancieroCronogramaDetalleRepositorio() : base()
        {
        }
        public GastoFinancieroCronogramaDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GastoFinancieroCronogramaDetalleBO> GetBy(Expression<Func<TGastoFinancieroCronogramaDetalle, bool>> filter)
        {
            IEnumerable<TGastoFinancieroCronogramaDetalle> listado = base.GetBy(filter);
            List<GastoFinancieroCronogramaDetalleBO> listadoBO = new List<GastoFinancieroCronogramaDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                GastoFinancieroCronogramaDetalleBO objetoBO = Mapper.Map<TGastoFinancieroCronogramaDetalle, GastoFinancieroCronogramaDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GastoFinancieroCronogramaDetalleBO FirstById(int id)
        {
            try
            {
                TGastoFinancieroCronogramaDetalle entidad = base.FirstById(id);
                GastoFinancieroCronogramaDetalleBO objetoBO = new GastoFinancieroCronogramaDetalleBO();
                Mapper.Map<TGastoFinancieroCronogramaDetalle, GastoFinancieroCronogramaDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GastoFinancieroCronogramaDetalleBO FirstBy(Expression<Func<TGastoFinancieroCronogramaDetalle, bool>> filter)
        {
            try
            {
                TGastoFinancieroCronogramaDetalle entidad = base.FirstBy(filter);
                GastoFinancieroCronogramaDetalleBO objetoBO = Mapper.Map<TGastoFinancieroCronogramaDetalle, GastoFinancieroCronogramaDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GastoFinancieroCronogramaDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGastoFinancieroCronogramaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GastoFinancieroCronogramaDetalleBO> listadoBO)
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

        public bool Update(GastoFinancieroCronogramaDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGastoFinancieroCronogramaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GastoFinancieroCronogramaDetalleBO> listadoBO)
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
        private void AsignacionId(TGastoFinancieroCronogramaDetalle entidad, GastoFinancieroCronogramaDetalleBO objetoBO)
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

        private TGastoFinancieroCronogramaDetalle MapeoEntidad(GastoFinancieroCronogramaDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGastoFinancieroCronogramaDetalle entidad = new TGastoFinancieroCronogramaDetalle();
                entidad = Mapper.Map<GastoFinancieroCronogramaDetalleBO, TGastoFinancieroCronogramaDetalle>(objetoBO,
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
        /// Obtiene [Id, Valor] de las GastoFinancieroCronogramaDetallees existentes en una lista 
        /// para ser mostradas en un ComboBox (utilizado en CRUD 'RendicionRequerimientos')
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaGastoFinancieroCronogramaDetalle()
        {
            try
            {
                List<FiltroDTO> GastoFinancieroCronogramaDetallees = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Valor as Nombre FROM fin.T_GastoFinancieroCronogramaDetalle WHERE Estado = 1";
                var GastoFinancieroCronogramaDetalleesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(GastoFinancieroCronogramaDetalleesDB) && !GastoFinancieroCronogramaDetalleesDB.Contains("[]"))
                {
                    GastoFinancieroCronogramaDetallees = JsonConvert.DeserializeObject<List<FiltroDTO>>(GastoFinancieroCronogramaDetalleesDB);
                }
                return GastoFinancieroCronogramaDetallees;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las GastoFinancieroCronogramaDetalle asociados a un GastoFinancieroCronograma
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorCronograma(int idGastoFinancieroCronograma, string usuario, List<GastoFinancieroCronogramaDetalleDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdGastoFinancieroCronograma == idGastoFinancieroCronograma && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
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

        /// <summary>
        /// Obtiene los CronogramaDetalle (Cutas) dado un id de GastoFinanciero (Cronograma)
        /// para ser mostradas en una grilla
        /// </summary>
        /// <returns></returns>
        public List<GastoFinancieroCronogramaDetalleDTO> ObtenerListaGastoFinancieroCronogramaDetallePorIdGastoFinanciero(int IdCronograma)
        {
            try
            {
                List<GastoFinancieroCronogramaDetalleDTO> GastoFinancieroCronogramaDetallees = new List<GastoFinancieroCronogramaDetalleDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdGastoFinancieroCronograma, NumeroCuota, CapitalCuota, InteresCuota,FechaVencimientoCuota FROM fin.T_GastoFinancieroCronogramaDetalle WHERE Estado = 1 and IdGastoFinancieroCronograma="+IdCronograma;
                var GastoFinancieroCronogramaDetalleesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(GastoFinancieroCronogramaDetalleesDB) && !GastoFinancieroCronogramaDetalleesDB.Contains("[]"))
                {
                    GastoFinancieroCronogramaDetallees = JsonConvert.DeserializeObject<List<GastoFinancieroCronogramaDetalleDTO>>(GastoFinancieroCronogramaDetalleesDB);
                }
                return GastoFinancieroCronogramaDetallees;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
