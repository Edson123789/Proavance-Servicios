using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DocumentoSeccionPwBO : BaseBO
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public int IdDocumentoPw { get; set; }
        public int IdSeccionPw { get; set; }
        public bool VisibleWeb { get; set; }
        public int? ZonaWeb { get; set; }
        public int? OrdenWeb { get; set; }
        public int? IdSeccionTipoDetallePw { get; set; }
        public int? NumeroFila { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
    }
}
