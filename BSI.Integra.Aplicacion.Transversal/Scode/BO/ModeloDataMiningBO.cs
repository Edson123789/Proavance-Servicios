using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloDataMiningBO : BaseBO
    {
        public decimal? ProbabilidadInicial { get; set; }
        public int? IdProbabilidadRegistroPwInicial { get; set; }
        public decimal? ProbabilidadActual { get; set; }
        public int? IdProbabilidadRegistroPwActual { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdAlumno { get; set; }
        public DateTime? FechaCreacionContacto { get; set; }
        public DateTime? FechaCreacionOportunidad { get; set; }
        public int? DiasEntreCreacionContactoOportunidad { get; set; }
        public int? Nombres { get; set; }
        public int? Apellidos { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdPais { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? Email1 { get; set; }
        public int? TelefonoFijo { get; set; }
        public int? TelefonoMovil { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTamanioEmpresa { get; set; }
        public int? Ciiuempresa { get; set; }
        public int? TelefonoEmpresa { get; set; }
        public int? IdCiudadEmpresa { get; set; }
        public int? IdPaisEmpresa { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdArea { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdCategoriaPrograma { get; set; }
        public string ProgramaGeneralDuracion { get; set; }
        public int? IdPartner { get; set; }
        public int? IdPespecifico { get; set; }
        public int? Modalidad { get; set; }
        public int? PrecioProgramaEspecifico { get; set; }
        public int? PrecioProgramaEspecificoDolares { get; set; }
        public int? MonedaPrecioProgramaEspecifico { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdOrigen { get; set; }
        public string FaseMaximaAlcanzada { get; set; }
        public string FaseActual { get; set; }
        public int? IdActividadFinal { get; set; }
        public int? IdOcurrenciaFinal { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public Guid? IdMigracion { get; set; }

        public decimal? PuntoCorte { get; set; }

        ModeloDataMiningRepositorio _repModeloDataMining;

        public ModeloDataMiningBO() {
            _repModeloDataMining = new ModeloDataMiningRepositorio();
        }
		public ModeloDataMiningBO(integraDBContext contexto)
		{
			_repModeloDataMining = new ModeloDataMiningRepositorio(contexto);
		}
        #region Metodos


        public void ObtenerProbabilidad(int idOportunidad) {
            var probabilidad = _repModeloDataMining.ObtenerProbabilidad(idOportunidad);
            //this.IdPgeneral = valorProbabildad.IdProgramaGeneral;
            //var probabilidad = _repModeloDataMining.ObtenerProbabilidad(idAreaFormacion, idCargo, idIndustria, idAreaTrabajo, idCategoriaDato, valorProbabildad.IdProgramaGeneral);
            this.ProbabilidadInicial = probabilidad.Probabilidad == null ? 0 : probabilidad.Probabilidad;
            this.ProbabilidadActual = probabilidad.Probabilidad == null ? 0 : probabilidad.Probabilidad;
            this.IdProbabilidadRegistroPwInicial = probabilidad.IdProbabilidaRegistroPW == null ? ValorEstatico.IdProbabilidadRegistroSinProbabilidad : probabilidad.IdProbabilidaRegistroPW;
            this.IdProbabilidadRegistroPwActual = probabilidad.IdProbabilidaRegistroPW == null ? ValorEstatico.IdProbabilidadRegistroSinProbabilidad : probabilidad.IdProbabilidaRegistroPW;
            this.IdAreaFormacion = probabilidad.IdareaFormacion == null ? 0 : probabilidad.IdareaFormacion;
            this.IdCargo = probabilidad.IdCargo == null ? 0 : probabilidad.IdCargo;
            this.IdIndustria = probabilidad.IdIndustria == null ? 0 : probabilidad.IdIndustria;
            this.IdAreaTrabajo = probabilidad.IdAreaTrabajo == null ? 0 : probabilidad.IdAreaTrabajo;
            this.IdCategoriaOrigen = probabilidad.IdCategoriaDato == null ? 0 : probabilidad.IdCategoriaDato;
        }

        public void ObtenerProbabilidad(int? idAreaFormacion, int? idCargo, int? idIndustria, int? idAreaTrabajo, int idCategoriaDato, int idCentroCosto) {
            var valorProbabildad = _repModeloDataMining.ObtenerValoresProbabilidadProgramaGeneral(idCentroCosto);
            this.IdPgeneral = valorProbabildad.IdProgramaGeneral;
            var probabilidad = _repModeloDataMining.ObtenerProbabilidad(idAreaFormacion, idCargo, idIndustria, idAreaTrabajo, idCategoriaDato, valorProbabildad.IdProgramaGeneral);
            this.ProbabilidadInicial = probabilidad.Probabilidad == null ? 0 : probabilidad.Probabilidad;
            this.ProbabilidadActual = probabilidad.Probabilidad == null ? 0 : probabilidad.Probabilidad;
            this.IdProbabilidadRegistroPwInicial = probabilidad.IdProbabilidadRegistroPw == null ? ValorEstatico.IdProbabilidadRegistroSinProbabilidad : probabilidad.IdProbabilidadRegistroPw.Value;
            this.IdProbabilidadRegistroPwActual = probabilidad.IdProbabilidadRegistroPw == null ? ValorEstatico.IdProbabilidadRegistroSinProbabilidad : probabilidad.IdProbabilidadRegistroPw.Value;
            this.IdAreaFormacion = idAreaFormacion == null ? 0: idAreaFormacion;
            this.IdCargo = idCargo == null ? 0 : idCargo;
            this.IdIndustria = idIndustria == null ? 0 : idIndustria;
            this.IdAreaTrabajo = idAreaTrabajo == null ? 0 : idAreaTrabajo;
            this.IdCategoriaOrigen = idCategoriaDato == null ? 0 : idCategoriaDato;
        }

        #endregion


    }
}
