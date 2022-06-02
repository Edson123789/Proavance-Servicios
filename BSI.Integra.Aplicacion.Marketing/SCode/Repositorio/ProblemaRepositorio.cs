
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

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class ProblemaRepositorio : BaseRepository<TProblema, ProblemaBO>
    {
        #region Metodos Base
        public ProblemaRepositorio() : base()
        {
        }
        public ProblemaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProblemaBO> GetBy(Expression<Func<TProblema, bool>> filter)
        {
            IEnumerable<TProblema> listado = base.GetBy(filter);
            List<ProblemaBO> listadoBO = new List<ProblemaBO>();
            foreach (var itemEntidad in listado)
            {
                ProblemaBO objetoBO = Mapper.Map<TProblema, ProblemaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProblemaBO FirstById(int id)
        {
            try
            {
                TProblema entidad = base.FirstById(id);
                ProblemaBO objetoBO = new ProblemaBO();
                Mapper.Map<TProblema, ProblemaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProblemaBO FirstBy(Expression<Func<TProblema, bool>> filter)
        {
            try
            {
                TProblema entidad = base.FirstBy(filter);
                ProblemaBO objetoBO = Mapper.Map<TProblema, ProblemaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProblemaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProblema entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProblemaBO> listadoBO)
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

        public bool Update(ProblemaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProblema entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProblemaBO> listadoBO)
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
        private void AsignacionId(TProblema entidad, ProblemaBO objetoBO)
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

        private TProblema MapeoEntidad(ProblemaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProblema entidad = new TProblema();
                entidad = Mapper.Map<ProblemaBO, TProblema>(objetoBO,
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
        /// Obtiene la lista de problemas (activos) registradas en el sistema (Usado para llenarlo un una grilla para su propio CRUD).
        /// </summary>
        /// <returns></returns>
        public List<ProblemaDTO> ObtenerTodoProblemas()
        {
            try
            {
                List<ProblemaDTO> problemas = new List<ProblemaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion FROM mkt.T_Problema WHERE Estado = 1";
                var problemasDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(problemasDB) && !problemasDB.Contains("[]"))
                {
                    problemas = JsonConvert.DeserializeObject<List<ProblemaDTO>>(problemasDB);
                }
                return problemas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
