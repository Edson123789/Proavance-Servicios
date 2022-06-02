using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EstructuraEspecificaBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPgeneralPadre { get; set; }
        public int IdPgeneralHijo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int CongelarEstructuraEspecificaMasivo(int Cantidad)
        {
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();

            var Usuario = "SYSTEM";
            //Con el IdCronograma se consigue el IdMatriculaCabecera
            var datosMatriculaCabecera = _repMatriculaCabecera.ObtenerListaEstructuraMatriculaNoCongelada(Cantidad);
            foreach (var mat in datosMatriculaCabecera)
            {
                CongelarEstructuraEspecificaPorMatricula(mat.CodigoMatricula,mat.Id, Usuario);
            }
            return Cantidad;

        }

        public DatosEstructuraCurricularDTO CongelarEstructuraEspecificaPorMatricula(string CodigoMatricula,int Idmatricula, string Usuario)
        {
            DatosEstructuraCurricularDTO listaDatosEstructura = new DatosEstructuraCurricularDTO();
            listaDatosEstructura.DatosEstructura = new List<DatosEstructuraEspecificaDTO>();
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
            PespecificoPadrePespecificoHijoRepositorio _PespecificoPadrePespecificoHijoRepositorio = new PespecificoPadrePespecificoHijoRepositorio();
            EstructuraEspecificaRepositorio _repEstructuraEspecifica = new EstructuraEspecificaRepositorio();
            PgeneralAsubPgeneralRepositorio _PgeneralAsubPgeneralRepositorio = new PgeneralAsubPgeneralRepositorio();

            listaDatosEstructura.Usuario = Usuario;
            var idProgramaEspecifico = _repMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(CodigoMatricula);
            //Verificar si es padre o hijo
            //var listaPrograma = _PespecificoPadrePespecificoHijoRepositorio.ObtenerPespecificosHijos(idProgramaEspecifico);
            var listaPrograma = _PgeneralAsubPgeneralRepositorio.ObtenerCursosCongelamientoEstrucuraCurricular(Idmatricula);
            if (listaPrograma != null)
            {
                if (listaPrograma.Count() > 0)//Significa que tiene hijos
                {
                    var IdProgramaGeneralPadre = _repMatriculaCabecera.ObtenerProgramaGeneral(idProgramaEspecifico);
                    foreach (var lp in listaPrograma)
                    {
                        //Conseguimos el programa GeneralHijo
                        //var idProgramaGeneral = _repMatriculaCabecera.ObtenerProgramaGeneral(lp.PEspecificoHijoId);
                        DatosEstructuraEspecificaDTO estructura = new DatosEstructuraEspecificaDTO();
                        estructura.IdMatriculaCabecera = Idmatricula;
                        estructura.IdPGeneralPadre = IdProgramaGeneralPadre;
                        estructura.IdPGeneralHijo = lp.Id;
                        //listaDatosEstructura.Insert(estructuraBO);
                        //var IdEstructuraEspecifica = this.Id;
                        estructura.Encuesta = new List<DatosEstructuraEspecificaEncuestaDTO>();
                        estructura.Tarea = new List<DatosEstructuraEspecificaTareaDTO>();
                        estructura.Capitulo = new List<DatosEstructuraEspecificaCapituloDTO>();

                        List<DatosEstructuraEspecificaCapituloDTO> listaCapitulos = new List<DatosEstructuraEspecificaCapituloDTO>();

                        var respuesta = this.ConseguirEstructuraPorPrograma(lp.Id);
                        if (respuesta != null) {
                            foreach (var r in respuesta)
                            {

                                if (r.Nombre.ToUpper().Contains("ENCUESTA"))
                                {

                                    DatosEstructuraEspecificaEncuestaDTO encuestaDTO = new DatosEstructuraEspecificaEncuestaDTO();
                                    encuestaDTO.IdEncuesta = r.Id;
                                    encuestaDTO.NombreEncuesta = r.Capitulo;
                                    encuestaDTO.OrdenCapitulo = r.OrdenFila;
                                    encuestaDTO.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                    estructura.Encuesta.Add(encuestaDTO);

                                }
                                else
                                {
                                    if (r.Nombre.ToUpper().Contains("TAREA"))
                                    {
                                        DatosEstructuraEspecificaTareaDTO tarea = new DatosEstructuraEspecificaTareaDTO();
                                        // tarea.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                        tarea.NombreTarea = r.Capitulo;
                                        tarea.OrdenCapitulo = r.OrdenFila;
                                        tarea.IdTarea = r.Id;
                                        tarea.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                        estructura.Tarea.Add(tarea);

                                    }
                                    else
                                    {
                                        DatosEstructuraEspecificaCapituloDTO capituloDTO = new DatosEstructuraEspecificaCapituloDTO();
                                        //capituloBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                        capituloDTO.Numero = r.OrdenFila;
                                        capituloDTO.Capitulo = r.Capitulo;
                                        if (r.ListaSesiones.Count() > 0)
                                        {
                                            List<DatosEstructuraEspecificaSesionDTO> Sesion = new List<DatosEstructuraEspecificaSesionDTO>();

                                            var sesiones = r.ListaSesiones.Where(x => x.Sesion != null).Select(x => x.Sesion).Distinct().ToList();
                                            foreach (var se in sesiones)
                                            {
                                                var sesion = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).FirstOrDefault();
                                                DatosEstructuraEspecificaSesionDTO sesionDTO = new DatosEstructuraEspecificaSesionDTO();
                                                //sesionBO.IdEstructuraEspecificaCapitulo = IdCapitulo;
                                                sesionDTO.Numero = sesion.OrdenSeccion;
                                                sesionDTO.Sesion = sesion.Sesion;
                                                sesionDTO.OrdenSesion = sesion.OrdenFila;
                                                var subSesiones = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).ToList();
                                                List<DatosEstructuraEspecificaSubSesionDTO> listaSubSesionBO = new List<DatosEstructuraEspecificaSubSesionDTO>();
                                                foreach (var sub in subSesiones)
                                                {
                                                    if (sub.SubSesion != null)
                                                    {
                                                        if (sub.SubSesion.Trim() != "")
                                                        {
                                                            DatosEstructuraEspecificaSubSesionDTO subSesionBO = new DatosEstructuraEspecificaSubSesionDTO();
                                                            //subSesionBO.IdEstructuraEspecificaSesion = IdSesion;
                                                            subSesionBO.Numero = sub.OrdenFila;
                                                            subSesionBO.SubSesion = sub.SubSesion;
                                                            //subSesisubSesionBOonBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                                            listaSubSesionBO.Add(subSesionBO);
                                                        }
                                                    }
                                                }
                                                sesionDTO.SubSesion = listaSubSesionBO;
                                                Sesion.Add(sesionDTO);
                                            }
                                            capituloDTO.Sesion = Sesion;
                                        }
                                        listaCapitulos.Add(capituloDTO);

                                    }
                                }


                            }
                            estructura.Capitulo = listaCapitulos;
                            listaDatosEstructura.DatosEstructura.Add(estructura);
                        }
                       
                    }
                }
                else //No tiene hijos
                {
                    //Conseguimos el programa General
                    var idProgramaGeneral = _repMatriculaCabecera.ObtenerProgramaGeneral(idProgramaEspecifico);
                    DatosEstructuraEspecificaDTO estructura = new DatosEstructuraEspecificaDTO();
                    estructura.IdMatriculaCabecera = Idmatricula;
                    estructura.IdPGeneralPadre = idProgramaGeneral;
                    estructura.IdPGeneralHijo = idProgramaGeneral;

                    var respuesta = this.ConseguirEstructuraPorPrograma(idProgramaGeneral);
                    estructura.Encuesta = new List<DatosEstructuraEspecificaEncuestaDTO>();
                    estructura.Tarea = new List<DatosEstructuraEspecificaTareaDTO>();
                    estructura.Capitulo = new List<DatosEstructuraEspecificaCapituloDTO>();

                    List<DatosEstructuraEspecificaCapituloDTO> listaCapitulos = new List<DatosEstructuraEspecificaCapituloDTO>();
                    if (respuesta != null) {
                        foreach (var r in respuesta)
                        {

                            if (r.Nombre.ToUpper().Contains("ENCUESTA"))
                            {

                                DatosEstructuraEspecificaEncuestaDTO encuestaDTO = new DatosEstructuraEspecificaEncuestaDTO();
                                encuestaDTO.IdEncuesta = r.Id;
                                encuestaDTO.NombreEncuesta = r.Nombre;
                                encuestaDTO.OrdenCapitulo = r.OrdenFila;
                                encuestaDTO.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                estructura.Encuesta.Add(encuestaDTO);

                            }
                            else
                            {
                                if (r.Nombre.ToUpper().Contains("TAREA"))
                                {
                                    DatosEstructuraEspecificaTareaDTO tarea = new DatosEstructuraEspecificaTareaDTO();
                                    // tarea.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                    tarea.NombreTarea = r.Nombre;
                                    tarea.OrdenCapitulo = r.OrdenFila;
                                    tarea.IdTarea = r.Id;
                                    tarea.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                    estructura.Tarea.Add(tarea);

                                }
                                else
                                {
                                    DatosEstructuraEspecificaCapituloDTO capituloDTO = new DatosEstructuraEspecificaCapituloDTO();
                                    //capituloBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                    capituloDTO.Numero = r.OrdenFila;
                                    capituloDTO.Capitulo = r.Capitulo;
                                    if (r.ListaSesiones.Count() > 0)
                                    {
                                        List<DatosEstructuraEspecificaSesionDTO> Sesion = new List<DatosEstructuraEspecificaSesionDTO>();

                                        var sesiones = r.ListaSesiones.Where(x => x.Sesion != null).Select(x => x.Sesion).Distinct().ToList();
                                        foreach (var se in sesiones)
                                        {
                                            var sesion = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).FirstOrDefault();
                                            DatosEstructuraEspecificaSesionDTO sesionDTO = new DatosEstructuraEspecificaSesionDTO();
                                            //sesionBO.IdEstructuraEspecificaCapitulo = IdCapitulo;
                                            sesionDTO.Numero = sesion.OrdenSeccion;
                                            sesionDTO.Sesion = sesion.Sesion;
                                            sesionDTO.OrdenSesion = sesion.OrdenFila;
                                            var subSesiones = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).ToList();
                                            List<DatosEstructuraEspecificaSubSesionDTO> listaSubSesionBO = new List<DatosEstructuraEspecificaSubSesionDTO>();
                                            foreach (var sub in subSesiones)
                                            {
                                                if (sub.SubSesion != null)
                                                {
                                                    if (sub.SubSesion.Trim() != "")
                                                    {
                                                        DatosEstructuraEspecificaSubSesionDTO subSesionBO = new DatosEstructuraEspecificaSubSesionDTO();
                                                        //subSesionBO.IdEstructuraEspecificaSesion = IdSesion;
                                                        subSesionBO.Numero = sesion.OrdenFila;
                                                        subSesionBO.SubSesion = sub.SubSesion;
                                                        //subSesisubSesionBOonBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                                        listaSubSesionBO.Add(subSesionBO);
                                                    }
                                                }

                                            }
                                            sesionDTO.SubSesion = listaSubSesionBO;
                                            Sesion.Add(sesionDTO);
                                        }
                                        capituloDTO.Sesion = Sesion;
                                    }
                                    listaCapitulos.Add(capituloDTO);

                                }
                            }


                        }
                        estructura.Capitulo = listaCapitulos;
                        listaDatosEstructura.DatosEstructura.Add(estructura);
                    }
                    
                }
            }
            _repEstructuraEspecifica.CongelarEstructuraAlumno(listaDatosEstructura.DatosEstructura, listaDatosEstructura.Usuario);
            return listaDatosEstructura;
        }
        public DatosEstructuraCurricularDTO CongelarEstructuraEspecificaPorIdMatricula(int IdMatriculaCabecera, string Usuario)
        {   
            DatosEstructuraCurricularDTO listaDatosEstructura = new DatosEstructuraCurricularDTO();
            listaDatosEstructura.DatosEstructura = new List<DatosEstructuraEspecificaDTO>();
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
            PespecificoPadrePespecificoHijoRepositorio _PespecificoPadrePespecificoHijoRepositorio = new PespecificoPadrePespecificoHijoRepositorio();
            EstructuraEspecificaRepositorio _repEstructuraEspecifica = new EstructuraEspecificaRepositorio();
            PgeneralAsubPgeneralRepositorio _PgeneralAsubPgeneralRepositorio = new PgeneralAsubPgeneralRepositorio();

            listaDatosEstructura.Usuario = Usuario;
            //Con el IdMatriculaCabecera se consigue el CodigoMatricula
            var datosMatriculaCabecera = _repMatriculaCabecera.ObtenerInformacionMatriculaCabeceraPorIdMatriculaCabecera(IdMatriculaCabecera);
            var idProgramaEspecifico = _repMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(datosMatriculaCabecera.CodigoMatricula);
            //Verificar si es padre o hijo
            //var listaPrograma = _PespecificoPadrePespecificoHijoRepositorio.ObtenerPespecificosHijos(idProgramaEspecifico);
            var listaPrograma = _PgeneralAsubPgeneralRepositorio.ObtenerCursosCongelamientoEstrucuraCurricular(IdMatriculaCabecera);
            if (listaPrograma != null)
            {
                if(listaPrograma.Count() >0)//Significa que tiene hijos
                {
                    var IdProgramaGeneralPadre= _repMatriculaCabecera.ObtenerProgramaGeneral(idProgramaEspecifico);
                    foreach ( var lp in listaPrograma)
                    {
                        //Conseguimos el programa GeneralHijo
                        //var idProgramaGeneral = _repMatriculaCabecera.ObtenerProgramaGeneral(lp.PEspecificoHijoId);
                        DatosEstructuraEspecificaDTO estructura = new DatosEstructuraEspecificaDTO();
                        estructura.IdMatriculaCabecera = IdMatriculaCabecera;
                        estructura.IdPGeneralPadre = IdProgramaGeneralPadre;
                        estructura.IdPGeneralHijo = lp.Id;
                        //listaDatosEstructura.Insert(estructuraBO);
                        //var IdEstructuraEspecifica = this.Id;
                        estructura.Encuesta = new List<DatosEstructuraEspecificaEncuestaDTO>();
                        estructura.Tarea = new List<DatosEstructuraEspecificaTareaDTO>();
                        estructura.Capitulo = new List<DatosEstructuraEspecificaCapituloDTO>();

                        List<DatosEstructuraEspecificaCapituloDTO> listaCapitulos = new List<DatosEstructuraEspecificaCapituloDTO>();

                        var respuesta = this.ConseguirEstructuraPorPrograma(lp.Id);
                        if (respuesta != null) {

                            foreach (var r in respuesta)
                            {

                                if (r.Nombre.ToUpper().Contains("ENCUESTA"))
                                {

                                    DatosEstructuraEspecificaEncuestaDTO encuestaDTO = new DatosEstructuraEspecificaEncuestaDTO();
                                    encuestaDTO.IdEncuesta = r.Id;
                                    encuestaDTO.NombreEncuesta = r.Capitulo;
                                    encuestaDTO.OrdenCapitulo = r.OrdenFila;
                                    encuestaDTO.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                    estructura.Encuesta.Add(encuestaDTO);

                                }
                                else
                                {
                                    if (r.Nombre.ToUpper().Contains("TAREA"))
                                    {
                                        DatosEstructuraEspecificaTareaDTO tarea = new DatosEstructuraEspecificaTareaDTO();
                                        // tarea.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                        tarea.NombreTarea = r.Capitulo;
                                        tarea.OrdenCapitulo = r.OrdenFila;
                                        tarea.IdTarea = r.Id;
                                        tarea.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                        estructura.Tarea.Add(tarea);

                                    }
                                    else
                                    {
                                        DatosEstructuraEspecificaCapituloDTO capituloDTO = new DatosEstructuraEspecificaCapituloDTO();
                                        //capituloBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                        capituloDTO.Numero = r.OrdenFila;
                                        capituloDTO.Capitulo = r.Capitulo;
                                        if (r.ListaSesiones.Count() > 0)
                                        {
                                            List<DatosEstructuraEspecificaSesionDTO> Sesion = new List<DatosEstructuraEspecificaSesionDTO>();

                                            var sesiones = r.ListaSesiones.Where(x => x.Sesion != null).Select(x => x.Sesion).Distinct().ToList();
                                            foreach (var se in sesiones)
                                            {
                                                var sesion = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).FirstOrDefault();
                                                DatosEstructuraEspecificaSesionDTO sesionDTO = new DatosEstructuraEspecificaSesionDTO();
                                                //sesionBO.IdEstructuraEspecificaCapitulo = IdCapitulo;
                                                sesionDTO.Numero = sesion.OrdenSeccion;
                                                sesionDTO.Sesion = sesion.Sesion;
                                                sesionDTO.OrdenSesion = sesion.OrdenFila;
                                                var subSesiones = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).ToList();
                                                List<DatosEstructuraEspecificaSubSesionDTO> listaSubSesionBO = new List<DatosEstructuraEspecificaSubSesionDTO>();
                                                foreach (var sub in subSesiones)
                                                {
                                                    if (sub.SubSesion != null)
                                                    {
                                                        if (sub.SubSesion.Trim() != "")
                                                        {
                                                            DatosEstructuraEspecificaSubSesionDTO subSesionBO = new DatosEstructuraEspecificaSubSesionDTO();
                                                            //subSesionBO.IdEstructuraEspecificaSesion = IdSesion;
                                                            subSesionBO.Numero = sub.OrdenFila;
                                                            subSesionBO.SubSesion = sub.SubSesion;
                                                            //subSesisubSesionBOonBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                                            listaSubSesionBO.Add(subSesionBO);
                                                        }
                                                    }
                                                }
                                                sesionDTO.SubSesion = listaSubSesionBO;
                                                Sesion.Add(sesionDTO);
                                            }
                                            capituloDTO.Sesion = Sesion;
                                        }
                                        listaCapitulos.Add(capituloDTO);

                                    }
                                }


                            }
                            estructura.Capitulo = listaCapitulos;
                            listaDatosEstructura.DatosEstructura.Add(estructura);
                        }
                    }
                }
                else //No tiene hijos
                {
                    //Conseguimos el programa General
                    var idProgramaGeneral = _repMatriculaCabecera.ObtenerProgramaGeneral(idProgramaEspecifico);
                    DatosEstructuraEspecificaDTO estructura = new DatosEstructuraEspecificaDTO();
                    estructura.IdMatriculaCabecera = IdMatriculaCabecera;
                    estructura.IdPGeneralPadre = idProgramaGeneral;
                    estructura.IdPGeneralHijo = idProgramaGeneral;

                    var respuesta = this.ConseguirEstructuraPorPrograma(idProgramaGeneral);
                    estructura.Encuesta = new List<DatosEstructuraEspecificaEncuestaDTO>();
                    estructura.Tarea = new List<DatosEstructuraEspecificaTareaDTO>();
                    estructura.Capitulo = new List<DatosEstructuraEspecificaCapituloDTO>();

                    List<DatosEstructuraEspecificaCapituloDTO> listaCapitulos = new List<DatosEstructuraEspecificaCapituloDTO>();
                    if (respuesta != null) {
                        foreach (var r in respuesta)
                        {

                            if (r.Nombre.ToUpper().Contains("ENCUESTA"))
                            {

                                DatosEstructuraEspecificaEncuestaDTO encuestaDTO = new DatosEstructuraEspecificaEncuestaDTO();
                                encuestaDTO.IdEncuesta = r.Id;
                                encuestaDTO.NombreEncuesta = r.Nombre;
                                encuestaDTO.OrdenCapitulo = r.OrdenFila;
                                encuestaDTO.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                estructura.Encuesta.Add(encuestaDTO);

                            }
                            else
                            {
                                if (r.Nombre.ToUpper().Contains("TAREA"))
                                {
                                    DatosEstructuraEspecificaTareaDTO tarea = new DatosEstructuraEspecificaTareaDTO();
                                    // tarea.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                    tarea.NombreTarea = r.Nombre;
                                    tarea.OrdenCapitulo = r.OrdenFila;
                                    tarea.IdTarea = r.Id;
                                    tarea.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                    estructura.Tarea.Add(tarea);

                                }
                                else
                                {
                                    DatosEstructuraEspecificaCapituloDTO capituloDTO = new DatosEstructuraEspecificaCapituloDTO();
                                    //capituloBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                    capituloDTO.Numero = r.OrdenFila;
                                    capituloDTO.Capitulo = r.Capitulo;
                                    if (r.ListaSesiones.Count() > 0)
                                    {
                                        List<DatosEstructuraEspecificaSesionDTO> Sesion = new List<DatosEstructuraEspecificaSesionDTO>();

                                        var sesiones = r.ListaSesiones.Where(x => x.Sesion != null).Select(x => x.Sesion).Distinct().ToList();
                                        foreach (var se in sesiones)
                                        {
                                            var sesion = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).FirstOrDefault();
                                            DatosEstructuraEspecificaSesionDTO sesionDTO = new DatosEstructuraEspecificaSesionDTO();
                                            //sesionBO.IdEstructuraEspecificaCapitulo = IdCapitulo;
                                            sesionDTO.Numero = sesion.OrdenSeccion;
                                            sesionDTO.Sesion = sesion.Sesion;
                                            sesionDTO.OrdenSesion = sesion.OrdenFila;
                                            var subSesiones = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).ToList();
                                            List<DatosEstructuraEspecificaSubSesionDTO> listaSubSesionBO = new List<DatosEstructuraEspecificaSubSesionDTO>();
                                            foreach (var sub in subSesiones)
                                            {
                                                if (sub.SubSesion != null)
                                                {
                                                    if (sub.SubSesion.Trim() != "")
                                                    {
                                                        DatosEstructuraEspecificaSubSesionDTO subSesionBO = new DatosEstructuraEspecificaSubSesionDTO();
                                                        //subSesionBO.IdEstructuraEspecificaSesion = IdSesion;
                                                        subSesionBO.Numero = sesion.OrdenFila;
                                                        subSesionBO.SubSesion = sub.SubSesion;
                                                        //subSesisubSesionBOonBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                                        listaSubSesionBO.Add(subSesionBO);
                                                    }
                                                }

                                            }
                                            sesionDTO.SubSesion = listaSubSesionBO;
                                            Sesion.Add(sesionDTO);
                                        }
                                        capituloDTO.Sesion = Sesion;
                                    }
                                    listaCapitulos.Add(capituloDTO);

                                }
                            }


                        }
                        estructura.Capitulo = listaCapitulos;
                        listaDatosEstructura.DatosEstructura.Add(estructura);
                    }
                }
                   
            }
            _repEstructuraEspecifica.CongelarEstructuraAlumno(listaDatosEstructura.DatosEstructura, listaDatosEstructura.Usuario);
            return listaDatosEstructura;
        }

        public DatosEstructuraCurricularDTO CongelarEstructuraEspecifica(int IdCronograma, string Usuario)
        {
            DatosEstructuraCurricularDTO listaDatosEstructura = new DatosEstructuraCurricularDTO();
            listaDatosEstructura.DatosEstructura = new List<DatosEstructuraEspecificaDTO>();
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
            PespecificoPadrePespecificoHijoRepositorio _PespecificoPadrePespecificoHijoRepositorio = new PespecificoPadrePespecificoHijoRepositorio();
            EstructuraEspecificaRepositorio _repEstructuraEspecifica = new EstructuraEspecificaRepositorio();
            PgeneralAsubPgeneralRepositorio _PgeneralAsubPgeneralRepositorio = new PgeneralAsubPgeneralRepositorio();

            listaDatosEstructura.Usuario = Usuario;
            //Con el IdCronograma se consigue el IdMatriculaCabecera
            var datosMatriculaCabecera = _repMatriculaCabecera.ObtenerInformacionMatriculaCabeceraPorIdCronograma(IdCronograma);
            var idProgramaEspecifico = _repMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(datosMatriculaCabecera.CodigoMatricula);
            //Verificar si es padre o hijo
            //var listaPrograma = _PespecificoPadrePespecificoHijoRepositorio.ObtenerPespecificosHijos(idProgramaEspecifico);
            var listaPrograma = _PgeneralAsubPgeneralRepositorio.ObtenerCursosCongelamientoEstrucuraCurricular(datosMatriculaCabecera.Id);
            if (listaPrograma != null)
            {
                if(listaPrograma.Count() >0)//Significa que tiene hijos
                {
                    var IdProgramaGeneralPadre= _repMatriculaCabecera.ObtenerProgramaGeneral(idProgramaEspecifico);
                    foreach ( var lp in listaPrograma)
                    {
                        //Conseguimos el programa GeneralHijo
                        //var idProgramaGeneral = _repMatriculaCabecera.ObtenerProgramaGeneral(lp.PEspecificoHijoId);
                        DatosEstructuraEspecificaDTO estructura = new DatosEstructuraEspecificaDTO();
                        estructura.IdMatriculaCabecera = datosMatriculaCabecera.Id;
                        estructura.IdPGeneralPadre = IdProgramaGeneralPadre;
                        estructura.IdPGeneralHijo = lp.Id;
                        //listaDatosEstructura.Insert(estructuraBO);
                        //var IdEstructuraEspecifica = this.Id;
                        estructura.Encuesta = new List<DatosEstructuraEspecificaEncuestaDTO>();
                        estructura.Tarea = new List<DatosEstructuraEspecificaTareaDTO>();
                        estructura.Capitulo = new List<DatosEstructuraEspecificaCapituloDTO>();

                        List<DatosEstructuraEspecificaCapituloDTO> listaCapitulos = new List<DatosEstructuraEspecificaCapituloDTO>();

                        var respuesta = this.ConseguirEstructuraPorPrograma(lp.Id);
                        if (respuesta != null) {

                            foreach (var r in respuesta)
                            {

                                if (r.Nombre.ToUpper().Contains("ENCUESTA"))
                                {

                                    DatosEstructuraEspecificaEncuestaDTO encuestaDTO = new DatosEstructuraEspecificaEncuestaDTO();
                                    encuestaDTO.IdEncuesta = r.Id;
                                    encuestaDTO.NombreEncuesta = r.Capitulo;
                                    encuestaDTO.OrdenCapitulo = r.OrdenFila;
                                    encuestaDTO.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                    estructura.Encuesta.Add(encuestaDTO);

                                }
                                else
                                {
                                    if (r.Nombre.ToUpper().Contains("TAREA"))
                                    {
                                        DatosEstructuraEspecificaTareaDTO tarea = new DatosEstructuraEspecificaTareaDTO();
                                        // tarea.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                        tarea.NombreTarea = r.Capitulo;
                                        tarea.OrdenCapitulo = r.OrdenFila;
                                        tarea.IdTarea = r.Id;
                                        tarea.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                        estructura.Tarea.Add(tarea);

                                    }
                                    else
                                    {
                                        DatosEstructuraEspecificaCapituloDTO capituloDTO = new DatosEstructuraEspecificaCapituloDTO();
                                        //capituloBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                        capituloDTO.Numero = r.OrdenFila;
                                        capituloDTO.Capitulo = r.Capitulo;
                                        if (r.ListaSesiones.Count() > 0)
                                        {
                                            List<DatosEstructuraEspecificaSesionDTO> Sesion = new List<DatosEstructuraEspecificaSesionDTO>();

                                            var sesiones = r.ListaSesiones.Where(x => x.Sesion != null).Select(x => x.Sesion).Distinct().ToList();
                                            foreach (var se in sesiones)
                                            {
                                                var sesion = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).FirstOrDefault();
                                                DatosEstructuraEspecificaSesionDTO sesionDTO = new DatosEstructuraEspecificaSesionDTO();
                                                //sesionBO.IdEstructuraEspecificaCapitulo = IdCapitulo;
                                                sesionDTO.Numero = sesion.OrdenSeccion;
                                                sesionDTO.Sesion = sesion.Sesion;
                                                sesionDTO.OrdenSesion = sesion.OrdenFila;
                                                var subSesiones = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).ToList();
                                                List<DatosEstructuraEspecificaSubSesionDTO> listaSubSesionBO = new List<DatosEstructuraEspecificaSubSesionDTO>();
                                                foreach (var sub in subSesiones)
                                                {
                                                    if (sub.SubSesion != null)
                                                    {
                                                        if (sub.SubSesion.Trim() != "")
                                                        {
                                                            DatosEstructuraEspecificaSubSesionDTO subSesionBO = new DatosEstructuraEspecificaSubSesionDTO();
                                                            //subSesionBO.IdEstructuraEspecificaSesion = IdSesion;
                                                            subSesionBO.Numero = sub.OrdenFila;
                                                            subSesionBO.SubSesion = sub.SubSesion;
                                                            //subSesisubSesionBOonBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                                            listaSubSesionBO.Add(subSesionBO);
                                                        }
                                                    }
                                                }
                                                sesionDTO.SubSesion = listaSubSesionBO;
                                                Sesion.Add(sesionDTO);
                                            }
                                            capituloDTO.Sesion = Sesion;
                                        }
                                        listaCapitulos.Add(capituloDTO);

                                    }
                                }


                            }
                            estructura.Capitulo = listaCapitulos;
                            listaDatosEstructura.DatosEstructura.Add(estructura);
                        }
                    }
                }
                else //No tiene hijos
                {
                    //Conseguimos el programa General
                    var idProgramaGeneral = _repMatriculaCabecera.ObtenerProgramaGeneral(idProgramaEspecifico);
                    DatosEstructuraEspecificaDTO estructura = new DatosEstructuraEspecificaDTO();
                    estructura.IdMatriculaCabecera = datosMatriculaCabecera.Id;
                    estructura.IdPGeneralPadre = idProgramaGeneral;
                    estructura.IdPGeneralHijo = idProgramaGeneral;

                    var respuesta = this.ConseguirEstructuraPorPrograma(idProgramaGeneral);
                    estructura.Encuesta = new List<DatosEstructuraEspecificaEncuestaDTO>();
                    estructura.Tarea = new List<DatosEstructuraEspecificaTareaDTO>();
                    estructura.Capitulo = new List<DatosEstructuraEspecificaCapituloDTO>();

                    List<DatosEstructuraEspecificaCapituloDTO> listaCapitulos = new List<DatosEstructuraEspecificaCapituloDTO>();
                    if (respuesta != null) {
                        foreach (var r in respuesta)
                        {

                            if (r.Nombre.ToUpper().Contains("ENCUESTA"))
                            {

                                DatosEstructuraEspecificaEncuestaDTO encuestaDTO = new DatosEstructuraEspecificaEncuestaDTO();
                                encuestaDTO.IdEncuesta = r.Id;
                                encuestaDTO.NombreEncuesta = r.Nombre;
                                encuestaDTO.OrdenCapitulo = r.OrdenFila;
                                encuestaDTO.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                estructura.Encuesta.Add(encuestaDTO);

                            }
                            else
                            {
                                if (r.Nombre.ToUpper().Contains("TAREA"))
                                {
                                    DatosEstructuraEspecificaTareaDTO tarea = new DatosEstructuraEspecificaTareaDTO();
                                    // tarea.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                    tarea.NombreTarea = r.Nombre;
                                    tarea.OrdenCapitulo = r.OrdenFila;
                                    tarea.IdTarea = r.Id;
                                    tarea.IdDocumentoSeccionPw = r.IdDocumentoSeccionPw;
                                    estructura.Tarea.Add(tarea);

                                }
                                else
                                {
                                    DatosEstructuraEspecificaCapituloDTO capituloDTO = new DatosEstructuraEspecificaCapituloDTO();
                                    //capituloBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                    capituloDTO.Numero = r.OrdenFila;
                                    capituloDTO.Capitulo = r.Capitulo;
                                    if (r.ListaSesiones.Count() > 0)
                                    {
                                        List<DatosEstructuraEspecificaSesionDTO> Sesion = new List<DatosEstructuraEspecificaSesionDTO>();

                                        var sesiones = r.ListaSesiones.Where(x => x.Sesion != null).Select(x => x.Sesion).Distinct().ToList();
                                        foreach (var se in sesiones)
                                        {
                                            var sesion = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).FirstOrDefault();
                                            DatosEstructuraEspecificaSesionDTO sesionDTO = new DatosEstructuraEspecificaSesionDTO();
                                            //sesionBO.IdEstructuraEspecificaCapitulo = IdCapitulo;
                                            sesionDTO.Numero = sesion.OrdenSeccion;
                                            sesionDTO.Sesion = sesion.Sesion;
                                            sesionDTO.OrdenSesion = sesion.OrdenFila;
                                            var subSesiones = r.ListaSesiones.Where(x => x.Sesion != null).Where(x => x.Sesion.Equals(se)).ToList();
                                            List<DatosEstructuraEspecificaSubSesionDTO> listaSubSesionBO = new List<DatosEstructuraEspecificaSubSesionDTO>();
                                            foreach (var sub in subSesiones)
                                            {
                                                if (sub.SubSesion != null)
                                                {
                                                    if (sub.SubSesion.Trim() != "")
                                                    {
                                                        DatosEstructuraEspecificaSubSesionDTO subSesionBO = new DatosEstructuraEspecificaSubSesionDTO();
                                                        //subSesionBO.IdEstructuraEspecificaSesion = IdSesion;
                                                        subSesionBO.Numero = sesion.OrdenFila;
                                                        subSesionBO.SubSesion = sub.SubSesion;
                                                        //subSesisubSesionBOonBO.IdEstructuraEspecifica = IdEstructuraEspecifica;
                                                        listaSubSesionBO.Add(subSesionBO);
                                                    }

                                                }
                                            }
                                            sesionDTO.SubSesion = listaSubSesionBO;
                                            Sesion.Add(sesionDTO);
                                        }
                                        capituloDTO.Sesion = Sesion;
                                    }
                                    listaCapitulos.Add(capituloDTO);

                                }
                            }


                        }
                        estructura.Capitulo = listaCapitulos;
                        listaDatosEstructura.DatosEstructura.Add(estructura);
                    }
                }
                   
            }
            _repEstructuraEspecifica.CongelarEstructuraAlumno(listaDatosEstructura.DatosEstructura, listaDatosEstructura.Usuario);
            return listaDatosEstructura;

            
        }
        public int Insertar()
        {
            EstructuraEspecificaRepositorio _repEstructuraEspecifica = new EstructuraEspecificaRepositorio();
            _repEstructuraEspecifica.Insert(this);
            return this.Id;
        }
        public bool VerificarCongelamientoEstructura(int IdCodigoMatricula)
        {
            EstructuraEspecificaRepositorio _repEstructuraEspecifica = new EstructuraEspecificaRepositorio();
            var congelamiento = _repEstructuraEspecifica.VerificacionEstructuraCongelada(IdCodigoMatricula);
            return congelamiento;
        }

        public int Actualizar()
        {
            EstructuraEspecificaRepositorio _repEstructuraEspecifica = new EstructuraEspecificaRepositorio();
            _repEstructuraEspecifica.Update(this);
            return this.Id;
        }

        public int Eliminar(string usuario)
        {
            EstructuraEspecificaRepositorio _repEstructuraEspecifica = new EstructuraEspecificaRepositorio();
            _repEstructuraEspecifica.Delete(this.Id, usuario);
            return this.Id;
        }

        public List<CapitulosSesionesProgramaBO> ConseguirEstructuraPorPrograma(int idProgramaGeneral)
        {
            ConfigurarVideoProgramaRepositorio _configurarVideoProgramaRepositorio = new ConfigurarVideoProgramaRepositorio();
            List<EstructuraCapituloProgramaBO> lista = new List<EstructuraCapituloProgramaBO>();

            var respuesta = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoPrograma(idProgramaGeneral);
            if (respuesta != null)
            {
                var _listadoEstructura = (from x in respuesta
                                          group x by x.NumeroFila into newGroup
                                          select newGroup).ToList();
                foreach (var item in _listadoEstructura)
                {
                    EstructuraCapituloProgramaBO objeto = new EstructuraCapituloProgramaBO();
                    objeto.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                objeto.Nombre = itemRegistros.Nombre;
                                objeto.Capitulo = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdPgeneral = itemRegistros.IdPgeneral;
                                break;
                            case "Sesion":
                                objeto.Sesion = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                break;
                            case "SubSeccion":
                                objeto.SubSesion = itemRegistros.Contenido;
                                if (!string.IsNullOrEmpty(objeto.SubSesion))
                                {
                                    objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                    objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                }
                                break;
                            default:
                                objeto.OrdenCapitulo = !Regex.IsMatch(itemRegistros.Contenido, @"^[0-9]+$") ?1:Convert.ToInt32(itemRegistros.Contenido);
                                objeto.TotalSegundos = itemRegistros.TotalSegundos;
                                break;
                        }
                    }
                    lista.Add(objeto);
                }

                var rpta = lista.OrderBy(x => x.OrdenFila).ToList();

                var _listas = rpta.GroupBy(x => new { x.IdPgeneral, x.Nombre, x.Capitulo, x.OrdenCapitulo });

                List<CapitulosSesionesProgramaBO> _listaRegistro = new List<CapitulosSesionesProgramaBO>();

                foreach (var capitulo in _listas)
                {
                    CapitulosSesionesProgramaBO _registro = new CapitulosSesionesProgramaBO();
                    _registro.IdPgeneral = capitulo.Key.IdPgeneral;
                    _registro.Nombre = capitulo.Key.Nombre;
                    _registro.Capitulo = capitulo.Key.Capitulo;
                    _registro.OrdenFila = capitulo.Key.OrdenCapitulo;

                    _registro.ListaSesiones = new List<EstructuraCapituloProgramaBO>();

                    foreach (var sesiones in capitulo)
                    {
                        EstructuraCapituloProgramaBO _sesion = new EstructuraCapituloProgramaBO();
                        _sesion.Sesion = sesiones.Sesion;
                        _sesion.OrdenFila = sesiones.OrdenFila;
                        _sesion.OrdenCapitulo = sesiones.OrdenCapitulo;
                        _sesion.OrdenSeccion = sesiones.OrdenSeccion;
                        _sesion.TotalSegundos = sesiones.TotalSegundos;
                        _sesion.SubSesion = sesiones.SubSesion;

                        _registro.ListaSesiones.Add(_sesion);
                    }
                    _listaRegistro.Add(_registro);


                }

                var _respuestaPreFinal = _listaRegistro.OrderBy(x => x.Capitulo).ToList();

                foreach (var item in _respuestaPreFinal)
                {
                    var _rptaEvaluacion = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoProgramaEvaluaciones(item.IdPgeneral, item.OrdenFila);
                    var _rptaExamenes = _configurarVideoProgramaRepositorio.ObtenerPreConfigurarVideoProgramaEncuestas(item.IdPgeneral, item.OrdenFila);

                    if (_rptaEvaluacion != null && _rptaEvaluacion.Count > 0)
                    {
                        foreach (var itemEvaluacion in _rptaEvaluacion)
                        {
                            CapitulosSesionesProgramaBO _registroEvaluacion = new CapitulosSesionesProgramaBO();
                            _registroEvaluacion.Id = itemEvaluacion.Id;
                            _registroEvaluacion.IdDocumentoSeccionPw = itemEvaluacion.IdSeccionTipoDetalle_PW;
                            _registroEvaluacion.IdPgeneral = itemEvaluacion.IdPgeneral;
                            _registroEvaluacion.Nombre = itemEvaluacion.Nombre;
                            _registroEvaluacion.Capitulo = itemEvaluacion.Contenido;
                            _registroEvaluacion.OrdenFila = itemEvaluacion.NumeroFila;

                            _registroEvaluacion.ListaSesiones = new List<EstructuraCapituloProgramaBO>();
                            _listaRegistro.Add(_registroEvaluacion);
                        }

                    }

                    if (_rptaExamenes != null && _rptaExamenes.Count > 0)
                    {
                        foreach (var itemEvaluacion in _rptaExamenes)
                        {
                            CapitulosSesionesProgramaBO _registroEvaluacion = new CapitulosSesionesProgramaBO();
                            _registroEvaluacion.Id = itemEvaluacion.Id;
                            _registroEvaluacion.IdDocumentoSeccionPw = itemEvaluacion.IdSeccionTipoDetalle_PW;
                            _registroEvaluacion.IdPgeneral = itemEvaluacion.IdPgeneral;
                            _registroEvaluacion.Nombre = itemEvaluacion.Nombre;
                            _registroEvaluacion.Capitulo = itemEvaluacion.Contenido;
                            _registroEvaluacion.OrdenFila = itemEvaluacion.NumeroFila;

                            _registroEvaluacion.ListaSesiones = new List<EstructuraCapituloProgramaBO>();
                            _listaRegistro.Add(_registroEvaluacion);
                        }

                    }
                }

                var listaFinal = _listaRegistro.OrderBy(x => x.Capitulo).ToList();
                return listaFinal;
            }
            else {
                return null;
            }
            
        }
    }
}
