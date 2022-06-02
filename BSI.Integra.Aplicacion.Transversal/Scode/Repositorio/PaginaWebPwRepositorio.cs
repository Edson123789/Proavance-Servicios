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
    public class PaginaWebPwRepositorio : BaseRepository<TPaginaWebPw, PaginaWebPwBO>
    {
        #region Metodos Base
        public PaginaWebPwRepositorio() : base()
        {
        }
        public PaginaWebPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PaginaWebPwBO> GetBy(Expression<Func<TPaginaWebPw, bool>> filter)
        {
            IEnumerable<TPaginaWebPw> listado = base.GetBy(filter);
            List<PaginaWebPwBO> listadoBO = new List<PaginaWebPwBO>();
            foreach (var itemEntidad in listado)
            {
                PaginaWebPwBO objetoBO = Mapper.Map<TPaginaWebPw, PaginaWebPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PaginaWebPwBO FirstById(int id)
        {
            try
            {
                TPaginaWebPw entidad = base.FirstById(id);
                PaginaWebPwBO objetoBO = new PaginaWebPwBO();
                Mapper.Map<TPaginaWebPw, PaginaWebPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PaginaWebPwBO FirstBy(Expression<Func<TPaginaWebPw, bool>> filter)
        {
            try
            {
                TPaginaWebPw entidad = base.FirstBy(filter);
                PaginaWebPwBO objetoBO = Mapper.Map<TPaginaWebPw, PaginaWebPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PaginaWebPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPaginaWebPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PaginaWebPwBO> listadoBO)
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

        public bool Update(PaginaWebPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPaginaWebPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PaginaWebPwBO> listadoBO)
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
        private void AsignacionId(TPaginaWebPw entidad, PaginaWebPwBO objetoBO)
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

        private TPaginaWebPw MapeoEntidad(PaginaWebPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPaginaWebPw entidad = new TPaginaWebPw();
                entidad = Mapper.Map<PaginaWebPwBO, TPaginaWebPw>(objetoBO,
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
        /// Obtiene los Portales (activo) para determinar a que pagina pertenecera el programa a crear 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<PaginaWebPwFiltroDTO> ObtenerPaginasWeb()
        {
            try
            {
                List<PaginaWebPwFiltroDTO> resultadoDTO = new List<PaginaWebPwFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre FROM pla.V_TPaginaWebFiltro WHERE Estado = 1";
                var respuestaDapper = _dapper.QueryDapper(_query, new{ });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<PaginaWebPwFiltroDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoria
        /// </summary>
        /// <returns></returns>
        public List<PaginaWebPwDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PaginaWebPwDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    ServidorVinculado = y.ServidorVinculado
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
