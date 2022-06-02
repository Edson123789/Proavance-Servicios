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
    public class ProveedorTipoServicioRepositorio : BaseRepository<TProveedorTipoServicio, ProveedorTipoServicioBO>
    {
        #region Metodos Base
        public ProveedorTipoServicioRepositorio() : base()
        {
        }
        public ProveedorTipoServicioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProveedorTipoServicioBO> GetBy(Expression<Func<TProveedorTipoServicio, bool>> filter)
        {
            IEnumerable<TProveedorTipoServicio> listado = base.GetBy(filter);
            List<ProveedorTipoServicioBO> listadoBO = new List<ProveedorTipoServicioBO>();
            foreach (var itemEntidad in listado)
            {
                ProveedorTipoServicioBO objetoBO = Mapper.Map<TProveedorTipoServicio, ProveedorTipoServicioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProveedorTipoServicioBO FirstById(int id)
        {
            try
            {
                TProveedorTipoServicio entidad = base.FirstById(id);
                ProveedorTipoServicioBO objetoBO = new ProveedorTipoServicioBO();
                Mapper.Map<TProveedorTipoServicio, ProveedorTipoServicioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProveedorTipoServicioBO FirstBy(Expression<Func<TProveedorTipoServicio, bool>> filter)
        {
            try
            {
                TProveedorTipoServicio entidad = base.FirstBy(filter);
                ProveedorTipoServicioBO objetoBO = Mapper.Map<TProveedorTipoServicio, ProveedorTipoServicioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProveedorTipoServicioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProveedorTipoServicio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProveedorTipoServicioBO> listadoBO)
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

        public bool Update(ProveedorTipoServicioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProveedorTipoServicio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProveedorTipoServicioBO> listadoBO)
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
        private void AsignacionId(TProveedorTipoServicio entidad, ProveedorTipoServicioBO objetoBO)
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

        private TProveedorTipoServicio MapeoEntidad(ProveedorTipoServicioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProveedorTipoServicio entidad = new TProveedorTipoServicio();
                entidad = Mapper.Map<ProveedorTipoServicioBO, TProveedorTipoServicio>(objetoBO,
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
        /// Obtiene todas los servicios asociados al proveedor
        /// </summary>
        /// <param name="listaIdPlantilla"></param>
        /// <returns></returns>
        public List<ProveedorTipoServicioDTO> ObtenerPorProveedor(List<int> listaIdProveedor)
        {
            try
            {
                var lista = new List<ProveedorTipoServicioDTO>();
                string query = $@"
                                SELECT Id, 
                                       IdProveedor, 
                                       IdTipoServicio
                                FROM mkt.V_ObtenerProveedorTipoServicio
                                WHERE EstadoProveedorTipoServicio = 1
                                      AND EstadoProveedor = 1
                                      AND EstadoTipoServicio = 1
	                                  AND IdProveedor IN @listaIdProveedor
                                ";
                var resultadoDB = _dapper.QueryDapper(query, new { listaIdProveedor = listaIdProveedor.ToArray() });

                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ProveedorTipoServicioDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Elimina de forma fisica los registros asociados al proveedor
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorPlantilla(int idProveedor, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdProveedor == idProveedor && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdProveedor));
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
