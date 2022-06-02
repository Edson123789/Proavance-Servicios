using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class EstadoPagoMatriculaRepositorio : BaseRepository<TEstadoPagoMatricula, EstadoPagoMatriculaBO>
    {
        #region Metodos Base
        public EstadoPagoMatriculaRepositorio() : base()
        {
        }
        public EstadoPagoMatriculaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstadoPagoMatriculaBO> GetBy(Expression<Func<TEstadoPagoMatricula, bool>> filter)
        {
            IEnumerable<TEstadoPagoMatricula> listado = base.GetBy(filter);
            List<EstadoPagoMatriculaBO> listadoBO = new List<EstadoPagoMatriculaBO>();
            foreach (var itemEntidad in listado)
            {
                EstadoPagoMatriculaBO objetoBO = Mapper.Map<TEstadoPagoMatricula, EstadoPagoMatriculaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstadoPagoMatriculaBO FirstById(int id)
        {
            try
            {
                TEstadoPagoMatricula entidad = base.FirstById(id);
                EstadoPagoMatriculaBO objetoBO = new EstadoPagoMatriculaBO();
                Mapper.Map<TEstadoPagoMatricula, EstadoPagoMatriculaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoPagoMatriculaBO FirstBy(Expression<Func<TEstadoPagoMatricula, bool>> filter)
        {
            try
            {
                TEstadoPagoMatricula entidad = base.FirstBy(filter);
                EstadoPagoMatriculaBO objetoBO = Mapper.Map<TEstadoPagoMatricula, EstadoPagoMatriculaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoPagoMatriculaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoPagoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoPagoMatriculaBO> listadoBO)
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

        public bool Update(EstadoPagoMatriculaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoPagoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoPagoMatriculaBO> listadoBO)
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
        private void AsignacionId(TEstadoPagoMatricula entidad, EstadoPagoMatriculaBO objetoBO)
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

        private TEstadoPagoMatricula MapeoEntidad(EstadoPagoMatriculaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoPagoMatricula entidad = new TEstadoPagoMatricula();
                entidad = Mapper.Map<EstadoPagoMatriculaBO, TEstadoPagoMatricula>(objetoBO,
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
        /// Obtiene un listado de estados de pago matricula para ser usados en filtros
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro() {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene el Id y Nombre del estado pago Matricula (pormatricular y matriculado)
		/// </summary>
		/// <param name="IdR"></param>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerEstadoPagoMatricula()
        {
            try
            {
                List<FiltroDTO> estadoPagoMatricula = new List<FiltroDTO>();
                string _queryEstadoPagoMatricula = string.Empty;
                _queryEstadoPagoMatricula = "SELECT Id,Nombre FROM fin.V_ObtenerEstadoPagoMatricula where Id < 3 and Estado = 1 ";
                var AlumnoDB = _dapper.QueryDapper(_queryEstadoPagoMatricula, new {});
                if (!string.IsNullOrEmpty(AlumnoDB) && !AlumnoDB.Contains("[]"))
                {
                    estadoPagoMatricula = JsonConvert.DeserializeObject<List<FiltroDTO>>(AlumnoDB);
                }
                return estadoPagoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene el Id y Nombre del estado pago Matricula (pormatricular y matriculado)
		/// </summary>
		/// <param name="IdR"></param>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerEstadoPagoMatriculaDevoluciones()
        {
            try
            {
                List<FiltroDTO> estadoPagoMatricula = new List<FiltroDTO>();
                string _queryEstadoPagoMatricula = string.Empty;
                _queryEstadoPagoMatricula = "SELECT Id,Nombre FROM fin.V_ObtenerEstadoPagoMatricula where Id > 2 and Estado = 1  ";
                var AlumnoDB = _dapper.QueryDapper(_queryEstadoPagoMatricula, new { });
                if (!string.IsNullOrEmpty(AlumnoDB) && !AlumnoDB.Contains("[]"))
                {
                    estadoPagoMatricula = JsonConvert.DeserializeObject<List<FiltroDTO>>(AlumnoDB);
                }
                return estadoPagoMatricula;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
