using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class MoodleCategoriaRepositorio : BaseRepository<TMoodleCategoria, MoodleCategoriaBO>
    {
        #region Metodos Base
        public MoodleCategoriaRepositorio() : base()
        {
        }
        public MoodleCategoriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MoodleCategoriaBO> GetBy(Expression<Func<TMoodleCategoria, bool>> filter)
        {
            IEnumerable<TMoodleCategoria> listado = base.GetBy(filter);
            List<MoodleCategoriaBO> listadoBO = new List<MoodleCategoriaBO>();
            foreach (var itemEntidad in listado)
            {
                MoodleCategoriaBO objetoBO = Mapper.Map<TMoodleCategoria, MoodleCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MoodleCategoriaBO FirstById(int id)
        {
            try
            {
                TMoodleCategoria entidad = base.FirstById(id);
                MoodleCategoriaBO objetoBO = new MoodleCategoriaBO();
                Mapper.Map<TMoodleCategoria, MoodleCategoriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MoodleCategoriaBO FirstBy(Expression<Func<TMoodleCategoria, bool>> filter)
        {
            try
            {
                TMoodleCategoria entidad = base.FirstBy(filter);
                MoodleCategoriaBO objetoBO = Mapper.Map<TMoodleCategoria, MoodleCategoriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MoodleCategoriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMoodleCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MoodleCategoriaBO> listadoBO)
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

        public bool Update(MoodleCategoriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMoodleCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MoodleCategoriaBO> listadoBO)
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
        private void AsignacionId(TMoodleCategoria entidad, MoodleCategoriaBO objetoBO)
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

        private TMoodleCategoria MapeoEntidad(MoodleCategoriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMoodleCategoria entidad = new TMoodleCategoria();
                entidad = Mapper.Map<MoodleCategoriaBO, TMoodleCategoria>(objetoBO,
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
        /// Este método obtiene una lista de categorias Moodle registrados en la base de datos
        /// </summary>
        /// <returns></returns>
        public List<CategoriaMoodleDTO> ObtenerCategoriasMoodleRegistradas()
        {
            try
            {
                List<CategoriaMoodleDTO> listaMoodleCategoria = new List<CategoriaMoodleDTO>();
                var query = "SELECT Id, IdCategoriaMoodle, NombreCategoria, MoodleCategoriaTipo, AplicaProyecto FROM [ope].[V_TMoodleCategoria_CategoriaTipoNombre] WHERE Estado = 1";
                var res = _dapper.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaMoodleCategoria = JsonConvert.DeserializeObject<List<CategoriaMoodleDTO>>(res);
                }
                return listaMoodleCategoria;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Este método obtiene una lista de nombres de categorias Moodle y su Id
        /// </summary>
        /// <returns></returns>
        public List<TipoCategoriaMoodleDTO> ObtenerCategoriasPorNombre()
        {
            try
            {
                return this.GetBy(x => x.Estado == true).Select(x => new TipoCategoriaMoodleDTO
                {
                    Id = x.IdCategoriaMoodle,
                    Nombre = x.NombreCategoria
                }).ToList();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
