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
    public class CertificadoEnvioRepositorio : BaseRepository<TCertificadoEnvio, CertificadoEnvioBO>
    {
        #region Metodos Base
        public CertificadoEnvioRepositorio() : base()
        {
        }
        public CertificadoEnvioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CertificadoEnvioBO> GetBy(Expression<Func<TCertificadoEnvio, bool>> filter)
        {
            IEnumerable<TCertificadoEnvio> listado = base.GetBy(filter);
            List<CertificadoEnvioBO> listadoBO = new List<CertificadoEnvioBO>();
            foreach (var itemEntidad in listado)
            {
                CertificadoEnvioBO objetoBO = Mapper.Map<TCertificadoEnvio, CertificadoEnvioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CertificadoEnvioBO FirstById(int id)
        {
            try
            {
                TCertificadoEnvio entidad = base.FirstById(id);
                CertificadoEnvioBO objetoBO = new CertificadoEnvioBO();
                Mapper.Map<TCertificadoEnvio, CertificadoEnvioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CertificadoEnvioBO FirstBy(Expression<Func<TCertificadoEnvio, bool>> filter)
        {
            try
            {
                TCertificadoEnvio entidad = base.FirstBy(filter);
                CertificadoEnvioBO objetoBO = Mapper.Map<TCertificadoEnvio, CertificadoEnvioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CertificadoEnvioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCertificadoEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CertificadoEnvioBO> listadoBO)
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

        public bool Update(CertificadoEnvioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCertificadoEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CertificadoEnvioBO> listadoBO)
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
        private void AsignacionId(TCertificadoEnvio entidad, CertificadoEnvioBO objetoBO)
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

        private TCertificadoEnvio MapeoEntidad(CertificadoEnvioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCertificadoEnvio entidad = new TCertificadoEnvio();
                entidad = Mapper.Map<CertificadoEnvioBO, TCertificadoEnvio>(objetoBO,
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

        public List<CertificadoEnvioDTO> ObtenerEnvioFisico(int IdCertificadoDetalle)
        {
            try
            {
                List<CertificadoEnvioDTO> _certificadoEnvio = new List<CertificadoEnvioDTO>();
                var _query = string.Empty;
                _query = "Select Id,IdCertificadoDetalle,IdCertificadoFormaEntrega,NombreCertificadoFormaEntrega,FechaEnvio,FechaRecepcion,CodigoSeguimiento,Observacion FROM ope.V_ObtenerCertificadoEnvioFisico WHERE IdCertificadoDetalle = @IdCertificadoDetalle";

                var CertificadoSolicitud = _dapper.QueryDapper(_query, new { IdCertificadoDetalle });

                if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                {
                    _certificadoEnvio = JsonConvert.DeserializeObject<List<CertificadoEnvioDTO>>(CertificadoSolicitud);
                }

                return _certificadoEnvio;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public object ObtenerTodoEnvioFisico(Paginador paginador, GridFilters filtroGrilla)
        {
            try
            {
                string Condicion = "";
                string NombreCentroCosto = "";
                string NombreAlumno = "";
                string CodigoMatricula = "";
                
                if (filtroGrilla != null)
                {

                    foreach (var item in filtroGrilla.Filters)
                    {
                        if (item.Field == "NombreCentroCosto" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and NombreCentroCosto like @NombreCentroCosto ";
                            NombreCentroCosto = item.Value;
                        }
                        if (item.Field == "CodigoMatricula" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and CodigoMatricula like @CodigoMatricula ";
                            CodigoMatricula = item.Value;
                        }
                        if (item.Field == "NombreAlumno" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and NombreAlumno like @NombreAlumno ";
                            NombreAlumno = item.Value;
                        }
                    }
                }

                List<TodoCertificadoEnvioDTO> _certificadoEnvio = new List<TodoCertificadoEnvioDTO>();
                var _query = string.Empty;
                if (paginador != null && paginador.take != 0)
                {
                    _query = "Select Id,CodigoMatricula,CodigoCertificado,NumeroSolicitud,NombreAlumno,NombreCentroCosto,NombreCertificadoTipo," +
                    "NombreCertificadoFormaEntrega,FechaEmision,AplicaPartner,IdCertificadoDetalle,IdCertificadoBrochure,IdCertificadoFormaEntrega," +
                    "FechaEnvio,FechaRecepcion,CodigoSeguimiento,Observacion,IdCertificadoFormaEntrega_Partner,FechaEnvio_Partner," +
                    "FechaRecepcion_Partner,CodigoSeguimiento_Partner,Observaciones_Partner,Estado,FechaEnvioDigital" +
                    " FROM ope.V_ObtenerTodoCertificadoEnvioFisico WHERE Estado=1 and FechaEmision Is not null " + Condicion +  " order by FechaEmision desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                    var CertificadoSolicitud = _dapper.QueryDapper(_query, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take });

                    if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                    {
                        _certificadoEnvio = JsonConvert.DeserializeObject<List<TodoCertificadoEnvioDTO>>(CertificadoSolicitud);
                        var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM ope.V_ObtenerTodoCertificadoEnvioFisico WHERE Estado=1 and FechaEmision Is not null " + Condicion, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take }));

                        return new { data = _certificadoEnvio, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                    }

                }
                else
                {
                    _query = "Select Id,CodigoMatricula,CodigoCertificado,NumeroSolicitud,NombreAlumno,NombreCentroCosto,NombreCertificadoTipo," +
                     "NombreCertificadoFormaEntrega,FechaEmision,AplicaPartner,IdCertificadoBrochure,IdCertificadoFormaEntrega," +
                     "FechaEnvio,FechaRecepcion,CodigoSeguimiento,Observacion,IdCertificadoFormaEntrega_Partner,FechaEnvio_Partner," +
                     "FechaRecepcion_Partner,CodigoSeguimiento_Partner,Observaciones_Partner,Estado,FechaEnvioDigital" +
                     " FROM ope.V_ObtenerTodoCertificadoEnvioFisico WHERE Estado=1 and FechaEmision Is not null " + Condicion ;

                    var CertificadoSolicitud = _dapper.QueryDapper(_query, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take });

                    if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                    {
                        _certificadoEnvio = JsonConvert.DeserializeObject<List<TodoCertificadoEnvioDTO>>(CertificadoSolicitud);
                        var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM ope.V_ObtenerTodoCertificadoEnvioFisico WHERE Estado=1 and FechaEmision Is not null " + Condicion, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take }));

                        return new { data = _certificadoEnvio, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                    }
                }
                return new { data = _certificadoEnvio, Total = 0 };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public object ObtenerPendienteEnvio(Paginador paginador, GridFilters filtroGrilla)
        {
            try
            {
                string Condicion = "";
                string NombreCentroCosto = "";
                string NombreAlumno = "";
                string CodigoMatricula = "";
                
                if (filtroGrilla != null)
                {

                    foreach (var item in filtroGrilla.Filters)
                    {
                        if (item.Field == "NombreCentroCosto" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and NombreCentroCosto like @NombreCentroCosto ";
                            NombreCentroCosto = item.Value;
                        }
                        if (item.Field == "CodigoMatricula" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and CodigoMatricula like @CodigoMatricula ";
                            CodigoMatricula = item.Value;
                        }
                        if (item.Field == "NombreAlumno" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and NombreAlumno like @NombreAlumno ";
                            NombreAlumno = item.Value;
                        }
                    }
                }

                List<TodoCertificadoEnvioDTO> _certificadoEnvio = new List<TodoCertificadoEnvioDTO>();
                var _query = string.Empty;
                if (paginador != null && paginador.take != 0)
                {
                    _query = "Select Id,CodigoMatricula,CodigoCertificado,NumeroSolicitud,NombreAlumno,NombreCentroCosto,NombreCertificadoTipo,NumeroSolicitud, " +
                    "FechaEmision,AplicaPartner,IdCertificadoDetalle,IdCertificadoBrochure,isnull(IdCertificadoFormaEntrega,0) AS IdCertificadoFormaEntrega," +
                    "FechaEnvio,FechaRecepcion,CodigoSeguimiento,Observacion,IdCertificadoFormaEntrega_Partner,FechaEnvio_Partner," +
                    "FechaRecepcion_Partner,CodigoSeguimiento_Partner,Observaciones_Partner,Estado" +
                    " FROM ope.V_ObtenerCertificadoPendienteEnvio WHERE Estado=1 and FechaEmision is not null " + Condicion + " order by FechaEmision desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                    var CertificadoSolicitud = _dapper.QueryDapper(_query, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take });

                    if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                    {
                        _certificadoEnvio = JsonConvert.DeserializeObject<List<TodoCertificadoEnvioDTO>>(CertificadoSolicitud);
                        var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM ope.V_ObtenerTodoCertificadoEnvioFisico WHERE Estado=1 " + Condicion, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take }));

                        return new { data = _certificadoEnvio, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                    }

                }
                else
                {
                    _query = "Select Id,CodigoMatricula,CodigoCertificado,NumeroSolicitud,NombreAlumno,NombreCentroCosto,NombreCertificadoTipo," +
                     "NombreCertificadoFormaEntrega,FechaEmision,AplicaPartner,IdCertificadoBrochure,isnull(IdCertificadoFormaEntrega,0) AS IdCertificadoFormaEntrega," +
                     "FechaEnvio,FechaRecepcion,CodigoSeguimiento,Observacion,IdCertificadoFormaEntrega_Partner,FechaEnvio_Partner," +
                     "FechaRecepcion_Partner,CodigoSeguimiento_Partner,Observaciones_Partner,Estado,FechaEnvioDigita" +
                     " FROM ope.V_ObtenerCertificadoPendienteEnvio WHERE Estado=1 and FechaEmision is not null " + Condicion ;

                    var CertificadoSolicitud = _dapper.QueryDapper(_query, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take });

                    if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                    {
                        _certificadoEnvio = JsonConvert.DeserializeObject<List<TodoCertificadoEnvioDTO>>(CertificadoSolicitud);
                        var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM ope.V_ObtenerTodoCertificadoEnvioFisico WHERE Estado=1 " + Condicion, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take }));

                        return new { data = _certificadoEnvio, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                    }
                }
                return new { data = _certificadoEnvio, Total = 0 };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public object ObtenerPendienteEntrega(Paginador paginador, GridFilters filtroGrilla)
        {
            try
            {
                string Condicion = "";
                string NombreCentroCosto = "";
                string NombreAlumno = "";
                string CodigoMatricula = "";
                
                if (filtroGrilla != null)
                {

                    foreach (var item in filtroGrilla.Filters)
                    {
                        if (item.Field == "NombreCentroCosto" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and NombreCentroCosto like @NombreCentroCosto ";
                            NombreCentroCosto = item.Value;
                        }
                        if (item.Field == "CodigoMatricula" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and CodigoMatricula like @CodigoMatricula ";
                            CodigoMatricula = item.Value;
                        }
                        if (item.Field == "NombreAlumno" && item.Value.Contains(""))
                        {
                            Condicion = Condicion + " and NombreAlumno like @NombreAlumno ";
                            NombreAlumno = item.Value;
                        }
                    }
                }

                List<TodoCertificadoEnvioDTO> _certificadoEnvio = new List<TodoCertificadoEnvioDTO>();
                var _query = string.Empty;
                if (paginador != null && paginador.take != 0)
                {
                    _query = "Select CodigoMatricula,NombreAlumno,NombreCentroCosto," +
                    "FechaEmision,FechaEnvio,FechaRecepcion,IdEstadoMatricula,UsuarioCoordinadorAcademico,FechaEnvio_Partner," +
                    "FechaRecepcion_Partner,Sistema " +
                    " FROM ope.IndicadorCertificadoSinFiltroRevisado WHERE Sistema is not null " + Condicion + " order by FechaEnvio desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                    var CertificadoSolicitud = _dapper.QueryDapper(_query, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take });

                    if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                    {
                        _certificadoEnvio = JsonConvert.DeserializeObject<List<TodoCertificadoEnvioDTO>>(CertificadoSolicitud);
                        var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM ope.V_ObtenerTodoCertificadoEnvioFisico WHERE CodigoMatricula is not null " + Condicion, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take }));

                        return new { data = _certificadoEnvio, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                    }

                }
                else
                {
                    _query = "Select CodigoMatricula,NombreAlumno,NombreCentroCosto," +
                    "FechaEmision,FechaEnvio,FechaRecepcion,IdEstadoMatricula,UsuarioCoordinadorAcademico,FechaEnvio_Partner," +
                    "FechaRecepcion_Partner,CodigoSeguimiento_Partner,Sistema " +
                    " FROM ope.IndicadorCertificadoSinFiltroRevisado WHERE Sistema is not null " + Condicion + " order by FechaEnvio desc";

                    var CertificadoSolicitud = _dapper.QueryDapper(_query, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take });

                    if (!string.IsNullOrEmpty(CertificadoSolicitud) && !CertificadoSolicitud.Contains("[]"))
                    {
                        _certificadoEnvio = JsonConvert.DeserializeObject<List<TodoCertificadoEnvioDTO>>(CertificadoSolicitud);
                        var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) FROM ope.V_ObtenerTodoCertificadoEnvioFisico WHERE CodigoMatricula is not null  " + Condicion, new { NombreCentroCosto = "%" + NombreCentroCosto + "%", CodigoMatricula = "%" + CodigoMatricula + "%", NombreAlumno = "%" + NombreAlumno + "%", Skip = paginador.skip, Take = paginador.take }));

                        return new { data = _certificadoEnvio, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                    }
                }
                return new { data = _certificadoEnvio, Total = 0 };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
