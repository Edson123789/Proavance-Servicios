using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MaterialEnvioRepositorio : BaseRepository<TMaterialEnvio, MaterialEnvioBO>
    {
        #region Metodos Base
        public MaterialEnvioRepositorio() : base()
        {
        }
        public MaterialEnvioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialEnvioBO> GetBy(Expression<Func<TMaterialEnvio, bool>> filter)
        {
            IEnumerable<TMaterialEnvio> listado = base.GetBy(filter);
            List<MaterialEnvioBO> listadoBO = new List<MaterialEnvioBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialEnvioBO objetoBO = Mapper.Map<TMaterialEnvio, MaterialEnvioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialEnvioBO FirstById(int id)
        {
            try
            {
                TMaterialEnvio entidad = base.FirstById(id);
                MaterialEnvioBO objetoBO = new MaterialEnvioBO();
                Mapper.Map<TMaterialEnvio, MaterialEnvioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialEnvioBO FirstBy(Expression<Func<TMaterialEnvio, bool>> filter)
        {
            try
            {
                TMaterialEnvio entidad = base.FirstBy(filter);
                MaterialEnvioBO objetoBO = Mapper.Map<TMaterialEnvio, MaterialEnvioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialEnvioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialEnvioBO> listadoBO)
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

        public bool Update(MaterialEnvioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialEnvioBO> listadoBO)
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
        private void AsignacionId(TMaterialEnvio entidad, MaterialEnvioBO objetoBO)
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

        private TMaterialEnvio MapeoEntidad(MaterialEnvioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialEnvio entidad = new TMaterialEnvio();
                entidad = Mapper.Map<MaterialEnvioBO, TMaterialEnvio>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaMaterialEnvioDetalle != null && objetoBO.ListaMaterialEnvioDetalle.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaMaterialEnvioDetalle)
                    {
                        TMaterialEnvioDetalle entidadHijo = new TMaterialEnvioDetalle();
                        entidadHijo = Mapper.Map<MaterialEnvioDetalleBO, TMaterialEnvioDetalle>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TMaterialEnvioDetalle.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialEnvioBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialEnvio, bool>>> filters, Expression<Func<TMaterialEnvio, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialEnvio> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialEnvioBO> listadoBO = new List<MaterialEnvioBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialEnvioBO objetoBO = Mapper.Map<TMaterialEnvio, MaterialEnvioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene una lista de materiales
        /// </summary>
        /// <returns></returns>
        public List<MaterialEnvioPanelDTO> Obtener()
        {
            try
            {
                var listaMaterialEnvio = new List<MaterialEnvioPanelDTO>();
                var _query = $@"
                                SELECT Id, 
                                       IdSedeTrabajo, 
                                       NombreSedeTrabajo, 
                                       IdPersonalRemitente, 
                                       NombrePersonalRemitente, 
                                       IdProveedorEnvio, 
                                       NombreProveedorEnvio, 
                                       FechaEnvio
                                FROM ope.V_ObtenerMaterialEnvio
                                WHERE EstadoMaterialEnvio = 1
                                      AND EstadoSedeTrabajo = 1
                                      AND EstadoPersonalRemitente = 1
                                      AND EstadoProveedorEnvio = 1;
                                ";
                var _respuesta = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(_respuesta) && !_respuesta.Contains("[]"))
                {
                    listaMaterialEnvio = JsonConvert.DeserializeObject<List<MaterialEnvioPanelDTO>>(_respuesta);
                }
                return listaMaterialEnvio;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
