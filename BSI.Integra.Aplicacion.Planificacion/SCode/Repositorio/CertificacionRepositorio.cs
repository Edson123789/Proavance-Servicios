
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class CertificacionRepositorio : BaseRepository<TCertificacion, CertificacionBO>
    {
        #region Metodos Base
        public CertificacionRepositorio() : base()
        {
        }
        public CertificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificacionBO> GetBy(Expression<Func<TCertificacion, bool>> filter)
        {
            IEnumerable<TCertificacion> listado = base.GetBy(filter);
            List<CertificacionBO> listadoBO = new List<CertificacionBO>();
            foreach (var itemEntidad in listado)
            {
                CertificacionBO objetoBO = Mapper.Map<TCertificacion, CertificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificacionBO FirstById(int id)
        {
            try
            {
                TCertificacion entidad = base.FirstById(id);
                CertificacionBO objetoBO = new CertificacionBO();
                Mapper.Map<TCertificacion, CertificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificacionBO FirstBy(Expression<Func<TCertificacion, bool>> filter)
        {
            try
            {
                TCertificacion entidad = base.FirstBy(filter);
                CertificacionBO objetoBO = Mapper.Map<TCertificacion, CertificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificacionBO> listadoBO)
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

        public bool Update(CertificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificacionBO> listadoBO)
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
        private void AsignacionId(TCertificacion entidad, CertificacionBO objetoBO)
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

        private TCertificacion MapeoEntidad(CertificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificacion entidad = new TCertificacion();
                entidad = Mapper.Map<CertificacionBO, TCertificacion>(objetoBO,
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
        /// Obtiene una lista de Certificaciones de tipo CERTIFICACION (Id, Nombre) para ser usados en un combobox
        /// </summary>
        /// <returns></returns>
        public List<CertificacionParaFiltroDTO> ObtenerTodoCertificacionTipoCertificacion()
        {
            try
            {
                List<CertificacionParaFiltroDTO> Certificaciones = new List<CertificacionParaFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.T_Certificacion WHERE Estado = 1 AND IdCertificacionTipo=1";
                var CertificacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(CertificacionesDB) && !CertificacionesDB.Contains("[]"))
                {
                    Certificaciones = JsonConvert.DeserializeObject<List<CertificacionParaFiltroDTO>>(CertificacionesDB);
                }
                return Certificaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de Certificaciones de tipo MEBRESIAS (Id, Nombre) para ser usados en un combobox
        /// </summary>
        /// <returns></returns>
        public List<CertificacionParaFiltroDTO> ObtenerTodoCertificacionTipoMembresia()
        {
            try
            {
                List<CertificacionParaFiltroDTO> Certificaciones = new List<CertificacionParaFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.T_Certificacion WHERE Estado = 1 AND IdCertificacionTipo=2";
                var CertificacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(CertificacionesDB) && !CertificacionesDB.Contains("[]"))
                {
                    Certificaciones = JsonConvert.DeserializeObject<List<CertificacionParaFiltroDTO>>(CertificacionesDB);
                }
                return Certificaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una los datos de Certificaciones (activos) para ser usados en una grilla (CRUD propio)
        /// </summary>
        /// <returns></returns>
        public List<CertificacionExtendidoDTO> ObtenerTodoCertificaciones()
        {
            try
            {
                List<CertificacionExtendidoDTO> Certificaciones = new List<CertificacionExtendidoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion, IdPartner, Costo, IdMoneda, IdCertificacionTipo, Peru, Colombia FROM [pla].[V_TCertificacionConCantidadesPeruColombia] WHERE Estado = 1";
                var CertificacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(CertificacionesDB) && !CertificacionesDB.Contains("[]"))
                {
                    Certificaciones = JsonConvert.DeserializeObject<List<CertificacionExtendidoDTO>>(CertificacionesDB);
                }
                return Certificaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una los datos de una (1) Certificacion  para ser usados en llenado de un registro de grilla (CRUD propio)
        /// </summary>
        /// <returns></returns>
        public List<CertificacionExtendidoDTO> ObtenerCertificacionPorId(int IdCertificacion)
        {
            try
            {
                List<CertificacionExtendidoDTO> Certificaciones = new List<CertificacionExtendidoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion, IdPartner, Costo, IdMoneda, IdCertificacionTipo, Peru, Colombia FROM [pla].[V_TCertificacionConCantidadesPeruColombia] WHERE Estado = 1 and Id="+IdCertificacion;
                var CertificacionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(CertificacionesDB) && !CertificacionesDB.Contains("[]"))
                {
                    Certificaciones = JsonConvert.DeserializeObject<List<CertificacionExtendidoDTO>>(CertificacionesDB);
                }
                return Certificaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
