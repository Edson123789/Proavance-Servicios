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
    /// Repositorio: GrupoFiltroProgramaCriticoPorAsesor
    /// Autor: Gian Miranda
    /// Fecha: 23/04/2021
    /// <summary>
    /// Gestion de los grupos de programas criticos por asesor (mkt.T_GrupoFiltroProgramaCriticoPorAsesor)
    /// </summary>
    public class GrupoFiltroProgramaCriticoPorAsesorRepositorio : BaseRepository<TGrupoFiltroProgramaCriticoPorAsesor, GrupoFiltroProgramaCriticoPorAsesorBO>
    {
        #region Metodos Base
        public GrupoFiltroProgramaCriticoPorAsesorRepositorio() : base()
        {
        }
        public GrupoFiltroProgramaCriticoPorAsesorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GrupoFiltroProgramaCriticoPorAsesorBO> GetBy(Expression<Func<TGrupoFiltroProgramaCriticoPorAsesor, bool>> filter)
        {
            IEnumerable<TGrupoFiltroProgramaCriticoPorAsesor> listado = base.GetBy(filter);
            List<GrupoFiltroProgramaCriticoPorAsesorBO> listadoBO = new List<GrupoFiltroProgramaCriticoPorAsesorBO>();
            foreach (var itemEntidad in listado)
            {
                GrupoFiltroProgramaCriticoPorAsesorBO objetoBO = Mapper.Map<TGrupoFiltroProgramaCriticoPorAsesor, GrupoFiltroProgramaCriticoPorAsesorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GrupoFiltroProgramaCriticoPorAsesorBO FirstById(int id)
        {
            try
            {
                TGrupoFiltroProgramaCriticoPorAsesor entidad = base.FirstById(id);
                GrupoFiltroProgramaCriticoPorAsesorBO objetoBO = new GrupoFiltroProgramaCriticoPorAsesorBO();
                Mapper.Map<TGrupoFiltroProgramaCriticoPorAsesor, GrupoFiltroProgramaCriticoPorAsesorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GrupoFiltroProgramaCriticoPorAsesorBO FirstBy(Expression<Func<TGrupoFiltroProgramaCriticoPorAsesor, bool>> filter)
        {
            try
            {
                TGrupoFiltroProgramaCriticoPorAsesor entidad = base.FirstBy(filter);
                GrupoFiltroProgramaCriticoPorAsesorBO objetoBO = Mapper.Map<TGrupoFiltroProgramaCriticoPorAsesor, GrupoFiltroProgramaCriticoPorAsesorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GrupoFiltroProgramaCriticoPorAsesorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGrupoFiltroProgramaCriticoPorAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GrupoFiltroProgramaCriticoPorAsesorBO> listadoBO)
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

        public bool Update(GrupoFiltroProgramaCriticoPorAsesorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGrupoFiltroProgramaCriticoPorAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GrupoFiltroProgramaCriticoPorAsesorBO> listadoBO)
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
        private void AsignacionId(TGrupoFiltroProgramaCriticoPorAsesor entidad, GrupoFiltroProgramaCriticoPorAsesorBO objetoBO)
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

        private TGrupoFiltroProgramaCriticoPorAsesor MapeoEntidad(GrupoFiltroProgramaCriticoPorAsesorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGrupoFiltroProgramaCriticoPorAsesor entidad = new TGrupoFiltroProgramaCriticoPorAsesor();
                entidad = Mapper.Map<GrupoFiltroProgramaCriticoPorAsesorBO, TGrupoFiltroProgramaCriticoPorAsesor>(objetoBO,
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
        /// Elimina (Actualiza estado a false ) todos los asesores asociados a un GrupoFiltroProgramaCritico
        /// </summary>
        /// <returns></returns>
        public void EliminacionLogicoPorGrupoFiltro(int idGrupo, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdGrupoFiltroProgramaCritico == idGrupo && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdPersonal));
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
