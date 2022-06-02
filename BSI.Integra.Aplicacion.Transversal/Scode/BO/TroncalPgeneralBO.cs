using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TroncalPgeneralBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdTroncalPartner { get; set; }
        public int Duracion { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdBusqueda { get; set; }
        public int? IdMigracion { get; set; }
        private TroncalPgeneralRepositorio _repTroncalPrograma;
        public TroncalPgeneralBO()
        {

        }
        public TroncalPgeneralBO(int id)
        {
            _repTroncalPrograma = new TroncalPgeneralRepositorio();
            var TroncalPrograma = _repTroncalPrograma.FirstById(id);

            this.Id = TroncalPrograma.Id;
            this.IdTroncalPartner = TroncalPrograma.IdTroncalPartner;
            this.IdArea = TroncalPrograma.IdArea;
            this.IdSubArea = TroncalPrograma.IdSubArea;
            this.Codigo = TroncalPrograma.Codigo;
            this.IdBusqueda = TroncalPrograma.IdBusqueda;
            this.Nombre = TroncalPrograma.Nombre;
            this.FechaCreacion = TroncalPrograma.FechaCreacion;
            this.FechaModificacion = TroncalPrograma.FechaModificacion;
            this.UsuarioCreacion = TroncalPrograma.UsuarioCreacion;
            this.UsuarioModificacion = TroncalPrograma.UsuarioModificacion;
            this.Estado = TroncalPrograma.Estado;
            this.RowVersion = TroncalPrograma.RowVersion;
        }
        //DapperRepository _dapperRepository;
        //public List<TroncalPgeneralBO> TroncalPgenerealBySubArea;
        //public TroncalPgeneralBO()
        //{
        //    _dapperRepository = new DapperRepository();
        //    TroncalPgenerealBySubArea = new List<TroncalPgeneralBO>();
        //}

        //public void GetTroncalPgeneralBySubArea(int IdSubArea)
        //{
        //    string _queryTroncalPgeneral = "Select Id,Nombre,Codigo From pla.T_TroncalPgeneral Where Estado=1 and IdSubArea=@IdSubArea";
        //    var queryTroncalPgeneral = _dapperRepository.QueryDapper(_queryTroncalPgeneral, new { IdSubArea });
        //    TroncalPgenerealBySubArea = JsonConvert.DeserializeObject<List<TroncalPgeneralBO>>(queryTroncalPgeneral);
        //}
    }
}
