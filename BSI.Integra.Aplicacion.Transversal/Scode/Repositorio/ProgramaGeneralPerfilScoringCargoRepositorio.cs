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
    public class ProgramaGeneralPerfilScoringCargoRepositorio : BaseRepository<TProgramaGeneralPerfilScoringCargo, ProgramaGeneralPerfilScoringCargoBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilScoringCargoRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilScoringCargoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilScoringCargoBO> GetBy(Expression<Func<TProgramaGeneralPerfilScoringCargo, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilScoringCargo> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilScoringCargoBO> listadoBO = new List<ProgramaGeneralPerfilScoringCargoBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilScoringCargoBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringCargo, ProgramaGeneralPerfilScoringCargoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilScoringCargoBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilScoringCargo entidad = base.FirstById(id);
                ProgramaGeneralPerfilScoringCargoBO objetoBO = new ProgramaGeneralPerfilScoringCargoBO();
                Mapper.Map<TProgramaGeneralPerfilScoringCargo, ProgramaGeneralPerfilScoringCargoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilScoringCargoBO FirstBy(Expression<Func<TProgramaGeneralPerfilScoringCargo, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilScoringCargo entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilScoringCargoBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringCargo, ProgramaGeneralPerfilScoringCargoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilScoringCargoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilScoringCargo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilScoringCargoBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilScoringCargoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilScoringCargo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilScoringCargoBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilScoringCargo entidad, ProgramaGeneralPerfilScoringCargoBO objetoBO)
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

        private TProgramaGeneralPerfilScoringCargo MapeoEntidad(ProgramaGeneralPerfilScoringCargoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilScoringCargo entidad = new TProgramaGeneralPerfilScoringCargo();
                entidad = Mapper.Map<ProgramaGeneralPerfilScoringCargoBO, TProgramaGeneralPerfilScoringCargo>(objetoBO,
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
        /// Obtiene la lista de scoring Cargo (activos ) por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ScoringCargoProgramaDTO> ObtenerScoringCargoPorPrograma(int idPGeneral)
        {
            try
            {

                List<ScoringCargoProgramaDTO> resultadoDTO = new List<ScoringCargoProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,IdCargo,IdSelect,Valor,Fila,Columna,Validar FROM pla.V_TProgramaGeneralPerfilScoringCargo WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ScoringCargoProgramaDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Escoring  Cargo asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ScoringCargoProgramaDTO> nuevos)
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
