
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class CategoriaIndicadorRepositorio : BaseRepository<TCategoriaIndicador, CategoriaIndicadorBO>
    {
        #region Metodos Base
        public CategoriaIndicadorRepositorio() : base()
        {
        }
        public CategoriaIndicadorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CategoriaIndicadorBO> GetBy(Expression<Func<TCategoriaIndicador, bool>> filter)
        {
            IEnumerable<TCategoriaIndicador> listado = base.GetBy(filter);
            List<CategoriaIndicadorBO> listadoBO = new List<CategoriaIndicadorBO>();
            foreach (var itemEntidad in listado)
            {
                CategoriaIndicadorBO objetoBO = Mapper.Map<TCategoriaIndicador, CategoriaIndicadorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CategoriaIndicadorBO FirstById(int id)
        {
            try
            {
                TCategoriaIndicador entidad = base.FirstById(id);
                CategoriaIndicadorBO objetoBO = new CategoriaIndicadorBO();
                Mapper.Map<TCategoriaIndicador, CategoriaIndicadorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CategoriaIndicadorBO FirstBy(Expression<Func<TCategoriaIndicador, bool>> filter)
        {
            try
            {
                TCategoriaIndicador entidad = base.FirstBy(filter);
                CategoriaIndicadorBO objetoBO = Mapper.Map<TCategoriaIndicador, CategoriaIndicadorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CategoriaIndicadorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCategoriaIndicador entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CategoriaIndicadorBO> listadoBO)
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

        public bool Update(CategoriaIndicadorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCategoriaIndicador entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CategoriaIndicadorBO> listadoBO)
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
        private void AsignacionId(TCategoriaIndicador entidad, CategoriaIndicadorBO objetoBO)
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

        private TCategoriaIndicador MapeoEntidad(CategoriaIndicadorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCategoriaIndicador entidad = new TCategoriaIndicador();
                entidad = Mapper.Map<CategoriaIndicadorBO, TCategoriaIndicador>(objetoBO,
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
        /// Obtiene una lista de CategoriaIndicadores [Id, Nombre] (activos) para ser usados en comboboxes.
        /// </summary>
        /// <returns></returns>
        public List<CategoriaIndicadorDTO> ObtenerTodoCategoriaIndicador()
        {
            try
            {
                List<CategoriaIndicadorDTO> CategoriaIndicadores = new List<CategoriaIndicadorDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM mkt.T_CategoriaIndicador WHERE Estado = 1";
                var CategoriaIndicadoresDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(CategoriaIndicadoresDB) && !CategoriaIndicadoresDB.Contains("[]"))
                {
                    CategoriaIndicadores = JsonConvert.DeserializeObject<List<CategoriaIndicadorDTO>>(CategoriaIndicadoresDB);
                }
                return CategoriaIndicadores;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
