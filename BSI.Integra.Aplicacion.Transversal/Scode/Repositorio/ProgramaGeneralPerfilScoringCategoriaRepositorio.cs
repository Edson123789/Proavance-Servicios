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
    public class ProgramaGeneralPerfilScoringCategoriaRepositorio : BaseRepository<TProgramaGeneralPerfilScoringCategoria, ProgramaGeneralPerfilScoringCategoriaBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilScoringCategoriaRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilScoringCategoriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilScoringCategoriaBO> GetBy(Expression<Func<TProgramaGeneralPerfilScoringCategoria, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilScoringCategoria> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilScoringCategoriaBO> listadoBO = new List<ProgramaGeneralPerfilScoringCategoriaBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilScoringCategoriaBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringCategoria, ProgramaGeneralPerfilScoringCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilScoringCategoriaBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilScoringCategoria entidad = base.FirstById(id);
                ProgramaGeneralPerfilScoringCategoriaBO objetoBO = new ProgramaGeneralPerfilScoringCategoriaBO();
                Mapper.Map<TProgramaGeneralPerfilScoringCategoria, ProgramaGeneralPerfilScoringCategoriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilScoringCategoriaBO FirstBy(Expression<Func<TProgramaGeneralPerfilScoringCategoria, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilScoringCategoria entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilScoringCategoriaBO objetoBO = Mapper.Map<TProgramaGeneralPerfilScoringCategoria, ProgramaGeneralPerfilScoringCategoriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilScoringCategoriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilScoringCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilScoringCategoriaBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilScoringCategoriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilScoringCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilScoringCategoriaBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilScoringCategoria entidad, ProgramaGeneralPerfilScoringCategoriaBO objetoBO)
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

        private TProgramaGeneralPerfilScoringCategoria MapeoEntidad(ProgramaGeneralPerfilScoringCategoriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilScoringCategoria entidad = new TProgramaGeneralPerfilScoringCategoria();
                entidad = Mapper.Map<ProgramaGeneralPerfilScoringCategoriaBO, TProgramaGeneralPerfilScoringCategoria>(objetoBO,
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
        /// Obtiene la lista de scoring Categoria (activos ) por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ScoringCategoriaProgramaDTO> ObtenerScoringCategoriaPorPrograma(int idPGeneral)
        {
            try
            {

                List<ScoringCategoriaProgramaDTO> resultadoDTO = new List<ScoringCategoriaProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,IdCategoriaOrigen,IdSelect,Valor,Fila,Columna,Validar FROM pla.V_TProgramaGeneralPerfilScoringCategoria WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ScoringCategoriaProgramaDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Escoring Categoria asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ScoringCategoriaProgramaDTO> nuevos)
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
