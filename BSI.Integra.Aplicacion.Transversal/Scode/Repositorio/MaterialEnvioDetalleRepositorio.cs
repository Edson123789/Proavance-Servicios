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
    public class MaterialEnvioDetalleRepositorio : BaseRepository<TMaterialEnvioDetalle, MaterialEnvioDetalleBO>
    {
        #region Metodos Base
        public MaterialEnvioDetalleRepositorio() : base()
        {
        }
        public MaterialEnvioDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialEnvioDetalleBO> GetBy(Expression<Func<TMaterialEnvioDetalle, bool>> filter)
        {
            IEnumerable<TMaterialEnvioDetalle> listado = base.GetBy(filter);
            List<MaterialEnvioDetalleBO> listadoBO = new List<MaterialEnvioDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialEnvioDetalleBO objetoBO = Mapper.Map<TMaterialEnvioDetalle, MaterialEnvioDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialEnvioDetalleBO FirstById(int id)
        {
            try
            {
                TMaterialEnvioDetalle entidad = base.FirstById(id);
                MaterialEnvioDetalleBO objetoBO = new MaterialEnvioDetalleBO();
                Mapper.Map<TMaterialEnvioDetalle, MaterialEnvioDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialEnvioDetalleBO FirstBy(Expression<Func<TMaterialEnvioDetalle, bool>> filter)
        {
            try
            {
                TMaterialEnvioDetalle entidad = base.FirstBy(filter);
                MaterialEnvioDetalleBO objetoBO = Mapper.Map<TMaterialEnvioDetalle, MaterialEnvioDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialEnvioDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialEnvioDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialEnvioDetalleBO> listadoBO)
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

        public bool Update(MaterialEnvioDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialEnvioDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialEnvioDetalleBO> listadoBO)
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
        private void AsignacionId(TMaterialEnvioDetalle entidad, MaterialEnvioDetalleBO objetoBO)
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

        private TMaterialEnvioDetalle MapeoEntidad(MaterialEnvioDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialEnvioDetalle entidad = new TMaterialEnvioDetalle();
                entidad = Mapper.Map<MaterialEnvioDetalleBO, TMaterialEnvioDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialEnvioDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialEnvioDetalle, bool>>> filters, Expression<Func<TMaterialEnvioDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialEnvioDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialEnvioDetalleBO> listadoBO = new List<MaterialEnvioDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialEnvioDetalleBO objetoBO = Mapper.Map<TMaterialEnvioDetalle, MaterialEnvioDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene el detalle del material
        /// </summary>
        /// <param name="idMaterialEnvio"></param>
        /// <returns></returns>
        public List<DetalleMaterialEnvioDTO> ObtenerDetalle(int idMaterialEnvio)
        {
            try
            {
                //return this.GetBy(x => x.Estado && x.IdMaterialEnvio == idMaterialEnvio).ToList();
                var lista = new List<DetalleMaterialEnvioDTO>();
                var query = $@"
                    SELECT Id, 
                           IdMaterialEnvio, 
                           IdPEspecifico, 
                           IdPEspecificoHijo, 
                           IdGrupo, 
                           IdMaterialVersion, 
                           IdMaterialEstadoRecepcion, 
                           IdPersonalReceptor, 
                           CantidadEnvio, 
                           CantidadRecepcion, 
                           ComentarioEnvio, 
                           ComentarioRecepcion, 
                           UsuarioCreacion, 
                           UsuarioModificacion, 
                           FechaCreacion, 
                           FechaModificacion
                    FROM ope.V_ObtenerMaterialEnvioDetalle
                    WHERE EstadoMaterialEnvioDetalle = 1
                          AND EstadoMaterialVersion = 1
                          AND EstadoMaterialPEspecificoSesion = 1
                          AND EstadoPEspecificoSesion = 1
                          AND EstadoPEspecifico = 1
                          AND IdMaterialEnvio = @idMaterialEnvio;
                ";
                var resultadoDB = _dapper.QueryDapper(query, new { idMaterialEnvio });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<DetalleMaterialEnvioDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
