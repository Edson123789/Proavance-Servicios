using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProcesadoDataActividadesRealizadasDTO
    {
        public int IdActividad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }

        // Informacion de LLamadas
        public string TiemposDuracionLlamadas {get;set;}
        public double MinutosTotalIntervaleLlamadas { get; set; }
        public double MinutosIntervale { get; set; }
        public double MinutosTotalTimbrado { get; set; }
        public double MinutosTotalContesto { get; set; }
        public double MinutosTotalPerdido { get; set; }
        public double MayorTiempo { get; set; }
        public string TiemposTresCX { get; set; }
        public string EstadosTresCX { get; set; }
        public string FechaLlamada { get; set; }
        public int TotalEjecutadas { get; set; }
        public int TotalNoEjecutadas { get; set; }
        public int TotalAsignacionManual { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public string NombreGrabacionTresCX { get; set; }
        public string NombreGrabacionIntegra { get; set; }
        public bool ExisteLlamadaExitosa { get; set; }

    }
}
