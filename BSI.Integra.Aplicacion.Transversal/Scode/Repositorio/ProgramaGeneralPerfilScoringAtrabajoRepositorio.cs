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
    public class ProgramaGeneralPerfilScoringAtrabajoRepositorio : BaseRepository<TProgramaGeneralPerfilScoringAtrabajo, ProgramaGeneralPerfilScoringAtrabajoBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilScoringAtrabajoRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilScoringAtrabajoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilScoringAtrabajoBO> GetBy(Expression<Func<TProgramaGeneralPerfilScoringAtrabajo, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilScoringAtrabajo> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilScoringAtrabajoBO> listadoBO = new List<ProgramaGeneralPerfilScoringAtrabajoBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilScoringAtrabajoBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringAtrabajo, ProgramaGeneralPerfilScoringAtrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilScoringAtrabajoBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilScoringAtrabajo entidad = base.FirstById(id);
                ProgramaGeneralPerfilScoringAtrabajoBO objetoBO = new ProgramaGeneralPerfilScoringAtrabajoBO();
                Mapper.Map<TProgramaGeneralPerfilScoringAtrabajo, ProgramaGeneralPerfilScoringAtrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilScoringAtrabajoBO FirstBy(Expression<Func<TProgramaGeneralPerfilScoringAtrabajo, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilScoringAtrabajo entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilScoringAtrabajoBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringAtrabajo, ProgramaGeneralPerfilScoringAtrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilScoringAtrabajoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilScoringAtrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilScoringAtrabajoBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilScoringAtrabajoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilScoringAtrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilScoringAtrabajoBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilScoringAtrabajo entidad, ProgramaGeneralPerfilScoringAtrabajoBO objetoBO)
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

        private TProgramaGeneralPerfilScoringAtrabajo MapeoEntidad(ProgramaGeneralPerfilScoringAtrabajoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilScoringAtrabajo entidad = new TProgramaGeneralPerfilScoringAtrabajo();
                entidad = Mapper.Map<ProgramaGeneralPerfilScoringAtrabajoBO, TProgramaGeneralPerfilScoringAtrabajo>(objetoBO,
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
        /// Obtiene la lista de scoring Area de Trabajo (activos ) por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ScoringTrabajoProgramaDTO> ObtenerScoringTrabajoPorPrograma(int idPGeneral)
        {
            try
            {

                List<ScoringTrabajoProgramaDTO> resultadoDTO = new List<ScoringTrabajoProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,IdAreaTrabajo,IdSelect,Valor,Fila,Columna,Validar FROM pla.V_TProgramaGeneralPerfilScoringAtrabajo WHERE " +
                    "Estado = 1 and IdPGeneral = @idPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ScoringTrabajoProgramaDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Escoring de Area de Trabajo asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ScoringTrabajoProgramaDTO> nuevos)
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
