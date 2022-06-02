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
    public class ProgramaGeneralPerfilEscalaProbabilidadRepositorio : BaseRepository<TProgramaGeneralPerfilEscalaProbabilidad, ProgramaGeneralPerfilEscalaProbabilidadBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilEscalaProbabilidadRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilEscalaProbabilidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilEscalaProbabilidadBO> GetBy(Expression<Func<TProgramaGeneralPerfilEscalaProbabilidad, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilEscalaProbabilidad> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilEscalaProbabilidadBO> listadoBO = new List<ProgramaGeneralPerfilEscalaProbabilidadBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilEscalaProbabilidadBO objetoBO = Mapper.Map<TProgramaGeneralPerfilEscalaProbabilidad, ProgramaGeneralPerfilEscalaProbabilidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilEscalaProbabilidadBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilEscalaProbabilidad entidad = base.FirstById(id);
                ProgramaGeneralPerfilEscalaProbabilidadBO objetoBO = new ProgramaGeneralPerfilEscalaProbabilidadBO();
                Mapper.Map<TProgramaGeneralPerfilEscalaProbabilidad, ProgramaGeneralPerfilEscalaProbabilidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilEscalaProbabilidadBO FirstBy(Expression<Func<TProgramaGeneralPerfilEscalaProbabilidad, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilEscalaProbabilidad entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilEscalaProbabilidadBO objetoBO = Mapper.Map<TProgramaGeneralPerfilEscalaProbabilidad, ProgramaGeneralPerfilEscalaProbabilidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilEscalaProbabilidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilEscalaProbabilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilEscalaProbabilidadBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilEscalaProbabilidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilEscalaProbabilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilEscalaProbabilidadBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilEscalaProbabilidad entidad, ProgramaGeneralPerfilEscalaProbabilidadBO objetoBO)
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

        private TProgramaGeneralPerfilEscalaProbabilidad MapeoEntidad(ProgramaGeneralPerfilEscalaProbabilidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilEscalaProbabilidad entidad = new TProgramaGeneralPerfilEscalaProbabilidad();
                entidad = Mapper.Map<ProgramaGeneralPerfilEscalaProbabilidadBO, TProgramaGeneralPerfilEscalaProbabilidad>(objetoBO,
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
        /// Obtiene las escalas con sus probabilidades (activos) para el perfil contacto Programa por programa 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<EscalaProbabilidadDTO> ObtenerEscalaPorPrograma(int idPGeneral)
        {

            try
            {
                List<EscalaProbabilidadDTO> resultadoDTO = new List<EscalaProbabilidadDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre,ProbabilidadActual,ProbabilidadInicial,Orden FROM pla.V_TProgramaGeneralPerfilEscalaProbabilidad WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral  = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<EscalaProbabilidadDTO>>(respuestaDapper);
                }
                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las escalas asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<EscalaProbabilidadDTO> nuevos)
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
