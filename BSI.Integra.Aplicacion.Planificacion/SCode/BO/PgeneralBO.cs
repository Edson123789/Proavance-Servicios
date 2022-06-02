using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class PgeneralBO : BaseEntity
    {
        public int? IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string PwImgPortada { get; set; }
        public string PwImgPortadaAlf { get; set; }
        public string PwImgSecundaria { get; set; }
        public string PwImgSecundariaAlf { get; set; }
        public int? IdPartner { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdCategoria { get; set; }
        public string PwEstado { get; set; }
        public string PwMostrarBsplay { get; set; }
        public string PwDuracion { get; set; }
        public int? IdBusqueda { get; set; }
        public int? IdChatZopim { get; set; }
        public string PgTitulo { get; set; }
        public string Codigo { get; set; }
        public string UrlImagenPortadaFr { get; set; }
        public string UrlBrochurePrograma { get; set; }
        public string UrlPartner { get; set; }
        public string UrlVersion { get; set; }
        public string PwTituloHtml { get; set; }
        public bool? EsModulo { get; set; }
        public string NombreCorto { get; set; }
        public int? IdPagina { get; set; }
        public int? ChatActivo { get; set; }
        public byte[] RowVersion { get; set; }

        public int CodigoPais { get; set; }//??

        AreaCapacitacionBO _areaCapacitacion;
        SubAreaCapacitacionBO _subAreaCapacitacion;

        integraDBContext _context;
        DapperRepository _dapperRepository;

        public PgeneralBO() {
            _context = new integraDBContext();
            _dapperRepository = new DapperRepository(_context);
            
        }

        /// <summary>
        /// Obtiene el Id,IdArea,IdSubArea de un programa general para ser usado en el chat del PortalWeb/SignalR 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProgramaGeneral ObtenerProgramaGeneralParaChatPorId(int id) {
            ProgramaGeneral programaGeneral = new ProgramaGeneral();
            var _query = string.Empty;
            _query = "SELECT Id AS Id, IdArea AS IdArea, IdSubArea AS IdSubArea, Estado AS Estado FROM com.V_TPGeneral_ObtenerParaValidarChatSignalR WHERE Estado = 1 AND Id = @id";
            var programaGeneralDB = _dapperRepository.FirstOrDefault(_query, new { id });
            programaGeneral = JsonConvert.DeserializeObject<ProgramaGeneral>(programaGeneralDB);
            return programaGeneral;
        }

        /// <summary>
        /// Obtiene todos los programas generales con estado = 1
        /// </summary>
        /// <returns></returns>
        public List<PgeneralBO> ObtenerTodosIds()
        {
            List<PgeneralBO> programaGeneral = new List<PgeneralBO>();
            var _query = string.Empty;
            _query = "SELECT Id FROM [com].[V_TPGeneral_ObtenerIds] WHERE estado = 1 GROUP BY Id";
            var programaGeneralDB = _dapperRepository.QueryDapper(_query);
            programaGeneral = JsonConvert.DeserializeObject<List<PgeneralBO>>(programaGeneralDB);
            return programaGeneral;
        }
        /// <summary>
        /// Obtiene todos los programas generales por id area
        /// </summary>
        /// <param name="listaAreas"></param>
        /// <returns></returns>
        public List<PgeneralBO> ObtenerTodosPorIdArea(List<AreaCapacitacionBO> listaAreas)
        {
            string listAreas = string.Join(",", listaAreas.Select(w => w.Id));

            List<PgeneralBO> programaGeneral = new List<PgeneralBO>();
            var _query = string.Empty;
            _query = "SELECT Id FROM [com].[V_TPGeneral_ObtenerIds] AS PG INNER JOIN (SELECT Item FROM conf.F_Splitstring(@listAreas,',')) AS L ON PG.IdArea = L.item WHERE PG.Estado = 1 GROUP BY Id";
            var programaGeneralDB = _dapperRepository.QueryDapper(_query,new { listAreas});
            programaGeneral = JsonConvert.DeserializeObject<List<PgeneralBO>>(programaGeneralDB);
            return programaGeneral;
        }

        /// <summary>
        /// Obtiene todos los programas generales por id sub area
        /// </summary>
        /// <param name="listaSubAreas"></param>
        /// <returns></returns>
        public List<PgeneralBO> ObtenerTodosPorIdSubArea(List<SubAreaCapacitacionBO> listaSubAreas)
        {
            string listSubAreas = string.Join(",", listaSubAreas.Select(w => w.Id));
            
            List<PgeneralBO> programaGeneral = new List<PgeneralBO>();
            var _query = string.Empty;
            _query = "SELECT Id FROM [com].[V_TPGeneral_ObtenerIds] AS PG INNER JOIN (SELECT Item FROM conf.F_Splitstring(@listSubAreas,',')) AS L ON PG.IdSubArea = L.item WHERE PG.Estado = 1 GROUP BY Id";
            var programaGeneralDB = _dapperRepository.QueryDapper(_query, new { listSubAreas });
            programaGeneral = JsonConvert.DeserializeObject<List<PgeneralBO>>(programaGeneralDB);
            return programaGeneral;
        }

        public ProgramaGeneralTroncal ObtenerProgramaGeneralParaPespecificoPorId(int idProgramaGeneral)
        {
            string _queryPgeneral = "Select Id,Nombre,Codigo,IdTroncalPartner,Duracion,IdArea,IdSubArea,IdCategoria,NombreCategoria From pla.V_TTroncalPgeneral_ObtenerInformacion Where Estado=1 and Id=@IdProgramaGeneral";
            var queryPgeneral = _dapperRepository.FirstOrDefault(_queryPgeneral, new { IdProgramaGeneral = idProgramaGeneral });
            ProgramaGeneralTroncal pgeneral = JsonConvert.DeserializeObject<ProgramaGeneralTroncal>(queryPgeneral);

            return pgeneral;
        }

        /// <summary>
        /// Obtiene todas las subareas capacitacion para utilizar en combobox
        /// </summary>
        /// <returns></returns>
        public List<ProgramaGeneralFiltroDTO> ObtenerTodoFiltro()
        {
            List<ProgramaGeneralFiltroDTO> programaGeneral = new List<ProgramaGeneralFiltroDTO>();
            string _query = string.Empty;
            _query = "SELECT Id,Nombre,IdSubAreaCapacitacion FROM pla.V_TProgramaGeneral_ObtenerIdNombreSubAreaCapacitacion Where Estado = 1";
            var subAreaCappacitacionDB = _dapperRepository.QueryDapper(_query);
            programaGeneral  = JsonConvert.DeserializeObject<List<ProgramaGeneralFiltroDTO>>(subAreaCappacitacionDB);
            return programaGeneral;
        }


        public RegistroProgramaGeneral RegistroProgramaGeneral()
        {
            try
            {
                RegistroProgramaGeneral _registroProgramaGeneral = new RegistroProgramaGeneral(); ;

                string _queryRegistroProgramaGeneral = string.Empty;

                _queryRegistroProgramaGeneral = "SELECT Id,IdPGeneral,Nombre,Pw_ImgPortada,Pw_ImgPortadaAlf,Pw_ImgSecundaria,Pw_ImgSecundariaAlf,IdPartner,IdArea,IdSubArea,IdCategoria,Pw_estado,Pw_mostrarBSPlay,pw_duracion,IdBusqueda,IdChatZopim,Pg_titulo,Codigo,UrlImagenPortadaFr,UrlBrochurePrograma,UrlPartner,UrlVersion,Pw_tituloHtml,EsModulo,NombreCorto,IdPagina,ChatActivo,Estado FROM pla.V_RegistrosProgramaGeneral WHERE Estado=1";

                var _listaRegistros = _dapperRepository.QueryDapper(_queryRegistroProgramaGeneral);
                ICollection<PgeneralBO> listaPGenerales = JsonConvert.DeserializeObject<ICollection<PgeneralBO>>(_listaRegistros);

                _registroProgramaGeneral.listaProgramaGeneral = listaPGenerales;

                _areaCapacitacion = new AreaCapacitacionBO();
                _subAreaCapacitacion = new SubAreaCapacitacionBO();

                _registroProgramaGeneral.listaAreasFiltro = _areaCapacitacion.listaFiltroAreaCapacitacion();
                _registroProgramaGeneral.listaSubAreasFiltro = _subAreaCapacitacion.listaFiltroSubAreaCapacitacionPorArea();

                return _registroProgramaGeneral;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public class ProgramaGeneral {
            public int Id { get; set; }
            public int IdArea { get; set; }
            public int IdSubArea { get; set; }
        }

        

    }
    public class ProgramaGeneralTroncal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdTroncalPatner { get; set; }
        public int Duracion { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
    }

    public class ProgramaGeneralRegistros
    {
        integraDBContext _context;
        DapperRepository _dapperRepository;
        public List<PgeneralBO> listaProgramaGeneral;

        public ProgramaGeneralRegistros()
        {
            _context = new integraDBContext();
            _dapperRepository = new DapperRepository(_context);
        }

        public PgeneralBO ObtenerProgramaGeneralById(int idProgramaGeneral)
        {
            string _queryPgeneral = "Select Id,Nombre From pla.V_TPGeneral_Nombre Where Estado=1 and Id=@IdProgramaGeneral";
            var queryPgeneral = _dapperRepository.FirstOrDefault(_queryPgeneral, new { IdProgramaGeneral = idProgramaGeneral });
            PgeneralBO Pgeneral = JsonConvert.DeserializeObject<PgeneralBO>(queryPgeneral);

            return Pgeneral;
        }
    }

    public partial class RegistroProgramaGeneral
    {
        public virtual ICollection<PgeneralBO> listaProgramaGeneral { get; set; }
        public virtual ICollection<AreaCapacitacionFiltro> listaAreasFiltro { get; set; }
        public virtual ICollection<SubAreaCapacitacionConfiguracionBO> listaSubAreasFiltro { get; set; }

    }
}
