using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ComprobantePagoOportunidadRepositorio : BaseRepository<TComprobantePagoOportunidad, ComprobantePagoOportunidadBO>
    {
        #region Metodos Base
        public ComprobantePagoOportunidadRepositorio() : base()
        {
        }
        public ComprobantePagoOportunidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ComprobantePagoOportunidadBO> GetBy(Expression<Func<TComprobantePagoOportunidad, bool>> filter)
        {
            IEnumerable<TComprobantePagoOportunidad> listado = base.GetBy(filter);
            List<ComprobantePagoOportunidadBO> listadoBO = new List<ComprobantePagoOportunidadBO>();
            foreach (var itemEntidad in listado)
            {
                ComprobantePagoOportunidadBO objetoBO = Mapper.Map<TComprobantePagoOportunidad, ComprobantePagoOportunidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ComprobantePagoOportunidadBO FirstById(int id)
        {
            try
            {
                TComprobantePagoOportunidad entidad = base.FirstById(id);
                ComprobantePagoOportunidadBO objetoBO = new ComprobantePagoOportunidadBO();
                Mapper.Map<TComprobantePagoOportunidad, ComprobantePagoOportunidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ComprobantePagoOportunidadBO FirstBy(Expression<Func<TComprobantePagoOportunidad, bool>> filter)
        {
            try
            {
                TComprobantePagoOportunidad entidad = base.FirstBy(filter);
                ComprobantePagoOportunidadBO objetoBO = Mapper.Map<TComprobantePagoOportunidad, ComprobantePagoOportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ComprobantePagoOportunidadDTO> FirstByIdContacto(int codigoAlumno)
        {
            try
            {
                List<ComprobantePagoOportunidadDTO> objeto = new List<ComprobantePagoOportunidadDTO>();
                var _query = "SELECT IdContacto, Nombres,Apellidos, Celular, Dni, Correo, NombrePais, IdPais, NombreCiudad, TipoComprobante, NroDocumento, NombreRazonSocial, Direccion, BitComprobante, IdOcurrencia, IdAsesor, IdOportunidad, Comentario, UsuarioCreacion,FechaCreacion " +
                             "FROM [fin].[T_ComprobantePagoOportunidad] where IdContacto = @codigoAlumno";
                var dataDB = _dapper.QueryDapper(_query, new {codigoAlumno});
                if (!string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]"))
                {
                    objeto = JsonConvert.DeserializeObject<List<ComprobantePagoOportunidadDTO>>(dataDB);
                }
                return objeto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ComprobantePagoAlumnoDTO> ObtenerReporteComprobanteAlumno(filtroReporteComprobanteDTO filtro)
        {
            try
            {
                PeriodoRepositorio repPeriodo = new PeriodoRepositorio();
                var FechaInicio = new DateTime();
                var FechaFin = new DateTime();
                if (filtro.IdPeriodo == null)
                {
                    FechaInicio = new DateTime(1999, 1, 1, 0, 0, 0);
                    FechaFin = new DateTime(2999, 1, 1, 0, 0, 0);
                }
                else
                {
                    var FechaInicioPeriodo = repPeriodo.ObtenerFechaInicialNulo(filtro.IdPeriodo);
                    var FechaFinalPeriodo = repPeriodo.ObtenerFechaFinalNulo(filtro.IdPeriodo);
                    var IdPeriodo = filtro.IdPeriodo;
                    FechaInicio = new DateTime(FechaInicioPeriodo.Year, FechaInicioPeriodo.Month, FechaInicioPeriodo.Day, 0, 0, 0);
                    FechaFin = new DateTime(FechaFinalPeriodo.Year, FechaFinalPeriodo.Month, FechaFinalPeriodo.Day, 23, 59, 59);
                }
                if (filtro.IdFormaPago == "") { filtro.IdFormaPago = "_"; }
                if (filtro.CodigoMatricula == "") { filtro.CodigoMatricula = "_"; }
                if (filtro.Alumno == "") { filtro.Alumno = "_"; }
                if (filtro.Programa == "") { filtro.Programa = "_"; }
                if (filtro.Comprobante == "") { filtro.Comprobante = "_"; }
                List<ComprobantePagoAlumnoDTO> items = new List<ComprobantePagoAlumnoDTO>();

                var query = _dapper.QuerySPDapper("[fin].[SP_ComprobantePagoAlumno]", new { filtro.IdFormaPago, filtro.CodigoMatricula, filtro.Alumno, filtro.Programa, filtro.Comprobante, FechaInicio, FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComprobantePagoAlumnoDTO>>(query);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(ComprobantePagoOportunidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TComprobantePagoOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ComprobantePagoOportunidadBO> listadoBO)
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

        public bool Update(ComprobantePagoOportunidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TComprobantePagoOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ComprobantePagoOportunidadBO> listadoBO)
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
        private void AsignacionId(TComprobantePagoOportunidad entidad, ComprobantePagoOportunidadBO objetoBO)
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

        private TComprobantePagoOportunidad MapeoEntidad(ComprobantePagoOportunidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TComprobantePagoOportunidad entidad = new TComprobantePagoOportunidad();
                entidad = Mapper.Map<ComprobantePagoOportunidadBO, TComprobantePagoOportunidad>(objetoBO,
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
    }
}
