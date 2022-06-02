using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class PgeneralModalidadRepositorio : BaseRepository<TPgeneralModalidad, PgeneralModalidadBO>
    {
        #region Metodos Base
        public PgeneralModalidadRepositorio() : base()
        {
        }
        public PgeneralModalidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralModalidadBO> GetBy(Expression<Func<TPgeneralModalidad, bool>> filter)
        {
            IEnumerable<TPgeneralModalidad> listado = base.GetBy(filter);
            List<PgeneralModalidadBO> listadoBO = new List<PgeneralModalidadBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralModalidadBO objetoBO = Mapper.Map<TPgeneralModalidad, PgeneralModalidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralModalidadBO FirstById(int id)
        {
            try
            {
                TPgeneralModalidad entidad = base.FirstById(id);
                PgeneralModalidadBO objetoBO = new PgeneralModalidadBO();
                Mapper.Map<TPgeneralModalidad, PgeneralModalidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralModalidadBO FirstBy(Expression<Func<TPgeneralModalidad, bool>> filter)
        {
            try
            {
                TPgeneralModalidad entidad = base.FirstBy(filter);
                PgeneralModalidadBO objetoBO = Mapper.Map<TPgeneralModalidad, PgeneralModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralModalidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralModalidadBO> listadoBO)
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

        public bool Update(PgeneralModalidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralModalidadBO> listadoBO)
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
        private void AsignacionId(TPgeneralModalidad entidad, PgeneralModalidadBO objetoBO)
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

        private TPgeneralModalidad MapeoEntidad(PgeneralModalidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralModalidad entidad = new TPgeneralModalidad();
                entidad = Mapper.Map<PgeneralModalidadBO, TPgeneralModalidad>(objetoBO,
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
        ///
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_PGeneralModalidad WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.Id));
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

        //Listar modalidades del curso
        public List<PgeneralModalidadDTO> ListarModalidadesCurso(int idPgeneral)
        {
            try
            {
                List<PgeneralModalidadDTO> modalidades = new List<PgeneralModalidadDTO>();
                var _query = string.Empty;
                _query = "select IdModalidadCurso from pla.T_PGeneralModalidad where IdPgeneral = @idPgeneral and Estado = 1";
                var pgeneralDB = _dapper.QueryDapper(_query, new { IdPGeneral = idPgeneral });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    modalidades = JsonConvert.DeserializeObject<List<PgeneralModalidadDTO>>(pgeneralDB);
                }
                return modalidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        //Listar las modalidades del curso por ID
        public List<int> ObtenerModalidaCurso(int idPGeneral)
        {
            try
            {
                var expositores = GetBy(x => x.Estado == true && x.IdPgeneral == idPGeneral).Select(x => x.IdModalidadCurso).ToList();

                return expositores;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
