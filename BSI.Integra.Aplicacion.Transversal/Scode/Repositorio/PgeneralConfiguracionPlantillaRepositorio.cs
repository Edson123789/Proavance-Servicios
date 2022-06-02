using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PgeneralConfiguracionPlantillaRepositorio : BaseRepository<TPgeneralConfiguracionPlantilla, PgeneralConfiguracionPlantillaBO>
    {
        #region Metodos Base
        public PgeneralConfiguracionPlantillaRepositorio() : base()
        {
        }
        public PgeneralConfiguracionPlantillaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralConfiguracionPlantillaBO> GetBy(Expression<Func<TPgeneralConfiguracionPlantilla, bool>> filter)
        {
            IEnumerable<TPgeneralConfiguracionPlantilla> listado = base.GetBy(filter);
            List<PgeneralConfiguracionPlantillaBO> listadoBO = new List<PgeneralConfiguracionPlantillaBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralConfiguracionPlantillaBO objetoBO = Mapper.Map<TPgeneralConfiguracionPlantilla, PgeneralConfiguracionPlantillaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralConfiguracionPlantillaBO FirstById(int id)
        {
            try
            {
                TPgeneralConfiguracionPlantilla entidad = base.FirstById(id);
                PgeneralConfiguracionPlantillaBO objetoBO = new PgeneralConfiguracionPlantillaBO();
                Mapper.Map<TPgeneralConfiguracionPlantilla, PgeneralConfiguracionPlantillaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralConfiguracionPlantillaBO FirstBy(Expression<Func<TPgeneralConfiguracionPlantilla, bool>> filter)
        {
            try
            {
                TPgeneralConfiguracionPlantilla entidad = base.FirstBy(filter);
                PgeneralConfiguracionPlantillaBO objetoBO = Mapper.Map<TPgeneralConfiguracionPlantilla, PgeneralConfiguracionPlantillaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralConfiguracionPlantillaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralConfiguracionPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralConfiguracionPlantillaBO> listadoBO)
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

        public bool Update(PgeneralConfiguracionPlantillaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralConfiguracionPlantilla entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralConfiguracionPlantillaBO> listadoBO)
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
        private void AsignacionId(TPgeneralConfiguracionPlantilla entidad, PgeneralConfiguracionPlantillaBO objetoBO)
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

        private TPgeneralConfiguracionPlantilla MapeoEntidad(PgeneralConfiguracionPlantillaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralConfiguracionPlantilla entidad = new TPgeneralConfiguracionPlantilla();
                entidad = Mapper.Map<PgeneralConfiguracionPlantillaBO, TPgeneralConfiguracionPlantilla>(objetoBO,
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

        public List<PgeneralConfiguracionPlantillaDTO> ObtenerPgeneralConfiuracionPlantilla(int IdPgeneral)
        {
            try
            {

                List<PgeneralConfiguracionPlantillaDTO> ConfiguracionPlantilla = new List<PgeneralConfiguracionPlantillaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPlantillaBase, IdPlantillaFrontal,IdPlantillaPosterior,UltimaFechaRemplazarCertificado FROM pla.V_PGeneral_ConfiguracionPlantilla WHERE Estado = 1 and IdPgeneral=@IdPgeneral";
                var pgeneralDB = _dapper.QueryDapper(_query, new { IdPgeneral});
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    ConfiguracionPlantilla = JsonConvert.DeserializeObject<List<PgeneralConfiguracionPlantillaDTO>>(pgeneralDB);
                    foreach (var item in ConfiguracionPlantilla)
                    {
                        item.detalle = new List<PgeneralConfiguracionPlantillaDetalleDTO>();
                        string _querydetalle = "SELECT Id, IdPgeneralConfiguracionPlantilla,IdModalidadCurso,IdOperadorComparacion,NotaAprobatoria,DeudaPendiente FROM pla.V_PGeneral_ConfiguracionPlantillaDetalle WHERE Estado = 1 and IdPgeneralConfiguracionPlantilla=@IdConfiguracionPlantilla";
                        var pgeneralDetalle = _dapper.QueryDapper(_querydetalle, new {IdConfiguracionPlantilla= item.Id});
                        if (!string.IsNullOrEmpty(pgeneralDetalle) && !pgeneralDetalle.Contains("[]"))
                        {
                            item.detalle = JsonConvert.DeserializeObject<List<PgeneralConfiguracionPlantillaDetalleDTO>>(pgeneralDetalle);
                            foreach (var estados in item.detalle)
                            {
                                estados.IdEstadoMatricula = new List<PgeneralConfiguracionPlantillaEstadoMatriculaDTO>();
                                estados.IdSubEstadoMatricula = new List<PgeneralConfiguracionPlantillaSubEstadoMatriculaDTO>();
                                string _queryEstado = "SELECT IdEstadoMatricula as Id,IdEstadoMatricula FROM pla.T_PgeneralConfiguracionPlantillaEstadoMatricula WHERE Estado = 1 and IdPgeneralConfiguracionPlantillaDetalle=@IdPgeneralConfiguracionPlantillaDetalle";
                                var queryEstado = _dapper.QueryDapper(_queryEstado, new { IdPgeneralConfiguracionPlantillaDetalle = estados.Id });
                                if(!string.IsNullOrEmpty(queryEstado) && !queryEstado.Contains("[]"))
                                {
                                    estados.IdEstadoMatricula = JsonConvert.DeserializeObject<List<PgeneralConfiguracionPlantillaEstadoMatriculaDTO>>(queryEstado);
                                }

                                string _querySubEstado = "SELECT IdSubEstadoMatricula AS Id,IdSubEstadoMatricula FROM pla.T_PgeneralConfiguracionPlantillaSubEstadoMatricula WHERE Estado = 1 and IdPgeneralConfiguracionPlantillaDetalle=@IdPgeneralConfiguracionPlantillaDetalle";
                                var querySubEstado = _dapper.QueryDapper(_querySubEstado, new { IdPgeneralConfiguracionPlantillaDetalle = estados.Id });
                                if (!string.IsNullOrEmpty(querySubEstado) && !querySubEstado.Contains("[]"))
                                {
                                    estados.IdSubEstadoMatricula = JsonConvert.DeserializeObject<List<PgeneralConfiguracionPlantillaSubEstadoMatriculaDTO>>(querySubEstado);
                                }
                            }
                        }
                    }
                    
                }
                return ConfiguracionPlantilla;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<DatosGenerarCertificadoDTO> ObtenerDatosParaCertificados()
        {
            var rpta = new List<DatosGenerarCertificadoDTO>();
            string _query = "pla.SP_ObtenerDatosGenerarCertificados";
            string query = _dapper.QuerySPDapper(_query,null);

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<DatosGenerarCertificadoDTO>>(query);
            }
            return rpta;
        }

        public List<DatosGenerarCertificadoDTO> ObtenerDatosParaCertificadosporAlumno(int idAlumno)
        {
            var rpta = new List<DatosGenerarCertificadoDTO>();
            string _query = "pla.SP_ObtenerDatosGenerarCertificadosPorAlumno";
            string query = _dapper.QuerySPDapper(_query,new { IdAlumno = idAlumno});

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<DatosGenerarCertificadoDTO>>(query);
            }
            return rpta;
        }

        public DatosGenerarCertificadoDTO ObtenerDatosParaCertificadosporAlumnoIdMatriculaCabecera(int idMatriculaCabecera)
        {
            var rpta = new DatosGenerarCertificadoDTO();
            string _query = "[pla].[SP_ObtenerDatosGenerarCertificadosPorIdMatriculaCabecera]";
            string query = _dapper.QuerySPFirstOrDefault(_query, new { IdMatriculaCabecera = idMatriculaCabecera });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<DatosGenerarCertificadoDTO>(query);
            }
            return rpta;
        }
        public List<DatosGenerarCertificadoDTO> ObtenerDatosParaCertificadosporAlumnoPortalWeb(int idAlumno)
        {
            var rpta = new List<DatosGenerarCertificadoDTO>();
            string _query = "pla.SP_ObtenerDatosGenerarCertificadosPorAlumnoPortalWeb";
            string query = _dapper.QuerySPDapper(_query, new { IdAlumno = idAlumno });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<DatosGenerarCertificadoDTO>>(query);
            }
            return rpta;
        }
        ///Autor: Priscila Pacsi Gamboa
        ///Fecha: 01/10/2021
        /// <summary>
        /// Obtiene los datos necesarios para emitir los certificados del nuevo aula virtual
        /// </summary>
        /// <returns>DatosGenerarCertificadoDTO</returns>
        /// <param name="IdMatriculaCabecera"> Id de la matricula</param>
        public DatosGenerarCertificadoDTO ObtenerDatosParaCertificadosporAlumnoPortalWebPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            var rpta = new DatosGenerarCertificadoDTO();
            string _query = "pla.SP_ObtenerDatosGenerarCertificadosPorAlumnoPortalWebPorIdMatriculaCabecera";
            string query = _dapper.QuerySPFirstOrDefault(_query, new { IdMatriculaCabecera = idMatriculaCabecera });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<DatosGenerarCertificadoDTO>(query);
            }
            return rpta;
        }
        public DatosGenerarCertificadoDTO ObtenerDatosParaConstanciasPorMatricula(int IdMatriculaCabecera)
        {
            var rpta = new DatosGenerarCertificadoDTO();
            string _query = "pla.SP_ObtenerDatosConstanciaPorMatricula";
            string query = _dapper.QuerySPFirstOrDefault(_query, new { IdMatriculacabecera =IdMatriculaCabecera });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<DatosGenerarCertificadoDTO>(query);
            }
            return rpta;
        }
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<PgeneralConfiguracionPlantillaDTO> nuevos,int IdPlantillaBase)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT conf.Id " +
                                "FROM  pla.T_PgeneralConfiguracionPlantilla conf " +
                                "Inner join mkt.T_Plantilla pla on pla.Id = conf.IdPlantillaFrontal " +
                                "WHERE conf.Estado = 1 and IdPGeneral = @idPGeneral and pla.IdPlantillaBase="+IdPlantillaBase+" ";
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
    }
}
