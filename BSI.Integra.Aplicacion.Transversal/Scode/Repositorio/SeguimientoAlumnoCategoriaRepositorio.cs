using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class SeguimientoAlumnoCategoriaRepositorio : BaseRepository<TSeguimientoAlumnoCategoria, SeguimientoAlumnoCategoriaBO>
    {
        #region Metodos Base
        public SeguimientoAlumnoCategoriaRepositorio() : base()
        {
        }
        public SeguimientoAlumnoCategoriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SeguimientoAlumnoCategoriaBO> GetBy(Expression<Func<TSeguimientoAlumnoCategoria, bool>> filter)
        {
            IEnumerable<TSeguimientoAlumnoCategoria> listado = base.GetBy(filter);
            List<SeguimientoAlumnoCategoriaBO> listadoBO = new List<SeguimientoAlumnoCategoriaBO>();
            foreach (var itemEntidad in listado)
            {
                SeguimientoAlumnoCategoriaBO objetoBO = Mapper.Map<TSeguimientoAlumnoCategoria, SeguimientoAlumnoCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SeguimientoAlumnoCategoriaBO FirstById(int id)
        {
            try
            {
                TSeguimientoAlumnoCategoria entidad = base.FirstById(id);
                SeguimientoAlumnoCategoriaBO objetoBO = new SeguimientoAlumnoCategoriaBO();
                Mapper.Map<TSeguimientoAlumnoCategoria, SeguimientoAlumnoCategoriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SeguimientoAlumnoCategoriaBO FirstBy(Expression<Func<TSeguimientoAlumnoCategoria, bool>> filter)
        {
            try
            {
                TSeguimientoAlumnoCategoria entidad = base.FirstBy(filter);
                SeguimientoAlumnoCategoriaBO objetoBO = Mapper.Map<TSeguimientoAlumnoCategoria, SeguimientoAlumnoCategoriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SeguimientoAlumnoCategoriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSeguimientoAlumnoCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SeguimientoAlumnoCategoriaBO> listadoBO)
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

        public bool Update(SeguimientoAlumnoCategoriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSeguimientoAlumnoCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SeguimientoAlumnoCategoriaBO> listadoBO)
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
        private void AsignacionId(TSeguimientoAlumnoCategoria entidad, SeguimientoAlumnoCategoriaBO objetoBO)
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

        private TSeguimientoAlumnoCategoria MapeoEntidad(SeguimientoAlumnoCategoriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSeguimientoAlumnoCategoria entidad = new TSeguimientoAlumnoCategoria();
                entidad = Mapper.Map<SeguimientoAlumnoCategoriaBO, TSeguimientoAlumnoCategoria>(objetoBO,
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
        /// Obtiene los 
        /// </summary>
        /// <returns></returns>
        public List<FiltroSeguimientoAlumnoCategoriaDTO> ObtenerSeguimientoAlumnoCategoria()
        {
            try
            {
                var listaSeguimientoAlumnoCategoria = new List<FiltroSeguimientoAlumnoCategoriaDTO>();
                string _query = $@"
                        SELECT Id, 
                               Nombre, 
                               IdTipoSeguimientoAlumnoCategoria, 
                               AplicaModalidadOnline, 
                               AplicaModalidadAonline, 
                               AplicaModalidadPresencial
                        FROM mkt.V_ObtenerSeguimientoAlumnoCategoria
                        WHERE EstadoSeguimientoAlumnoCategoria = 1
                              AND EstadoTipoSeguimientoAlumnoCategoria = 1
                        ";
                string _queryCategoria = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_queryCategoria) && !_queryCategoria.Contains("[]"))
                {
                    listaSeguimientoAlumnoCategoria = JsonConvert.DeserializeObject<List<FiltroSeguimientoAlumnoCategoriaDTO>>(_queryCategoria);
                }
                return listaSeguimientoAlumnoCategoria;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
