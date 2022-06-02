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
    public class ProgramaGeneralPerfilCargoCoeficienteRepositorio : BaseRepository<TProgramaGeneralPerfilCargoCoeficiente, ProgramaGeneralPerfilCargoCoeficienteBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilCargoCoeficienteRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilCargoCoeficienteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilCargoCoeficienteBO> GetBy(Expression<Func<TProgramaGeneralPerfilCargoCoeficiente, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilCargoCoeficiente> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilCargoCoeficienteBO> listadoBO = new List<ProgramaGeneralPerfilCargoCoeficienteBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilCargoCoeficienteBO objetoBO = Mapper.Map<TProgramaGeneralPerfilCargoCoeficiente, ProgramaGeneralPerfilCargoCoeficienteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilCargoCoeficienteBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilCargoCoeficiente entidad = base.FirstById(id);
                ProgramaGeneralPerfilCargoCoeficienteBO objetoBO = new ProgramaGeneralPerfilCargoCoeficienteBO();
                Mapper.Map<TProgramaGeneralPerfilCargoCoeficiente, ProgramaGeneralPerfilCargoCoeficienteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilCargoCoeficienteBO FirstBy(Expression<Func<TProgramaGeneralPerfilCargoCoeficiente, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilCargoCoeficiente entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilCargoCoeficienteBO objetoBO = Mapper.Map<TProgramaGeneralPerfilCargoCoeficiente, ProgramaGeneralPerfilCargoCoeficienteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilCargoCoeficienteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilCargoCoeficiente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilCargoCoeficienteBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilCargoCoeficienteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilCargoCoeficiente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilCargoCoeficienteBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilCargoCoeficiente entidad, ProgramaGeneralPerfilCargoCoeficienteBO objetoBO)
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

        private TProgramaGeneralPerfilCargoCoeficiente MapeoEntidad(ProgramaGeneralPerfilCargoCoeficienteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilCargoCoeficiente entidad = new TProgramaGeneralPerfilCargoCoeficiente();
                entidad = Mapper.Map<ProgramaGeneralPerfilCargoCoeficienteBO, TProgramaGeneralPerfilCargoCoeficiente>(objetoBO,
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
        /// Obtiene los coeficientes(activos) para las columnas del scoring Cargo  por programa registrada en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ProgramaGeneralPerfilCoeficienteDTO> ObtenerCoeficienteCargoPorPrograma(int idPGeneral)
        {
            try
            {
                List<ProgramaGeneralPerfilCoeficienteDTO> resultadoDTO = new List<ProgramaGeneralPerfilCoeficienteDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Nombre,Coeficiente,IdSelect,IdColumna FROM pla.V_TProgramaGeneralPerfilCargoCoeficiente WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
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
        /// Elimina (Actualiza estado a false ) todos los coeficiente Cargo asociados a un programa
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
