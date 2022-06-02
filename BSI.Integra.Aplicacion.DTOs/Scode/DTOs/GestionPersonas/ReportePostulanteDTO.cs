using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReportePostulanteDTO
    {
        public List<int> ListaPostulante { get; set; }
        public int? ListaProcesoSeleccion { get; set; }
        public int? IdGrupoComparacion { get; set; }
        public List<int> ListaEtapaProceso { get; set; }
        public List<int> ListaEstadoProceso { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool Check { get; set; }
        public List<int> ListaPostulanteGrupoComparacion { get; set; }
        public int? ListaProcesoSeleccionGrupoComparacion { get; set; }
    }

    public class OrdenEtapaTipoDTO
    {
        public int Id { get; set; }
        public int NroOrden { get; set; }
        public int TipoExamen { get; set; }
    }
    public class CantidadExamenTestDTO
    {
        public int Cantidad { get; set; }
        public int IdExamenTest { get; set; }
    }

    public class CalificacionAutomaticaDTO
    {
        public int Cantidad { get; set; }
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdExamenTest { get; set; }
    }


    public class ListaEtapaExamenesPorPostulante
    {
        public int Id { get; set; }
        public int IdProcesoSeleccionEtapa { get; set; }
        public int IdPostulante { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public int IdEstadoEtapaProcesoSeleccion { get; set; }
        public bool EsContactado { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string Nombre { get; set; }
        public bool EsCalificadoPorPostulante { get; set; }
        public int NroOrden { get; set; }
    }
}
