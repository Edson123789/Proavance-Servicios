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
    public class ProgramaGeneralPerfilModalidadCoeficienteRepositorio : BaseRepository<TProgramaGeneralPerfilModalidadCoeficiente, ProgramaGeneralPerfilModalidadCoeficienteBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilModalidadCoeficienteRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilModalidadCoeficienteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilModalidadCoeficienteBO> GetBy(Expression<Func<TProgramaGeneralPerfilModalidadCoeficiente, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilModalidadCoeficiente> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilModalidadCoeficienteBO> listadoBO = new List<ProgramaGeneralPerfilModalidadCoeficienteBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilModalidadCoeficienteBO objetoBO = Mapper.Map<TProgramaGeneralPerfilModalidadCoeficiente, ProgramaGeneralPerfilModalidadCoeficienteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilModalidadCoeficienteBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilModalidadCoeficiente entidad = base.FirstById(id);
                ProgramaGeneralPerfilModalidadCoeficienteBO objetoBO = new ProgramaGeneralPerfilModalidadCoeficienteBO();
                Mapper.Map<TProgramaGeneralPerfilModalidadCoeficiente, ProgramaGeneralPerfilModalidadCoeficienteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilModalidadCoeficienteBO FirstBy(Expression<Func<TProgramaGeneralPerfilModalidadCoeficiente, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilModalidadCoeficiente entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilModalidadCoeficienteBO objetoBO = Mapper.Map<TProgramaGeneralPerfilModalidadCoeficiente, ProgramaGeneralPerfilModalidadCoeficienteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilModalidadCoeficienteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilModalidadCoeficiente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilModalidadCoeficienteBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilModalidadCoeficienteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilModalidadCoeficiente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilModalidadCoeficienteBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilModalidadCoeficiente entidad, ProgramaGeneralPerfilModalidadCoeficienteBO objetoBO)
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

        private TProgramaGeneralPerfilModalidadCoeficiente MapeoEntidad(ProgramaGeneralPerfilModalidadCoeficienteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilModalidadCoeficiente entidad = new TProgramaGeneralPerfilModalidadCoeficiente();
                entidad = Mapper.Map<ProgramaGeneralPerfilModalidadCoeficienteBO, TProgramaGeneralPerfilModalidadCoeficiente>(objetoBO,
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
        /// Obtiene los coeficientes(activos) para las columnas del scoring Modalidad  por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ProgramaGeneralPerfilCoeficienteDTO> ObtenerCoeficienteModalidadPorPrograma(int idPGeneral)
        {
            try
            {
                List<ProgramaGeneralPerfilCoeficienteDTO> resultadoDTO = new List<ProgramaGeneralPerfilCoeficienteDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,Coeficiente,IdSelect,IdColumna FROM pla.V_TProgramaGeneralPerfilModalidadCoeficiente WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral  = idPGeneral});
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
        /// Elimina (Actualiza estado a false ) todos los coeficiente Modalidad asociados a un programa
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
