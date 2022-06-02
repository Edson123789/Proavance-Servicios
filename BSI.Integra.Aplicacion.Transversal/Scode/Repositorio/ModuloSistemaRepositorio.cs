using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.IO;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    ///ModuloSistemaRepositorio
	/// Autor: Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión Módulos del Sistema
	/// </summary>
    public class ModuloSistemaRepositorio : BaseRepository<TModuloSistema, ModuloSistemaBO>
    {
        #region Metodos Base
        public ModuloSistemaRepositorio() : base()
        {
        }
        public ModuloSistemaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModuloSistemaBO> GetBy(Expression<Func<TModuloSistema, bool>> filter)
        {
            IEnumerable<TModuloSistema> listado = base.GetBy(filter);
            List<ModuloSistemaBO> listadoBO = new List<ModuloSistemaBO>();
            foreach (var itemEntidad in listado)
            {
                ModuloSistemaBO objetoBO = Mapper.Map<TModuloSistema, ModuloSistemaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModuloSistemaBO FirstById(int id)
        {
            try
            {
                TModuloSistema entidad = base.FirstById(id);
                ModuloSistemaBO objetoBO = new ModuloSistemaBO();
                Mapper.Map<TModuloSistema, ModuloSistemaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModuloSistemaBO FirstBy(Expression<Func<TModuloSistema, bool>> filter)
        {
            try
            {
                TModuloSistema entidad = base.FirstBy(filter);
                ModuloSistemaBO objetoBO = Mapper.Map<TModuloSistema, ModuloSistemaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModuloSistemaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModuloSistema entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModuloSistemaBO> listadoBO)
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

        public bool Update(ModuloSistemaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModuloSistema entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModuloSistemaBO> listadoBO)
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
        private void AsignacionId(TModuloSistema entidad, ModuloSistemaBO objetoBO)
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

        private TModuloSistema MapeoEntidad(ModuloSistemaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModuloSistema entidad = new TModuloSistema();
                entidad = Mapper.Map<ModuloSistemaBO, TModuloSistema>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //if (objetoBO.CursoPespecifico != null)
                //{
                //    TCursoPespecifico entidadHijo = new TCursoPespecifico();
                //    entidadHijo = Mapper.Map<CursoModuloSistemaBO, TCursoPespecifico>(objetoBO.CursoPespecifico,
                //        opt => opt.ConfigureMap(MemberList.None));
                //    entidad.TCursoPespecifico.Add(entidadHijo);
                //}
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
        /// Obtiene el Id, Nombre del modulo
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(w => w.Estado, w => new FiltroDTO { Id = w.Id, Nombre = w.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        ///ModuloSistemaRepositorio
        /// Autor: Edgar S.
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene registros de Modulos, Grupo y Tipo
        /// </summary>
        /// <returns> Lista de registros de Modulos, Grupo y Tipo </returns>
        /// <returns> Lista de Objeto DTO: List<ModuloSistemaModuloGrupoDTO> </returns>
        public List<ModuloSistemaModuloGrupoDTO> ObtenerModulosGrupoModulo()
        {
            try
            {
                List<ModuloSistemaModuloGrupoDTO> listaModuloPuestoTrabajo = new List<ModuloSistemaModuloGrupoDTO>();
                var _query = "SELECT Id, Nombre, IdModuloSistemaGrupo, ModuloSistemaGrupo, Url, IdTipo, NombreTipo FROM gp.V_TModuloSistema_ObtenerGrupoPorModuloSistema WHERE Estado = 1";
                var res = _dapper.QueryDapper(_query, null);
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    listaModuloPuestoTrabajo = JsonConvert.DeserializeObject<List<ModuloSistemaModuloGrupoDTO>>(res);
                }
                return listaModuloPuestoTrabajo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
