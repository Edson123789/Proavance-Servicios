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
    public class ProgramaAreaRelacionadaRepositorio : BaseRepository<TProgramaAreaRelacionada, ProgramaAreaRelacionadaBO>
    {
        #region Metodos Base
        public ProgramaAreaRelacionadaRepositorio() : base()
        {
        }
        public ProgramaAreaRelacionadaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProgramaAreaRelacionadaBO> GetBy(Expression<Func<TProgramaAreaRelacionada, bool>> filter)
        {
            IEnumerable<TProgramaAreaRelacionada> listado = base.GetBy(filter);
            List<ProgramaAreaRelacionadaBO> listadoBO = new List<ProgramaAreaRelacionadaBO>();
            foreach (var itemEntidad in listado)
            {
                ProgramaAreaRelacionadaBO objetoBO = Mapper.Map<TProgramaAreaRelacionada, ProgramaAreaRelacionadaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProgramaAreaRelacionadaBO FirstById(int id)
        {
            try
            {
                TProgramaAreaRelacionada entidad = base.FirstById(id);
                ProgramaAreaRelacionadaBO objetoBO = new ProgramaAreaRelacionadaBO();
                Mapper.Map<TProgramaAreaRelacionada, ProgramaAreaRelacionadaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProgramaAreaRelacionadaBO FirstBy(Expression<Func<TProgramaAreaRelacionada, bool>> filter)
        {
            try
            {
                TProgramaAreaRelacionada entidad = base.FirstBy(filter);
                ProgramaAreaRelacionadaBO objetoBO = Mapper.Map<TProgramaAreaRelacionada, ProgramaAreaRelacionadaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProgramaAreaRelacionadaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProgramaAreaRelacionada entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProgramaAreaRelacionadaBO> listadoBO)
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

        public bool Update(ProgramaAreaRelacionadaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProgramaAreaRelacionada entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProgramaAreaRelacionadaBO> listadoBO)
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
        private void AsignacionId(TProgramaAreaRelacionada entidad, ProgramaAreaRelacionadaBO objetoBO)
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

        private TProgramaAreaRelacionada MapeoEntidad(ProgramaAreaRelacionadaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProgramaAreaRelacionada entidad = new TProgramaAreaRelacionada();
                entidad = Mapper.Map<ProgramaAreaRelacionadaBO, TProgramaAreaRelacionada>(objetoBO,
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
        /// Obtiene todas las areas relacionadas registradas para un programa
        /// </summary>
        public List<int> ObtenerAreasRelacionadasPorPrograma(int idPGeneral)
        {
            try
            {
                var areas = GetBy(x => x.Estado == true && x.IdPgeneral == idPGeneral).Select(x => x.IdAreaCapacitacion).ToList();

                return areas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Areas Relacionadas  Por Programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_ProgramaAreaRelacionada WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
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
    }
}
