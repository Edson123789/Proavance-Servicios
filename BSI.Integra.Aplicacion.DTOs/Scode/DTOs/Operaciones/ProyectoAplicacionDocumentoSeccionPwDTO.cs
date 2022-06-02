using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Operaciones
{
    public class ProyectoAplicacionDocumentoSeccionPwDTO
    {
        public int Id { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Valor { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdDocumentoPw { get; set; }
        public int IdProyectoAplicacionEntregaVersionPw { get; set; }
        public DateTime FechaCalificacion { get; set; }
        public string Usuario { get; set; }
    }

    public class ProyectoAplicacionDocumentoSeccionPwInsertarDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdDocumentoPw { get; set; }
        public int IdSeccionPw { get; set; }
        public int IdAlumnoPEspecifico { get; set; }
        public string Usuario { get; set; }
        public List<listaGridListaSeccionesDTO> ListaSecciones { get; set; }
    }
}
