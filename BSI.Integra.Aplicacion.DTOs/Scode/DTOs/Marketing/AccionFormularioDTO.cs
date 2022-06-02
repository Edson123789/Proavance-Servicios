using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AccionFormularioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int UltimaLlamadaEjecutada { get; set; }
        public bool CamposSinValores { get; set; }
        public string NombreCamposSinValores { get; set; }
        public bool TodosCampos { get; set; }
        public int TiempoRedirecionamiento { get; set; }
        public bool CamposSegunProbabilidad { get; set; }
        public string NombreCamposSegunProbabilidad { get; set; }
        public int? NumeroClics { get; set; }
        public string Usuario { get; set; }
        public List<int> ListaCategoriaOrigen { get; set; }
        public List<AccionFormularioPorCampoContactoDTO> ListaCampoContacto { get; set; }
    }
}
