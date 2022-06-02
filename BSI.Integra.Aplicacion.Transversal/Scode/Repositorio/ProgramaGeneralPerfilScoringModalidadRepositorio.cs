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
    public class ProgramaGeneralPerfilScoringModalidadRepositorio : BaseRepository<TProgramaGeneralPerfilScoringModalidad, ProgramaGeneralPerfilScoringModalidadBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilScoringModalidadRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilScoringModalidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilScoringModalidadBO> GetBy(Expression<Func<TProgramaGeneralPerfilScoringModalidad, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilScoringModalidad> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilScoringModalidadBO> listadoBO = new List<ProgramaGeneralPerfilScoringModalidadBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilScoringModalidadBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringModalidad, ProgramaGeneralPerfilScoringModalidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilScoringModalidadBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilScoringModalidad entidad = base.FirstById(id);
                ProgramaGeneralPerfilScoringModalidadBO objetoBO = new ProgramaGeneralPerfilScoringModalidadBO();
                Mapper.Map<TProgramaGeneralPerfilScoringModalidad, ProgramaGeneralPerfilScoringModalidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilScoringModalidadBO FirstBy(Expression<Func<TProgramaGeneralPerfilScoringModalidad, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilScoringModalidad entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilScoringModalidadBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringModalidad, ProgramaGeneralPerfilScoringModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilScoringModalidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilScoringModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilScoringModalidadBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilScoringModalidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilScoringModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilScoringModalidadBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilScoringModalidad entidad, ProgramaGeneralPerfilScoringModalidadBO objetoBO)
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

        private TProgramaGeneralPerfilScoringModalidad MapeoEntidad(ProgramaGeneralPerfilScoringModalidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilScoringModalidad entidad = new TProgramaGeneralPerfilScoringModalidad();
                entidad = Mapper.Map<ProgramaGeneralPerfilScoringModalidadBO, TProgramaGeneralPerfilScoringModalidad>(objetoBO,
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
        /// Obtiene la lista de scoring Modalidades (activos ) por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ScoringModalidadProgramaDTO> ObtenerScoringModalidadPorPrograma(int idPGeneral)
        {
            try
            {

                List<ScoringModalidadProgramaDTO> resultadoDTO = new List<ScoringModalidadProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,IdModalidad,IdSelect,Valor,Fila,Columna,Validar FROM pla.V_TProgramaGeneralPerfilScoringModalidad WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ScoringModalidadProgramaDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Escoring Modalidad asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ScoringModalidadProgramaDTO> nuevos)
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
