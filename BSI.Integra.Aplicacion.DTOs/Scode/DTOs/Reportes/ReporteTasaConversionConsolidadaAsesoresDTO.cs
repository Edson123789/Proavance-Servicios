using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteTasaConversionConsolidadaAsesoresDTO
    {
        public List<TCRM_ConsolidadTCAsesores> Consolidado { get; set; }
        public List<TCRM_TasaConversionByCategoriaDatoPaisDTO> Desagregado { get; set; }
    }
    public class TCRM_TasaConversionByCategoriaDatoPaisDTO
    {
        public int orden { get; set; }
        public string probabilidadDesc { get; set; }
        public string grupo { get; set; }
        public string nombreGrupo { get; set; }
        public string pais { get; set; }
        public string nombrePais { get; set; }
        public int tcMeta { get; set; }

        public List<TCRM_ConsolidadTCAsesores> listaMuyAlta { get; set; }


    }
    public class TCRM_ConsolidadTCAsesores
    {
        public int orden { get; set; }
        public int idSub { get; set; }
        public string nombreSub { get; set; }
        public int idcategoriaOrigen { get; set; }
        public int idCoordinador { get; set; }
        public string nombreCoordinador { get; set; }
        public int idasesor { get; set; }
        public string ca_nombre { get; set; }
        public int pais { get; set; }
        public string nombrePais { get; set; }
        public string probabilidad { get; set; }
        public string nombre { get; set; }
        public int grupo { get; set; }
        public string nombreGrupo { get; set; }
        public int inscritosMatriculados { get; set; }
        public int oportunidadesCerradas { get; set; }
        public decimal tasaConversion { get; set; }
        public int ordenp { get; set; }
        public string probabilidadDesc { get; set; }
        public int tcMeta { get; set; }
        public int centroCosto { get; set; }
        public string nombreCentroCosto { get; set; }
    }
    public class TCRM_OportunidadesOCByAsesorDTO
    {
        public int idCoordinador { get; set; }
        public int idOportunidadCC { get; set; }

        public string centrocosto { get; set; }
        public int idasesor { get; set; }

        public int Cantidad { get; set; }
        public double precioReal { get; set; }
        public int cantidadIS { get; set; }
        public double precioByIS { get; set; }
        public int tipo { get; set; }

    }
    public class TCRM_CentroCostoByAsesorDTO
    {
        public int idasesor { get; set; }
        public double precioPromedio { get; set; }
        public double ingresoReal { get; set; }
        public double ingresoMes { get; set; }
        public double DescuentoPromedio { get; set; }
        public int oportunidadesOCAnyIS { get; set; }
        public int oportunidadesOCTotal { get; set; }
        public bool estadoAsesor { get; set; }
        public double precioListaFinal { get; set; }//nuevos valores solo para calcular valor
        public int idcodigopais { get; set; }
        public double Descuento { get; set; }//nuevos valores solo para calcular valor

    }
    public class TCRM_CentroCostoByAsesorDetallesDTO
    {
        public int Id { get; set; }
        public string Alumno { get; set; }
        public string Asesor { get; set; }
        public int idCentroCosto { get; set; }
        public string nombreCC { get; set; }
        public int idAsesor { get; set; }
        public int idCodigoPais { get; set; }
        public double PrecioReal { get; set; }//nuevos valores solo para calcular valor
        public double PrecioSinDesc { get; set; }
        public double Descuento { get; set; }//nuevos valores solo para calcular valor
        public double Mes { get; set; }
        public string CodigoMatricula { get; set; }

    }
    public class TCRM_CentroCostoByAsesorgrupadoDTO
    {
        public int idasesor { get; set; }
        public double precioPromedio { get; set; }
        public double ingresoReal { get; set; }
        public double ingresoMes { get; set; }
        public double DescuentoPromedio { get; set; }
        public int oportunidadesOCAnyIS { get; set; }
        public int oportunidadesOCTotal { get; set; }
        public bool estadoAsesor { get; set; }

    }
    public class TCRM_PrecioDTO
    {
        public double precio { get; set; }

    }
    public class TCRM_CambioDeFaseDTO
    {
        public int? IdAsesor { get; set; }
        public int? IdCodigoPais { get; set; }
        public decimal? IngresoReal { get; set; }
        public decimal? IngresoMes { get; set; }
        public decimal? PrecioSinDesc { get; set; }
        public decimal? PrecioListaFinal { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? DescuentoPromedio { get; set; }
        public decimal? PrecioPromedio { get; set; }
        public int? OportunidadesOCAnyIS { get; set; }
        public int? OportunidadesOCTotal { get; set; }
        public decimal? Meta { get; set; }
        public bool EstadoAsesor { get; set; }
        public string Nombres { get; set; }
        public decimal? TCReal { get; set; }
        public decimal? TCMeta { get; set; }
        public decimal? TCReal_TCMeta { get; set; }
        public decimal? PP_IM_USD { get; set; }
        public decimal? PP_OC_USD { get; set; }
        public decimal? PorcentajeIngresoMes { get; set; }
        public decimal? IngresoMeta { get; set; }
        public decimal? IR_IM { get; set; }
    }
}
