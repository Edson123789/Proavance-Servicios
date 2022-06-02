using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class RevisionNivelPwRepositorio : BaseRepository<TRevisionNivelPw, RevisionNivelPwBO>
    {
        #region Metodos Base
        public RevisionNivelPwRepositorio() : base()
        {
        }
        public RevisionNivelPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RevisionNivelPwBO> GetBy(Expression<Func<TRevisionNivelPw, bool>> filter)
        {
            IEnumerable<TRevisionNivelPw> listado = base.GetBy(filter);
            List<RevisionNivelPwBO> listadoBO = new List<RevisionNivelPwBO>();
            foreach (var itemEntidad in listado)
            {
                RevisionNivelPwBO objetoBO = Mapper.Map<TRevisionNivelPw, RevisionNivelPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RevisionNivelPwBO FirstById(int id)
        {
            try
            {
                TRevisionNivelPw entidad = base.FirstById(id);
                RevisionNivelPwBO objetoBO = new RevisionNivelPwBO();
                Mapper.Map<TRevisionNivelPw, RevisionNivelPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RevisionNivelPwBO FirstBy(Expression<Func<TRevisionNivelPw, bool>> filter)
        {
            try
            {
                TRevisionNivelPw entidad = base.FirstBy(filter);
                RevisionNivelPwBO objetoBO = Mapper.Map<TRevisionNivelPw, RevisionNivelPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RevisionNivelPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRevisionNivelPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RevisionNivelPwBO> listadoBO)
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

        public bool Update(RevisionNivelPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRevisionNivelPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RevisionNivelPwBO> listadoBO)
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
        private void AsignacionId(TRevisionNivelPw entidad, RevisionNivelPwBO objetoBO)
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

        private TRevisionNivelPw MapeoEntidad(RevisionNivelPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRevisionNivelPw entidad = new TRevisionNivelPw();
                entidad = Mapper.Map<RevisionNivelPwBO, TRevisionNivelPw>(objetoBO,
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
        /// Obtiene todos los registros de RevisionNivel por el IdRevision
        /// </summary>
        /// <returns></returns>
        public List<RevisionNivelPwFiltroDTO> ObtenerRevisionNivelPorIdRevision(int idRevision)
        {
            try
            {
                List<RevisionNivelPwFiltroDTO> revisionesNivel = new List<RevisionNivelPwFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre,Prioridad,IdTipoRevisionPw,IdRevisionPw FROM pla.V_ObtenerRevisonNivelPorIdRevisionPw WHERE IdRevisionPw = @IdRevisionPw and Estado = 1 ";
                var revisionNivel = _dapper.QueryDapper(_query, new { IdRevisionPw = idRevision });
                revisionesNivel = JsonConvert.DeserializeObject<List<RevisionNivelPwFiltroDTO>>(revisionNivel);

                return revisionesNivel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene registros de RevisionNivel filtrado por el IdPlantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        public List<RevisionNivelPwFiltroIdPlantillaDTO> ObtenerRevisionNivelPorIdPlantilla(int idPlantilla)
        {
            try
            {
                List<RevisionNivelPwFiltroIdPlantillaDTO> obtenerRevisionNivelPorIdPlantilla = new List<RevisionNivelPwFiltroIdPlantillaDTO>();
                var _query = "SELECT Id, IdPlantillaPW , IdRevisionNivelPW, Identificador, Nombre, Prioridad, NombreCompleto, Email, Rol, IdTipoRevisionPW  FROM pla.V_ObtenerRevisionNivelPorIdPlantilla WHERE IdPlantillaPW =   @idPlantilla AND  EstadoPlantillaRevision = 1 AND EstadoRevisionNivel = 1 AND EstadoPersonal = 1 AND EstadoTipoRevision = 1 ";
                var obtenerRevisionNivelIdPlantillaDB = _dapper.QueryDapper(_query, new { idPlantilla });
                if (!string.IsNullOrEmpty(obtenerRevisionNivelIdPlantillaDB) && !obtenerRevisionNivelIdPlantillaDB.Contains("[]"))
                {
                    obtenerRevisionNivelPorIdPlantilla = JsonConvert.DeserializeObject<List<RevisionNivelPwFiltroIdPlantillaDTO>>(obtenerRevisionNivelIdPlantillaDB);
                }
                return obtenerRevisionNivelPorIdPlantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Elimina (Actualiza estado a false ) todos las registros de RevisionNivel
        /// </summary>
        /// <param name="idRevision"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorIdRevision(int idRevision, string usuario, List<RevisionNivelPwFiltroDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdRevisionPw == idRevision && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Equals(x.IdRevisionPw)));
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
