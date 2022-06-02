using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    /// Repositorio: GrupoFiltroProgramaCritico
    /// Autor: Gian Miranda
    /// Fecha: 23/04/2021
    /// <summary>
    /// Gestion de los grupos de programas criticos (pla.T_GrupoFiltroProgramaCritico)
    /// </summary>
    public class GrupoFiltroProgramaCriticoRepositorio : BaseRepository<TGrupoFiltroProgramaCritico, GrupoFiltroProgramaCriticoBO>
    {
        #region Metodos Base
        public GrupoFiltroProgramaCriticoRepositorio() : base()
        {
        }
        public GrupoFiltroProgramaCriticoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GrupoFiltroProgramaCriticoBO> GetBy(Expression<Func<TGrupoFiltroProgramaCritico, bool>> filter)
        {
            IEnumerable<TGrupoFiltroProgramaCritico> listado = base.GetBy(filter);
            List<GrupoFiltroProgramaCriticoBO> listadoBO = new List<GrupoFiltroProgramaCriticoBO>();
            foreach (var itemEntidad in listado)
            {
                GrupoFiltroProgramaCriticoBO objetoBO = Mapper.Map<TGrupoFiltroProgramaCritico, GrupoFiltroProgramaCriticoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GrupoFiltroProgramaCriticoBO FirstById(int id)
        {
            try
            {
                TGrupoFiltroProgramaCritico entidad = base.FirstById(id);
                GrupoFiltroProgramaCriticoBO objetoBO = new GrupoFiltroProgramaCriticoBO();
                Mapper.Map<TGrupoFiltroProgramaCritico, GrupoFiltroProgramaCriticoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GrupoFiltroProgramaCriticoBO FirstBy(Expression<Func<TGrupoFiltroProgramaCritico, bool>> filter)
        {
            try
            {
                TGrupoFiltroProgramaCritico entidad = base.FirstBy(filter);
                GrupoFiltroProgramaCriticoBO objetoBO = Mapper.Map<TGrupoFiltroProgramaCritico, GrupoFiltroProgramaCriticoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GrupoFiltroProgramaCriticoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGrupoFiltroProgramaCritico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GrupoFiltroProgramaCriticoBO> listadoBO)
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

        public bool Update(GrupoFiltroProgramaCriticoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGrupoFiltroProgramaCritico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GrupoFiltroProgramaCriticoBO> listadoBO)
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
        private void AsignacionId(TGrupoFiltroProgramaCritico entidad, GrupoFiltroProgramaCriticoBO objetoBO)
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

        private TGrupoFiltroProgramaCritico MapeoEntidad(GrupoFiltroProgramaCriticoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGrupoFiltroProgramaCritico entidad = new TGrupoFiltroProgramaCritico();
                entidad = Mapper.Map<GrupoFiltroProgramaCriticoBO, TGrupoFiltroProgramaCritico>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.GrupoFiltroProgramaCriticoPorAsesor != null && objetoBO.GrupoFiltroProgramaCriticoPorAsesor.Count > 0)
                {
                    foreach (var hijo in objetoBO.GrupoFiltroProgramaCriticoPorAsesor)
                    {
                        TGrupoFiltroProgramaCriticoPorAsesor entidadHijo = new TGrupoFiltroProgramaCriticoPorAsesor();
                        entidadHijo = Mapper.Map<GrupoFiltroProgramaCriticoPorAsesorBO, TGrupoFiltroProgramaCriticoPorAsesor>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TGrupoFiltroProgramaCriticoPorAsesor.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría
        /// </summary>
        /// <returns>Lista de objetos de clase GrupoFiltroProgramaCriticoDTO</returns>
        public List<GrupoFiltroProgramaCriticoDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new GrupoFiltroProgramaCriticoDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
                    
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id y Nombre de todos los registros.
        /// </summary>
        /// <returns>Lista de objetos de clase FiltroIdNombreDTO</returns>
        public List<FiltroIdNombreDTO> ObtenerFiltro()
        {
            try
            {
                var lista = new List<FiltroIdNombreDTO>();

                lista = GetBy(x => x.Nombre != "W. coordinadores" && x.Nombre != "V. CESADOS", y => new FiltroIdNombreDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre
                }).OrderBy(x => x.Nombre).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
     
}
