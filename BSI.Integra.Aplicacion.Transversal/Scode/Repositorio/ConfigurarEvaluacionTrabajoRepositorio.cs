using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ConfigurarEvaluacionTrabajoRepositorio : BaseRepository<TConfigurarEvaluacionTrabajo, ConfigurarEvaluacionTrabajoBO>
    {
        #region Metodos Base
        public ConfigurarEvaluacionTrabajoRepositorio() : base()
        {
        }
        public ConfigurarEvaluacionTrabajoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfigurarEvaluacionTrabajoBO> GetBy(Expression<Func<TConfigurarEvaluacionTrabajo, bool>> filter)
        {
            IEnumerable<TConfigurarEvaluacionTrabajo> listado = base.GetBy(filter);
            List<ConfigurarEvaluacionTrabajoBO> listadoBO = new List<ConfigurarEvaluacionTrabajoBO>();
            foreach (var itemEntidad in listado)
            {
                ConfigurarEvaluacionTrabajoBO objetoBO = Mapper.Map<TConfigurarEvaluacionTrabajo, ConfigurarEvaluacionTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfigurarEvaluacionTrabajoBO FirstById(int id)
        {
            try
            {
                TConfigurarEvaluacionTrabajo entidad = base.FirstById(id);
                ConfigurarEvaluacionTrabajoBO objetoBO = new ConfigurarEvaluacionTrabajoBO();
                Mapper.Map<TConfigurarEvaluacionTrabajo, ConfigurarEvaluacionTrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfigurarEvaluacionTrabajoBO FirstBy(Expression<Func<TConfigurarEvaluacionTrabajo, bool>> filter)
        {
            try
            {
                TConfigurarEvaluacionTrabajo entidad = base.FirstBy(filter);
                ConfigurarEvaluacionTrabajoBO objetoBO = Mapper.Map<TConfigurarEvaluacionTrabajo, ConfigurarEvaluacionTrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfigurarEvaluacionTrabajoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfigurarEvaluacionTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfigurarEvaluacionTrabajoBO> listadoBO)
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

        public bool Update(ConfigurarEvaluacionTrabajoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfigurarEvaluacionTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfigurarEvaluacionTrabajoBO> listadoBO)
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
        private void AsignacionId(TConfigurarEvaluacionTrabajo entidad, ConfigurarEvaluacionTrabajoBO objetoBO)
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

        private TConfigurarEvaluacionTrabajo MapeoEntidad(ConfigurarEvaluacionTrabajoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfigurarEvaluacionTrabajo entidad = new TConfigurarEvaluacionTrabajo();
                entidad = Mapper.Map<ConfigurarEvaluacionTrabajoBO, TConfigurarEvaluacionTrabajo>(objetoBO,
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

        public List<listaConfigurarEvaluacionTrabajoBO> ObtenerConfigurarEvaluacionTrabajoFiltro(int IdPGeneral, int IdSeccion, int Fila)
        {
            try
            {
                List<listaConfigurarEvaluacionTrabajoBO> capitulosFiltro = new List<listaConfigurarEvaluacionTrabajoBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select Id,IdTipoEvaluacionTrabajo,Nombre,Descripcion,IdDocumentoPw,ArchivoNombre,ArchivoCarpeta,IdPgeneral,IdSeccion,Fila,FechaCreacion,UsuarioCreacion,NombreTipoEvaluacion,DescripcionPregunta,HabilitarInstrucciones,HabilitarArchivo,HabilitarPreguntas,OrdenCapitulo FROM pla.V_RegistroConfigurarEvaluacionTrabajo Where IdPgeneral=@IdPGeneral AND IdSeccion=@IdSeccion AND Fila=@Fila";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { IdPgeneral = IdPGeneral, IdSeccion = IdSeccion, Fila = Fila });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<listaConfigurarEvaluacionTrabajoBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public registrosConfigurarEvaluacionTrabajoBO ObtenerConfigurarEvaluacionTrabajoPorIdEvaluacion(int Id)
        {
            try
            {
                registrosConfigurarEvaluacionTrabajoBO capitulosFiltro = new registrosConfigurarEvaluacionTrabajoBO();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select Id,IdTipoEvaluacionTrabajo,Nombre,Descripcion,IdDocumentoPw,ArchivoNombre,ArchivoCarpeta,IdPgeneral,IdSeccion,Fila,FechaCreacion,UsuarioCreacion,NombreTipoEvaluacion,DescripcionPregunta,HabilitarInstrucciones,HabilitarArchivo,HabilitarPreguntas,OrdenCapitulo FROM pla.V_RegistroConfigurarEvaluacionTrabajo Where Id=@Id";
                var SubfiltroCapitulo = _dapper.FirstOrDefault(_queryfiltrocapitulo, new { Id = Id });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<registrosConfigurarEvaluacionTrabajoBO>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public List<registroPreguntaTipoEvaluacionBO> ObtenerPreguntasAsignadosConfiguracionTipoEvaluacion(int IdConfigurarEvaluacionTrabajo)
        {
            try
            {
                List<registroPreguntaTipoEvaluacionBO> capitulosFiltro = new List<registroPreguntaTipoEvaluacionBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select IdPreguntaEvaluacionTrabajo, IdPregunta,IdConfigurarEvaluacionTrabajo,EnunciadoPregunta,IdPgeneral,IdPEspecifico FROM pla.V_PreguntasAsignadosConfiguracionTipoEvaluacion Where IdConfigurarEvaluacionTrabajo=@IdConfigurarEvaluacionTrabajo";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { IdConfigurarEvaluacionTrabajo = IdConfigurarEvaluacionTrabajo });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<registroPreguntaTipoEvaluacionBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public List<registroDocumentoSeccionesBO> ObtenerInstruccionEvaluacionTrabajoPorId(int IdPGeneral, int IdDocumento)
        {
            try
            {
                List<registroDocumentoSeccionesBO> capitulosFiltro = new List<registroDocumentoSeccionesBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select Id,Titulo,Contenido,OrdenWeb,ZonaWeb FROM pla.V_registroInstruccionDocumentoSeccion Where IdPGeneral=@IdPGeneral AND IdDocumento=@IdDocumento";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { IdPGeneral = IdPGeneral, IdDocumento = IdDocumento });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<registroDocumentoSeccionesBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public List<registroPreguntasProgramaEstructuraDTO> ObtenerPreguntasPorProgramaEstructuraFiltro(int IdPGeneral)
        {
            try
            {
                List<registroPreguntasProgramaEstructuraDTO> capitulosFiltro = new List<registroPreguntasProgramaEstructuraDTO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select Id,EnunciadoPregunta FROM pla.V_PreguntasPorPrograma Where OrdenFilaCapitulo IS NULL AND OrdenFilaSesion IS NULL AND IdPgeneral=@IdPGeneral";
                //var _queryfiltrocapitulo = "Select Id,EnunciadoPregunta FROM pla.V_PreguntasPorPrograma Where OrdenFilaSesion IS NULL AND IdPgeneral=@IdPGeneral";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { IdPgeneral = IdPGeneral });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<registroPreguntasProgramaEstructuraDTO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public List<registroDocumentoProgramaEstructuraDTO> ObtenerDocumentoProgramaGeneralTrabajosEvaluacionFiltro(int IdPGeneral)
        {
            try
            {
                List<registroDocumentoProgramaEstructuraDTO> capitulosFiltro = new List<registroDocumentoProgramaEstructuraDTO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                //var _queryfiltrocapitulo = "Select Id,EnunciadoPregunta FROM pla.V_PreguntasPorPrograma Where OrdenFilaCapitulo IS NULL AND OrdenFilaSesion IS NULL AND IdPgeneral=@IdPGeneral";
                var _queryfiltrocapitulo = "Select Id,Nombre FROM pla.V_DocumentoProgramaGeneralTrabajosEvaluacion Where IdPGeneral=@IdPGeneral";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { IdPgeneral = IdPGeneral });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<registroDocumentoProgramaEstructuraDTO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        public List<listaConfigurarEvaluacionTrabajoBO> ObtenerConfigurarProyectoFiltro(int IdPGeneral)
        {
            try
            {
                List<listaConfigurarEvaluacionTrabajoBO> capitulosFiltro = new List<listaConfigurarEvaluacionTrabajoBO>();
                //Select IdPGeneral, Nombre, Titulo, Contenido, NombreTitulo, NroCapitulo From pla.V_ListadoEstructuraProgramaCapitulosV2 Where IdPGeneral = @IdPGeneral
                var _queryfiltrocapitulo = "Select Id,IdTipoEvaluacionTrabajo,Nombre,Descripcion,IdDocumentoPw,ArchivoNombre,ArchivoCarpeta,IdPgeneral,IdSeccion,Fila,FechaCreacion,UsuarioCreacion,NombreTipoEvaluacion,DescripcionPregunta,HabilitarInstrucciones,HabilitarArchivo,HabilitarPreguntas,OrdenCapitulo FROM pla.V_RegistroConfigurarEvaluacionTrabajo Where IdPgeneral=@IdPGeneral AND IdTipoEvaluacionTrabajo=2";
                var SubfiltroCapitulo = _dapper.QueryDapper(_queryfiltrocapitulo, new { IdPgeneral = IdPGeneral});
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                {
                    capitulosFiltro = JsonConvert.DeserializeObject<List<listaConfigurarEvaluacionTrabajoBO>>(SubfiltroCapitulo);
                }
                return capitulosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }
    }
}
