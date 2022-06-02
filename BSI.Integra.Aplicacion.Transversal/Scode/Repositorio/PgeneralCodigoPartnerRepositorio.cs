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
    public class PgeneralCodigoPartnerRepositorio : BaseRepository<TPgeneralCodigoPartner, PgeneralCodigoPartnerBO>
    {
        #region Metodos Base
        public PgeneralCodigoPartnerRepositorio() : base()
        {
        }
        public PgeneralCodigoPartnerRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralCodigoPartnerBO> GetBy(Expression<Func<TPgeneralCodigoPartner, bool>> filter)
        {
            IEnumerable<TPgeneralCodigoPartner> listado = base.GetBy(filter);
            List<PgeneralCodigoPartnerBO> listadoBO = new List<PgeneralCodigoPartnerBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralCodigoPartnerBO objetoBO = Mapper.Map<TPgeneralCodigoPartner, PgeneralCodigoPartnerBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralCodigoPartnerBO FirstById(int id)
        {
            try
            {
                TPgeneralCodigoPartner entidad = base.FirstById(id);
                PgeneralCodigoPartnerBO objetoBO = new PgeneralCodigoPartnerBO();
                Mapper.Map<TPgeneralCodigoPartner, PgeneralCodigoPartnerBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralCodigoPartnerBO FirstBy(Expression<Func<TPgeneralCodigoPartner, bool>> filter)
        {
            try
            {
                TPgeneralCodigoPartner entidad = base.FirstBy(filter);
                PgeneralCodigoPartnerBO objetoBO = Mapper.Map<TPgeneralCodigoPartner, PgeneralCodigoPartnerBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(PgeneralCodigoPartnerBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralCodigoPartner entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralCodigoPartnerBO> listadoBO)
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

        public bool Update(PgeneralCodigoPartnerBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralCodigoPartner entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralCodigoPartnerBO> listadoBO)
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
        private void AsignacionId(TPgeneralCodigoPartner entidad, PgeneralCodigoPartnerBO objetoBO)
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

        private TPgeneralCodigoPartner MapeoEntidad(PgeneralCodigoPartnerBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralCodigoPartner entidad = new TPgeneralCodigoPartner();
                entidad = Mapper.Map<PgeneralCodigoPartnerBO, TPgeneralCodigoPartner>(objetoBO,
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
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<PgeneralCodigoPartnerDTO> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_PgeneralCodigoPartner WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
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
        public List<PgeneralCodigoPartnerDTO> ObtenerListaCodigoPartner(int IdPgeneral)
        {
            List<PgeneralCodigoPartnerDTO> rpta = new List<PgeneralCodigoPartnerDTO>();
            string _queryCodigoPartner = "Select Id,Codigo from pla.V_ObtenerCodigoPartnerPgeneral Where Estado=1 and IdPgeneral=@IdPgeneral";
            string queryCodigoPartner = _dapper.QueryDapper(_queryCodigoPartner, new { IdPgeneral });
            if (!string.IsNullOrEmpty(queryCodigoPartner) && !queryCodigoPartner.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<PgeneralCodigoPartnerDTO>>(queryCodigoPartner);
                foreach (var item in rpta)
                {
                    item.IdVersionPrograma = new List<PgeneralCodigoPartnerVersionProgramaDTO>();
                    item.IdModalidadCurso = new List<PgeneralCodigoPartnerModalidadCursoDTO>();

                    string _queryVersionPrograma = "Select IdVersionPrograma as Id ,IdVersionPrograma From pla.T_PgeneralCodigoPartnerVersionPrograma Where Estado=1 and IdPgeneralCodigoPartner=@Id";
                    string queryVersionPrograma = _dapper.QueryDapper(_queryVersionPrograma, new { item.Id });
                    if (!string.IsNullOrEmpty(queryVersionPrograma) && !queryVersionPrograma.Contains("[]"))
                    {
                        item.IdVersionPrograma = JsonConvert.DeserializeObject<List<PgeneralCodigoPartnerVersionProgramaDTO>>(queryVersionPrograma);
                    }
                    
                    string _queryModalidadCurso = "Select IdModalidadCurso as Id,IdModalidadCurso From pla.T_PgeneralCodigoPartnerModalidadCurso Where Estado=1 and IdPgeneralCodigoPartner=@Id";
                    string queryModalidadCurso = _dapper.QueryDapper(_queryModalidadCurso, new { item.Id });
                    if (!string.IsNullOrEmpty(queryModalidadCurso) && !queryModalidadCurso.Contains("[]"))
                    {
                        item.IdModalidadCurso = JsonConvert.DeserializeObject<List<PgeneralCodigoPartnerModalidadCursoDTO>>(queryModalidadCurso);
                    }

                }
            }

            return rpta;
        }
    }
}
