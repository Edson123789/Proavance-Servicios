using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EsquemaEvaluacionRepositorio : BaseRepository<TEsquemaEvaluacion, EsquemaEvaluacionBO>
    {
        #region Metodos Base
        public EsquemaEvaluacionRepositorio() : base()
        {
        }
        public EsquemaEvaluacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EsquemaEvaluacionBO> GetBy(Expression<Func<TEsquemaEvaluacion, bool>> filter)
        {
            IEnumerable<TEsquemaEvaluacion> listado = base.GetBy(filter);
            List<EsquemaEvaluacionBO> listadoBO = new List<EsquemaEvaluacionBO>();
            foreach (var itemEntidad in listado)
            {
                EsquemaEvaluacionBO objetoBO = Mapper.Map<TEsquemaEvaluacion, EsquemaEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EsquemaEvaluacionBO FirstById(int id)
        {
            try
            {
                TEsquemaEvaluacion entidad = base.FirstById(id);
                EsquemaEvaluacionBO objetoBO = new EsquemaEvaluacionBO();
                Mapper.Map<TEsquemaEvaluacion, EsquemaEvaluacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EsquemaEvaluacionBO FirstBy(Expression<Func<TEsquemaEvaluacion, bool>> filter)
        {
            try
            {
                TEsquemaEvaluacion entidad = base.FirstBy(filter);
                EsquemaEvaluacionBO objetoBO = Mapper.Map<TEsquemaEvaluacion, EsquemaEvaluacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EsquemaEvaluacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEsquemaEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EsquemaEvaluacionBO> listadoBO)
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

        public bool Update(EsquemaEvaluacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEsquemaEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EsquemaEvaluacionBO> listadoBO)
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
        private void AsignacionId(TEsquemaEvaluacion entidad, EsquemaEvaluacionBO objetoBO)
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

        private TEsquemaEvaluacion MapeoEntidad(EsquemaEvaluacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEsquemaEvaluacion entidad = new TEsquemaEvaluacion();
                entidad = Mapper.Map<EsquemaEvaluacionBO, TEsquemaEvaluacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListadoDetalle != null && objetoBO.ListadoDetalle.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListadoDetalle)
                    {
                        TEsquemaEvaluacionDetalle entidadHijo = new TEsquemaEvaluacionDetalle();
                        entidadHijo = Mapper.Map<EsquemaEvaluacionDetalleBO, TEsquemaEvaluacionDetalle>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TEsquemaEvaluacionDetalle.Add(entidadHijo);

                        //mapea al hijo interno
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<EsquemaEvaluacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TEsquemaEvaluacion, bool>>> filters, Expression<Func<TEsquemaEvaluacion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TEsquemaEvaluacion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<EsquemaEvaluacionBO> listadoBO = new List<EsquemaEvaluacionBO>();

            foreach (var itemEntidad in listado)
            {
                EsquemaEvaluacionBO objetoBO = Mapper.Map<TEsquemaEvaluacion, EsquemaEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        public IEnumerable<EsquemaEvaluacion_ListadoDTO> ObtenerTodo()
        {
            //IEnumerable<TEsquemaEvaluacion> listado = base.GetAll();
            //List<EsquemaEvaluacionBO> listadoBO = new List<EsquemaEvaluacionBO>();
            //foreach (var itemEntidad in listado)
            //{
            //    EsquemaEvaluacionBO objetoBO = Mapper.Map<TEsquemaEvaluacion, EsquemaEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
            //    listadoBO.Add(objetoBO);
            //}
            var listado = GetBy(w => 1 == 1,
                s => new EsquemaEvaluacion_ListadoDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    IdFormaCalculoEvaluacion = s.IdFormaCalculoEvaluacion,
                    FormaCalculoEvaluacion = s.IdFormaCalculoEvaluacionNavigation.Nombre
                });

            return listado;
        }

        public IEnumerable<ComboGenericoDTO> ObtenerCombo()
        {
            return GetAll().Select(s => new ComboGenericoDTO() { Id = s.Id, Nombre = s.Nombre });
        }

        public List<ProgramaCalificar_DocenteDTO> ListadoProgramasDocenteFiltrado(ActividadEvaluacionFiltroDTO filtro)
        {
            try
            {
                //var query = "select * from ope.V_ListadoProgramasCalificar_DocenteFiltrado where IdProveedor=@IdProveedor";
                //var res = _dapper.QueryDapper(query,
                //    new
                //    {
                //        IdProveedor = filtro.IdProveedor
                //    });

                var query = "ope.SP_Obtener_ListadoProgramasCalificar_DocenteFiltrado_Portal";
                var res = _dapper.QuerySPDapper(query,
                    new
                    {
                        filtro.IdProveedor,
                        filtro.IdArea,
                        filtro.IdSubArea,
                        filtro.IdPGeneral,
                        filtro.IdProgramaEspecifico,
                        filtro.IdCentroCostoD,
                        filtro.EstadoEvaluacion
                    });
                return JsonConvert.DeserializeObject<List<ProgramaCalificar_DocenteDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<AlumnoMatriculaCursoDTO> ListadoAlumnosCalificarPorPespecifico(int idPespecifico, int grupo)
        {
            try
            {
                var query = "select * from ope.V_ListadoAlumnosCalificar_PorPespecifico where IdPespecifico=@idPespecifico AND Grupo=@grupo";
                var res = _dapper.QueryDapper(query,
                    new
                    {
                        idPespecifico = idPespecifico,
                        grupo = grupo
                    });
                return JsonConvert.DeserializeObject<List<AlumnoMatriculaCursoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<AlumnoMatriculaCursoDTO> ListadoAlumnosCalificarPorPespecificoAlterno(int idPespecifico, int grupo)
        {
            try
            {
                var query = "ope.SP_ListadoAlumnosCalificar_PorPespecifico";
                var res = _dapper.QuerySPDapper(query,
                    new
                    {
                        idPespecifico = idPespecifico,
                        grupo = grupo
                    });
                return JsonConvert.DeserializeObject<List<AlumnoMatriculaCursoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<AlumnoMatriculaCursoDTO> ListadoAlumnosCalificarPorPespecificoCongelado(int idPespecifico)
        {
            try
            {
                var query = "select * from ope.V_ListadoAlumnosCongeladosCalificar_PorPespecifico where IdPespecifico=@idPespecifico";
                var res = _dapper.QueryDapper(query,
                    new
                    {
                        idPespecifico = idPespecifico
                    });
                return JsonConvert.DeserializeObject<List<AlumnoMatriculaCursoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<EsquemaEvaluacion_ItemEvaluableAlumnoDTO> ListadoDetalladoItemEvaluablePorAlumno(int idMatriculaCabecera, int idPEspecifico, int grupo)
        {

            try
            {
                var query = "select * from ope.V_ListadoActividadesCalificables_Completo where IdMatriculaCabecera=@idMatriculaCabecera AND IdPespecifico=@idPespecifico AND Grupo=@grupo";
                var res = _dapper.QueryDapper(query,
                    new
                    {
                        idMatriculaCabecera = idMatriculaCabecera,
                        idPespecifico = idPEspecifico,
                        grupo = grupo
                    });
                return JsonConvert.DeserializeObject<List<EsquemaEvaluacion_ItemEvaluableAlumnoDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CursoAplicaProyectoAnteriorDTO> ListaCursosAplicaProyectoAnterior(int idAlumno)
        {
            try
            {
                var query = "ope.SP_Obtener_ListadoMatriculas_AplicaProyectoAnterior";
                var res = _dapper.QuerySPDapper(query,
                    new
                    {
                        idAlumno = idAlumno
                    });
                return JsonConvert.DeserializeObject<List<CursoAplicaProyectoAnteriorDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Max Mantilla y Renato Escobar
        /// Fecha: 15/10/2021
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>Int</returns>
       public int Congelar()
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("ope.SP_CongelarEsquemasEvaluacionMasivo", new { });
                var valor = JsonConvert.DeserializeObject<ValorIntDTO>(registroDB);
                return valor.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        /// Autor: Max Mantilla y Renato Escobar
        /// Fecha: 15/10/2021
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>int</returns>
        public int InsertarMatriculaNueva(int IdMatriculaCabecera)
        {
            try
            {

                _dapper.QuerySPFirstOrDefault("ope.SP_CongelarEsquemaEvaluacionMatriculaAlumno", new { IdMatriculaCabecera });
                
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla y Renato Escobar
        /// Fecha: 15/10/2021
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>int</returns>
        public int InsertarMatriculaAntigua(int IdMatriculaCabecera)
        {
            try
            {
                 _dapper.QuerySPFirstOrDefault("ope.SP_CongelarEsquemaEvaluacionMatriculaAlumnoAntiguo", new { IdMatriculaCabecera });
               
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla y Renato Escobar
        /// Fecha: 15/10/2021
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>int</returns>
        public int EliminarMatriculaCabecera(int IdMatriculaCabecera)
        {
            try
            {
               _dapper.QuerySPFirstOrDefault("ope.SP_EliminarCongelamientoPEspecificoMatriculaAlumno", new {IdMatriculaCabecera});
               
                return 1; 
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla y Renato Escobar
        /// Fecha: 15/10/2021
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>bool</returns>
        public bool ExisteNuevaAulaVirtual(int idPEspecifico)
        {
            try
            {
                var query = "SELECT Id FROM [pla].[V_TPEspecificoNuevoAulaVirtual_DataBasica] WHERE Id = @idPEspecifico";
                var resultado = _dapper.QueryDapper(query, new { idPEspecifico });

                return !string.IsNullOrEmpty(resultado) && !resultado.Contains("[]");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool ActualizarCongelamientoEsquemaPorMatricula(EditarCongelamientoPEspecificoMatriculaAlumnoDTO Json)
        {
            try
            {
                var query = "ope.SP_ActualizarCongelamientoEsquemaEvaluacionMatricula";
                var res = _dapper.QuerySPDapper(query,
                    new
                    {
                        IdMatriculaCabecera = Json.IdMatriculaCabecera,
                        IdPEspecifico = Json.IdPEspecifico,
                        idEsquemaEvaluacionGeneral = Json.idEsquemaEvaluacionGeneral,
                    });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string ObtenerNombreCongelamientoEsquemaPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                List<CongelamientoPEspecificoMatriculaAlumnoDTO> Esquemas = new List<CongelamientoPEspecificoMatriculaAlumnoDTO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltro = "Select * FROM [ope].[V_CongelamientoPEspecificoAlumno] WHERE IdMatriculaCabecera=@IdMatriculaCabecera";
                var Subfiltro = _dapper.QueryDapper(_queryfiltro, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    Esquemas = JsonConvert.DeserializeObject<List<CongelamientoPEspecificoMatriculaAlumnoDTO>>(Subfiltro);
                }
                else {
                    List<ValorIdMatriculaDTO> matriculas = new List<ValorIdMatriculaDTO>();
                    ValorIdMatriculaDTO matricula = new ValorIdMatriculaDTO();
                    matricula.IdMatriculaCabecera = idMatriculaCabecera;
                    matriculas.Add(matricula);
                    var esquemaEvaluacionBO = new EsquemaEvaluacionBO();
                    var listado = esquemaEvaluacionBO.InsertarMatricula(matriculas);
                    Subfiltro = _dapper.QueryDapper(_queryfiltro, new { idMatriculaCabecera });
                    Esquemas = JsonConvert.DeserializeObject<List<CongelamientoPEspecificoMatriculaAlumnoDTO>>(Subfiltro);
                }
                _queryfiltro = "Select * FROM [ope].[V_CongelamientoPEspecificoEsquemasEvaluacionAlumno] WHERE IdMatriculaCabecera=@IdMatriculaCabecera";
                Subfiltro = _dapper.QueryDapper(_queryfiltro, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    Esquemas[0].EsquemasEvaluacion = JsonConvert.DeserializeObject<List<EsquemaEvaluacionCongelado_ListadoDTO>>(Subfiltro);
                }
                return Esquemas[0].EsquemasEvaluacion[0].NombreEsquema;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CongelamientoPEspecificoMatriculaAlumnoDTO> ObtenerCongelamientoEsquemaPorMatricula(int idMatriculaCabecera)
        {
            try
            {

                List<CongelamientoPEspecificoMatriculaAlumnoDTO> Esquemas = new List<CongelamientoPEspecificoMatriculaAlumnoDTO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltro = "Select * FROM [ope].[V_CongelamientoPEspecificoAlumno] WHERE IdMatriculaCabecera=@IdMatriculaCabecera";
                var Subfiltro = _dapper.QueryDapper(_queryfiltro, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                {
                    Esquemas = JsonConvert.DeserializeObject<List<CongelamientoPEspecificoMatriculaAlumnoDTO>>(Subfiltro);
                }
                var i = 0;
                var j = 0;
                foreach (var esquema in Esquemas)
                {
                    _queryfiltro = "Select * FROM [ope].[V_CongelamientoPEspecificoEsquemasEvaluacionAlumno] WHERE IdProgramaGeneral=@IdProgramaGeneral AND  (IdMatriculaCabecera = 0 OR IdMatriculaCabecera=@IdMatriculaCabecera)";
                    Subfiltro = _dapper.QueryDapper(_queryfiltro, new { IdProgramaGeneral = esquema.IdProgramaGeneral, IdMatriculaCabecera = idMatriculaCabecera });
                    if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                    {
                        Esquemas[i].EsquemasEvaluacion = JsonConvert.DeserializeObject<List<EsquemaEvaluacionCongelado_ListadoDTO>>(Subfiltro);
                    }
                    if(esquema.EsquemasEvaluacion != null)
                    {
                        foreach (var esquemaevaluacion in Esquemas[i].EsquemasEvaluacion)
                        {
                            _queryfiltro = "Select * FROM [ope].[V_CongelamientoEsquemasEvaluacionDetallesAlumno] WHERE (IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno=@IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno AND IdEsquemaEvaluacionPGeneral=0) OR (IdEsquemaEvaluacionPGeneral=@IdEsquemaEvaluacionPGeneral AND IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno=0)";
                            Subfiltro = _dapper.QueryDapper(_queryfiltro, new {
                                IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno = esquemaevaluacion.IdCongelamientoPEspecificoEsquemaEvaluacionMatriculaAlumno ,
                                IdEsquemaEvaluacionPGeneral=esquemaevaluacion.IdEsquemaEvaluacionPGeneral
                            });
                            if (!string.IsNullOrEmpty(Subfiltro) && !Subfiltro.Contains("[]"))
                            {
                                Esquemas[i].EsquemasEvaluacion[j].EsquemasEvaluacionDetalle = JsonConvert.DeserializeObject<List<EsquemaEvaluacionDetalle_CongeladoDTO>>(Subfiltro);
                            }
                            j++;
                        }
                    }
                    j = 0;
                    i++;
                };

                return Esquemas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: EsquemaEvaluacionPgeneralRepositorio
        /// Autor: Miguel Mora
        /// Fecha: 10/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el esquema evaluacion predeterminado
        /// </summary>
        /// <param name=”idEsquemaEvaluacionPGeneral”>Identificador del esquema</param>
        /// <returns>bool</returns>
        public bool UpdateEsquemaEvaluacionPredefinido(int idEsquemaEvaluacionPGeneral)
        {
            try
            {
                var _queryfiltro = "ope.SP_ModificarEsquemaDeEvaluacionPredefinido";
                var Subfiltro = _dapper.QueryDapper(_queryfiltro, new { EsquemaPredeterminado=true, IdEsquemaEvaluacionPGeneral=idEsquemaEvaluacionPGeneral });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
