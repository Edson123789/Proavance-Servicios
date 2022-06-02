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
    /// Repositorio: PGeneralParametroSeoPwRepositorio
    /// Autor:Jose Villena
    /// Fecha: 03/05/2021
    /// <summary>
    /// Repositorio para consultas de T_PGeneralParametroSeoPw
    /// </summary>
    public class PGeneralParametroSeoPwRepositorio : BaseRepository<TPgeneralParametroSeoPw, PgeneralParametroSeoPwBO>
    {
        #region Metodos Base
        public PGeneralParametroSeoPwRepositorio() : base()
        {
        }
        public PGeneralParametroSeoPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralParametroSeoPwBO> GetBy(Expression<Func<TPgeneralParametroSeoPw, bool>> filter)
        {
            IEnumerable<TPgeneralParametroSeoPw> listado = base.GetBy(filter);
            List<PgeneralParametroSeoPwBO> listadoBO = new List<PgeneralParametroSeoPwBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralParametroSeoPwBO objetoBO = Mapper.Map<TPgeneralParametroSeoPw, PgeneralParametroSeoPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralParametroSeoPwBO FirstById(int id)
        {
            try
            {
                TPgeneralParametroSeoPw entidad = base.FirstById(id);
                PgeneralParametroSeoPwBO objetoBO = new PgeneralParametroSeoPwBO();
                Mapper.Map<TPgeneralParametroSeoPw, PgeneralParametroSeoPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralParametroSeoPwBO FirstBy(Expression<Func<TPgeneralParametroSeoPw, bool>> filter)
        {
            try
            {
                TPgeneralParametroSeoPw entidad = base.FirstBy(filter);
                PgeneralParametroSeoPwBO objetoBO = Mapper.Map<TPgeneralParametroSeoPw, PgeneralParametroSeoPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralParametroSeoPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralParametroSeoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralParametroSeoPwBO> listadoBO)
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

        public bool Update(PgeneralParametroSeoPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralParametroSeoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralParametroSeoPwBO> listadoBO)
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
        private void AsignacionId(TPgeneralParametroSeoPw entidad, PgeneralParametroSeoPwBO objetoBO)
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

        private TPgeneralParametroSeoPw MapeoEntidad(PgeneralParametroSeoPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralParametroSeoPw entidad = new TPgeneralParametroSeoPw();
                entidad = Mapper.Map<PgeneralParametroSeoPwBO, TPgeneralParametroSeoPw>(objetoBO,
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



        ///Repositorio: PGeneralParametroSeoPwRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene los parametros SEO de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> Lista Parametros SEO: List<ParametroSeoPGeneralDTO></returns> 
        public List<ParametroSeoPGeneralDTO> ObtenerParametrosSEOPorIdPGeneral(int idPGeneral)
        {
            try
            {
                List<ParametroSeoPGeneralDTO> parametrosSeoPGeneral = new List<ParametroSeoPGeneralDTO>();
                string query = "SELECT * FROM  pla.V_ParametrosSeoPrograma WHERE IdProgramaGeneral = @idPGeneral ";
                var parametroSEOProgramaGeneral = _dapper.QueryDapper(query, new { idPGeneral });
                parametrosSeoPGeneral = JsonConvert.DeserializeObject<List<ParametroSeoPGeneralDTO>>(parametroSEOProgramaGeneral);
                return parametrosSeoPGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los parametros seo Por Programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<ParametrosSeoProgramaDTO> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_PGeneralParametroSEO_PW WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdPGeneralParametroSEO == x.Id));
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
