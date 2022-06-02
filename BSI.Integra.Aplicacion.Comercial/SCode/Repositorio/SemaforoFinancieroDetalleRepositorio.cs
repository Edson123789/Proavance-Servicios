using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs.Comercial;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class SemaforoFinancieroDetalleRepositorio : BaseRepository<TSemaforoFinancieroDetalle, SemaforoFinancieroDetalleBO>
    {
        #region Metodos Base
        public SemaforoFinancieroDetalleRepositorio() : base()
        {
        }
        public SemaforoFinancieroDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<SemaforoFinancieroDetalleBO> GetBy(Expression<Func<TSemaforoFinancieroDetalle, bool>> filter)
        {
            IEnumerable<TSemaforoFinancieroDetalle> listado = base.GetBy(filter);
            List<SemaforoFinancieroDetalleBO> listadoBO = new List<SemaforoFinancieroDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                SemaforoFinancieroDetalleBO objetoBO = Mapper.Map<TSemaforoFinancieroDetalle, SemaforoFinancieroDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SemaforoFinancieroDetalleBO FirstById(int id)
        {
            try
            {
                TSemaforoFinancieroDetalle entidad = base.FirstById(id);
                SemaforoFinancieroDetalleBO objetoBO = new SemaforoFinancieroDetalleBO();
                Mapper.Map<TSemaforoFinancieroDetalle, SemaforoFinancieroDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SemaforoFinancieroDetalleBO FirstBy(Expression<Func<TSemaforoFinancieroDetalle, bool>> filter)
        {
            try
            {
                TSemaforoFinancieroDetalle entidad = base.FirstBy(filter);
                SemaforoFinancieroDetalleBO objetoBO = Mapper.Map<TSemaforoFinancieroDetalle, SemaforoFinancieroDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SemaforoFinancieroDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSemaforoFinancieroDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SemaforoFinancieroDetalleBO> listadoBO)
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

        public bool Update(SemaforoFinancieroDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSemaforoFinancieroDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SemaforoFinancieroDetalleBO> listadoBO)
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
        private void AsignacionId(TSemaforoFinancieroDetalle entidad, SemaforoFinancieroDetalleBO objetoBO)
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

        private TSemaforoFinancieroDetalle MapeoEntidad(SemaforoFinancieroDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSemaforoFinancieroDetalle entidad = new TSemaforoFinancieroDetalle();
                entidad = Mapper.Map<SemaforoFinancieroDetalleBO, TSemaforoFinancieroDetalle>(objetoBO,
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
        /// Autor: Jashin Salazar
        /// Fecha: 08/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene las caracteristicas para la construccion del avatar
        /// </summary>
        /// <returns> AvatarCaracteristicaAgrupadoDTO </returns>
		public List<SemaforoFinancieroDetalleVariableDTO> ObtenerVariables(int IdSemaforoFinancieroDetalle)
        {
            try
            {
                List<SemaforoFinancieroDetalleVariableDTO> respuesta = new List<SemaforoFinancieroDetalleVariableDTO>();
                var query = "com.SP_ObtenerSemaforoFinancieroDetalleVariable";
                var respuestaBD = _dapper.QuerySPDapper(query, new { semaforoDetalle = IdSemaforoFinancieroDetalle });

                if (!string.IsNullOrEmpty(respuestaBD) && !respuestaBD.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<SemaforoFinancieroDetalleVariableDTO>>(respuestaBD);
                }
                return respuesta;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

