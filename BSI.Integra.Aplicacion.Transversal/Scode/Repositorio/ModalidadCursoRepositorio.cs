using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ModalidadCursoRepositorio : BaseRepository<TModalidadCurso, ModalidadCursoBO>
    {
        #region Metodos Base
        public ModalidadCursoRepositorio() : base()
        {
        }
        public ModalidadCursoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModalidadCursoBO> GetBy(Expression<Func<TModalidadCurso, bool>> filter)
        {
            IEnumerable<TModalidadCurso> listado = base.GetBy(filter);
            List<ModalidadCursoBO> listadoBO = new List<ModalidadCursoBO>();
            foreach (var itemEntidad in listado)
            {
                ModalidadCursoBO objetoBO = Mapper.Map<TModalidadCurso, ModalidadCursoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModalidadCursoBO FirstById(int id)
        {
            try
            {
                TModalidadCurso entidad = base.FirstById(id);
                ModalidadCursoBO objetoBO = new ModalidadCursoBO();
                Mapper.Map<TModalidadCurso, ModalidadCursoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModalidadCursoBO FirstBy(Expression<Func<TModalidadCurso, bool>> filter)
        {
            try
            {
                TModalidadCurso entidad = base.FirstBy(filter);
                ModalidadCursoBO objetoBO = Mapper.Map<TModalidadCurso, ModalidadCursoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModalidadCursoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModalidadCurso entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModalidadCursoBO> listadoBO)
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

        public bool Update(ModalidadCursoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModalidadCurso entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModalidadCursoBO> listadoBO)
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
        private void AsignacionId(TModalidadCurso entidad, ModalidadCursoBO objetoBO)
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

        private TModalidadCurso MapeoEntidad(ModalidadCursoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModalidadCurso entidad = new TModalidadCurso();
                entidad = Mapper.Map<ModalidadCursoBO, TModalidadCurso>(objetoBO,
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
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 04/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las modalidades existente para un curso (onlin asincronica, online sincronica, presencial)
        /// </summary>
        /// <returns>Retorna una lista de ModalidadCursoFiltroDTO</returns>
        public List<ModalidadCursoFiltroDTO> ObtenerModalidadCursoFiltro()
        {
            try
            {
                List<ModalidadCursoFiltroDTO> modalidad = new List<ModalidadCursoFiltroDTO>();
                string _queryModalidad = string.Empty;
                _queryModalidad = "SELECT Id,Nombre FROM pla.V_TModalidadCurso_Filtro WHERE Estado=1";
                var queryModalidad = _dapper.QueryDapper(_queryModalidad,null);
                if (!string.IsNullOrEmpty(queryModalidad) && !queryModalidad.Contains("[]"))
                {
                    modalidad = JsonConvert.DeserializeObject<List<ModalidadCursoFiltroDTO>>(queryModalidad);
                }
                return modalidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los registros 
        /// </summary>
        /// <returns></returns>
        public List<ModalidadCursoDatosFiltroDTO> ObtenerTodoGrid()
        {
            try
            {
                var listaAmbiente = GetBy(x => true, y => new ModalidadCursoDatosFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Codigo = y.Codigo
                }).OrderByDescending(x => x.Id).ToList();

                return listaAmbiente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un listado de estados de pago matricula para ser usados en filtros
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
