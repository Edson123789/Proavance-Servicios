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
    public class ProgramaGeneralPerfilScoringCiudadRepositorio : BaseRepository<TProgramaGeneralPerfilScoringCiudad, ProgramaGeneralPerfilScoringCiudadBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilScoringCiudadRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilScoringCiudadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilScoringCiudadBO> GetBy(Expression<Func<TProgramaGeneralPerfilScoringCiudad, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilScoringCiudad> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilScoringCiudadBO> listadoBO = new List<ProgramaGeneralPerfilScoringCiudadBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilScoringCiudadBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringCiudad, ProgramaGeneralPerfilScoringCiudadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilScoringCiudadBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilScoringCiudad entidad = base.FirstById(id);
                ProgramaGeneralPerfilScoringCiudadBO objetoBO = new ProgramaGeneralPerfilScoringCiudadBO();
                Mapper.Map<TProgramaGeneralPerfilScoringCiudad, ProgramaGeneralPerfilScoringCiudadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilScoringCiudadBO FirstBy(Expression<Func<TProgramaGeneralPerfilScoringCiudad, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilScoringCiudad entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilScoringCiudadBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringCiudad, ProgramaGeneralPerfilScoringCiudadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilScoringCiudadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilScoringCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilScoringCiudadBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilScoringCiudadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilScoringCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilScoringCiudadBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilScoringCiudad entidad, ProgramaGeneralPerfilScoringCiudadBO objetoBO)
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

        private TProgramaGeneralPerfilScoringCiudad MapeoEntidad(ProgramaGeneralPerfilScoringCiudadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilScoringCiudad entidad = new TProgramaGeneralPerfilScoringCiudad();
                entidad = Mapper.Map<ProgramaGeneralPerfilScoringCiudadBO, TProgramaGeneralPerfilScoringCiudad>(objetoBO,
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
        /// Obtiene la lista de scoring ciudades (activos ) por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ScoringCiudadProgramaDTO> ObtenerScoringCiudadPorPrograma(int idPGeneral)
        {
            try
            {

                List<ScoringCiudadProgramaDTO> resultadoDTO = new List<ScoringCiudadProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,IdCiudad,IdSelect,Valor,Fila,Columna,Validar FROM pla.V_TProgramaGeneralPerfilScoringCiudad WHERE " +
                    "Estado = 1 and IdCiudad is not null and IdPGeneral = " + idPGeneral;
                var respuestaDapper = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ScoringCiudadProgramaDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Escoring Ciudad asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ScoringCiudadProgramaDTO> nuevos)
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
