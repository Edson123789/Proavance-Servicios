using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class AreaParametroSeoPwRepositorio : BaseRepository<TAreaParametroSeoPw, AreaParametroSeoPwBO>
    {
        #region Metodos Base
        public AreaParametroSeoPwRepositorio() : base()
        {
        }
        public AreaParametroSeoPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AreaParametroSeoPwBO> GetBy(Expression<Func<TAreaParametroSeoPw, bool>> filter)
        {
            IEnumerable<TAreaParametroSeoPw> listado = base.GetBy(filter);
            List<AreaParametroSeoPwBO> listadoBO = new List<AreaParametroSeoPwBO>();
            foreach (var itemEntidad in listado)
            {
                AreaParametroSeoPwBO objetoBO = Mapper.Map<TAreaParametroSeoPw, AreaParametroSeoPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AreaParametroSeoPwBO FirstById(int id)
        {
            try
            {
                TAreaParametroSeoPw entidad = base.FirstById(id);
                AreaParametroSeoPwBO objetoBO = new AreaParametroSeoPwBO();
                Mapper.Map<TAreaParametroSeoPw, AreaParametroSeoPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AreaParametroSeoPwBO FirstBy(Expression<Func<TAreaParametroSeoPw, bool>> filter)
        {
            try
            {
                TAreaParametroSeoPw entidad = base.FirstBy(filter);
                AreaParametroSeoPwBO objetoBO = Mapper.Map<TAreaParametroSeoPw, AreaParametroSeoPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AreaParametroSeoPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAreaParametroSeoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AreaParametroSeoPwBO> listadoBO)
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

        public bool Update(AreaParametroSeoPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAreaParametroSeoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AreaParametroSeoPwBO> listadoBO)
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
        private void AsignacionId(TAreaParametroSeoPw entidad, AreaParametroSeoPwBO objetoBO)
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

        private TAreaParametroSeoPw MapeoEntidad(AreaParametroSeoPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAreaParametroSeoPw entidad = new TAreaParametroSeoPw();
                entidad = Mapper.Map<AreaParametroSeoPwBO, TAreaParametroSeoPw>(objetoBO,
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
        /// Obtiene registros por el IdTag
        /// </summary>
        /// <param name="idTag"></param>
        /// <returns></returns>
        public List<ParametroContenidoDatosDTO> ObtenerTodoPorIdTag(int idTag)
        {
            try
            {
                List<ParametroContenidoDatosDTO> obtenerTodoIdTag = new List<ParametroContenidoDatosDTO>();
                var _query = "SELECT Id, NombreParametroSeo , NumeroCaracteresParametrosSeo, ContenidoParametroSeo  FROM pla.V_ObtenerAreaParametrosSeoPorIdArea WHERE IdAreaCapacitacion = @idTag AND  EstadoAreaParametroSeoPW = 1 AND EstadoParametroSeoPW = 1 ";
                var obtenerTodoIdTagDB = _dapper.QueryDapper(_query, new { idTag });
                if (!string.IsNullOrEmpty(obtenerTodoIdTagDB) && !obtenerTodoIdTagDB.Contains("[]"))
                {
                    obtenerTodoIdTag = JsonConvert.DeserializeObject<List<ParametroContenidoDatosDTO>>(obtenerTodoIdTagDB);
                }
                return obtenerTodoIdTag;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Area Parametro Pw asociados a una Area Capacitacion
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorArea(int idArea, string usuario, List<ParametroContenidoDatosDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdAreaCapacitacion == idArea && x.Estado == true).ToList();
                foreach (var item in nuevos)
                {
                    listaBorrar.RemoveAll(x => x.IdParametroSeopw== item.Id);
                }
                
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
