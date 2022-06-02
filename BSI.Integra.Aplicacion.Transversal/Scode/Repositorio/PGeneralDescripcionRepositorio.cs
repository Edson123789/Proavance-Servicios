using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PgeneralDescripcionRepositorio : BaseRepository<TPgeneralDescripcion, PgeneralDescripcionBO>
    {
        #region Metodos Base
        public PgeneralDescripcionRepositorio() : base()
        {
        }
        public PgeneralDescripcionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralDescripcionBO> GetBy(Expression<Func<TPgeneralDescripcion, bool>> filter)
        {
            IEnumerable<TPgeneralDescripcion> listado = base.GetBy(filter);
            List<PgeneralDescripcionBO> listadoBO = new List<PgeneralDescripcionBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralDescripcionBO objetoBO = Mapper.Map<TPgeneralDescripcion, PgeneralDescripcionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralDescripcionBO FirstById(int id)
        {
            try
            {
                TPgeneralDescripcion entidad = base.FirstById(id);
                PgeneralDescripcionBO objetoBO = new PgeneralDescripcionBO();
                Mapper.Map<TPgeneralDescripcion, PgeneralDescripcionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralDescripcionBO FirstBy(Expression<Func<TPgeneralDescripcion, bool>> filter)
        {
            try
            {
                TPgeneralDescripcion entidad = base.FirstBy(filter);
                PgeneralDescripcionBO objetoBO = Mapper.Map<TPgeneralDescripcion, PgeneralDescripcionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralDescripcionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralDescripcion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralDescripcionBO> listadoBO)
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

        public bool Update(PgeneralDescripcionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralDescripcion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralDescripcionBO> listadoBO)
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
        private void AsignacionId(TPgeneralDescripcion entidad, PgeneralDescripcionBO objetoBO)
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

        private TPgeneralDescripcion MapeoEntidad(PgeneralDescripcionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralDescripcion entidad = new TPgeneralDescripcion();
                entidad = Mapper.Map<PgeneralDescripcionBO, TPgeneralDescripcion>(objetoBO,
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
        ///  Obtiene la lista Descripción web General para un programa general registradas en el sistema(activos)
        /// </summary>
        /// <returns></returns>
        public List<PgeneralDescripcionProgramaDTO> ObtnerPgeneralDescripcionPorPrograma(int idPGeneral)
        {
            try
            {
                List<PgeneralDescripcionProgramaDTO> descripcion = new List<PgeneralDescripcionProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Texto FROM pla.V_TPGeneralDescripcion WHERE Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    descripcion = JsonConvert.DeserializeObject<List<PgeneralDescripcionProgramaDTO>>(respuestaDapper);
                }

                return descripcion;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Descripciones seo Por Programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<PgeneralDescripcionProgramaDTO> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_PGeneralDescripcion WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
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
