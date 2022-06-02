using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.DTO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PgeneralProyectoAplicacionEnvioRepositorio : BaseRepository<TPgeneralProyectoAplicacionEnvio, PgeneralProyectoAplicacionEnvioBO>
    {
        #region Metodos Base
        public PgeneralProyectoAplicacionEnvioRepositorio() : base()
        {
        }
        public PgeneralProyectoAplicacionEnvioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralProyectoAplicacionEnvioBO> GetBy(Expression<Func<TPgeneralProyectoAplicacionEnvio, bool>> filter)
        {
            IEnumerable<TPgeneralProyectoAplicacionEnvio> listado = base.GetBy(filter);
            List<PgeneralProyectoAplicacionEnvioBO> listadoBO = new List<PgeneralProyectoAplicacionEnvioBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralProyectoAplicacionEnvioBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacionEnvio, PgeneralProyectoAplicacionEnvioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralProyectoAplicacionEnvioBO FirstById(int id)
        {
            try
            {
                TPgeneralProyectoAplicacionEnvio entidad = base.FirstById(id);
                PgeneralProyectoAplicacionEnvioBO objetoBO = new PgeneralProyectoAplicacionEnvioBO();
                Mapper.Map<TPgeneralProyectoAplicacionEnvio, PgeneralProyectoAplicacionEnvioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralProyectoAplicacionEnvioBO FirstBy(Expression<Func<TPgeneralProyectoAplicacionEnvio, bool>> filter)
        {
            try
            {
                TPgeneralProyectoAplicacionEnvio entidad = base.FirstBy(filter);
                PgeneralProyectoAplicacionEnvioBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacionEnvio, PgeneralProyectoAplicacionEnvioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralProyectoAplicacionEnvioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralProyectoAplicacionEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralProyectoAplicacionEnvioBO> listadoBO)
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

        public bool Update(PgeneralProyectoAplicacionEnvioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralProyectoAplicacionEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralProyectoAplicacionEnvioBO> listadoBO)
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
        private void AsignacionId(TPgeneralProyectoAplicacionEnvio entidad, PgeneralProyectoAplicacionEnvioBO objetoBO)
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

        private TPgeneralProyectoAplicacionEnvio MapeoEntidad(PgeneralProyectoAplicacionEnvioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralProyectoAplicacionEnvio entidad = new TPgeneralProyectoAplicacionEnvio();
                entidad = Mapper.Map<PgeneralProyectoAplicacionEnvioBO, TPgeneralProyectoAplicacionEnvio>(objetoBO,
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
        /// Obtiene el listado resumen de los Proyectos del Aula virtual anterior in calificar
        /// </summary>
        /// <returns></returns>
        public List<ProyectoAplicacionEnvioSinCalificarResumenCabeceraCorreoDTO> ListadoProyectoAplicacionAulaAnterior_SinCalificar()
        {
            try
            {
                List<ProyectoAplicacionEnvioSinCalificarResumenCabeceraCorreoDTO> listado = new List<ProyectoAplicacionEnvioSinCalificarResumenCabeceraCorreoDTO>();
                string sql = "SELECT IdPgeneralProyectoAplicacionEnvio, IdMatriculaCabecera, IdProveedor, EmailProveedor, IdPersonalResponsableCoordinacion, EmailResponsableCoordinacion FROM ope.V_ObtenerProyectoAplicacion_AulaAnterior_SinCalificar";
                var query = _dapper.QueryDapper(sql, null);
                listado = JsonConvert.DeserializeObject<List<ProyectoAplicacionEnvioSinCalificarResumenCabeceraCorreoDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado resumen de los Proyectos del Aula virtual anterior in calificar por Proveedor
        /// </summary>
        /// <returns>Lista de ProyectoAplicacionEnvioSinCalificarResumenCabeceraCorreoDTO</returns>
        public List<ProyectoAplicacionEnvioSinCalificarResumenCabeceraCorreoDTO> ListadoProyectoAplicacionAulaAnterior_SinCalificarPorProveedor(int idProveedor)
        {
            try
            {
                List<ProyectoAplicacionEnvioSinCalificarResumenCabeceraCorreoDTO> listado = new List<ProyectoAplicacionEnvioSinCalificarResumenCabeceraCorreoDTO>();
                string sql = "SELECT IdPgeneralProyectoAplicacionEnvio, IdMatriculaCabecera, CodigoMatricula, Alumno, PEspecifico, IdProveedor, EmailProveedor, IdPersonalResponsableCoordinacion, EmailResponsableCoordinacion FROM ope.V_ObtenerProyectoAplicacion_AulaAnterior_SinCalificar WHERE IdProveedor = @idProveedor";
                var query = _dapper.QueryDapper(sql, new {idProveedor = idProveedor});
                listado = JsonConvert.DeserializeObject<List<ProyectoAplicacionEnvioSinCalificarResumenCabeceraCorreoDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
