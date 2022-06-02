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
    /// Repositorio: Transversal/MaterialAsociacionVersion
    /// Autor: Wilber Choque - Gian Miranda
    /// Fecha: 11/07/2021
    /// <summary>
    /// Repositorio para operaciones con la tabla ope.T_MaterialAsociacionVersion
    /// </summary>
    public class MaterialAsociacionVersionRepositorio : BaseRepository<TMaterialAsociacionVersion, MaterialAsociacionVersionBO>
    {
        #region Metodos Base
        public MaterialAsociacionVersionRepositorio() : base()
        {
        }
        public MaterialAsociacionVersionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialAsociacionVersionBO> GetBy(Expression<Func<TMaterialAsociacionVersion, bool>> filter)
        {
            IEnumerable<TMaterialAsociacionVersion> listado = base.GetBy(filter);
            List<MaterialAsociacionVersionBO> listadoBO = new List<MaterialAsociacionVersionBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialAsociacionVersionBO objetoBO = Mapper.Map<TMaterialAsociacionVersion, MaterialAsociacionVersionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialAsociacionVersionBO FirstById(int id)
        {
            try
            {
                TMaterialAsociacionVersion entidad = base.FirstById(id);
                MaterialAsociacionVersionBO objetoBO = new MaterialAsociacionVersionBO();
                Mapper.Map<TMaterialAsociacionVersion, MaterialAsociacionVersionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialAsociacionVersionBO FirstBy(Expression<Func<TMaterialAsociacionVersion, bool>> filter)
        {
            try
            {
                TMaterialAsociacionVersion entidad = base.FirstBy(filter);
                MaterialAsociacionVersionBO objetoBO = Mapper.Map<TMaterialAsociacionVersion, MaterialAsociacionVersionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialAsociacionVersionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialAsociacionVersion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialAsociacionVersionBO> listadoBO)
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

        public bool Update(MaterialAsociacionVersionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialAsociacionVersion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialAsociacionVersionBO> listadoBO)
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
        private void AsignacionId(TMaterialAsociacionVersion entidad, MaterialAsociacionVersionBO objetoBO)
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

        private TMaterialAsociacionVersion MapeoEntidad(MaterialAsociacionVersionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialAsociacionVersion entidad = new TMaterialAsociacionVersion();
                entidad = Mapper.Map<MaterialAsociacionVersionBO, TMaterialAsociacionVersion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialAsociacionVersionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialAsociacionVersion, bool>>> filters, Expression<Func<TMaterialAsociacionVersion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialAsociacionVersion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialAsociacionVersionBO> listadoBO = new List<MaterialAsociacionVersionBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialAsociacionVersionBO objetoBO = Mapper.Map<TMaterialAsociacionVersion, MaterialAsociacionVersionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
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
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdMaterialVersion));
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
