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
    public class ProgramaGeneralPerfilCiudadCoeficienteRepositorio : BaseRepository<TProgramaGeneralPerfilCiudadCoeficiente, ProgramaGeneralPerfilCiudadCoeficienteBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilCiudadCoeficienteRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilCiudadCoeficienteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilCiudadCoeficienteBO> GetBy(Expression<Func<TProgramaGeneralPerfilCiudadCoeficiente, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilCiudadCoeficiente> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilCiudadCoeficienteBO> listadoBO = new List<ProgramaGeneralPerfilCiudadCoeficienteBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilCiudadCoeficienteBO objetoBO = Mapper.Map<TProgramaGeneralPerfilCiudadCoeficiente, ProgramaGeneralPerfilCiudadCoeficienteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilCiudadCoeficienteBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilCiudadCoeficiente entidad = base.FirstById(id);
                ProgramaGeneralPerfilCiudadCoeficienteBO objetoBO = new ProgramaGeneralPerfilCiudadCoeficienteBO();
                Mapper.Map<TProgramaGeneralPerfilCiudadCoeficiente, ProgramaGeneralPerfilCiudadCoeficienteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilCiudadCoeficienteBO FirstBy(Expression<Func<TProgramaGeneralPerfilCiudadCoeficiente, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilCiudadCoeficiente entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilCiudadCoeficienteBO objetoBO = Mapper.Map<TProgramaGeneralPerfilCiudadCoeficiente, ProgramaGeneralPerfilCiudadCoeficienteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilCiudadCoeficienteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilCiudadCoeficiente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilCiudadCoeficienteBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilCiudadCoeficienteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilCiudadCoeficiente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilCiudadCoeficienteBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilCiudadCoeficiente entidad, ProgramaGeneralPerfilCiudadCoeficienteBO objetoBO)
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

        private TProgramaGeneralPerfilCiudadCoeficiente MapeoEntidad(ProgramaGeneralPerfilCiudadCoeficienteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilCiudadCoeficiente entidad = new TProgramaGeneralPerfilCiudadCoeficiente();
                entidad = Mapper.Map<ProgramaGeneralPerfilCiudadCoeficienteBO, TProgramaGeneralPerfilCiudadCoeficiente>(objetoBO,
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
        /// Obtiene los coeficientes(activos) para las columnas del scoring Ciudad  por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ProgramaGeneralPerfilCoeficienteDTO> ObtenerCoeficienteCiudadPorPrograma(int idPGeneral)
        {
            try
            {
                List<ProgramaGeneralPerfilCoeficienteDTO> resultadoDTO = new List<ProgramaGeneralPerfilCoeficienteDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,Coeficiente,IdSelect,IdColumna FROM pla.V_TProgramaGeneralPerfilCiudadCoeficiente WHERE " +
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
        /// Elimina (Actualiza estado a false ) todos los coeficiente Ciudad asociados a un programa
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
