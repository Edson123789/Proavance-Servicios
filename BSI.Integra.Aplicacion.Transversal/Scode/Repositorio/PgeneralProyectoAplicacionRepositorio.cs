using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.DTOs.DTOs.Comercial;
using BSI.Integra.Aplicacion.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PgeneralProyectoAplicacionRepositorio : BaseRepository<TPgeneralProyectoAplicacion, PgeneralProyectoAplicacionBO>
    {
        #region Metodos Base
        public PgeneralProyectoAplicacionRepositorio() : base()
        {
        }
        public PgeneralProyectoAplicacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralProyectoAplicacionBO> GetBy(Expression<Func<TPgeneralProyectoAplicacion, bool>> filter)
        {
            IEnumerable<TPgeneralProyectoAplicacion> listado = base.GetBy(filter).ToList();
            List<PgeneralProyectoAplicacionBO> listadoBO = new List<PgeneralProyectoAplicacionBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralProyectoAplicacionBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacion, PgeneralProyectoAplicacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralProyectoAplicacionBO FirstById(int id)
        {
            try
            {
                TPgeneralProyectoAplicacion entidad = base.FirstById(id);
                PgeneralProyectoAplicacionBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacion, PgeneralProyectoAplicacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralProyectoAplicacionBO FirstBy(Expression<Func<TPgeneralProyectoAplicacion, bool>> filter)
        {
            try
            {
                TPgeneralProyectoAplicacion entidad = base.FirstBy(filter);
                PgeneralProyectoAplicacionBO objetoBO = Mapper.Map<TPgeneralProyectoAplicacion, PgeneralProyectoAplicacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralProyectoAplicacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralProyectoAplicacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralProyectoAplicacionBO> listadoBO)
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

        public bool Update(PgeneralProyectoAplicacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralProyectoAplicacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralProyectoAplicacionBO> listadoBO)
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
        private void AsignacionId(TPgeneralProyectoAplicacion entidad, PgeneralProyectoAplicacionBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                    objetoBO.RowVersion = entidad.RowVersion;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TPgeneralProyectoAplicacion MapeoEntidad(PgeneralProyectoAplicacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralProyectoAplicacion entidad = new TPgeneralProyectoAplicacion();
                entidad = Mapper.Map<PgeneralProyectoAplicacionBO, TPgeneralProyectoAplicacion>(objetoBO,
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
        public List<PgeneralProyectoAplicacionDTO> ObtenerPgeneralProyectoAplicacion(int IdPgeneral)
        {
            List<PgeneralProyectoAplicacionDTO> rpta = new List<PgeneralProyectoAplicacionDTO>();
            string _queryCodigoPartner = "Select Id from pla.V_ObtenerPgeneralProyectoAplicacion Where Estado=1 and IdPgeneral=@IdPgeneral";
            string queryCodigoPartner = _dapper.QueryDapper(_queryCodigoPartner, new { IdPgeneral });
            if (!string.IsNullOrEmpty(queryCodigoPartner) && !queryCodigoPartner.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<PgeneralProyectoAplicacionDTO>>(queryCodigoPartner);
                foreach (var item in rpta)
                {
                    item.IdModalidadCurso = new List<PgeneralProyectoAplicacionModalidadDTO>();
                    item.IdProveedor = new List<PgeneralProyectoAplicacionProveedorDTO>();

                    string _queryModalidad = "Select IdModalidadCurso as Id ,IdModalidadCurso From pla.T_PgeneralProyectoAplicacionModalidad Where Estado=1 and IdPgeneralProyectoAplicacion=@Id";
                    string queryModalidad = _dapper.QueryDapper(_queryModalidad, new { item.Id });
                    if (!string.IsNullOrEmpty(queryModalidad) && !queryModalidad.Contains("[]"))
                    {
                        item.IdModalidadCurso = JsonConvert.DeserializeObject<List<PgeneralProyectoAplicacionModalidadDTO>>(queryModalidad);
                    }

                    string _queryProveedor = "Select IdProveedor as Id,IdProveedor From pla.T_PgeneralProyectoAplicacionProveedor Where Estado=1 and IdPgeneralProyectoAplicacion=@Id";
                    string queryProveedor = _dapper.QueryDapper(_queryProveedor, new { item.Id });
                    if (!string.IsNullOrEmpty(queryProveedor) && !queryProveedor.Contains("[]"))
                    {
                        item.IdProveedor = JsonConvert.DeserializeObject<List<PgeneralProyectoAplicacionProveedorDTO>>(queryProveedor);
                    }

                }
            }
            return rpta;
        }
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<PgeneralProyectoAplicacionDTO> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_PgeneralProyectoAplicacion WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
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

        public List<PEspecificoAsignacionCalificacionProyectoAplicacionDTO> ObtenerPEspecificoAplicaProyectoAplicacionPorProveedor(int idProveedor)
        {
            try
            {
                List<PEspecificoAsignacionCalificacionProyectoAplicacionDTO> listado = new List<PEspecificoAsignacionCalificacionProyectoAplicacionDTO>();
                string _query = "SELECT IdPEspecifico, PEspecifico, IdProveedor FROM pla.V_ObtenerConfiguracion_AsignacionProyectoAplicacion WHERE IdProveedor = @idProveedor ORDER BY IdPEspecifico DESC";
                var query = _dapper.QueryDapper(_query, new { idProveedor });
                listado = JsonConvert.DeserializeObject<List<PEspecificoAsignacionCalificacionProyectoAplicacionDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<AlumnoEnvioProyectoAplicacionDTO> ListadoAlumnoPendienteCalificacion(int idPespecifico)
        {
            try
            {
                List<AlumnoEnvioProyectoAplicacionDTO> listado = new List<AlumnoEnvioProyectoAplicacionDTO>();
                string _query =
                    "SELECT IdPgeneralProyectoAplicacionEnvio, IdPgeneralProyectoAplicacion, Alumno, RutaArchivo, FechaEnvio, FechaCalificacion, Nota, Comentarios FROM pla.V_Obtener_ListadoProyectoAplicacion_UltimoEnviado WHERE IdPespecifico = @idPespecifico ORDER BY Alumno";
                var query = _dapper.QueryDapper(_query, new {idPespecifico = idPespecifico});
                listado = JsonConvert.DeserializeObject<List<AlumnoEnvioProyectoAplicacionDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<AlumnoEnvioProyectoAplicacionDTO> ListadoAlumnoHistorico(int idMatriculaCabecera)
        {
            try
            {
                List<AlumnoEnvioProyectoAplicacionDTO> listado = new List<AlumnoEnvioProyectoAplicacionDTO>();
                string _query =
                    "SELECT IdPgeneralProyectoAplicacion, Alumno, NombreArchivo, RutaArchivo, FechaEnvio, FechaCalificacion, Nota, Comentarios, IdPEspecifico, PEspecifico, NombreArchivoRetroalimentacion, RutaArchivoRetroalimentacion, EsEntregable FROM pla.V_Obtener_ListadoProyectoAplicacion_HistoricoEnviado WHERE IdMatriculaCabecera = @idMatriculaCabecera ORDER BY FechaEnvio DESC";
                var query = _dapper.QueryDapper(_query, new {IdMatriculaCabecera = idMatriculaCabecera});
                listado = JsonConvert.DeserializeObject<List<AlumnoEnvioProyectoAplicacionDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ProyectoAplicacionConsolidadoListadoDTO> ListadoConsolidado_ProyectosRegistrados(int idClasificacionPersona)
        {
            try
            {
                List<ProyectoAplicacionConsolidadoListadoDTO> listado = new List<ProyectoAplicacionConsolidadoListadoDTO>();
                ClasificacionPersonaRepositorio rep = new ClasificacionPersonaRepositorio();
                var bo = rep.FirstById(idClasificacionPersona);
                var idProveedor = bo.IdTablaOriginal;

                var query = "pla.SP_Obtener_HistoricoProyectoAplicacionPorProveedor";
                var res = _dapper.QuerySPDapper(query,
                    new
                    {
                        idProveedor = idProveedor
                    });
                listado = JsonConvert.DeserializeObject<List<ProyectoAplicacionConsolidadoListadoDTO>>(res);

                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
