using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Helpers;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class ExamenAsignadoBO : BaseBO
    {
        public int IdProcesoSeleccion { get; set; }
        public int IdExamen { get; set; }
        public int IdPostulante { get; set; }
        public bool? EstadoExamen { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EstadoAcceso { get; set; }

        public bool InsertarPostulanteNuevo(PostulanteFormularioDTO Postulante, integraDBContext _integraDBContext)
        {
            try
            {
                PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
                PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
                ExamenAsignadoRepositorio _repExamenAsignado = new ExamenAsignadoRepositorio(_integraDBContext);
                PostulanteBO postulante = new PostulanteBO();
                ExamenAsignadoEvaluadorRepositorio _repExamenAsignadoEvaluador = new ExamenAsignadoEvaluadorRepositorio(_integraDBContext);
                IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                var personal = _repIntegraAspNetUsers.ObtenerIdentidadUsusario(Postulante.Usuario);
                PersonaBO persona = new PersonaBO(_integraDBContext);
                ProcesoSeleccionEtapaRepositorio _repProcesoSeleccionEtapa = new ProcesoSeleccionEtapaRepositorio(_integraDBContext);
                EtapaProcesoSeleccionCalificadoRepositorio _reEtapaCalificacion = new EtapaProcesoSeleccionCalificadoRepositorio(_integraDBContext);
                ExamenRealizadoRespuestaEvaluadorRepositorio _repExamenRealizadoRespuestaEvaluador = new ExamenRealizadoRespuestaEvaluadorRepositorio(_integraDBContext);
                var IdExamenAsignadoEvaluadorCriterioDesaprobatorio = 0;
                ExamenAsignadoEvaluadorBO examenAsignadoEvaluadorCriterioDesaprobatorioBO = new ExamenAsignadoEvaluadorBO();

                var existe = _repPostulante.FirstBy(x => x.Email.Equals(Postulante.InformacionPostulante.Email.Trim()));
                if (existe != null)
                {
                    var mensaje = "No se realizo el registro ya que el postulante ya existe";
                    return false;
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        postulante.Nombre = Postulante.InformacionPostulante.Nombre;
                        postulante.ApellidoPaterno = Postulante.InformacionPostulante.ApellidoPaterno;
                        postulante.ApellidoMaterno = Postulante.InformacionPostulante.ApellidoMaterno;
                        postulante.NroDocumento = Postulante.InformacionPostulante.NroDocumento;
                        postulante.Celular = Postulante.InformacionPostulante.Celular;
                        postulante.Email = Postulante.InformacionPostulante.Email;
                        postulante.IdTipoDocumento = Postulante.InformacionPostulante.IdTipoDocumento;
                        postulante.IdPais = Postulante.InformacionPostulante.IdPais;
                        postulante.IdCiudad = Postulante.InformacionPostulante.IdCiudad;
                        postulante.Estado = true;
                        postulante.UsuarioCreacion = Postulante.Usuario;
                        postulante.UsuarioModificacion = Postulante.Usuario;
                        postulante.FechaCreacion = DateTime.Now;
                        postulante.FechaModificacion = DateTime.Now;
                        _repPostulante.Insert(postulante);

                        int? idCreacionCorrecta = persona.InsertarPersona(postulante.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Postulante, Postulante.Usuario);
                        if (idCreacionCorrecta == null)
                        {
                            throw new Exception("No se creo clasificacion persona");
                        }
                        if (Postulante.InformacionPostulante.IdProcesoSeleccion.HasValue)
                        {
                            PostulanteProcesoSeleccionBO postulanteProcesoSeleccion = new PostulanteProcesoSeleccionBO
                            {
                                IdPostulante = postulante.Id,
                                IdProcesoSeleccion = Postulante.InformacionPostulante.IdProcesoSeleccion.Value,
                                FechaRegistro = DateTime.Now,
                                Estado = true,
                                UsuarioCreacion = Postulante.Usuario,
                                UsuarioModificacion = Postulante.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdEstadoProcesoSeleccion = Postulante.InformacionPostulante.IdEstadoProcesoSeleccion,
                                IdPostulanteNivelPotencial = Postulante.InformacionPostulante.IdPostulanteNivelPotencial,
                                IdProveedor = Postulante.InformacionPostulante.IdProveedor,
                                IdPersonalOperadorProceso = Postulante.InformacionPostulante.IdPersonal_OperadorProceso,
                                IdConvocatoriaPersonal = Postulante.InformacionPostulante.IdConvocatoriaPersonal
                            };
                            var res = _repPostulanteProcesoSeleccion.Insert(postulanteProcesoSeleccion);
                            if (res)
                            {
                                var postulacion = _repExamenAsignado.FirstBy(x => x.IdPostulante == postulanteProcesoSeleccion.IdPostulante && x.IdProcesoSeleccion == postulanteProcesoSeleccion.IdProcesoSeleccion);
                                if (postulacion == null)
                                {
                                    //var examenPorProceso = _repExamenAsignado.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccion(postulanteProcesoSeleccion.IdProcesoSeleccion);
                                    var examenPorProceso = _repExamenAsignado.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
                                    var examenEvaluadorPorProceso = _repExamenAsignadoEvaluador.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
                                    foreach (var item in examenPorProceso)
                                    {
                                        ExamenAsignadoBO examenAsignado = new ExamenAsignadoBO();
                                        examenAsignado.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
                                        examenAsignado.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
                                        examenAsignado.IdExamen = item.IdExamen;
                                        examenAsignado.EstadoExamen = false;
                                        examenAsignado.Estado = true;
                                        examenAsignado.UsuarioCreacion = Postulante.Usuario;
                                        examenAsignado.UsuarioModificacion = Postulante.Usuario;
                                        examenAsignado.FechaCreacion = DateTime.Now;
                                        examenAsignado.FechaModificacion = DateTime.Now;
                                        _repExamenAsignado.Insert(examenAsignado);
                                    }
                                    foreach (var item in examenEvaluadorPorProceso)
                                    {
                                        ExamenAsignadoEvaluadorBO examenAsignadoEvaluador = new ExamenAsignadoEvaluadorBO();
                                        examenAsignadoEvaluador.IdPersonal = personal.Id;
                                        examenAsignadoEvaluador.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
                                        examenAsignadoEvaluador.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
                                        examenAsignadoEvaluador.IdExamen = item.IdExamen;
                                        examenAsignadoEvaluador.EstadoExamen = false;
                                        examenAsignadoEvaluador.Estado = true;
                                        examenAsignadoEvaluador.UsuarioCreacion = Postulante.Usuario;
                                        examenAsignadoEvaluador.UsuarioModificacion = Postulante.Usuario;
                                        examenAsignadoEvaluador.FechaCreacion = DateTime.Now;
                                        examenAsignadoEvaluador.FechaModificacion = DateTime.Now;

                                        if (examenAsignadoEvaluador.IdExamen == 111 && Postulante.InformacionPostulante.ListaRespuestaDesaprobatoria != null)
                                        {
                                            examenAsignadoEvaluador.EstadoExamen = true;
                                        }
                                        _repExamenAsignadoEvaluador.Insert(examenAsignadoEvaluador);

                                        if (examenAsignadoEvaluador.IdExamen == 111 && Postulante.InformacionPostulante.ListaRespuestaDesaprobatoria != null)
                                        {
                                            IdExamenAsignadoEvaluadorCriterioDesaprobatorio = examenAsignadoEvaluador.Id;
                                            examenAsignadoEvaluadorCriterioDesaprobatorioBO = examenAsignadoEvaluador;
                                        }

                                    }
                                    if (IdExamenAsignadoEvaluadorCriterioDesaprobatorio > 0 && Postulante.InformacionPostulante.ListaRespuestaDesaprobatoria != null)
                                    {

                                        foreach (var item in Postulante.InformacionPostulante.ListaRespuestaDesaprobatoria)
                                        {
                                            ExamenRealizadoRespuestaEvaluadorBO evaluadorExamen = new ExamenRealizadoRespuestaEvaluadorBO();
                                            evaluadorExamen.IdExamenAsignadoEvaluador = IdExamenAsignadoEvaluadorCriterioDesaprobatorio;
                                            evaluadorExamen.IdPregunta = 761; // Id de Pregunta de Examen de Criterio de Desaprobacion
                                            evaluadorExamen.IdRespuestaPregunta = item;
                                            evaluadorExamen.TextoRespuesta = null;
                                            evaluadorExamen.Estado = true;
                                            evaluadorExamen.UsuarioCreacion = Postulante.Usuario;
                                            evaluadorExamen.UsuarioModificacion = Postulante.Usuario;
                                            evaluadorExamen.FechaCreacion = DateTime.Now;
                                            evaluadorExamen.FechaModificacion = DateTime.Now;

                                            _repExamenRealizadoRespuestaEvaluador.Insert(evaluadorExamen);
                                        }
                                    }
                                }
                                //Se asignan todas las etapas del proceso al postulante
                                var EtapasProceso = _repProcesoSeleccionEtapa.GetBy(x => x.IdProcesoSeleccion == Postulante.InformacionPostulante.IdProcesoSeleccion && x.Estado == true);
                                foreach (var item in EtapasProceso)
                                {
                                    EtapaProcesoSeleccionCalificadoBO etapaCalificacion = new EtapaProcesoSeleccionCalificadoBO();
                                    etapaCalificacion.IdProcesoSeleccionEtapa = item.Id;
                                    etapaCalificacion.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
                                    if (Postulante.InformacionPostulante.IdProcesoSeleccionEtapa == item.Id)
                                    {
                                        etapaCalificacion.EsEtapaAprobada = Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
                                        etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == null || Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion;
                                        etapaCalificacion.EsEtapaActual = true;
                                        etapaCalificacion.EsContactado = true;
                                    }
                                    else
                                    {
                                        etapaCalificacion.EsEtapaAprobada = false;
                                        etapaCalificacion.IdEstadoEtapaProcesoSeleccion = 9;//Las demas etapas ingresan con el estado Sin rendir
                                        etapaCalificacion.EsEtapaActual = false;
                                        etapaCalificacion.EsContactado = false;
                                    }
                                    etapaCalificacion.Estado = true;
                                    etapaCalificacion.UsuarioCreacion = Postulante.Usuario;
                                    etapaCalificacion.UsuarioModificacion = Postulante.Usuario;
                                    etapaCalificacion.FechaCreacion = DateTime.Now;
                                    etapaCalificacion.FechaModificacion = DateTime.Now;
                                    _reEtapaCalificacion.Insert(etapaCalificacion);
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        scope.Complete();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al insertar en las tablas");
            }
        }        
    }
}
