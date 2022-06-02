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
    public class SolicitudCertificadoFisicoRepositorio : BaseRepository<TSolicitudCertificadoFisico, SolicitudCertificadoFisicoBO>
    {
        #region Metodos Base
        public SolicitudCertificadoFisicoRepositorio() : base()
        {
        }
        public SolicitudCertificadoFisicoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SolicitudCertificadoFisicoBO> GetBy(Expression<Func<TSolicitudCertificadoFisico, bool>> filter)
        {
            IEnumerable<TSolicitudCertificadoFisico> listado = base.GetBy(filter);
            List<SolicitudCertificadoFisicoBO> listadoBO = new List<SolicitudCertificadoFisicoBO>();
            foreach (var itemEntidad in listado)
            {
                SolicitudCertificadoFisicoBO objetoBO = Mapper.Map<TSolicitudCertificadoFisico, SolicitudCertificadoFisicoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SolicitudCertificadoFisicoBO FirstById(int id)
        {
            try
            {
                TSolicitudCertificadoFisico entidad = base.FirstById(id);
                SolicitudCertificadoFisicoBO objetoBO = new SolicitudCertificadoFisicoBO();
                Mapper.Map<TSolicitudCertificadoFisico, SolicitudCertificadoFisicoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SolicitudCertificadoFisicoBO FirstBy(Expression<Func<TSolicitudCertificadoFisico, bool>> filter)
        {
            try
            {
                TSolicitudCertificadoFisico entidad = base.FirstBy(filter);
                SolicitudCertificadoFisicoBO objetoBO = Mapper.Map<TSolicitudCertificadoFisico, SolicitudCertificadoFisicoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SolicitudCertificadoFisicoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSolicitudCertificadoFisico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SolicitudCertificadoFisicoBO> listadoBO)
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

        public bool Update(SolicitudCertificadoFisicoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSolicitudCertificadoFisico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SolicitudCertificadoFisicoBO> listadoBO)
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
        private void AsignacionId(TSolicitudCertificadoFisico entidad, SolicitudCertificadoFisicoBO objetoBO)
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

        private TSolicitudCertificadoFisico MapeoEntidad(SolicitudCertificadoFisicoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSolicitudCertificadoFisico entidad = new TSolicitudCertificadoFisico();
                entidad = Mapper.Map<SolicitudCertificadoFisicoBO, TSolicitudCertificadoFisico>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SolicitudCertificadoFisicoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TSolicitudCertificadoFisico, bool>>> filters, Expression<Func<TSolicitudCertificadoFisico, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TSolicitudCertificadoFisico> listado = base.GetFiltered(filters, orderBy, ascending);
            List<SolicitudCertificadoFisicoBO> listadoBO = new List<SolicitudCertificadoFisicoBO>();

            foreach (var itemEntidad in listado)
            {
                SolicitudCertificadoFisicoBO objetoBO = Mapper.Map<TSolicitudCertificadoFisico, SolicitudCertificadoFisicoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
        /// <summary>
        /// Obtiene Codigo de seguimiento de CertificadosFisicos
        /// </summary>
        /// <returns></returns>
        public List<FiltroBasicoDTO> ObtenerCodigoSeguimientoFiltro(string CodigoSeguimientoEnvio)
        {
            try
            {
                List<FiltroBasicoDTO> rpta = new List<FiltroBasicoDTO>();
                string _query = "Select Id,Codigo From ope.V_ObtenerCodigoSeguimientoCertificado Where CodigoSeguimientoEnvio Like CONCAT('%',@CodigoSeguimientoEnvio,'%') and Estado=1 ";
                string query = _dapper.QueryDapper(_query, new { CodigoSeguimientoEnvio });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FiltroBasicoDTO>>(query);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ContenidoSolicitudCertificadoFisicoDTO ObtenerSolicitudesCertificadoFisico(filtroSolicitudCertificadoFisicoDTO filtro)
        {
            try
            {
                ContenidoSolicitudCertificadoFisicoDTO rpta = new ContenidoSolicitudCertificadoFisicoDTO();
                var filtros = new
                {
                    conFiltros = filtro.conFiltros,
                    IdPersonal = filtro.ListaCoordinador.Count() == 0 ? "0" : string.Join(",", filtro.ListaCoordinador.Select(x => x)),
                    CodigoSeguimiento = filtro.CodigoSeguimiento,
                    IdMatriculaCabecera = filtro.IdMatriculacabecera,
                    IdEstadoCertificadoFisico = filtro.IdEstadoCertificadoFisico,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    Skip = filtro.Skip,
                    Take = filtro.Take
                };

                var filtrosV2 = new
                {
                    conFiltros = filtro.conFiltros,
                    IdPersonal = filtro.ListaCoordinador.Count() == 0 ? "0" : string.Join(",", filtro.ListaCoordinador.Select(x => x)),
                    CodigoSeguimiento = filtro.CodigoSeguimiento,
                    IdMatriculaCabecera = filtro.IdMatriculacabecera,
                    IdEstadoCertificadoFisico = filtro.IdEstadoCertificadoFisico,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    Skip = filtro.Skip,
                    Take = filtro.Take                    
                };


                var query = "ope.SP_SolicitudCertificadoFisico";
                var res = _dapper.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    rpta.ListaSolicitusCertificado = JsonConvert.DeserializeObject<List<DataSolicitudCertificadoFisicoDTO>>(res);
                    
                }

                var queryCantidad = "ope.SP_SolicitudCertificadoFisicoCantidad";
                var resCantidad = _dapper.QuerySPFirstOrDefault(queryCantidad, filtrosV2);
                if (!string.IsNullOrEmpty(resCantidad) && !resCantidad.Contains("null"))
                {
                    var cantidad = JsonConvert.DeserializeObject<ValorIntDTO>(resCantidad);
                    rpta.Total = cantidad.Valor;

                }

                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DatosRegistroEnvioFisico DatosRegistroEnvioFisico(int IdSolicitudCertificadoFisico)
        {
            try
            {
                DatosRegistroEnvioFisico rpta = new DatosRegistroEnvioFisico();
                string _query = "Select * From [ope].[V_ObtenerRegistroEnvioFisico] Where IdSolicitudCertificadoFisico = @IdSolicitudCertificadoFisico ";
                string query = _dapper.FirstOrDefault(_query, new { IdSolicitudCertificadoFisico });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<DatosRegistroEnvioFisico>(query);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SolicitudCertificadoFisicoDTO> DatosReporteCertificadoEnvioFisico()
        {
            try
            {
                List<SolicitudCertificadoFisicoDTO> rpta= new List<SolicitudCertificadoFisicoDTO>();
                string _query = "Select * From [mkt].[V_ReporteSolicitudCertificadoFisico]";
                string query = _dapper.FirstOrDefault(_query, null);
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudCertificadoFisicoDTO>>(query);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 03/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del reporte de Certificado Físico según el CodigoMatricula
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<DatosReporteEnvioCertificadoFisicoDTO> DatosReporteCertificadoEnvioFisicoPorId(string CodigoMatricula)
        {
            try
            {
                List <DatosReporteEnvioCertificadoFisicoDTO> rpta = new List<DatosReporteEnvioCertificadoFisicoDTO>();
                string query = "SELECT * FROM mkt.V_ReporteSolicitudCertificadoFisico WHERE CodigoMatricula='" + CodigoMatricula + "'";
                var datosCertificadoEnvioFisico = _dapper.QueryDapper(query,null);
                if (!string.IsNullOrEmpty(datosCertificadoEnvioFisico) && !datosCertificadoEnvioFisico.Contains("[]") && datosCertificadoEnvioFisico != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<DatosReporteEnvioCertificadoFisicoDTO>>(datosCertificadoEnvioFisico);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Miguel Mora
        /// Fecha: 13/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nombre del courier de una solicitud
        /// </summary>
        /// <returns>L></returns>
        public string ObtenerCourierPorNombre(int IdSolicitudCertificado)
        {
            try
            {
                CourierIdDTO courier = new CourierIdDTO();
                var queryText = string.Empty;
                queryText = "select c.Id,c.Nombre from mkt.T_SolicitudCertificadoFisico sol "+
                "INNER JOIN pla.T_Courier c on c.Id = sol.IdCourier WHERE sol.id="+ IdSolicitudCertificado;
                var rpta = _dapper.FirstOrDefault(queryText, null);

                if (!string.IsNullOrEmpty(rpta) && !rpta.Contains("[]") && rpta != "null")
                {
                    courier = JsonConvert.DeserializeObject<CourierIdDTO>(rpta);
                }
                else
                {
                    return null;
                }

                return courier.Nombre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
