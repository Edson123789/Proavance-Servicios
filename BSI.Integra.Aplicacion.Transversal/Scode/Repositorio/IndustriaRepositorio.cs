using System;
using System.Collections.Generic;
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
    public class IndustriaRepositorio : BaseRepository<TIndustria, IndustriaBO>
    {
        #region Metodos Base
        public IndustriaRepositorio() : base()
        {
        }
        public IndustriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<IndustriaBO> GetBy(Expression<Func<TIndustria, bool>> filter)
        {
            IEnumerable<TIndustria> listado = base.GetBy(filter);
            List<IndustriaBO> listadoBO = new List<IndustriaBO>();
            foreach (var itemEntidad in listado)
            {
                IndustriaBO objetoBO = Mapper.Map<TIndustria, IndustriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IndustriaBO FirstById(int id)
        {
            try
            {
                TIndustria entidad = base.FirstById(id);
                IndustriaBO objetoBO = new IndustriaBO();
                Mapper.Map<TIndustria, IndustriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IndustriaBO FirstBy(Expression<Func<TIndustria, bool>> filter)
        {
            try
            {
                TIndustria entidad = base.FirstBy(filter);
                IndustriaBO objetoBO = Mapper.Map<TIndustria, IndustriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IndustriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<IndustriaBO> listadoBO)
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

        public bool Update(IndustriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<IndustriaBO> listadoBO)
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
        private void AsignacionId(TIndustria entidad, IndustriaBO objetoBO)
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

        private TIndustria MapeoEntidad(IndustriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TIndustria entidad = new TIndustria();
                entidad = Mapper.Map<IndustriaBO, TIndustria>(objetoBO,
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


        /// Autor: ----------
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de nombres de industrias (activas) registradas en el sistema, y sus IDs, (Usado para el llenado de combobox).    
        /// </summary>
        /// <param></param>
        /// <returns>Objeto(Id,Nombre)</returns>       
        public List<IndustriaFiltroDTO> ObtenerIndustriaFiltro()
        {
            try
            {
                List<IndustriaFiltroDTO> industrias = new List<IndustriaFiltroDTO>();
                var query = string.Empty;
                query = "SELECT Id, Nombre FROM pla.V_TIndustria_ObtenerIdNombre WHERE Estado = 1";
                var industriasDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(industriasDB) && !industriasDB.Contains("[]"))
                {
                    industrias = JsonConvert.DeserializeObject<List<IndustriaFiltroDTO>>(industriasDB);
                }
                return industrias;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene Id, Nombre y Descripcion de T_Industria (para visualizacion en grilla de su propio CRUD).
        /// </summary>
        /// <returns>Id, Nombre</returns>
        public List<IndustriaDTO> ObtenerAllIndustria()
        {
            try
            {
                List<IndustriaDTO> industrias = new List<IndustriaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion FROM pla.T_Industria WHERE Estado = 1";
                var industriasDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(industriasDB) && !industriasDB.Contains("[]"))
                {
                    industrias = JsonConvert.DeserializeObject<List<IndustriaDTO>>(industriasDB);
                }
                return industrias;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
