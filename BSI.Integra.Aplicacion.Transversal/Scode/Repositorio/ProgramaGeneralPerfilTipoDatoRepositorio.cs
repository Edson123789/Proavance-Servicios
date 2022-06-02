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
    public class ProgramaGeneralPerfilTipoDatoRepositorio : BaseRepository<TProgramaGeneralPerfilTipoDato, ProgramaGeneralPerfilTipoDatoBO>
    {
        #region Metodos Base
        public ProgramaGeneralPerfilTipoDatoRepositorio() : base()
        {
        }
        public ProgramaGeneralPerfilTipoDatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaGeneralPerfilTipoDatoBO> GetBy(Expression<Func<TProgramaGeneralPerfilTipoDato, bool>> filter)
        {
            IEnumerable<TProgramaGeneralPerfilTipoDato> listado = base.GetBy(filter);
            List<ProgramaGeneralPerfilTipoDatoBO> listadoBO = new List<ProgramaGeneralPerfilTipoDatoBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaGeneralPerfilTipoDatoBO objetoBO = Mapper.Map<TProgramaGeneralPerfilTipoDato, ProgramaGeneralPerfilTipoDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaGeneralPerfilTipoDatoBO FirstById(int id)
        {
            try
            {
                TProgramaGeneralPerfilTipoDato entidad = base.FirstById(id);
                ProgramaGeneralPerfilTipoDatoBO objetoBO = new ProgramaGeneralPerfilTipoDatoBO();
                Mapper.Map<TProgramaGeneralPerfilTipoDato, ProgramaGeneralPerfilTipoDatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaGeneralPerfilTipoDatoBO FirstBy(Expression<Func<TProgramaGeneralPerfilTipoDato, bool>> filter)
        {
            try
            {
                TProgramaGeneralPerfilTipoDato entidad = base.FirstBy(filter);
                ProgramaGeneralPerfilTipoDatoBO objetoBO = Mapper.Map<TProgramaGeneralPerfilTipoDato, ProgramaGeneralPerfilTipoDatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaGeneralPerfilTipoDatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaGeneralPerfilTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaGeneralPerfilTipoDatoBO> listadoBO)
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

        public bool Update(ProgramaGeneralPerfilTipoDatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaGeneralPerfilTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaGeneralPerfilTipoDatoBO> listadoBO)
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
        private void AsignacionId(TProgramaGeneralPerfilTipoDato entidad, ProgramaGeneralPerfilTipoDatoBO objetoBO)
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

        private TProgramaGeneralPerfilTipoDato MapeoEntidad(ProgramaGeneralPerfilTipoDatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPerfilTipoDato entidad = new TProgramaGeneralPerfilTipoDato();
                entidad = Mapper.Map<ProgramaGeneralPerfilTipoDatoBO, TProgramaGeneralPerfilTipoDato>(objetoBO,
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
        /// Obtiene los Tipo de Datos(activos) para el perfil contacto Programa por programa 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<TipoDatoPerFilContactoProgramaDTO> ObtenerTipoDatoPorPrograma(int idPGeneral)
        {
            
            try
            {
                List<TipoDatoPerFilContactoProgramaDTO> resultadoDTO = new List<TipoDatoPerFilContactoProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdTipoDato FROM pla.V_TProgramaGeneralPerfilTipoDato WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<TipoDatoPerFilContactoProgramaDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Tipo Dato asociados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<TipoDatoPerFilContactoProgramaDTO> nuevos)
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
