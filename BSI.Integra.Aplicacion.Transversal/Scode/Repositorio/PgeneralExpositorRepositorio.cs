using System;
using System.Collections.Generic;
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
    public class PgeneralExpositorRepositorio : BaseRepository<TPgeneralExpositor, PgeneralExpositorBO>
    {
        #region Metodos Base
        public PgeneralExpositorRepositorio() : base()
        {
        }
        public PgeneralExpositorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralExpositorBO> GetBy(Expression<Func<TPgeneralExpositor, bool>> filter)
        {
            IEnumerable<TPgeneralExpositor> listado = base.GetBy(filter);
            List<PgeneralExpositorBO> listadoBO = new List<PgeneralExpositorBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralExpositorBO objetoBO = Mapper.Map<TPgeneralExpositor, PgeneralExpositorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralExpositorBO FirstById(int id)
        {
            try
            {
                TPgeneralExpositor entidad = base.FirstById(id);
                PgeneralExpositorBO objetoBO = new PgeneralExpositorBO();
                Mapper.Map<TPgeneralExpositor, PgeneralExpositorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralExpositorBO FirstBy(Expression<Func<TPgeneralExpositor, bool>> filter)
        {
            try
            {
                TPgeneralExpositor entidad = base.FirstBy(filter);
                PgeneralExpositorBO objetoBO = Mapper.Map<TPgeneralExpositor, PgeneralExpositorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralExpositorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralExpositor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralExpositorBO> listadoBO)
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

        public bool Update(PgeneralExpositorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralExpositor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralExpositorBO> listadoBO)
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
        private void AsignacionId(TPgeneralExpositor entidad, PgeneralExpositorBO objetoBO)
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

        private TPgeneralExpositor MapeoEntidad(PgeneralExpositorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralExpositor entidad = new TPgeneralExpositor();
                entidad = Mapper.Map<PgeneralExpositorBO, TPgeneralExpositor>(objetoBO,
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
        /// Obtiene los Ids de los Expositores registrados para un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<int> ObtenerExpositoresPorPrograma(int idPGeneral)
        {
            try
            {
                var expositores = GetBy(x => x.Estado == true && x.IdPgeneral == idPGeneral).Select(x=>x.IdExpositor).ToList();
       
                return expositores;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Expositores Por Programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_PGeneralExpositor WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.Id));
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
        /// Obtiene el Top 5 de los Expositores
        /// </summary>
        /// <param name="idPrioridad"></param>
        /// <returns></returns>
        public List<CampaniaMailingNombreProgramaDetalleDTO> ObtenerTop5PGeneralExpositor(int idPrioridad)
        {
            try
            {
                List<CampaniaMailingNombreProgramaDetalleDTO> listaExpositor = new List<CampaniaMailingNombreProgramaDetalleDTO>();
                string _queryObtenerExpositores = "select Top 5 IdPGeneral, Nombre, Contenido, Etiqueta from mkt.V_ObtenerPGeneralExpositor " +
                    "where IdCampaniaMailingDetalle = @IdCampaniaMailingDetalle and EstadoDetallePrograma = 1 and TipoPrograma != @TipoPrograma ";
                string registrosBD = _dapper.QueryDapper(_queryObtenerExpositores, new { @IdCampaniaMailingDetalle = idPrioridad, @TipoPrograma = "Filtro" });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    listaExpositor = JsonConvert.DeserializeObject<List<CampaniaMailingNombreProgramaDetalleDTO>>(registrosBD);
                }
                return listaExpositor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Top 5 de los Expositores
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general (PK de la tabla pla.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de tipo CampaniaMailingNombreProgramaDetalleDTO</returns>
        public List<CampaniaMailingNombreProgramaDetalleDTO> ObtenerTop5PGeneralExpositorCampaniaGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaMailingNombreProgramaDetalleDTO> listaExpositor = new List<CampaniaMailingNombreProgramaDetalleDTO>();
                string queryObtenerExpositores = "select Top 5 IdPGeneral, Nombre, Contenido, Etiqueta from mkt.V_ObtenerPGeneralExpositorCampaniaGeneral " +
                    "WHERE IdCampaniaGeneralDetalle = @IdCampaniaGeneralDetalle";
                string registrosBD = _dapper.QueryDapper(queryObtenerExpositores, new { @IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
                if (!string.IsNullOrEmpty(registrosBD) && !registrosBD.Contains("[]"))
                {
                    listaExpositor = JsonConvert.DeserializeObject<List<CampaniaMailingNombreProgramaDetalleDTO>>(registrosBD);
                }
                return listaExpositor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
