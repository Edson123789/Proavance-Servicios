using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: GrupoFiltroProgramaCriticoPGeneral
    /// Autor: Gian Miranda
    /// Fecha: 26/04/2021
    /// <summary>
    /// Repositorio de la tabla mkt.T_GrupoFiltroProgramaCriticoPGeneral
    /// </summary>
    public class GrupoFiltroProgramaCriticoPgeneralRepositorio : BaseRepository<TGrupoFiltroProgramaCriticoPgeneral, GrupoFiltroProgramaCriticoPgeneralBO>
    {
        #region Metodos Base
        public GrupoFiltroProgramaCriticoPgeneralRepositorio() : base()
        {
        }
        public GrupoFiltroProgramaCriticoPgeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GrupoFiltroProgramaCriticoPgeneralBO> GetBy(Expression<Func<TGrupoFiltroProgramaCriticoPgeneral, bool>> filter)
        {
            IEnumerable<TGrupoFiltroProgramaCriticoPgeneral> listado = base.GetBy(filter);
            List<GrupoFiltroProgramaCriticoPgeneralBO> listadoBO = new List<GrupoFiltroProgramaCriticoPgeneralBO>();
            foreach (var itemEntidad in listado)
            {
                GrupoFiltroProgramaCriticoPgeneralBO objetoBO = Mapper.Map<TGrupoFiltroProgramaCriticoPgeneral, GrupoFiltroProgramaCriticoPgeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GrupoFiltroProgramaCriticoPgeneralBO FirstById(int id)
        {
            try
            {
                TGrupoFiltroProgramaCriticoPgeneral entidad = base.FirstById(id);
                GrupoFiltroProgramaCriticoPgeneralBO objetoBO = new GrupoFiltroProgramaCriticoPgeneralBO();
                Mapper.Map<TGrupoFiltroProgramaCriticoPgeneral, GrupoFiltroProgramaCriticoPgeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GrupoFiltroProgramaCriticoPgeneralBO FirstBy(Expression<Func<TGrupoFiltroProgramaCriticoPgeneral, bool>> filter)
        {
            try
            {
                TGrupoFiltroProgramaCriticoPgeneral entidad = base.FirstBy(filter);
                GrupoFiltroProgramaCriticoPgeneralBO objetoBO = Mapper.Map<TGrupoFiltroProgramaCriticoPgeneral, GrupoFiltroProgramaCriticoPgeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GrupoFiltroProgramaCriticoPgeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGrupoFiltroProgramaCriticoPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GrupoFiltroProgramaCriticoPgeneralBO> listadoBO)
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

        public bool Update(GrupoFiltroProgramaCriticoPgeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGrupoFiltroProgramaCriticoPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GrupoFiltroProgramaCriticoPgeneralBO> listadoBO)
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
        private void AsignacionId(TGrupoFiltroProgramaCriticoPgeneral entidad, GrupoFiltroProgramaCriticoPgeneralBO objetoBO)
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

        private TGrupoFiltroProgramaCriticoPgeneral MapeoEntidad(GrupoFiltroProgramaCriticoPgeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGrupoFiltroProgramaCriticoPgeneral entidad = new TGrupoFiltroProgramaCriticoPgeneral();
                entidad = Mapper.Map<GrupoFiltroProgramaCriticoPgeneralBO, TGrupoFiltroProgramaCriticoPgeneral>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<GrupoFiltroProgramaCriticoPgeneralBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TGrupoFiltroProgramaCriticoPgeneral, bool>>> filters, Expression<Func<TGrupoFiltroProgramaCriticoPgeneral, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TGrupoFiltroProgramaCriticoPgeneral> listado = base.GetFiltered(filters, orderBy, ascending);
            List<GrupoFiltroProgramaCriticoPgeneralBO> listadoBO = new List<GrupoFiltroProgramaCriticoPgeneralBO>();

            foreach (var itemEntidad in listado)
            {
                GrupoFiltroProgramaCriticoPgeneralBO objetoBO = Mapper.Map<TGrupoFiltroProgramaCriticoPgeneral, GrupoFiltroProgramaCriticoPgeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
		/// Se obtienen los programas generales por GrupoFiltroProgramaCritico
		/// </summary>
		/// <param name="idGrupo">Id del grupo de programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
		/// <returns>Lista de objetos de clase PGeneralSubAreaDTO</returns>
		public List<PGeneralSubAreaDTO> ObtenerPorIdGrupo(int idGrupo)
        {
            try
            {
                List<PGeneralSubAreaDTO> lista = new List<PGeneralSubAreaDTO>();
                var query = "SELECT Id, Nombre, NombreAreaCapacitacion, NombreSubAreaCapacitacion FROM mkt.V_ObtenerPGeneralPorGrupoFiltro WHERE IdGrupoFiltro = @idGrupo";
                var respuestaDapper = _dapper.QueryDapper(query, new { idGrupo = idGrupo });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PGeneralSubAreaDTO>>(respuestaDapper);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
		/// Se eliminan logicamente la configuracion por grupo de filtro de programa critico
		/// </summary>
        /// <param name="idGrupo">Id del grupo de filtro de programas criticos (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
        /// <param name="usuario">Usuario que realiza el eliminado</param>
        /// <param name="nuevos">Lista de objetos de clase PGeneralSubAreaDTO que va a entrar como nuevos</param>
		/// <returns>Lista de objetos de clase PGeneralSubAreaDTO</returns>
        public void EliminadoLogicoPorGrupo(int idGrupo, string usuario, List<PGeneralSubAreaDTO> nuevos)
        {
            try
            {
                List<EliminacionGrupoFiltroPGeneralDTO> listaBorrar = new List<EliminacionGrupoFiltroPGeneralDTO>();
                listaBorrar = GetBy(x => x.IdGrupoFiltroProgramaCritico == idGrupo, y => new EliminacionGrupoFiltroPGeneralDTO()
                {
                    Id = y.Id,
                    IdPGeneral = y.IdPgeneral
                }).ToList();

                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.IdPGeneral));
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
