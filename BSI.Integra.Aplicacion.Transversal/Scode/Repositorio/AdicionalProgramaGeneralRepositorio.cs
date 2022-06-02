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
    public class AdicionalProgramaGeneralRepositorio : BaseRepository<TAdicionalProgramaGeneral, AdicionalProgramaGeneralBO>
    {
        #region Metodos Base
        public AdicionalProgramaGeneralRepositorio() : base()
        {
        }
        public AdicionalProgramaGeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AdicionalProgramaGeneralBO> GetBy(Expression<Func<TAdicionalProgramaGeneral, bool>> filter)
        {
            IEnumerable<TAdicionalProgramaGeneral> listado = base.GetBy(filter);
            List<AdicionalProgramaGeneralBO> listadoBO = new List<AdicionalProgramaGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                AdicionalProgramaGeneralBO objetoBO = Mapper.Map<TAdicionalProgramaGeneral, AdicionalProgramaGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AdicionalProgramaGeneralBO FirstById(int id)
        {
            try
            {
                TAdicionalProgramaGeneral entidad = base.FirstById(id);
                AdicionalProgramaGeneralBO objetoBO = new AdicionalProgramaGeneralBO();
                Mapper.Map<TAdicionalProgramaGeneral, AdicionalProgramaGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AdicionalProgramaGeneralBO FirstBy(Expression<Func<TAdicionalProgramaGeneral, bool>> filter)
        {
            try
            {
                TAdicionalProgramaGeneral entidad = base.FirstBy(filter);
                AdicionalProgramaGeneralBO objetoBO = Mapper.Map<TAdicionalProgramaGeneral, AdicionalProgramaGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AdicionalProgramaGeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAdicionalProgramaGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AdicionalProgramaGeneralBO> listadoBO)
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

        public bool Update(AdicionalProgramaGeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAdicionalProgramaGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AdicionalProgramaGeneralBO> listadoBO)
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
        private void AsignacionId(TAdicionalProgramaGeneral entidad, AdicionalProgramaGeneralBO objetoBO)
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

        public List<DatoAdicionalPaginaDTO> ObtenerAdicionalProgramaPorIdPlantilla(int? idPlantilla, int idPGeneral)
        {
            throw new NotImplementedException();
        }

        private TAdicionalProgramaGeneral MapeoEntidad(AdicionalProgramaGeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAdicionalProgramaGeneral entidad = new TAdicionalProgramaGeneral();
                entidad = Mapper.Map<AdicionalProgramaGeneralBO, TAdicionalProgramaGeneral>(objetoBO,
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
        ///  Obtiene la lista Descripciones Adicionales  para un programa general registradas en el sistema(activos)
        /// </summary>
        /// <returns></returns>
        public List<PgeneralAdicionalInformacionDTO> ObtenerDescripcionesAdicionales(int idPGeneral)
        {
            try
            {
                List<PgeneralAdicionalInformacionDTO> datosBD = new List<PgeneralAdicionalInformacionDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Descripcion,NombreImagen,IdTitulo,NombreTitulo FROM pla.V_TAdicionalProgramaGeneral WHERE Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query,new { IdPGeneral= idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    datosBD = JsonConvert.DeserializeObject<List<PgeneralAdicionalInformacionDTO>>(respuestaDapper);
                }

                return datosBD;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Adicional Descripciones  Por Programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<PgeneralAdicionalInformacionDTO> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_AdicionalProgramaGeneral WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
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
        /// Obtiene los adicionales de PGeneral de acuerdo al IdPlantilla y IdPGeneral
        /// </summary>
        /// <param name="IdPlantilla"></param>
        /// <param name="IdPGeneral"></param>
        /// <returns></returns>
        public List<DatoAdicionalPaginaDTO> ObtenerAdicionalProgramaPorIdPlantilla(int IdPlantilla, int IdPGeneral)
        {
            try
            {
                List<DatoAdicionalPaginaDTO> lista = new List<DatoAdicionalPaginaDTO>();
                string _query = "SELECT IdTitulo, NombreTitulo, Descripcion, NombreImagen, ColorTitulo, ColorDescripcion " +
                " FROM  mkt.V_ObtenerPlantillaAdicionalPGeneral WHERE EstadoAdicional = 1 and IdPGeneral = @idPGeneral and IdPlantillaLandingPage = @IdPlantilla" +
                " ORDER BY IdPlantillaLandingPagePGeneralAdicional ASC";
                var query = _dapper.QueryDapper(_query, new { IdPGeneral = IdPGeneral, IdPlantilla = IdPlantilla });
                lista = JsonConvert.DeserializeObject<List<DatoAdicionalPaginaDTO>>(query);
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
