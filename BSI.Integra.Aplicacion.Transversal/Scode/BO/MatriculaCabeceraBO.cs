using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Finanzas/MatriculaCabecera
    /// Autor: Fischer Valdez - Carlos Crispin - Ansoli Espinoza - Wilber Choque - Gian Miranda
    /// Fecha: 06/02/2021
    /// <summary>
    /// BO para el obtener informacion de la matricula cabecera
    /// </summary>
    public class MatriculaCabeceraBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// CodigoMatricula		                Codigo de matricula (Codigo: "IdAlumno"A"IdCentroCosto")
        /// IdAlumno                            Id del alumno (PK de la tabla mkt.T_Alumno)
        /// IdPespecifico		                Id del PEspecifico (PK de la tabla pla.T_PEspecifico)
        /// IdEstadoMatricula		            Id del Estado de matricula (PK de la tabla fin.T_EstadoMatricula)
        /// IdEstadoPagoMatricula		        Id del Estado del pago de matricula
        /// EstadoMatricula	                    Estado de la matricula en texto
        /// FechaMatricula                      Fecha de la matricula realizada
        /// EmpresaRuc                          RUC de la empresa
        /// EmpresaNombre                       Nombre de la empresa
        /// EmpresaContacto                     Contacto de la empresa
        /// EmpresaEmail                        Email de la empresa
        /// EmpresaPaga                         Paga de la empresa
        /// EmpresaObservaciones                Observaciones de la empresa
        /// IdDocumentoPago                     Id del DocumentoPago con el que se registra la matricula (PK de la tabla fin.T_DocumentoPago)
        /// IdCoordinador                       Id del personal de tipo coordinador con el que se registra la matricula (PK de la tabla gp.T_Personal)
        /// IdAsesor                            Id del personal de tipo asesor con el que se registra la matricula (PK de la tabla gp.T_Personal)
        /// IdEstado_matricula                  Id del estado de matricula en el que se encuentra la matricula (PK de la tabla fin.T_EstadoMatricula)
        /// FechaSuspendido                     Fecha de suspension de matricula
        /// UsuarioCoordinadorAcademico         Usuario de integra coordinador academico del alumno
        /// ObservacionGeneralOperaciones       Observaciones personalizadas de operaciones respecto a la matricula
        /// UsuarioCoordinadorSupervision       Usuario de integra supervisor academico del alumno
        /// IdCronograma                        Id del cronograma de pago con el que se registra la matricula(PK de la tabla fin.T_CronogramaPago)
        /// IdPeriodo                           Id del periodo que se registra en Integra (PK de la tabla fin.T_CronogramaPago)
        /// UsuarioCoordinadorPreAsignacion     Usuario de integra coordinador preasignacion
        /// VerificacionConforme                Conformidad de la verificacion de datos
        /// FechaMatriculaValidada              Fecha de validacion de matricula
        /// FechaPagoValidada                   Fecha de validacion de pago
        /// FechaRetiro                         Fecha de retiro del alumno
        /// IdPaquete                           Id del paquete (Version de programa contratada por el alumno)
        /// IdMigracion                         Id de migracion de V3 (Campo obligatorio)
        /// IdSubEstadoMatricula                Id del subestado de matricula (PK de la tabla fin.T_SubEstadoMatricula)
        /// FechaFinalizacion                   Fecha de finalizacion del curso por parte del alumno
        public string CodigoMatricula { get; set; }
        public int IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        public int IdEstadoMatricula { get; set; }
        public int IdEstadoPagoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
        public DateTime FechaMatricula { get; set; }
        public string EmpresaRuc { get; set; }
        public string EmpresaNombre { get; set; }
        public string EmpresaContacto { get; set; }
        public string EmpresaEmail { get; set; }
        public string EmpresaPaga { get; set; }
        public string EmpresaObservaciones { get; set; }
        public int IdDocumentoPago { get; set; }
        public int IdCoordinador { get; set; }
        public int IdAsesor { get; set; }
        public int IdEstado_matricula { get; set; }
        public string FechaSuspendido { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }
        public string ObservacionGeneralOperaciones { get; set; }
        public string UsuarioCoordinadorSupervision { get; set; }
        public int IdCronograma { get; set; }
        public int IdPeriodo { get; set; }
        public string UsuarioCoordinadorPreAsignacion { get; set; }
        public bool VerificacionConforme { get; set; }
        public bool? FechaMatriculaValidada { get; set; }
        public bool? FechaPagoValidada { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public int IdPaquete { get; set; }
        public string IdMigracion { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public int? IdEstadoMatriculaCertificado { get; set; }
        public int? IdSubEstadoMatriculaCertificado { get; set; }

        public MatriculaCabeceraBO() {

        }

        /// <summary>
        /// Obtiene en un objeto del tipo DetalleOportunidadOperacionesDTO los detalles de matriculas
        /// </summary>
        /// <returns>Objeto del tipo DetalleOportunidadOperacionesDTO</returns>
        public DetalleOportunidadOperacionesDTO ObtenerDetalleMatricula() {
            try
            {
                var _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                return _repMatriculaCabecera.ObtenerDetalleMatricula(this.Id);
            }
            catch (Exception e) 
            {
                throw e;
            }
        }
    }
}
