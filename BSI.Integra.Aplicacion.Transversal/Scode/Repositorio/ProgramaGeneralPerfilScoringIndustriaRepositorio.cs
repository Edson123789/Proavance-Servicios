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
    public class ProgramaGeneralPerfilScoringIndustriaRepositorio : BaseRepository<TProgramaGeneralPerfilScoringIndustria, ProgramaGeneralPerfilScoringIndustriaBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilScoringIndustriaRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilScoringIndustriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilScoringIndustriaBO> GetBy(Expression<Func<TProgramaGeneralPerfilScoringIndustria, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilScoringIndustria> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilScoringIndustriaBO> listadoBO = new List<ProgramaGeneralPerfilScoringIndustriaBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilScoringIndustriaBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringIndustria, ProgramaGeneralPerfilScoringIndustriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilScoringIndustriaBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilScoringIndustria entidad = base.FirstById(id);
                ProgramaGeneralPerfilScoringIndustriaBO objetoBO = new ProgramaGeneralPerfilScoringIndustriaBO();
                Mapper.Map<TProgramaGeneralPerfilScoringIndustria, ProgramaGeneralPerfilScoringIndustriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilScoringIndustriaBO FirstBy(Expression<Func<TProgramaGeneralPerfilScoringIndustria, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilScoringIndustria entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilScoringIndustriaBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringIndustria, ProgramaGeneralPerfilScoringIndustriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilScoringIndustriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilScoringIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilScoringIndustriaBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilScoringIndustriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilScoringIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilScoringIndustriaBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilScoringIndustria entidad, ProgramaGeneralPerfilScoringIndustriaBO objetoBO)
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

        private TProgramaGeneralPerfilScoringIndustria MapeoEntidad(ProgramaGeneralPerfilScoringIndustriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilScoringIndustria entidad = new TProgramaGeneralPerfilScoringIndustria();
                entidad = Mapper.Map<ProgramaGeneralPerfilScoringIndustriaBO, TProgramaGeneralPerfilScoringIndustria>(objetoBO,
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
        /// Obtiene la lista de scoring Industria (activos ) por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ScoringIndustriaProgramaDTO> ObtenerScoringIndustriaPorPrograma(int idPGeneral)
        {
            try
            {

                List<ScoringIndustriaProgramaDTO> resultadoDTO = new List<ScoringIndustriaProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,IdIndustria,IdSelect,Valor,Fila,Columna,Validar FROM pla.V_TProgramaGeneralPerfilScoringIndustria WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ScoringIndustriaProgramaDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Escoring  Industria asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ScoringIndustriaProgramaDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPgeneral == idPGeneral && x.Estado == true).ToList();
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
    }
}
