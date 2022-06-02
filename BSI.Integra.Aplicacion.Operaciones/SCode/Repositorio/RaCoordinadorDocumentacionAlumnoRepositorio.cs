using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaCoordinadorDocumentacionAlumnoRepositorio : BaseRepository<TRaCoordinadorDocumentacionAlumno, RaCoordinadorDocumentacionAlumnoBO>
    {
        #region Metodos Base
        public RaCoordinadorDocumentacionAlumnoRepositorio() : base()
        {
        }
        public RaCoordinadorDocumentacionAlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaCoordinadorDocumentacionAlumnoBO> GetBy(Expression<Func<TRaCoordinadorDocumentacionAlumno, bool>> filter)
        {
            IEnumerable<TRaCoordinadorDocumentacionAlumno> listado = base.GetBy(filter);
            List<RaCoordinadorDocumentacionAlumnoBO> listadoBO = new List<RaCoordinadorDocumentacionAlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                RaCoordinadorDocumentacionAlumnoBO objetoBO = Mapper.Map<TRaCoordinadorDocumentacionAlumno, RaCoordinadorDocumentacionAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaCoordinadorDocumentacionAlumnoBO FirstById(int id)
        {
            try
            {
                TRaCoordinadorDocumentacionAlumno entidad = base.FirstById(id);
                RaCoordinadorDocumentacionAlumnoBO objetoBO = new RaCoordinadorDocumentacionAlumnoBO();
                Mapper.Map<TRaCoordinadorDocumentacionAlumno, RaCoordinadorDocumentacionAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaCoordinadorDocumentacionAlumnoBO FirstBy(Expression<Func<TRaCoordinadorDocumentacionAlumno, bool>> filter)
        {
            try
            {
                TRaCoordinadorDocumentacionAlumno entidad = base.FirstBy(filter);
                RaCoordinadorDocumentacionAlumnoBO objetoBO = Mapper.Map<TRaCoordinadorDocumentacionAlumno, RaCoordinadorDocumentacionAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaCoordinadorDocumentacionAlumnoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaCoordinadorDocumentacionAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaCoordinadorDocumentacionAlumnoBO> listadoBO)
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

        public bool Update(RaCoordinadorDocumentacionAlumnoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaCoordinadorDocumentacionAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaCoordinadorDocumentacionAlumnoBO> listadoBO)
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
        private void AsignacionId(TRaCoordinadorDocumentacionAlumno entidad, RaCoordinadorDocumentacionAlumnoBO objetoBO)
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

        private TRaCoordinadorDocumentacionAlumno MapeoEntidad(RaCoordinadorDocumentacionAlumnoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaCoordinadorDocumentacionAlumno entidad = new TRaCoordinadorDocumentacionAlumno();
                entidad = Mapper.Map<RaCoordinadorDocumentacionAlumnoBO, TRaCoordinadorDocumentacionAlumno>(objetoBO,
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
        /// Obtiene un listado de alumnos sin matricula verificada por centro de costo
        /// </summary>
        /// <param name="nombreCentroCosto"></param>
        /// <returns></returns>
        public List<AlumnoPresencialSinMatriculaVerificadaDTO> ListadoAlumnosSinMatriculaVerificadaPorCentroCosto(string nombreCentroCosto)
        {
            try
            {
                List<AlumnoPresencialSinMatriculaVerificadaDTO> listadoAlumnosSinMatricular = new List<AlumnoPresencialSinMatriculaVerificadaDTO>();
                var query = "SELECT NombreCentroCosto, NombreAlumno, IdMatriculaCabecera, CodigoMatricula, UsuarioCoordinadorAcademico, EstadoMatricula,  IdEstadoMatricula, Genero, Email1, Email2, Nombre1, Nombre2 FROM ope.V_ObtenerAlumnoSinMatriculaVerificadaPorCentroCosto WHERE EstadoAlumno = 1 AND EstadoMatriculaCabecera = 1 AND EstadoPEspecifico = 1 AND IdEstadoPagoMatricula = @IdEstadoPagoMatriculaMatriculado AND NombreCentroCosto LIKE CONCAT('%',@nombreCentroCosto,'%')";
                var listadoAlumnosSinMatricularDB = _dapper.QueryDapper(query, new { ValorEstatico.IdEstadoPagoMatriculaMatriculado ,nombreCentroCosto });
                if (!string.IsNullOrEmpty(listadoAlumnosSinMatricularDB) && !listadoAlumnosSinMatricularDB.Contains("[]"))
                {
                    listadoAlumnosSinMatricular = JsonConvert.DeserializeObject<List<AlumnoPresencialSinMatriculaVerificadaDTO>>(listadoAlumnosSinMatricularDB);
                }
                return listadoAlumnosSinMatricular;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <returns></returns>
        public AlumnoPresencialSinMatriculaVerificadaDTO AlumnoSinMatriculaVerificadaPorCodigo(string codigoMatricula)
        {
            try
            {
                AlumnoPresencialSinMatriculaVerificadaDTO alumnoSinMatricular = new AlumnoPresencialSinMatriculaVerificadaDTO();
                var query = "SELECT NombreCentroCosto, NombreAlumno, IdMatriculaCabecera, CodigoMatricula, UsuarioCoordinadorAcademico, EstadoMatricula,  IdEstadoMatricula, Genero, Email1, Email2, Nombre1, Nombre2 FROM ope.V_ObtenerAlumnoSinMatriculaVerificadaPorCentroCosto WHERE EstadoAlumno = 1 AND EstadoMatriculaCabecera = 1 AND EstadoPEspecifico = 1 AND IdEstadoPagoMatricula = @IdEstadoPagoMatriculaMatriculado AND CodigoMatricula = @codigoMatricula";
                var alumnoSinMatricularDB = _dapper.FirstOrDefault(query, new { ValorEstatico.IdEstadoPagoMatriculaMatriculado, codigoMatricula });
                if (!string.IsNullOrEmpty(alumnoSinMatricularDB))
                {
                    alumnoSinMatricular = JsonConvert.DeserializeObject<AlumnoPresencialSinMatriculaVerificadaDTO>(alumnoSinMatricularDB);
                }
                return alumnoSinMatricular;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public FormatoPagareDTO ObtenerModeloFormatoPagare(AlumnoPresencialSinMatriculaVerificadaDTO alumno)
        {
            //obtener la direccion de la sede
            string ciudad = "AQP";
            if (alumno.NombreCentroCosto.Contains("LIMA"))
            {
                ciudad = "LIMA";
            }
            else if (alumno.NombreCentroCosto.Contains("BOGOTA")) {
                ciudad = "BOGOTA";
            }
            var empresa = this.ObtenerEmpresaPorCiudad(ciudad);

            //obtener informacion del alumno
            var alumno_integra = this.ObtenerAlumnoPorCodigo(alumno.CodigoMatricula);

            FormatoPagareDTO formato = new FormatoPagareDTO()
            {
                RazonSocial = empresa.RazonSocial,
                NumeroIdentificacionContribuyente = empresa.NumeroIdentificacionContribuyente,
                TipoIdentificacionContribuyente = empresa.TipoIdentificacionContribuyente,
                DireccionOficinas = empresa.DireccionOficina,
                NombreAlumno = alumno.NombreAlumno,
                Direccion = alumno_integra.Direccion,
                NroDocumento = alumno_integra.DNI,
                Celular = alumno_integra.Celular
            };
            return formato;
        }

        /// <summary>
        /// Obtiene los datos de una empresa por ciudad
        /// </summary>
        /// <param name="ciudad"></param>
        /// <returns></returns>
        public EmpresaDetalleDTO ObtenerEmpresaPorCiudad(string ciudad)
        {
            try
            {
                EmpresaDetalleDTO empresa = new EmpresaDetalleDTO();
                var query = "SELECT RazonSocial, NumeroIdentificacionContribuyente, TipoIdentificacionContribuyente, DireccionOficina FROM ope.T_ObtenerDatosEmpresaPorCiudad WHERE EstadoSede = 1 AND EstadoRazonSocial = 1 AND AplicaDocumentacion = 1 AND Ciudad = @ciudad";
                var empresaDB = _dapper.FirstOrDefault(query, new { ciudad });
                if (!string.IsNullOrEmpty(empresaDB))
                {
                    empresa = JsonConvert.DeserializeObject<EmpresaDetalleDTO>(empresaDB);
                }
                return empresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de un alumno por codigo matricula
        /// </summary>
        /// <param name="codigoMatricula"></param>
        /// <returns></returns>
        public AlumnoDetalleDTO ObtenerAlumnoPorCodigo(string codigoMatricula)
        {
            try
            {
                AlumnoDetalleDTO alumno = new AlumnoDetalleDTO();
                var query = "SELECT Direccion, DNI, Celular FROM ope.V_ObtenerAlumnoPorCodigoMatricula WHERE EstadoAlumno = 1 AND EstadoMatriculaCabecera = 1 AND CodigoMatricula = @codigoMatricula";
                var alumnoDB = _dapper.FirstOrDefault(query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(alumnoDB))
                {
                    alumno = JsonConvert.DeserializeObject<AlumnoDetalleDTO>(alumnoDB);
                }
                return alumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<TotalCuotaDTO> ListadoCuotasSinMatriculaVerificadaPorCodigoAlumno(string codigoMatricula)
        {
            return null;
        }
    }
}
