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
    public class ProgramaGeneralPerfilAtrabajoCoeficienteRepositorio : BaseRepository<TProgramaGeneralPerfilAtrabajoCoeficiente, ProgramaGeneralPerfilAtrabajoCoeficienteBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilAtrabajoCoeficienteRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilAtrabajoCoeficienteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilAtrabajoCoeficienteBO> GetBy(Expression<Func<TProgramaGeneralPerfilAtrabajoCoeficiente, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilAtrabajoCoeficiente> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilAtrabajoCoeficienteBO> listadoBO = new List<ProgramaGeneralPerfilAtrabajoCoeficienteBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilAtrabajoCoeficienteBO objetoBO = Mapper.Map<TProgramaGeneralPerfilAtrabajoCoeficiente, ProgramaGeneralPerfilAtrabajoCoeficienteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilAtrabajoCoeficienteBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilAtrabajoCoeficiente entidad = base.FirstById(id);
                ProgramaGeneralPerfilAtrabajoCoeficienteBO objetoBO = new ProgramaGeneralPerfilAtrabajoCoeficienteBO();
                Mapper.Map<TProgramaGeneralPerfilAtrabajoCoeficiente, ProgramaGeneralPerfilAtrabajoCoeficienteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilAtrabajoCoeficienteBO FirstBy(Expression<Func<TProgramaGeneralPerfilAtrabajoCoeficiente, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilAtrabajoCoeficiente entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilAtrabajoCoeficienteBO objetoBO = Mapper.Map<TProgramaGeneralPerfilAtrabajoCoeficiente, ProgramaGeneralPerfilAtrabajoCoeficienteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilAtrabajoCoeficienteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilAtrabajoCoeficiente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilAtrabajoCoeficienteBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilAtrabajoCoeficienteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilAtrabajoCoeficiente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilAtrabajoCoeficienteBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilAtrabajoCoeficiente entidad, ProgramaGeneralPerfilAtrabajoCoeficienteBO objetoBO)
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

        private TProgramaGeneralPerfilAtrabajoCoeficiente MapeoEntidad(ProgramaGeneralPerfilAtrabajoCoeficienteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilAtrabajoCoeficiente entidad = new TProgramaGeneralPerfilAtrabajoCoeficiente();
                entidad = Mapper.Map<ProgramaGeneralPerfilAtrabajoCoeficienteBO, TProgramaGeneralPerfilAtrabajoCoeficiente>(objetoBO,
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
        /// Obtiene los coeficientes(activos) para las columnas del scoring Area de Trabajo  por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ProgramaGeneralPerfilCoeficienteDTO> ObtenerCoeficienteATrabajoPorPrograma(int idPGeneral)
        {
            try
            {
                List<ProgramaGeneralPerfilCoeficienteDTO> resultadoDTO = new List<ProgramaGeneralPerfilCoeficienteDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,Coeficiente,IdSelect,IdColumna FROM pla.V_TProgramaGeneralPerfilAtrabajoCoeficiente WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral  = idPGeneral });
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
        /// Elimina (Actualiza estado a false ) todos los coeficiente Area Trabajo asociados a un programa
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
