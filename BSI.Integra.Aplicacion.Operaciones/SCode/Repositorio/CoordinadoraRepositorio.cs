using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class CoordinadoraRepositorio : BaseRepository<TCoordinadora, CoordinadoraBO>
    {
        #region Metodos Base
        public CoordinadoraRepositorio() : base()
        {
        }
        public CoordinadoraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CoordinadoraBO> GetBy(Expression<Func<TCoordinadora, bool>> filter)
        {
            IEnumerable<TCoordinadora> listado = base.GetBy(filter);
            List<CoordinadoraBO> listadoBO = new List<CoordinadoraBO>();
            foreach (var itemEntidad in listado)
            {
                CoordinadoraBO objetoBO = Mapper.Map<TCoordinadora, CoordinadoraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CoordinadoraBO FirstById(int id)
        {
            try
            {
                TCoordinadora entidad = base.FirstById(id);
                CoordinadoraBO objetoBO = new CoordinadoraBO();
                Mapper.Map<TCoordinadora, CoordinadoraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CoordinadoraBO FirstBy(Expression<Func<TCoordinadora, bool>> filter)
        {
            try
            {
                TCoordinadora entidad = base.FirstBy(filter);
                CoordinadoraBO objetoBO = Mapper.Map<TCoordinadora, CoordinadoraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CoordinadoraBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCoordinadora entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CoordinadoraBO> listadoBO)
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

        public bool Update(CoordinadoraBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCoordinadora entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CoordinadoraBO> listadoBO)
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
        private void AsignacionId(TCoordinadora entidad, CoordinadoraBO objetoBO)
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

        private TCoordinadora MapeoEntidad(CoordinadoraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCoordinadora entidad = new TCoordinadora();
                entidad = Mapper.Map<CoordinadoraBO, TCoordinadora>(objetoBO,
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
        /// /Obtiene un listado de coordinadoras de la modalidad presencial
        /// </summary>
        public List<CoordinadoraFiltroDTO> ObtenerListadoCoordinadoraPresencial() {

            try
            {
                //return this.GetBy(x => x.Modalidad.Contains("PRESENCIAL"), x => new CoordinadoraFiltroDTO { Usuario = x.Usuario, Nombre = x.NombreResumido}).OrderBy(x => x.Nombre).ToList();
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Retornar si existe un coordinador con ese usuario
        /// </summary>
        /// <param name="usuarioCoordinador"></param>
        /// <returns></returns>
        public bool ExistePorNombreUsuario(string usuarioCoordinador) {
            try
            {
                if (this.GetBy(x => x.Usuario == usuarioCoordinador.Trim()).ToList().Count() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CoordinadoraBO ObtenerCoordinador(string usuario) {
            try
            {
                return this.FirstBy(x => x.Usuario == usuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los coordinadores para ser mostrados en una grilla (para CRUD Propio)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoCoordinadoresDocentes()
        {
            try
            {
                List<FiltroDTO> paises = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.V_Coordinador_NombreCompleto WHERE Estado = 1 and Id in (17,4215,4108,4661)";
                var paisesDB = _dapper.QueryDapper(_query, null);
                paises = JsonConvert.DeserializeObject<List<FiltroDTO>>(paisesDB);
                return paises;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

    }
}
