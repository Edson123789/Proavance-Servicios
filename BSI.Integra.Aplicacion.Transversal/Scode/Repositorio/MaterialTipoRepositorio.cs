using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/PGeneral
    /// Autor: Wilber Choque - Gian Miranda
    /// Fecha: 11/07/2021
    /// <summary>
    /// Repositorio para consultas de ope.T_MaterialTipo
    /// </summary>
    public class MaterialTipoRepositorio : BaseRepository<TMaterialTipo, MaterialTipoBO>
    {
        #region Metodos Base
        public MaterialTipoRepositorio() : base()
        {
        }
        public MaterialTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialTipoBO> GetBy(Expression<Func<TMaterialTipo, bool>> filter)
        {
            IEnumerable<TMaterialTipo> listado = base.GetBy(filter);
            List<MaterialTipoBO> listadoBO = new List<MaterialTipoBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialTipoBO objetoBO = Mapper.Map<TMaterialTipo, MaterialTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialTipoBO FirstById(int id)
        {
            try
            {
                TMaterialTipo entidad = base.FirstById(id);
                MaterialTipoBO objetoBO = new MaterialTipoBO();
                Mapper.Map<TMaterialTipo, MaterialTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialTipoBO FirstBy(Expression<Func<TMaterialTipo, bool>> filter)
        {
            try
            {
                TMaterialTipo entidad = base.FirstBy(filter);
                MaterialTipoBO objetoBO = Mapper.Map<TMaterialTipo, MaterialTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialTipoBO> listadoBO)
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

        public bool Update(MaterialTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialTipoBO> listadoBO)
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
        private void AsignacionId(TMaterialTipo entidad, MaterialTipoBO objetoBO)
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

        private TMaterialTipo MapeoEntidad(MaterialTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialTipo entidad = new TMaterialTipo();
                entidad = Mapper.Map<MaterialTipoBO, TMaterialTipo>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaMaterialAsociacionAccion != null && objetoBO.ListaMaterialAsociacionAccion.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaMaterialAsociacionAccion)
                    {
                        TMaterialAsociacionAccion entidadHijo = new TMaterialAsociacionAccion();
                        entidadHijo = Mapper.Map<MaterialAsociacionAccionBO, TMaterialAsociacionAccion>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TMaterialAsociacionAccion.Add(entidadHijo);
                    }
                }
                if (objetoBO.ListaMaterialAsociacionCriterioVerificacion != null && objetoBO.ListaMaterialAsociacionCriterioVerificacion.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaMaterialAsociacionCriterioVerificacion)
                    {
                        TMaterialAsociacionCriterioVerificacion entidadHijo = new TMaterialAsociacionCriterioVerificacion();
                        entidadHijo = Mapper.Map<MaterialAsociacionCriterioVerificacionBO, TMaterialAsociacionCriterioVerificacion>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TMaterialAsociacionCriterioVerificacion.Add(entidadHijo);
                    }
                }
                if (objetoBO.ListaMaterialAsociacionVersion != null && objetoBO.ListaMaterialAsociacionVersion.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaMaterialAsociacionVersion)
                    {
                        TMaterialAsociacionVersion entidadHijo = new TMaterialAsociacionVersion();
                        entidadHijo = Mapper.Map<MaterialAsociacionVersionBO, TMaterialAsociacionVersion>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TMaterialAsociacionVersion.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialTipoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialTipo, bool>>> filters, Expression<Func<TMaterialTipo, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialTipo> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialTipoBO> listadoBO = new List<MaterialTipoBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialTipoBO objetoBO = Mapper.Map<TMaterialTipo, MaterialTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los tipo de materiales
        /// </summary>
        /// <returns>Lista de objetos de clase FiltroDTO</returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la lista de tipos de materiales
        /// </summary>
        /// <returns>Lista de objetos de clase MaterialTipoBO</returns>
        public List<MaterialTipoBO> Obtener()
        {
            try
            {
                return this.GetBy(x => x.Estado).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
