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
    public class ProgramaGeneralPerfilScoringAformacionRepositorio : BaseRepository<TProgramaGeneralPerfilScoringAformacion, ProgramaGeneralPerfilScoringAformacionBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilScoringAformacionRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilScoringAformacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilScoringAformacionBO> GetBy(Expression<Func<TProgramaGeneralPerfilScoringAformacion, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilScoringAformacion> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilScoringAformacionBO> listadoBO = new List<ProgramaGeneralPerfilScoringAformacionBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilScoringAformacionBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringAformacion, ProgramaGeneralPerfilScoringAformacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilScoringAformacionBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilScoringAformacion entidad = base.FirstById(id);
                ProgramaGeneralPerfilScoringAformacionBO objetoBO = new ProgramaGeneralPerfilScoringAformacionBO();
                Mapper.Map<TProgramaGeneralPerfilScoringAformacion, ProgramaGeneralPerfilScoringAformacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilScoringAformacionBO FirstBy(Expression<Func<TProgramaGeneralPerfilScoringAformacion, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilScoringAformacion entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilScoringAformacionBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringAformacion, ProgramaGeneralPerfilScoringAformacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilScoringAformacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilScoringAformacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilScoringAformacionBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilScoringAformacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilScoringAformacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilScoringAformacionBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilScoringAformacion entidad, ProgramaGeneralPerfilScoringAformacionBO objetoBO)
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

        private TProgramaGeneralPerfilScoringAformacion MapeoEntidad(ProgramaGeneralPerfilScoringAformacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilScoringAformacion entidad = new TProgramaGeneralPerfilScoringAformacion();
                entidad = Mapper.Map<ProgramaGeneralPerfilScoringAformacionBO, TProgramaGeneralPerfilScoringAformacion>(objetoBO,
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
        /// Obtiene la lista de scoring Area formacion (activos ) por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ScoringAFormacionProgramaDTO> ObtenerScoringAFormacionPorPrograma(int idPGeneral)
        {
            try
            {

                List<ScoringAFormacionProgramaDTO> resultadoDTO = new List<ScoringAFormacionProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,IdAreaFormacion,IdSelect,Valor,Fila,Columna,Validar FROM pla.V_TProgramaGeneralPerfilScoringAformacion WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ScoringAFormacionProgramaDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Escoring Area Formacion asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ScoringAFormacionProgramaDTO> nuevos)
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
