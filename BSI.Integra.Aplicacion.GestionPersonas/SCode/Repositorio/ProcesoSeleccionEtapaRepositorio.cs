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
    /// Repositorio: ProcesoSeleccionEtapaRepositorio
    /// Autor: Britsel C., Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión Etapas por Proceso de Seleccion
    /// </summary>
    public class ProcesoSeleccionEtapaRepositorio : BaseRepository<TProcesoSeleccionEtapa, ProcesoSeleccionEtapaBO>
    {
        #region Metodos Base
        public ProcesoSeleccionEtapaRepositorio() : base()
        {
        }
        public ProcesoSeleccionEtapaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProcesoSeleccionEtapaBO> GetBy(Expression<Func<TProcesoSeleccionEtapa, bool>> filter)
        {
            IEnumerable<TProcesoSeleccionEtapa> listado = base.GetBy(filter);
            List<ProcesoSeleccionEtapaBO> listadoBO = new List<ProcesoSeleccionEtapaBO>();
            foreach (var itemEntidad in listado)
            {
                ProcesoSeleccionEtapaBO objetoBO = Mapper.Map<TProcesoSeleccionEtapa, ProcesoSeleccionEtapaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProcesoSeleccionEtapaBO FirstById(int id)
        {
            try
            {
                TProcesoSeleccionEtapa entidad = base.FirstById(id);
                ProcesoSeleccionEtapaBO objetoBO = new ProcesoSeleccionEtapaBO();
                Mapper.Map<TProcesoSeleccionEtapa, ProcesoSeleccionEtapaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProcesoSeleccionEtapaBO FirstBy(Expression<Func<TProcesoSeleccionEtapa, bool>> filter)
        {
            try
            {
                TProcesoSeleccionEtapa entidad = base.FirstBy(filter);
                ProcesoSeleccionEtapaBO objetoBO = Mapper.Map<TProcesoSeleccionEtapa, ProcesoSeleccionEtapaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProcesoSeleccionEtapaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProcesoSeleccionEtapa entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProcesoSeleccionEtapaBO> listadoBO)
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

        public bool Update(ProcesoSeleccionEtapaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProcesoSeleccionEtapa entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProcesoSeleccionEtapaBO> listadoBO)
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
        private void AsignacionId(TProcesoSeleccionEtapa entidad, ProcesoSeleccionEtapaBO objetoBO)
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

        private TProcesoSeleccionEtapa MapeoEntidad(ProcesoSeleccionEtapaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProcesoSeleccionEtapa entidad = new TProcesoSeleccionEtapa();
                entidad = Mapper.Map<ProcesoSeleccionEtapaBO, TProcesoSeleccionEtapa>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ProcesoSeleccionEtapaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TProcesoSeleccionEtapa, bool>>> filters, Expression<Func<TProcesoSeleccionEtapa, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TProcesoSeleccionEtapa> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ProcesoSeleccionEtapaBO> listadoBO = new List<ProcesoSeleccionEtapaBO>();

            foreach (var itemEntidad in listado)
            {
                ProcesoSeleccionEtapaBO objetoBO = Mapper.Map<TProcesoSeleccionEtapa, ProcesoSeleccionEtapaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion


        public List<ProcesoSeleccionEtapaDTO> ObtenerProcesoSeleccionEtapa()
        {
            try
            {
                List<ProcesoSeleccionEtapaDTO> etapa = new List<ProcesoSeleccionEtapaDTO>();
                var campos = "Id,Nombre,IdProcesoSeleccion,NombreProcesoSeleccion,NroOrden";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerProcesoSeleccionEtapa ";
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    etapa = JsonConvert.DeserializeObject<List<ProcesoSeleccionEtapaDTO>>(dataDB);
                }
                return etapa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
