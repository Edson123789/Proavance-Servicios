using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: GrupoComparacionProcesoSeleccionRepositorio
    /// Autor: _ _ _ _ _ _ _ _.
    /// Fecha: 29/03/2021
    /// <summary>
    /// Gestión de Grupo de Comperación de Procesos de Selección
    /// </summary>
    public class GrupoComparacionProcesoSeleccionRepositorio : BaseRepository<TGrupoComparacionProcesoSeleccion, GrupoComparacionProcesoSeleccionBO>
    {
        #region Metodos Base
        public GrupoComparacionProcesoSeleccionRepositorio() : base()
        {
        }
        public GrupoComparacionProcesoSeleccionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GrupoComparacionProcesoSeleccionBO> GetBy(Expression<Func<TGrupoComparacionProcesoSeleccion, bool>> filter)
        {
            IEnumerable<TGrupoComparacionProcesoSeleccion> listado = base.GetBy(filter);
            List<GrupoComparacionProcesoSeleccionBO> listadoBO = new List<GrupoComparacionProcesoSeleccionBO>();
            foreach (var itemEntidad in listado)
            {
                GrupoComparacionProcesoSeleccionBO objetoBO = Mapper.Map<TGrupoComparacionProcesoSeleccion, GrupoComparacionProcesoSeleccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GrupoComparacionProcesoSeleccionBO FirstById(int id)
        {
            try
            {
                TGrupoComparacionProcesoSeleccion entidad = base.FirstById(id);
                GrupoComparacionProcesoSeleccionBO objetoBO = new GrupoComparacionProcesoSeleccionBO();
                Mapper.Map<TGrupoComparacionProcesoSeleccion, GrupoComparacionProcesoSeleccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GrupoComparacionProcesoSeleccionBO FirstBy(Expression<Func<TGrupoComparacionProcesoSeleccion, bool>> filter)
        {
            try
            {
                TGrupoComparacionProcesoSeleccion entidad = base.FirstBy(filter);
                GrupoComparacionProcesoSeleccionBO objetoBO = Mapper.Map<TGrupoComparacionProcesoSeleccion, GrupoComparacionProcesoSeleccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GrupoComparacionProcesoSeleccionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGrupoComparacionProcesoSeleccion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GrupoComparacionProcesoSeleccionBO> listadoBO)
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

        public bool Update(GrupoComparacionProcesoSeleccionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGrupoComparacionProcesoSeleccion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GrupoComparacionProcesoSeleccionBO> listadoBO)
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
        private void AsignacionId(TGrupoComparacionProcesoSeleccion entidad, GrupoComparacionProcesoSeleccionBO objetoBO)
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

        private TGrupoComparacionProcesoSeleccion MapeoEntidad(GrupoComparacionProcesoSeleccionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGrupoComparacionProcesoSeleccion entidad = new TGrupoComparacionProcesoSeleccion();
                entidad = Mapper.Map<GrupoComparacionProcesoSeleccionBO, TGrupoComparacionProcesoSeleccion>(objetoBO,
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

        ///Repositorio: GrupoComparacionProcesoSeleccionRepositorio
        ///Autor: _ _ _ _ _ _ _ _ _ _ _.
        ///Fecha: 29/03/2021
        /// <summary>
        /// Obtiene lista de Grupos de Comparación Configurados
        /// </summary>
        /// <returns> lista de objetos DTO: List<GrupoComparacionProcesoSeleccionDTO> </returns>
        public List<GrupoComparacionProcesoSeleccionDTO> ObtenerGrupoComparacion()
        {
            try
            {
                List<GrupoComparacionProcesoSeleccionDTO> listaComparacion = new List<GrupoComparacionProcesoSeleccionDTO>();
                var campos = "Id,Nombre,IdPuestoTrabajo,IdSedeTrabajo,IdPostulante ";

                var query = "SELECT " + campos + " FROM  gp.V_GrupoComparacionProsesoSeleccion";
                var dataDB = _dapper.QueryDapper(query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    listaComparacion = JsonConvert.DeserializeObject<List<GrupoComparacionProcesoSeleccionDTO>>(dataDB);
                }
                return listaComparacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: GrupoComparacionProcesoSeleccionRepositorio
        ///Autor: _ _ _ _ _ _ _ _ _ _ _.
        ///Fecha: 29/03/2021
        /// <summary>
        /// Obtiene filtro Id Nombre
        /// </summary>
        /// <returns> lista de objetos DTO: List<FiltroDTO> </returns>
        public List<FiltroDTO> GetFiltroIdNombre()
        {
            var lista = GetBy(x => x.Estado == true, y => new FiltroDTO
            {
                Id = y.Id,
                Nombre = y.Nombre
            }).ToList();
            return lista;
        }
    }
}
