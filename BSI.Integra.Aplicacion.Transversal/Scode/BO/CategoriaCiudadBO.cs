using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CategoriaCiudadBO : BaseBO
    {
        public int IdCategoriaPrograma { get; set; }
        public int? IdCiudad { get; set; }
        public string TroncalCompleto { get; set; }
        public int IdRegionCiudad { get; set; }

        //Adicionales
        public string NombreRegionCiudad { get; set; }
        public string NombreCategoriaPrograma { get; set; }

        private CategoriaCiudadRepositorio _repTroncalCiudad;




        DapperRepository _dapperRepository;
        public List<CategoriaCiudadBO> CategoriaCiudad;
        public CategoriaCiudadBO()
        {
            _dapperRepository = new DapperRepository();
            CategoriaCiudad = new List<CategoriaCiudadBO>();
        }

        public void GetAllTroncal()
        {
            string _queryTroncal = "SELECT Id,IdCategoriaPrograma,IdRegionCiudad,TroncalCompleto,NombreRegionCiudad,NombreCategoriaPrograma  FROM pla.V_Troncal WHERE Estado=1";
            var queryTroncal = _dapperRepository.QueryDapper(_queryTroncal,null);
            CategoriaCiudad = JsonConvert.DeserializeObject<List<CategoriaCiudadBO>>(queryTroncal);
        }
        
        public bool FilterTroncalByCiudadAndCategoria(int idCategoria, int idCiudad)
        {
            string _queryCategoriaPrograma = "SELECT top 1 Id,Categoria FROM pla.T_CategoriaPrograma WHERE Estado=1";
            var queryCategoriaPrograma = _dapperRepository.QueryDapper(_queryCategoriaPrograma,null);
            var resultado = JsonConvert.DeserializeObject<List<CategoriaProgramaDTO>>(queryCategoriaPrograma);

            if (resultado != null && resultado.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CategoriaCiudadBO(int id)
        {

            _repTroncalCiudad = new CategoriaCiudadRepositorio();
            var TroncalCiudad = _repTroncalCiudad.FirstById(id);
            if (TroncalCiudad != null)
            {
                this.Id = TroncalCiudad.Id;
                this.IdRegionCiudad = TroncalCiudad.IdCategoriaPrograma;
                this.IdCiudad = TroncalCiudad.IdCiudad;
                this.TroncalCompleto = TroncalCiudad.TroncalCompleto;
                this.IdRegionCiudad = TroncalCiudad.IdRegionCiudad;
                this.IdRegionCiudad = TroncalCiudad.IdRegionCiudad;
                this.Estado = TroncalCiudad.Estado;
                this.NombreRegionCiudad = TroncalCiudad.NombreRegionCiudad;
                this.NombreCategoriaPrograma = TroncalCiudad.NombreCategoriaPrograma;
                this.FechaCreacion = TroncalCiudad.FechaCreacion;
                this.FechaModificacion = TroncalCiudad.FechaModificacion;
                this.UsuarioCreacion = TroncalCiudad.UsuarioCreacion;
                this.UsuarioModificacion = TroncalCiudad.UsuarioModificacion;
                this.RowVersion = TroncalCiudad.RowVersion;
            }


           
    }
    }
}
