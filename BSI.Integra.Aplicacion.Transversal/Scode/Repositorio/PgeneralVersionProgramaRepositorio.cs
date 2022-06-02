using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PgeneralVersionProgramaRepositorio : BaseRepository<TPgeneralVersionPrograma, PgeneralVersionProgramaBO>
    {
        #region Metodos Base
        public PgeneralVersionProgramaRepositorio() : base()
        {
        }
        public PgeneralVersionProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralVersionProgramaBO> GetBy(Expression<Func<TPgeneralVersionPrograma, bool>> filter)
        {
            IEnumerable<TPgeneralVersionPrograma> listado = base.GetBy(filter);
            List<PgeneralVersionProgramaBO> listadoBO = new List<PgeneralVersionProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralVersionProgramaBO objetoBO = Mapper.Map<TPgeneralVersionPrograma, PgeneralVersionProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IEnumerable<PgeneralVersionProgramaBO> GetBy(Expression<Func<TPgeneralVersionPrograma, bool>> filter, int skip, int take)
        {
            IEnumerable<TPgeneralVersionPrograma> listado = base.GetBy(filter).Skip(skip).Take(take);
            List<PgeneralVersionProgramaBO> listadoBO = new List<PgeneralVersionProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralVersionProgramaBO objetoBO = Mapper.Map<TPgeneralVersionPrograma, PgeneralVersionProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public PgeneralVersionProgramaBO FirstById(int id)
        {
            try
            {
                TPgeneralVersionPrograma entidad = base.FirstById(id);
                PgeneralVersionProgramaBO objetoBO = new PgeneralVersionProgramaBO();
                Mapper.Map<TPgeneralVersionPrograma, PgeneralVersionProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralVersionProgramaBO FirstBy(Expression<Func<TPgeneralVersionPrograma, bool>> filter)
        {
            try
            {
                TPgeneralVersionPrograma entidad = base.FirstBy(filter);
                PgeneralVersionProgramaBO objetoBO = Mapper.Map<TPgeneralVersionPrograma, PgeneralVersionProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralVersionProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralVersionPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralVersionProgramaBO> listadoBO)
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

        public bool Update(PgeneralVersionProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralVersionPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralVersionProgramaBO> listadoBO)
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
        private void AsignacionId(TPgeneralVersionPrograma entidad, PgeneralVersionProgramaBO objetoBO)
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

        private TPgeneralVersionPrograma MapeoEntidad(PgeneralVersionProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralVersionPrograma entidad = new TPgeneralVersionPrograma();
                entidad = Mapper.Map<PgeneralVersionProgramaBO, TPgeneralVersionPrograma>(objetoBO,
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
        /// Obtiene todas los paises para combobox
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaVersionesPrograma(int IdPgeneral)
        {
            string _query = string.Empty;
            try
            {
                List<FiltroDTO> _lista = new List<FiltroDTO>();
                _query = "SELECT Id, Nombre FROM pla.V_VersionesPrograma WHERE IdPgeneral=@IdPgeneral";

                var pgeneralVersiones = _dapper.QueryDapper(_query, new { IdPgeneral = IdPgeneral });
                if (!string.IsNullOrEmpty(pgeneralVersiones) && !pgeneralVersiones.Contains("[]") && pgeneralVersiones != null)
                {
                    _lista = JsonConvert.DeserializeObject<List<FiltroDTO>>(pgeneralVersiones);
                }
                return _lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<ListaPgeneralVersionProgramaDTO> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_PgeneralVersionPrograma WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdPgeneralVersionPrograma == x.Id));
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
