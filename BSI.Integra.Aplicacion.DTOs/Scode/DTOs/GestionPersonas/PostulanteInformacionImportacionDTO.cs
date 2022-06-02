using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PostulanteInformacionImportacionDTO
    {
        public int Id { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }
        public int CantidadTotal { get; set; }
        public int CantidadPrimerCriterio { get; set; }
        public int CantidadSegundoCriterio { get; set; }
        public int CantidadFinal { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class RegistrarImportacionDTO
    {
        public int Id { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int CantidadTotal { get; set; }
        public int CantidadPrimerCriterio { get; set; }
        public int CantidadSegundoCriterio { get; set; }
        public int CantidadFinal { get; set; }
        public string Usuario { get; set; }
    }
}
