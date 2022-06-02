using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/MaterialAsociacionAccion
    /// Autor: Wilber Choque - Gian Miranda
    /// Fecha: 11/07/2021
    /// <summary>
    /// Repositorio para operaciones con la tabla ope.T_MaterialAsociacionAccion
    /// </summary>
    public class MaterialAsociacionAccionRepositorio : BaseRepository<TMaterialAsociacionAccion, MaterialAsociacionAccionBO>
    {
        #region Metodos Base
        public MaterialAsociacionAccionRepositorio() : base()
        {
        }
        public MaterialAsociacionAccionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialAsociacionAccionBO> GetBy(Expression<Func<TMaterialAsociacionAccion, bool>> filter)
        {
            IEnumerable<TMaterialAsociacionAccion> listado = base.GetBy(filter);
            List<MaterialAsociacionAccionBO> listadoBO = new List<MaterialAsociacionAccionBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialAsociacionAccionBO objetoBO = Mapper.Map<TMaterialAsociacionAccion, MaterialAsociacionAccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialAsociacionAccionBO FirstById(int id)
        {
            try
            {
                TMaterialAsociacionAccion entidad = base.FirstById(id);
                MaterialAsociacionAccionBO objetoBO = new MaterialAsociacionAccionBO();
                Mapper.Map<TMaterialAsociacionAccion, MaterialAsociacionAccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialAsociacionAccionBO FirstBy(Expression<Func<TMaterialAsociacionAccion, bool>> filter)
        {
            try
            {
                TMaterialAsociacionAccion entidad = base.FirstBy(filter);
                MaterialAsociacionAccionBO objetoBO = Mapper.Map<TMaterialAsociacionAccion, MaterialAsociacionAccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialAsociacionAccionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialAsociacionAccion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialAsociacionAccionBO> listadoBO)
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

        public bool Update(MaterialAsociacionAccionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialAsociacionAccion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialAsociacionAccionBO> listadoBO)
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
        private void AsignacionId(TMaterialAsociacionAccion entidad, MaterialAsociacionAccionBO objetoBO)
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

        private TMaterialAsociacionAccion MapeoEntidad(MaterialAsociacionAccionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialAsociacionAccion entidad = new TMaterialAsociacionAccion();
                entidad = Mapper.Map<MaterialAsociacionAccionBO, TMaterialAsociacionAccion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialAsociacionAccionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialAsociacionAccion, bool>>> filters, Expression<Func<TMaterialAsociacionAccion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialAsociacionAccion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialAsociacionAccionBO> listadoBO = new List<MaterialAsociacionAccionBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialAsociacionAccionBO objetoBO = Mapper.Map<TMaterialAsociacionAccion, MaterialAsociacionAccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Elimina de forma fisica los registros asociados
        /// </summary>
        /// <param name="idMaterialTipo"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorMaterialTipo(int idMaterialTipo, string nombreUsuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdMaterialTipo == idMaterialTipo && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdMaterialAccion));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, nombreUsuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
