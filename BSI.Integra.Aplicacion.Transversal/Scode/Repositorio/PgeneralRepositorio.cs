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
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Planificacion/PGeneral
    /// Autor: Fischer Valdez - Jose Villena - Esthephany Tanco - Carlos Crispin - Wilber Choque - Priscila Pacsi -Luis Huallpa - Alexsandra Flores - Gian Miranda - Edgar S.
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_PGeneral
    /// </summary>
    public class PgeneralRepositorio : BaseRepository<TPgeneral, PgeneralBO>
    {
        #region Metodos Base
        public PgeneralRepositorio() : base()
        {
        }
        public PgeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralBO> GetBy(Expression<Func<TPgeneral, bool>> filter)
        {
            IEnumerable<TPgeneral> listado = base.GetBy(filter);
            List<PgeneralBO> listadoBO = new List<PgeneralBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralBO objetoBO = Mapper.Map<TPgeneral, PgeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralBO FirstById(int id)
        {
            try
            {
                TPgeneral entidad = base.FirstById(id);
                PgeneralBO objetoBO = new PgeneralBO();
                Mapper.Map<TPgeneral, PgeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralBO FirstBy(Expression<Func<TPgeneral, bool>> filter)
        {
            try
            {
                TPgeneral entidad = base.FirstBy(filter);
                PgeneralBO objetoBO = Mapper.Map<TPgeneral, PgeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralBO> listadoBO)
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

        public bool Update(PgeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralBO> listadoBO)
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
        private void AsignacionId(TPgeneral entidad, PgeneralBO objetoBO)
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

        private TPgeneral MapeoEntidad(PgeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneral entidad = new TPgeneral();
                entidad = Mapper.Map<PgeneralBO, TPgeneral>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.PGeneralParametroSeoPw != null && objetoBO.PGeneralParametroSeoPw.Count > 0)
                {
                    foreach (var hijo in objetoBO.PGeneralParametroSeoPw)
                    {
                        TPgeneralParametroSeoPw entidadHijo = new TPgeneralParametroSeoPw();
                        entidadHijo = Mapper.Map<PgeneralParametroSeoPwBO, TPgeneralParametroSeoPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPgeneralParametroSeoPw.Add(entidadHijo);
                    }
                }
                if (objetoBO.PgeneralDescripcion != null && objetoBO.PgeneralDescripcion.Count > 0)
                {
                    foreach (var hijo in objetoBO.PgeneralDescripcion)
                    {
                        TPgeneralDescripcion entidadHijo = new TPgeneralDescripcion();
                        entidadHijo = Mapper.Map<PgeneralDescripcionBO, TPgeneralDescripcion>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPgeneralDescripcion.Add(entidadHijo);
                    }
                }
                if (objetoBO.AdicionalProgramaGeneral != null && objetoBO.AdicionalProgramaGeneral.Count > 0)
                {
                    foreach (var hijo in objetoBO.AdicionalProgramaGeneral)
                    {
                        TAdicionalProgramaGeneral entidadHijo = new TAdicionalProgramaGeneral();
                        entidadHijo = Mapper.Map<AdicionalProgramaGeneralBO, TAdicionalProgramaGeneral>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TAdicionalProgramaGeneral.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaAreaRelacionada != null && objetoBO.ProgramaAreaRelacionada.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaAreaRelacionada)
                    {
                        TProgramaAreaRelacionada entidadHijo = new TProgramaAreaRelacionada();
                        entidadHijo = Mapper.Map<ProgramaAreaRelacionadaBO, TProgramaAreaRelacionada>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaAreaRelacionada.Add(entidadHijo);
                    }
                }
                if (objetoBO.PgeneralExpositor != null && objetoBO.PgeneralExpositor.Count > 0)
                {
                    foreach (var hijo in objetoBO.PgeneralExpositor)
                    {
                        TPgeneralExpositor entidadHijo = new TPgeneralExpositor();
                        entidadHijo = Mapper.Map<PgeneralExpositorBO, TPgeneralExpositor>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPgeneralExpositor.Add(entidadHijo);
                    }
                }
                if (objetoBO.SuscripcionProgramaGeneral != null && objetoBO.SuscripcionProgramaGeneral.Count > 0)
                {
                    foreach (var hijo in objetoBO.SuscripcionProgramaGeneral)
                    {
                        TSuscripcionProgramaGeneral entidadHijo = new TSuscripcionProgramaGeneral();
                        entidadHijo = Mapper.Map<SuscripcionProgramaGeneralBO, TSuscripcionProgramaGeneral>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSuscripcionProgramaGeneral.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilScoringCiudad != null && objetoBO.ProgramaGeneralPerfilScoringCiudad.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilScoringCiudad)
                    {
                        TProgramaGeneralPerfilScoringCiudad entidadHijo = new TProgramaGeneralPerfilScoringCiudad();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilScoringCiudadBO, TProgramaGeneralPerfilScoringCiudad>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilScoringCiudad.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilScoringModalidad != null && objetoBO.ProgramaGeneralPerfilScoringModalidad.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilScoringModalidad)
                    {
                        TProgramaGeneralPerfilScoringModalidad entidadHijo = new TProgramaGeneralPerfilScoringModalidad();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilScoringModalidadBO, TProgramaGeneralPerfilScoringModalidad>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilScoringModalidad.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilScoringAformacion != null && objetoBO.ProgramaGeneralPerfilScoringAformacion.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilScoringAformacion)
                    {
                        TProgramaGeneralPerfilScoringAformacion entidadHijo = new TProgramaGeneralPerfilScoringAformacion();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilScoringAformacionBO, TProgramaGeneralPerfilScoringAformacion>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilScoringAformacion.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilScoringIndustria != null && objetoBO.ProgramaGeneralPerfilScoringIndustria.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilScoringIndustria)
                    {
                        TProgramaGeneralPerfilScoringIndustria entidadHijo = new TProgramaGeneralPerfilScoringIndustria();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilScoringIndustriaBO, TProgramaGeneralPerfilScoringIndustria>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilScoringIndustria.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilScoringCargo != null && objetoBO.ProgramaGeneralPerfilScoringCargo.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilScoringCargo)
                    {
                        TProgramaGeneralPerfilScoringCargo entidadHijo = new TProgramaGeneralPerfilScoringCargo();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilScoringCargoBO, TProgramaGeneralPerfilScoringCargo>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilScoringCargo.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilScoringAtrabajo != null && objetoBO.ProgramaGeneralPerfilScoringAtrabajo.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilScoringAtrabajo)
                    {
                        TProgramaGeneralPerfilScoringAtrabajo entidadHijo = new TProgramaGeneralPerfilScoringAtrabajo();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilScoringAtrabajoBO, TProgramaGeneralPerfilScoringAtrabajo>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilScoringAtrabajo.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilScoringCategoria != null && objetoBO.ProgramaGeneralPerfilScoringCategoria.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilScoringCategoria)
                    {
                        TProgramaGeneralPerfilScoringCategoria entidadHijo = new TProgramaGeneralPerfilScoringCategoria();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilScoringCategoriaBO, TProgramaGeneralPerfilScoringCategoria>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilScoringCategoria.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilCiudadCoeficiente != null && objetoBO.ProgramaGeneralPerfilCiudadCoeficiente.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilCiudadCoeficiente)
                    {
                        TProgramaGeneralPerfilCiudadCoeficiente entidadHijo = new TProgramaGeneralPerfilCiudadCoeficiente();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilCiudadCoeficienteBO, TProgramaGeneralPerfilCiudadCoeficiente>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilCiudadCoeficiente.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilModalidadCoeficiente != null && objetoBO.ProgramaGeneralPerfilModalidadCoeficiente.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilModalidadCoeficiente)
                    {
                        TProgramaGeneralPerfilModalidadCoeficiente entidadHijo = new TProgramaGeneralPerfilModalidadCoeficiente();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilModalidadCoeficienteBO, TProgramaGeneralPerfilModalidadCoeficiente>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilModalidadCoeficiente.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilCategoriaCoeficiente != null && objetoBO.ProgramaGeneralPerfilCategoriaCoeficiente.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilCategoriaCoeficiente)
                    {
                        TProgramaGeneralPerfilCategoriaCoeficiente entidadHijo = new TProgramaGeneralPerfilCategoriaCoeficiente();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilCategoriaCoeficienteBO, TProgramaGeneralPerfilCategoriaCoeficiente>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilCategoriaCoeficiente.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilCargoCoeficiente != null && objetoBO.ProgramaGeneralPerfilCargoCoeficiente.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilCargoCoeficiente)
                    {
                        TProgramaGeneralPerfilCargoCoeficiente entidadHijo = new TProgramaGeneralPerfilCargoCoeficiente();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilCargoCoeficienteBO, TProgramaGeneralPerfilCargoCoeficiente>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilCargoCoeficiente.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilIndustriaCoeficiente != null && objetoBO.ProgramaGeneralPerfilIndustriaCoeficiente.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilIndustriaCoeficiente)
                    {
                        TProgramaGeneralPerfilIndustriaCoeficiente entidadHijo = new TProgramaGeneralPerfilIndustriaCoeficiente();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilIndustriaCoeficienteBO, TProgramaGeneralPerfilIndustriaCoeficiente>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilIndustriaCoeficiente.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilAformacionCoeficiente != null && objetoBO.ProgramaGeneralPerfilAformacionCoeficiente.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilAformacionCoeficiente)
                    {
                        TProgramaGeneralPerfilAformacionCoeficiente entidadHijo = new TProgramaGeneralPerfilAformacionCoeficiente();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilAformacionCoeficienteBO, TProgramaGeneralPerfilAformacionCoeficiente>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilAformacionCoeficiente.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilAtrabajoCoeficiente != null && objetoBO.ProgramaGeneralPerfilAtrabajoCoeficiente.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilAtrabajoCoeficiente)
                    {
                        TProgramaGeneralPerfilAtrabajoCoeficiente entidadHijo = new TProgramaGeneralPerfilAtrabajoCoeficiente();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilAtrabajoCoeficienteBO, TProgramaGeneralPerfilAtrabajoCoeficiente>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilAtrabajoCoeficiente.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilTipoDato != null && objetoBO.ProgramaGeneralPerfilTipoDato.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilTipoDato)
                    {
                        TProgramaGeneralPerfilTipoDato entidadHijo = new TProgramaGeneralPerfilTipoDato();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilTipoDatoBO, TProgramaGeneralPerfilTipoDato>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilTipoDato.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilEscalaProbabilidad != null && objetoBO.ProgramaGeneralPerfilEscalaProbabilidad.Count > 0)
                {
                    foreach (var hijo in objetoBO.ProgramaGeneralPerfilEscalaProbabilidad)
                    {
                        TProgramaGeneralPerfilEscalaProbabilidad entidadHijo = new TProgramaGeneralPerfilEscalaProbabilidad();
                        entidadHijo = Mapper.Map<ProgramaGeneralPerfilEscalaProbabilidadBO, TProgramaGeneralPerfilEscalaProbabilidad>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProgramaGeneralPerfilEscalaProbabilidad.Add(entidadHijo);
                    }
                }
                if (objetoBO.ProgramaGeneralPerfilIntercepto != null)
                {
                    TProgramaGeneralPerfilIntercepto entidadHijo = new TProgramaGeneralPerfilIntercepto();
                    entidadHijo = Mapper.Map<ProgramaGeneralPerfilInterceptoBO, TProgramaGeneralPerfilIntercepto>(objetoBO.ProgramaGeneralPerfilIntercepto,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TProgramaGeneralPerfilIntercepto.Add(entidadHijo);

                }
                if (objetoBO.ModeloPredictivoIndustria != null && objetoBO.ModeloPredictivoIndustria.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloPredictivoIndustria)
                    {
                        TModeloPredictivoIndustria entidadHijo = new TModeloPredictivoIndustria();
                        entidadHijo = Mapper.Map<ModeloPredictivoIndustriaBO, TModeloPredictivoIndustria>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloPredictivoIndustria.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloPredictivoCargo != null && objetoBO.ModeloPredictivoCargo.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloPredictivoCargo)
                    {
                        TModeloPredictivoCargo entidadHijo = new TModeloPredictivoCargo();
                        entidadHijo = Mapper.Map<ModeloPredictivoCargoBO, TModeloPredictivoCargo>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloPredictivoCargo.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloPredictivoFormacion != null && objetoBO.ModeloPredictivoFormacion.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloPredictivoFormacion)
                    {
                        TModeloPredictivoFormacion entidadHijo = new TModeloPredictivoFormacion();
                        entidadHijo = Mapper.Map<ModeloPredictivoFormacionBO, TModeloPredictivoFormacion>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloPredictivoFormacion.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloPredictivoTrabajo != null && objetoBO.ModeloPredictivoTrabajo.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloPredictivoTrabajo)
                    {
                        TModeloPredictivoTrabajo entidadHijo = new TModeloPredictivoTrabajo();
                        entidadHijo = Mapper.Map<ModeloPredictivoTrabajoBO, TModeloPredictivoTrabajo>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloPredictivoTrabajo.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloPredictivoCategoriaDato != null && objetoBO.ModeloPredictivoCategoriaDato.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloPredictivoCategoriaDato)
                    {
                        TModeloPredictivoCategoriaDato entidadHijo = new TModeloPredictivoCategoriaDato();
                        entidadHijo = Mapper.Map<ModeloPredictivoCategoriaDatoBO, TModeloPredictivoCategoriaDato>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloPredictivoCategoriaDato.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloPredictivoTipoDato != null && objetoBO.ModeloPredictivoTipoDato.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloPredictivoTipoDato)
                    {
                        TModeloPredictivoTipoDato entidadHijo = new TModeloPredictivoTipoDato();
                        entidadHijo = Mapper.Map<ModeloPredictivoTipoDatoBO, TModeloPredictivoTipoDato>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloPredictivoTipoDato.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloPredictivoEscalaProbabilidad != null && objetoBO.ModeloPredictivoEscalaProbabilidad.Count > 0)
                {
                    foreach (var hijo in objetoBO.ModeloPredictivoEscalaProbabilidad)
                    {
                        TModeloPredictivoEscalaProbabilidad entidadHijo = new TModeloPredictivoEscalaProbabilidad();
                        entidadHijo = Mapper.Map<ModeloPredictivoEscalaProbabilidadBO, TModeloPredictivoEscalaProbabilidad>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TModeloPredictivoEscalaProbabilidad.Add(entidadHijo);
                    }
                }
                if (objetoBO.PgeneralModalidad != null && objetoBO.PgeneralModalidad.Count > 0)
                {
                    foreach (var hijo in objetoBO.PgeneralModalidad)
                    {
                        TPgeneralModalidad entidadHijo = new TPgeneralModalidad();
                        entidadHijo = Mapper.Map<PgeneralModalidadBO, TPgeneralModalidad>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPgeneralModalidad.Add(entidadHijo);
                    }
                }
                if (objetoBO.ModeloPredictivo != null)
                {
                    TModeloPredictivo entidadHijo = new TModeloPredictivo();
                    entidadHijo = Mapper.Map<ModeloPredictivoBO, TModeloPredictivo>(objetoBO.ModeloPredictivo,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TModeloPredictivo.Add(entidadHijo);

                }
                if (objetoBO.PgeneralVersionPrograma != null && objetoBO.PgeneralVersionPrograma.Count > 0)
                {
                    foreach (var hijo in objetoBO.PgeneralVersionPrograma)
                    {
                        TPgeneralVersionPrograma entidadHijo = new TPgeneralVersionPrograma();
                        entidadHijo = Mapper.Map<PgeneralVersionProgramaBO, TPgeneralVersionPrograma>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPgeneralVersionPrograma.Add(entidadHijo);
                    }
                }
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene los datos de un programa general especifico
        /// </summary>
        /// <returns>Objeto de tipo PgeneralDTO</returns>
        public PgeneralDTO ObtenerPgeneralporId(int id)
        {
            try
            {
                PgeneralDTO pgeneral = new PgeneralDTO();
                string queryPGeneral = "Select Id,IdPGeneral,Nombre,Pw_ImgPortada,Pw_ImgPortadaAlf,Pw_ImgSecundaria,Pw_ImgSecundariaAlf,IdPartner,IdArea,IdSubArea,IdCategoria,Pw_estado" +
                                        ",Pw_mostrarBSPlay,pw_duracion,IdBusqueda,IdChatZopim,Pg_titulo,Codigo,UrlImagenPortadaFr,UrlBrochurePrograma,UrlPartner,UrlVersion,Pw_tituloHtml," +
                                        "EsModulo,NombreCorto,IdPagina,ChatActivo From pla.V_TPGeneral_ObtenerDatos Where Id=@Id and Estado=1";
                var programaGeneral = _dapper.FirstOrDefault(queryPGeneral, new { Id = id });
                if (!string.IsNullOrEmpty(programaGeneral) && !programaGeneral.Equals("null"))
                {
                    pgeneral = JsonConvert.DeserializeObject<PgeneralDTO>(programaGeneral);
                }
                return pgeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PgeneralDocumentoSeccionDTO ObtenePgeneralPorIdBusqueda(int idBusqueda)
        {
            try
            {
                string _queryPrograma = "Select Id,Nombre from pla.V_TPgeneral_PorIdBusqueda where IdBusqueda=@IdBusqueda";
                var queryPrgrama = _dapper.FirstOrDefault(_queryPrograma, new { IdBusqueda = idBusqueda });
                return JsonConvert.DeserializeObject<PgeneralDocumentoSeccionDTO>(queryPrgrama);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public PgeneralDocumentoSeccionDTO ObtenePgeneralDocumentoPorId(int id)
        {
            try
            {
                string _queryPrograma = "Select Id,Nombre,pw_duracion from pla.V_TPgeneral_PorIdBusqueda where Id=@Id";
                var queryPrgrama = _dapper.FirstOrDefault(_queryPrograma, new { Id = id });
                return JsonConvert.DeserializeObject<PgeneralDocumentoSeccionDTO>(queryPrgrama);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public CuotasProgramaDTO ObtenerProgramaParaCuotas(int idMatricula)
        {
            try
            {
                string _queryCuotasProgramaDTO = "Select IdBusqueda,NombreCurso,IdPespecifico,NombrePespecifico,IdMatricula,CodigoMatricula,TipoPrograma,DuracionPgeneral,DuracionPespecifico,NumeroCuotas " +
                    ",WebMoneda,TotalPagar, WebTotalPagar,WebTipoCambio,EstadoCronogramaMod  From com.V_CuotascronogramaPorMatricula where IdMatricula=@IdMatricula and EstadoCronogramaMod = 1";
                var _CuotasProgramaDTO = _dapper.FirstOrDefault(_queryCuotasProgramaDTO, new { IdMatricula = idMatricula });
                return JsonConvert.DeserializeObject<CuotasProgramaDTO>(_CuotasProgramaDTO);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: PgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene las frecuencias de un programa general
        /// </summary>
        /// <param name="id">Id Programa General </param>
        /// <returns> Lista Frecuencia Programa General: List<FrecuenciaProgramaGeneralDTO></returns>  
        public List<FrecuenciaProgramaGeneralDTO> ObtenerFrecuenciasPorIdPGeneral(int id)
        {
            try
            {
                List<FrecuenciaProgramaGeneralDTO> frecuenciasProgramaGeneral = new List<FrecuenciaProgramaGeneralDTO>();
                string queryPGeneral = "SELECT IdPEspecifico, Nombre FROM pla.V_ObtenerFrecuenciaPGeneral WHERE IdProgramaGeneral = @id AND EstadoPEspecifico = 1 AND EstadoPGeneral = 1";
                var programaGeneral = _dapper.QueryDapper(queryPGeneral, new { id });
                frecuenciasProgramaGeneral = JsonConvert.DeserializeObject<List<FrecuenciaProgramaGeneralDTO>>(programaGeneral);
                return frecuenciasProgramaGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las los padreEspecifico e hijoEspecifico de un programa general
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PadrePespecificoHijoDTO> ObtenerPadreHijoEspecifico(int id)
        {
            try
            {
                List<PadrePespecificoHijoDTO> padresPespecificoHijos = new List<PadrePespecificoHijoDTO>();
                string _queryPGeneral = "SELECT Id,IdPEspecificoPadre,IdPespecificoHijo FROM pla.V_ObtenerPadreEspecificoHijoEspecifico WHERE IdProgramaGeneral = @id";
                var _programaGeneral = _dapper.QueryDapper(_queryPGeneral, new { id });
                padresPespecificoHijos = JsonConvert.DeserializeObject<List<PadrePespecificoHijoDTO>>(_programaGeneral);
                return padresPespecificoHijos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        ///Repositorio: PgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene los padreEspecifico e hijoEspecifico de un programa general con restriccion de Lanzamiento y Estado
        /// </summary>
        /// <param name="id">Id Programa General </param>
        /// <returns> Lista Programa Especifico Padre e Hijo : List<PadrePespecificoHijoDTO></returns>   
        public List<PadrePespecificoHijoDTO> ObtenerPadreHijoEspecificoV2(int id)
        {
            try
            {
                List<PadrePespecificoHijoDTO> padresPespecificoHijos = new List<PadrePespecificoHijoDTO>();
                string queryPGeneral = "SELECT Id,IdPEspecificoPadre,IdPespecificoHijo FROM pla.V_ObtenerPadreEspecificoHijoEspecificoV2 WHERE IdProgramaGeneral = @id AND EstadoPGeneral = 1";
                var programaGeneral = _dapper.QueryDapper(queryPGeneral, new { id });
                padresPespecificoHijos = JsonConvert.DeserializeObject<List<PadrePespecificoHijoDTO>>(programaGeneral);
                return padresPespecificoHijos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: PgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene las sesiones de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> Lista Sesiones Programa General: List<PEspecificoSesionDTO></returns>  
        public List<PEspecificoSesionDTO> ObtenerSesionesPorProgramaGeneral(int idPGeneral)
        {
            try
            {
                List<PEspecificoSesionDTO> pEspecificoSesiones = new List<PEspecificoSesionDTO>();
                var sesionesPEspecifico = _dapper.QuerySPDapper("pla.SP_GetPEspecificoSesion", new { idPGeneral });
                pEspecificoSesiones = JsonConvert.DeserializeObject<List<PEspecificoSesionDTO>>(sesionesPEspecifico);
                return pEspecificoSesiones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: PgeneralRepositorio
        ///Autor: Edgar Serruto
        ///Fecha: 30/09/2021
        /// <summary>
        /// Obtiene las sesiones de un programa general validando las sesiones configuradas para si visualización
        /// </summary>
        /// <param name="idPGeneral">FK de T_Pgeneral</param>
        /// <returns>List<PEspecificoSesionDTO></returns>  
        public List<PEspecificoSesionDTO> ObtenerSesionesProgramaGeneralValidadoVisualizacionAgenda(int idPGeneral)
        {
            try
            {
                List<PEspecificoSesionDTO> pEspecificoSesiones = new List<PEspecificoSesionDTO>();
                var sesionesPEspecifico = _dapper.QuerySPDapper("pla.SP_ObtenerSesionesValidadoVisualizacion", new { idPGeneral });
                pEspecificoSesiones = JsonConvert.DeserializeObject<List<PEspecificoSesionDTO>>(sesionesPEspecifico);
                return pEspecificoSesiones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: PgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene los beneficios por programa general y pais tipo 1
        /// </summary>
        /// <param name="idPGeneral">Id del PGeneral </param>
        /// <param name="codigoPais">Codigo del pais </param>
        /// <returns> Lista de objeto:BeneficioDTO</returns>   
        public List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1(int idPGeneral, int codigoPais = 0)
        {
            List<BeneficioDTO> beneficios = new List<BeneficioDTO>();
            //var query = "SELECT Paquete, Titulo, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @idPGeneral And CodigoPais = @codigoPais AND EstadoMontoPago = 1 AND EstadoMontoPagoSuscripcion = 1 AND EstadoSuscripcionProgramaGeneral = 1 AND Paquete IS NOT NULL";
            var query = "SELECT Paquete, Titulo, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @idPGeneral And CodigoPais = @codigoPais";
            var beneficiosDB = _dapper.QueryDapper(query, new { idPGeneral, codigoPais });
            if (!beneficiosDB.Contains("[]"))
            {
                beneficios = JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB);
            }
            
            return beneficios;
        }

        /// <summary>
        /// Obtiene los beneficios por programa general y pais
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <param name="codigoPais"></param>
        /// <param name="tipopaquete"></param>
        /// <returns></returns>
        public List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1Convenio(int idPGeneral, int tipoPaquete, int codigoPais = 0)
        {
            List<BeneficioDTO> beneficios = new List<BeneficioDTO>();
            var _query = "SELECT Paquete, Titulo, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @idPGeneral And CodigoPais = @codigoPais AND EstadoMontoPago = 1 AND EstadoMontoPagoSuscripcion = 1 AND EstadoSuscripcionProgramaGeneral = 1 AND Paquete IS NOT NULL";

            if (tipoPaquete == 1)
            {
                _query += " and Paquete in (1) and OrdenBeneficio <> 1";
            }
            else if (tipoPaquete == 2)
            {
                _query += " and Paquete in (1,2) and OrdenBeneficio <> 1";
            }
            else if (tipoPaquete == 3)
            {
                _query += " and Paquete in (1,2,3) and OrdenBeneficio <> 1";
            }

            var beneficiosDB = _dapper.QueryDapper(_query, new { idPGeneral, codigoPais });
            if (!beneficiosDB.Contains("[]"))
            {
                beneficios = JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB);
            }

            return beneficios;
        }

  
        ///Repositorio: PgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene el titulo de un beneficio por programa general tipo 2
        /// </summary>
        /// <param name="idPGeneral">Id del PGeneral </param>
        /// <returns>Objeto del tipo BeneficioDTO</returns>   
        public BeneficioDTO ObtenerBeneficiosPGeneralTipo2(int idPGeneral)
        {
            BeneficioDTO beneficio = new BeneficioDTO();
            var query = "SELECT Titulo FROM pla.V_BeneficiosProgramaTipo2 WHERE TituloDocumentoSeccion = @nombre AND IdProgramaGeneral = @idPGeneral AND EstadoDocumentoSeccion = 1 AND EstadoProgramaGeneralDocumento = 1 AND EstadoDocumento = 1 AND EstadoProgramaGeneral = 1";
            var beneficiosDB = _dapper.FirstOrDefault(query, new { idPGeneral, nombre = "Beneficios" });
            if (!string.IsNullOrEmpty(beneficiosDB) && !beneficiosDB.Equals("null"))
            {
                beneficio = JsonConvert.DeserializeObject<BeneficioDTO>(beneficiosDB);
            }            
            return beneficio;
        }

        ///Repositorio: PgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener beneficios del programa general Tipo 1 version 2 Internacional
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> Lista Beneficios: List<BeneficioDTO></returns>   
        public List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1V2Internacional(int idPGeneral)
        {
            List<BeneficioDTO> beneficios = new List<BeneficioDTO>();
            //var query = "SELECT Paquete, Titulo, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @idPGeneral And CodigoPais = @codigoPais AND EstadoMontoPago = 1 AND EstadoMontoPagoSuscripcion = 1 AND EstadoSuscripcionProgramaGeneral = 1 AND Paquete IS NOT NULL";
            var query = "SELECT Paquete, Titulo, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1V2 WHERE Id = @idPGeneral group by OrdenBeneficio,Paquete,Titulo";
            var beneficiosDB = _dapper.QueryDapper(query, new { idPGeneral });
            if (!beneficiosDB.Contains("[]"))
            {
                beneficios = JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB);
            }

            return beneficios;
        }

        ///Repositorio: PgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener beneficios del programa general Tipo 1 version 2
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <param name="codigoPais">Codigo pais </param>
        /// <returns> Lista Beneficios: List<BeneficioDTO></returns> 
        public List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1V2(int idPGeneral, int codigoPais = 0)
        {
            List<BeneficioDTO> beneficios = new List<BeneficioDTO>();
            //var query = "SELECT Paquete, Titulo, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @idPGeneral And CodigoPais = @codigoPais AND EstadoMontoPago = 1 AND EstadoMontoPagoSuscripcion = 1 AND EstadoSuscripcionProgramaGeneral = 1 AND Paquete IS NOT NULL";
            var query = "SELECT Paquete, Titulo, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1V2 WHERE Id = @idPGeneral And CodigoPais = @codigoPais";
            var beneficiosDB = _dapper.QueryDapper(query, new { idPGeneral, codigoPais });
            if (!beneficiosDB.Contains("[]"))
            {
                beneficios = JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB);
            }

            return beneficios;
        }

        /// <summary>
        /// Obtiene el titulo de un beneficio por programa general
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public BeneficioDTO ObtenerBeneficiosPGeneralTipo2Convenio(int idPGeneral)
        {
            BeneficioDTO beneficio = new BeneficioDTO();
            var _query = "SELECT Titulo FROM pla.V_BeneficiosProgramaTipo2 WHERE TituloDocumentoSeccion = @nombre AND IdProgramaGeneral = @idPGeneral AND EstadoDocumentoSeccion = 1 AND EstadoProgramaGeneralDocumento = 1 AND EstadoDocumento = 1 AND EstadoProgramaGeneral = 1";
            var beneficiosDB = _dapper.FirstOrDefault(_query, new { idPGeneral, nombre = "Beneficios" });
            if (!string.IsNullOrEmpty(beneficiosDB) && !beneficiosDB.Equals("null"))
            {
                beneficio = JsonConvert.DeserializeObject<BeneficioDTO>(beneficiosDB);
            }
            return beneficio;
        }


        ///Repositorio: PgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Retorna los paquetes, nombre paquete,precio y pais de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> Lista Monto Pago modalidad: List<MontoPagoModalidadDTO></returns>   
        public List<MontoPagoModalidadDTO> ObtenerMontosPorId(int idPGeneral)
        {
            List<MontoPagoModalidadDTO> montoPagoModalidad = new List<MontoPagoModalidadDTO>();
            var montoPagoModalidadDB = _dapper.QuerySPDapper("pla.SP_MontoPago", new { idPGeneral });
            montoPagoModalidad = JsonConvert.DeserializeObject<List<MontoPagoModalidadDTO>>(montoPagoModalidadDB);
            return montoPagoModalidad;
        }
        /// <summary>
        /// Retorna el monto pagado
        /// </summary>
        /// <param name="IdMatriculaCabecera"></param>
        /// <param name="idOportunidad"></param>
        public List<MontoPagadoDTO> ObtenerMontoPagado(int IdMatriculaCabecera, int idOportunidad)
        {
            List<MontoPagadoDTO> montoPagado = new List<MontoPagadoDTO>();
            var montoPagadoDB = _dapper.QuerySPDapper("ope.SP_MontoPagado", new { idOportunidad,IdMatriculaCabecera });
            montoPagado = JsonConvert.DeserializeObject<List<MontoPagadoDTO>>(montoPagadoDB);
            return montoPagado;
        }


        ///Repositorio: PgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene programa general, area y subArea por centro costo
        /// </summary>
        /// <param name="idCentroCosto">Id de centro de costo </param>
        /// <returns> Programa General: ProgramaDTO</returns>  
        public ProgramaDTO ObtenerPGeneralPEspecificoPorCentroCosto(int idCentroCosto)
        {
            try
            {
                ProgramaDTO programa = new ProgramaDTO();
                var query = "SELECT IdProgramaGeneral, IdArea, IdSubArea FROM pla.V_ObtenerPGeneralPEspecificoPorCentroCosto WHERE IdCentroCosto = @idCentroCosto AND EstadoPEspecifico = 1 AND EstadoPGeneral = 1";
                var RegistrosDB = _dapper.FirstOrDefault(query, new { idCentroCosto });
                programa = JsonConvert.DeserializeObject<ProgramaDTO>(RegistrosDB);
                return programa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el resumen de programas filtrados por area, subArea
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ResumenProgramaDTO> ObtenerResumenPrograma(Dictionary<string, string> filtros)
        {
            try
            {

                var filtro = this.ObtenerFiltro(filtros);
                List<ResumenProgramaDTO> resumenProgramas = new List<ResumenProgramaDTO>();
                var _query = "SELECT IdArea,IdSubArea,IdPGeneral,Duracion,Paquete,NombreVersion,NombrePrograma,InversionContado,InversionCredito,ContadoDolares,Pais,Orden,Certificacion FROM Pla.V_ResumenProgramas " + filtro.ToString();
                var resumenProgramaBD = _dapper.QueryDapper(_query, null);
                resumenProgramas = JsonConvert.DeserializeObject<List<ResumenProgramaDTO>>(resumenProgramaBD);
                return resumenProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PgeneralRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtener modalidades por programa general
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<ModalidadProgramaDTO> </returns>
        public List<ModalidadProgramaDTO> ObtenerModalidadesPorProgramaGeneral(int idPGeneral)
        {
            try
            {
                List<ModalidadProgramaDTO> modalidadPrograma = new List<ModalidadProgramaDTO>();
                var resumenProgramaBD = _dapper.QuerySPDapper("pla.SP_ObtenerModalidadesPorPrograma", new { idPGeneral });
                modalidadPrograma = JsonConvert.DeserializeObject<List<ModalidadProgramaDTO>>(resumenProgramaBD);
                return modalidadPrograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// <summary>
        /// Obtener modalidades por programa general
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ModalidadProgramaDTO> ObtenerFechaInicioProgramaGeneral(int idPGeneral , int IdCodigoPais)
        {
            try
            {
                List<ModalidadProgramaDTO> modalidadPrograma = new List<ModalidadProgramaDTO>();
                var resumenProgramaBD = _dapper.QuerySPDapper("pla.SP_ObtenerFechaInicioPrograma", new { idPGeneral,IdCodigoPais });

                if (resumenProgramaBD.Contains("[]"))
                {
                    return modalidadPrograma;
                }
                else 
                {
                    return JsonConvert.DeserializeObject<List<ModalidadProgramaDTO>>(resumenProgramaBD);                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PgeneralRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene la lista de secciones de un programa general
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<InformacionProgramaDTO> </returns>
        public List<InformacionProgramaDTO> ObtenerSeccionesInformacionProgramaPorProgramaGeneral(int idPGeneral)
        {
            try
            {
                List<InformacionProgramaDTO> informacionProgramas = new List<InformacionProgramaDTO>();
                var informacionProgramaDB = _dapper.QuerySPDapper("pla.SP_SeccionesInformacionPrograma", new { idPGeneral });
                informacionProgramas = JsonConvert.DeserializeObject<List<InformacionProgramaDTO>>(informacionProgramaDB);
                return informacionProgramas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de nombres de los programas (activos)  registradas en el sistema 
        ///  y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns>Lista de objetos de tipo PGeneralFiltroDTO</returns>
        public List<PGeneralFiltroDTO> ObtenerProgramasFiltro()
        {
            try
            {
                List<PGeneralFiltroDTO> programasGenerales = new List<PGeneralFiltroDTO>();
                var query = string.Empty;
                query = "SELECT Id, Nombre FROM pla.V_TPGeneral_Nombre WHERE Estado = 1";
                var pgeneralDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralFiltroDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// Repositorio: PgeneralRepositorio
        /// Autor: Jose Villena .
        /// Fecha: 16/12/2021
        /// <summary>
        /// Obtiene la lista de nombres de los programas (activos)  registradas en el sistema 
        /// </summary>        
        /// <returns> List<InformacionProgramaDTO> </returns>
        public List<PGeneralFiltroDTO> ObtenerProgramasGeneralesFiltro()
        {
            try
            {
                List<PGeneralFiltroDTO> programasGenerales = new List<PGeneralFiltroDTO>();
                var query = string.Empty;
                query = "SELECT Id, Nombre FROM pla.V_TPGeneral_Nombre WHERE Estado = 1 and Nombre NOT LIKE 'Webinar%' ORDER BY Id DESC";
                var pgeneralDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralFiltroDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id, Nombre y IdSubAreaCapacitacion
        /// </summary>
        /// <returns>Lista de objetos de clase PGeneralSubAreaFiltroDTO</returns>
        public List<PGeneralSubAreaFiltroDTO> ObtenerProgramaSubAreaFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new PGeneralSubAreaFiltroDTO { Id = x.Id, Nombre = x.Nombre, IdSubAreaCapacitacion = x.IdSubArea }).ToList();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id, Nombre y IdSubAreaCapacitacion, sin limitar solo los que se muestran en el portal web
        /// </summary>
        /// <returns>Lista de objetos de tipo (PGeneralSubAreaFiltroDTO)</returns>
        public List<PGeneralSubAreaFiltroDTO> ObtenerProgramaSubAreaFiltroTodo()
        {
            try
            {
                return this.GetAll().Select(s => new PGeneralSubAreaFiltroDTO { Id = s.Id, Nombre = s.Nombre, IdSubAreaCapacitacion = s.IdSubArea }).ToList();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id, Nombre y IdSubAreaCapacitacion, limitando solo los que se muestran en el portal web
        /// </summary>
        /// <returns>Lista de objetos de tipo (PGeneralSubAreaFiltroDTO)</returns>
        public List<PGeneralSubAreaFiltroDTO> ObtenerProgramaSubAreaFiltroSoloPortal()
        {
            try
            {
                return this.GetBy(x => x.Estado == true && x.IdPagina == 1, x => new PGeneralSubAreaFiltroDTO { Id = x.Id, Nombre = x.Nombre, IdSubAreaCapacitacion = x.IdSubArea }).ToList();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        
        /// <summary>
        /// Obtiene la lista de nombres de los programas (activos)  registradas en el sistema,sus IDs, Nombre e IDs del subarea
        /// al que pertenecen  (Usado para el llenado de combobox que depende de otro combobox).
        /// </summary>
        /// <returns></returns>
        public List<PGeneralFiltroSubAreaDTO> ObtenerProgramasFiltroDeSubAreasCapacitacion()
        {
            try
            {
                List<PGeneralFiltroSubAreaDTO> programasGenerales = new List<PGeneralFiltroSubAreaDTO>();
                var _query = string.Empty;
                _query = "SELECT  Id, Nombre, IdSubArea FROM pla.V_TPGeneral_ObtenerDatos WHERE Estado = 1";
                var pgeneralDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralFiltroSubAreaDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de los programas (activos)  registradas en el sistema [Id,Nombre,IdArea,IdSubArea,IdCategoria] 
        /// (Usado para el llenado de combobox).
        /// </summary>
        /// <returns></returns>
        public List<PGeneralFiltroSubAreaDTO> ObtenerProgramasConAreaSubAreaCategoriaFiltro()
        {
            try
            {
                List<PGeneralFiltroSubAreaDTO> programasGenerales = new List<PGeneralFiltroSubAreaDTO>();
                var _query = string.Empty;
                _query = "SELECT  Id, Nombre, IdArea, IdSubArea, IdCategoria FROM pla.V_TPGeneral_ObtenerDatos WHERE Estado = 1";
                var pgeneralDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralFiltroSubAreaDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public PgeneralAreaSubAreaDTO ObtenerAreaSubArea(int idPgeneral)
        {
            try
            {
                string _queryPGeneral = "Select IdArea, IdSubArea from pla.V_TPGeneral_AreaSubArea where Id=@Id and Estado=1";
                var _programaGeneral = _dapper.FirstOrDefault(_queryPGeneral, new { Id = idPgeneral });
                return JsonConvert.DeserializeObject<PgeneralAreaSubAreaDTO>(_programaGeneral);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public FiltroProgramaByCentroCostoDTO ObtenerDatosPFrecuentes(int idCentroCosto)
        {
            try
            {
                string _queryPEspecifico = "Select IdPGeneral, IdArea, IdSubArea, TipoId from pla.V_TPGeneral_ObtenerDatosPFrecuentes where IdCentroCosto = @IdCentroCosto and EstadoPG = 1 and EstadoPE = 1";
                var _programaEspecifico = _dapper.FirstOrDefault(_queryPEspecifico, new { IdCentroCosto = idCentroCosto });

                return JsonConvert.DeserializeObject<FiltroProgramaByCentroCostoDTO>(_programaEspecifico);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// obtiene el idPagina de un programa general por centro de costo
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        public PgeneralIdPaginaDTO ObtenerIdPagina(int idCentroCosto)
        {
            try
            {
                PgeneralIdPaginaDTO pgeneralIdPagina = new PgeneralIdPaginaDTO()
                {
                    Id = 0,
                    IdPagina = 0
                };
                string _query = "SELECT IdPagina FROM pla.V_ObtenerIdPagina WHERE IdCentroCosto = @idCentroCosto AND EstadoProgramaGeneral = 1 AND EstadoProgramaEspecifico = 1";
                var registrosDB = _dapper.FirstOrDefault(_query, new { idCentroCosto });
                pgeneralIdPagina = JsonConvert.DeserializeObject<PgeneralIdPaginaDTO>(registrosDB);
                return pgeneralIdPagina;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: PgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene un string, que es el filtro sql que se genera dependiendo de lo que se le envie
        /// </summary>
        /// <param name="filtros">Objeto tipo Dictionary<string, string> </param>
        /// <returns> Objeto: Filtro</returns>  
        private string ObtenerFiltro(Dictionary<string, string> filtros) {
            string filtro = string.Empty;
            int c = 0;
            foreach (var prop in filtros)
            {
                if (prop.Key == "codigoPais")
                {
                    continue;
                }
                if (prop.Key != null && prop.Value != null)
                {
                    if (c == 0)
                    {
                        filtro += " WHERE ";
                    }
                    else if (c > 0)
                    {
                        filtro += " AND ";
                    }

                    if (prop.Value.Contains(","))
                    {
                        filtro += prop.Key + " IN (" + prop.Value + ")";
                        c++;
                    }
                    else
                    {
                        filtro += " " + prop.Key + " = " + prop.Value + "";
                        c++;
                    }
                }
            }
            return filtro;
        }

        /// <summary>
        ///  Obtiene la lista de Programas  registradas en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <returns>Lista de objetos de clase PgeneralWebinarDTO</returns>
        public List<PgeneralWebinarDTO> ListarProgramasPanel()
        {
            try
            {
                List<PgeneralWebinarDTO> programasGenerales = new List<PgeneralWebinarDTO>();
                var query = string.Empty;
                query = "SELECT Id,Nombre,IdTipoPrograma FROM [pla].[V_TPGeneral_Webinar] WHERE Estado = 1 order by Id desc";
                var pgeneralDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PgeneralWebinarDTO>>(pgeneralDB);
                }

                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        ///  Obtiene la lista de Programas  registradas en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<PgeneralDTO> ListarProgramaGeneral(FiltroGridProgramaGeneralDTO filtro)
        {
            try
            {
                List<PgeneralDTO> programasGenerales = new List<PgeneralDTO>();
                var _query = string.Empty;
                _query = "pla.SP_General_PanelFiltro";
                var pgeneralDB = _dapper.QuerySPDapper(_query, new { filtro.IdArea, filtro.IdSubArea, filtro.IdPgeneral, filtro.IdPartner});
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PgeneralDTO>>(pgeneralDB);
                }

                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        ///  Obtiene la lista de Parametros Seo y su descripcion  registradas en el sistema
        ///  para un programa.
        /// </summary>
        /// <returns></returns>
        public List<ParametrosSeoProgramaDTO> ListarParametrosSeoPorPrograma(int idPGeneral)
        {
            try
            {
                List<ParametrosSeoProgramaDTO> parametrosSEO = new List<ParametrosSeoProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT IdPGeneralParametroSEO,IdPGeneral,NombreParametro,IdParametroSEO,Descripcion FROM pla.V_TPGeneral_ParametrosSeo WHERE " +
                    "EstadoSeo = 1 and EstadoPrograma = 1 and IdPGeneral = @IdPgeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    parametrosSEO = JsonConvert.DeserializeObject<List<ParametrosSeoProgramaDTO>>(respuestaDapper);
                }

                return parametrosSEO;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Inerta un Programa General mediante un procedure con IdBusqueda,IdPgeneral y Id con el Id 
        /// de troncalTPgeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public int InsertaPGeneralSinIdentity(PgeneralBO programa)
        {
            try
            {

                var query = _dapper.QuerySPDapper("pla.SP_InsertarPGeneral", new {
                    IdPGeneral = programa.IdPgeneral,
                    Nombre = programa.Nombre,
                    Pw_ImgPortada = programa.PwImgPortada,
                    Pw_ImgPortadaAlf = programa.PwImgPortadaAlf,
                    Pw_ImgSecundaria = programa.PwImgSecundaria,
                    Pw_ImgSecundariaAlf = programa.PwImgSecundariaAlf,
                    IdPartner = programa.IdPartner,
                    IdArea = programa.IdArea,
                    IdSubArea = programa.IdSubArea,
                    IdCategoria = programa.IdCategoria,
                    Pw_estado = programa.PwEstado,
                    Pw_mostrarBSPlay = programa.PwMostrarBsplay,
                    pw_duracion = programa.PwDuracion,
                    IdBusqueda = programa.IdBusqueda,
                    IdChatZopim = programa.IdChatZopim,
                    Pg_titulo = programa.PgTitulo,
                    Codigo = programa.Codigo,
                    UrlImagenPortadaFr = programa.UrlImagenPortadaFr,
                    UrlBrochurePrograma = programa.UrlBrochurePrograma,
                    UrlPartner = programa.UrlPartner,
                    UrlVersion = programa.UrlVersion,
                    Pw_tituloHtml = programa.PwTituloHtml,
                    EsModulo = programa.EsModulo,
                    NombreCorto = programa.NombreCorto,
                    IdPagina = programa.IdPagina,
                    ChatActivo = programa.ChatActivo,
                    UsuarioCreacion = programa.UsuarioCreacion,
                    UsuarioModificacion = programa.UsuarioModificacion

                });
                int idPgeneral = JsonConvert.DeserializeObject<List<PgeneralIdDTO>>(query).FirstOrDefault().Id;
                return idPgeneral;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        ///  Obtiene la lista Programa Generales (activos) con el Id, Nombre, Area, SubArea, Categoria  ordenados descendentemente
        ///  para un programa.
        /// </summary>
        /// <returns></returns>
        public List<ProgramaGeneralMontoPagoPanelDTO> ListarProgramaGeneralParaMontoPago()
        {
            try
            {
                List<ProgramaGeneralMontoPagoPanelDTO> parametrosSEO = new List<ProgramaGeneralMontoPagoPanelDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre,IdArea,IdSubArea,IdCategoria FROM pla.V_TMontoPago_ProgramasGenerales WHERE " +
                    "Estado = 1 order by Id desc";
                var respuestaDapper = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    parametrosSEO = JsonConvert.DeserializeObject<List<ProgramaGeneralMontoPagoPanelDTO>>(respuestaDapper);
                }

                return parametrosSEO;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Obtiene la lista de nombres, y Url para direccionar en el Portal de los programas (activos)  registradas en el sistema 
        ///  y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns></returns>
        public List<PGeneralFiltroConUrlDTO> ObtenerProgramasConUrlFiltro()
        {
            try
            {
                List<PGeneralFiltroConUrlDTO> programasGenerales = new List<PGeneralFiltroConUrlDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Titulo, IdBusqueda, Nombre, Descripcion FROM pla.V_TPGeneral_FiltroConURL WHERE Estado = 1";
                var pgeneralDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralFiltroConUrlDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Retorna una lista de programas relacionados a un Programa filtrado
        /// </summary>
        /// <param name="IdProgramaGeneral"></param>
        /// <returns></returns>
        public List<PGeneralASubPGeneral2DTO> ObtenerPgeneralCursos(int idProgramaGeneral)
        {
            try
            {
                var _queryPgeneralCursos = "Select Id,IdPGeneral_Hijo From pla.V_PGeneralASubPGeneralPorIdPrograma Where Estado=1 and IdPGeneral_Padre = @idProgramaGeneral Order by FechaCreacion asc";
                var PgeneralCursos = _dapper.QueryDapper(_queryPgeneralCursos, new { idProgramaGeneral });
                return JsonConvert.DeserializeObject<List<PGeneralASubPGeneral2DTO>>(PgeneralCursos);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Retorna datos de Programa General Mediante IdProgramaGeneral para Programa Especifico
        /// </summary>
        /// <param name="IdProgramaGeneral"></param>
        /// <returns></returns>
        public PGeneralDatosDTO ObtenerPGeneralParaPEspecifico(int idProgramaGeneral)
        {
            try
            {
                string _queryProgramaGeneral = "Select Id,Nombre,Codigo,IdArea,IdSubArea,IdCategoria From pla.V_GetProgramaGeneral Where Estado = 1 and Id = @idProgramaGeneral";
                var ProgramaGeneral = _dapper.FirstOrDefault(_queryProgramaGeneral, new { idProgramaGeneral });
                return JsonConvert.DeserializeObject<PGeneralDatosDTO>(ProgramaGeneral);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el programa por el Id de programa especifico
        /// </summary>
        /// <param name="idProgramaGeneral"></param>
        /// <returns></returns>
        public ProgramaGeneralTroncalDTO ObtenerProgramaGeneralParaPespecificoPorId(int idProgramaGeneral)
        {
            string _queryPgeneral = "Select Id,Nombre,Codigo,IdTroncalPartner,Duracion,IdArea,IdSubArea,IdCategoria,NombreCategoria From pla.V_TTroncalPgeneral_ObtenerInformacion Where Estado=1 and Id=@IdProgramaGeneral";
            var queryPgeneral = _dapper.FirstOrDefault(_queryPgeneral, new { IdProgramaGeneral = idProgramaGeneral });
            return JsonConvert.DeserializeObject<ProgramaGeneralTroncalDTO>(queryPgeneral);
        }

        public List<CorreosGmailDTO> ObtenerCorreosIdPersonalAprobacion(int IdPersonal)
        {
            try
            {
                List<CorreosGmailDTO> Correos = new List<CorreosGmailDTO>();
                var _correos = _dapper.QuerySPDapper("gp.SP_ObtenerCorreosByIdPersonaAprobacionCronograma", new { IdPersonal });
                Correos = JsonConvert.DeserializeObject<List<CorreosGmailDTO>>(_correos);
                return Correos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<PGeneralPrincipalDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PGeneralPrincipalDTO
                {
                    Id = y.Id,
                    IdPgeneral = y.IdPgeneral,
                    Nombre = y.Nombre,
                    PwImgPortada = y.PwImgPortada,
                    PwImgPortadaAlf = y.PwImgPortadaAlf,
                    PwImgSecundaria = y.PwImgSecundaria,
                    PwImgSecundariaAlf = y.PwImgSecundariaAlf,
                    IdPartner = y.IdPartner,
                    IdArea = y.IdArea,
                    IdSubArea = y.IdSubArea,
                    IdCategoria = y.IdCategoria,
                    PwEstado = y.PwEstado,
                    PwMostrarBsplay = y.PwMostrarBsplay,
                    PwDuracion = y.PwDuracion,
                    IdBusqueda = y.IdBusqueda,
                    IdChatZopim = y.IdChatZopim,
                    PgTitulo = y.PgTitulo,
                    Codigo = y.Codigo,
                    UrlImagenPortadaFr = y.UrlImagenPortadaFr,
                    UrlBrochurePrograma = y.UrlBrochurePrograma,
                    UrlPartner = y.UrlPartner,
                    UrlVersion = y.UrlVersion,
                    PwTituloHtml = y.PwTituloHtml,
                    NombreCorto = y.NombreCorto,
                    IdPagina = y.IdPagina,
                    ChatActivo = y.ChatActivo,

                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PGeneralFiltroNombreDTO> ObtenerProgramaGeneralAutocomplete(string nombre)
        {
            try
            {
                List<PGeneralFiltroNombreDTO> pGeneralFiltroNombre = new List<PGeneralFiltroNombreDTO>();
                var _query = "SELECT  Id, Nombre  FROM pla.V_ObtenerPGeneralFiltro WHERE Nombre LIKE CONCAT('%',@nombre,'%') AND Estado = 1 ";
                var pGeneralFiltroNombreDB = _dapper.QueryDapper(_query, new { nombre });
                if (!string.IsNullOrEmpty(pGeneralFiltroNombreDB) && !pGeneralFiltroNombreDB.Contains("[]"))
                {
                    pGeneralFiltroNombre = JsonConvert.DeserializeObject<List<PGeneralFiltroNombreDTO>>(pGeneralFiltroNombreDB);
                }
                return pGeneralFiltroNombre;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un listado los ids de programas generales
        /// </summary>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerTodosIds() {
            try
            {
                return this.GetBy(x => x.Estado, x => new IdDTO { Id = x.Id }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los programas generales por id area
        /// </summary>
        /// <param name="listaAreas">Lista de indices de las areas (PK de la tabla pla.T_AreaCapacitacion)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerTodosPorIdArea(List<int> listaAreas)
        {
            try
            {
                string listAreas = string.Join(",", listaAreas);

                List<IdDTO> programaGeneral = new List<IdDTO>();
                var _query = "SELECT Id FROM com.V_TPGeneral_ObtenerIds AS PG INNER JOIN (SELECT Item FROM conf.F_Splitstring(@listAreas,',')) AS L ON PG.IdArea = L.item WHERE PG.Estado = 1 GROUP BY Id";
                var programaGeneralDB = _dapper.QueryDapper(_query, new { listAreas });
                if (!string.IsNullOrEmpty(programaGeneralDB) && !programaGeneralDB.Contains("[]"))
                {
                    programaGeneral = JsonConvert.DeserializeObject<List<IdDTO>>(programaGeneralDB);
                }
                return programaGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los programas generales por id sub area
        /// </summary>
        /// <param name="listaSubAreas">Lista de indices de las subareas (PK de la tabla pla.T_SubAreaCapacitacion)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerTodosPorIdSubArea(List<int> listaSubAreas)
        {
            try
            {
                string listSubAreas = string.Join(",", listaSubAreas);

                List<IdDTO> programaGeneral = new List<IdDTO>();
                var _query = string.Empty;
                _query = "SELECT Id FROM com.V_TPGeneral_ObtenerIds AS PG INNER JOIN (SELECT Item FROM conf.F_Splitstring(@listSubAreas,',')) AS L ON PG.IdSubArea = L.item WHERE PG.Estado = 1 GROUP BY Id";
                var programaGeneralDB = _dapper.QueryDapper(_query, new { listSubAreas });
                if (!string.IsNullOrEmpty(programaGeneralDB) && !programaGeneralDB.Contains("[]"))
                {
                    programaGeneral = JsonConvert.DeserializeObject<List<IdDTO>>(programaGeneralDB);
                }
                return programaGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public class tst {
            public int Id { get; set; }
        }

        public List<tst> Regularizar_EliminarMetodo()
        {
            List<tst> a = new List<tst>();
            //var query = "select  Id from mkt.T_AsignacionAutomatica where idMigracion is null";
            var query = "select Id from mkt.T_AsignacionAutomatica where CONVERT(date, FechaCreacion) > CONVERT(date, '2019-08-01 21:06:28.000')";
            var _correos = _dapper.QueryDapper(query, null );
            if (!string.IsNullOrEmpty(_correos))
            {
                a = JsonConvert.DeserializeObject<List<tst>>(_correos);
            }
            return a; //.Select(x => x.Values).ToList();
        }

        public List<tst> RegularizarAATemp_EliminarMetodo()
        {
            List<tst> a = new List<tst>();
            var query = "select  Id from mkt.T_AsignacionAutomatica_Temp where idMigracion is null and  UsuarioCreacion NOT IN ('WebHookFacebookLeads')";
            var _correos = _dapper.QueryDapper(query, null);
            if (!string.IsNullOrEmpty(_correos))
            {
                a = JsonConvert.DeserializeObject<List<tst>>(_correos);
            }
            return a; //.Select(x => x.Values).ToList();
        }

        public List<tst> RegularizarAlumno_EliminarMetodo()
        {
            List<tst> a = new List<tst>();
            var query = "select  Id from mkt.t_alumno where idMigracion is null";
            var _correos = _dapper.QueryDapper(query, null);
            if (!string.IsNullOrEmpty(_correos))
            {
                a = JsonConvert.DeserializeObject<List<tst>>(_correos);
            }
            return a; //.Select(x => x.Values).ToList();
        }


        public List<tst> RegularizarOportunidad_EliminarMetodo()
        {
            List<tst> a = new List<tst>();
            var query = "select Id from com.T_Oportunidad where idMigracion is null";
            var _correos = _dapper.QueryDapper(query, null);
            if (!string.IsNullOrEmpty(_correos))
            {
                a = JsonConvert.DeserializeObject<List<tst>>(_correos);
            }
            return a; //.Select(x => x.Values).ToList();
        }

        public List<tst> RegularizarOportunidad_SinModeloDataMining()
        {
            List<tst> a = new List<tst>();
            var query = @"
SELECT t1.Id
FROM com.T_Oportunidad AS t1
     INNER JOIN pla.T_FaseOportunidad AS t3 ON T1.IdFaseOportunidad = t3.Id
     LEFT JOIN com.T_ModeloDataMining AS t2 ON t1.Id = T2.IdOportunidad
WHERE t2.Id IS NULL
      AND YEAR(t1.FechaCreacion) >= 2019
      AND t1.idpersonal_asignado != 125
      AND t1.Idcentrocosto IS NOT NULL
      AND t1.idalumno IS NOT NULL
      AND t3.codigo IN('RN2', 'BNC', 'IS', 'IT')
ORDER BY t1.idfaseoportunidad ASC;
";
            var _correos = _dapper.QueryDapper(query, null);
            if (!string.IsNullOrEmpty(_correos))
            {
                a = JsonConvert.DeserializeObject<List<tst>>(_correos);
            }
            return a; //.Select(x => x.Values).ToList();
        }


        public List<tst> RegularizarAAError_EliminarMetodo()
        {
            List<tst> a = new List<tst>();
            var query = "select distinct IdAsignacionAutomatica  as Id from mkt.T_AsignacionAutomaticaError where idMigracion is null";
            var _correos = _dapper.QueryDapper(query, null);
            if (!string.IsNullOrEmpty(_correos))
            {
                a = JsonConvert.DeserializeObject<List<tst>>(_correos);
            }
            return a; //.Select(x => x.Values).ToList();
        }

        public InformacionProgramaDocumentosDTO ObtenerPgeneralparaDocumentosporIdAlumno(int idAlumno)
        {
            try
            {
                string _queryPGeneral = "SELECT TOP 1 pe.IdProgramaGeneral, pg.Nombre, pg.UrlBrochurePrograma, pe.Tipo, pe.Ciudad, pe.UrlDocumentoCronograma, op.Id AS IdOportunidad, op.IdAlumno, fo.Codigo AS CodigoFase, op.IdActividadDetalle_Ultima AS IdActividadDetalle " +
                                        "FROM com.T_Oportunidad AS op " +
                                        "INNER JOIN conf.T_ClasificacionPersona AS CLA ON CLA.Id = op.IdClasificacionPersona and IdTipoPersona = 1 " +
                                        "INNER JOIN pla.T_PEspecifico AS pe ON op.IdCentroCosto=pe.IdCentroCosto " +
                                        "INNER JOIN pla.T_PGeneral AS pg ON pe.IdProgramaGeneral=pg.Id " +
                                        "INNER JOIN pla.T_FaseOportunidad AS fo ON op.IdFaseOportunidad=fo.Id " +
                                        "WHERE CLA.IdTablaOriginal =@idAlumno " +
                                        "ORDER BY op.FechaCreacion DESC";
                var _programaGeneral = _dapper.FirstOrDefault(_queryPGeneral, new { IdAlumno = idAlumno });
                return JsonConvert.DeserializeObject<InformacionProgramaDocumentosDTO>(_programaGeneral);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de programas generales por sub area
        /// </summary>
        /// <returns></returns>
        public List<FiltroPGeneralDTO> ObtenerPorSubArea() {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroPGeneralDTO { Id = x.Id, Nombre = x.Nombre, IdSubArea = x.IdSubArea }).ToList();
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtener montos
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <param name="idPais"></param>
        /// <returns></returns>
        private List<MontoPagoModalidadDTO> ObtenerMontos(int idPGeneral, int idPais = 0)
        {
            var montos = this.ObtenerMontosPorId(idPGeneral);
            var montosPorPais = montos.Where(s => s.Pais.Equals(idPais)).OrderBy(x => x.Paquete).ToList();
            if (montosPorPais.Count() == 0)
            {
                var result1 = montos.Where(s => s.Pais.Equals(0)).OrderBy(x => x.Paquete).ToList();
                if (result1.Where(x => x.Paquete == 0).ToList().Count() == 0)
                {
                    //tipo 1
                    var beneficios = this.ObtenerBeneficiosPGeneralTipo1(idPGeneral);
                    foreach (var item in result1)
                    {
                        var items = beneficios.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                        string Detalles = "<ul>";
                        foreach (var item2 in items)
                        {
                            Detalles += "<li>" + item2 + "</li>";
                        }
                        Detalles += "</ul>";
                        item.Beneficios = Detalles;

                    }
                }
                else
                {
                    //tipo 2
                    var beneficio = this.ObtenerBeneficiosPGeneralTipo2(idPGeneral); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB).FirstOrDefault();
                    foreach (var item2 in result1)
                    {
                        item2.Beneficios = beneficio.Titulo;
                    }
                }
                return result1;
            }
            else
            {
                if (montosPorPais.Where(x => x.Paquete == 0).ToList().Count() == 0)
                {
                    var beneficios = this.ObtenerBeneficiosPGeneralTipo1(idPGeneral, idPais); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB);
                    foreach (var item in montosPorPais)
                    {
                        var items = beneficios.Where(w => w.Paquete == item.Paquete).OrderBy(w => w.OrdenBeneficio).Select(w => w.Titulo).ToList();
                        string detalles = "<ul>";
                        foreach (var _item in items)
                        {
                            detalles += "<li>" + _item + "</li>";
                        }
                        detalles += "</ul><br>";
                        item.Beneficios = detalles;
                    }
                }
                else
                {
                    var beneficio = this.ObtenerBeneficiosPGeneralTipo2(idPGeneral); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB).FirstOrDefault();
                    foreach (var item2 in montosPorPais)
                    {
                        item2.Beneficios = beneficio.Titulo;
                    }
                }
                return montosPorPais;
            }
        }



        /// <summary>
        /// Obtener montos
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General del que se desea obtener los montos (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idPais">Id del Pais del cual se desea obtener los beneficios (PK de la tabla conf.T_Pais)</param>
        /// <returns>Lista de objetos del tipo BeneficioDTO</returns>
        private List<BeneficioDTO> ObtenerBeneficios(int idPGeneral, int idPais = 0)
        {

            var listaBeneficios = new List<BeneficioDTO>();

            var montos = this.ObtenerMontosPorId(idPGeneral);
            var montosPorPais = montos.Where(s => s.Pais.Equals(idPais)).OrderBy(x => x.Paquete).ToList();
            if (montosPorPais.Count() == 0)
            {
                var result1 = montos.Where(s => s.Pais.Equals(0)).OrderBy(x => x.Paquete).ToList();
                if (result1.Where(x => x.Paquete == 0).ToList().Count() == 0)
                {
                    // Tipo 1
                    listaBeneficios.AddRange(ObtenerBeneficiosPGeneralTipo1(idPGeneral));
                }
                else
                {
                    // Tipo 2
                    listaBeneficios.Add(ObtenerBeneficiosPGeneralTipo2(idPGeneral));
                }
            }
            else
            {
                if (montosPorPais.Where(x => x.Paquete == 0).ToList().Count() == 0)
                {
                    listaBeneficios.AddRange(ObtenerBeneficiosPGeneralTipo1(idPGeneral, idPais)); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB);
                   
                }
                else
                {
                    listaBeneficios.Add(ObtenerBeneficiosPGeneralTipo2(idPGeneral)); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB).FirstOrDefault();
                }
            }
            return listaBeneficios;
        }

        /// <summary>
        /// Obtenere beneficios por version
        /// </summary>
        /// <param name="id">Id de la del programa general del cual se desea obtener los beneficios (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idCodigoPais">Id del pais del cual se desea obtener los beneficios (PK de la tabla conf.T_Pais)</param>
        /// <returns>Cadena con los beneficios por version</returns>
        private string ObtenerBeneficiosPorVersion(int id, int idPaquete, int idCodigoPais = 0 )
        {
            try
            {
                var beneficios = this.ObtenerBeneficios(id, idCodigoPais);

                //var montos = this.ObtenerMontos(id, idCodigoPais);

                //Filtramos los resultados por paquete

                //WHEN cc.Paquete = 0 THEN 'Sin Versión'
                //WHEN cc.Paquete = 1 THEN 'Básica'
                //WHEN cc.Paquete = 2 THEN 'Profesional'
                // WHEN cc.Paquete = 3 THEN 'Gerencial'
                //if (idPaquete == 0)
                //{
                //    montos = montos.Where(x => x.Paquete == idPaquete).ToList();
                //}
                //else if (idPaquete == 1)
                //{
                //    montos = montos.Where(x => x.Paquete == idPaquete).ToList();
                //}
                //else if (idPaquete == 2)
                //{
                //    montos = montos.Where(x => x.Paquete == idPaquete || x.Paquete == 1).ToList();
                //}
                //else if (idPaquete == 3)
                //{
                //    montos = montos.Where(x => x.Paquete == idPaquete || x.Paquete == 1 || x.Paquete == 2).ToList();
                //}
                //else {//ultimo caso
                //    montos = montos.Where(x => x.Paquete == idPaquete).ToList();
                //}
                if (idPaquete == 0)
                {
                    beneficios = beneficios.Where(x => x.Paquete == idPaquete).ToList();
                }
                else if (idPaquete == 1)
                {
                    beneficios = beneficios.Where(x => x.Paquete == idPaquete).ToList();
                }
                else if (idPaquete == 2)
                {
                    beneficios = beneficios.Where(x => x.Paquete == idPaquete || x.Paquete == 1).ToList();
                }
                else if (idPaquete == 3)
                {
                    beneficios = beneficios.Where(x => x.Paquete == idPaquete || x.Paquete == 1 || x.Paquete == 2).ToList();
                }
                else
                {//ultimo caso
                    beneficios = beneficios.Where(x => x.Paquete == idPaquete).ToList();
                }
                //extramelos lo que estan de màs
                beneficios = beneficios.Where(x => !x.Titulo.Contains("Todos los beneficios de la ")).ToList();

                //beneficios = beneficios.Where(x => !x.Titulo.Contains("Todos los beneficios de la "))
                    //si es profesional - agregamos la basica
                //agregamos l
                //Lo convertimos a tabla html
                //var stringHtml = "<table class=\"table table-hover \"><tr><td><strong>Versión</strong></td><td><strong>Beneficios</strong></td></tr>"
                //                              + string.Join("", montos.Select(s => "<tr><td>" + s.NombrePaquete + "</td> <td>" + s.Beneficios + "</td></tr>").ToArray()) + "</table>";

                var nombrePaquete = "";
                if (idPaquete == 0)
                {
                    nombrePaquete = "Sin version";
                }
                else if (idPaquete == 1)
                {
                    nombrePaquete = "Version basica:";
                }
                else if (idPaquete == 2)
                {
                    nombrePaquete = "Version profesional:";
                }
                else if (idPaquete == 3)
                {
                    nombrePaquete = "Version gerencial";
                }

                var stringHtml = $@"
                                <span>{nombrePaquete}</span>";

                beneficios = beneficios.OrderBy(x => x.OrdenBeneficio).ToList();

                if (idPaquete == 0)
                {
                    stringHtml += "<div style='font-size:11pt;font-family:Calibri,sans-serif'>";
                    foreach (var item in beneficios)
                    {
                        stringHtml += item.Titulo;
                    }
                    stringHtml += "</div>";
                }
                else {
                    stringHtml += "<ul style = 'font-size:11pt;font-family:Calibri,sans-serif'> ";
                    foreach (var item in beneficios)
                    {
                        item.Titulo = item.Titulo.Replace("<p>", "");
                        item.Titulo = item.Titulo.Replace("</p>", "");
                        stringHtml += $@"
                                  <li> { item.Titulo } </li>
                                ";
                    }
                    stringHtml += "</ul>";
                }
                return stringHtml;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los beneficios por matricula
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera que se desea obtener los beneficios (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con los beneficios por version segun la matricula cabecera enviada</returns>
        public string ObtenerBeneficiosVersion(int idMatriculaCabecera)
        {
            try
            {
                var resultadoFinal = new DetalleMatriculaDTO();
                var query = $@"ope.SP_ObtenerDetalleMatriculaBeneficios";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<DetalleMatriculaDTO>(resultado);
                }

                return this.ObtenerBeneficiosPorVersion(resultadoFinal.IdPGeneral, resultadoFinal.IdPaquete, resultadoFinal.IdCodigoPais);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la version por matricula
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con la version formateada</returns>
        public string ObtenerVersion(int idMatriculaCabecera)
        {
            try
            {
                var resultadoFinal = new ValorStringDTO();
                var query = $@"ope.SP_ObtenerVersionMatriculaAlumno";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la duracion del programa en meses
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la duracion del programa en meses</returns>
        public string ObtenerDuracionMeses(int idMatriculaCabecera)
        {
            try
            {
                var resultadoFinal = new ValorStringDTO();
                var query = $@"ope.SP_ObtenerDuracionPGeneralMatriculaCabecera";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene lista de programas generales padre
        /// </summary>
        /// <returns></returns>
        public List<ProgramaGeneralSubAreaFiltroDTO> ObtenerProgramaGeneralPadre(int? tipo)
		{
			try
			{
				var query = "";
				if (tipo.HasValue)
				{
					query = $@"
						SELECT DISTINCT 
								IdPGeneral AS Id, 
								PGeneral AS Nombre, 
								IdSubArea
						FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
						WHERE Estado = 1
								AND RowNumber = 1
								AND Tipo = 1;
						";
				}
				else
				{
					query = $@"
						SELECT DISTINCT 
								IdPGeneral AS Id, 
								PGeneral AS Nombre, 
								IdSubArea
						FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
						WHERE Estado = 1
								AND RowNumber = 1;
						";
				}

				
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<ProgramaGeneralSubAreaFiltroDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        public PgeneralBO ObtenerProgramaPorId(int Id)
        {
            try
            {
                string _query = "Select * from pla.T_Pgeneral Where Id = @Id";
                string queryPrograma = _dapper.FirstOrDefault(_query, new { Id });
                return JsonConvert.DeserializeObject<PgeneralBO>(queryPrograma);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
        public List<ListaPgeneralVersionProgramaDTO> ListaVersionPrograma(int idPGeneral)
        {
            try
            {
                List<ListaPgeneralVersionProgramaDTO> pgeneralVersionPrograma = new List<ListaPgeneralVersionProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT IdPgeneralVersionPrograma,IdPGeneral,NombreVersion,IdVersionPrograma,Duracion FROM pla.V_TPGeneral_VersionPrograma WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPgeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    pgeneralVersionPrograma = JsonConvert.DeserializeObject<List<ListaPgeneralVersionProgramaDTO>>(respuestaDapper);
                }

                return pgeneralVersionPrograma;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public string ObtenerCodigoPartner(int IdMatriculacabecera)
        {
            try
            {
                ValorStringDTO valor = new ValorStringDTO();
                string _query = "Select CodigoPartner AS Valor from pla.V_ObtenerCodigoPartner Where Id = @IdMatriculacabecera";
                string queryPrograma = _dapper.FirstOrDefault(_query, new { IdMatriculacabecera });
                if (!string.IsNullOrEmpty(queryPrograma) && !queryPrograma.Contains("null"))
                {
                    valor = JsonConvert.DeserializeObject<ValorStringDTO>(queryPrograma);
                    return valor.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string ObtenerNombrePorIdPespecifico(int IdPespecifico)
        {
            try
            {
                ValorStringDTO valor = new ValorStringDTO();
                string _query = "Select Nombre as Valor from pla.V_TPgeneral_NombrePorIdPespecifico Where IdPespecifico = @IdPespecifico";
                string queryPrograma = _dapper.FirstOrDefault(_query, new { IdPespecifico });
                if (!string.IsNullOrEmpty(queryPrograma) && !queryPrograma.Contains("null"))
                {
                    valor = JsonConvert.DeserializeObject<ValorStringDTO>(queryPrograma);
                }
                return valor.Valor;        
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public string ObtenerNombrePorIdCursoMoodle(int IdProgramaGeneral)
        {
            try
            {
                ValorStringDTO valor = new ValorStringDTO();
                string _query = "Select Nombre as Valor from pla.V_TPgeneral_NombrePorIdCursoMoodle Where Id = @IdProgramaGeneral";
                string queryPrograma = _dapper.FirstOrDefault(_query, new { IdProgramaGeneral });
                if (!string.IsNullOrEmpty(queryPrograma) && !queryPrograma.Contains("null"))
                {
                    valor = JsonConvert.DeserializeObject<ValorStringDTO>(queryPrograma);
                }
                return valor.Valor;        
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PgeneralCursoIRCADTO> obtenerCursosIrcaAlumno (int IdMatriculaCabecera)
        {
            try
            {
                List<PgeneralCursoIRCADTO> listacurso = new List<PgeneralCursoIRCADTO>();
                string _query = "Select NombreCurso, EstadoCurso from pla.V_PgeneralCursosIrca Where IdMatriculaCabecera = @IdMatriculaCabecera";
                string queryPrograma = _dapper.QueryDapper(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(queryPrograma) && !queryPrograma.Contains("null"))
                {
                    listacurso = JsonConvert.DeserializeObject<List<PgeneralCursoIRCADTO>>(queryPrograma);
                }
                return listacurso;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public List<SesionesOnlineWebinarDocenteDTO> ObtenerPGeneralPorProveedor(int idProveedor)
        {
            try
            {
                List<SesionesOnlineWebinarDocenteDTO> listado = new List<SesionesOnlineWebinarDocenteDTO>();
                string _query = "SELECT DISTINCT IdPGeneral, PGeneral FROM pla.V_ObtenerSesionesOnlineWebinarDocente WHERE IdProveedor = @idProveedor";
                var query = _dapper.QueryDapper(_query, new { idProveedor });
                listado = JsonConvert.DeserializeObject<List<SesionesOnlineWebinarDocenteDTO>>(query);
                return listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: PgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene lista de monto de programa por idarea y subarea
        /// </summary>
        /// <param name="filtros">Objeto tipo Dictionary<string, string> </param>
        /// <returns> Lista Monto Pago Programa General: List<MontoPagoProgramaDTO></returns>  
        public List<MontoPagoProgramaDTO> ObtenerResumenProgramaV2(Dictionary<string, string> filtros)
		{
			try
			{
				var filtro = this.ObtenerFiltro(filtros);
				List<MontoPagoProgramaDTO> lista = new List<MontoPagoProgramaDTO>();
				var query = "SELECT Id, Precio, PrecioLetras, IdMoneda, SimboloMoneda, Matricula, Cuotas, NroCuotas, NombrePrograma, DuracionPrograma, IdPrograma, IdTipoPago, TipoPago, IdPais, Descripcion, VisibleWeb, Paquete, IdArea, IdSubArea FROM pla.V_TMontoPagoPrograma_ObtenerMontoPagoProgramaGeneral "+ filtro.ToString() + " AND Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<MontoPagoProgramaDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// Repositorio: PgeneralRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene seccion especifica dependiendo del parametro
        /// </summary>
        /// <param name="idPGeneral"> Id Programa General </param>
        /// <param name="seccion"> Sección </param>
        /// <returns> ProgramaSeccionIndividualDTO </returns>
        public ProgramaSeccionIndividualDTO SeccionIndividualPGeneral(int idPGeneral, string seccion)
        {
            try
            {
                ProgramaSeccionIndividualDTO valor = new ProgramaSeccionIndividualDTO();

                string query = @"SELECT Titulo,
                                        Contenido,
                                        IdSeccionTipoDetalle_PW,
                                        NumeroFila,
                                        Cabecera,
                                        PiePagina,
                                        OrdenWeb
                                    FROM pla.V_ListaSeccionesPorIdPrograma_Documento
                                    WHERE IdPGeneral = @idPGeneral AND Titulo = @seccion";
                string queryPrograma = _dapper.FirstOrDefault(query, new { idPGeneral, seccion });
                if (!string.IsNullOrEmpty(queryPrograma) && !queryPrograma.Contains("null"))
                {
                    valor = JsonConvert.DeserializeObject<ProgramaSeccionIndividualDTO>(queryPrograma);
                    return valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string guardarArchivos(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"repositorioweb/img/programas/logo/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = tipo;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        _nombreLink = "";
                    }


                    return _nombreLink;

                }
                catch (Exception ex)
                {
                    return "";
                }

            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return "";
            }
        }

        /// <summary>
		/// Se obtienen los centros de costos que no esten asociados a un GrupoFiltroProgramaCritico
		/// </summary>
		/// <returns>Lista de objetos de clase PGeneralProgramaCriticoSubAreaDTO</returns>
		public List<PGeneralProgramaCriticoSubAreaDTO> ObtenerPGeneralProgramaCriticoPorSubArea()
        {
            try
            {
                List<PGeneralProgramaCriticoSubAreaDTO> listaPGeneral = new List<PGeneralProgramaCriticoSubAreaDTO>();
                var registrosBO = _dapper.QuerySPDapper("com.SP_ObtenerPGeneralProgramaCriticoPorSubArea", new { });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    listaPGeneral = JsonConvert.DeserializeObject<List<PGeneralProgramaCriticoSubAreaDTO>>(registrosBO);
                }
                return listaPGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PGeneralRepositorio
        /// Autor: Edgar Serruto
        /// Fecha: 08/07/2021
        /// <summary>
        /// Obtiene registros de IdSubgArea Asociada a PGeneral para combo
        /// </summary>
        /// <returns>List<PGeneralSubAreaCapacitacionFiltroDTO></returns>
        public List<PGeneralSubAreaCapacitacionFiltroDTO> ObtenerSubAreaPGeneralParaCombo()
        {
            try
            {
                List<PGeneralSubAreaCapacitacionFiltroDTO> lista = new List<PGeneralSubAreaCapacitacionFiltroDTO>();
                var query = "SELECT Id, Nombre, IdSubAreaCapacitacion FROM pla.V_TPGeneral_FiltroCompuestoAsociadoArea";
                var respuesta = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PGeneralSubAreaCapacitacionFiltroDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PGeneralRepositorio
        /// Autor: Jashin Salazar
        /// Fecha: 09/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene cabecera para la agenda de speech
        /// </summary>
        /// <returns> PGeneralCabeceraSpeechDTO </returns>
        public PGeneralCabeceraSpeechDTO ObtenerCabeceraSpeech(int idOportunidad, int idCentroCosto)
        {
            try
            {
                PGeneralCabeceraSpeechDTO lista = new PGeneralCabeceraSpeechDTO();
                var query = "com.SP_ObtenerCabeceraSpeech";
                var respuesta = _dapper.QuerySPFirstOrDefault(query, new { IdOportunidad= idOportunidad, IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<PGeneralCabeceraSpeechDTO>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PGeneralRepositorio
        /// Autor: Jashin Salazar
        /// Fecha: 09/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene publico objetivo para agenda
        /// </summary>
        /// <returns> List<PGeneralPublicoObjetivoDTO> </returns>
        public List<PGeneralPublicoObjetivoDTO> ObtenerPublicoObjetivoPrograma(int idCentroCosto, int idOportunidad)
        {
            try
            {
                List<PGeneralPublicoObjetivoDTO>  lista = new List<PGeneralPublicoObjetivoDTO>();
                var query = "com.SP_ObtenerPublicoObjetivoProgramaGeneral";
                var respuesta = _dapper.QuerySPDapper(query, new { IdCentroCosto = idCentroCosto, IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PGeneralPublicoObjetivoDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PGeneralRepositorio
        /// Autor: Jashin Salazar
        /// Fecha: 09/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene requisitos de certificacion para agenda
        /// </summary>
        /// <returns> List<PGeneralPublicoObjetivoDTO> </returns>
        public List<PGeneralRequisitoCertificacionDTO> ObtenerRequisitosCertificacionPrograma(int idOportunidad)
        {
            try
            {
                List<PGeneralRequisitoCertificacionDTO> lista = new List<PGeneralRequisitoCertificacionDTO>();
                var query = "com.SP_ObtenerRequisitosCertificacionProgramaGeneral";
                var respuesta = _dapper.QuerySPDapper(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PGeneralRequisitoCertificacionDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PGeneralRepositorio
        /// Autor: Jashin Salazar
        /// Fecha: 09/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene requisitos de certificacion para agenda
        /// </summary>
        /// <returns> List<PGeneralPublicoObjetivoDTO> </returns>
        public List<PGeneralArgumentoCertificacionDTO> ObtenerRequisitosCertificacionArgPrograma(int IdCertificacion)
        {
            try
            {
                List<PGeneralArgumentoCertificacionDTO> lista = new List<PGeneralArgumentoCertificacionDTO>();
                var query = "SELECT * FROM pla.T_ProgramaGeneralCertificacionArgumento WHERE IdProgramaGeneralCertificacion=@Id and Estado=1";
                var respuesta = _dapper.QueryDapper(query, new { Id = IdCertificacion });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PGeneralArgumentoCertificacionDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PGeneralRepositorio
        /// Autor: Jashin Salazar
        /// Fecha: 09/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene factores de motivacion para agenda
        /// </summary>
        /// <returns> List<PGeneralPublicoObjetivoDTO> </returns>
        public List<PGeneralFactorBeneficioDTO> ObtenerFactorMotivacionPrograma(int idOportunidad)
        {
            try
            {
                List<PGeneralFactorBeneficioDTO> lista = new List<PGeneralFactorBeneficioDTO>();
                var query = "com.SP_ObtenerFactorMotivacionProgramaGeneral";
                var respuesta = _dapper.QuerySPDapper(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PGeneralFactorBeneficioDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PGeneralRepositorio
        /// Autor: Jashin Salazar
        /// Fecha: 09/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene argumento de factores de motivcaicon para agenda
        /// </summary>
        /// <returns> List<PGeneralPublicoObjetivoDTO> </returns>
        public List<PGeneralArgumentoMotivacionDTO> ObtenerArgumentosMotivacionPrograma(int IdMotivacion)
        {
            try
            {
                List<PGeneralArgumentoMotivacionDTO> lista = new List<PGeneralArgumentoMotivacionDTO>();
                var query = "SELECT * FROM pla.T_ProgramaGeneralMotivacionArgumento WHERE IdProgramaGeneralMotivacion=@Id and Estado=1";
                var respuesta = _dapper.QueryDapper(query, new { Id = IdMotivacion });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PGeneralArgumentoMotivacionDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PGeneralRepositorio
        /// Autor: Jashin Salazar
        /// Fecha: 09/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene factores de motivacion para agenda
        /// </summary>
        /// <returns> List<PGeneralPublicoObjetivoDTO> </returns>
        public List<PGeneralProblemaDTO> ObtenerProblemaPrograma(int idOportunidad)
        {
            try
            {
                List<PGeneralProblemaDTO> lista = new List<PGeneralProblemaDTO>();
                var query = "com.SP_ObtenerProblemaProgramaGeneral";
                var respuesta = _dapper.QuerySPDapper(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PGeneralProblemaDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: PGeneralRepositorio
        /// Autor: Jashin Salazar
        /// Fecha: 09/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene argumento de factores de motivcaicon para agenda
        /// </summary>
        /// <returns> List<PGeneralPublicoObjetivoDTO> </returns>
        public List<PGeneralArgumentoProblemaDTO> ObtenerArgumentosProblemaPrograma(int IdProblema, int IdOportunidad)
        {
            try
            {
                List<PGeneralArgumentoProblemaDTO> lista = new List<PGeneralArgumentoProblemaDTO>();
                var query = "com.SP_ObtenerArgumentoProblemaProgramaGeneral";
                var respuesta = _dapper.QuerySPDapper(query, new { IdOportunidad= IdOportunidad, IdProgramaGeneralProblema= IdProblema });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PGeneralArgumentoProblemaDTO>>(respuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
