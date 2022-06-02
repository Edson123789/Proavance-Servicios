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
    public class ProgramaGeneralPerfilCategoriaCoeficienteRepositorio : BaseRepository<TProgramaGeneralPerfilCategoriaCoeficiente, ProgramaGeneralPerfilCategoriaCoeficienteBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilCategoriaCoeficienteRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilCategoriaCoeficienteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilCategoriaCoeficienteBO> GetBy(Expression<Func<TProgramaGeneralPerfilCategoriaCoeficiente, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilCategoriaCoeficiente> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilCategoriaCoeficienteBO> listadoBO = new List<ProgramaGeneralPerfilCategoriaCoeficienteBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilCategoriaCoeficienteBO objetoBO = Mapper.Map<TProgramaGeneralPerfilCategoriaCoeficiente, ProgramaGeneralPerfilCategoriaCoeficienteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilCategoriaCoeficienteBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilCategoriaCoeficiente entidad = base.FirstById(id);
                ProgramaGeneralPerfilCategoriaCoeficienteBO objetoBO = new ProgramaGeneralPerfilCategoriaCoeficienteBO();
                Mapper.Map<TProgramaGeneralPerfilCategoriaCoeficiente, ProgramaGeneralPerfilCategoriaCoeficienteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilCategoriaCoeficienteBO FirstBy(Expression<Func<TProgramaGeneralPerfilCategoriaCoeficiente, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilCategoriaCoeficiente entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilCategoriaCoeficienteBO objetoBO = Mapper.Map<TProgramaGeneralPerfilCategoriaCoeficiente, ProgramaGeneralPerfilCategoriaCoeficienteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilCategoriaCoeficienteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilCategoriaCoeficiente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilCategoriaCoeficienteBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilCategoriaCoeficienteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilCategoriaCoeficiente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilCategoriaCoeficienteBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilCategoriaCoeficiente entidad, ProgramaGeneralPerfilCategoriaCoeficienteBO objetoBO)
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

        private TProgramaGeneralPerfilCategoriaCoeficiente MapeoEntidad(ProgramaGeneralPerfilCategoriaCoeficienteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilCategoriaCoeficiente entidad = new TProgramaGeneralPerfilCategoriaCoeficiente();
                entidad = Mapper.Map<ProgramaGeneralPerfilCategoriaCoeficienteBO, TProgramaGeneralPerfilCategoriaCoeficiente>(objetoBO,
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
        /// Obtiene los coeficientes(activos) para las columnas del scoring Categoria Origen  por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ProgramaGeneralPerfilCoeficienteDTO> ObtenerCoeficienteCategoriaPorPrograma(int idPGeneral)
        {
            try
            {
                List<ProgramaGeneralPerfilCoeficienteDTO> resultadoDTO = new List<ProgramaGeneralPerfilCoeficienteDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,Coeficiente,IdSelect,IdColumna FROM pla.V_TProgramaGeneralPerfilCategoriaCoeficiente WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ProgramaGeneralPerfilCoeficienteDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los coeficiente Categoria asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ProgramaGeneralPerfilCoeficienteDTO> nuevos)
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
