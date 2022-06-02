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
    public class ParametroSeoPwRepositorio : BaseRepository<TParametroSeoPw, ParametroSeoPwBO>
    {
        #region Metodos Base
        public ParametroSeoPwRepositorio() : base()
        {
        }
        public ParametroSeoPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ParametroSeoPwBO> GetBy(Expression<Func<TParametroSeoPw, bool>> filter)
        {
            IEnumerable<TParametroSeoPw> listado = base.GetBy(filter);
            List<ParametroSeoPwBO> listadoBO = new List<ParametroSeoPwBO>();
            foreach (var itemEntidad in listado)
            {
                ParametroSeoPwBO objetoBO = Mapper.Map<TParametroSeoPw, ParametroSeoPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ParametroSeoPwBO FirstById(int id)
        {
            try
            {
                TParametroSeoPw entidad = base.FirstById(id);
                ParametroSeoPwBO objetoBO = new ParametroSeoPwBO();
                Mapper.Map<TParametroSeoPw, ParametroSeoPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ParametroSeoPwBO FirstBy(Expression<Func<TParametroSeoPw, bool>> filter)
        {
            try
            {
                TParametroSeoPw entidad = base.FirstBy(filter);
                ParametroSeoPwBO objetoBO = Mapper.Map<TParametroSeoPw, ParametroSeoPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ParametroSeoPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TParametroSeoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ParametroSeoPwBO> listadoBO)
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

        public bool Update(ParametroSeoPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TParametroSeoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ParametroSeoPwBO> listadoBO)
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
        private void AsignacionId(TParametroSeoPw entidad, ParametroSeoPwBO objetoBO)
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

        private TParametroSeoPw MapeoEntidad(ParametroSeoPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TParametroSeoPw entidad = new TParametroSeoPw();
                entidad = Mapper.Map<ParametroSeoPwBO, TParametroSeoPw>(objetoBO,
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
        /// Obtiene Parametros SEO para los Combo Box
        /// </summary>
        /// <returns></returns>
        public List<ParametroSeoPwFiltroDTO> ObtnerParametroSeoFiltro()
        {
            try
            {
                List<ParametroSeoPwFiltroDTO> parametroSeo = new List<ParametroSeoPwFiltroDTO>();
                string _querySeo = string.Empty;
                _querySeo = "SELECT Id,Nombre FROM pla.V_TParametroSEOPW_Filtro WHERE Estado=1";
                var querySeo = _dapper.QueryDapper(_querySeo, null);
                if (!string.IsNullOrEmpty(querySeo) && !querySeo.Contains("[]"))
                {
                    parametroSeo = JsonConvert.DeserializeObject<List<ParametroSeoPwFiltroDTO>>(querySeo);
                }
                return parametroSeo;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<ParametroSeoPwDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new ParametroSeoPwDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    NumeroCaracteres = y.NumeroCaracteres
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<ParametroSeoPwFiltroDatosDTO> ObtenerTodoParametroGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new ParametroSeoPwFiltroDatosDTO
                {
                    Id = y.Id,
                    NombreParametroSeo = y.Nombre,
                    NumeroCaracteres = y.NumeroCaracteres,
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
