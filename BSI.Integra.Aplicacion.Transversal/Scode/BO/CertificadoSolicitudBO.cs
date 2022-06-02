using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System.Linq;
using System.Linq.Expressions;


namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CertificadoSolicitudBO : BaseBO
    {
        public string NombreBeneficioDocumento { get; set; }
        public int NumeroSolicitud { get; set; }
        public int? IdMigracion { get; set; }

        CertificadoSolicitudRepositorio _repCertificadoSolicitud;
        CertificadoDetalleRepositorio _repCertificadoSolicitudDetalle;
        PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadrePespecificoHijo;
        MatriculaCabeceraRepositorio _repMatriculaCabecera;
        public  CertificadoSolicitudBO(integraDBContext _integraDBContext)
        {
            _repCertificadoSolicitud = new CertificadoSolicitudRepositorio(_integraDBContext);
            _repCertificadoSolicitudDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            _repPespecificoPadrePespecificoHijo = new PespecificoPadrePespecificoHijoRepositorio(_integraDBContext);
            _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
        }
        public  CertificadoSolicitudBO()
        { 
            
        }

        public List<DatosAlumnoMatriculaDTO> ObtenerAlumnosSinSolicitud(string CodigoMatricula)
        {
            List<DatosAlumnoMatriculaDTO> resultado = new List<DatosAlumnoMatriculaDTO>();
            var datosAlumnoMatricula = _repMatriculaCabecera.ObtenerInformacionAlumnoPorCodigoMatricula(CodigoMatricula);
            if (datosAlumnoMatricula == null)
            {
                return resultado; 
            }
            var programasHijos = _repPespecificoPadrePespecificoHijo.ObtenerProgramasHijos(CodigoMatricula);
            var Solicititado = _repCertificadoSolicitudDetalle.GetBy(w => w.IdMatriculaCabecera == datosAlumnoMatricula.Id, y => new { y.IdPespecifico,y.AplicaPartner,y.FechaInicio,y.FechaTermino }).ToList();

            if (programasHijos.Count() > 0)
            {
                if (Solicititado.Exists(w => w.IdPespecifico == datosAlumnoMatricula.IdPespecifico))
                {
                    var solicitud = Solicititado.Where(w => w.IdPespecifico == datosAlumnoMatricula.IdPespecifico).FirstOrDefault();
                    datosAlumnoMatricula.Modulo = false;
                    datosAlumnoMatricula.Solicitado = true;
                    //datosAlumnoMatricula.SolicitadoPartner = solicitud.AplicaPartner;
                    datosAlumnoMatricula.FechaInicio = solicitud.FechaInicio;
                    datosAlumnoMatricula.FechaFin = solicitud.FechaTermino;
                }
                else
                {
                    if (string.IsNullOrEmpty(datosAlumnoMatricula.Genero) || (datosAlumnoMatricula.Genero != "M" && datosAlumnoMatricula.Genero != "F"))
                    {
                        datosAlumnoMatricula.MensajeCondicion = "El Alumno no Tiene Actualizado el Genero";
                    }
                    if (datosAlumnoMatricula.IdEstadoMatricula != 5 && datosAlumnoMatricula.IdEstadoMatricula != 7)
                    {
                        datosAlumnoMatricula.MensajeCondicion = "Revisar si el alumno Culmino";
                    }
                    datosAlumnoMatricula.Modulo = false;
                    datosAlumnoMatricula.Solicitado = false;
                    //datosAlumnoMatricula.SolicitadoPartner = false;
                    if (datosAlumnoMatricula.MensajeCondicion == null)
                    {
                        datosAlumnoMatricula.FechaInicio = CalcularFechaInicioAonline(datosAlumnoMatricula.CodigoMatricula);
                        datosAlumnoMatricula.FechaFin = CalcularFechaTerminoAonline(datosAlumnoMatricula.CodigoMatricula);
                        datosAlumnoMatricula.EscalaCalificacion = "100";
                    }
                    resultado.Add(datosAlumnoMatricula);
                }
                

                foreach (var item in programasHijos)
                {
                    DatosAlumnoMatriculaDTO alumnoTemporal = new DatosAlumnoMatriculaDTO();
                    if (Solicititado.Exists(w => w.IdPespecifico == item.IdPespecificoHijo))
                    {
                        var solicitud = Solicititado.Where(w => w.IdPespecifico == datosAlumnoMatricula.IdPespecifico).FirstOrDefault();

                        alumnoTemporal.Id = datosAlumnoMatricula.Id;
                        alumnoTemporal.CodigoMatricula = datosAlumnoMatricula.CodigoMatricula;
                        alumnoTemporal.IdPespecifico = item.IdPespecificoHijo;
                        alumnoTemporal.NombreAlumno = datosAlumnoMatricula.NombreAlumno;
                        alumnoTemporal.Genero = datosAlumnoMatricula.Genero;
                        alumnoTemporal.IdCentroCosto = item.IdCentroCosto;
                        alumnoTemporal.NombreCentroCosto = item.NombreCentroCosto;
                        alumnoTemporal.IdEstadoMatricula = datosAlumnoMatricula.IdEstadoMatricula;
                        alumnoTemporal.NombreEstadoMatricula = datosAlumnoMatricula.NombreEstadoMatricula;
                        alumnoTemporal.Modulo = true;
                        alumnoTemporal.Solicitado = true;
                        //alumnoTemporal.SolicitadoPartner = solicitud.AplicaPartner;
                        alumnoTemporal.FechaInicio = solicitud.FechaInicio;
                        alumnoTemporal.FechaFin = solicitud.FechaTermino;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(datosAlumnoMatricula.Genero) || (datosAlumnoMatricula.Genero != "M" && datosAlumnoMatricula.Genero != "F"))
                        {
                            alumnoTemporal.MensajeCondicion = "El Alumno no Tiene Actualizado el Genero";
                        }
                        alumnoTemporal.Id = datosAlumnoMatricula.Id;
                        alumnoTemporal.CodigoMatricula = datosAlumnoMatricula.CodigoMatricula;
                        alumnoTemporal.IdPespecifico = item.IdPespecificoHijo;
                        alumnoTemporal.NombreAlumno = datosAlumnoMatricula.NombreAlumno;
                        alumnoTemporal.Genero = datosAlumnoMatricula.Genero;
                        alumnoTemporal.IdCentroCosto = item.IdCentroCosto;
                        alumnoTemporal.NombreCentroCosto = item.NombreCentroCosto;
                        alumnoTemporal.IdEstadoMatricula = datosAlumnoMatricula.IdEstadoMatricula;
                        alumnoTemporal.NombreEstadoMatricula = datosAlumnoMatricula.NombreEstadoMatricula;
                        alumnoTemporal.Modulo = true;
                        alumnoTemporal.Solicitado = false;
                        //alumnoTemporal.SolicitadoPartner = false;

                        if (alumnoTemporal.MensajeCondicion == null)
                        {
                            alumnoTemporal.FechaInicio = CalcularFechaInicioAonline(datosAlumnoMatricula.CodigoMatricula);
                            alumnoTemporal.FechaFin = CalcularFechaTerminoAonline(datosAlumnoMatricula.CodigoMatricula);
                            alumnoTemporal.EscalaCalificacion = "100";
                        }
                        resultado.Add(alumnoTemporal);
                    }
                    
                }
                //var resultadoFinal = 
            }
            else
            {
                if (Solicititado.Exists(w => w.IdPespecifico == datosAlumnoMatricula.IdPespecifico))
                {
                    var solicitud = Solicititado.Where(w => w.IdPespecifico == datosAlumnoMatricula.IdPespecifico).FirstOrDefault();
                    datosAlumnoMatricula.Modulo = false;
                    datosAlumnoMatricula.Solicitado = true;
                    //datosAlumnoMatricula.SolicitadoPartner = solicitud.AplicaPartner;
                    datosAlumnoMatricula.FechaInicio = solicitud.FechaInicio;
                    datosAlumnoMatricula.FechaFin = solicitud.FechaTermino;
                }
                else
                {
                    if (string.IsNullOrEmpty(datosAlumnoMatricula.Genero) || (datosAlumnoMatricula.Genero != "M" && datosAlumnoMatricula.Genero != "F"))
                    {
                        datosAlumnoMatricula.MensajeCondicion = "El Alumno no Tiene Actualizado el Genero";
                    }
                    if (datosAlumnoMatricula.IdEstadoMatricula != 5 && datosAlumnoMatricula.IdEstadoMatricula != 7)
                    {
                        datosAlumnoMatricula.MensajeCondicion = "Revisar si el alumno Culmino";
                    }
                    datosAlumnoMatricula.Modulo = false;
                    datosAlumnoMatricula.Solicitado = false;
                    //datosAlumnoMatricula.SolicitadoPartner = false;
                    if (datosAlumnoMatricula.MensajeCondicion == null)
                    {
                        datosAlumnoMatricula.FechaInicio = CalcularFechaInicioAonline(datosAlumnoMatricula.CodigoMatricula);
                        datosAlumnoMatricula.FechaFin = CalcularFechaTerminoAonline(datosAlumnoMatricula.CodigoMatricula);
                        datosAlumnoMatricula.EscalaCalificacion = "100";
                    }
                    resultado.Add(datosAlumnoMatricula);
                }
                
            }
            return resultado;
        }
        public List<DatosAlumnoMatriculaDTO> ObtenerAlumnosSinSolicitudCC(int IdCentroCosto)
        {
            List<DatosAlumnoMatriculaDTO> resultado = new List<DatosAlumnoMatriculaDTO>();
            resultado = _repMatriculaCabecera.ObtenerInformacionAlumnoPorCentroCosto(IdCentroCosto);

            if (resultado == null || resultado.Count() ==0)
            {
                return resultado; 
            }
            //var programasHijos = _repPespecificoPadrePespecificoHijo.ObtenerProgramasHijos(CodigoMatricula);
            //var Solicititado = _repCertificadoSolicitudDetalle.GetBy(w => w.IdMatriculaCabecera == datosAlumnoMatricula.Id, y => new { y.IdPespecifico,y.AplicaPartner,y.FechaInicio,y.FechaTermino }).ToList();

            foreach (var  datosAlumnoMatricula  in resultado)
            {
                
                if (string.IsNullOrEmpty(datosAlumnoMatricula.Genero) || (datosAlumnoMatricula.Genero != "M" && datosAlumnoMatricula.Genero != "F"))
                {
                    datosAlumnoMatricula.MensajeCondicion = "El Alumno no Tiene Actualizado el Genero";
                }
                if (datosAlumnoMatricula.IdEstadoMatricula != 5 && datosAlumnoMatricula.IdEstadoMatricula != 7 && datosAlumnoMatricula.IdEstadoMatricula != 12 )
                {
                    datosAlumnoMatricula.MensajeCondicion = "Revisar si el alumno Culmino";
                }
                datosAlumnoMatricula.Modulo = false;
                datosAlumnoMatricula.Solicitado = false;
                //datosAlumnoMatricula.SolicitadoPartner = false;
                if (datosAlumnoMatricula.MensajeCondicion == null)
                {
                    datosAlumnoMatricula.FechaInicio = CalcularFechaInicioAonline(datosAlumnoMatricula.CodigoMatricula);
                    datosAlumnoMatricula.FechaFin = CalcularFechaTerminoAonline(datosAlumnoMatricula.CodigoMatricula);
                }
                
                //resultado.Add(datosAlumnoMatricula);
            }
            
            return resultado;
        }

        public DateTime? CalcularFechaInicioAonline(string CodigoMatricula)
        {
            return _repMatriculaCabecera.ObtenerFechaInicioAonline(CodigoMatricula);
        }
        public DateTime? CalcularFechaTerminoAonline(string CodigoMatricula)
        {
            DateTime? fechaFin = null;  
            fechaFin = _repMatriculaCabecera.ObtenerFechaFinAonline(CodigoMatricula);

            if (fechaFin == null)
            {
                fechaFin = _repMatriculaCabecera.ObtenerFechaFinAonlineNoAplicaProyecto(CodigoMatricula);
            }

            return fechaFin;

        }

        public int ObtenerNumeroSolictud(DateTime FechaActual)
        {

            //int? numeroSolicitudActual = _repCertificadoSolicitud.GetBy(w => w.FechaCreacion.Year == FechaActual.Year).OrderByDescending(w => w.NumeroSolicitud).FirstOrDefault().NumeroSolicitud;
            int? numeroSolicitudActual = _repCertificadoSolicitud.ObtenerNumeroSolictud(FechaActual);
            return (numeroSolicitudActual?? 0)+1;
        }
    }
}
