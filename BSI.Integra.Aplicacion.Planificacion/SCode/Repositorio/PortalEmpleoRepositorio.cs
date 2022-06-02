using System;
using System.Collections.Generic;
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
    public class PortalEmpleoRepositorio : BaseRepository<TPortalEmpleo, PortalEmpleoBO>
    {
        #region Metodos Base
        public PortalEmpleoRepositorio() : base()
        {
        }
        public PortalEmpleoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PortalEmpleoBO> GetBy(Expression<Func<TPortalEmpleo, bool>> filter)
        {
            IEnumerable<TPortalEmpleo> listado = base.GetBy(filter);
            List<PortalEmpleoBO> listadoBO = new List<PortalEmpleoBO>();
            foreach (var itemEntidad in listado)
            {
                PortalEmpleoBO objetoBO = Mapper.Map<TPortalEmpleo, PortalEmpleoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PortalEmpleoBO FirstById(int id)
        {
            try
            {
                TPortalEmpleo entidad = base.FirstById(id);
                PortalEmpleoBO objetoBO = new PortalEmpleoBO();
                Mapper.Map<TPortalEmpleo, PortalEmpleoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PortalEmpleoBO FirstBy(Expression<Func<TPortalEmpleo, bool>> filter)
        {
            try
            {
                TPortalEmpleo entidad = base.FirstBy(filter);
                PortalEmpleoBO objetoBO = Mapper.Map<TPortalEmpleo, PortalEmpleoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PortalEmpleoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPortalEmpleo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PortalEmpleoBO> listadoBO)
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

        public bool Update(PortalEmpleoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPortalEmpleo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PortalEmpleoBO> listadoBO)
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
        private void AsignacionId(TPortalEmpleo entidad, PortalEmpleoBO objetoBO)
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

        private TPortalEmpleo MapeoEntidad(PortalEmpleoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPortalEmpleo entidad = new TPortalEmpleo();
                entidad = Mapper.Map<PortalEmpleoBO, TPortalEmpleo>(objetoBO,
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
        /// Obtiene la lista de nombres,Url,IdPais de Portal Empleo registradas en el sistema
        /// </summary>
        /// <returns></returns>
        public List<PortalEmpleoDTO> ObtenerTodoPortalEmpleo()
        {
            try
            {
                List<PortalEmpleoDTO> areasFormacion = new List<PortalEmpleoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre,Url FROM [pla].[V_TPotalEmpleo_ObtenerTodo]  WHERE  Estado = 1";
                var areasFormacionDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(areasFormacionDB) && !areasFormacionDB.Contains("[]"))
                {
                    areasFormacion = JsonConvert.DeserializeObject<List<PortalEmpleoDTO>>(areasFormacionDB);
                }
                return areasFormacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Obtiene la lista de Id,Nombres,IdPais de Portal Empleo registradas en el sistema (Usado para combobox que filtran PortalEmpleo por Pais)
        /// </summary>
        /// <returns></returns>
        public List<PortalEmpleoDTO> ObtenerTodoPortalEmpleoFiltro()
        {
            try
            {
                List<PortalEmpleoDTO> listaPortalEmpleo = new List<PortalEmpleoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdPais, Nombre FROM [pla].[V_TPortalEmpleoConIdPais]  WHERE  Estado = 1";
                var PortalEmpleoEnDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(PortalEmpleoEnDB) && !PortalEmpleoEnDB.Contains("[]"))
                {
                    listaPortalEmpleo = JsonConvert.DeserializeObject<List<PortalEmpleoDTO>>(PortalEmpleoEnDB);
                }
                return listaPortalEmpleo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
